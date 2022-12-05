using System.Data;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Tikitapp.Website.Data;

public class TestDatabase : IDisposable {
	public static async Task<TikitappDbContext> CreateDbContext() {
		var tdb = new TestDatabase();
		await tdb.DbContext.PopulateWithTestDataAsync();
		return tdb.DbContext;
	}

	private TikitappDbContext dbContext;
	public TikitappDbContext DbContext => dbContext;

	private TestDatabase() {
		var sqliteConnection = new SqliteConnection("Filename=:memory:");
		sqliteConnection.Open();
		var options = new DbContextOptionsBuilder<TikitappDbContext>()
			.UseSqlite(sqliteConnection)
			.Options;		
		this.dbContext = new TikitappDbContext(options);
	}


	public void Dispose() {
		var connection = this.dbContext.Database.GetDbConnection();
		if (connection.State == ConnectionState.Open) {
			connection.Close();
			connection.Dispose();
		}
	}
}
