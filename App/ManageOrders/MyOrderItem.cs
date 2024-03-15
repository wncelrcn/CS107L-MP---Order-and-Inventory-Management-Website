using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CS107L_MP.App.ManageOrders
{
    public class MyOrderItem
    {
        public string TransactionID { get; set; }
        public string Username { get; set; }
        public List<OrderProduct> Products { get; set; }
        public double TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }

        public MyOrderItem()
        {
            Products = new List<OrderProduct>();
        }
    }

    public class OrderProduct
    {
        public string ProductName { get; set; }
        public int Quantity { get; set; }
    }
}