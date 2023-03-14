using PickAndGo.Models;

namespace PickAndGo.ViewModels
{
    public class OverviewVM
    {
        public int Outstanding { get; set; }
        public int Completed { get; set; }
        public decimal OutstandingVal { get; set; }
        public decimal CompletedVal { get; set; }
        public string ViewDate { get; set; }
        public int Accounts { get; set; }
        public int Guests { get; set; }
        public List<IngredientVM> Ingredients { get; set; }
    }
}
