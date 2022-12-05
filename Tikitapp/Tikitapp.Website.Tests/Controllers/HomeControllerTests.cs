namespace Tikitapp.Website.Tests.Controllers;

public class HomeControllerTests  {
	[Fact]
	public void Home_Index_Returns_ViewResult() {
		// Arrange
		var c = new HomeController(null!);

		// Act
		var result = c.Index();

		// Assert
		result.ShouldBeOfType<ViewResult>();
     
	}
}
