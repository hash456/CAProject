using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CAProject.Models
{
    public class Review
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string UserName { get; set; }

        [MaxLength(1000)]
        public string MainReview { get; set; }

        [Required]
        public int Rating { get; set; }

        [Required]
        [MaxLength(30)]
        public string DateReviewed { get; set; }

        [Required]
        public int ProductId { get; set; } 

        public virtual Product Product { get; set; }
        public virtual User User { get; set; }


    }
}
