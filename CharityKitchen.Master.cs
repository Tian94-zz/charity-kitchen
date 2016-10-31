using CharityKitchen.CharityKitchenServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CharityKitchen
{
    public partial class CharityKitchenItem : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogOut_Click(object sender, EventArgs e)
        {
            CharityKitchen.CharityKitchenServiceReference.User u = new CharityKitchen.CharityKitchenServiceReference.User(); //Creates a new dummy user
            u.Roles = new RoleCombo[0];

            Session["user"] = u;
            //FormsAuthentication.SignOut();
            Response.Redirect("~/Login.aspx");
        }
    }
}