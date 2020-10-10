using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;

namespace CAProject.Models
{
    public class CartInput
    {
        public int ProductId { get; set; }

        public int Quantity { get; set; }
    }
}
