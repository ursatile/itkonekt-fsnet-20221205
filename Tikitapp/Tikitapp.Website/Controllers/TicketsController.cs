using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tikitapp.Website.Data;
using Tikitapp.Website.Models;

namespace Tikitapp.Website.Controllers;

public class TicketsController : Controller {
	private readonly ILogger<TicketsController> logger;
	private readonly TikitappDbContext db;

	public TicketsController(ILogger<TicketsController> logger, TikitappDbContext db) {
		this.logger = logger;
		this.db = db;
	}

	public IActionResult Show(Guid id) {
		var show = db.Shows
			.Include(show => show.Artist)
			.Include(show => show.Venue)
			.Include(show => show.TicketTypes)
			.FirstOrDefault(show => show.Id == id);
		if (show == default) return NotFound();

		var model = new BasketViewModel {
			ShowId = show.Id,
			ArtistName = show.Artist.Name,
			ShowDate = show.DoorsOpen.ToString("D", CultureInfo.InvariantCulture),
			VenueName = show.Venue.Name,
			Contents = show.TicketTypes.Select(t => new TicketTypeViewModel() {
				Id = t.Id,
				Name = t.Name,
				Price = t.Price,
				FormattedPrice = t.Price.ToString("C", show.Venue.CultureInfo),
				Quantity = 0
			}).ToList()
		};
		return View(model);
	}
	
	[HttpPost]
	public IActionResult Update(Guid showId,
		Guid? increment, Guid? decrement) {
		if (increment.HasValue) return Json($"Adding 1 of {increment.Value}");
		if (decrement.HasValue) return Json($"Removing 1 of {decrement.Value}");
		return Content("OK");
	}
}
