using Microsoft.EntityFrameworkCore;
using Tikitapp.Website.Data.Entities;

namespace Tikitapp.Website.Data;

public class TikitappDbContext : DbContext {

	public TikitappDbContext(DbContextOptions<TikitappDbContext> options)
	: base(options) { }
    
	public DbSet<Artist> Artists => Set<Artist>();
}


