using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CS107L_MP.App.Products
{
    // Define a class to represent a product
    public class Product
    {
        public string ProductId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Stock { get; set; }
        
    }
}