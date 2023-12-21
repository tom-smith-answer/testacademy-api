using Dotnet.AspNetCore.Samples.WebApi.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Newtonsoft.Json;

namespace Dotnet.AspNetCore.Players.Test;

[TestFixture]
public class SampleTest
{
    private HttpClient? Client { get; set; }

    [SetUp]
    public void Setup()
    {
        var factory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
        {
            var path = Path.Join(TestContext.CurrentContext.TestDirectory, "Data/players-sqlite3.db");
    
            builder.ConfigureServices(collection =>
            {
                collection.RemoveAll(typeof(DbContextOptions<PlayerContext>));
                collection.AddDbContext<PlayerContext>(options =>
                    options.UseSqlite($"Data Source={path}")
                );
            });
        });
        this.Client = factory.CreateClient();
    }


    [Test]
    public void Test()
    {
        var response = Client?.GetAsync("api/Players");
        var json = JsonConvert.SerializeObject(response?.Result.Content.ReadAsStringAsync().Result);
        Console.Write(json);
    }
}