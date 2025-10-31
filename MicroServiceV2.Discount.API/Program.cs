using MicroServiceV2.Basket.API;
using MicroServiceV2.Discount.API;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();// Custom common Extensions from 'ServiceDefaults' project


builder.Services.AddOpenApi();

//.Net 9.0'da default olarak yok
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Custom Extensions
builder.Services.AddOptionExtension();
builder.Services.AddDatabaseServiceExtension();
builder.Services.AddVersioningExtension();
builder.Services.AddCommonServiceExtension(typeof(DiscountAssembly));
builder.Services.AddMassTransitExtension(builder.Configuration);
builder.Services.AddAuthenticationAndAuthorizationServiceExtension(builder.Configuration);




var app = builder.Build();

app.MapDefaultEndpoints();

//Custom Endpoint Extensions
app.UseExceptionHandler(x => { });
app.AddDiscountGroupEndpointExtension(app.AddVersioningSetExtension());





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

