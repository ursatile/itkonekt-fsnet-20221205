using Tikitapp.Website.Data.Entities;

namespace Tikitapp.Website.Services.Mail;

public interface IRenderEmails {
	public Task<string> MakeTextOrderConfirmationEmailAsync(Order order);
	public Task<string> MakeHtmlOrderConfirmationEmailAsync(Order order);
}

public class EmailRenderer : IRenderEmails {
	public async Task<string> MakeTextOrderConfirmationEmailAsync(Order order) {
		return await Task.FromResult("THIS IS TEXT");
	}

	public async Task<string> MakeHtmlOrderConfirmationEmailAsync(Order order) {
		return await Task.FromResult("<h1>This email is HTML</h1>");
	}
}
