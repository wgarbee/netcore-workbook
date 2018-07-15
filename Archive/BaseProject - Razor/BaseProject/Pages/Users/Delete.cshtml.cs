using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BaseProject.Data;
using BaseProject.Models;

namespace BaseProject.Pages.Users
{
    public class DeleteModel : PageModel
    {
        private readonly BaseProject.Data.IssueTrackerContext _context;

        public DeleteModel(BaseProject.Data.IssueTrackerContext context)
        {
            _context = context;
        }

        [BindProperty]
        public User UserVM { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            UserVM = await _context.Users.FirstOrDefaultAsync(m => m.Id == id);

            if (UserVM == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            UserVM = await _context.Users.FindAsync(id);

            if (User != null)
            {
                _context.Users.Remove(UserVM);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
