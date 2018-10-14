using System;
using Litgraph.DAL;
using Litgraph.Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Litgraph.Server
{
    public static class IoC
    {
        public static void AddLitgraphServices(this IServiceCollection services)
        {
            services.AddScoped<ISeedDataInitializer, SeedDataInitializer>();
        }
    }
}