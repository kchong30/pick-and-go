using System;
using System.Collections.Generic;

namespace PickAndGo.Models
{
    public partial class Ingredient
    {
        public Ingredient()
        {
            LineIngredients = new HashSet<LineIngredient>();
        }

        public int IngredientId { get; set; }
        public string Description { get; set; } = null!;
        public decimal? Price { get; set; }
        public string CategoryId { get; set; } = null!;
        public string? InStock { get; set; }
        public string? Image { get; set; }

        public virtual Category Category { get; set; } = null!;
        public virtual ICollection<LineIngredient> LineIngredients { get; set; }
    }
}
