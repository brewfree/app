using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using OpenIddict.Core;
using OpenIddict.Models;

namespace BrewFree
{
    public static class OpenIddictConfig
    {
        public static async Task UseOpenIddictApplication(this IApplicationBuilder app)
        {
            // Note: when using a custom entity or a custom key type, replace OpenIddictApplication by the appropriate type.
            var manager =app.ApplicationServices.GetRequiredService<OpenIddictApplicationManager<OpenIddictApplication>>();

            if (await manager.FindByClientIdAsync("BrewFree", CancellationToken.None) == null)
            {
                var application = new OpenIddictApplication
                {
                    ClientId = "BrewFree",
                    DisplayName = "Brew Free",
                    LogoutRedirectUri = "https://localhost:44399",
                    RedirectUri = "https://localhost:44399/signin-oidc"
                };

                //todo: add to secrets
                await manager.CreateAsync(application, "1A14ABA6-5E3B-4288-A8D5-83C0062484A6", CancellationToken.None);
            }
        }
    }
}
