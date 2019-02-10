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
                new IdentityResources.Profile(),
                new IdentityResources.Email()
            };

        public static IEnumerable<ApiResource> GetApiResources()
            => new List<ApiResource> { new ApiResource(Apis.GraphQL.name, Apis.GraphQL.desc) };

        public static IEnumerable<Client> GetClients()
            => new List<Client>
            {
                new Client
                {
                    ClientId = Clients.VueClient.name,
                    ClientName = Clients.VueClient.desc,
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,
                    RequireConsent = false,
                    RequireClientSecret = false,

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.OfflineAccess,

                        Apis.GraphQL.name
                    },

                    RedirectUris = { "http://localhost:9000/oidc" },
                    PostLogoutRedirectUris = { "http://localhost:9000/" },
                    AllowedCorsOrigins = { "http://localhost:9000" }
                }
            };
    }
}