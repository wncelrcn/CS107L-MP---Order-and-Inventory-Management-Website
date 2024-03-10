﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
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
        }

        // Event handler for category selection change
        protected void CategoryDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedCategory = CategoryDropDown.SelectedValue;
            var products = AllProducts(selectedCategory);

            // Bind the repeater to the list of products
            ProductRepeater.DataSource = products;
            ProductRepeater.DataBind();
        }

        // Method to retrieve products from the database based on category
        public IEnumerable<Product> AllProducts(string category)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Admin\Documents\GitHub\CS107L-MP---Order-and-Inventory-Management-Website\App_Data\websiteDatabase.mdf;Integrated Security=True";

            string query = "SELECT ProductName, Price, Stock FROM Products";
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
                    product.Name = reader["ProductName"].ToString();
                    product.Price = Convert.ToDouble(reader["Price"]);
                    product.Stock = Convert.ToInt32(reader["Stock"]);
                    products.Add(product);
                }
            }

            return products;
        }
        
    }
}
