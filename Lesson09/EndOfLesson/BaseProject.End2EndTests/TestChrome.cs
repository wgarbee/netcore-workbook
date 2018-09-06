using System.IO;
using System.Linq;
using System.Reflection;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Xunit;

namespace BaseProject.End2EndTests
{
    public class TestChrome
    {
        [Fact]
        public void PageLoads()
        {
            using (var driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)))
            {
                driver.Navigate().GoToUrl(@"https://localhost:5000");
                var link = driver.FindElementByXPath("//body/nav/div/div/a");
                Assert.Contains("E2E", link.Text);
            }
        }

        [Fact]
        public void CreatingNewIssuePopulatesListView()
        {
            var taskTitle = "I am a new task";
            var taskDescription = "My first task!";

            using (var driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)))
            {
                driver.Navigate().GoToUrl(@"https://localhost:5000");
                var links = driver.FindElementsByXPath("//a");
                var createLink = links.FirstOrDefault(link => link.Text == "Create New");
                createLink.Click();
                var titleInput = driver.FindElementById("Title");
                var descriptionInput = driver.FindElementById("Description");
                var saveButton = driver.FindElementByCssSelector("input.btn.btn-default");
                titleInput.SendKeys(taskTitle);
                descriptionInput.SendKeys(taskDescription);
                saveButton.Click();
                var grid = driver.FindElementsByCssSelector("tbody tr");
                foreach (var row in grid)
                {
                    var columns = row.FindElements(By.CssSelector("td"));
                    var col1 = columns.FirstOrDefault();
                    var col2 = columns.Skip(1).FirstOrDefault();
                    if (col1.Text == taskTitle && col2.Text == taskDescription)
                        return;
                }
                throw new NotFoundException("Expected row not found");
            }
        }
    }
}
