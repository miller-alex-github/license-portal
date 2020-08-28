using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(License.Portal.Web.Areas.Identity.IdentityHostingStartup))]
namespace License.Portal.Web.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}