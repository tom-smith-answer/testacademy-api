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
        [TearDown]
        public void TearDown()
        {
            Given(httpClient)
                .When()
                .Delete(baseUrl + "Players/13")
                .Then();
        }

        [TestCase("Players", 200)]
        [TestCase("Players/1", 200)]
        [TestCase("Players/999", 404)]
        [TestCase("Players/fail", 400)]
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

        [TestCase("Players/13", 204)]
        [TestCase("Players/14", 404)]
        [TestCase("Players/fail", 400)]
        public void DeletePlayerCheckStatus(string endpoint, int status)
        {
            DataHelper dataHelper = new DataHelper();

            Player player = dataHelper.GetPlayerData("Post_Duncan");

            Given(httpClient)
                .Body(player)
                .When()
                .Post(baseUrl + "Players")
                .Then();

            Given(httpClient)
                .When()
                .Delete(baseUrl + endpoint)
                .Then()
                .StatusCode(status);
        }

        [TestCase("Update_Harvey_Invalid", 400)]
        [TestCase("Update_Harvey_Incomplete", 400)]
        [TestCase("Update_Harvey", 204)]
        [TestCase("Update_Harvey_Multi", 204)]
        public void PutPlayer(string name, int status)
        {
            DataHelper dataHelper = new DataHelper();
            Player player = dataHelper.GetPlayerData(name);

            Given(httpClient)
                .Body(player)
                .When()
                .Put(baseUrl + "Players/12")
                .Then()
                .StatusCode(status);

            object playerResponse = Given(httpClient)
                .When()
                .Get(baseUrl + "Players/12")
                .Then()
                .Extract().Body("$");


            Snapshot.Match(playerResponse);
        }

        [TestCase("Players/13", 400)]
        [TestCase("Players/fail", 400)]

        public void PutBadEndpoint(string endpoint, int status)
        {
            DataHelper dataHelper = new DataHelper();
            Player player = dataHelper.GetPlayerData("Update_Harvey");

            Given(httpClient)
                .Body(player)
                .When()
                .Put(baseUrl + endpoint)
                .Then()
                .StatusCode(status);
        }

        [Test]
        public void PutEmpty()
        {
            Given(httpClient)
                .Body("")
                .When()
                .Put(baseUrl + "Players/12")
                .Then()
                .StatusCode(400);
        }
    }
}

