using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Servibes.Bootstrapper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Servibes.Bootstrapper.Areas.Identity.Pages.Account.RegisterModel;

namespace Servibes.Bootstrapper.Data
{
    public class IdentityDataSeeder
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public IdentityDataSeeder(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task SeedAsync()
        {
            var businessRole = new IdentityRole
            {
                Id = "cac43a6e-f7bb-4448-baaf-1add431ccbbf",
                Name = "Business",
                NormalizedName = "BUSINESS"
            };
            var clientRole = new IdentityRole
            {
                Id = "1b95d174-37b7-11eb-adc1-0242ac120002",
                Name = "Client",
                NormalizedName = "CLIENT"
            };

            await CreateRoleAsync(businessRole);
            await CreateRoleAsync(clientRole);

            var businessUserPassword = "!Business123";
            var businessUser = new ApplicationUser
            {
                Id = "b8633e2d-a33b-45e6-8329-1958b3252bbd",
                UserName = "business@business.pl",
                NormalizedUserName = "BUSINESS@BUSINESS.PL",
                Email = "business@business.pl",
                NormalizedEmail = "BUSINESS@BUSINESS.PL",
                EmailConfirmed = true,
            };

            var clientUserPassword = "!Client123";
            var clientUser = new ApplicationUser
            {
                Id = "3f396b9a-37b7-11eb-adc1-0242ac120002",
                UserName = "client@client.pl",
                NormalizedUserName = "CLIENT@CLIENT.PL",
                Email = "client@client.pl",
                NormalizedEmail = "CLIENT@CLIENT.PL",
                EmailConfirmed = true
            };

            await CreateUserAsync(businessUser, businessUserPassword);
            await CreateUserAsync(clientUser, clientUserPassword);

            var businessUserInRole = await _userManager.IsInRoleAsync(businessUser, businessRole.Name);
            if (!businessUserInRole)
                await _userManager.AddToRoleAsync(businessUser, businessRole.Name);

            var clientUserInRole = await _userManager.IsInRoleAsync(clientUser, clientRole.Name);
            if (!clientUserInRole)
                await _userManager.AddToRoleAsync(clientUser, clientRole.Name);

        }

        private async Task CreateRoleAsync(IdentityRole role)
        {
            var exits = await _roleManager.RoleExistsAsync(role.Name);
            if (!exits)
                await _roleManager.CreateAsync(role);
        }

        private async Task CreateUserAsync(ApplicationUser user, string password)
        {
            var exists = await _userManager.FindByEmailAsync(user.Email);
            if (exists == null)
            {
                var result = await _userManager.CreateAsync(user, password);
            }
        }
    }

    public class SetupIdentityDataSeeder : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;
        public SetupIdentityDataSeeder(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var seeder = scope.ServiceProvider.GetRequiredService<IdentityDataSeeder>();

                await seeder.SeedAsync();
            }
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
