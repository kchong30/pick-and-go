using Microsoft.EntityFrameworkCore;
using PickAndGo.Models;

namespace PickAndGo.ViewModels
{
    [Keyless]
    public class IngredientOVVM
    {

        public int IngredientId { get; set; }
        public string Description { get; set; }
        public string CategoryId { get; set; }
        public int? SalesQty { get; set; }

    }
}
