using System.Reflection;
using RazorEngine.Templating;
using Tikitapp.Website.Data.Entities;

namespace Tikitapp.Website.Services.Mail;

public interface IRenderEmails {
	public Task<string> MakeTextOrderConfirmationEmailAsync(Order order);
	public Task<string> MakeHtmlOrderConfirmationEmailAsync(Order order);
}

public class EmailRenderer : IRenderEmails {

	private readonly IRazorEngineService razorEngine;

	public EmailRenderer(IRazorEngineService razorEngine) {
		this.razorEngine = razorEngine;
		CompileTextTemplate(Templates.TICKET_TEXT, typeof(Order));
		CompileHtmlTemplate(Templates.TICKET_HTML, typeof(Order));
	}

	private static string ReadEmbeddedResource(string resourceFileName) {
		var assembly = Assembly.GetAssembly(typeof(EmailRenderer));
		var name = assembly.GetManifestResourceNames().FirstOrDefault(n => n.EndsWith(resourceFileName));
		var stream = assembly.GetManifestResourceStream(name);
		return new StreamReader(stream).ReadToEnd();
	}

	public static class Templates {
		public const string TICKET_HTML = "tickets.cshtml";
		public const string TICKET_TEXT = "tickets.txt";
	}

	private string Render(string template, object model) =>
		razorEngine.Run(template, model.GetType(), model);

	private void CompileTextTemplate(string key, Type modelType) {
		razorEngine.AddTemplate(key, ReadEmbeddedResource(key));
		razorEngine.Compile(key, modelType);
	}

	private void CompileHtmlTemplate(string key, Type modelType) {
		var templateSource = ReadEmbeddedResource(key);
		//#if DEBUG
		//		templateSource = RenderMjmlIntoRazor(templateSource);
		//#endif
		razorEngine.AddTemplate(key, templateSource);
		razorEngine.Compile(key, modelType);
	}
	
	public async Task<string> MakeTextOrderConfirmationEmailAsync(Order order)
		=> await Task.FromResult(Render(Templates.TICKET_TEXT, order));

	public async Task<string> MakeHtmlOrderConfirmationEmailAsync(Order order)
		=> await Task.FromResult(Render(Templates.TICKET_HTML, order));
}

