namespace FoodFit.Models
{
    public class RecipeProduct
    {
        public int ID { get; set; }
        public int RecipeID { get; set; }
        public int ProductID { get; set; }
        public Recipe Recipe { get; set; }
        public Product Product { get; set; }
    }
}
