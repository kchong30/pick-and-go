using Microsoft.AspNetCore.Mvc;

namespace pick_and_go.ViewModels
{
    public class OrderListVM
    {
        public int OrderId { get; set; }
        public string OrderStr { get; set; }
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string OrderDate { get; set; }
        public string PickupTime { get; set; }
        public int LineId { get; set; }
        public int ProductId { get; set; }
        public string Description { get; set; }
        public int? Quantity { get; set; }
        public decimal? Price { get; set; }
        public decimal OrderValue { get; set; }
        public string OrderStatus { get; set; }
        [BindProperty]
        public string SelectedStatus { get; set; }
        public List<OrderIngredientVM> Ingredients { get; set; }
        public string OrderFilter { get; set; }
    }
}
