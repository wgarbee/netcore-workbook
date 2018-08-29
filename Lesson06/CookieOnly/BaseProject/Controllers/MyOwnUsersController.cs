using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BaseProject.Data;
using BaseProject.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BaseProject.Controllers
{
    [Route("MyOwnUsers")]
    public class MyOwnUsersController : Controller
    {
        private readonly ApplicationContext _context;

        public MyOwnUsersController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: SignIn
        [Route("SignIn")]
        public async Task<IActionResult> SignIn()
        {
            return View();
        }

        // GET: SignIn
        [Route("SignIn")]
        public async Task<IActionResult> SignIn([Bind("UserName,Password")]MyOwnUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, user.IsAdmin ? "Admin" : "User"),
            };
            var identity = new ClaimsIdentity(claims);
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(principal);
            return RedirectToAction(nameof(Index), nameof(IssueController));
        }

        // GET: MyOwnUsers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Users.ToListAsync());
        }

        // GET: MyOwnUsers/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var myOwnUser = await _context.Users
                .FirstOrDefaultAsync(m => m.UserName == id);
            if (myOwnUser == null)
            {
                return NotFound();
            }

            return View(myOwnUser);
        }

        // GET: MyOwnUsers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MyOwnUsers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserName,Password,IsAdmin")] MyOwnUser myOwnUser)
        {
            if (ModelState.IsValid)
            {
                _context.Add(myOwnUser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(myOwnUser);
        }

        // GET: MyOwnUsers/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var myOwnUser = await _context.Users.FindAsync(id);
            if (myOwnUser == null)
            {
                return NotFound();
            }
            return View(myOwnUser);
        }

        // POST: MyOwnUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("UserName,Password,IsAdmin")] MyOwnUser myOwnUser)
        {
            if (id != myOwnUser.UserName)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(myOwnUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MyOwnUserExists(myOwnUser.UserName))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(myOwnUser);
        }

        // GET: MyOwnUsers/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var myOwnUser = await _context.Users
                .FirstOrDefaultAsync(m => m.UserName == id);
            if (myOwnUser == null)
            {
                return NotFound();
            }

            return View(myOwnUser);
        }

        // POST: MyOwnUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var myOwnUser = await _context.Users.FindAsync(id);
            _context.Users.Remove(myOwnUser);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MyOwnUserExists(string id)
        {
            return _context.Users.Any(e => e.UserName == id);
        }
    }
}
