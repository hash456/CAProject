﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CAProject.Models
{
    public class Cart
    {
        [Required]
        public int Quantity { get; set; }

        [Required]
        public int ProductId { get; set; }

        
        public int OrderId { get; set; }

        public virtual Order Order { get; set; }

        public virtual Product Product { get; set; }

    }
}
