using MicroServiceV2.Shared.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();// Custom common Extensions from 'ServiceDefaults' project

builder.Services.AddReverseProxy().LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));
builder.Services.AddAuthenticationAndAuthorizationServiceExtension(builder.Configuration);
var app = builder.Build();

app.MapDefaultEndpoints();


app.MapReverseProxy();
app.MapGet("/", () => "YARP Gateway");

app.UseAuthentication();
app.UseAuthorization();

app.Run();
