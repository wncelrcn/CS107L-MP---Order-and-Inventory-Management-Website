using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI.WebControls;
using CS107L_MP.App.ManageOrders;

namespace CS107L_MP
{
    public partial class ManageOrders : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadOrders();
            }
        }

        // Load orders from the database and bind them to the repeater
        private void LoadOrders()
        {
            // Get orders from the database
            IEnumerable<MyOrderItem> orderItems = GetOrderItems();

            // Bind order items to the repeater
            OrderRepeater.DataSource = orderItems;
            OrderRepeater.DataBind();
        }


        // Retrieve order items from the database
        private IEnumerable<MyOrderItem> GetOrderItems()
        {
            List<MyOrderItem> orderItems = new List<MyOrderItem>();

            // Connect to the database and retrieve order items
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            string query = "SELECT TransactionID, Username, ProductName, Quantity, TotalOrderPrice, OrderDate, OrderStatus FROM Orders ORDER BY OrderDate DESC";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                // Read data and create MyOrderItem objects
                while (reader.Read())
                {
                    string transactionID = reader["TransactionID"].ToString();

                    // Check if the order item already exists in the list
                    MyOrderItem orderItem = orderItems.FirstOrDefault(item => item.TransactionID == transactionID);

                    // If the order item doesn't exist, create a new one and add it to the list
                    if (orderItem == null)
                    {
                        orderItem = new MyOrderItem();
                        orderItem.TransactionID = transactionID;
                        orderItem.Username = reader["Username"].ToString();
                        orderItem.OrderDate = Convert.ToDateTime(reader["OrderDate"]);
                        orderItem.Status = reader["OrderStatus"].ToString();
                        orderItems.Add(orderItem);
                    }

                    // Add the product to the order item
                    OrderProduct product = new OrderProduct();
                    product.ProductName = reader["ProductName"].ToString();
                    product.Quantity = Convert.ToInt32(reader["Quantity"]);
                    orderItem.Products.Add(product);

                    // Accumulate the total price for the order item
                    orderItem.TotalPrice += Convert.ToDouble(reader["TotalOrderPrice"]);
                }
            }

            return orderItems;
        }

        protected void OrderRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                // Find the inner repeater control
                Repeater ProductRepeater = (Repeater)e.Item.FindControl("ProductRepeater");

                // Bind product details to the inner repeater
                MyOrderItem orderItem = (MyOrderItem)e.Item.DataItem;
                ProductRepeater.DataSource = orderItem.Products;
                ProductRepeater.DataBind();

                // Find the current status label
                Label lblCurrentStatus = (Label)e.Item.FindControl("lblCurrentStatus");

                // Set the text of the current status label
                lblCurrentStatus.Text = orderItem.Status;
            }
        }

        protected void ddlOrderStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedStatus = ddlOrderStatus.SelectedValue;

            // Filter orders based on the selected status
            IEnumerable<MyOrderItem> filteredOrders = GetFilteredOrders(selectedStatus);

            // Bind filtered orders to the repeater
            OrderRepeater.DataSource = filteredOrders;
            OrderRepeater.DataBind();
        }

        private IEnumerable<MyOrderItem> GetFilteredOrders(string selectedStatus)
        {
            // Get all orders if no status is selected
            if (string.IsNullOrEmpty(selectedStatus))
            {
                return GetOrderItems();
            }

            // Filter orders based on the selected status
            return GetOrderItems().Where(order => order.Status == selectedStatus);
        }


        // Load orders with the specified status from the database and bind them to the repeater
        private void LoadOrdersWithSelectedStatus(string selectedStatus)
        {
            // Get orders with the specified status from the database
            IEnumerable<MyOrderItem> orderItems = GetOrderItemsWithStatus(selectedStatus);

            // Bind order items to the repeater
            OrderRepeater.DataSource = orderItems;
            OrderRepeater.DataBind();
        }

        // Retrieve order items with the specified status from the database
        private IEnumerable<MyOrderItem> GetOrderItemsWithStatus(string selectedStatus)
        {
            List<MyOrderItem> orderItems = new List<MyOrderItem>();

            // Connect to the database and retrieve order items with the specified status
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            string query = "SELECT TransactionID, Username, ProductName, Quantity, TotalOrderPrice, OrderDate, OrderStatus FROM Orders WHERE OrderStatus = @SelectedStatus ORDER BY OrderDate DESC";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@SelectedStatus", selectedStatus);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                // Read data and create MyOrderItem objects
                while (reader.Read())
                {
                    string transactionID = reader["TransactionID"].ToString();

                    // Check if the order item already exists in the list
                    MyOrderItem orderItem = orderItems.FirstOrDefault(item => item.TransactionID == transactionID);

                    // If the order item doesn't exist, create a new one and add it to the list
                    if (orderItem == null)
                    {
                        orderItem = new MyOrderItem();
                        orderItem.TransactionID = transactionID;
                        orderItem.Username = reader["Username"].ToString();
                        orderItem.OrderDate = Convert.ToDateTime(reader["OrderDate"]);
                        orderItem.Status = reader["OrderStatus"].ToString();
                        orderItems.Add(orderItem);
                    }

                    // Add the product to the order item
                    OrderProduct product = new OrderProduct();
                    product.ProductName = reader["ProductName"].ToString();
                    product.Quantity = Convert.ToInt32(reader["Quantity"]);
                    orderItem.Products.Add(product);

                    // Accumulate the total price for the order item
                    orderItem.TotalPrice += Convert.ToDouble(reader["TotalOrderPrice"]);
                }
            }

            return orderItems;
        }

        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlStatus = (DropDownList)sender;
            RepeaterItem item = (RepeaterItem)ddlStatus.NamingContainer;
            Label lblTransactionID = (Label)item.FindControl("lblTransactionID");

            UpdateOrderStatus(lblTransactionID.Text, ddlStatus.SelectedValue);
            LoadOrdersWithSelectedStatus(ddlOrderStatus.SelectedValue); // Modified to load orders with selected status from ddlOrderStatus
        }

        protected void btnUpdateStatus_Click(object sender, EventArgs e)
        {
            Button btnUpdateStatus = (Button)sender;
            RepeaterItem item = (RepeaterItem)btnUpdateStatus.NamingContainer;
            DropDownList ddlStatus = (DropDownList)item.FindControl("ddlStatus");
            Label lblTransactionID = (Label)item.FindControl("lblTransactionID");
            Label lblCurrentStatus = (Label)item.FindControl("lblCurrentStatus");

            // Get the current selected status filter from the dropdown
            string currentFilter = ddlOrderStatus.SelectedValue;

            UpdateOrderStatus(lblTransactionID.Text, ddlStatus.SelectedValue);
            lblCurrentStatus.Text = ddlStatus.SelectedValue;

            // Reapply the current filter selection after updating the status
            ddlOrderStatus.SelectedValue = currentFilter;

            // Reload orders based on the current filter selection
            if (string.IsNullOrEmpty(currentFilter))
            {
                LoadOrders();
            }
            else
            {
                LoadOrdersWithSelectedStatus(currentFilter);
            }
        }

        private void UpdateOrderStatus(string transactionID, string newStatus)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            string query = "UPDATE Orders SET OrderStatus = @Status WHERE TransactionID = @TransactionID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Status", newStatus);
                command.Parameters.AddWithValue("@TransactionID", transactionID);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}