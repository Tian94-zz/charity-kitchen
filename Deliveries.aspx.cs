using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CharityKitchen
{
    public partial class Deliveries : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;            
               // calDate.SelectedDate = GetTodaysDate();
               // GetDeliveries(GetTodaysDate());            
        }

        #region methods
        protected DateTime GetTodaysDate()
        {
            return DateTime.Today;
        }

        protected void GetDeliveries()
        {
            CharityKitchenServiceReference.CKServiceSoapClient svc = new CharityKitchenServiceReference.CKServiceSoapClient();
            gvResults.DataSource = svc.GetDeliveries(calDate.SelectedDate);
            gvResults.DataBind();
        }

        protected void GetDeliveries(DateTime CurrentDate)
        {
            CharityKitchenServiceReference.CKServiceSoapClient svc = new CharityKitchenServiceReference.CKServiceSoapClient();
            gvResults.DataSource = svc.GetDeliveries(CurrentDate);
            gvResults.DataBind();
        }
        #endregion methods

        protected void calDate_SelectionChanged(object sender, EventArgs e)
        {
            GetDeliveries();
        }

        protected void btnShowHelp_Click(object sender, EventArgs e)
        {
            txtHelp.Visible = true;
            HelpStrings help = new HelpStrings();
            txtHelp.Text = help.Deliveries;
        }
    }
}