using MicroServiceV2.Bus;
using MicroServiceV2.File.API.Consumers;

namespace MicroServiceV2.File.API
{
    public static class MassTransitConfigurationExtension
    {
        public static IServiceCollection AddMassTransitExtension(this IServiceCollection services,
            IConfiguration configuration)
        {
            var busOptions = configuration.GetSection(nameof(BusOption)).Get<BusOption>();
            services.AddMassTransit(configure =>
            {
                configure.AddConsumer<UploadCoursePictureCommandConsumer>();
                configure.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host(new Uri($"rabbitmq://{busOptions.Address}:{busOptions.Port}"), host =>
                    {
                        host.Username(busOptions.UserName);
                        host.Password(busOptions.Password);
                    });
                    // cfg.ConfigureEndpoints(ctx);
                    cfg.ReceiveEndpoint("file-microservice.upload-course-picture-command.queue",
                        e =>
                        {
                            e.ConfigureConsumer<UploadCoursePictureCommandConsumer>(ctx);

                        });
                });
            });

            return services;
        }
    }
}
