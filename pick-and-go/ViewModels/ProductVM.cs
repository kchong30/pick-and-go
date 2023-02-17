using System;
using System.Collections.Generic;

namespace PickAndGo.ViewModels
{
    public class ProductVM
    {
        public int ProductId { get; set; }
        public string Description { get; set; } = null!;
        public decimal? BasePrice { get; set; }
        public string? Image { get; set; }
        public int SelectedProductId { get; set; }
    }
}
