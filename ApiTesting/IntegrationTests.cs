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
    using ApiTesting.Classes;
    using Newtonsoft.Json.Linq;

    public class IntegrationTestsWebApplicationFactory: IDisposable
    {
        public void Dispose() { }

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
            //JObject jPlayer = (JObject)Given(httpClient)
            //.When()
            //.Get(baseUrl + "Players")
            //.Then().StatusCode(200).Extract().Body("$.[0]");


            //Player player = jPlayer.ToObject<Player>();
    

            //Assert.That(player.FirstName, NUnit.Framework.Is.EqualTo("Damián"));
            //Assert.That(player.MiddleName, NUnit.Framework.Is.EqualTo("Emiliano"));
            //Assert.That(player.LastName, NUnit.Framework.Is.EqualTo("Martínez"));
            //Assert.That((DateTime)player.DateOfBirth, NUnit.Framework.Is.EqualTo("1992-09-02T01:00:00+01:00"));
            //Assert.That(player.SquadNumber, NUnit.Framework.Is.EqualTo(23));
            //Assert.That(player.Position, NUnit.Framework.Is.EqualTo("Goalkeeper"));
            //Assert.That(player.AbbrPosition, NUnit.Framework.Is.EqualTo("GK"));
            //Assert.That(player.Team, NUnit.Framework.Is.EqualTo("Aston Villa FC"));
            //Assert.That(player.League, NUnit.Framework.Is.EqualTo("Premier League"));
            //Assert.That(player.Starting11, NUnit.Framework.Is.EqualTo(true));
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

