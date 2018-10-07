using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ToDoApp.Models;
using ToDoApp.Services;

namespace ToDoApp.ViewComponents
{
    public class CardGroupViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(int count)
        {
            var dueSoon = Repository.ToDos.OrderByDescending(x => x.Created).Take(Math.Min(12, count)).ToList();
            var width = dueSoon.Count > 0 ? Convert.ToInt32(Math.Floor(12m / dueSoon.Count)) : 12;
            var model = dueSoon.Select(x => new CardViewModel
            {
                Title = x.Title,
                Summary = x.Description,
                Action = $"Due in {(x.Created?.Subtract(DateTime.Now).TotalDays ?? 0):N0} days",
                Width = width
            });
            return View(model);
        }
    }
}
