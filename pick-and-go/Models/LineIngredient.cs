using System;
using System.Collections.Generic;

namespace PickAndGo.Models
{
    public partial class LineIngredient
    {
        public int OrderId { get; set; }
        public int LineId { get; set; }
        public int IngredientId { get; set; }
        public int? Quantity { get; set; }

        public virtual Ingredient Ingredient { get; set; } = null!;
        public virtual OrderHeader Order { get; set; } = null!;
        public virtual OrderLine OrderLine { get; set; } = null!;
    }
}
