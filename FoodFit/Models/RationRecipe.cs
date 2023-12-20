namespace FoodFit.Models
{
    public class RationRecipe
    {
        public int ID { get; set; }
        public int RationID { get; set; }
        public int RecipeID { get; set; }
        public Ration Ration { get; set; }
        public Recipe Recipe { get; set; }
    }
}
