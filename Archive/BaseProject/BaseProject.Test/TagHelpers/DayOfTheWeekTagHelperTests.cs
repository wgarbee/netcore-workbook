using BaseProject.Data;
using BaseProject.Intrastructure;
using FluentAssertions;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BaseProject.Test.TagHelpers
{
    public class DayOfTheWeekTagHelperTests
    {
        [Fact]
        public void TagHelper_ShouldShowCurrentDayOfTheWeek()
        {
            // Assemble
            var dateTimeProvider = new Mock<IDateTimeProvider>();
            dateTimeProvider.SetupGet(x => x.Now).Returns(new DateTime(2018, 07, 05));
            TagHelper myTagHelper = new DayOfTheWeekTagHelper(dateTimeProvider.Object);
            var context = new TagHelperContext(
                new TagHelperAttributeList(),
                new Dictionary<object, object>(),
                Guid.NewGuid().ToString("N"));
            var output = new TagHelperOutput(
                "day-of-week",
                new TagHelperAttributeList(),
                (useCachedResult, encoder) =>
                {
                    var tagHelperContent = new DefaultTagHelperContent();
                    tagHelperContent.SetContent(string.Empty);
                    return Task.FromResult<TagHelperContent>(tagHelperContent);
                }
                );

            // Act
            myTagHelper.Process(context, output);

            // Assert
            output.TagName.Should().Be("span");
            output.Content.GetContent().Should().Be("Thursday");
        }
    }
}
