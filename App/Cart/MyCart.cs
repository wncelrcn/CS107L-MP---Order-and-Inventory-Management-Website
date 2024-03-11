using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CS107L_MP.App.Cart
{
    public class MyCart
    {

        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public double TotalPrice { get; set; }
    }
}
