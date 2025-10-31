using MicroServiceV2.File.API;
using MicroServiceV2.File.API.Features.File;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();// Custom common Extensions from 'ServiceDefaults' project


builder.Services.AddOpenApi();
// .Net 9.0'da default olarak yok
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Custom common Extensions
builder.Services.AddCommonServiceExtension(typeof(FileAssembly));
builder.Services.AddMassTransitExtension(builder.Configuration);
builder.Services.AddVersioningExtension();
builder.Services.AddAuthenticationAndAuthorizationServiceExtension(builder.Configuration);

//for wwwroot
builder.Services.AddSingleton<IFileProvider>(new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")));


var app = builder.Build();

app.MapDefaultEndpoints();

// Custom common Extensions
app.UseExceptionHandler(x => { });
app.AddFileGroupEndpointExtension(app.AddVersioningSetExtension());

//for wwwroot
app.UseStaticFiles();

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
