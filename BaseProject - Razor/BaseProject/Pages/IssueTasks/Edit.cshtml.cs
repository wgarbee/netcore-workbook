using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BaseProject.Data;
using BaseProject.Models;

namespace BaseProject.Pages.IssueTasks
{
    public class EditModel : PageModel
    {
        private readonly BaseProject.Data.IssueTrackerContext _context;

        public EditModel(BaseProject.Data.IssueTrackerContext context)
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
           ViewData["AssigneeId"] = new SelectList(_context.Users, "Id", "Id");
           ViewData["IssueId"] = new SelectList(_context.Issues, "Id", "Id");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(IssueTaskVM).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IssueTaskExists(IssueTaskVM.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool IssueTaskExists(int id)
        {
            return _context.IssueTasks.Any(e => e.Id == id);
        }
    }
}
