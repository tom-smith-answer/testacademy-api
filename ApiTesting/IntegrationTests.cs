namespace TestProject1
{
    using static RestAssured.Dsl;
    using FluentAssertions;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Hosting;
    using Microsoft.AspNetCore.Mvc.Testing;
    using Microsoft.AspNetCore.Hosting;
    using Dotnet.AspNetCore.Samples.WebApi;
    using System.Security.AccessControl;
    using System.Runtime.CompilerServices;

    public class IntegrationTestsWebApplicationFactory
    {
        private readonly WebApplicationFactory<Program> webApplicationFactory;

        private readonly HttpClient httpClient;

        private readonly string baseUrl;

        public IntegrationTestsWebApplicationFactory()
        {
            webApplicationFactory = new WebApplicationFactory<Program>();
            httpClient = webApplicationFactory.CreateClient();
            baseUrl = "https://localhost:7135/api/";
        }

        [TestCase("Players")]
        [TestCase("Players/1")]
        public void StatusCodeIndicatingSuccessCanBeVerifedForGetRequests(string url)
        {
            Given(httpClient)
                .When()
                .Get(baseUrl + url)
                .Then()
                .StatusCode(200);
        }

        [TestCase("Players")]
        [TestCase("Players/1")]
        public void ResponseContentTypeHeaderCanBeVerifiedForGetRequests(string url)
        {
            Given(httpClient)
            .When()
            .Get(baseUrl + url)
            .Then()
            .StatusCode(200)
            .ContentType("application/json; charset=utf-8");
        }

        [Test]
        public void JsonStringResponseBodyCanBeVerifiedForGetRequests()
        {
            string playerName = (string)Given(httpClient)
            .When()
            .Get(baseUrl + "Players")
            .Then().StatusCode(200).Extract().Body("$.[0].firstName");

            Assert.That(playerName, NUnit.Framework.Is.EqualTo("Damián"));
        }

        [Test]
        public void responsebodylengthcanbeverified()
        {
            List<object> players = (List<object>)Given(httpClient)
            .When()    
            .Get(baseUrl + "Players")
            .Then().StatusCode(200).Extract().Body("$.[0:]");

            Assert.That(players.Count, NUnit.Framework.Is.EqualTo(11));

        }

        [Test]
        public void ResponseTimeCanBeVerified()
        {
            Given(httpClient)
            .When()
            .Get(baseUrl + "Players")
            .Then()
            .ResponseTime(NHamcrest.Is.LessThan(TimeSpan.FromMilliseconds(200)));
        }

    }
}

