using Microsoft.AspNetCore.Mvc.Testing;

namespace ApiTesting.Classes
{


    public class BasicTests : IDisposable
    {
        public void Dispose() { }

        public readonly WebApplicationFactory<Program> webApplicationFactory;

        public readonly HttpClient httpClient;

        public readonly string baseUrl;

        public BasicTests()
        {
            webApplicationFactory = new WebApplicationFactory<Program>();
            httpClient = webApplicationFactory.CreateClient();
            baseUrl = "https://localhost:7135/api/";
        }
    }

}