using MimeKit.Utils;
using Tikitapp.Website.Data.Entities;

namespace Tikitapp.Website.Services.Mail; 

public interface IMailSender {
	Task SendOrderConfirmationAsync(Order order);
}
