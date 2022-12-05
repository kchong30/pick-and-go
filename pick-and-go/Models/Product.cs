using System;
using System.Collections.Generic;

namespace pick_and_go.Models
{
    public partial class Product
    {
        public Product()
        {
            OrderLines = new HashSet<OrderLine>();
        }

        public int ProductId { get; set; }
        public string Description { get; set; } = null!;
        public decimal? BasePrice { get; set; }
        public byte[]? Image { get; set; }

        public virtual ICollection<OrderLine> OrderLines { get; set; }
    }
}
