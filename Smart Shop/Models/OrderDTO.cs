using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Smart_Shop.Models
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public System.DateTime Date { get; set; }
        public decimal TotalAmount { get; set; }
        public int DtlID { get; set; }
    }
}