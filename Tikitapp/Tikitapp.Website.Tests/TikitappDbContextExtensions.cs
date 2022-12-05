using Tikitapp.Website.Data;

public static class TikitappDbContextExtensions {
	public static async Task<TikitappDbContext> PopulateWithTestDataAsync(this TikitappDbContext db) {
        await db.Database.EnsureCreatedAsync();        
        db.Artists.Add(TestData.Artist1);
        db.Artists.Add(TestData.Artist2);      
        await db.SaveChangesAsync();
		return db;
	}
}
