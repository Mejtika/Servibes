using System.Collections.Generic;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Servibes.Bootstrapper.Data;
using Servibes.Bootstrapper.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Servibes.BusinessProfile.Api;
using Servibes.Shared;
using Servibes.Availability.Api;
using Servibes.Appointments.Api;

namespace Servibes.Bootstrapper
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSharedModule(Configuration);
            services.AddBusinessProfileModule(Configuration);
            services.AddAvailabilityModule(Configuration);
            services.AddAppointmentsModule(Configuration);

            services.AddDbContext<IdentityContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection"), options =>
                    {
                        options.MigrationsHistoryTable("__IdentityMigrationsHistory", "identity");
                    }));

            services.AddIdentity<ApplicationUser, IdentityRole>(
                    options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<IdentityContext>()
                .AddDefaultTokenProviders()
                .AddDefaultUI();

            services.AddIdentityServer()
                .AddApiAuthorization<ApplicationUser, IdentityContext>(options =>
                {
                    options.IdentityResources.Add(new IdentityResource(
                            name: "roles",
                            displayName: "Roles",
                            claimTypes: new List<string> { "role" })
                    { Required = true });
                    options.Clients[0].AllowedScopes.Add("roles");
                    options.Clients[0].AlwaysSendClientClaims = true;
                });

            services.AddAuthentication()
                .AddIdentityServerJwt();
            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            services.AddScoped<IdentityDataSeeder>();
            services.AddHostedService<SetupIdentityDataSeeder>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseRouting();

            app.UseSharedModule();
            app.UseBusinessProfileModule();
            app.UseAvailabilityModule();
            app.UseAppointmentsModule();

            app.UseAuthentication();
            app.UseIdentityServer();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";
                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
