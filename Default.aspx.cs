using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CharityKitchen
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Will need code on other pages to allow access or just redirect
            if (!IsPostBack)
            {
                if (this.GetAccessLevel("Master") != AccessLevel.Deny)
                {
                    Response.Redirect("Accounts.aspx");
                }
                else if (this.GetAccessLevel("Admin") != AccessLevel.Deny)
                {
                    Response.Redirect("Accounts.aspx");
                }
                else if (this.GetAccessLevel("Warehouse") != AccessLevel.Deny)
                {
                    Response.Redirect("Stock.aspx");
                }
                else if (this.GetAccessLevel("Delivery") != AccessLevel.Deny)
                {
                    Response.Redirect("Deliveries.aspx");
                }
                else
                {
                    Response.Redirect("AccessDenied.aspx");
                }
            }
        }

        #region methods

        #endregion methods
    }
}