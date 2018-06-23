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
    public class IndexModel : PageModel
    {
        private readonly BaseProject.Data.IssueTrackerContext _context;

        public IndexModel(BaseProject.Data.IssueTrackerContext context)
        {
            _context = context;
        }

        public IList<User> UserVM { get;set; }

        public async Task OnGetAsync()
        {
            UserVM = await _context.Users.ToListAsync();
        }
    }
}
