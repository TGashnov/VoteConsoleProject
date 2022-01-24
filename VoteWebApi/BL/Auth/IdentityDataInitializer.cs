using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using VoteDbContext.Model.DTO;

namespace VoteWebApi.BL.Auth
{
    public class IdentityDataInitializer
    {
        readonly IConfiguration configuration;
        readonly RoleManager<IdentityRole> roleManager;
        readonly UserManager<UserDbDTO> userManager;

        public IdentityDataInitializer(IConfiguration configuration, 
            RoleManager<IdentityRole> roleManager, UserManager<UserDbDTO> userManager)
        {
            this.configuration = configuration;
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        public async Task InitializeAsync()
        {
            await AddRole("Admin");
            await AddRole("User");
            var defaultUsers = configuration
                .GetSection("DefaultUsers")
                .GetChildren();
            foreach (var user in defaultUsers)
            {
                await AddUser(user["UserName"], user["Email"], user["Password"], user["Roles"].Split(","));
            }
        }

        private async Task AddUser(string userName, string email, string password, IEnumerable<string> roles)
        {
            var user = await userManager.FindByNameAsync(userName);
            if(user == null)
            {
                user = new UserDbDTO()
                {
                    UserName = userName,
                    Email = email,
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(user, password);
            }
            foreach (var role in roles)
            {
                await AddUserToRole(user, role);
            }
        }

        private async Task AddUserToRole(UserDbDTO user, string role)
        {
            if (user != null && !await userManager.IsInRoleAsync(user, role))
            {
                await userManager.AddToRoleAsync(user, role);
            }
        }

        private async Task AddRole(string role)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }
    }

    public class SetupIdentityDataInitializer : IHostedService
    {
        private readonly IServiceProvider serviceProvider;

        public SetupIdentityDataInitializer(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = serviceProvider.CreateScope();
            var initializer = scope.ServiceProvider.GetRequiredService<IdentityDataInitializer>();
            await initializer.InitializeAsync();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
