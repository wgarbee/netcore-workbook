using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BaseProject.Data;
using BaseProject.Models;

namespace BaseProject.Pages.Users
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
            return Page();
        }

        [BindProperty]
        public User UserVM { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Users.Add(UserVM);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}