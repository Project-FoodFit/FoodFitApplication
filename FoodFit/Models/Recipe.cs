namespace FoodFit.Models
{
    public class Recipe
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public double Calories { get; set; }
        public double Proteins { get; set; }
        public double Fats { get; set; }
        public double Carbonhydrates { get; set; }
        public int RecipeTypeID { get; set; }
        public int TimeOfReceiptID { get; set; }
        public RecipeType RecipeType { get; set; }
        public TimeOfReceipt TimeOfReceipt { get; set; }
    }
}
