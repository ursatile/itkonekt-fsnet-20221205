namespace Tikitapp.Website.Data.Entities;

public class OrderLineItem {
	public Guid Id { get; set; }
	public Order Order { get; set; } = null!;
	public int Quantity { get; set; } = 0;
	public TicketType TicketType { get; set; } = null!;
}
