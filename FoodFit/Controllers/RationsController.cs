using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FoodFit.Data;
using FoodFit.Models;

namespace FoodFit.Controllers
{
    public class RationsController : Controller
    {
        private readonly FoodFitContext _context;

        public RationsController(FoodFitContext context)
        {
            _context = context;
        }

        // GET: Rations
        public async Task<IActionResult> Index()
        {
              return _context.Ration != null ? 
                          View(await _context.Ration.ToListAsync()) :
                          Problem("Entity set 'FoodFitContext.Ration'  is null.");
        }

        // GET: Rations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Ration == null)
            {
                return NotFound();
            }

            var ration = await _context.Ration
                .FirstOrDefaultAsync(m => m.ID == id);
            if (ration == null)
            {
                return NotFound();
            }

            return View(ration);
        }

        // GET: Rations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Rations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Day,Title")] Ration ration)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ration);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ration);
        }

        // GET: Rations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Ration == null)
            {
                return NotFound();
            }

            var ration = await _context.Ration.FindAsync(id);
            if (ration == null)
            {
                return NotFound();
            }
            return View(ration);
        }

        // POST: Rations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Day,Title")] Ration ration)
        {
            if (id != ration.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ration);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RationExists(ration.ID))
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
            return View(ration);
        }

        // GET: Rations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Ration == null)
            {
                return NotFound();
            }

            var ration = await _context.Ration
                .FirstOrDefaultAsync(m => m.ID == id);
            if (ration == null)
            {
                return NotFound();
            }

            return View(ration);
        }

        // POST: Rations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Ration == null)
            {
                return Problem("Entity set 'FoodFitContext.Ration'  is null.");
            }
            var ration = await _context.Ration.FindAsync(id);
            if (ration != null)
            {
                _context.Ration.Remove(ration);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RationExists(int id)
        {
          return (_context.Ration?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
