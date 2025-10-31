using MassTransit;
using MediatR;
using MicroServiceV2.Bus.Events;
using MicroServiceV2.Order.Application.Contract.Refit.PaymentService;
using MicroServiceV2.Order.Application.Contract.Repositories;
using MicroServiceV2.Order.Application.Contract.UnitOfWork;
using MicroServiceV2.Order.Domain.Entities;
using MicroServiceV2.Shared;
using MicroServiceV2.Shared.Services;
using System.Net;

namespace MicroServiceV2.Order.Application.Features.Orders.Create
{
    public class CreateOrderCommandHandler(
        IOrderRepository orderRepository,
        IGenericRepository<int, Address> addressRepository,
        IUnitOfWork unitOfWork,
        IIdentityService identityService,
        IPublishEndpoint publishEndpoint,
        IPaymentService paymentService)
        : IRequestHandler<CreateOrderCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            if (!request.Items.Any())
            {
                return ServiceResult.Error("Order Items Not Found", "Order must have at least one item.", HttpStatusCode.BadRequest);
            }

            var newAddress = new Address()
            {
                Province = request.Address.Province,
                District = request.Address.District,
                Street = request.Address.Street,
                ZipCode = request.Address.ZipCode,
                Line = request.Address.Line
            };

            var order = Domain.Entities.Order.CreateUnpaidOrder(identityService.UserId, request.DiscountRate, newAddress.Id);
            foreach (var orderItem in request.Items)
            {
                order.AddOrderItem(orderItem.ProductId, orderItem.ProductName, orderItem.UnitPrice);
            }
            order.Address = newAddress;

            orderRepository.Add(order);
            await unitOfWork.CommitAsync(cancellationToken);

            //payment işlemi
            CreatePaymentRequest paymentRequest = new CreatePaymentRequest(
                order.Code,
                request.Payment.CardNumber,
                request.Payment.CardHolderName,
                request.Payment.Expiration,
                request.Payment.Cvc,
                order.TotalPrice);
            var paymentResponse = await paymentService.CreateAsync(paymentRequest);

            if (!paymentResponse.Status)
                return ServiceResult.Error(paymentResponse.ErrorMessage!,HttpStatusCode.InternalServerError);

            order.SetPaidStatus(paymentResponse.PaymentId!.Value);

            orderRepository.Update(order);
            await unitOfWork.CommitAsync(cancellationToken);

            //Event Fırlatma
            await publishEndpoint.Publish(new OrderCreatedEvent(order.Id, identityService.UserId), cancellationToken);

            return ServiceResult.SuccessAsNoContent();

        }
    }
}
