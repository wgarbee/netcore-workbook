using System;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoApp.TagHelpers
{
    [HtmlTargetElement("*", Attributes = "DayOfWeek")]
    public class DayOfWeekTagHelper : TagHelper
    {
        public DateTime toDoDate { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            string stringDate = toDoDate > DateTime.Now.AddDays(-7) 
                ? toDoDate.DayOfWeek.ToString() 
                : toDoDate.ToString("M/d/yyyy");

            output.Content.SetContent(stringDate);
        }
    }
}
