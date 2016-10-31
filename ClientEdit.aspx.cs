using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CharityKitchen
{
    public partial class ClientEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            
            lblClientID.Text = (String)HttpContext.Current.Items["idData"];//Recieves the ClientID from previous page
            AddStatesToDropDown();//Adds States to the dropdown list
            GetClientInfo();//Queries the client based on the data received from previous page
        }

        #region vars
        int pageID;
        #endregion vars

        #region events
        protected void btnSaveEdit_Click(object sender, EventArgs e)
        {
            SaveCurrentEdit();
        }
        #endregion events

        #region methods
        protected void GetClientInfo()
        {
            //int index;
            try
            {
                int clientID = Convert.ToInt32(lblClientID.Text);

                CharityKitchenServiceReference.CKServiceSoapClient svc = new CharityKitchenServiceReference.CKServiceSoapClient();
                //Converting all data into textboxes etc.                
                txtFirstName.Text = svc.GetClientInfo(clientID)[1].ToString();
                txtLastName.Text = svc.GetClientInfo(clientID)[2].ToString();
                txtDOB.Text = svc.GetClientInfo(clientID)[3].ToString();
                txtPhoneNumber.Text = svc.GetClientInfo(clientID)[4].ToString();
                txtEmail.Text = svc.GetClientInfo(clientID)[5].ToString();
                txtAddress.Text = svc.GetClientInfo(clientID)[6].ToString();
                drpState.SelectedIndex = (int)svc.GetClientInfo(clientID)[7]-1;
                txtSuburb.Text = svc.GetClientInfo(clientID)[8].ToString();
                txtPostcode.Text = svc.GetClientInfo(clientID)[9].ToString();
            }
            catch
            {
                //Server.Transfer("GeneralError.aspx");
            }
        }

        protected void AddStatesToDropDown()
        {
            CharityKitchenServiceReference.CKServiceSoapClient svc = new CharityKitchenServiceReference.CKServiceSoapClient();
            drpState.DataSource = svc.GetStateList();
            drpState.DataTextField = "StateName";
            drpState.DataValueField = "StateID";
            drpState.DataBind();
        }

        protected void SaveCurrentEdit()
        {
            pageID = Convert.ToInt32(lblClientID.Text);
            DateTime dt = Convert.ToDateTime(txtDOB.Text);

            CharityKitchenServiceReference.CKServiceSoapClient svc = new CharityKitchenServiceReference.CKServiceSoapClient();
            svc.SaveClientEdit(txtFirstName.Text, txtLastName.Text, dt, txtPhoneNumber.Text, txtEmail.Text, txtAddress.Text, Convert.ToInt32(drpState.SelectedItem.Value), txtSuburb.Text, txtPostcode.Text, pageID);
            lblUserMessage.Text = "Client edit was successful";
            System.Threading.Thread.Sleep(2000);
            Server.Transfer("Clients.aspx");
        }
        #endregion methods

        protected void btnDeleteClient_Click(object sender, EventArgs e)
        {
            pageID = Convert.ToInt32(lblClientID.Text);
            CharityKitchenServiceReference.CKServiceSoapClient svc = new CharityKitchenServiceReference.CKServiceSoapClient();
            svc.DeleteClient(pageID);
            Server.Transfer("Clients.aspx");
        }
    }
}