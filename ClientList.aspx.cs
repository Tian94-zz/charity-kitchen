using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CharityKitchen
{
    public partial class ClientList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetClientList();
            }
        }

        #region events
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            CharityKitchenServiceReference.CKServiceSoapClient svc = new CharityKitchenServiceReference.CKServiceSoapClient();
            gvResults.DataSource = svc.SearchClient(txtFirstName.Text, txtLastName.Text);
            gvResults.DataBind();
        }
        #endregion events

        #region methods
        protected void GetClientList()
        {
            CharityKitchenServiceReference.CKServiceSoapClient svc = new CharityKitchenServiceReference.CKServiceSoapClient();
            gvResults.DataSource = svc.GetClientList();
            gvResults.DataBind();
        }
        #endregion methods


    }
}