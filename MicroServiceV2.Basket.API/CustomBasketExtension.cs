namespace MicroServiceV2.Basket.API
{
    public static class CustomBasketExtension
    {
        public static IServiceCollection AddBasketExtension(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration.GetConnectionString("Redis");
            });

            services.AddScoped<BasketService>();

            return services;
        }
    }
}
