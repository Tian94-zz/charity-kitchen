using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CharityKitchen
{
    public partial class Accounts : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (this.GetAccessLevel("Master") != AccessLevel.Write)
                {
                    Response.Redirect("AccessDenied.aspx");
                }
                LoadAllUserAccounts();
                LoadRoles();
                LoadAccessLevels();
            }
        }

        #region vars
        int RowIndex;
        #endregion vars

        #region events
        protected void gvResult_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                RowIndex = Convert.ToInt32(e.CommandArgument);
            }
            else
            {
                RowIndex = Convert.ToInt32(e.CommandArgument);
                LoadUserInfo();
                LoadUserRoleInfo();
            }
        }

        protected void gvResult_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (Convert.ToInt32(gvResult.Rows[RowIndex].Cells[3].Text) != 1)
            {
                CharityKitchenServiceReference.CKServiceSoapClient svc = new CharityKitchenServiceReference.CKServiceSoapClient();
                svc.DeleteUserRole(Convert.ToInt32(gvResult.Rows[RowIndex].Cells[3].Text));
                svc.DeleteUser(Convert.ToInt32(gvResult.Rows[RowIndex].Cells[2].Text));
            }
            else
            {
                lblInfo.ForeColor = System.Drawing.Color.Red;
                lblInfo.Text = "*You cannot delete a Master";
            }

            LoadAllUserAccounts();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (lblID.Text == "0")
            {
                AddUser();
                AddRoleCombo();
                LoadAllUserAccounts();
            }
            else
            {
                //update query
            }
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {

        }
        #endregion events

        #region methods
        protected void AddUser()
        {
            CharityKitchenServiceReference.CKServiceSoapClient svc = new CharityKitchenServiceReference.CKServiceSoapClient();
            svc.AddUser(txtUsername.Text, txtFirstName.Text, txtLastName.Text, txtPassword.Text);            
        }

        protected void AddRoleCombo()
        {
            CharityKitchenServiceReference.CKServiceSoapClient svc = new CharityKitchenServiceReference.CKServiceSoapClient();
            int id = svc.GetLastUserID();
            svc.AddRoleCombo(id, Convert.ToInt32(drpRoles.SelectedItem.Value), Convert.ToInt32(drpAccessLevel.SelectedItem.Value));
        }

        protected void LoadRoles()
        {
            CharityKitchenServiceReference.CKServiceSoapClient svc = new CharityKitchenServiceReference.CKServiceSoapClient();
            drpRoles.DataSource = svc.GetRoles();
            drpRoles.DataTextField = "RoleDescription";
            drpRoles.DataValueField = "RoleID";
            drpRoles.DataBind();
        }

        protected void LoadAccessLevels()
        {
            drpAccessLevel.Items.Insert(0, new ListItem("2", "2"));
            drpAccessLevel.Items.Insert(0, new ListItem("1", "1"));
            drpAccessLevel.Items.Insert(0, new ListItem("0", "0"));
        }
        protected void LoadAllUserAccounts()
        {
            CharityKitchenServiceReference.CKServiceSoapClient svc = new CharityKitchenServiceReference.CKServiceSoapClient();
            gvResult.DataSource = svc.GetAllUserAccounts();
            gvResult.DataBind();
        }

        protected void LoadUserInfo()
        {
            int id = Convert.ToInt32(gvResult.Rows[RowIndex].Cells[2].Text);
            CharityKitchenServiceReference.CKServiceSoapClient svc = new CharityKitchenServiceReference.CKServiceSoapClient();

            lblID.Text = id.ToString();
            txtUsername.Text = svc.GetUserInfo(id)[1].ToString();
            txtFirstName.Text = svc.GetUserInfo(id)[2].ToString();
            txtLastName.Text = svc.GetUserInfo(id)[3].ToString();
            txtPassword.Text = svc.GetUserInfo(id)[4].ToString();
        }

        protected void LoadUserRoleInfo()
        {
            int id = Convert.ToInt32(gvResult.Rows[RowIndex].Cells[2].Text);
            CharityKitchenServiceReference.CKServiceSoapClient svc = new CharityKitchenServiceReference.CKServiceSoapClient();

            drpRoles.SelectedIndex = (int)svc.GetRoleCombo(id)[0]-1;
            drpAccessLevel.SelectedIndex = (int)svc.GetRoleCombo(id)[1];
        }
        #endregion methods

        protected void btnShowHelp_Click(object sender, EventArgs e)
        {
            txtHelp.Visible = true;
            HelpStrings help = new HelpStrings();
            txtHelp.Text = help.Accounts;
        }
    }
}