using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using MimeKit.Text;
using Tikitapp.Website.Data;
using Tikitapp.Website.Models;
using Tikitapp.Website.Services.Mail;

namespace Tikitapp.Website.Controllers;

public class CheckoutController : Controller {
	private readonly ILogger<CheckoutController> logger;
	private readonly TikitappDbContext db;
	private readonly IMailSender mailSender;

	public CheckoutController(ILogger<CheckoutController> logger, 
		TikitappDbContext db,
		IMailSender mailSender
		) {
		this.logger = logger;
		this.db = db;
		this.mailSender = mailSender;
	}

	[HttpPost]
	public async Task<IActionResult> Confirm(ConfirmOrderPostModel post) {
		if (!ModelState.IsValid) return await Details(post.Id);
		var order = await db.Orders
			.Include(o => o.Show).ThenInclude(s => s.Artist)
			.Include(o => o.Show).ThenInclude(s => s.Venue)
			.Include(o => o.Contents).ThenInclude(item => item.TicketType)
			.FirstOrDefaultAsync(o => o.Id == post.Id);
		
		if (order == default) return NotFound();
		order.CustomerEmail = post.CustomerEmail;
		order.CustomerName = post.CustomerName;
		await db.SaveChangesAsync();
		await mailSender.SendOrderConfirmationAsync(order);
		return Content("OK! Your tickets are sent!");
	}

	public async Task<IActionResult> Details(Guid id) {
		var order = await db.Orders
			.Include(o => o.Show).ThenInclude(s => s.Artist)
			.Include(o => o.Show).ThenInclude(s => s.Venue)
			.Include(o => o.Contents)
			.FirstOrDefaultAsync(o => o.Id == id);
		return View("Details", order);
	}
}
