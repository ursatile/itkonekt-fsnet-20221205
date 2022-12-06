namespace Tikitapp.Website.Data.Entities;

public class Order {
	public Guid Id { get; set; }
	public Show Show { get; set; } = null!;
	public virtual List<OrderLineItem> Contents { get; set; } = new();
	public string CustomerName { get; set; } = String.Empty;
	public string CustomerEmail { get; set; } = String.Empty;
}
