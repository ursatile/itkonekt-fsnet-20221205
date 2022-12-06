namespace Tikitapp.Website.Models;

public class BasketViewModel {
	public Guid ShowId { get; set; }
	public string VenueName { get; set; } = String.Empty;
	public string ArtistName { get; set; } = String.Empty;
	public string ShowDate { get; set; } = String.Empty;
	public List<TicketTypeViewModel> Contents { get; set; } = new();
}

public class TicketTypeViewModel {
	public Guid Id { get; set; }
	public string FormattedPrice { get; set; } = String.Empty;
	public decimal Price { get; set; }
	public string Name { get; set; } = String.Empty;
	public int Quantity { get; set; } = 0;
}

