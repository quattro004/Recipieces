﻿using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using RecipiecesWeb.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RecipiecesWeb.Areas.Identity.Services;
using RecipiecesWeb.Models;
using RecipeUIClassLib.Areas.Recipes.Models;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authorization;
using RecipiecesWeb.Areas.Identity;
using Microsoft.AspNetCore.DataProtection;
using System.IO;
using RecipeUIClassLib.Infrastructure.Interfaces;
using Refit;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Api.Domain.Interfaces;
using Api.Infrastructure.Services;
using Api.Infrastructure;

namespace RecipiecesWeb
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
            var containerPath = Configuration.GetValue<string>("DataPath");
            DirectoryInfo dataKeyDirectory;

            if (Directory.Exists(containerPath))
            {
                dataKeyDirectory = new DirectoryInfo(containerPath);
            }
            else
            {
                dataKeyDirectory = new DirectoryInfo(AppContext.BaseDirectory);
            }

            services.AddDataProtection()
                .SetApplicationName("recipieces-web")
                .PersistKeysToFileSystem(dataKeyDirectory);

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<ApplicationUser, IdentityRole>(config =>
            {
                config.SignIn.RequireConfirmedEmail = true;
            })
            .AddDefaultUI()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
            
            services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, AdditionalUserClaimsPrincipalFactory>();

            services.AddSingleton<IAuthorizationHandler, IsAdminHandler>();
            services.AddAuthorization(options =>
            {
                options.AddPolicy("IsAdmin", policyIsAdminRequirement =>
                {
                    policyIsAdminRequirement.Requirements.Add(new IsAdminRequirement());
                });
            });

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 8;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(60);
                options.Lockout.MaxFailedAccessAttempts = 4;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;
            });

            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromDays(1);

                options.LoginPath = "/Identity/Account/Login";
                options.LogoutPath = "/Identity/Account/Logout";
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                options.SlidingExpiration = true;
            });
            // services.AddSingleton<IEmailSender, EmailSender>();
            services.Configure<AuthMessageSenderOptions>(Configuration);
            services.AddOptions()
                    .Configure<ApiOptions>(Configuration)
                    .Configure<ApiOptions>(x => x.MongoDbConnection = Configuration.GetConnectionString("MongoDbConnection"));
            services.AddScoped<IDataService<Album<MediaContent>>, DataService<Album<MediaContent>>>();
            var settings = new RefitSettings();
            // Configure refit settings here
            //
            var recipeBaseUrl = Configuration.GetValue<string>("RecipeApiBaseUrl");
            services.AddRefitClient<IRecipeWebApi>(settings)
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(Path.Combine(recipeBaseUrl, "recipes")));
            services.AddRefitClient<ICategoryWebApi>(settings)
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(Path.Combine(recipeBaseUrl, "categories")));
            // Add additional IHttpClientBuilder chained methods as required here:
            // .AddHttpMessageHandler<MyHandler>()
            // .SetHandlerLifetime(TimeSpan.FromMinutes(2));

            services.AddControllersWithViews().AddNewtonsoftJson();
            services.AddRazorPages();
            //
            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });
        }



        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1822:Mark members as static", 
            Justification = "Framework code")]
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }            
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.UseFileServer();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();
            app.UseRouting();
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapRazorPages();
            });
            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    // spa.UseReactDevelopmentServer(npmScript: "start");
                    spa.UseProxyToSpaDevelopmentServer("http://localhost:3005");
                }
            });
        }
    }
}