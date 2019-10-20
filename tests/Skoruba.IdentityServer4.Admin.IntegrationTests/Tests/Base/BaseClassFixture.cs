﻿using System.Net.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Skoruba.IdentityServer4.Admin.Configuration.Interfaces;
using Skoruba.IdentityServer4.Admin.IntegrationTests.Common;
using Xunit;

namespace Skoruba.IdentityServer4.Admin.IntegrationTests.Tests.Base
{
    public class BaseClassFixture : IClassFixture<WebApplicationFactory<Startup>>
    {
        protected readonly WebApplicationFactory<Startup> Factory;
        protected readonly HttpClient Client;

        public BaseClassFixture(WebApplicationFactory<Startup> factory)
        {
            Factory = factory;
            Client = factory.SetupClient();
            Factory.CreateClient();
        }

        protected virtual void SetupAdminClaimsViaHeaders()
        {
            using (var scope = Factory.Server.Host.Services.CreateScope())
            {
                var configuration = scope.ServiceProvider.GetRequiredService<IRootConfiguration>();
                Client.SetAdminClaimsViaHeaders(configuration.AdminConfiguration);
            }
        }
    }
}