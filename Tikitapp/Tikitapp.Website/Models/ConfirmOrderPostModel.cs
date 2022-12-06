using System.ComponentModel.DataAnnotations;

namespace Tikitapp.Website.Models;

public class ConfirmOrderPostModel {
	public Guid Id { get; set; }

	[Required]
	[MaxLength(100)]
	public string CustomerName { get; set; } = null!;

	[EmailAddress]
	[Required]
	[MaxLength(100)]
	public string CustomerEmail { get; set; } = null!;
}
