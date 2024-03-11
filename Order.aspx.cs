﻿using System;
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
            int quantity = int.Parse(((TextBox)item.FindControl("quantityNo")).Text);

            // Calculate total price
            double totalPrice = price * quantity;

            // Display information in alert
            string alertMessage = $"Username: {Session["Username"]}, Product ID: {productId}, Product Name: {productName}, Product Price: {price:C}, Quantity: {quantity}, Total Price: {totalPrice:C}";
            ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('{alertMessage}');", true);

            // Reset the quantity to 1
            quantityTextBox.Text = "1";

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Check if the product is already in the cart for the user
                string selectQuery = "SELECT COUNT(*) FROM ShoppingCart WHERE ProductID = @ProductID AND Username = @Username";
                using (SqlCommand selectCommand = new SqlCommand(selectQuery, connection))
                {
                    selectCommand.Parameters.AddWithValue("@ProductID", productId);
                    selectCommand.Parameters.AddWithValue("@Username", Session["Username"]);

                    int existingItemCount = (int)selectCommand.ExecuteScalar();

                    if (existingItemCount > 0)
                    {
                        // Update the existing record
                        string updateQuery = "UPDATE ShoppingCart SET Quantity = Quantity + @Quantity, TotalPrice = TotalPrice + @TotalPrice WHERE ProductID = @ProductID AND Username = @Username";
                        using (SqlCommand updateCommand = new SqlCommand(updateQuery, connection))
                        {
                            updateCommand.Parameters.AddWithValue("@ProductID", productId);
                            updateCommand.Parameters.AddWithValue("@Username", Session["Username"]);
                            updateCommand.Parameters.AddWithValue("@Quantity", quantity);
                            updateCommand.Parameters.AddWithValue("@TotalPrice", totalPrice);

                            updateCommand.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        // Insert a new record
                        string insertQuery = "INSERT INTO ShoppingCart (ProductID, ProductName, UnitPrice, Username, Quantity, TotalPrice) " +
                   "VALUES (@ProductID, @ProductName, @UnitPrice, @Username, @Quantity, @TotalPrice)";
                        using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
                        {
                            insertCommand.Parameters.AddWithValue("@ProductID", productId);
                            insertCommand.Parameters.AddWithValue("@ProductName", productName);
                            insertCommand.Parameters.AddWithValue("@UnitPrice", price);
                            insertCommand.Parameters.AddWithValue("@Username", Session["Username"]);
                            insertCommand.Parameters.AddWithValue("@Quantity", quantity);
                            insertCommand.Parameters.AddWithValue("@TotalPrice", totalPrice);

                            insertCommand.ExecuteNonQuery();
                        }
                    }
                }
            }
        }

    }

}