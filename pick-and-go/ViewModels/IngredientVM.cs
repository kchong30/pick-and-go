using Microsoft.EntityFrameworkCore;
using PickAndGo.Models;

namespace PickAndGo.ViewModels
{
    [Keyless]
    public class IngredientVM
    {

        public int IngredientId { get; set; }
        public string Description { get; set; }
        public decimal? Price { get; set; }
        public string CategoryId { get; set; }
        public string? InStock { get; set; }
        public Boolean OutstandingOrders {get; set; }
        public string ?InStockIcon { get; set; }
        public bool IngredientInStock { get; set; }

    }
}
