using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoApp.Models;
using ToDoApp.Services;

namespace ToDoApp.Controllers
{
    public class StatusController : Controller
    {
        // GET: Status
        public ActionResult Index()
        {
            return View(Repository.Instance().Statuses);
        }

        // GET: Status/Details/5
        public ActionResult Details(int id)
        {
            return View(Repository.Instance().GetStatus(id));
        }

        // GET: Status/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Status/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Status status)
        {
            try
            {
                // TODO: Add insert logic here
                Repository.Instance().Add(status);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Status/Edit/5
        public ActionResult Edit(int id)
        {
            return View(Repository.Instance().GetStatus(id));
        }

        // POST: Status/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Status status)
        {
            try
            {
                // TODO: Add update logic here
                Repository.Instance().Update(id, status);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Status/Delete/5
        public ActionResult Delete(int id)
        {
            return View(Repository.Instance().GetStatus(id));
        }

        // POST: Status/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Status status)
        {
            try
            {
                // TODO: Add delete logic here
                Repository.Instance().DeleteStatus(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}