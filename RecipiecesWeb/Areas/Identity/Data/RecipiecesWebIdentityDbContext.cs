using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace RecipiecesWeb.Areas.Identity.Data
{
    public class RecipiecesWebIdentityDbContext : IdentityDbContext<IdentityUser>
    {
        public RecipiecesWebIdentityDbContext(DbContextOptions<RecipiecesWebIdentityDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            //
            SeedAspNetRoles(builder);
        }

        private void SeedAspNetRoles(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>()
                .HasData(new IdentityRole("Admins"));
            builder.Entity<IdentityRole>()
                .HasData(new IdentityRole("Users"));
        }
    }
}
