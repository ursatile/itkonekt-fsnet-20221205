namespace Tikitapp.Website.Tests.Controllers;

public class HomeControllerTests  {
	[Fact]
	public void Home_Index_Returns_ViewResult() {
		var logger = new NullLogger<HomeController>();
		// Arrange
		var c = new HomeController(logger);

		// Act
		var result = c.Index();

		// Assert
		result.ShouldBeOfType<ViewResult>();
     
	}
}
