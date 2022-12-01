using System;
using System.Collections.Generic;

namespace pick_and_go.Models
{
    public partial class DietaryType
    {
        public DietaryType()
        {
            Customers = new HashSet<Customer>();
            Ingredients = new HashSet<Ingredient>();
        }

        public string DietaryId { get; set; } = null!;
        public string? DietaryName { get; set; }
        public byte[]? DietaryImage { get; set; }

        public virtual ICollection<Customer> Customers { get; set; }
        public virtual ICollection<Ingredient> Ingredients { get; set; }
    }
}
