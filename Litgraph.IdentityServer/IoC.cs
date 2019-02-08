using System;
using Litgraph.IdentityServer.DAL;
using Litgraph.IdentityServer.Services.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace Litgraph.IdentityServer
{
    public static class IoC
    {
        public static void AddLitgraphServices(this IServiceCollection services)
        {
            services.AddScoped<ISeedDataInitializer, SeedDataInitializer>();
        }
    }
}