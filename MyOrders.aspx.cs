using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

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
            string query = "SELECT ProductName, Quantity, UnitPrice, TotalPrice, OrderDate, Status FROM Orders WHERE Username = @Username";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Username", username);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                // Read data and create MyOrderItem objects
                while (reader.Read())
                {
                    MyOrderItem orderItem = new MyOrderItem();
                    orderItem.ProductName = reader["ProductName"].ToString();
                    orderItem.Quantity = Convert.ToInt32(reader["Quantity"]);
                    orderItem.Price = Convert.ToDouble(reader["UnitPrice"]);
                    orderItem.TotalPrice = Convert.ToDouble(reader["TotalPrice"]);
                    orderItem.OrderDate = Convert.ToDateTime(reader["OrderDate"]); // Add this line           
                    orderItems.Add(orderItem);
                }
            }

            return orderItems;
        }

        public class MyOrderItem
        {
            public string ProductName { get; set; }
            public int Quantity { get; set; }
            public double Price { get; set; }
            public double TotalPrice { get; set; }
            public DateTime OrderDate { get; internal set; }
            public int ProductID { get; set; }
        }
    }
}
