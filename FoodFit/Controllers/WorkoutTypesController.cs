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
    public class WorkoutTypesController : Controller
    {
        private readonly FoodFitContext _context;

        public WorkoutTypesController(FoodFitContext context)
        {
            _context = context;
        }

        // GET: WorkoutTypes
        public async Task<IActionResult> Index()
        {
              return _context.WorkoutType != null ? 
                          View(await _context.WorkoutType.ToListAsync()) :
                          Problem("Entity set 'FoodFitContext.WorkoutType'  is null.");
        }

        // GET: WorkoutTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.WorkoutType == null)
            {
                return NotFound();
            }

            var workoutType = await _context.WorkoutType
                .FirstOrDefaultAsync(m => m.ID == id);
            if (workoutType == null)
            {
                return NotFound();
            }

            return View(workoutType);
        }

        // GET: WorkoutTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: WorkoutTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Title")] WorkoutType workoutType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(workoutType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(workoutType);
        }

        // GET: WorkoutTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.WorkoutType == null)
            {
                return NotFound();
            }

            var workoutType = await _context.WorkoutType.FindAsync(id);
            if (workoutType == null)
            {
                return NotFound();
            }
            return View(workoutType);
        }

        // POST: WorkoutTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Title")] WorkoutType workoutType)
        {
            if (id != workoutType.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(workoutType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WorkoutTypeExists(workoutType.ID))
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
            return View(workoutType);
        }

        // GET: WorkoutTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.WorkoutType == null)
            {
                return NotFound();
            }

            var workoutType = await _context.WorkoutType
                .FirstOrDefaultAsync(m => m.ID == id);
            if (workoutType == null)
            {
                return NotFound();
            }

            return View(workoutType);
        }

        // POST: WorkoutTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.WorkoutType == null)
            {
                return Problem("Entity set 'FoodFitContext.WorkoutType'  is null.");
            }
            var workoutType = await _context.WorkoutType.FindAsync(id);
            if (workoutType != null)
            {
                _context.WorkoutType.Remove(workoutType);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WorkoutTypeExists(int id)
        {
          return (_context.WorkoutType?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
