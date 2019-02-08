using System;
using Litgraph.IdentityServer.Services.Contracts;
using Litgraph.IdentityServer.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using IdentityServer4.EntityFramework.DbContexts;
using Litgraph.IdentityServer.Model;
using IdentityServer4.EntityFramework.Mappers;

namespace Litgraph.IdentityServer.DAL
{
    public class SeedDataInitializer : ISeedDataInitializer
    {
        //replace by env variables
        private const string ADMIN_PASS = "admin";
        private const string ADMIN_EMAIL = "admin@admin.com";

        private LitgraphContext _context;
        private ConfigurationDbContext _configContext;
        private PersistedGrantDbContext _persistedGrantContext;

        private UserManager<UserEntity> _userManager;
        private RoleManager<IdentityRole> _roleManager;

        public SeedDataInitializer(LitgraphContext context, 
                                   ConfigurationDbContext configContext, 
                                   PersistedGrantDbContext persistedGrantContext, 
                                   UserManager<UserEntity> userManager, 
                                   RoleManager<IdentityRole> roleManager)
        {
            this._context = context;
            this._configContext = configContext;
            this._persistedGrantContext = persistedGrantContext;
            this._userManager = userManager;
            this._roleManager = roleManager;
        }

        public async Task Initialize()
        {
            this._context.Database.Migrate();
            this._persistedGrantContext.Database.Migrate();
            this._configContext.Database.Migrate();

            if (!this._context.Roles.Any(r => r.Name.Equals(Roles.ADMIN)) || !this._userManager.Users.Any(u => u.UserName.Equals(Roles.ADMIN)))
            {
                await this._roleManager.CreateAsync(new IdentityRole(Roles.ADMIN));
                await this._roleManager.CreateAsync(new IdentityRole(Roles.USER));

                await this._userManager.CreateAsync(new UserEntity { UserName = Roles.ADMIN, Email = ADMIN_EMAIL }, ADMIN_PASS);
                await this._userManager.AddToRoleAsync(await this._userManager.FindByNameAsync(Roles.ADMIN), Roles.ADMIN);
            }

            if (!_configContext.Clients.Any())
                foreach (var client in IdentityServerConfiguration.GetClients())
                    this._configContext.Clients.Add(client.ToEntity());
            
            if (!_configContext.IdentityResources.Any())
                foreach (var resource in IdentityServerConfiguration.GetIdentityResources())
                    this._configContext.IdentityResources.Add(resource.ToEntity());

            if (!_configContext.ApiResources.Any())
                foreach (var resource in IdentityServerConfiguration.GetApiResources())
                    this._configContext.ApiResources.Add(resource.ToEntity());

            await this._context.SaveChangesAsync();
            await this._configContext.SaveChangesAsync();
            await this._persistedGrantContext.SaveChangesAsync();
        }
    }
}