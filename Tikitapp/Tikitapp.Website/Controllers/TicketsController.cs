using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tikitapp.Website.Data;
using Tikitapp.Website.Data.Entities;
using Tikitapp.Website.Models;

namespace Tikitapp.Website.Controllers;

public class TicketsController : Controller {
	private readonly ILogger<TicketsController> logger;
	private readonly TikitappDbContext db;

	public TicketsController(ILogger<TicketsController> logger, TikitappDbContext db) {
		this.logger = logger;
		this.db = db;
	}

	private Show? LoadShow(Guid id) => db.Shows
			.Include(show => show.Artist)
			.Include(show => show.Venue)
			.Include(show => show.TicketTypes)
			.FirstOrDefault(show => show.Id == id);

	public IActionResult Show(Guid id) {
		var show = LoadShow(id);
		if (show == default) return NotFound();
		var model = show.ToBasketViewModel();
		return View(model);
	}

	[HttpPost]
	public IActionResult Update(BasketViewModel post, Guid? increment, Guid? decrement) {
		var show = LoadShow(post.ShowId);
		if (show == default) return NotFound();
		var quantities = post.Contents.ToDictionary(item => item.Id, item => item.Quantity);
		if (increment.HasValue) quantities[increment.Value]++;
		if (decrement.HasValue) quantities[decrement.Value]--;
		var model = show.ToBasketViewModel(quantities);
		return View("Show", model);
	}
}
