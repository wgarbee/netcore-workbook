using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BaseProject.Data;
using BaseProject.Models;

namespace BaseProject.Pages.IssueTasks
{
    public class DeleteModel : PageModel
    {
        private readonly BaseProject.Data.IssueTrackerContext _context;

        public DeleteModel(BaseProject.Data.IssueTrackerContext context)
        {
            _context = context;
        }

        [BindProperty]
        public IssueTask IssueTaskVM { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            IssueTaskVM = await _context.IssueTasks
                .Include(i => i.Assignee)
                .Include(i => i.Issue).FirstOrDefaultAsync(m => m.Id == id);

            if (IssueTaskVM == null)
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

            IssueTaskVM = await _context.IssueTasks.FindAsync(id);

            if (IssueTaskVM != null)
            {
                _context.IssueTasks.Remove(IssueTaskVM);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
