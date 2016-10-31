using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CharityKitchen
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            lblInfo.Text = "Logging in...";

            var svc = new CharityKitchenServiceReference.CKServiceSoapClient();
            var user = svc.UserLogin(txtUsername.Text, txtPassword.Text);

            if (user.ID > 0)
            {
                Session["user"] = user;
                Response.Redirect("~/Default.aspx");
            }
            else
            {
                lblInfo.ForeColor = System.Drawing.Color.Red;
                lblInfo.Text = "Invalid credentials";
            }
        }
    }
}