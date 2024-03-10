using System;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace CS107L_MP
{
    public partial class Cart : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Call a method to retrieve and bind cart details
                BindCartDetails();
            }
        }

        private void BindCartDetails()
        {
            // Check if the user is logged in
            if (Session["Username"] != null)
            {
                string username = Session["Username"].ToString();

                // Fetch cart details from the ShoppingCart table for the logged-in user
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string selectQuery = "SELECT ShoppingCart.ProductID, Products.ProductName, ShoppingCart.Quantity, ShoppingCart.TotalPrice, Products.Stock FROM ShoppingCart INNER JOIN Products ON ShoppingCart.ProductID = Products.ProductID WHERE Username = @Username";

                    using (SqlCommand command = new SqlCommand(selectQuery, connection))
                    {
                        command.Parameters.AddWithValue("@Username", username);

                        SqlDataReader reader = command.ExecuteReader();

                        // Display cart details in a GridView or other suitable control
                        CartGridView.DataSource = reader;
                        CartGridView.DataBind();
                    }
                }
            }
            else
            {
                // Redirect to the login page if the user is not logged in
                Response.Redirect("Login.aspx");
            }
        }

        protected void CartGridView_Command(object sender, System.Web.UI.WebControls.CommandEventArgs e)
        {
            if (e.CommandName == "ReduceQuantity")
            {
                string[] args = e.CommandArgument.ToString().Split(';');
                int productId = int.Parse(args[0]);
                int currentQuantity = int.Parse(args[1]);
                decimal totalPrice = decimal.Parse(args[2]);

                // Implement logic to reduce the quantity in the database
                int newQuantity = Math.Max(1, currentQuantity - 1); // Ensure the quantity doesn't go below 1
                decimal newTotalPrice = totalPrice / currentQuantity * newQuantity;

                UpdateCartQuantity(productId, newQuantity, newTotalPrice);
            }
            else if (e.CommandName == "AddQuantity")
            {
                string[] args = e.CommandArgument.ToString().Split(';');
                int productId = int.Parse(args[0]);
                int currentQuantity = int.Parse(args[1]);
                decimal totalPrice = decimal.Parse(args[2]);
                int stock = int.Parse(args[3]);

                // Implement logic to add quantity in the database
                int newQuantity = Math.Min(currentQuantity + 1, stock); // Ensure the quantity doesn't exceed stock
                decimal newTotalPrice = totalPrice / currentQuantity * newQuantity;

                UpdateCartQuantity(productId, newQuantity, newTotalPrice);
            }
            else if (e.CommandName == "RemoveItem")
            {
                int productId = int.Parse(e.CommandArgument.ToString());

                // Implement logic to remove the item from the database
                RemoveCartItem(productId);
            }

            // Refresh the cart details
            BindCartDetails();
        }

        private void UpdateCartQuantity(int productId, int newQuantity, decimal newTotalPrice)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string updateQuery = "UPDATE ShoppingCart SET Quantity = @Quantity, TotalPrice = @TotalPrice WHERE ProductID = @ProductID";

                using (SqlCommand command = new SqlCommand(updateQuery, connection))
                {
                    command.Parameters.AddWithValue("@Quantity", newQuantity);
                    command.Parameters.AddWithValue("@TotalPrice", newTotalPrice);
                    command.Parameters.AddWithValue("@ProductID", productId);

                    command.ExecuteNonQuery();
                }
            }
        }

        private void RemoveCartItem(int productId)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string deleteQuery = "DELETE FROM ShoppingCart WHERE ProductID = @ProductID";

                using (SqlCommand command = new SqlCommand(deleteQuery, connection))
                {
                    command.Parameters.AddWithValue("@ProductID", productId);

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
