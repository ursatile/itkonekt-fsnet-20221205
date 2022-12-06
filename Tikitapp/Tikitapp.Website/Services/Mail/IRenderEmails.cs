using System.Reflection;
using Mjml.AspNetCore;
using RazorEngine.Templating;
using Tikitapp.Website.Data.Entities;

namespace Tikitapp.Website.Services.Mail;

public interface IRenderEmails {
	public Task<string> MakeTextOrderConfirmationEmailAsync(Order order);
	public Task<string> MakeHtmlOrderConfirmationEmailAsync(Order order);
}

public class EmailRenderer : IRenderEmails {

	private readonly IRazorEngineService razorEngine;
	private readonly IMjmlServices mjmlEngine;


	public EmailRenderer(IMjmlServices mjmlEngine, IRazorEngineService razorEngine) {
		this.mjmlEngine = mjmlEngine;
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
#if DEBUG
		public const string TICKET_HTML = "tickets.mjml";
#else
		public const string TICKET_HTML = "tickets.cshtml";
#endif
		public const string TICKET_TEXT = "tickets.txt";
	}

	private string Render(string template, object model) =>
		razorEngine.Run(template, model.GetType(), model);

	private readonly string[] cssAtRules = {
		"bottom-center", "bottom-left", "bottom-left-corner", "bottom-right", "bottom-right-corner", "charset", "counter-style",
		"document", "font-face", "font-feature-values", "import", "left-bottom", "left-middle", "left-top", "keyframes", "media",
		"namespace", "page", "right-bottom", "right-middle", "right-top", "supports", "top-center", "top-left", "top-left-corner",
		"top-right", "top-right-corner"
	};

	private string EscapeCssRulesInRazorTemplate(string razor) =>
		cssAtRules.Aggregate(razor, (current, rule) => current.Replace($"@{rule}", $"@@{rule}"));


	private string RenderMjmlIntoRazor(string mjml) {
		var razor = mjmlEngine.Render(mjml).Result.Html;
		return EscapeCssRulesInRazorTemplate(razor);
	}

	private void CompileTextTemplate(string key, Type modelType) {
		razorEngine.AddTemplate(key, ReadEmbeddedResource(key));
		razorEngine.Compile(key, modelType);
	}

	private void CompileHtmlTemplate(string key, Type modelType) {
		var templateSource = ReadEmbeddedResource(key);
#if DEBUG
		templateSource = RenderMjmlIntoRazor(templateSource);
#endif
		razorEngine.AddTemplate(key, templateSource);
		razorEngine.Compile(key, modelType);
	}

	public async Task<string> MakeTextOrderConfirmationEmailAsync(Order order)
		=> await Task.FromResult(Render(Templates.TICKET_TEXT, order));

	public async Task<string> MakeHtmlOrderConfirmationEmailAsync(Order order)
		=> await Task.FromResult(Render(Templates.TICKET_HTML, order));
}

