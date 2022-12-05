using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Tikitapp.Website.Data;
using Tikitapp.Website.Data.Entities;

public class ArtistsTests : IClassFixture<WebApplicationFactory<Program>> {
    private readonly WebApplicationFactory<Program> factory = new();

    [Fact]
    public async Task Artists_Index_Returns_SuccessStatusCode() {

        var options = new DbContextOptionsBuilder<TikitappDbContext>()
			.UseInMemoryDatabase(databaseName: "some-database-name")
			.Options;
		var db = new TikitappDbContext(options);
		db.Database.EnsureCreated();

        var testArtistName1 = Guid.NewGuid().ToString();
        var testArtistName2 = Guid.NewGuid().ToString();
    
        db.Artists.Add(new Artist { Name = testArtistName1 });
        db.Artists.Add(new Artist { Name = testArtistName2 });
        db.SaveChanges();

        var client = factory.WithWebHostBuilder(builder => {
            builder.ConfigureServices(services => services.AddSingleton(db));
        }).CreateClient();

        var response = await client.GetAsync("/artists");
        response.IsSuccessStatusCode.ShouldBe(true);
        var html = await response.Content.ReadAsStringAsync();
        html.ShouldContain(testArtistName1);
        html.ShouldContain(testArtistName2);
    }
}