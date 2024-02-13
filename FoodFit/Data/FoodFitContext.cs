using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FoodFit.Models;

namespace FoodFit.Data
{
    public class FoodFitContext : DbContext
    {
        public FoodFitContext (DbContextOptions<FoodFitContext> options)
            : base(options)
        {
        }

        public DbSet<FoodFit.Models.WorkoutType> WorkoutType { get; set; } = default!;

        public DbSet<FoodFit.Models.Workout>? Workout { get; set; }

        public DbSet<FoodFit.Models.Role>? Role { get; set; }

        public DbSet<FoodFit.Models.Users>? Users { get; set; }

        public DbSet<FoodFit.Models.TimeOfReceipt>? TimeOfReceipt { get; set; }

        public DbSet<FoodFit.Models.RecipeType>? RecipeType { get; set; }

        public DbSet<FoodFit.Models.Recipe>? Recipe { get; set; }

        public DbSet<FoodFit.Models.ProductType>? ProductType { get; set; }

        public DbSet<FoodFit.Models.Product>? Product { get; set; }

        public DbSet<FoodFit.Models.RecipeProduct>? RecipeProduct { get; set; }

        public DbSet<FoodFit.Models.Ration>? Ration { get; set; }

        public DbSet<FoodFit.Models.RationRecipe>? RationRecipe { get; set; }

        public DbSet<FoodFit.Models.RationWorkout>? RationWorkout { get; set; }
    }
}
