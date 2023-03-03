using Microsoft.AspNetCore.Mvc;
using System.Drawing;

namespace PickAndGo.ViewModels
{
    public class OrderHistoryVM
    {
        public int OrderId { get; set; }
        public string OrderStr { get; set; }
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string OrderDate { get; set; }
        public int LineId { get; set; }
        public int ProductId { get; set; }
        public string Description { get; set; }
        public int? Quantity { get; set; }
        public decimal? Price { get; set; }
        public decimal OrderValue { get; set; }
        public decimal LineValue { get; set; }
        public Boolean IsFavorite { get; set; }
        public List<OrderIngredientVM> Ingredients { get; set; }
    }
}
