using System;
using System.Collections.Generic;

namespace pick_and_go.Models
{
    public partial class OrderLine
    {
        public OrderLine()
        {
            Favorites = new HashSet<Favorite>();
            LineIngredients = new HashSet<LineIngredient>();
        }

        public int OrderId { get; set; }
        public int LineId { get; set; }
        public int ProductId { get; set; }
        public int? Quantity { get; set; }

        public virtual OrderHeader Order { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;
        public virtual ICollection<Favorite> Favorites { get; set; }
        public virtual ICollection<LineIngredient> LineIngredients { get; set; }
    }
}
