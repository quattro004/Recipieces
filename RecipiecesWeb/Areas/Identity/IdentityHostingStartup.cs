using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RecipiecesWeb.Areas.Identity.Data;

[assembly: HostingStartup(typeof(RecipiecesWeb.Areas.Identity.IdentityHostingStartup))]
namespace RecipiecesWeb.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<RecipiecesWebIdentityDbContext>(options =>
                    options.UseSqlite(
                        context.Configuration.GetConnectionString("RecipiecesWebIdentityDbContextConnection")));

                services.AddDefaultIdentity<IdentityUser>(config =>
                {
                    config.SignIn.RequireConfirmedEmail = true;
                })
                .AddEntityFrameworkStores<RecipiecesWebIdentityDbContext>();
            });
        }
    }
}