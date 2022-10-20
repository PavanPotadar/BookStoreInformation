using System;
using Microsoft.AspNetCore.Mvc.Testing;

namespace BookInformation.IntegrationTest.Controller
{
    public class BookControllerWithIDTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _httpclient;

        public BookControllerWithIDTest(WebApplicationFactory<Program> factory)
        {
            _httpclient = factory.CreateDefaultClient();
        }

        [Fact]
        public async Task GetBook_ReturnSuccessCode()
        {
            var response = await _httpclient.GetAsync("api/Books/d84658f9-d093-4478-bb91-4263c1f1640b");

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task GetBook_ReturnsExpectedMediaType()
        {
            var response = await _httpclient.GetAsync("api/Books/d84658f9-d093-4478-bb91-4263c1f1640b");

            Assert.Equal("application/json", response.Content.Headers.ContentType.MediaType);
        }

        [Fact]
        public async Task GetBook_ReturnsContent()
        {
            var response = await _httpclient.GetAsync("api/Books/d84658f9-d093-4478-bb91-4263c1f1640b");

            Assert.NotNull(response.Content);
            Assert.True(response.Content.Headers.ContentLength > 0);
        }

        [Fact]
        public async Task GetBook_ReturnExpectedJson()
        {
            var response = await _httpclient.GetStringAsync("api/Books/d84658f9-d093-4478-bb91-4263c1f1640b");
            Assert.Equal("{\"id\":\"d84658f9-d093-4478-bb91-4263c1f1640b\",\"name\":\"Adventures of Tom Sawyer\",\"authoName\":\"Mark Twain\"}", response);

        }
    }
}

