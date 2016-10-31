using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CharityKitchen
{
    public partial class Orders : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {                
                ClearDataTable();
                ResetOrderTotal();
                GetMealList();
                GetMealPrice();
                GetOrderLineColumns();
                lblCurrentDate.Text = DateTime.Now.ToShortDateString();
            }            
        }

        #region vars
        static DataTable OrderLines = new DataTable();
        static int clientID;
        static decimal OrderTotal = 0;
        static int rowIndex;
        #endregion vars

        #region events
        protected void btnClientList_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "var Mleft = (screen.width/2)-(760/2);var Mtop = (screen.height/2)-(700/2);window.open( 'ClientList.aspx', null, 'status=yes,toolbar=no,scrollbars=yes,menubar=no,location=no,top=\'+Mtop+\', left=\'+Mleft+\'' );", true);
        }

        protected void btnCancelOrder_Click(object sender, EventArgs e)
        {
            Response.Redirect("Orders.aspx");
        }

        protected void btnConfirmOrder_Click(object sender, EventArgs e)
        {
            if (AddOrderToDB())
            {
                AddOrderLinesToDB();
                Response.Redirect("Orders.aspx");
            }
        }

        protected void gvResults_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                rowIndex = Convert.ToInt32(e.CommandArgument);
            }
        }

        protected void gvResults_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            DeleteOrderItem();
            gvResults.DataSource = OrderLines;
            gvResults.DataBind();
        }

        protected void btnAddToOrder_Click(object sender, EventArgs e)
        {
            AddOrderItem();
            UpdateOrderTotal(Convert.ToDecimal(lblPrice.Text));
        }

        protected void drpMeals_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetMealPrice();
        }

        protected void btnClientByID_Click(object sender, EventArgs e)
        {
            GetClientInfo();
        }
        #endregion events

        #region methods
        protected void GetMealPrice()
        {
            CharityKitchenServiceReference.CKServiceSoapClient svc = new CharityKitchenServiceReference.CKServiceSoapClient();
            lblPrice.Text = svc.GetMealPrice(Convert.ToInt32(drpMeals.SelectedItem.Value));
        }

        protected void GetMealList()
        {
            CharityKitchenServiceReference.CKServiceSoapClient svc = new CharityKitchenServiceReference.CKServiceSoapClient();
            drpMeals.DataSource = svc.GetMealNames();
            drpMeals.DataValueField = "MealID";
            drpMeals.DataTextField = "MealName";
            drpMeals.DataBind();
        }

        protected void GetClientInfo()
        {
            try
            {
                clientID = Convert.ToInt32(txtID.Text);

                CharityKitchenServiceReference.CKServiceSoapClient svc = new CharityKitchenServiceReference.CKServiceSoapClient();
                //Converting all data into textboxes etc.                
                lblClientFirstName.Text = svc.GetClientInfo(clientID)[1].ToString();
                lblClientLastName.Text = svc.GetClientInfo(clientID)[2].ToString();
            }
            catch
            {
                //Server.Transfer("GeneralError.aspx");
            }
        }

        protected void AddOrderItem()
        {
            string[] OrderItem = new string[3];
            OrderItem[0] = drpMeals.SelectedItem.Text;
            OrderItem[1] = lblPrice.Text;
            OrderItem[2] = drpMeals.SelectedItem.Value.ToString();

            DataRow OrderLine = OrderLines.NewRow();

            OrderLine["MealName"] = OrderItem[0];
            OrderLine["MealPrice"] = OrderItem[1].ToString();
            OrderLine["MealID"] = OrderItem[2];
            OrderLines.Rows.Add(OrderLine);

            gvResults.DataSource = OrderLines;
            gvResults.DataBind();
        }

        protected bool CheckForClient()
        {
            bool HasClient;
            if (lblClientFirstName.Text == "First")
            {
                HasClient = false;
                lblInfo.ForeColor = System.Drawing.Color.Red;
                lblInfo.Text = "*Please submit a Client ID";
            }
            else
            {
                HasClient = true;
                lblInfo.Text = "";
            }

            return HasClient;
        }

        protected bool CheckForDeliveryDate()
        {
            {
                bool HasDeliveryDate;
                if (calDelDate.SelectedDate.Date == DateTime.MinValue.Date)
                {
                    HasDeliveryDate = false;
                    lblDeliveryInfo.ForeColor = System.Drawing.Color.Red;
                    lblDeliveryInfo.Text = "*Please select a delivery date";
                }
                else
                {
                    HasDeliveryDate = true;
                    lblDeliveryInfo.Text = "";
                }

                return HasDeliveryDate;
            }
        }

        protected void GetOrderLineColumns()
        {
            DataColumnCollection columns = OrderLines.Columns;
            if (columns.Contains("MealName"))
            {
            }
            else
            {
                OrderLines.Columns.Add(new DataColumn("MealName", typeof(string)));
                OrderLines.Columns.Add(new DataColumn("MealPrice", typeof(string)));
                OrderLines.Columns.Add(new DataColumn("MealID", typeof(string)));

                gvResults.DataSource = OrderLines;
                gvResults.DataBind();
            }
        }

        protected void UpdateOrderTotal(decimal _itemPrice)
        {
            decimal total = Convert.ToDecimal(lblOrderTotal.Text);
            decimal price = Convert.ToDecimal(_itemPrice);
            decimal OrderTotal = decimal.Add(total, price);
            lblOrderTotal.Text = OrderTotal.ToString();
        }

        protected void DeleteOrderItem()
        {
            for (int i = OrderLines.Rows.Count - 1; i >= 0; i--)
            {
                DataRow OrderLine = OrderLines.Rows[i];
                if (rowIndex == i)
                {
                    UpdateOrderTotalDelete(OrderLine);
                    OrderLine.Delete();
                }               
            }
        }

        protected void UpdateOrderTotalDelete(DataRow OrderLine)
        {
            decimal Price = Convert.ToDecimal(OrderLine["MealPrice"]);
            Price = decimal.Subtract(Convert.ToDecimal(lblOrderTotal.Text), Price);
            lblOrderTotal.Text = Price.ToString();
        }

        protected bool AddOrderToDB()
        {
            bool Success = true;
            if (CheckForDeliveryDate() && CheckForClient())
            {
                CharityKitchenServiceReference.CKServiceSoapClient svc = new CharityKitchenServiceReference.CKServiceSoapClient();
                svc.AddOrder(calDelDate.SelectedDate, Convert.ToInt32(txtID.Text), Convert.ToDecimal(lblOrderTotal.Text), Convert.ToDateTime(lblCurrentDate.Text));//ADDS THE ORDER
                lblID.Text = svc.GetLastOrderID();
            }
            else
            {
                Success = false;
            }
            return Success;
        }

        protected void AddOrderLinesToDB()
        {
            CharityKitchenServiceReference.CKServiceSoapClient svc = new CharityKitchenServiceReference.CKServiceSoapClient();

            for (int i = OrderLines.Rows.Count - 1; i >= 0; i--)
            {
                DataRow OrderLine = OrderLines.Rows[i];
                svc.AddOrderLine(Convert.ToInt32(lblID.Text), Convert.ToInt32(OrderLine["MealID"]), Convert.ToDecimal(OrderLine["MealPrice"]));
            }
        }

        protected void ClearDataTable()
        {
            OrderLines.Clear();
        }

        protected void ResetOrderTotal()
        {
            OrderTotal = 0;
        }



        #endregion methods

        protected void btnShowHelp_Click(object sender, EventArgs e)
        {
            txtHelp.Visible = true;
            HelpStrings help = new HelpStrings();
            txtHelp.Text = help.NewOrder;
        }
    }
}