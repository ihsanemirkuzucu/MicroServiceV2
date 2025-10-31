using MicroServiceV2.Catalog.API;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();// Custom common Extensions from 'ServiceDefaults' project

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Custom Extensions
builder.Services.AddOptionExtension();
builder.Services.AddDatabaseServiceExtension();
builder.Services.AddCommonServiceExtension(typeof(CatalogAssembly));
builder.Services.AddMassTransitExtension(builder.Configuration);
builder.Services.AddVersioningExtension();
builder.Services.AddAuthenticationAndAuthorizationServiceExtension(builder.Configuration);

var app = builder.Build();

app.MapDefaultEndpoints();

// Seed data for the application
app.AddSeedDataExtension().ContinueWith(x =>
{
    Console.WriteLine(x.IsFaulted ? x.Exception.Message : "Seed data added successfully.");
});

//Custom Endpoint Extensions
app.UseExceptionHandler(x => { });
app.AddCategoryGroupEndpointExtension(app.AddVersioningSetExtension());
app.AddCourseGroupEndpointExtension(app.AddVersioningSetExtension());


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.Run();


