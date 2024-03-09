using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CS107L_MP
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.UnobtrusiveValidationMode = System.Web.UI.UnobtrusiveValidationMode.None;
        }

        protected void loginBtn_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                // Retrieve username and password from the login form
                string username = usernameTxtBox.Text;
                string password = passTxtBox.Text;

                try
                {
                    // Establish connection to the SQL database
                    string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        // Check if the username and password combination exists in AuthUsers table
                        string query = "SELECT COUNT(*) FROM AuthUsers WHERE username = @username AND password = @password";
                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@username", username);
                        command.Parameters.AddWithValue("@password", password);
                        int count = (int)command.ExecuteScalar();

                        if (count > 0)
                        {
                            // Authentication successful
                            // Set the session variable for the username
                            Session["Username"] = username;

                            // Redirect to the Order.aspx page
                            Response.Redirect("Order.aspx");
                        }
                        else
                        {
                            // Authentication failed
                            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Invalid username or password.');", true);
                        }

                        connection.Close();
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('An error occurred: " + ex.Message + "');", true);
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Please check your Inputs.');", true);
            }
        }

    }
}