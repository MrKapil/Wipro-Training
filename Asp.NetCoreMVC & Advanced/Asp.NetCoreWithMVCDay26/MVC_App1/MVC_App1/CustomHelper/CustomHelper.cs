using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text;

namespace MVC_App1.CustomHelper
{
    [HtmlTargetElement("My-first-tag-helper")]
    public class CustomHelper : TagHelper
    {
        public string Name { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "CustomerTagHelper";
            output.TagMode = TagMode.StartTagAndEndTag;

            var sb = new StringBuilder();

            sb.AppendFormat("<span>Hi (0) </sapn>", this.Name);

            output.PreContent.SetHtmlContent(sb.ToString());
        }

    }
}
