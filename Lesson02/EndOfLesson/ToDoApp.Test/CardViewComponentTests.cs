using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Moq;
using ToDoApp.Models;
using ToDoApp.Services;
using ToDoApp.ViewComponents;
using Xunit;

namespace ToDoApp.Test
{
    public class CardViewComponentTests
    {
        private List<ToDo> getMockData()
        {
            var mockNow = new DateTime(2018, 12, 24);
            var inProgress = new Status
            {
                Id = 2,
                Value = "In Progress"
            };
            var notStarted = new Status
            {
                Id = 1,
                Value = "Not Started"
            };
            return new List<ToDo>
            {
                new ToDo
                {
                    Id = 1,
                    Title = "My First ToDo",
                    Description = "Get the app working",
                    Status = inProgress,
                    Created = mockNow
                },
                new ToDo
                {
                    Id = 2,
                    Title = "Add DateTime",
                    Description = "Should track when the ToDo was created",
                    Status = notStarted,
                    Created = mockNow.AddDays(4)
                },
                new ToDo
                {
                    Id = 3,
                    Title = "Add day-of-the-week TagHelper",
                    Description = "Need an attribute we can use in our view that will pretty format the DateTime as a weekday when possible",
                    Status = notStarted,
                    Created = mockNow.AddDays(-4)
                },
                new ToDo
                {
                    Id = 4,
                    Title = "Add ViewComponent",
                    Description = "Should track when the ToDo was created",
                    Status = notStarted,
                    Created = mockNow.AddDays(-60)
                },
                new ToDo
                {
                    Id = 5,
                    Title = "Make Tests Pass",
                    Description = "Should track when the ToDo was created",
                    Status = notStarted,
                    Created = mockNow.AddDays(60)
                }
            };
        }

        [Fact]
        public void CardGroup_ShouldGetRequestedCountForView()
        {
            // Arrange
            Mock<IRepository> mock = new Mock<IRepository>();
            mock.SetupGet(x => x.ToDos).Returns(getMockData());
            var viewComponent = new CardGroupViewComponent(mock.Object);
            var urlHelper = new Mock<IUrlHelper>();
            urlHelper.Setup(o => o.Action(It.IsAny<UrlActionContext>())).Returns("action/controller");
            viewComponent.Url = urlHelper.Object;

            //Act
            var componentResult = viewComponent.Invoke(3) as ViewViewComponentResult;
            var model = componentResult.ViewData.Model as IEnumerable<CardViewModel>;

            //Assert
            Assert.Equal(3, model.Count());
        }

        [Fact]
        public void CardGroup_ShouldOrderCardsOldestFirstForView()
        {
            // Arrange
            var regex = new Regex("Created (.*) days ago");
            Mock<IRepository> mock = new Mock<IRepository>();
            mock.SetupGet(x => x.ToDos).Returns(getMockData());
            var viewComponent = new CardGroupViewComponent(mock.Object);
            var urlHelper = new Mock<IUrlHelper>();
            urlHelper.Setup(o => o.Action(It.IsAny<UrlActionContext>())).Returns("action/controller");
            viewComponent.Url = urlHelper.Object;

            //Act
            var componentResult = viewComponent.Invoke(3) as ViewViewComponentResult;
            var model = (componentResult.ViewData.Model as IEnumerable<CardViewModel>).ToList();

            //Assert
            for (int i = 0; i < model.Count; i++)
            {
                var card = model[i];
                var cardMatch = regex.Match(card.ActionDescription).Groups.Skip(1).First().Value;
                var cardDay = int.Parse(cardMatch);
                foreach (var item in model.Skip(i))
                {
                    var itemMatch = regex.Match(item.ActionDescription).Groups.Skip(1).First().Value;
                    var itemDay = int.Parse(itemMatch);
                    Assert.True(cardDay >= itemDay);
                }
            }
        }
    }
}
