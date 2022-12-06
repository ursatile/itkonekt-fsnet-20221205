namespace Tikitapp.Website.Services.Mail;

public class SmtpMailOptions {
	public string Hostname { get; set; } = null!;
	public string Username { get; set; } = null!;
	public string Password { get; set; } = null!;
	public int Port { get; set; }
}
