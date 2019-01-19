using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ToDoApp.TagHelpers
{
    [HtmlTargetElement("input")]
    public class AutoCompleteTagHelper : TagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Attributes.Add("autocomplete", "off");
        }
    }

    //output.Attributes.Add("", "");
}
