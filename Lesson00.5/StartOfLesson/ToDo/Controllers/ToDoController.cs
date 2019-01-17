using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToDoApp.Models;

namespace ToDoApp.Controllers
{
    public class ToDoController : Controller
    {
        public List<Models.ToDo> list = new List<Models.ToDo>
        {
            new Models.ToDo{ ID = 1, Description = "Laundry", Status = new Status() { ID = 2, Value = "Complete" } },
            new Models.ToDo{ ID = 2, Description = "Finish the ToDo App", Status = new Status() { ID = 1, Value = "Pending" } },
            new Models.ToDo{ ID = 3, Description = "Do more things", Status = new Status() { ID = 1, Value = "Pending" } }

        };

        public IActionResult Index()
        {
            return View(list);
        }
    }
}