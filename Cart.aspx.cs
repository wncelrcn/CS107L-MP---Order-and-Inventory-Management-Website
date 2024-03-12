using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web.UI.WebControls;
using CS107L_MP.App.Cart;

namespace CS107L_MP
{
    public partial class Cart : System.Web.UI.Page
    {
        public object command { get; private set; }
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
                lblTotalCartPrice.Text = $"Total Cart Price: {totalCartPrice:C}";
            }
            else
            {
                // Redirect to login page or handle accordingly if the user is not logged in
                Response.Redirect("~/Login.aspx");
            }
        }

        // method for checkout
        protected void btnCheckout_Click(object sender, EventArgs e)
        {
            // Get the currently logged-in username
            string username = Session["Username"] != null ? Session["Username"].ToString() : "";

            if (!string.IsNullOrEmpty(username))
            {
                // Get cart items for the specific user
                IEnumerable<MyCart> cartItems = GetCartItems(username);

                // Insert items into the Orders table
                foreach (var cartItem in cartItems)
                {
                    InsertOrder(username, cartItem.ProductId, cartItem.ProductName, cartItem.Quantity, cartItem.Price, cartItem.TotalPrice);
                }

                // Clear the shopping cart after successful checkout
                ClearShoppingCart(username);

                // Redirect to MyOrders page
                Response.Redirect("~/MyOrders.aspx");
            }
            else
            {
                // Redirect to login page or handle accordingly if the user is not logged in
                Response.Redirect("~/Login.aspx");
            }
        }

        // Method to insert items into the Orders table
        private void InsertOrder(string username, string productId, string productName, int quantity, double unitPrice, double totalPrice)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            string orderQuery = "INSERT INTO Orders (Username, ProductName, Quantity, UnitPrice, TotalPrice) VALUES (@Username, @ProductName, @Quantity, @UnitPrice, @TotalPrice)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand orderCommand = new SqlCommand(orderQuery, connection);
                orderCommand.Parameters.AddWithValue("@Username", username);
                orderCommand.Parameters.AddWithValue("@ProductID", productId);
                orderCommand.Parameters.AddWithValue("@ProductName", productName);
                orderCommand.Parameters.AddWithValue("@Quantity", quantity);
                orderCommand.Parameters.AddWithValue("@UnitPrice", unitPrice);
                orderCommand.Parameters.AddWithValue("@TotalPrice", totalPrice);

                connection.Open();
                orderCommand.ExecuteNonQuery();

                // Update product stock
                UpdateProductStock(connection, productId, quantity);
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

        // Method to clear the shopping cart for a specific user
        private void ClearShoppingCart(string username)
        {
            // Implement logic to clear the shopping cart in the database
            // For example, you can delete all rows from the ShoppingCart table for the specific user

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            string query = "DELETE FROM ShoppingCart WHERE Username = @Username";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Username", username);

                connection.Open();
                command.ExecuteNonQuery();
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
    }
}
