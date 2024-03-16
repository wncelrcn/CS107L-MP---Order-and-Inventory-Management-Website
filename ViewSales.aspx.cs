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
        protected void DateRangeDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSales();
        }

        // Load sales summary from the database and bind them to the repeater
        private void LoadSales()
        {
            // Get the selected date range from the dropdown
            string selectedDateRange = DateRangeDropDown.SelectedValue;

            // Get sales summary from the database
            IEnumerable<SalesSummary> salesSummaries = GetSalesSummary(selectedDateRange);

            // Bind sales summaries to the repeater
            SalesRepeater.DataSource = salesSummaries;
            SalesRepeater.DataBind();

            // Calculate and set total accumulated sales and total accumulated profit
            double totalAccumulatedSales = salesSummaries.Sum(s => s.TotalRevenue);
            double totalAccumulatedProfit = salesSummaries.Sum(s => s.TotalRevenue * 0.2); // 20% profit

            // Set the summary values
            TotalAccumulatedSalesLiteral.Text = totalAccumulatedSales.ToString("C");
            TotalAccumulatedProfitLiteral.Text = totalAccumulatedProfit.ToString("C");
        }

        // Retrieve sales summary from the database
        private IEnumerable<SalesSummary> GetSalesSummary(string selectedDateRange)
        {
            List<SalesSummary> salesSummaries = new List<SalesSummary>();

            // Connect to the database and retrieve sales summary
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            // Modify the query based on the selected date range
            string query = "";
            switch (selectedDateRange)
            {
                case "Daily":
                    query = "SELECT TransactionID, SUM(TotalOrderPrice) AS TotalOrderPrice FROM Orders WHERE OrderDate = CONVERT(date, GETDATE()) GROUP BY TransactionID";
                    break;
                case "Weekly":
                    query = "SELECT TransactionID, SUM(TotalOrderPrice) AS TotalOrderPrice FROM Orders WHERE OrderDate BETWEEN DATEADD(week, -1, GETDATE()) AND GETDATE() GROUP BY TransactionID";
                    break;
                case "Monthly":
                    query = "SELECT TransactionID, SUM(TotalOrderPrice) AS TotalOrderPrice FROM Orders WHERE MONTH(OrderDate) = MONTH(GETDATE()) AND YEAR(OrderDate) = YEAR(GETDATE()) GROUP BY TransactionID";
                    break;
                case "Yearly": // Add this case
                    query = "SELECT TransactionID, SUM(TotalOrderPrice) AS TotalOrderPrice FROM Orders WHERE YEAR(OrderDate) = YEAR(GETDATE()) GROUP BY TransactionID";
                    break;
                default:
                    // Fetch all orders if no specific date range is selected
                    query = "SELECT TransactionID, SUM(TotalOrderPrice) AS TotalOrderPrice FROM Orders GROUP BY TransactionID";
                    break;

            }


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
                    salesSummary.TotalRevenue = Convert.ToDouble(reader["TotalOrderPrice"]);
                    salesSummaries.Add(salesSummary);
                }
            }

            return salesSummaries;
        }
    }
}