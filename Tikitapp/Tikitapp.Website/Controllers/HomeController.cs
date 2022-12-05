using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Tikitapp.Website.Models;

namespace Tikitapp.Website.Controllers;

public class HomeController : Controller {
	private readonly ILogger<HomeController> logger;

	public HomeController(ILogger<HomeController> logger) {
		this.logger = logger;
	}

	public IActionResult Index() {

		logger.LogTrace("This is way more detail than you'll ever need...");
		logger.LogDebug("This is probably useful if you're troubleshooting something");

		logger.LogInformation("Hey! Everything is fine; just checking in.");
		logger.LogWarning("Something weird happened... let's keep an eye on it in case it happens 100 times");
		logger.LogError("Something definitely went wrong, but only one person noticed");
		logger.LogCritical("The entire website is on fire. Wake up the boss");
		return View();
	}

	public IActionResult Privacy() {
		return View();
	}

	[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
	public IActionResult Error() {
		return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
	}
}
