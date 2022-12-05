public class WebTests : IClassFixture<WebApplicationFactory<Program>> {
    private readonly WebApplicationFactory<Program> factory = new();

    [Fact]
    public async Task Home_Index_Returns_SuccessStatusCode() {
        var client = factory.CreateClient();
        var response = await client.GetAsync("/");
        response.IsSuccessStatusCode.ShouldBe(true);
    }

    
    [Fact]
    public async Task Home_Index_Includes_Welcome_Message() {
        var client = factory.CreateClient();
        var response = await client.GetAsync("/");
        response.IsSuccessStatusCode.ShouldBe(true);
        var html = await response.Content.ReadAsStringAsync();
        html.ShouldContain("Welcome");
    }
}