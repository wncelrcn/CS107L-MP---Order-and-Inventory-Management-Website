using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using CS107L_MP.App.Cart;

namespace CS107L_MP
{
    public partial class Cart : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCart();
            }
        }

        // Method to load cart items
        private void LoadCart()
        {
            // Get the currently logged-in username
            string username = Session["Username"] != null ? Session["Username"].ToString() : "";

            if (!string.IsNullOrEmpty(username))
            {
                // Get cart items for the specific user
                IEnumerable<MyCart> cartItems = GetCartItems(username);

                // Bind cart items to the repeater
                ItemRepeater.DataSource = cartItems;
                ItemRepeater.DataBind();

                // Calculate and display the total price of all items in the cart
                double totalCartPrice = cartItems.Sum(item => item.TotalPrice);
                lblTotalCartPrice.Text = $"Total Checkout Price: {totalCartPrice:C} ";
            }
            else
            {
                // Redirect to login page or handle accordingly if the user is not logged in
                Response.Redirect("~/Login.aspx");
            }
        }


        // Method to fetch cart items for a specific user from the database
        private IEnumerable<MyCart> GetCartItems(string username)
        {
            List<MyCart> cartItems = new List<MyCart>();

            // Connect to the database and retrieve cart items for the specific user
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            string query = "SELECT ShoppingCart.ProductID, Products.ProductName, ShoppingCart.Quantity, ShoppingCart.UnitPrice, ShoppingCart.TotalPrice, Products.Stock FROM ShoppingCart INNER JOIN Products ON ShoppingCart.ProductID = Products.ProductID WHERE Username = @Username";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Username", username);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                // Read data and create MyCart objects
                while (reader.Read())
                {
                    MyCart cartItem = new MyCart();
                    cartItem.ProductName = reader["ProductName"].ToString();
                    cartItem.ProductId = reader["ProductId"].ToString();
                    cartItem.Price = Convert.ToDouble(reader["UnitPrice"]);
                    cartItem.Quantity = Convert.ToInt32(reader["Quantity"]);
                    cartItem.TotalPrice = Convert.ToDouble(reader["TotalPrice"]);
                    cartItem.Stock = Convert.ToInt32(reader["Stock"]);
                    cartItems.Add(cartItem);
                }
            }

            return cartItems;
        }

        // Event handler for removing items from the cart
        protected void RemoveFromCart_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string productId = btn.CommandArgument; // Retrieve the product ID directly from CommandArgument

            // Get the currently logged-in username
            string username = Session["Username"] != null ? Session["Username"].ToString() : "";

            if (!string.IsNullOrEmpty(username))
            {
                // Remove the item from the cart for the specific user
                RemoveCartItem(username, productId);

                // Reload the cart to reflect the changes
                LoadCart();
            }
            else
            {
                // Redirect to login page or handle accordingly if user is not logged in
                Response.Redirect("~/Login.aspx");
            }
        }


        // Method to remove an item from the cart for a specific user
        private void RemoveCartItem(string username, string productId)
        {
            // Connect to the database and remove the item from the cart for the specific user
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            string query = "DELETE FROM ShoppingCart WHERE Username = @Username AND ProductId = @ProductId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@ProductId", productId);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        protected void minusButton_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string[] args = btn.CommandArgument.ToString().Split(';');
            string productId = args[0];
            int currentQuantity = int.Parse(args[1]);
            decimal totalPrice = decimal.Parse(args[2]);

            string username = Session["Username"] != null ? Session["Username"].ToString() : "";

            if (!string.IsNullOrEmpty(username))
            {
                // Implement logic to reduce the quantity in the database
                int newQuantity = Math.Max(1, currentQuantity - 1); // Ensure the quantity doesn't go below 1
                decimal newTotalPrice = totalPrice / currentQuantity * newQuantity;

                // Display information in alert
                //string alertMessage = $"Product ID: {productId}, New Quantity: {newQuantity}, newTotalPrice: {newTotalPrice}";
                //ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('{alertMessage}');", true);

                UpdateCartQuantity(productId, newQuantity, newTotalPrice, username);

                // Reload the cart to reflect the changes
                LoadCart();
            }
            else
            {
                // Redirect to login page or handle accordingly if user is not logged in
                Response.Redirect("~/Login.aspx");
            }
        }

        protected void plusButton_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string[] args = btn.CommandArgument.ToString().Split(';');
            string productId = args[0];
            int currentQuantity = int.Parse(args[1]);
            decimal totalPrice = decimal.Parse(args[2]);
            int stock = int.Parse(args[3]);

            string username = Session["Username"] != null ? Session["Username"].ToString() : "";

            if (!string.IsNullOrEmpty(username))
            {
                int newQuantity = Math.Min(currentQuantity + 1, stock); // Ensure the quantity doesn't exceed stock
                decimal newTotalPrice = totalPrice / currentQuantity * newQuantity;

                // Display information in alert
                //string alertMessage = $"Product ID: {productId}, New Quantity: {newQuantity}, newTotalPrice: {newTotalPrice}";
                //ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('{alertMessage}');", true);


                UpdateCartQuantity(productId, newQuantity, newTotalPrice, username);

                // Reload the cart to reflect the changes
                LoadCart();
            }
            else
            {
                // Redirect to login page or handle accordingly if user is not logged in
                Response.Redirect("~/Login.aspx");
            }
        }


        private void UpdateCartQuantity(string productId, int newQuantity, decimal newTotalPrice, string username)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string updateQuery = "UPDATE ShoppingCart SET Quantity = @Quantity, TotalPrice = @TotalPrice WHERE Username = @Username AND ProductId = @ProductId";

                using (SqlCommand command = new SqlCommand(updateQuery, connection))
                {
                    command.Parameters.AddWithValue("@Quantity", newQuantity);
                    command.Parameters.AddWithValue("@TotalPrice", newTotalPrice);
                    command.Parameters.AddWithValue("@ProductID", productId);
                    command.Parameters.AddWithValue("@Username", username);
                    command.ExecuteNonQuery();
                }
            }
        }


        protected void btnCheckout_Click(object sender, EventArgs e)
        {
            // Get the currently logged-in username
            string username = Session["Username"] != null ? Session["Username"].ToString() : "";

            if (!string.IsNullOrEmpty(username))
            {
                // Check if the cart is empty
                if (IsCartEmpty(username))
                {
                    // Display an alert indicating that the user doesn't have any items in the cart
                    ScriptManager.RegisterStartupScript(this, GetType(), "showAlert", "alert('You don\'t have any items in your cart!');", true);
                    return;
                }

                // Generate a unique TransactionID for the current checkout
                string transactionID = Guid.NewGuid().ToString();

                // Get cart items for the specific user
                IEnumerable<MyCart> cartItems = GetCartItems(username);

                // Establish connection
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Insert cart items into the Orders table
                    InsertOrderItems(cartItems, transactionID, username, connection);

                    // Remove cart items from the ShoppingCart table
                    ClearCart(username);
                }

                // Redirect to the order summary page
                Response.Redirect("~/MyOrders.aspx");
            }
            else
            {
                // Redirect to login page or handle accordingly if user is not logged in
                Response.Redirect("~/Login.aspx");
            }
        }

        // Method to check if the cart is empty
        private bool IsCartEmpty(string username)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            string query = "SELECT COUNT(*) FROM ShoppingCart WHERE Username = @Username";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Username", username);
                connection.Open();
                int cartItemCount = (int)command.ExecuteScalar();

                return cartItemCount == 0;
            }
        }

        // Method to insert cart items into the Orders table
        private void InsertOrderItems(IEnumerable<MyCart> cartItems, string transactionID, string username, SqlConnection connection)
        {
            foreach (var item in cartItems)
            {
                string insertQuery = @"INSERT INTO Orders (TransactionID, Username, ProductName, Quantity, TotalOrderPrice, OrderStatus)
                               VALUES (@TransactionID, @Username, @ProductName, @Quantity, @TotalOrderPrice, @OrderStatus)";

                using (SqlCommand command = new SqlCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@TransactionID", transactionID);
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@ProductName", item.ProductName);
                    command.Parameters.AddWithValue("@Quantity", item.Quantity);
                    command.Parameters.AddWithValue("@TotalOrderPrice", item.TotalPrice);
                    command.Parameters.AddWithValue("@OrderStatus", "Order Placed"); // You can set the initial order status here
                    command.ExecuteNonQuery();
                }

                // Update product stock after each successful order
                UpdateProductStock(connection, item.ProductId, item.Quantity);
            }
        }

        // Method to update product stock after successful order
        private void UpdateProductStock(SqlConnection connection, string productId, int quantity)
        {
            string updateStockQuery = "UPDATE Products SET Stock = Stock - @Quantity WHERE ProductID = @ProductID";
            using (SqlCommand updateStockCommand = new SqlCommand(updateStockQuery, connection))
            {
                updateStockCommand.Parameters.AddWithValue("@Quantity", quantity);
                updateStockCommand.Parameters.AddWithValue("@ProductID", productId);
                updateStockCommand.ExecuteNonQuery();
            }
        }


        // Method to remove cart items from the ShoppingCart table
        private void ClearCart(string username)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string deleteQuery = "DELETE FROM ShoppingCart WHERE Username = @Username";

                using (SqlCommand command = new SqlCommand(deleteQuery, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    command.ExecuteNonQuery();
                }
            }
        }

    }
}