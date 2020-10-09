using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CAProject.Models
{
    public class Cart
    {
        public int id { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public string ProductId { get; set; }

        public virtual Product Product { get; set; }

        /*[Required]
        public User UerId { get; set; }
        public virtual User User { get; set; }*/
    }
}
