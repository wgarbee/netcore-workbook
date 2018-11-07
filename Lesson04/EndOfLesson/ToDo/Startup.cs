using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ToDoApp.Data;
using ToDoApp.Infrastructure;
using ToDoApp.Services;
using WebServerUtilities;

namespace ToDoApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<UnwrapExceptionMiddleware>();
            services.AddSingleton<InternalServerErrorStatusCodeMiddleware>();
            services.AddScoped<IRepository, Repository>();
            services.AddScoped<IReadOnlyToDoContext, ToDoContext>();

            services.AddHostedService<PurgeOldToDosService>();

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => false;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<ToDoContext>(config => config.UseSqlServer(Configuration.GetConnectionString("ToDoApp")));    

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStatusCodePagesWithReExecute("/Error", "?statusCode={0}");
            app.UseMiddleware<InternalServerErrorStatusCodeMiddleware>();
            app.UseMiddleware<UnwrapExceptionMiddleware>();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            EnsureDatabaseUpdated(app);   
        }


        private void EnsureDatabaseUpdated(IApplicationBuilder app)
        {
            var scopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
            using (var serviceScope = scopeFactory.CreateScope())
            using (var context = serviceScope.ServiceProvider.GetService<ToDoContext>())
            {
                context.Database.EnsureCreated();
            }
        }
    }
}
