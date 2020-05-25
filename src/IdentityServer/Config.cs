// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4.Models;
using System.Collections.Generic;

namespace IdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> Ids =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                // MVC client using code flow + pkce
                new Client
                {
                    ClientId = "webapp",
                    ClientName = "WebApp",

                    AllowedGrantTypes = GrantTypes.CodeAndClientCredentials,
                    RequirePkce = true,
                    ClientSecrets = { new Secret("49C1A7E1-0C79-4A89-A3D6-A37998FB86B0".Sha256()) },

                    RedirectUris = { "https://localhost:5002/signin-oidc/oidcprovider" },
                    
                    AllowOfflineAccess = true,
                    AllowedScopes = { "openid", "profile", "api1" }
                }
            };
    }
}