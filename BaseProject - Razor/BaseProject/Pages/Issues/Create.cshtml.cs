using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BaseProject.Data;
using BaseProject.Models;

namespace BaseProject.Pages.Issues
{
    public class CreateModel : PageModel
    {
        private readonly BaseProject.Data.IssueTrackerContext _context;

        public CreateModel(BaseProject.Data.IssueTrackerContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["AssigneeId"] = new SelectList(_context.Users, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public Issue Issue { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Issues.Add(Issue);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}