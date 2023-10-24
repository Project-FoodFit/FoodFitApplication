namespace FoodFit.Models
{
    public class Product
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int ProductTypeID { get; set; }
        public double Calories { get; set; }
        public double Proteins { get; set; }
        public double Fats { get; set; }
        public double Carbonhydrates { get; set; }
        public string? Image { get; set; }
        public ProductType ProductType { get; set; }
    }
}
