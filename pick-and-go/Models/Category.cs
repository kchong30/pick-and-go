using System;
using System.Collections.Generic;

namespace pick_and_go.Models
{
    public partial class Category
    {
        public Category()
        {
            Ingredients = new HashSet<Ingredient>();
        }

        public string CategoryId { get; set; } = null!;

        public virtual ICollection<Ingredient> Ingredients { get; set; }
    }
}
