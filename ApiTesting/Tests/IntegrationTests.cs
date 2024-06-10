namespace ApiTesting.Tests
{
    using System;
    using static RestAssured.Dsl;
    using FluentAssertions;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Hosting;
    using Microsoft.AspNetCore.Mvc.Testing;
    using Microsoft.AspNetCore.Hosting;
    using Dotnet.AspNetCore.Samples.WebApi;
    using System.Security.AccessControl;
    using System.Runtime.CompilerServices;
    using Newtonsoft.Json.Linq;
    using Snapshooter.NUnit;
    using RestAssured.Response;
    using ApiTesting.Classes.TestData;
    using FluentAssertions.Equivalency;
    using ApiTesting.Classes.POCO;
    using ApiTesting.Classes.Utilities;

    public class IntegrationTests : BasicTests
    {

        [TestCase("Players", 200)]
        [TestCase("Players/1", 200)]
        [TestCase("Players/999", 404)]
        public void GetStatusCode(string endpoint, int status)
        {
            Given(httpClient)
                .When()
                .Get(baseUrl + endpoint)
                .Then()
                .StatusCode(status);
        }

        [TestCase("Players")]
        [TestCase("Players/1")]
        public void GetContentType(string endpoint)
        {
            Given(httpClient)
            .When()
            .Get(baseUrl + endpoint)
            .Then()
            .StatusCode(200)
            .ContentType("application/json; charset=utf-8");
        }

        [TestCase("Players")]
        public void GetPlayersBodyContent(string endpoint)
        {
            object playerResponse = Given(httpClient)
                .When()
                .Get(baseUrl + endpoint)
                .Then()
                .StatusCode(200)
            .Extract().Body("$");

            Console.WriteLine(playerResponse);

            Snapshot.Match(playerResponse);
        }

        [Test]
        public void GetPlayerBodyContent()
        {
            object playerResponse = Given(httpClient)
                .When()
                .Get(baseUrl + "Players/1")
                .Then()
                .StatusCode(200)
            .Extract().Body("$");

            Console.WriteLine(playerResponse);

            Snapshot.Match(playerResponse);
        }

        [Test]
        public void GetResponseTime()
        {
            Given(httpClient)
            .When()
            .Get(baseUrl + "Players")
            .Then()
            .ResponseTime(NHamcrest.Is.LessThan(TimeSpan.FromMilliseconds(2000)));
        }

        [TestCase("Post_Harvey", 201, "id")]
        [TestCase("Post_Incomplete", 400, "traceId")]
        //[TestCase("Post_Invalid", 400, "traceId")]
        public void PostRequest(string name, int status, string ignore)
        {
            DataHelper dataHelper = new DataHelper();

            Player player = dataHelper.GetPlayerData(name);

         object playerResponse = Given(httpClient)
        .Body(player)
        .When()
        .Post(baseUrl + "Players")
        .Then()
        .StatusCode(status)
        .Extract().Body("$");

         Snapshot.Match(playerResponse, matchOptions => matchOptions.IgnoreField(ignore));
        }

        [Test]
        public void PostEmptyRequest()
        {
            Given(httpClient)
                .Body("")
                .When()
                .Post(baseUrl + "Players")
                .Then().StatusCode(400);
        }

        [Test]
        public void PostInvalidRequest () 
        {
            InvalidPlayer player = new InvalidPlayer 
            {
                Id = 0,
                FirstName = 1,
                MiddleName = 2,
                LastName = 3,
                DateOfBirth = 4,
                SquadNumber = "5",
                Position = 6, 
                AbbrPosition = 7,
                Team = 8,
                League = 9,
                Starting11 = "true"
            };

            object playerResponse = Given(httpClient)
                .Body(player)
                .When()
                .Post(baseUrl + "Players")
                .Then()
                .StatusCode(400)
                .Extract().Body("$");

            Snapshot.Match(playerResponse, matchOptions => matchOptions.IgnoreField("traceId"));
        }

        [TestCase("Players/13")]
        public void DeletePlayer(string endpoint)
        {
            DataHelper dataHelper = new DataHelper();

            Player player = dataHelper.GetPlayerData("Post_Harvey");

            Given(httpClient)
                .When()
                .Delete(baseUrl + endpoint)
                .Then()
                .StatusCode(204);
                
        }
    }
}

