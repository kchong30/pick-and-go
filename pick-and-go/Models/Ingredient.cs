using System;
using System.Collections.Generic;

namespace pick_and_go.Models
{
    public partial class Ingredient
    {
        public Ingredient()
        {
            LineIngredients = new HashSet<LineIngredient>();
            Dietaries = new HashSet<DietaryType>();
        }

        public int IngredientId { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public string? CategoryId { get; set; }
        public string? InStock { get; set; }

        public virtual Category? Category { get; set; }
        public virtual ICollection<LineIngredient> LineIngredients { get; set; }

        public virtual ICollection<DietaryType> Dietaries { get; set; }
    }
}
