using MicroServiceV2.WEB.DelegateHandler;
using MicroServiceV2.WEB.ExceptionHandlers;
using MicroServiceV2.WEB.Extensions;
using MicroServiceV2.WEB.Options;
using MicroServiceV2.WEB.Pages.Auth.SignIn;
using MicroServiceV2.WEB.Pages.Auth.SignUp;
using MicroServiceV2.WEB.Services;
using MicroServiceV2.WEB.Services.Refit;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Refit;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();// Custom common Extensions from 'ServiceDefaults' project

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddMvc(opt => opt.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);

//Custom Extension For Web
builder.Services.AddOptionsExtension()
    .AddCustomApplicationExtension()
    .AddRefitExtension(builder.Configuration);

var app = builder.Build();

app.MapDefaultEndpoints();

app.AddCustomCultureExtension();// Set Custom Culture Info

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
