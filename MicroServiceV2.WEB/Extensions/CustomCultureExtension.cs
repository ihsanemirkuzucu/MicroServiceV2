using Microsoft.AspNetCore.Localization;
using System.Globalization;

namespace MicroServiceV2.WEB.Extensions
{
    public static class CustomCultureExtension
    {
        public static void AddCustomCultureExtension(this WebApplication app)
        {
            var cultureInfo = new CultureInfo("en-GB");
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;// Sets date, number formats, etc.
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;// Sets resource lookups

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(cultureInfo),
                SupportedCultures = [cultureInfo],
                SupportedUICultures = [cultureInfo]
            });
        }
    }
}
