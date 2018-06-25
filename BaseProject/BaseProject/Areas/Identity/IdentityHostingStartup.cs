using System;
using BaseProject.Areas.Identity.Data;
using BaseProject.Areas.Identity.Services;
using BaseProject.Data;
using BaseProject.Intrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(BaseProject.Areas.Identity.IdentityHostingStartup))]
namespace BaseProject.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<AuthenticationContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString(Constants.AuthenticationContextConnectionConfigurationKey)));

                services.AddDefaultIdentity<AuthUser>()
                    .AddEntityFrameworkStores<AuthenticationContext>();

                services.ConfigureApplicationCookie(options =>
                {
                    options.LoginPath = $"/Identity/Account/Login";
                    options.LogoutPath = $"/Identity/Account/Logout";
                    options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
                });

                // using Microsoft.AspNetCore.Identity.UI.Services;
                services.AddSingleton<IEmailSender, EmailSender>();
            });
        }
    }
}