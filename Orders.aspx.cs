using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CharityKitchen
{
    public partial class Orders1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetAllOrders();
            }
        }

        #region vars
        int rowIndex;
        #endregion vars


        #region events
        protected void gvResults_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                rowIndex = Convert.ToInt32(e.CommandArgument);
            }
            else
            {
                rowIndex = Convert.ToInt32(e.CommandArgument);
            }
        }

        protected void gvResults_RowEditing(object sender, GridViewEditEventArgs e)
        {
            HttpContext.Current.Items["idData"] = gvResults.Rows[rowIndex].Cells[2].Text;//holds data from the client ID wanting to be edited
            Server.Transfer("OrderEdit.aspx");
        }

        protected void gvResults_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            CharityKitchenServiceReference.CKServiceSoapClient svc = new CharityKitchenServiceReference.CKServiceSoapClient();
            int id = Convert.ToInt32(gvResults.Rows[rowIndex].Cells[2].Text);//NEED A METHOD TO DELETE RELATED ORDERLINES OR IT SHITS ITSELF

            DeleteOrderLines(id);
            DeleteOrder(id);
            gvResults.DataSource = svc.GetAllOrders();
            gvResults.DataBind();
        }

        protected void btnNewOrder_Click(object sender, EventArgs e)
        {
            Server.Transfer("NewOrder.aspx");
        }
        #endregion events
        #region methods        
        protected void GetAllOrders()
        {
            CharityKitchenServiceReference.CKServiceSoapClient svc = new CharityKitchenServiceReference.CKServiceSoapClient();
            gvResults.DataSource = svc.GetAllOrders();
            gvResults.DataBind();
        }

        protected void DeleteOrder(int id)
        {
            CharityKitchenServiceReference.CKServiceSoapClient svc = new CharityKitchenServiceReference.CKServiceSoapClient();
            svc.DeleteOrder(id);
        }

        protected void DeleteOrderLines(int id)
        {
            CharityKitchenServiceReference.CKServiceSoapClient svc = new CharityKitchenServiceReference.CKServiceSoapClient();
            svc.DeleteOrderLines(id);
        }

        #endregion methods

        protected void btnShowHelp_Click(object sender, EventArgs e)
        {
            txtHelp.Visible = true;
            HelpStrings help = new HelpStrings();
            txtHelp.Text = help.Orders;
        }
    }
}