using Microsoft.AspNetCore.Mvc;
using Tikitapp.Website.Data;

public class ArtistsController : Controller {
	private readonly ILogger<ArtistsController> logger;
	private readonly TikitappDbContext db;

	public ArtistsController(ILogger<ArtistsController> logger, TikitappDbContext db) {
		this.logger = logger;
		this.db = db;
	}
    
    public IActionResult Index() {
        var artists = db.Artists.ToList();
        return View(artists);        
    }

	public IActionResult Shows(Guid id) {
        var artist = db.Artists.Find(id);
		if (artist == default) return NotFound();
		return View(artist);
	}
}