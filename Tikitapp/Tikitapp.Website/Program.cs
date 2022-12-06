using Microsoft.EntityFrameworkCore;
using Tikitapp.Website.Data;
using Tikitapp.Website.Services.Mail;

var builder = WebApplication.CreateBuilder(args);

#if DEBUG
builder.Services.AddSassCompiler();
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
#endif

var smtpConfig = new SmtpMailOptions();
builder.Configuration.Bind("SmtpMail", smtpConfig);
builder.Services.AddTransient<IRenderEmails, EmailRenderer>();
builder.Services.AddTransient<IMailSender>(services => {
	var renderer = services.GetService<IRenderEmails>();
	return new SmtpMailSender(smtpConfig, renderer);
});
builder.Services.AddRouting(options => options.LowercaseUrls = true);

// Add services to the container.
builder.Services.AddControllersWithViews();

var sqlConnectionString = builder.Configuration.GetConnectionString("Tikitapp");
builder.Services.AddDbContext<TikitappDbContext>(options => options.UseSqlServer(sqlConnectionString));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()) {
	app.UseExceptionHandler("/Home/Error");
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

// We COULD make Program visible to our test project like this:
// public partial class Program {} 
