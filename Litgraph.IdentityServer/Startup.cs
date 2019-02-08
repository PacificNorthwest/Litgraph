using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Litgraph.IdentityServer.DAL;
using Litgraph.IdentityServer.DAL.Entities;
using Litgraph.IdentityServer.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Litgraph.IdentityServer.Middleware;

namespace Litgraph.IdentityServer
{
    public class Startup
    {
        private IHostingEnvironment _env;
        private IConfiguration _conf;
        public Startup(IHostingEnvironment env)
        {
            this._env = env;
            this._conf = new ConfigurationBuilder()
                        .SetBasePath(_env.ContentRootPath)
                        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                        .AddEnvironmentVariables()
                        .Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            services.AddDbContext<LitgraphContext>(options => 
                    options.UseSqlServer(_conf.GetConnectionString("LitgraphDB")));
            
            services.AddIdentity<UserEntity, IdentityRole>()
                    .AddEntityFrameworkStores<LitgraphContext>()
                    .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options => 
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 5;

                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.AllowedForNewUsers = true;
                options.User.RequireUniqueEmail = false;
            });
            
            services.AddMvc();
            services.AddLitgraphServices();

            var builder = services.AddIdentityServer(options => 
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseSuccessEvents = true;

            }).AddOperationalStore(options => options.ConfigureDbContext = b => b.UseSqlServer(_conf.GetConnectionString("LitgraphDB")))
              .AddConfigurationStore(options => options.ConfigureDbContext = b => b.UseSqlServer(_conf.GetConnectionString("LitgraphDB")))
              .AddAspNetIdentity<UserEntity>();

            if (_env.IsDevelopment())
                builder.AddDeveloperSigningCredential();
            else builder.AddSigningCredential("*Certificate name from env variable here*");
        }

        public void Configure(IApplicationBuilder app, 
                              IHostingEnvironment env, 
                              ISeedDataInitializer seedDataInitializer, 
                              ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseHsts();

            loggerFactory.AddConsole();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseIdentityServer();

            app.UseMiddleware<ExceptionsMiddleware>();
            
            app.UseMvc(routes => routes.MapRoute(name: "default", template: "{controller}/{action}"));

            seedDataInitializer.Initialize().GetAwaiter().GetResult();
        }
    }
}
