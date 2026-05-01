using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Cherukuri_Spring26.Data;
using Cherukuri_Spring26.Models;

namespace Cherukuri_Spring26.Controllers
{
    // Admin only - manages who owns which property
    [Authorize(Roles = Constants.Admin)]
    public class PropertyOwnersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PropertyOwnersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PropertyOwners
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.PropertyOwners
                .Include(p => p.Owner)
                .Include(p => p.Property);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: PropertyOwners/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var propertyOwner = await _context.PropertyOwners
                .Include(p => p.Owner)
                .Include(p => p.Property)
                .FirstOrDefaultAsync(m => m.PropertyOwnerID == id);
            if (propertyOwner == null)
            {
                return NotFound();
            }

            return View(propertyOwner);
        }

        // GET: PropertyOwners/Create
        public IActionResult Create()
        {
            ViewData["OwnerID"] = new SelectList(_context.Owners, "PersonID", "FullName");
            ViewData["PropertyID"] = new SelectList(_context.Properties, "PropertyID", "FullAddress");
            return View();
        }

        // POST: PropertyOwners/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PropertyOwnerID,PropertyID,OwnerID")] PropertyOwner propertyOwner)
        {
            if (ModelState.IsValid)
            {
                _context.Add(propertyOwner);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OwnerID"] = new SelectList(_context.Owners, "PersonID", "FullName", propertyOwner.OwnerID);
            ViewData["PropertyID"] = new SelectList(_context.Properties, "PropertyID", "FullAddress", propertyOwner.PropertyID);
            return View(propertyOwner);
        }

        // GET: PropertyOwners/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var propertyOwner = await _context.PropertyOwners.FindAsync(id);
            if (propertyOwner == null)
            {
                return NotFound();
            }
            ViewData["OwnerID"] = new SelectList(_context.Owners, "PersonID", "FullName", propertyOwner.OwnerID);
            ViewData["PropertyID"] = new SelectList(_context.Properties, "PropertyID", "FullAddress", propertyOwner.PropertyID);
            return View(propertyOwner);
        }

        // POST: PropertyOwners/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PropertyOwnerID,PropertyID,OwnerID")] PropertyOwner propertyOwner)
        {
            if (id != propertyOwner.PropertyOwnerID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(propertyOwner);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PropertyOwnerExists(propertyOwner.PropertyOwnerID))
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
            ViewData["OwnerID"] = new SelectList(_context.Owners, "PersonID", "FullName", propertyOwner.OwnerID);
            ViewData["PropertyID"] = new SelectList(_context.Properties, "PropertyID", "FullAddress", propertyOwner.PropertyID);
            return View(propertyOwner);
        }

        // GET: PropertyOwners/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var propertyOwner = await _context.PropertyOwners
                .Include(p => p.Owner)
                .Include(p => p.Property)
                .FirstOrDefaultAsync(m => m.PropertyOwnerID == id);
            if (propertyOwner == null)
            {
                return NotFound();
            }

            return View(propertyOwner);
        }

        // POST: PropertyOwners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var propertyOwner = await _context.PropertyOwners.FindAsync(id);
            if (propertyOwner != null)
            {
                _context.PropertyOwners.Remove(propertyOwner);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PropertyOwnerExists(int id)
        {
            return _context.PropertyOwners.Any(e => e.PropertyOwnerID == id);
        }
    }
}