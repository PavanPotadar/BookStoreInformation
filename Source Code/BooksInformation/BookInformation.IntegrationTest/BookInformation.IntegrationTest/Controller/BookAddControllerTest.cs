using System;
using System.Net.Http;
using System.Text;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json.Linq;

namespace BookInformation.IntegrationTest.Controller
{
    public class BookAddControllerTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _httpclient;
        private static Random random = new Random();

        public BookAddControllerTest(WebApplicationFactory<Program> factory)
        {
            _httpclient = factory.CreateDefaultClient();
        }

        public string ReturnPayload()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return new string(Enumerable.Repeat(chars, 5)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        [Fact]
        public async Task BookAdd_RequiredFeildTest()
        {
            string payload = "{\"name\":\" Book Name -"+ ReturnPayload() + "\",\"authoName\":\" Author Name - "+ReturnPayload()+"\"}";

            var httpContent = new StringContent(payload.ToString(), Encoding.UTF8, "application/json");

            var response = await _httpclient.PostAsync("api/Books", httpContent);

            Assert.Equal("application/json", response.Content.Headers.ContentType.MediaType);
        }

        [Fact]
        public async Task BookAdd_TestAddinSuccessfully()
        {
            string payload = "{\"name\":\" Book Name -" + ReturnPayload() + "\",\"authoName\":\" Author Name - " + ReturnPayload() + "\"}";

            var httpContent = new StringContent(payload.ToString(), Encoding.UTF8, "application/json");

            var response = await _httpclient.PostAsync("api/Books", httpContent);

            string contentdata = await response.Content.ReadAsStringAsync();
            JObject data = JObject.Parse(contentdata);
            
            string id = data["id"].ToString();

            var newResp = await _httpclient.GetStringAsync("api/Books/"+ id);

             
            dynamic datanew = JObject.Parse(newResp);
            string name = data["name"].ToString();
            Assert.Contains(name, payload);
            //string Location = response.Content.Headers.ContentLocation.ToString();
        }

    }
}

