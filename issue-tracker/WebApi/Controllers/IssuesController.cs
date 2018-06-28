using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using IssueTracker.Domain.Contracts;
using WebApi.Models;
using IssueTracker.Domain.Internal.Contracts;

namespace WebApi.Controllers
{
    public class IssuesController : Controller
    {
        private readonly IIssueRepository repository;

        public IssuesController(IIssueRepository repository)
        {
            this.repository = repository;
        }

        // GET: Issues
        public async Task<ActionResult> Index()
        {
            return View(await repository.GetIssues());
        }

        // GET: Issues/Details/5
        public async Task<ActionResult> Details(int id)
        {
            return View(await repository.GetIssue(id));
        }

        // GET: Issues/Create
        public async Task<ActionResult> Create()
        {
            ViewBag.NextKeyGuess = await repository.NextKeyGuess();
            return View();
        }

        // POST: Issues/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([FromForm] Models.Issue issue)
        {
            if (!ModelState.IsValid)
                return View();
            try
            {
                await repository.SaveIssue(issue);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Error while saving");
                return View();
            }
        }

        // GET: Issues/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            return View(await repository.GetIssue(id));
        }

        // POST: Issues/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, [FromForm] Models.Issue issue)
        {
            try
            {
                await repository.SaveIssue(issue);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}