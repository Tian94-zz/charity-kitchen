using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CharityKitchen
{
    public partial class Stock : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript:Maximise()", true);
            if (!IsPostBack)//without this the drpState.SelectedIndex will always return 0 and mess up or insert query
            {
                GetUnitList();
                GetStockList();
            }
        }

        #region events
        protected void btnEditStock_Click(object sender, EventArgs e)
        {
            OpenEditStockWindow();
        }

        protected void btnAddStockItem_Click(object sender, EventArgs e)
        {
            AddStock();
            GetStockList();
        }

        protected void btnClearFields_Click(object sender, EventArgs e)
        {
            ClearTheFields();
        }

        protected void btnQuickEdit_Click(object sender, EventArgs e)
        {
            QuickEditStockQty();
            GetStockList();
        }


        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SearchStockByName();
        }
        #endregion events

        #region methods
        protected void GetUnitList()
        {
            CharityKitchenServiceReference.CKServiceSoapClient svc = new CharityKitchenServiceReference.CKServiceSoapClient();
            drpUnits.DataSource = svc.GetMeasurementUnits();
            drpUnits.DataTextField = "Shorthand";
            drpUnits.DataValueField = "MeasurementUnitID";
            drpUnits.DataBind();
        }

        protected void GetStockList()
        {
            CharityKitchenServiceReference.CKServiceSoapClient svc = new CharityKitchenServiceReference.CKServiceSoapClient();
            gvResults.DataSource = svc.GetStockList();
            gvResults.DataBind();
        }

        protected void AddStock()
        {
            int qty = Convert.ToInt16(txtStockQuantity.Text);
            CharityKitchenServiceReference.CKServiceSoapClient svc = new CharityKitchenServiceReference.CKServiceSoapClient();
            svc.AddNewStock(txtStockName.Text, qty, drpUnits.SelectedIndex+1, txtStockDescription.Text);
            ClearTheFields();
        }

        protected void ClearTheFields()
        {
            txtStockName.Text = "";
            txtStockDescription.Text = "";
            txtStockQuantity.Text = "";
            drpUnits.SelectedIndex = 0;
        }

        protected void QuickEditStockQty()
        {
            try
            {
                int _qty = Convert.ToInt16(txtQuickEditUnits.Text);
                int _id = Convert.ToInt16(txtQuickEditID.Text);
                CharityKitchenServiceReference.CKServiceSoapClient svc = new CharityKitchenServiceReference.CKServiceSoapClient();
                svc.QuickEditStockQty(_qty, _id);

                txtQuickEditID.Text = "*Enter Stock ID*";
                txtQuickEditUnits.Text = "*Updated Quantity*";
                lblInfo.Text = "*SUCCESSFUL OPERATION";
            }
            catch
            {
                lblInfo.Text = "*Incorrect value type entered";
            }
        }

        protected void SearchStockByName()
        {
            CharityKitchenServiceReference.CKServiceSoapClient svc = new CharityKitchenServiceReference.CKServiceSoapClient();
            gvResults.DataSource = svc.SearchStock(txtSearch.Text);
            gvResults.DataBind();
        }

        private void OpenEditStockWindow()
        {
            try
            {
                CharityKitchenServiceReference.CKServiceSoapClient svc = new CharityKitchenServiceReference.CKServiceSoapClient();
                if (svc.ValidateStockID(Convert.ToInt32(txtStockID.Text)))
                {
                    HttpContext.Current.Items["idData"] = txtStockID.Text;//holds data from the client ID wanting to be edited
                    Server.Transfer("StockEdit.aspx"); //sends the data to the page and opens page
                }
                else
                {
                    lblInfo.Text = "*Stock ID not found, please enter valid ID";
                }
            }
            catch
            {
                lblInfo.Text = "*Stock ID not found, please enter valid ID";
            }
        }
        #endregion methods

        protected void btnShowHelp_Click(object sender, EventArgs e)
        {
            txtHelp.Visible = true;
            HelpStrings help = new HelpStrings();
            txtHelp.Text = help.Stock;
        }
    }
}