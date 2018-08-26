using System.Linq;
using BaseProject.Areas.Identity.Data;
using BaseProject.Areas.Identity.Services;
using BaseProject.Cookies;
using BaseProject.Cookies.Authorization;
using BaseProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(BaseProject.Areas.Identity.IdentityHostingStartup))]
namespace BaseProject.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        private static bool CannotSeemToFigureOutWhatIsGoingOnMode = false;

        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
                services.AddDbContext<IdentityContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("BaseProject")));

                services.AddDefaultIdentity<User>()
                    .AddEntityFrameworkStores<IdentityContext>();
            });
        }
    }
}