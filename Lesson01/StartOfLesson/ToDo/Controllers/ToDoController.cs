using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoApp.Models;
using ToDoApp.Services;

namespace ToDoApp.Controllers
{
    public class ToDoController : Controller
    {
        // GET: ToDo
        public ActionResult Index()
        {
            return View(Repository.list);
        }

        // GET: ToDo/Details/5
        public ActionResult Details(int id)
        {
            var toDo = Repository.GetToDoById(id);
            return View(toDo);
        }

        // GET: ToDo/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ToDo/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // call the repository with the new method we going to create
                Repository.CreateToDo(collection);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ToDo/Edit/5
        public ActionResult Edit(int id)
        {
            var toDo = Repository.GetToDoById(id);
            return View(toDo);
        }

        // POST: ToDo/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                Repository.SaveToDo(id, collection);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ToDo/Delete/5
        public ActionResult Delete(int id)
        {
            var toDo = Repository.GetToDoById(id);
            return View(toDo);
        }

        // POST: ToDo/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection) // Could remove IForm
        {
            try
            {
                Repository.DeleteToDo(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}