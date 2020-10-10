using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CAProject.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public string OrderDate { get; set; }

        public string CheckOutDate { get; set; }

        [Required]
        [DefaultValue(false)]
        public bool IsPaid { get; set; }

        public virtual User User { get; set; }
    }
}
