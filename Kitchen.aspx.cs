using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CharityKitchen
{
    public partial class Kitchen : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetKitchenOrders();
                GetKitchenOrderLines();
            }
        }

        #region events

        protected void btnSubmitOrder_Click(object sender, EventArgs e)
        {
            SetOrderToDeliveryReady();
            GetKitchenOrders();
            GetKitchenOrderLines();
        }
        #endregion events

        #region methods
        protected void GetKitchenOrders()
        {
            CharityKitchenServiceReference.CKServiceSoapClient svc = new CharityKitchenServiceReference.CKServiceSoapClient();
            gvResults.DataSource = svc.GetKitchenOrders();
            gvResults.DataBind();

        }

        protected void GetKitchenOrderLines()
        {
            CharityKitchenServiceReference.CKServiceSoapClient svc = new CharityKitchenServiceReference.CKServiceSoapClient();
            gvOrderMeals.DataSource = svc.GetKitchenOrderLines();
            gvOrderMeals.DataBind();
        }

        protected void SetOrderToDeliveryReady()
        {
            CharityKitchenServiceReference.CKServiceSoapClient svc = new CharityKitchenServiceReference.CKServiceSoapClient();
            svc.SetDeliveryReady(Convert.ToInt32(txtOrderNumber.Text));
        }
        #endregion methods

        protected void btnShowHelp_Click(object sender, EventArgs e)
        {
            txtHelp.Visible = true;
            HelpStrings help = new HelpStrings();
            txtHelp.Text = help.Kitchen;
        }
    }
}