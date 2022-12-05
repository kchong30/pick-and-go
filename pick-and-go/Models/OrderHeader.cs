﻿using System;
using System.Collections.Generic;

namespace pick_and_go.Models
{
    public partial class OrderHeader
    {
        public OrderHeader()
        {
            Favorites = new HashSet<Favorite>();
            LineIngredients = new HashSet<LineIngredient>();
            OrderLines = new HashSet<OrderLine>();
        }

        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public DateTime? OrderDate { get; set; }
        public decimal? OrderValue { get; set; }
        public TimeSpan? PickupTime { get; set; }
        public string? OrderStatus { get; set; }

        public virtual Customer Customer { get; set; } = null!;
        public virtual ICollection<Favorite> Favorites { get; set; }
        public virtual ICollection<LineIngredient> LineIngredients { get; set; }
        public virtual ICollection<OrderLine> OrderLines { get; set; }
    }
}
