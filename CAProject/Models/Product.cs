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
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        [Required]
        [MaxLength(200)]
        public string Image { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public int StockCount { get; set; }

        [Required]
        [MaxLength(30)]
        public string Platform { get; set; }

        [Required]
        public int NumberSold { get; set; } //by right should be the sum of all sold

        [Required]
        public int NumberRatings { get; set; } //by right should not be here, shld be the sum of all ratings

        [Required]
        public double Ratings { get; set; } //0 to 5; by right should be in another table
    }
}
