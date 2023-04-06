using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RealEstateWA;
using RealEstateWA.Models;

namespace RealEstateWA.Controllers
{
    public class LocationsController : Controller
    {
        private readonly DbRealEstateContext _context;

        public LocationsController(DbRealEstateContext context)
        {
            _context = context;
        }

        // GET: Locations
        public async Task<IActionResult> Index(int? id)
        {
            if (id == null) return RedirectToAction("Cities", "Index");
            //знаходження локацій за містами
            ViewBag.CityId = id;
            ViewBag.CityName = _context.Cities.Where(b => b.Id == id).Single().Title;
            var dbRealEstateContext = _context.Locations.Where(b => b.CityId == id).Include(l => l.City);

            return View(await dbRealEstateContext.ToListAsync());
        }

        // GET: Locations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Locations == null)
            {
                return NotFound();
            }

            var location = await _context.Locations
                .Include(l => l.City)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (location == null)
            {
                return NotFound();
            }

            int cityId = _context.Locations.Where(b => b.Id == id).Single().CityId;
            ViewBag.CityId = cityId;
            ViewBag.CityName = _context.Cities.Where(b => b.Id == cityId).Single().Title;

            return View(location);
        }

        // GET: Locations/Create
        public IActionResult Create(int cityId)
        {
            // ViewData["CityId"] = new SelectList(_context.Cities, "Id", "Title");
            ViewBag.CityId = cityId;
            ViewBag.CityName = _context.Cities.Where(c => c.Id == cityId).Single().Title;
            return View();
        }

        // POST: Locations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int cityId, [Bind("Id,Area,Street,BrieflyAbout")] Location location)
        {
            location.CityId = cityId;
            if (ModelState.IsValid)
            {
                _context.Add(location);
                await _context.SaveChangesAsync();
                // return RedirectToAction(nameof(Index));
                return RedirectToAction("Index", "Locations", new { id = cityId });
            }
            // ViewData["CityId"] = new SelectList(_context.Cities, "Id", "Title", location.CityId);
            // return Problem(Convert.ToString(location.City));
            return RedirectToAction("Index", "Locations", new { id = cityId });
        }

        // GET: Locations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Locations == null)
            {
                return NotFound();
            }

            var location = await _context.Locations.FindAsync(id);
            if (location == null)
            {
                return NotFound();
            }
            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "Title", location.CityId);

            int cityId = _context.Locations.Where(b => b.Id == id).Single().CityId;
            ViewBag.CityId = cityId;
            ViewBag.CityName = _context.Cities.Where(b => b.Id == cityId).Single().Title;

            return View(location);
        }

        // POST: Locations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Area,Street,CityId,BrieflyAbout")] Location location)
        {
            if (id != location.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(location);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LocationExists(location.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), new { id = location.CityId });
            }
            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "Title", location.CityId);
            return View(location);
        }

        // GET: Locations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Locations == null)
            {
                return NotFound();
            }

            var location = await _context.Locations
                .Include(l => l.City)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (location == null)
            {
                return NotFound();
            }

            int cityId = _context.Locations.Where(b => b.Id == id).Single().CityId;
            ViewBag.CityId = cityId;
            ViewBag.CityName = _context.Cities.Where(b => b.Id == cityId).Single().Title;

            return View(location);
        }

        // POST: Locations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Locations == null)
            {
                return Problem("Entity set 'DbRealEstateContext.Locations'  is null.");
            }
            var location = await _context.Locations.FindAsync(id);
            int cityId = _context.Locations.Where(b => b.Id == id).Single().CityId;
            if (location != null)
            {
                _context.Locations.Remove(location);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Locations", new { id = cityId });
        }

        private bool LocationExists(int id)
        {
            return (_context.Locations?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

