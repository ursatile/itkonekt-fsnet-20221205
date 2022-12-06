using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Tikitapp.Website.Data.Entities;

namespace Tikitapp.Website.Data;

public class TikitappDbContext : DbContext {

	public TikitappDbContext(DbContextOptions<TikitappDbContext> options)
	: base(options) { }

	public virtual DbSet<Artist> Artists => Set<Artist>();
	public virtual DbSet<Venue> Venues => Set<Venue>();
	public virtual DbSet<Show> Shows => Set<Show>();
	public virtual DbSet<TicketType> TicketTypes => Set<TicketType>();
	public virtual DbSet<Order> Orders => Set<Order>();

	protected override void OnModelCreating(ModelBuilder builder) {
		base.OnModelCreating(builder);

		ConfigureSlugs(builder);
		builder.Entity<OrderLineItem>(entity => entity.ToTable("OrderLineItems"));

		builder.Entity<Artist>(entity => {
			entity.HasMany(a => a.Shows).WithOne(show => show.Artist);
		});

		builder.Entity<Venue>(entity => {
			entity.HasMany(e => e.Shows).WithOne(e => e.Venue);
			entity.Property(v => v.CultureInfoName).HasMaxLength(16).IsUnicode(false);
		});

		builder.Entity<TicketType>(entity => {
			entity.Property(e => e.Price).HasColumnType("money");
		});

		builder.Entity<Order>(entity => {
			entity
				.HasMany(o => o.Contents)
				.WithOne(item => item.Order)
				.OnDelete(DeleteBehavior.Restrict);
		});
	}

	private static bool IsSlug(IMutableProperty prop) {
		var info = prop.PropertyInfo;
		if (info == null) return false;
		if (info.PropertyType != typeof(string)) return false;
		return info.Name == "Slug";
	}

	private static void ConfigureSlugs(ModelBuilder builder) {
		foreach (var entity in builder.Model.GetEntityTypes()) {
			var properties = entity.GetProperties();
			foreach (var slug in properties.Where(IsSlug)) {
				slug.SetIsUnicode(false);
				slug.SetMaxLength(100);
				entity.AddIndex(slug).IsUnique = true;
			}
		}
	}
}


