using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace Litgraph.IdentityServer.Model
{
    public static class IdentityServerConfiguration
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
            => new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };

        public static IEnumerable<ApiResource> GetApiResources()
            => new List<ApiResource> { new ApiResource(Apis.GRAPHQL, "GraphQL API") };

        public static IEnumerable<Client> GetClients()
            => new List<Client>
            {
                new Client
                {
                    ClientId = Clients.VUE_CLIENT,
                    ClientName = "Litgraph VueJS client",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    RequireConsent = false,
                    RequireClientSecret = false,

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        Apis.GRAPHQL
                    },

                    RedirectUris = { "http://localhost:9000/dashboard" },
                    PostLogoutRedirectUris = { "http://localhost:9000/" },
                    AllowedCorsOrigins = { "http://localhost:9000" }
                }
            };
    }
}