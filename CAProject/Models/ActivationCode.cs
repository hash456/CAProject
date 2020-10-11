using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CAProject.Models
{
    public class ActivationCode
    {
        [Required]
        [MaxLength(19)]
        public string ActivationCodeId { get; set; }

        [Required]
        [DefaultValue(false)]
        public bool IsSold { get; set; }

        [DefaultValue(null)]
        public int? OrderId { get; set; }

        [Required]
        public int ProductId { get; set; }

        public virtual Product Product { get; set; }

        public virtual Order Order { get; set; }
    }
}
