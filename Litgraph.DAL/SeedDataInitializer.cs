using System;
using Litgraph.Domain.Services;
using Litgraph.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Linq;

namespace Litgraph.DAL
{
    public class SeedDataInitializer : ISeedDataInitializer
    {

        private const string ADMIN = "admin";
        private const string ADMIN_PASS = "admin";
        private const string ADMIN_EMAIL = "admin@admin.com";

        private const string USER = "user";

        private LitgraphContext _context;
        private UserManager<UserEntity> _userManager;
        private RoleManager<IdentityRole> _roleManager;

        public SeedDataInitializer(LitgraphContext context, UserManager<UserEntity> userManager, RoleManager<IdentityRole> roleManager)
        {
            this._context = context;
            this._userManager = userManager;
            this._roleManager = roleManager;
        }

        public async Task Initialize()
        {
            this._context.Database.EnsureCreated();
            if (!this._context.Roles.Any(r => r.Name.Equals(ADMIN)) || !this._userManager.Users.Any(u => u.UserName.Equals(ADMIN)))
            {
                await this._roleManager.CreateAsync(new IdentityRole(ADMIN));
                await this._roleManager.CreateAsync(new IdentityRole(USER));

                await this._userManager.CreateAsync(new UserEntity { UserName = ADMIN, Email = ADMIN_EMAIL }, ADMIN_PASS);
                await this._userManager.AddToRoleAsync(await this._userManager.FindByNameAsync(ADMIN), ADMIN);

            }

            await this._context.SaveChangesAsync();
        }
    }
}