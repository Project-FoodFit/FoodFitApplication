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
    public class RecipeProductsController : Controller
    {
        private readonly FoodFitContext _context;

        public RecipeProductsController(FoodFitContext context)
        {
            _context = context;
        }

        // GET: RecipeProducts
        public async Task<IActionResult> Index()
        {
            var foodFitContext = _context.RecipeProduct.Include(r => r.Product).Include(r => r.Recipe);
            return View(await foodFitContext.ToListAsync());
        }

        // GET: RecipeProducts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.RecipeProduct == null)
            {
                return NotFound();
            }

            var recipeProduct = await _context.RecipeProduct
                .Include(r => r.Product)
                .Include(r => r.Recipe)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (recipeProduct == null)
            {
                return NotFound();
            }

            return View(recipeProduct);
        }

        // GET: RecipeProducts/Create
        public IActionResult Create()
        {
            ViewData["ProductID"] = new SelectList(_context.Product, "ID", "ID");
            ViewData["RecipeID"] = new SelectList(_context.Recipe, "ID", "ID");
            return View();
        }

        // POST: RecipeProducts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,RecipeID,ProductID")] RecipeProduct recipeProduct)
        {
            if (ModelState.IsValid)
            {
                _context.Add(recipeProduct);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductID"] = new SelectList(_context.Product, "ID", "ID", recipeProduct.ProductID);
            ViewData["RecipeID"] = new SelectList(_context.Recipe, "ID", "ID", recipeProduct.RecipeID);
            return View(recipeProduct);
        }

        // GET: RecipeProducts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.RecipeProduct == null)
            {
                return NotFound();
            }

            var recipeProduct = await _context.RecipeProduct.FindAsync(id);
            if (recipeProduct == null)
            {
                return NotFound();
            }
            ViewData["ProductID"] = new SelectList(_context.Product, "ID", "ID", recipeProduct.ProductID);
            ViewData["RecipeID"] = new SelectList(_context.Recipe, "ID", "ID", recipeProduct.RecipeID);
            return View(recipeProduct);
        }

        // POST: RecipeProducts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,RecipeID,ProductID")] RecipeProduct recipeProduct)
        {
            if (id != recipeProduct.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(recipeProduct);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecipeProductExists(recipeProduct.ID))
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
            ViewData["ProductID"] = new SelectList(_context.Product, "ID", "ID", recipeProduct.ProductID);
            ViewData["RecipeID"] = new SelectList(_context.Recipe, "ID", "ID", recipeProduct.RecipeID);
            return View(recipeProduct);
        }

        // GET: RecipeProducts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.RecipeProduct == null)
            {
                return NotFound();
            }

            var recipeProduct = await _context.RecipeProduct
                .Include(r => r.Product)
                .Include(r => r.Recipe)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (recipeProduct == null)
            {
                return NotFound();
            }

            return View(recipeProduct);
        }

        // POST: RecipeProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.RecipeProduct == null)
            {
                return Problem("Entity set 'FoodFitContext.RecipeProduct'  is null.");
            }
            var recipeProduct = await _context.RecipeProduct.FindAsync(id);
            if (recipeProduct != null)
            {
                _context.RecipeProduct.Remove(recipeProduct);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RecipeProductExists(int id)
        {
          return (_context.RecipeProduct?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
