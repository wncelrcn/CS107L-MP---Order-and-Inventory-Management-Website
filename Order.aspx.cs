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

            // Extract product information and quantity from CommandArgument
            string[] args = btn.CommandArgument.Split(';');
            string productId = args[0];
            string productName = args[1];
            double price = double.Parse(args[2]);
            int quantity = int.Parse(((TextBox)item.FindControl("quantityNo")).Text);

            // Calculate total price
            double totalPrice = price * quantity;

            // Display information in alert
            string alertMessage = $"Product ID: {productId}, Product Name: {productName}, Username: {Session["Username"]}, Quantity: {quantity}, Total Price: {totalPrice:C}";
            ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('{alertMessage}');", true);



        }
    }
}
