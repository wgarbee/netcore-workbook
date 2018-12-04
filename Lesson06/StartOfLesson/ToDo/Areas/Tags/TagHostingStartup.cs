using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ToDoApp.Areas.Tags.Data;

[assembly: HostingStartup(typeof(ToDoApp.Areas.Tags.TagHostingStartup))]
namespace ToDoApp.Areas.Tags
{
    public class TagHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<TagContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("TagContextConnection")));
            });
        }
    }
}