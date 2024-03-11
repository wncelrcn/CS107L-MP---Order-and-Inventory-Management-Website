using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using CS107L_MP.App.Products;

namespace CS107L_MP
{
    public partial class Order : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string selectedCategory = CategoryDropDown.SelectedValue;
                var products = AllProducts(selectedCategory);

                ProductRepeater.DataSource = products;
                ProductRepeater.DataBind();

            }
            SetMaxQuantityValues();

        }

        // Method to set the maximum value for the quantity textboxes based on available stock
        private void SetMaxQuantityValues()
        {
            foreach (RepeaterItem item in ProductRepeater.Items)
            {
                TextBox quantityTextBox = (TextBox)item.FindControl("quantityNo");
                Label stockLabel = (Label)item.FindControl("stockLabel");

                int maxQuantity = int.Parse(stockLabel.Text); // Set the maximum value based on the available stock
                quantityTextBox.Attributes["max"] = maxQuantity.ToString();
            }
        }

        // Event handler for category selection change
        protected void CategoryDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedCategory = CategoryDropDown.SelectedValue;
            var products = AllProducts(selectedCategory);

            // Bind the repeater to the list of products
            ProductRepeater.DataSource = products;
            ProductRepeater.DataBind();


            SetMaxQuantityValues();

        }

        // Method to retrieve products from the database based on category
        public IEnumerable<Product> AllProducts(string category)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            string query = "SELECT ProductID, ProductName, Price, Stock FROM Products";
            if (!string.IsNullOrEmpty(category))
            {
                query += " WHERE Category = @Category";
            }

            // List to store products
            List<Product> products = new List<Product>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                if (!string.IsNullOrEmpty(category))
                {
                    command.Parameters.AddWithValue("@Category", category);
                }
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                // Read data and create Product objects
                while (reader.Read())
                {
                    Product product = new Product();
                    product.ProductId = reader["ProductId"].ToString();
                    product.Name = reader["ProductName"].ToString();
                    product.Price = Convert.ToDouble(reader["Price"]);
                    product.Stock = Convert.ToInt32(reader["Stock"]);
                    products.Add(product);
                }
            }

            return products;
        }

        protected void AddToCart_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            RepeaterItem item = (RepeaterItem)btn.NamingContainer;
            TextBox quantityTextBox = (TextBox)item.FindControl("quantityNo");

            // Check if the quantity textbox is empty
            if (string.IsNullOrEmpty(quantityTextBox.Text))
            {
                // Display an alert indicating that the quantity cannot be empty
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Please enter a quantity.');", true);
                return; // Exit the method if the quantity is empty
            }

            // Extract product information and quantity from CommandArgument
            string[] args = btn.CommandArgument.Split(';');
            string productId = args[0];
            string productName = args[1];
            double price = double.Parse(args[2]);
            int requestedQuantity = int.Parse(((TextBox)item.FindControl("quantityNo")).Text);

            // Check if the requested quantity exceeds the available stock
            int availableStock = GetAvailableStock(productId);
            int cartQuantity = GetCartQuantity(productId);

            if (requestedQuantity + cartQuantity > availableStock)
            {
                // Display an alert indicating insufficient stock
                ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('Insufficient stock. Available stock: {availableStock}.');", true);
                return; // Exit the method if the quantity exceeds stock
            }

            // Check if the product is already in the cart for the user
            if (IsProductInCart(productId))
            {
                // Update the existing record
                UpdateCart(productId, cartQuantity + requestedQuantity, price);
            }
            else
            {
                // Insert a new record
                InsertIntoCart(productId, productName, requestedQuantity, price);
            }

            // Display information in alert
            string alertMessage = $"Product ID: {productId}, Product Name: {productName}, Username: {Session["Username"]}, Quantity: {requestedQuantity}, Total Price: {price * requestedQuantity:C}";
            ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('{alertMessage}');", true);

            // Reset the quantity to 1
            quantityTextBox.Text = "1";
        }

        private int GetCartQuantity(string productId)
        {
            int cartQuantity = 0;
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string selectQuery = "SELECT Quantity FROM ShoppingCart WHERE ProductID = @ProductID AND Username = @Username";

                using (SqlCommand command = new SqlCommand(selectQuery, connection))
                {
                    command.Parameters.AddWithValue("@ProductID", productId);
                    command.Parameters.AddWithValue("@Username", Session["Username"]);

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        cartQuantity = (int)reader["Quantity"];
                    }
                }
            }

            return cartQuantity;
        }


        private int GetAvailableStock(string productId)
        {
            int availableStock = 0;
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string selectQuery = "SELECT Stock FROM Products WHERE ProductID = @ProductID";

                using (SqlCommand command = new SqlCommand(selectQuery, connection))
                {
                    command.Parameters.AddWithValue("@ProductID", productId);

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        availableStock = (int)reader["Stock"];
                    }
                }
            }

            return availableStock;
        }

        private bool IsProductInCart(string productId)
        {
            bool isInCart = false;
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string selectQuery = "SELECT COUNT(*) FROM ShoppingCart WHERE ProductID = @ProductID AND Username = @Username";

                using (SqlCommand command = new SqlCommand(selectQuery, connection))
                {
                    command.Parameters.AddWithValue("@ProductID", productId);
                    command.Parameters.AddWithValue("@Username", Session["Username"]);

                    int itemCount = (int)command.ExecuteScalar();

                    if (itemCount > 0)
                    {
                        isInCart = true;
                    }
                }
            }

            return isInCart;
        }

        private void UpdateCart(string productId, int newQuantity, double price)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string updateQuery = "UPDATE ShoppingCart SET Quantity = @Quantity, TotalPrice = @TotalPrice WHERE ProductID = @ProductID AND Username = @Username";

                using (SqlCommand command = new SqlCommand(updateQuery, connection))
                {
                    command.Parameters.AddWithValue("@Quantity", newQuantity);
                    command.Parameters.AddWithValue("@TotalPrice", price * newQuantity);
                    command.Parameters.AddWithValue("@ProductID", productId);
                    command.Parameters.AddWithValue("@Username", Session["Username"]);

                    command.ExecuteNonQuery();
                }
            }
        }

        private void InsertIntoCart(string productId, string productName, int quantity, double price)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string insertQuery = "INSERT INTO ShoppingCart (ProductID, ProductName, Username, Quantity, TotalPrice) VALUES (@ProductID, @ProductName, @Username, @Quantity, @TotalPrice)";

                using (SqlCommand command = new SqlCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@ProductID", productId);
                    command.Parameters.AddWithValue("@ProductName", productName);
                    command.Parameters.AddWithValue("@Username", Session["Username"]);
                    command.Parameters.AddWithValue("@Quantity", quantity);
                    command.Parameters.AddWithValue("@TotalPrice", price * quantity);

                    command.ExecuteNonQuery();
                }
            }
        }


    }

}