using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Services.TagHelpers
{
    [HtmlTargetElement( "asp-form")]
    public class FormAsp : TagHelper
    {
        public MethodType MethodType { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "form";
            output.Attributes.SetAttribute("method", MethodType.ToString());
            output.Attributes.SetAttribute("data-ajax", "true");
            output.Attributes.SetAttribute("data-ajax-url", this.Action);
            if (!string.IsNullOrEmpty(this.OnSuccess))
                output.Attributes.SetAttribute("data-ajax-success", OnSuccess);
            if (!string.IsNullOrEmpty(this.OnComplete))
                output.Attributes.SetAttribute("data-ajax-complete", this.OnComplete);

        }

        public string OnComplete { get; set; }
        public string Action { get; set; }
        public string OnSuccess { get; set; }

    }

    
}
