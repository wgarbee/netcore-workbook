using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BaseProject.Data;
using BaseProject.Models;

namespace BaseProject.Pages.IssueTasks
{
    public class CreateModel : PageModel
    {
        private readonly BaseProject.Data.IssueTrackerContext _context;

        public CreateModel(BaseProject.Data.IssueTrackerContext context)
        {
            _context = context;
        }

        public IActionResult OnGet(int issueId)
        {
            ViewData["AssigneeId"] = new SelectList(_context.Users, nameof(Models.User.Id), nameof(Models.User.FirstName));
            ViewData["IssueId"] = new SelectList(_context.Issues, nameof(Models.Issue.Id), nameof(Models.Issue.Description));
            ViewData["StatusId"] = new SelectList(Enum.GetValues(typeof(Status)));
            return Page();
        }

        [BindProperty]
        public IssueTask IssueTaskVM { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.IssueTasks.Add(IssueTaskVM);
            await _context.SaveChangesAsync();

            return RedirectToPage("../Issues/Details", new { id = IssueTaskVM.IssueId });
        }
    }
}