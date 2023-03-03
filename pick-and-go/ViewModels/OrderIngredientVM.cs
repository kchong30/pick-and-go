namespace PickAndGo.ViewModels
{
    public class OrderIngredientVM
    {
        public int IngredientId { get; set; }
        public string IngDescription { get; set; }
        public int? Quantity { get; set; }
        public decimal? Price { get; set; }
        public decimal? CurrentPrice { get; set; }
        public decimal? IngValue { get; set; }
    }
}
