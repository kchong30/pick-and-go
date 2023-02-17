using System;
using System.Collections.Generic;

namespace PickAndGo.Models
{
    public partial class DietaryType
    {
        public DietaryType()
        {
            Customers = new HashSet<Customer>();
            Ingredients = new HashSet<Ingredient>();
        }

        public string DietaryId { get; set; } = null!;
        public string DietaryName { get; set; } = null!;
        public string? DietaryImage { get; set; }

        public virtual ICollection<Customer> Customers { get; set; }
        public virtual ICollection<Ingredient> Ingredients { get; set; }
    }
}
