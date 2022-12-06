using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using Tikitapp.Website.Data.Entities;

namespace Tikitapp.Website.Services.Mail;

public class SmtpMailSender : IMailSender {
	private readonly SmtpMailOptions options;
	private readonly IRenderEmails renderer;

	public SmtpMailSender(SmtpMailOptions options, IRenderEmails renderer) {
		this.options = options;
		this.renderer = renderer;
	}

	private async Task<MimeMessage> BuildMessage(Order order) {
		var message = new MimeMessage();
		message.From.Add(new MailboxAddress("Tikitapp", "orders@tikitapp.com"));
		message.To.Add(new MailboxAddress(order.CustomerName, order.CustomerEmail));
		message.Subject = "Your gig tickets!";
		var builder = new BodyBuilder {
			TextBody = await renderer.MakeTextOrderConfirmationEmailAsync(order),
			HtmlBody = await renderer.MakeHtmlOrderConfirmationEmailAsync(order)
		};
		// builder.Attachments.Add(@"C:\Users\Joey\Documents\party.ics");
		message.Body = builder.ToMessageBody();
		return message;
	}

	public async Task SendOrderConfirmationAsync(Order order) {
		var message = await BuildMessage(order);
		using var smtp = new SmtpClient();
		await smtp.ConnectAsync(options.Hostname, options.Port, SecureSocketOptions.StartTls);
		await smtp.AuthenticateAsync(options.Username, options.Password);
		await smtp.SendAsync(message);
		await smtp.DisconnectAsync(true);
	}
}
