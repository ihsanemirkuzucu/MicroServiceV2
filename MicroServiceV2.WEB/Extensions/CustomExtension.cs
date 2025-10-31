using MicroServiceV2.WEB.DelegateHandler;
using MicroServiceV2.WEB.ExceptionHandlers;
using MicroServiceV2.WEB.Pages.Auth.SignIn;
using MicroServiceV2.WEB.Pages.Auth.SignUp;
using MicroServiceV2.WEB.Services;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace MicroServiceV2.WEB.Extensions
{
    public static class CustomExtension
    {
        public static IServiceCollection AddCustomApplicationExtension(this IServiceCollection services)
        {

            services.AddHttpClient<SignUpService>();
            services.AddHttpClient<SignInService>();

            services.AddScoped<TokenService>();
            services.AddScoped<CatalogService>();
            services.AddScoped<UserService>();
            services.AddScoped<BasketService>();
            services.AddScoped<OrderService>();

            services.AddExceptionHandler<UnauthorizedAccessExceptionHandler>();

            services.AddScoped<AuthenticatedHttpClientHandler>();
            services.AddScoped<ClientAuthenticatedHttpClientHandler>();
            services.AddHttpContextAccessor();
            services.AddAuthentication(configureOption =>
            {
                configureOption.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                configureOption.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            }).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
            {
                options.LoginPath = "/Auth/SignIn";
                options.ExpireTimeSpan = TimeSpan.FromDays(60);
                options.Cookie.Name = "MicroServiceV2WebCookie";
                options.AccessDeniedPath = "/Auth/AccessDenied";
            });

            services.AddAuthorization();

            return services;
        }
    }
}
