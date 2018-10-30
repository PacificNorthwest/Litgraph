using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Litgraph.DAL;
using Litgraph.DAL.Entities;
using Litgraph.Domain.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Litgraph.Server.Middleware;

namespace Litgraph.Server
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            this.Configuration = new ConfigurationBuilder()
                        .SetBasePath(env.ContentRootPath)
                        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                        .AddEnvironmentVariables()
                        .Build();
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            services.AddDbContext<LitgraphContext>(options => 
                    options.UseSqlServer(Configuration.GetConnectionString("LitgraphDB")));
            
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

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            ValidateIssuer = true,
                            ValidAudience = "LitgraphUser",
                            ValidIssuer = this.Configuration.GetValue<string>("JwtCreds:Issuer"),
                            IssuerSigningKey = new SymmetricSecurityKey(
                                Encoding.UTF8.GetBytes(this.Configuration.GetValue<string>("JwtCreds:Key")))
                        }
                    );
            
            services.AddMvc();
            services.AddLitgraphServices();
        }

        public void Configure(IApplicationBuilder app, 
                              IHostingEnvironment env, 
                              ISeedDataInitializer seedDataInitializer, 
                              ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            loggerFactory.AddConsole();
            app.UseDefaultFiles();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMiddleware<ExceptionsMiddleware>();
            
            app.UseMvc(routes => routes.MapRoute(name: "default", template: "{controller}/{action}"));

            seedDataInitializer.Initialize().GetAwaiter().GetResult();
        }
    }
}
