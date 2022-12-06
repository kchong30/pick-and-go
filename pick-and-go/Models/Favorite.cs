﻿using System;
using System.Collections.Generic;

namespace PickAndGo.Models
{
    public partial class Favorite
    {
        public int CustomerId { get; set; }
        public int OrderId { get; set; }
        public int LineId { get; set; }
        public string FavoriteName { get; set; } = null!;

        public virtual Customer Customer { get; set; } = null!;
        public virtual OrderHeader Order { get; set; } = null!;
        public virtual OrderLine OrderLine { get; set; } = null!;
    }
}
