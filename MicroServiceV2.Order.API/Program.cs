using MicroServiceV2.Bus;
using MicroServiceV2.Order.API;
using MicroServiceV2.Order.API.Endpoints.Orders;
using MicroServiceV2.Order.Application;
using MicroServiceV2.Order.Persistence;
using MicroServiceV2.Shared.Extensions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();// Custom common Extensions from 'ServiceDefaults' project



builder.Services.AddOpenApi();
//.Net 9.0'da default olarak yok
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Custom common Extensions
builder.Services.AddCommonServiceExtension(typeof(OrderApplicationAssembly));
builder.Services.AddCommonMassTransitExtension(builder.Configuration);
builder.Services.AddVersioningExtension();
builder.Services.AddAuthenticationAndAuthorizationServiceExtension(builder.Configuration);

//Custom Order Extensions(It only concerns Order.API)
builder.AddSqlServerDbContext<AppDbContext>("order-db-aspire");// .Net Aspire*
builder.Services.AddOrderExtension(builder.Configuration);


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate();
}

app.MapDefaultEndpoints();

//Custom Endpoint Extensions
app.UseExceptionHandler(x => { });
app.AddOrderGroupEndpointExtension(app.AddVersioningSetExtension());



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


