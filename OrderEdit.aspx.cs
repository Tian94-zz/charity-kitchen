using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CharityKitchen
{
    public partial class OrderEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblID.Text = (String)HttpContext.Current.Items["idData"];
                GetMealList();
                GetMealPrice();
                LoadOrderLines();
                GetOrderInfo();
                lblOrderDate.Text = DateTime.Now.ToShortDateString();
            }
        }

        #region vars
        static int clientID;
        static decimal OrderTotal = 0;
        static int rowIndex;
        #endregion vars

        #region events
        protected void gvResults_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                rowIndex = Convert.ToInt32(e.CommandArgument);
            }
        }

        protected void gvResults_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            UpdateOrderTotalDelete(rowIndex);
            DeleteOrderLine(rowIndex);
            LoadOrderLines();            
        }

        protected void btnCancelOrder_Click(object sender, EventArgs e)
        {

        }

        protected void btnAddToOrder_Click(object sender, EventArgs e)
        {
            AddOrderLineToDB();
            UpdateOrderTotal();
            LoadOrderLines();
        }

        protected void drpMeals_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetMealPrice();
        }

        protected void btnClientByID_Click(object sender, EventArgs e)
        {
            ChangeClient();
        }

        protected void btnUpdateOrder_Click(object sender, EventArgs e)
        {
            UpdateOrder();   
        }

        protected void btnClientList_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "var Mleft = (screen.width/2)-(760/2);var Mtop = (screen.height/2)-(700/2);window.open( 'ClientList.aspx', null, 'status=yes,toolbar=no,scrollbars=yes,menubar=no,location=no,top=\'+Mtop+\', left=\'+Mleft+\'' );", true);
        }
        #endregion events

        #region methods
        protected void UpdateOrder() {
            CharityKitchenServiceReference.CKServiceSoapClient svc = new CharityKitchenServiceReference.CKServiceSoapClient();
            svc.UpdateOrder(calDelDate.SelectedDate, clientID, Convert.ToInt32(lblID.Text));
        }

        protected void ChangeClient()
        {
            try
            {
                clientID = Convert.ToInt32(txtID.Text);
                lblID.Text = txtID.Text;
                CharityKitchenServiceReference.CKServiceSoapClient svc = new CharityKitchenServiceReference.CKServiceSoapClient();
                lblClientFirstName.Text = svc.GetClientName(clientID)[0].ToString();
                lblClientLastName.Text = svc.GetClientName(clientID)[1].ToString();               
            }
            catch
            {
                //Server.Transfer("GeneralError.aspx");
            }
        }

        protected void GetMealList()
        {
            CharityKitchenServiceReference.CKServiceSoapClient svc = new CharityKitchenServiceReference.CKServiceSoapClient();
            drpMeals.DataSource = svc.GetMealNames();
            drpMeals.DataValueField = "MealID";
            drpMeals.DataTextField = "MealName";
            drpMeals.DataBind();
        }

        protected void GetMealPrice()
        {
            CharityKitchenServiceReference.CKServiceSoapClient svc = new CharityKitchenServiceReference.CKServiceSoapClient();
            lblPrice.Text = svc.GetMealPrice(Convert.ToInt32(drpMeals.SelectedItem.Value));
        }


        protected void AddOrderLineToDB()
        {
            CharityKitchenServiceReference.CKServiceSoapClient svc = new CharityKitchenServiceReference.CKServiceSoapClient();
            svc.AddOrderLine(Convert.ToInt32(lblID.Text), Convert.ToInt32(drpMeals.SelectedItem.Value), Convert.ToDecimal(lblPrice.Text));
        }

        protected void LoadOrderLines()
        {
            CharityKitchenServiceReference.CKServiceSoapClient svc = new CharityKitchenServiceReference.CKServiceSoapClient();
            gvResults.DataSource = svc.GetOrderLines(Convert.ToInt32(lblID.Text));
            gvResults.DataBind();
        }

        protected void GetOrderInfo()
        {
            CharityKitchenServiceReference.CKServiceSoapClient svc = new CharityKitchenServiceReference.CKServiceSoapClient();
            calDelDate.SelectedDate = (DateTime)svc.GetOrderInfo(Convert.ToInt32(lblID.Text))[0];
            clientID = (int)svc.GetOrderInfo(Convert.ToInt32(lblID.Text))[1];
            lblOrderTotal.Text = svc.GetOrderInfo(Convert.ToInt32(lblID.Text))[2].ToString();
            lblOrderDate.Text = svc.GetOrderInfo(Convert.ToInt32(lblID.Text))[3].ToString();
            lblClientFirstName.Text = svc.GetClientName(clientID)[0].ToString();
            lblClientLastName.Text = svc.GetClientName(clientID)[1].ToString();

        }

        protected void DeleteOrderLine(int _rowIndex)
        {
            CharityKitchenServiceReference.CKServiceSoapClient svc = new CharityKitchenServiceReference.CKServiceSoapClient();
            svc.DeleteOrderLine(Convert.ToInt32(gvResults.Rows[_rowIndex].Cells[1].Text));
        }

        protected void UpdateOrderTotalDelete(int _rowIndex) //has to update total to server
       {
            decimal Price = Convert.ToDecimal(gvResults.Rows[_rowIndex].Cells[4].Text); // ensure this cell is the price
            Price = decimal.Subtract(Convert.ToDecimal(lblOrderTotal.Text), Price);
            OrderTotal = Price;
            lblOrderTotal.Text = Price.ToString();

            UpdateTotalToDB();
        }

        protected void UpdateOrderTotal() //has to update total to server.
        {
            decimal price = Convert.ToDecimal(lblPrice.Text);
            decimal currentTotal = Convert.ToDecimal(lblOrderTotal.Text);         
            OrderTotal = decimal.Add(currentTotal, price);
            lblOrderTotal.Text = OrderTotal.ToString();

            UpdateTotalToDB();
        }

        protected void UpdateTotalToDB()
        {
            CharityKitchenServiceReference.CKServiceSoapClient svc = new CharityKitchenServiceReference.CKServiceSoapClient();
            svc.UpdateOrderTotal(OrderTotal, Convert.ToInt32(lblID.Text));
        }
        #endregion methods


    }
}