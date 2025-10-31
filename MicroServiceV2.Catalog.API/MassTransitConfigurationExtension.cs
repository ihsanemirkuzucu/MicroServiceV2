using MicroServiceV2.Catalog.API.Consumers;

namespace MicroServiceV2.Catalog.API
{
    public static class MassTransitConfigurationExtension
    {
        public static IServiceCollection AddMassTransitExtension(this IServiceCollection services,
            IConfiguration configuration)
        {
            var busOptions = configuration.GetSection(nameof(BusOption)).Get<BusOption>();
            services.AddMassTransit(configure =>
            {
                configure.AddConsumer<CoursePictureUploadedEventConsumer>();
                configure.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host(new Uri($"rabbitmq://{busOptions.Address}:{busOptions.Port}"), host =>
                    {
                        host.Username(busOptions.UserName);
                        host.Password(busOptions.Password);
                    });
                    // cfg.ConfigureEndpoints(ctx);
                    cfg.ReceiveEndpoint("catalog-microservice.course-picture-picture-uploaded.queue",
                        e =>
                        {
                            e.ConfigureConsumer<CoursePictureUploadedEventConsumer>(ctx);

                        });
                });
            });

            return services;
        }
    }
}
