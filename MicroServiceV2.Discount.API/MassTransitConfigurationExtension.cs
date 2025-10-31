using MicroServiceV2.Discount.API.Consumers;

namespace MicroServiceV2.Basket.API
{
    public static class MassTransitConfigurationExtension
    {
        public static IServiceCollection AddMassTransitExtension(this IServiceCollection services,
            IConfiguration configuration)
        {
            var busOptions = configuration.GetSection(nameof(BusOption)).Get<BusOption>();
            services.AddMassTransit(configure =>
            {
                configure.AddConsumer<OrderCreatedEventConsumer>();
                configure.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host(new Uri($"rabbitmq://{busOptions.Address}:{busOptions.Port}"), host =>
                    {
                        host.Username(busOptions.UserName);
                        host.Password(busOptions.Password);
                    });
                    // cfg.ConfigureEndpoints(ctx);
                    cfg.ReceiveEndpoint("discount-microservice.order-created.queue",
                        e =>
                        {
                            e.ConfigureConsumer<OrderCreatedEventConsumer>(ctx);

                        });
                });
            });

            return services;
        }
    }
}
