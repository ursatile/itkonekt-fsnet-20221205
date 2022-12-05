using Microsoft.EntityFrameworkCore;
using Tikitapp.Website.Data.Entities;

namespace Tikitapp.Website.Data;

public class TikitappDbContext : DbContext {

	public TikitappDbContext(DbContextOptions<TikitappDbContext> options)
	: base(options) { }

	public DbSet<Artist> Artists => Set<Artist>();

	protected override void OnModelCreating(ModelBuilder modelBuilder) {
		base.OnModelCreating(modelBuilder);
		modelBuilder.Entity<Artist>(entity => {
			entity.Property(e => e.Slug).IsUnicode(false);
			entity.HasIndex(e => e.Slug).IsUnique();
		});		
	}
}


