using System;
using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Xunit;

namespace BookInformation.IntegrationTest
{
    public class HealthCheckest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _httpclient;

        public HealthCheckest(WebApplicationFactory<Program> factory)
        {
            _httpclient = factory.CreateDefaultClient();
        }

        [Fact]
        public async Task HealthCheck_ReturnOk()
        {
            var response = await _httpclient.GetAsync("/api/health");

            response.EnsureSuccessStatusCode(); 

            //Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}

