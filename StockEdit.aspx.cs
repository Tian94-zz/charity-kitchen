using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CharityKitchen
{
    public partial class StockEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;

            lblStockID.Text = (String)HttpContext.Current.Items["idData"];//Recieves the StockID from previous page
            GetUnitList();
            GetStockInfo();
        }

        #region events
        protected void btnDelete_Click(object sender, EventArgs e)
        {
                DeleteStock();
        }

        protected void btnSaveEdit_Click(object sender, EventArgs e)
        {
            SaveEdit();
            Server.Transfer("Stock.aspx");
        }
        #endregion events

        #region methods
        protected void DeleteStock()
        {
            CharityKitchenServiceReference.CKServiceSoapClient svc = new CharityKitchenServiceReference.CKServiceSoapClient();

            if (svc.DeleteStock(Convert.ToInt16(lblStockID.Text)))
            {
                Server.Transfer("Stock.aspx");
            }
            else
            {
                lblInfo.ForeColor = System.Drawing.Color.Red;
                lblInfo.Text = "*Cannot delete item because it is used in a meal";
            }
        }

        protected void GetUnitList()
        {
            CharityKitchenServiceReference.CKServiceSoapClient svc = new CharityKitchenServiceReference.CKServiceSoapClient();
            drpUnits.DataSource = svc.GetMeasurementUnits();
            drpUnits.DataTextField = "Shorthand";
            drpUnits.DataValueField = "MeasurementUnitID";
            drpUnits.DataBind();
        }

        protected void GetStockInfo()
        {
            int id = Convert.ToInt16(lblStockID.Text);
            CharityKitchenServiceReference.CKServiceSoapClient svc = new CharityKitchenServiceReference.CKServiceSoapClient();
            txtStockName.Text = svc.GetStockInfo(id)[0].ToString();
            txtQuantity.Text = svc.GetStockInfo(id)[1].ToString();
            drpUnits.SelectedIndex = (int)svc.GetStockInfo(id)[2]-1;
            txtDescription.Text = svc.GetStockInfo(id)[3].ToString();
        }

        protected void SaveEdit()
        {
            CharityKitchenServiceReference.CKServiceSoapClient svc = new CharityKitchenServiceReference.CKServiceSoapClient();
            svc.SaveStockEdit(txtStockName.Text, Convert.ToInt16(txtQuantity.Text), drpUnits.SelectedIndex+1, txtDescription.Text, Convert.ToInt16(lblStockID.Text));
        }
        #endregion methods


    }
}