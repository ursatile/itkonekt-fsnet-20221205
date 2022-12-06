using Microsoft.AspNetCore.Razor.TagHelpers;
using Tikitapp.Website.Data;

namespace Tikitapp.Website.TagHelpers;
public class CountryFlagTagHelper : TagHelper {
	public Country Country { get; set; } = Country.Unknown;

	public override void Process(TagHelperContext context, TagHelperOutput output) {
		output.TagName = "img";
		output.TagMode = TagMode.SelfClosing;
		output.Attributes.Add("src", $"/images/countries/{Country.Code}.png");
		output.Attributes.Add("title", Country.Name);
		output.Attributes.Add("alt", $"The flag of {Country.Name}");
	}
}

