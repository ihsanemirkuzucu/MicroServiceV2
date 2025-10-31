using System.Text;

namespace MicroServiceV2.Order.Domain.Entities
{
    //Anemic Model => Rich Domain Model
    public class Order : BaseEntity<Guid>
    {
        public string Code { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public Guid CustomerId { get; set; }
        public OrderStatus Status { get; set; }
        public int AddressId { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal? DiscountRate { get; set; }
        public Guid? PaymentId { get; set; }
        public List<OrderItem> OrderItems { get; set; } = new();
        public Address Address { get; set; } = null!;

        // Generate a random 10-digit order code.
        public static string GenerateCode()
        {
            var random = new Random();
            var orderCode = new StringBuilder(10);
            for (int i = 0; i < 10; i++)
            {
                orderCode.Append(random.Next(0, 10));
            }
            return orderCode.ToString();
        }

        // Create a new order with the specified customer ID, discount rate, and address.
        public static Order CreateUnpaidOrder(Guid customerId, decimal? discountRate, int addressId)
        {
            return new Order
            {
                Id = Guid.CreateVersion7(),
                Code = GenerateCode(),
                CustomerId = customerId,
                CreatedDate = DateTime.Now,
                Status = OrderStatus.WaitingForPayment,
                AddressId = addressId,
                DiscountRate = discountRate,
                TotalPrice = 0
            };
        }

        // Create a new order with the specified customer ID and discount rate.
        public static Order CreateUnpaidOrder(Guid customerId, decimal? discountRate)
        {
            return new Order
            {
                Id = Guid.CreateVersion7(),
                Code = GenerateCode(),
                CustomerId = customerId,
                CreatedDate = DateTime.Now,
                Status = OrderStatus.WaitingForPayment,
                DiscountRate = discountRate,
                TotalPrice = 0
            };
        }

        // Add an item to the order with the specified product ID, name, and unit price.
        public void AddOrderItem(Guid productId, string productName, decimal unitPrice)
        {
            var orderItem = new OrderItem();
            if (DiscountRate.HasValue)
            {
                unitPrice -= unitPrice * (DiscountRate.Value / 100);
            }

            orderItem.SetItem(productId, productName, unitPrice);
            OrderItems.Add(orderItem);
            CalculateTotalPrice();
        }

        // Apply a discount to the order.
        public void ApplyDiscount(decimal discountRate)
        {
            if (discountRate < 0 || discountRate > 100)
            {
                throw new ArgumentException("Discount rate must be between 0 and 100");
            }
            DiscountRate = discountRate;
            CalculateTotalPrice();
        }

        // Set the payment ID and update the order status to paid.
        public void SetPaidStatus(Guid paymentId)
        {
            PaymentId = paymentId;
            Status = OrderStatus.Paid;
        }

        // Calculate the total price of the order based on the items and any discount applied.
        private void CalculateTotalPrice()
        {
            TotalPrice = OrderItems.Sum(item => item.UnitPrice);
        }
    }

}
