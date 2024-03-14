using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using CS107L_MP.App.Products;

namespace CS107L_MP
{
    public partial class ManageInventory : System.Web.UI.Page
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

        protected void CategoryDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedCategory = CategoryDropDown.SelectedValue;
            var products = AllProducts(selectedCategory);

            ProductRepeater.DataSource = products;
            ProductRepeater.DataBind();
        }

        public IEnumerable<Product> AllProducts(string category)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            string query = "SELECT ProductID, ProductName, Price, Stock FROM Products";
            if (!string.IsNullOrEmpty(category))
            {
                query += " WHERE Category = @Category";
            }

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

                while (reader.Read())
                {
                    Product product = new Product();
                    product.ProductId = reader["ProductID"].ToString();
                    product.Name = reader["ProductName"].ToString();
                    product.Price = Convert.ToDouble(reader["Price"]);
                    product.Stock = Convert.ToInt32(reader["Stock"]);
                    products.Add(product);
                }
            }

            return products;
        }

        protected void UpdateStock_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            RepeaterItem item = (RepeaterItem)btn.NamingContainer;
            TextBox quantityTextBox = (TextBox)item.FindControl("quantityNo");

            string[] args = btn.CommandArgument.Split(';');
            string productId = args[0];
            int newStock = int.Parse(quantityTextBox.Text);

            UpdateProductStock(productId, newStock);

            // Refresh the repeater to reflect the updated stock
            string selectedCategory = CategoryDropDown.SelectedValue;
            var products = AllProducts(selectedCategory);
            ProductRepeater.DataSource = products;
            ProductRepeater.DataBind();
        }

        private void UpdateProductStock(string productId, int newStock)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string updateQuery = "UPDATE Products SET Stock = @Stock WHERE ProductID = @ProductID";

                using (SqlCommand command = new SqlCommand(updateQuery, connection))
                {
                    command.Parameters.AddWithValue("@Stock", newStock);
                    command.Parameters.AddWithValue("@ProductID", productId);

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
