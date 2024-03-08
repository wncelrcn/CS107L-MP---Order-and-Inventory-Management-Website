using System;
using System.Collections.Generic;
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
                //mema  
                // Set the session variable for the username
                Session["Username"] = usernameTxtBox.Text;

                // Redirect to the Order.aspx page
                Response.Redirect("Order.aspx?username=" + usernameTxtBox.Text);
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Please check your Inputs.');", true);
            }
        }

    }
}