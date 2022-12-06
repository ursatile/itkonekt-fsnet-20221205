using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Tikitapp.Website.Data;
using Tikitapp.Website.Data.Entities;

public class ArtistsTests : IClassFixture<WebApplicationFactory<Program>> {
    private readonly WebApplicationFactory<Program> factory = new();

    [Fact]
    public async Task Artists_Index_Returns_SuccessStatusCode() {
        
        using var db = await TestDatabase.CreateDbContext();
        
        var client = factory.WithWebHostBuilder(builder => {
            builder.ConfigureServices(services => services.AddSingleton(db));
        }).CreateClient();

        var response = await client.GetAsync("/artists");
        response.IsSuccessStatusCode.ShouldBe(true);
        var html = await response.Content.ReadAsStringAsync();
        html.ShouldContain(TestData.Artist1.Name);
        html.ShouldContain(TestData.Artist2.Name);

    }

        [Fact]
    public async Task Artists_Shows_Includes_Correct_Artist_Name() {
        
        using var db = await TestDatabase.CreateDbContext();
        
        var client = factory.WithWebHostBuilder(builder => {
            builder.ConfigureServices(services => services.AddSingleton(db));
        }).CreateClient();

        var response = await client.GetAsync($"/artists/shows/{TestData.Artist1.Slug}");
        response.IsSuccessStatusCode.ShouldBe(true);
        var html = await response.Content.ReadAsStringAsync();
        html.ShouldContain(TestData.Artist1.Name);
    }
}