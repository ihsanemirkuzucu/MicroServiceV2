using FluentValidation;
using FluentValidation.AspNetCore;
using MicroServiceV2.Shared.ExceptionHandlers;
using MicroServiceV2.Shared.Services;
using Microsoft.Extensions.DependencyInjection;

namespace MicroServiceV2.Shared.Extensions
{
    public static class CommonServiceExtension
    {
        public static IServiceCollection AddCommonServiceExtension(this IServiceCollection services,Type assembly)
        {
            services.AddHttpContextAccessor();

            services.AddMediatR(x => x.RegisterServicesFromAssemblyContaining(assembly));

            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssemblyContaining(assembly);

            services.AddAutoMapper(assembly);

            services.AddScoped<IIdentityService, IdentityService>();
            services.AddExceptionHandler<GlobalExceptionHandler>();

            return services;
        }
    }
}
