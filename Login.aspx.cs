using System;
using System.Data.SqlClient;

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

                        string query;
                        if (username.ToLower() == "admin")
                        {
                            // If the username is admin, retrieve password from AdminCredentials table
                            query = "SELECT COUNT(*) FROM AdminCredentials WHERE AdminUsername = @username AND Password = @password";
                        }
                        else
                        {
                            // Otherwise, retrieve password from AuthUsers table
                            query = "SELECT COUNT(*) FROM AuthUsers WHERE Username = @username AND Password = @password";
                        }

                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@username", username);
                        command.Parameters.AddWithValue("@password", password);
                        int count = (int)command.ExecuteScalar();

                        if (count > 0)
                        {
                            // Authentication successful
                            // Set the session variable for the username
                            Session["Username"] = username;

                            // Redirect to the appropriate page based on user role
                            if (username.ToLower() == "admin")
                            {
                                // Redirect to the Admin.aspx page for admin users
                                Response.Redirect("Admin.aspx");
                            }
                            else
                            {
                                // Redirect to the Order.aspx page for regular users
                                Response.Redirect("Order.aspx");
                            }
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
