using System.ComponentModel.DataAnnotations;

namespace Tikitapp.Website.Data.Entities;

public class Artist {
	public Guid Id { get; set; }
	[MaxLength(100)]
	public string Name { get; set; } = String.Empty;
	[MaxLength(100)]
	public string Slug { get; set; } = String.Empty;

	public virtual List<Show> Shows { get; set; } = new();
}

public class Order {
	public Guid Id { get; set; }
	public Show Show { get; set; } = null!;
	public virtual List<OrderLineItem> Contents { get; set; } = new();
	public string CustomerName { get; set; } = String.Empty;
	public string CustomerEmail { get; set; } = String.Empty;
}

public class OrderLineItem {
	public Guid Id { get; set; }
	public Order Order { get; set; } = null!;
	public int Quantity { get; set; } = 0;
	public TicketType TicketType { get; set; } = null!;
}
