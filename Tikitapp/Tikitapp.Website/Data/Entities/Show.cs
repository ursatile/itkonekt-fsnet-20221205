namespace Tikitapp.Website.Data.Entities;

public class Show {
	public Guid Id { get; set; }
	public Venue Venue { get; set; } = null!;
	public Artist Artist { get; set; } = null!;
	public DateTimeOffset DoorsOpen { get; set; }
	public DateTimeOffset ShowStart { get; set; }
	public virtual List<TicketType> TicketTypes { get; set; } = new();
	public string Summary => $"{Artist.Name} at {Venue.Name} on {DoorsOpen:D}";

	public Order CreateOrder(Dictionary<Guid, int> quantities) => new() {
		Show = this,
		Contents = quantities.Select(pair => new OrderLineItem {
			TicketType = TicketTypes.Single(t => t.Id == pair.Key),
			Quantity = pair.Value
		}).ToList()
	};
}

