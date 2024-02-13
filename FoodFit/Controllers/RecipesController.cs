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
    public class RecipesController : Controller
    {
        private readonly FoodFitContext _context;

        public RecipesController(FoodFitContext context)
        {
            _context = context;
        }

        // GET: Recipes
        public async Task<IActionResult> Index(IFormCollection form)
        {
            if (form.Count == 0)
            {
                var foodFitContext = _context.Recipe.Include(r => r.RecipeType).Include(r => r.TimeOfReceipt);
                return View(await foodFitContext.ToListAsync());
            }
            string textFieldValue = form["searchBox"];
            var foodFitContextSearch = _context.Recipe.Include(r => r.RecipeType).Where(r => r.Title.Contains(textFieldValue));
            return View(await foodFitContextSearch.ToListAsync());
        }
        [HttpGet]
        public ActionResult SearchSuggestions(string query)
        {
            var suggestions = _context.Recipe.Include(r => r.RecipeType).Where(r => r.Title.Contains(query)).Select(r => r.Title).ToList();
            return PartialView("_SuggestionsList", suggestions);
        }

        // GET: Recipes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Recipe == null)
            {
                return NotFound();
            }

            var recipe = await _context.Recipe
                .Include(r => r.RecipeType)
                .Include(r => r.TimeOfReceipt)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (recipe == null)
            {
                return NotFound();
            }

            return View(recipe);
        }
        [HttpPost]
        public ActionResult NameToId(string name)
        {
            int id = _context.Recipe.FirstOrDefault(r => r.Title == name).ID;
            return Json(id);
        }

        // GET: Recipes/Create
        public IActionResult Create()
        {
            ViewData["RecipeTypeID"] = new SelectList(_context.RecipeType, "ID", "ID");
            ViewData["TimeOfReceiptID"] = new SelectList(_context.TimeOfReceipt, "ID", "ID");
            return View();
        }

        // POST: Recipes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Title,Description,Calories,Proteins,Fats,Carbonhydrates,RecipeTypeID,TimeOfReceiptID,Image")] Recipe recipe)
        {
            if (ModelState.IsValid)
            {
                _context.Add(recipe);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RecipeTypeID"] = new SelectList(_context.RecipeType, "ID", "ID", recipe.RecipeTypeID);
            ViewData["TimeOfReceiptID"] = new SelectList(_context.TimeOfReceipt, "ID", "ID", recipe.TimeOfReceiptID);
            return View(recipe);
        }

        // GET: Recipes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Recipe == null)
            {
                return NotFound();
            }

            var recipe = await _context.Recipe.FindAsync(id);
            if (recipe == null)
            {
                return NotFound();
            }
            ViewData["RecipeTypeID"] = new SelectList(_context.RecipeType, "ID", "ID", recipe.RecipeTypeID);
            ViewData["TimeOfReceiptID"] = new SelectList(_context.TimeOfReceipt, "ID", "ID", recipe.TimeOfReceiptID);
            return View(recipe);
        }

        // POST: Recipes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Title,Description,Calories,Proteins,Fats,Carbonhydrates,RecipeTypeID,TimeOfReceiptID,Image")] Recipe recipe)
        {
            if (id != recipe.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(recipe);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecipeExists(recipe.ID))
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
            ViewData["RecipeTypeID"] = new SelectList(_context.RecipeType, "ID", "ID", recipe.RecipeTypeID);
            ViewData["TimeOfReceiptID"] = new SelectList(_context.TimeOfReceipt, "ID", "ID", recipe.TimeOfReceiptID);
            return View(recipe);
        }

        // GET: Recipes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Recipe == null)
            {
                return NotFound();
            }

            var recipe = await _context.Recipe
                .Include(r => r.RecipeType)
                .Include(r => r.TimeOfReceipt)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (recipe == null)
            {
                return NotFound();
            }

            return View(recipe);
        }

        // POST: Recipes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Recipe == null)
            {
                return Problem("Entity set 'FoodFitContext.Recipe'  is null.");
            }
            var recipe = await _context.Recipe.FindAsync(id);
            if (recipe != null)
            {
                _context.Recipe.Remove(recipe);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RecipeExists(int id)
        {
          return (_context.Recipe?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
