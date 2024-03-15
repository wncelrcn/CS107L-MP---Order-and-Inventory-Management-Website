using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI.WebControls;
using CS107L_MP.App.Orders;

namespace CS107L_MP
{
    public partial class MyOrders : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadOrders();
            }
        }

        private void LoadOrders()
        {
            // Get the currently logged-in username
            string username = Session["Username"] != null ? Session["Username"].ToString() : "";

            if (!string.IsNullOrEmpty(username))
            {
                // Get orders for the specific user
                IEnumerable<MyOrderItem> orderItems = GetOrderItems(username);

                // Bind order items to the repeater
                OrderRepeater.DataSource = orderItems;
                OrderRepeater.DataBind();
            }
            else
            {
                // Redirect to login page or handle accordingly if the user is not logged in
                Response.Redirect("~/Login.aspx");
            }
        }

        private IEnumerable<MyOrderItem> GetOrderItems(string username)
        {
            List<MyOrderItem> orderItems = new List<MyOrderItem>();

            // Connect to the database and retrieve order items for the specific user
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            string query = "SELECT TransactionID, ProductName, Quantity, TotalOrderPrice, OrderDate, OrderStatus FROM Orders WHERE Username = @Username ORDER BY OrderDate DESC"; // Order by OrderDate in descending order
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Username", username);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                // Read data and create MyOrderItem objects
                while (reader.Read())
                {
                    string transactionID = reader["TransactionID"].ToString();

                    // Check if the order item already exists in the list
                    MyOrderItem orderItem = orderItems.FirstOrDefault(item => item.TransactionID == transactionID);

                    // If the order item doesn't exist, create a new one and add it to the list
                    if (orderItem == null)
                    {
                        orderItem = new MyOrderItem();
                        orderItem.TransactionID = transactionID;
                        orderItem.OrderDate = Convert.ToDateTime(reader["OrderDate"]);
                        orderItem.Status = reader["OrderStatus"].ToString();
                        orderItems.Add(orderItem);
                    }

                    // Add the product to the order item
                    OrderProduct product = new OrderProduct();
                    product.ProductName = reader["ProductName"].ToString();
                    product.Quantity = Convert.ToInt32(reader["Quantity"]);
                    orderItem.Products.Add(product);

                    // Accumulate the total price for the order item
                    orderItem.TotalPrice += Convert.ToDouble(reader["TotalOrderPrice"]);
                }
            }

            return orderItems;
        }



        protected void OrderRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                // Find the inner repeater control
                Repeater ProductRepeater = (Repeater)e.Item.FindControl("ProductRepeater");

                // Bind product details to the inner repeater
                MyOrderItem orderItem = (MyOrderItem)e.Item.DataItem;
                ProductRepeater.DataSource = orderItem.Products;
                ProductRepeater.DataBind();
            }
        }
    }
}

