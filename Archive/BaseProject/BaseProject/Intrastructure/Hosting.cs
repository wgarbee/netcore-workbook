using BaseProject.Areas.Identity.Data;
using BaseProject.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaseProject.Intrastructure
{
    public static class Hosting
    {
        public static IWebHost EnsureDBInitialized(this IWebHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var appContext = services.GetRequiredService<IssueTrackerContext>();
                    var authContext = services.GetRequiredService<AuthenticationContext>();
                    Data.DBInitializer.Initialize(appContext);
                    Areas.Identity.Data.DBInitializer.Initialize(authContext);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while seeding the database.");
                }
            }
            return host;
        }
    }
}
