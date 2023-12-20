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
    public class RationWorkoutsController : Controller
    {
        private readonly FoodFitContext _context;

        public RationWorkoutsController(FoodFitContext context)
        {
            _context = context;
        }

        // GET: RationWorkouts
        public async Task<IActionResult> Index()
        {
            var foodFitContext = _context.RationWorkout.Include(r => r.Ration).Include(r => r.Workout);
            return View(await foodFitContext.ToListAsync());
        }

        // GET: RationWorkouts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.RationWorkout == null)
            {
                return NotFound();
            }

            var rationWorkout = await _context.RationWorkout
                .Include(r => r.Ration)
                .Include(r => r.Workout)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (rationWorkout == null)
            {
                return NotFound();
            }

            return View(rationWorkout);
        }

        // GET: RationWorkouts/Create
        public IActionResult Create()
        {
            ViewData["RationID"] = new SelectList(_context.Ration, "ID", "ID");
            ViewData["WorkoutID"] = new SelectList(_context.Workout, "ID", "ID");
            return View();
        }

        // POST: RationWorkouts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,RationID,WorkoutID")] RationWorkout rationWorkout)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rationWorkout);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RationID"] = new SelectList(_context.Ration, "ID", "ID", rationWorkout.RationID);
            ViewData["WorkoutID"] = new SelectList(_context.Workout, "ID", "ID", rationWorkout.WorkoutID);
            return View(rationWorkout);
        }

        // GET: RationWorkouts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.RationWorkout == null)
            {
                return NotFound();
            }

            var rationWorkout = await _context.RationWorkout.FindAsync(id);
            if (rationWorkout == null)
            {
                return NotFound();
            }
            ViewData["RationID"] = new SelectList(_context.Ration, "ID", "ID", rationWorkout.RationID);
            ViewData["WorkoutID"] = new SelectList(_context.Workout, "ID", "ID", rationWorkout.WorkoutID);
            return View(rationWorkout);
        }

        // POST: RationWorkouts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,RationID,WorkoutID")] RationWorkout rationWorkout)
        {
            if (id != rationWorkout.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rationWorkout);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RationWorkoutExists(rationWorkout.ID))
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
            ViewData["RationID"] = new SelectList(_context.Ration, "ID", "ID", rationWorkout.RationID);
            ViewData["WorkoutID"] = new SelectList(_context.Workout, "ID", "ID", rationWorkout.WorkoutID);
            return View(rationWorkout);
        }

        // GET: RationWorkouts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.RationWorkout == null)
            {
                return NotFound();
            }

            var rationWorkout = await _context.RationWorkout
                .Include(r => r.Ration)
                .Include(r => r.Workout)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (rationWorkout == null)
            {
                return NotFound();
            }

            return View(rationWorkout);
        }

        // POST: RationWorkouts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.RationWorkout == null)
            {
                return Problem("Entity set 'FoodFitContext.RationWorkout'  is null.");
            }
            var rationWorkout = await _context.RationWorkout.FindAsync(id);
            if (rationWorkout != null)
            {
                _context.RationWorkout.Remove(rationWorkout);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RationWorkoutExists(int id)
        {
          return (_context.RationWorkout?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
