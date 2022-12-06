using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using MimeKit.Text;
using Tikitapp.Website.Data;
using Tikitapp.Website.Models;

namespace Tikitapp.Website.Controllers;

public class CheckoutController : Controller {
	private readonly ILogger<CheckoutController> logger;
	private readonly TikitappDbContext db;

	public CheckoutController(ILogger<CheckoutController> logger, TikitappDbContext db) {
		this.logger = logger;
		this.db = db;
	}

	private const string SMTP_USERNAME = "";
	private const string SMTP_PASSWORD = "";
	[HttpPost]
	public async Task<IActionResult> Confirm(ConfirmOrderPostModel post) {
		
		if (!ModelState.IsValid) return await Details(post.Id);

		var email = new MimeMessage();
		email.From.Add(MailboxAddress.Parse("orders@tikitapp.com"));
		email.To.Add(new MailboxAddress(post.CustomerName, post.CustomerEmail));
		email.Subject = "Your tickets are enclosed";
		email.Body = new TextPart(TextFormat.Plain) { Text = "Example Plain Text Message Body" };

		using var smtp = new SmtpClient();
		await smtp.ConnectAsync("smtp.mailtrap.io", 587, SecureSocketOptions.StartTls);
		await smtp.AuthenticateAsync(SMTP_USERNAME, SMTP_PASSWORD);
		await smtp.SendAsync(email);
		await smtp.DisconnectAsync(true);
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
