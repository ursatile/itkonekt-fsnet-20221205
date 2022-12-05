using Microsoft.EntityFrameworkCore;
using Tikitapp.Website.Data;
using Tikitapp.Website.Data.Entities;

namespace Tikitapp.Website.Tests.Controllers;

public class ArtistsControllerTests  {
	private TikitappDbContext db = null!;
	private NullLogger<ArtistsController> logger = new ();

	[Fact]
	public async Task Artists_Index_Returns_ViewResult() {
        using var db = await TestDatabase.CreateDbContext();
        
		var c = new ArtistsController(logger, db); // Arrange
		var result = c.Index(); // Act
		result.ShouldBeOfType<ViewResult>(); // Assert
	}

	[Fact]
	public async Task Artist_Shows_Returns_ViewResult_With_Artist_In_Model() {
        using var db = await TestDatabase.CreateDbContext();

		var c = new ArtistsController(logger, db); // Arrange
		var result = c.Shows(TestData.Artist1.Slug) as ViewResult;
		result.ShouldNotBeNull();
		var artist =((result).Model as Artist);
		artist.ShouldNotBeNull();
		artist.Id.ShouldBe(TestData.Artist1.Id);
		artist.Name.ShouldBe(TestData.Artist1.Name);
	}

	
	[Fact]
	public async Task Artists_Index_Returns_ViewResult_With_Records_In_Model() {
        using var db = await TestDatabase.CreateDbContext();
		var c = new ArtistsController(logger, db);
		var result = c.Index();
		result.ShouldBeOfType<ViewResult>();
		var artists = ((ViewResult)result).Model as IEnumerable<Artist>;
		artists.ShouldNotBeNull();
		artists.ShouldContain(a => a.Name == TestData.Artist1.Name);
	}
}
