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
    public class TimeOfReceiptsController : Controller
    {
        private readonly FoodFitContext _context;

        public TimeOfReceiptsController(FoodFitContext context)
        {
            _context = context;
        }

        // GET: TimeOfReceipts
        public async Task<IActionResult> Index()
        {
              return _context.TimeOfReceipt != null ? 
                          View(await _context.TimeOfReceipt.ToListAsync()) :
                          Problem("Entity set 'FoodFitContext.TimeOfReceipt'  is null.");
        }

        // GET: TimeOfReceipts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TimeOfReceipt == null)
            {
                return NotFound();
            }

            var timeOfReceipt = await _context.TimeOfReceipt
                .FirstOrDefaultAsync(m => m.ID == id);
            if (timeOfReceipt == null)
            {
                return NotFound();
            }

            return View(timeOfReceipt);
        }

        // GET: TimeOfReceipts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TimeOfReceipts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Title")] TimeOfReceipt timeOfReceipt)
        {
            if (ModelState.IsValid)
            {
                _context.Add(timeOfReceipt);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(timeOfReceipt);
        }

        // GET: TimeOfReceipts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TimeOfReceipt == null)
            {
                return NotFound();
            }

            var timeOfReceipt = await _context.TimeOfReceipt.FindAsync(id);
            if (timeOfReceipt == null)
            {
                return NotFound();
            }
            return View(timeOfReceipt);
        }

        // POST: TimeOfReceipts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Title")] TimeOfReceipt timeOfReceipt)
        {
            if (id != timeOfReceipt.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(timeOfReceipt);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TimeOfReceiptExists(timeOfReceipt.ID))
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
            return View(timeOfReceipt);
        }

        // GET: TimeOfReceipts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TimeOfReceipt == null)
            {
                return NotFound();
            }

            var timeOfReceipt = await _context.TimeOfReceipt
                .FirstOrDefaultAsync(m => m.ID == id);
            if (timeOfReceipt == null)
            {
                return NotFound();
            }

            return View(timeOfReceipt);
        }

        // POST: TimeOfReceipts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TimeOfReceipt == null)
            {
                return Problem("Entity set 'FoodFitContext.TimeOfReceipt'  is null.");
            }
            var timeOfReceipt = await _context.TimeOfReceipt.FindAsync(id);
            if (timeOfReceipt != null)
            {
                _context.TimeOfReceipt.Remove(timeOfReceipt);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TimeOfReceiptExists(int id)
        {
          return (_context.TimeOfReceipt?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
