using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ToDoApp.Data;
using ToDoApp.Models;
using ToDoApp.Services;

namespace ToDoApp.Controllers
{
    public class ToDoController : Controller
    {
        private readonly AppContext _appContext;

        public ToDoController(AppContext appContext)
        {
            _appContext = appContext;
        }

        // GET: ToDo
        public ActionResult Index()
        {
            return View(_appContext.ToDos);
        }

        // GET: ToDo/Details/5
        public ActionResult Details(int id)
        {
            return View(_appContext.ToDos.Find(id));
        }

        // GET: ToDo/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ToDo/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ToDo toDo)
        {
            try
            {
                // TODO: Add insert logic here
                _appContext.Add(toDo);
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
            return View(_appContext.ToDos.Find(id));
        }

        // POST: ToDo/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ToDo toDo)
        {
            try
            {
                // TODO: Add update logic here
                toDo.Id = id;
                _appContext.ToDos.Update(toDo);
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
            return View(_appContext.ToDos.Find(id));
        }

        // POST: ToDo/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, ToDo collection)
        {
            try
            {
                // TODO: Add delete logic here
                var toDo = _appContext.ToDos.Find(id);
                _appContext.ToDos.Remove(toDo);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}