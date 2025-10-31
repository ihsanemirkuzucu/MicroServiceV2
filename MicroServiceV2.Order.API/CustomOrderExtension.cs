using MicroServiceV2.Order.Application.BackgroundServices;
using MicroServiceV2.Order.Application.Contract.Refit;
using MicroServiceV2.Order.Application.Contract.Refit.PaymentService;
using MicroServiceV2.Order.Application.Contract.Repositories;
using MicroServiceV2.Order.Application.Contract.UnitOfWork;
using MicroServiceV2.Order.Persistence;
using MicroServiceV2.Order.Persistence.Repositories;
using MicroServiceV2.Order.Persistence.UnitOfWork;
using MicroServiceV2.Shared.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Refit;

namespace MicroServiceV2.Order.API
{
    public static class CustomOrderExtension
    {
        public static IServiceCollection AddOrderExtension(this IServiceCollection services,IConfiguration configuration)
        {
            #region Refit

            services.AddScoped<AuthenticatedHttpClientHandler>();
            services.AddScoped<ClientAuthenticatedHttpClientHandler>();

            services.AddOptions<IdentityOption>().BindConfiguration(nameof(IdentityOption)).ValidateDataAnnotations().ValidateOnStart();
            services.AddSingleton<IdentityOption>(sp => sp.GetRequiredService<IOptions<IdentityOption>>().Value);

            services.AddOptions<ClientSecretOptions>().BindConfiguration(nameof(ClientSecretOptions)).ValidateDataAnnotations().ValidateOnStart();
            services.AddSingleton<ClientSecretOptions>(sp => sp.GetRequiredService<IOptions<ClientSecretOptions>>().Value);

            services.AddRefitClient<IPaymentService>().ConfigureHttpClient(configure =>
            {
                var addressUrlOption = configuration.GetSection(nameof(AddressUrlOption)).Get<AddressUrlOption>();
                configure.BaseAddress = new Uri(addressUrlOption!.PaymentUrl);
            }).AddHttpMessageHandler<AuthenticatedHttpClientHandler>()
               .AddHttpMessageHandler<ClientAuthenticatedHttpClientHandler>();

            #endregion


            services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //services.AddDbContext<AppDbContext>(options =>
            //{
            //    options.UseSqlServer(configuration.GetConnectionString("SqlServer"));
            //});

            services.AddHostedService<CheckPaymentStatusOrderBackgroundService>();

            return services;
        }
    }
}
