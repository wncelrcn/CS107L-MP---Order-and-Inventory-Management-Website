using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CS107L_MP.App.Sales;

namespace CS107L_MP
{
    public partial class ViewSales : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadSales();
            }
        }

        // Load sales summary from the database and bind them to the repeater
        private void LoadSales()
        {
            // Get sales summary from the database
            IEnumerable<SalesSummary> salesSummaries = GetSalesSummary();

            // Bind sales summaries to the repeater
            SalesRepeater.DataSource = salesSummaries;
            SalesRepeater.DataBind();
        }

        // Retrieve sales summary from the database
        private IEnumerable<SalesSummary> GetSalesSummary()
        {
            List<SalesSummary> salesSummaries = new List<SalesSummary>();

            // Connect to the database and retrieve sales summary
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            string query = "SELECT TransactionID, SUM(TotalOrderPrice) AS TotalOrderPrice FROM Orders GROUP BY TransactionID";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                // Read data and create SalesSummary objects
                while (reader.Read())
                {
                    SalesSummary salesSummary = new SalesSummary();
                    salesSummary.TransactionID = reader["TransactionID"].ToString();
                    double totalOrderPrice = Convert.ToDouble(reader["TotalOrderPrice"]);
                    salesSummary.TotalRevenue = totalOrderPrice * 0.2; // 20% of the total order price
                    salesSummaries.Add(salesSummary);
                }
            }

            return salesSummaries;
        }
    }

    
}