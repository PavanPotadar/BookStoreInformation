using System;
using Microsoft.AspNetCore.Mvc.Testing;

namespace BookInformation.IntegrationTest.Controller
{
    public class BooksControllerTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _httpclient;

        public BooksControllerTest(WebApplicationFactory<Program> factory)
        {
            _httpclient = factory.CreateDefaultClient();
        }

        [Fact]
        public async Task GetAllBooks_ReturnSuccessCode()
        {
            var response = await _httpclient.GetAsync("api/Books");

            response.EnsureSuccessStatusCode(); 
        }

        [Fact]
        public async Task GetAllBooks_ReturnsExpectedMediaType()
        {
            var response = await _httpclient.GetAsync("api/Books");

            Assert.Equal("application/json", response.Content.Headers.ContentType.MediaType);
        }

        [Fact]
        public async Task GetAllBooks_ReturnsContent()
        {
            var response = await _httpclient.GetAsync("api/Books");

            Assert.NotNull( response.Content);
            Assert.True(response.Content.Headers.ContentLength > 0);
        }

        //[Fact]
        //public async Task GettAll_ReturnExpectedJson()
        //{
        //    var response = await _httpclient.GetStringAsync("api/Books");
        //    Assert.Equal("[{\"id\":\"40c2a6e4-6543-4861-b5cc-38632ee5a169-DB\",\"name\":\"Rich Dad Poor Dad\",\"authoName\":\"Robert Kiyosaki\"},{\"id\":\"6bd412e5-4d9c-4da0-8f54-264e5b268c67\",\"name\":\"Utopia\",\"authoName\":\"Sir Thomas Moor\"},{\"id\":\"d84658f9-d093-4478-bb91-4263c1f1640b\",\"name\":\"Adventures of Tom Sawyer\",\"authoName\":\"Mark Twain\"},{\"id\":\"d978d4ec-eea0-4b64-92a3-f5d5b567f2dd\",\"name\":\"Arthashastra\",\"authoName\":\"Kautilya\"}]", response);

        //}
    }
}

