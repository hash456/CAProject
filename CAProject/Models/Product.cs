using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CAProject.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(35)]
        public string Name { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        [Required]
        [MaxLength(128)]
        public string Image { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public int StockCount { get; set; }
    }
}
