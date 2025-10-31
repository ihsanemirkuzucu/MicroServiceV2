using MicroServiceV2.Basket.API;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();// Custom common Extensions from 'ServiceDefaults' project

builder.Services.AddOpenApi();
// .Net 9.0'da default olarak yok
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Custom common Extensions
builder.Services.AddCommonServiceExtension(typeof(BasketAssembly));
builder.Services.AddMassTransitExtension(builder.Configuration);
builder.Services.AddVersioningExtension();
builder.Services.AddAuthenticationAndAuthorizationServiceExtension(builder.Configuration);

//Custom Basket Extensions(It only concerns Basket.API)
//builder.Services.AddBasketExtension(builder.Configuration); //app host ile iletişime geçeceği için kullanılmayacak.
builder.AddRedisDistributedCache("redis-db-basket"); // Redis Cache için Distributed Cache ekleniyor. apphost üzerinden geliyor.

var app = builder.Build();

app.MapDefaultEndpoints();

//Custom Endpoint Extensions
app.UseExceptionHandler(x => { });
app.AddBasketGroupEndpointExtension(app.AddVersioningSetExtension());

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    //Swagger UI için gerekli middlewareler
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.Run();

