using Asp.Versioning.Builder;

namespace MicroServiceV2.Order.API.Endpoints.Orders
{
    public static class OrderEndpointExtension
    {
        public static void AddOrderGroupEndpointExtension(this WebApplication app, ApiVersionSet apiVersionSet)
        {
            app.MapGroup("api/v{version:apiVersion}/orders")
                .WithTags("Orders")
                .WithApiVersionSet(apiVersionSet)
                .CreateOrderGroupItemEndpoint()
                .GetOrdersGroupItemEndpoint()
                .RequireAuthorization("Password");
        }
    }
}
