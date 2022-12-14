using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Tikitapp.Website.Data.Entities;

public class Venue {
	public Guid Id { get; set; }
	[MaxLength(100)]
	public string Name { get; set; } = String.Empty;
	[MaxLength(100)]
	public string Slug { get; set; } = String.Empty;
	public string StreetAddress { get; set; } = String.Empty;
	public string CultureInfoName { get; set; } = String.Empty;
	public virtual List<Show> Shows { get; set; } = new();

	public CultureInfo CultureInfo => new(CultureInfoName);

	public Country Country => Country.FromCultureInfoName(CultureInfoName);

	public string CountryName => Country.Name;
}
