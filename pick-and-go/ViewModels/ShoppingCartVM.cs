using PickAndGo.Models;

namespace PickAndGo.ViewModels
{
    public class ShoppingCartVM
    {
        public string productId { get; set; }
        public string description { get; set; }
        public List<ShoppingCartLineVM> ingredients { get; set; }
        public string subtotal { get; set; }
        public string productPrice { get; set; }


    }
}
