using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Cherukuri_Spring26.Data;
using Cherukuri_Spring26.Models;

namespace Cherukuri_Spring26.Controllers
{
    [Authorize]
    public class LeaseAgreementsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LeaseAgreementsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: LeaseAgreements
        // Admin, Owner, Tenant can all view
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.LeaseAgreements.Include(l => l.Property);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: LeaseAgreements/Details/5
        // Admin, Owner, Tenant can all view
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaseAgreement = await _context.LeaseAgreements
                .Include(l => l.Property)
                .FirstOrDefaultAsync(m => m.LeaseID == id);
            if (leaseAgreement == null)
            {
                return NotFound();
            }

            return View(leaseAgreement);
        }

        // GET: LeaseAgreements/Create
        // Admin and Owner only
        [Authorize(Roles = Constants.Admin + "," + Constants.Owner)]
        public IActionResult Create()
        {
            ViewData["PropertyID"] = new SelectList(_context.Properties, "PropertyID", "Address");
            return View();
        }

        // POST: LeaseAgreements/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Constants.Admin + "," + Constants.Owner)]
        public async Task<IActionResult> Create([Bind("LeaseID,PropertyID,StartDate,EndDate,MonthlyRent")] LeaseAgreement leaseAgreement)
        {
            if (ModelState.IsValid)
            {
                _context.Add(leaseAgreement);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PropertyID"] = new SelectList(_context.Properties, "PropertyID", "Address", leaseAgreement.PropertyID);
            return View(leaseAgreement);
        }

        // GET: LeaseAgreements/Edit/5
        // Admin and Owner only
        [Authorize(Roles = Constants.Admin + "," + Constants.Owner)]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaseAgreement = await _context.LeaseAgreements.FindAsync(id);
            if (leaseAgreement == null)
            {
                return NotFound();
            }
            ViewData["PropertyID"] = new SelectList(_context.Properties, "PropertyID", "Address", leaseAgreement.PropertyID);
            return View(leaseAgreement);
        }

        // POST: LeaseAgreements/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Constants.Admin + "," + Constants.Owner)]
        public async Task<IActionResult> Edit(int id, [Bind("LeaseID,PropertyID,StartDate,EndDate,MonthlyRent")] LeaseAgreement leaseAgreement)
        {
            if (id != leaseAgreement.LeaseID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(leaseAgreement);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LeaseAgreementExists(leaseAgreement.LeaseID))
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
            ViewData["PropertyID"] = new SelectList(_context.Properties, "PropertyID", "Address", leaseAgreement.PropertyID);
            return View(leaseAgreement);
        }

        // GET: LeaseAgreements/Delete/5
        // Admin only
        [Authorize(Roles = Constants.Admin)]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaseAgreement = await _context.LeaseAgreements
                .Include(l => l.Property)
                .FirstOrDefaultAsync(m => m.LeaseID == id);
            if (leaseAgreement == null)
            {
                return NotFound();
            }

            return View(leaseAgreement);
        }

        // POST: LeaseAgreements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Constants.Admin)]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var leaseAgreement = await _context.LeaseAgreements.FindAsync(id);
            if (leaseAgreement != null)
            {
                _context.LeaseAgreements.Remove(leaseAgreement);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LeaseAgreementExists(int id)
        {
            return _context.LeaseAgreements.Any(e => e.LeaseID == id);
        }
    }
}