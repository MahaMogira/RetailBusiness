using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailBusiness.Core.Entities
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
