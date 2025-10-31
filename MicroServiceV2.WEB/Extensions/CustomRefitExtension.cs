using MicroServiceV2.WEB.DelegateHandler;
using MicroServiceV2.WEB.Options;
using MicroServiceV2.WEB.Services.Refit;
using Refit;

namespace MicroServiceV2.WEB.Extensions
{
    public static class CustomRefitExtension
    {
        public static IServiceCollection AddRefitExtension(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddRefitClient<ICatalogRefitService>().ConfigureHttpClient(configure =>
                {
                    var microServiceOption = configuration.GetSection(nameof(MicroServiceOption)).Get<MicroServiceOption>();
                    //configure.BaseAddress = new Uri(microServiceOption!.Catalog.BaseAddress);
                    configure.BaseAddress = new Uri("http://microservicev2-catalog-api");//.net core aspire
                }).AddHttpMessageHandler<AuthenticatedHttpClientHandler>()
                .AddHttpMessageHandler<ClientAuthenticatedHttpClientHandler>();

            services.AddRefitClient<IBasketRefitService>().ConfigureHttpClient(configure =>
                {
                    var microServiceOption = configuration.GetSection(nameof(MicroServiceOption)).Get<MicroServiceOption>();
                    //configure.BaseAddress = new Uri(microServiceOption!.Basket.BaseAddress);eski
                    configure.BaseAddress = new Uri("http://microservicev2-basket-api");//.net core aspire
                }).AddHttpMessageHandler<AuthenticatedHttpClientHandler>()
                .AddHttpMessageHandler<ClientAuthenticatedHttpClientHandler>();

            services.AddRefitClient<IDiscountRefitService>().ConfigureHttpClient(configure =>
                {
                    var microServiceOption = configuration.GetSection(nameof(MicroServiceOption)).Get<MicroServiceOption>();
                    // configure.BaseAddress = new Uri(microServiceOption!.Discount.BaseAddress);
                    configure.BaseAddress = new Uri("http://microservicev2-discount-api");//.net core aspire
                }).AddHttpMessageHandler<AuthenticatedHttpClientHandler>()
                .AddHttpMessageHandler<ClientAuthenticatedHttpClientHandler>();

            services.AddRefitClient<IOrderRefitService>().ConfigureHttpClient(configure =>
                {
                    var microServiceOption = configuration.GetSection(nameof(MicroServiceOption)).Get<MicroServiceOption>();
                   // configure.BaseAddress = new Uri(microServiceOption!.Order.BaseAddress);
                   configure.BaseAddress = new Uri("http://microservicev2-order-api");//.net core aspire
                }).AddHttpMessageHandler<AuthenticatedHttpClientHandler>()
                .AddHttpMessageHandler<ClientAuthenticatedHttpClientHandler>();
            return services;
        }
    }
}
