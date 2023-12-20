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
    public class RationRecipesController : Controller
    {
        private readonly FoodFitContext _context;

        public RationRecipesController(FoodFitContext context)
        {
            _context = context;
        }

        // GET: RationRecipes
        public async Task<IActionResult> Index()
        {
            var foodFitContext = _context.RationRecipe.Include(r => r.Ration).Include(r => r.Recipe);
            return View(await foodFitContext.ToListAsync());
        }

        // GET: RationRecipes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.RationRecipe == null)
            {
                return NotFound();
            }

            var rationRecipe = await _context.RationRecipe
                .Include(r => r.Ration)
                .Include(r => r.Recipe)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (rationRecipe == null)
            {
                return NotFound();
            }

            return View(rationRecipe);
        }

        // GET: RationRecipes/Create
        public IActionResult Create()
        {
            ViewData["RationID"] = new SelectList(_context.Ration, "ID", "ID");
            ViewData["RecipeID"] = new SelectList(_context.Recipe, "ID", "ID");
            return View();
        }

        // POST: RationRecipes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,RationID,RecipeID")] RationRecipe rationRecipe)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rationRecipe);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RationID"] = new SelectList(_context.Ration, "ID", "ID", rationRecipe.RationID);
            ViewData["RecipeID"] = new SelectList(_context.Recipe, "ID", "ID", rationRecipe.RecipeID);
            return View(rationRecipe);
        }

        // GET: RationRecipes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.RationRecipe == null)
            {
                return NotFound();
            }

            var rationRecipe = await _context.RationRecipe.FindAsync(id);
            if (rationRecipe == null)
            {
                return NotFound();
            }
            ViewData["RationID"] = new SelectList(_context.Ration, "ID", "ID", rationRecipe.RationID);
            ViewData["RecipeID"] = new SelectList(_context.Recipe, "ID", "ID", rationRecipe.RecipeID);
            return View(rationRecipe);
        }

        // POST: RationRecipes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,RationID,RecipeID")] RationRecipe rationRecipe)
        {
            if (id != rationRecipe.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rationRecipe);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RationRecipeExists(rationRecipe.ID))
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
            ViewData["RationID"] = new SelectList(_context.Ration, "ID", "ID", rationRecipe.RationID);
            ViewData["RecipeID"] = new SelectList(_context.Recipe, "ID", "ID", rationRecipe.RecipeID);
            return View(rationRecipe);
        }

        // GET: RationRecipes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.RationRecipe == null)
            {
                return NotFound();
            }

            var rationRecipe = await _context.RationRecipe
                .Include(r => r.Ration)
                .Include(r => r.Recipe)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (rationRecipe == null)
            {
                return NotFound();
            }

            return View(rationRecipe);
        }

        // POST: RationRecipes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.RationRecipe == null)
            {
                return Problem("Entity set 'FoodFitContext.RationRecipe'  is null.");
            }
            var rationRecipe = await _context.RationRecipe.FindAsync(id);
            if (rationRecipe != null)
            {
                _context.RationRecipe.Remove(rationRecipe);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RationRecipeExists(int id)
        {
          return (_context.RationRecipe?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
