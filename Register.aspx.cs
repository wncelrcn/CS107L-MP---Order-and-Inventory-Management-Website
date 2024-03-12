using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace CS107L_MP
{
    public partial class WebForm3 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.UnobtrusiveValidationMode = System.Web.UI.UnobtrusiveValidationMode.None;
        }

        protected void regBtn_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                // Retrieve user input from the registration form
                string firstName = firstNameTxtBox.Text;
                string lastName = lastNameTxtBox.Text;
                string contactNumber = contactTxtBox.Text;
                string address = addTxtBox.Text;
                string username = usernameTxtBox.Text;
                string password = passTxtBox.Text;

                try
                {
                    // Check if the username is "admin"
                    if (username.ToLower() == "admin")
                    {
                        // Display alert using ScriptManager to avoid interference with validation controls
                        if (ScriptManager.GetCurrent(this.Page) != null)
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "adminAlert", "alert('Username cannot be 'admin'. Please choose another username.');", true);
                        }
                        else
                        {
                            // Fallback if ScriptManager is not available
                            Response.Write("<script>alert('Username cannot be 'admin'. Please choose another username.');</script>");
                        }
                        return; // Exit the method, preventing further registration steps
                    }

                    string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        // Check if the username already exists
                        string checkUsernameQuery = "SELECT COUNT(*) FROM Users WHERE username = @username";
                        SqlCommand checkUsernameCommand = new SqlCommand(checkUsernameQuery, connection);
                        checkUsernameCommand.Parameters.AddWithValue("@username", username);
                        int usernameCount = (int)checkUsernameCommand.ExecuteScalar();

                        if (usernameCount > 0)
                        {
                            // Username already exists, show alert and return
                            ScriptManager.RegisterStartupScript(this, GetType(), "usernameExistsAlert", "alert('Username already exists! Please choose a different username.');", true);
                            connection.Close();
                            return;
                        }

                        string usersQuery = "INSERT INTO Users (username, FirstName, LastName, ContactNumber, Address) VALUES (@username, @firstName, @lastName, @contactNumber, @address)";
                        SqlCommand usersCommand = new SqlCommand(usersQuery, connection);
                        usersCommand.Parameters.AddWithValue("@username", username);
                        usersCommand.Parameters.AddWithValue("@firstName", firstName);
                        usersCommand.Parameters.AddWithValue("@lastName", lastName);
                        usersCommand.Parameters.AddWithValue("@contactNumber", contactNumber);
                        usersCommand.Parameters.AddWithValue("@address", address);
                        int usersRowsAffected = usersCommand.ExecuteNonQuery();

                        string authUsersQuery = "INSERT INTO AuthUsers (username, password) VALUES (@username, @password)";
                        SqlCommand authUsersCommand = new SqlCommand(authUsersQuery, connection);
                        authUsersCommand.Parameters.AddWithValue("@username", username);
                        authUsersCommand.Parameters.AddWithValue("@password", password);
                        int authUsersRowsAffected = authUsersCommand.ExecuteNonQuery();

                        connection.Close();
                    }

                    ScriptManager.RegisterStartupScript(this, GetType(), "registrationSuccessAlert", "alert('Registration successful!');", true);
                    Response.Redirect("Login.aspx");
                }
                catch (Exception ex)
                {
                    // Handle exceptions
                    ScriptManager.RegisterStartupScript(this, GetType(), "registrationErrorAlert", $"alert('Registration failed: {ex.Message}');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "validationErrorAlert", "alert('Please check your Inputs.');", true);
            }
        }
    }
}
