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
    public class RecipeTypesController : Controller
    {
        private readonly FoodFitContext _context;

        public RecipeTypesController(FoodFitContext context)
        {
            _context = context;
        }

        // GET: RecipeTypes
        public async Task<IActionResult> Index()
        {
              return _context.RecipeType != null ? 
                          View(await _context.RecipeType.ToListAsync()) :
                          Problem("Entity set 'FoodFitContext.RecipeType'  is null.");
        }

        // GET: RecipeTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.RecipeType == null)
            {
                return NotFound();
            }

            var recipeType = await _context.RecipeType
                .FirstOrDefaultAsync(m => m.ID == id);
            if (recipeType == null)
            {
                return NotFound();
            }

            return View(recipeType);
        }

        // GET: RecipeTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RecipeTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Title")] RecipeType recipeType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(recipeType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(recipeType);
        }

        // GET: RecipeTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.RecipeType == null)
            {
                return NotFound();
            }

            var recipeType = await _context.RecipeType.FindAsync(id);
            if (recipeType == null)
            {
                return NotFound();
            }
            return View(recipeType);
        }

        // POST: RecipeTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Title")] RecipeType recipeType)
        {
            if (id != recipeType.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(recipeType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecipeTypeExists(recipeType.ID))
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
            return View(recipeType);
        }

        // GET: RecipeTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.RecipeType == null)
            {
                return NotFound();
            }

            var recipeType = await _context.RecipeType
                .FirstOrDefaultAsync(m => m.ID == id);
            if (recipeType == null)
            {
                return NotFound();
            }

            return View(recipeType);
        }

        // POST: RecipeTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.RecipeType == null)
            {
                return Problem("Entity set 'FoodFitContext.RecipeType'  is null.");
            }
            var recipeType = await _context.RecipeType.FindAsync(id);
            if (recipeType != null)
            {
                _context.RecipeType.Remove(recipeType);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RecipeTypeExists(int id)
        {
          return (_context.RecipeType?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
