using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Smart_Shop.Models
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public decimal Quantity { get; set; }
        public string Photo { get; set; }
    }
}