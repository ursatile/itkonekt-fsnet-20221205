using Microsoft.EntityFrameworkCore;
using Tikitapp.Website.Data;
using Tikitapp.Website.Data.Entities;

namespace Tikitapp.Website.Tests.Controllers;

public class ArtistsControllerTests  {
	private TikitappDbContext db = null!;
	private NullLogger<ArtistsController> logger = new ();

	public ArtistsControllerTests() {
		var options = new DbContextOptionsBuilder<TikitappDbContext>()
			.UseInMemoryDatabase(databaseName: "some-database-name")
			.Options;
		db = new TikitappDbContext(options);
		db.Database.EnsureCreated();
	}

	[Fact]
	public void Artists_Index_Returns_ViewResult() {
		var c = new ArtistsController(logger, db); // Arrange
		var result = c.Index(); // Act
		result.ShouldBeOfType<ViewResult>(); // Assert
	}

	[Fact]
	public void Artist_Shows_Returns_ViewResult_With_Artist_In_Model() {
		var guid = Guid.NewGuid();
		const string name = "TEST ARTIST 1";
		db.Artists.Add(new Artist { Id = guid, Name = name });
		db.SaveChanges();

		var c = new ArtistsController(logger, db); // Arrange
		var result = c.Shows(guid) as ViewResult;
		var artist =((result).Model as Artist);
		artist.Id.ShouldBe(guid);
		artist.Name.ShouldBe(name);
	}

	
	[Fact]
	public void Artists_Index_Returns_ViewResult_With_Records_In_Model() {
		db.Artists.Add(new Artist { Name = "TEST ARTIST 1" });
		db.SaveChanges();

		var c = new ArtistsController(logger, db); // Arrange

		var result = c.Index(); // Act
		result.ShouldBeOfType<ViewResult>(); // Assert
		var artists = ((ViewResult)result).Model as IEnumerable<Artist>;
		artists.ShouldNotBeNull();
		artists.ShouldContain(a => a.Name == "TEST ARTIST 1");
	}
}
