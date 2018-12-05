using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BankWithUs.Data;
using BankWithUs.Models;
using System.Net.Http;

namespace BankWithUs.Controllers
{
    public class AccountsController : Controller
    {
        private readonly IBankContext _context;

        public AccountsController(IBankContext context)
        {
            _context = context;
        }

        // GET: Accounts
        public async Task<IActionResult> Index()
        {
            
                return View(await _context.Accounts.ToListAsync());
            
        }

        // GET: Accounts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

           
                var account = await _context.Accounts
                  .FirstOrDefaultAsync(m => m.Id == id);
                if (account == null)
                {
                    return NotFound();
                }

                return View(account);
            
        }

        // GET: Accounts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Accounts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,SSN")] Account account)
        {
            if (!ModelState.IsValid)
            {
                return View(account);
            }
            var task = _context.Update(null);
            var count = task.CurrentValues;
                if (_context.Accounts.Any(a => a.Name == account.Name))
                {
                    return new StatusCodeResult((int)System.Net.HttpStatusCode.UnprocessableEntity);
                }

                if (!(await this.VerifyWithFBI(account.SSN)))
                {
                    return new StatusCodeResult((int)System.Net.HttpStatusCode.BadRequest);
                }
                account.DateCreated = DateTime.Now;
                _context.AddEntity(account);
                await _context.SaveChangesAsync();
                return View(nameof(Details), account);
            
        }

        // GET: Accounts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            
                var account = await _context.Accounts.FirstOrDefaultAsync(x => x.Id == id);
                if (account == null)
                {
                    return NotFound();
                }
                return View(account);
            
        }

        // POST: Accounts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,SSN")] Account account)
        {
            if (id != account.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                
                    try
                    {
                            _context.Update(account);
                            await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!AccountExists(account.Id))
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
            return View(account);
        }

        // GET: Accounts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

           
                var account = await _context.Accounts
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (account == null)
                {
                    return NotFound();
                }

                return View(account);
            
        }

        // POST: Accounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
           
                var account = await _context.Accounts.FirstOrDefaultAsync(x => x.Id == id);
                _context.Remove(account);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            
        }

        private async Task<bool> VerifyWithFBI(string ssn)
        {
            HttpClient client = new HttpClient();
            string response = await client.GetStringAsync("https://jsonplaceholder.typicode.com/posts/1");
            if (!string.IsNullOrWhiteSpace(response))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool AccountExists(int id)
        {
            return _context.Accounts.Any(e => e.Id == id);
        }
    }
}
