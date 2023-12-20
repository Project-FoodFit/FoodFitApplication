namespace FoodFit.Models
{
    public class Product
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public int ProductTypeID { get; set; }
        public string? Image { get; set; }
        public ProductType ProductType { get; set; }
    }
}
