using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Cherukuri_Spring26.Data;
using Cherukuri_Spring26.Models;

namespace Cherukuri_Spring26.Controllers
{
    [Authorize]
    public class TenantsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TenantsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Tenants
        // Admin and Owner can see all tenants
        [Authorize(Roles = Constants.Admin + "," + Constants.Owner)]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Tenants.Include(t => t.LeaseAgreement);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Tenants/Details/5
        // Admin, Owner, and Tenant can view details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tenant = await _context.Tenants
                .Include(t => t.LeaseAgreement)
                .FirstOrDefaultAsync(m => m.PersonID == id);
            if (tenant == null)
            {
                return NotFound();
            }

            return View(tenant);
        }

        // GET: Tenants/Create
        // Admin only creates Tenant accounts
        [Authorize(Roles = Constants.Admin)]
        public IActionResult Create()
        {
            ViewData["LeaseID"] = new SelectList(_context.LeaseAgreements, "LeaseID", "LeaseID");
            return View();
        }

        // POST: Tenants/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Constants.Admin)]
        public async Task<IActionResult> Create([Bind("LeaseID,EmployerName,MonthlyIncome,PersonID,FirstName,LastName,Phone,Email,UserID")] Tenant tenant)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tenant);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LeaseID"] = new SelectList(_context.LeaseAgreements, "LeaseID", "LeaseID", tenant.LeaseID);
            return View(tenant);
        }

        // GET: Tenants/Edit/5
        // Admin and Owner can edit tenant info
        [Authorize(Roles = Constants.Admin + "," + Constants.Owner)]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tenant = await _context.Tenants.FindAsync(id);
            if (tenant == null)
            {
                return NotFound();
            }
            ViewData["LeaseID"] = new SelectList(_context.LeaseAgreements, "LeaseID", "LeaseID", tenant.LeaseID);
            return View(tenant);
        }

        // POST: Tenants/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Constants.Admin + "," + Constants.Owner)]
        public async Task<IActionResult> Edit(int id, [Bind("LeaseID,EmployerName,MonthlyIncome,PersonID,FirstName,LastName,Phone,Email,UserID")] Tenant tenant)
        {
            if (id != tenant.PersonID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tenant);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TenantExists(tenant.PersonID))
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
            ViewData["LeaseID"] = new SelectList(_context.LeaseAgreements, "LeaseID", "LeaseID", tenant.LeaseID);
            return View(tenant);
        }

        // GET: Tenants/Delete/5
        // Admin only
        [Authorize(Roles = Constants.Admin)]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tenant = await _context.Tenants
                .Include(t => t.LeaseAgreement)
                .FirstOrDefaultAsync(m => m.PersonID == id);
            if (tenant == null)
            {
                return NotFound();
            }

            return View(tenant);
        }

        // POST: Tenants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Constants.Admin)]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tenant = await _context.Tenants.FindAsync(id);
            if (tenant != null)
            {
                _context.Tenants.Remove(tenant);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TenantExists(int id)
        {
            return _context.Tenants.Any(e => e.PersonID == id);
        }
    }
}