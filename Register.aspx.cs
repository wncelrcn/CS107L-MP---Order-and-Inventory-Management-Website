using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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
                string script = $"alert('Successfully Registered! \\nUsername: {usernameTxtBox.Text}, Password: {passTxtBox.Text}');";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
                Response.Redirect("Login.aspx");
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Please check your Inputs.');", true);
            }


            


        }
    }
}