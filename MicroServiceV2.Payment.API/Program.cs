using MicroServiceV2.Bus;
using MicroServiceV2.Payment.API;
using MicroServiceV2.Payment.API.Features.Payments;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();// Custom common Extensions from 'ServiceDefaults' project

builder.Services.AddOpenApi();
// .Net 9.0'da default olarak yok
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Custom common Extensions
builder.Services.AddCommonServiceExtension(typeof(PaymentAssembly));
builder.Services.AddCommonMassTransitExtension(builder.Configuration);
builder.Services.AddVersioningExtension();
builder.Services.AddAuthenticationAndAuthorizationServiceExtension(builder.Configuration);

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseInMemoryDatabase("payment-in-memory-db");
});

var app = builder.Build();

app.MapDefaultEndpoints();

// Custom endpoint Extensions
app.UseExceptionHandler(x => { });
app.AddPaymentGroupEndpointExtension(app.AddVersioningSetExtension());

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
