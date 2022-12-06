using System.Globalization;
using Tikitapp.Website.Data.Entities;

namespace Tikitapp.Website.Models;

public class BasketViewModel {
	public Guid ShowId { get; set; }
	public string VenueName { get; set; } = String.Empty;
	public CultureInfo CultureInfo { get; set; } = CultureInfo.InvariantCulture;
	public string ArtistName { get; set; } = String.Empty;
	public string ShowDate { get; set; } = String.Empty;
	public List<TicketTypeViewModel> Contents { get; set; } = new();
	public decimal TotalPrice => Contents.Sum(t => t.TotalPrice);
	public string FormattedTotalPrice => TotalPrice.ToString("C", CultureInfo);
}

public class TicketTypeViewModel {
	public Guid Id { get; set; }
	public string FormattedPrice { get; set; } = String.Empty;
	public decimal Price { get; set; }
	public string Name { get; set; } = String.Empty;
	public int Quantity { get; set; } = 0;
	public decimal TotalPrice => Quantity * Price;
}

public static class EntityExtensions {
	public static BasketViewModel ToBasketViewModel(this Show show) {
		return show.ToBasketViewModel(new Dictionary<Guid, int>());
	}

	public static BasketViewModel ToBasketViewModel(this Show show, Dictionary<Guid, int> quantities) {
		return new BasketViewModel {
			ShowId = show.Id,
			ArtistName = show.Artist.Name,
			ShowDate = show.DoorsOpen.ToString("D", CultureInfo.InvariantCulture),
			VenueName = show.Venue.Name,
			CultureInfo = show.Venue.CultureInfo,
			Contents = show.TicketTypes.Select(t => new TicketTypeViewModel() {
				Id = t.Id,
				Name = t.Name,
				Price = t.Price,
				FormattedPrice = t.Price.ToString("C", show.Venue.CultureInfo),
				Quantity = Math.Max(quantities.GetValueOrDefault(t.Id), 0)
			}).ToList()
		};
	}
}

