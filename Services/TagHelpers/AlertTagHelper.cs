using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Services.TagHelpers
{
    [HtmlTargetElement("error-alert")]
    public class AlertTagHelper : TagHelper
    {
        public object Errors { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {

            if (Errors != null)
            {
                var errors = "<div class='row'><div class='col-lg-12'>";
                foreach (var i in (List<string>)Errors)
                {
                    errors += "<div class='alert alert-danger'>";
                    errors += i;
                    errors += "</div>";
                }
                errors += "</div></div>";
                output.Content.AppendHtml(errors);
            }
        }
    }
}
