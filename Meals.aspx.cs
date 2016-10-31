using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CharityKitchen
{
    public partial class Meals : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                GetStockList();
                ChangeIngredientUnit();
                GetAllMeals();
                ViewMeal();
            }
        }

        #region vars
        //These vars need to be static or else they'll be reintialised each time an event/postback occurs
        static int MealIndex = 0;
        static int CurrentIndex = 0;
        static int Unit;
        static int IngredientID;
        static int rowIndex;
        static CharityKitchen.CharityKitchenServiceReference.Meal[] AllMeals;
        #endregion vars

        #region events
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            DeleteMeal();
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
            IngredientID = Convert.ToInt32(gvResults.DataKeys[rowIndex].Values[0]);
            DeleteIngredient(IngredientID);
            ViewMealIngredients();
        }

        protected void gvResults_RowDeleted(object sender, GridViewDeletedEventArgs e)
        {
            
        }
        protected void btnAddIngredient_Click(object sender, EventArgs e)
        {
            AddIngredientToList();
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            DisableButtons();
            ClearFields();
            lblMealID.Text = "0";
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            EnableButtons();
            if (lblMealID.Text == "0")
            {
                SaveNewMeal();
                GetAllMeals();
                MealIndex = AllMeals.Length-1;
                ViewMeal();
            }
            else
            {
                UpdateMeal();
                GetAllMeals();
                ViewMeal();
            }
        }

        protected void drpIngredients_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangeIngredientUnit();
        }

        protected void btnFirst_Click(object sender, EventArgs e)
        {
            GetFirst();
        }

        protected void btnLast_Click(object sender, EventArgs e)
        {
            GetLast();
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            GetNext();
        }

        protected void btnPrevious_Click(object sender, EventArgs e)
        {
            GetPrevious();
        }
        #endregion events

        #region methods
        protected void GetStockList()
        {
            CharityKitchenServiceReference.CKServiceSoapClient svc = new CharityKitchenServiceReference.CKServiceSoapClient();
            drpIngredients.DataSource = svc.GetStockList();
            drpIngredients.DataValueField = "StockID";
            drpIngredients.DataTextField = "StockName";
            drpIngredients.DataBind();
        }

        protected void ChangeIngredientUnit()
        {
            CharityKitchenServiceReference.CKServiceSoapClient svc = new CharityKitchenServiceReference.CKServiceSoapClient();
            Unit = svc.ReturnStockUnit(drpIngredients.SelectedItem.Value);
            if (Unit == 1)
            {
                lblUnit.Text = "KG";
            }
            else if (Unit == 2)
            {
                lblUnit.Text = "g";
            }
            else if (Unit == 3)
            {
                lblUnit.Text = "L";
            }
            else if (Unit == 4)
            {
                lblUnit.Text = "mL";
            }
            else
            {
                lblUnit.Text = "Por";
            }
        }

        protected void GetAllMeals()
        {
            CharityKitchenServiceReference.CKServiceSoapClient svc = new CharityKitchenServiceReference.CKServiceSoapClient();
            AllMeals = svc.GetAllMeals();
        }

        protected void ViewMealIngredients()
        {
            int id = AllMeals[MealIndex].ID;

            try
            {
                CharityKitchenServiceReference.CKServiceSoapClient svc = new CharityKitchenServiceReference.CKServiceSoapClient();
                gvResults.DataSource = svc.GetMealLinesByID(id);
                gvResults.DataBind();
            }
            catch
            {
                lblInfo.ForeColor = System.Drawing.Color.Red;
                lblInfo.Text = "This meal has no ingredients set";
            }
        }

        protected void ViewMeal()
        {
            lblMealID.Text = AllMeals[MealIndex].ID.ToString();
            txtName.Text = AllMeals[MealIndex].Name;
            txtPrice.Text = AllMeals[MealIndex].Price.ToString();
            txtDescription.Text = AllMeals[MealIndex].Description.ToString();
            ViewMealIngredients();
            UpdateIndex();
        }

        protected void UpdateIndex()
        {
            CurrentIndex = MealIndex+1;
            lblCurrentIndex.Text = CurrentIndex.ToString();
            lblTotalIndex.Text = AllMeals.Length.ToString();
        }

        protected void GetFirst()
        {
            MealIndex = 0;
            ViewMeal();
        }

        protected void GetLast()
        {
            //int check = AllMeals.Length;
            MealIndex = AllMeals.Length-1;
            ViewMeal();
        }

        protected void GetPrevious()
        {
            if (CurrentIndex != 1)
            {
                MealIndex = MealIndex - 1;
                ViewMeal();
            }
        }

        protected void GetNext()
        {
            if (CurrentIndex != AllMeals.Length)
            {
                MealIndex = MealIndex + 1;
                ViewMeal();
            }
        }

        protected void SaveNewMeal()
        {
            CharityKitchenServiceReference.CKServiceSoapClient svc = new CharityKitchenServiceReference.CKServiceSoapClient();
            svc.SaveMeal(txtName.Text, Convert.ToDecimal(txtPrice.Text), txtDescription.Text);
        }

        protected void UpdateMeal()
        {
            CharityKitchenServiceReference.CKServiceSoapClient svc = new CharityKitchenServiceReference.CKServiceSoapClient();
            svc.UpdateMeal(txtName.Text, Convert.ToDecimal(txtPrice.Text), txtDescription.Text, Convert.ToInt32(lblMealID.Text));
        }

        protected void ClearFields()
        {
            gvResults.DataSource = null;
            gvResults.DataBind();
            txtName.Text = "";
            txtPrice.Text = "";
            txtDescription.Text = "";
        }

        protected void DisableButtons()
        {
            btnAddIngredient.Enabled = false;
            btnFirst.Enabled = false;
            btnLast.Enabled = false;
            btnPrevious.Enabled = false;
            btnNext.Enabled = false;
            btnDelete.Enabled = false;
        }

        protected void EnableButtons()
        {
            btnAddIngredient.Enabled = true;
            btnFirst.Enabled = true;
            btnLast.Enabled = true;
            btnPrevious.Enabled = true;
            btnNext.Enabled = true;
            btnDelete.Enabled = true;
        }

        protected void AddIngredientToList()
        {
            CharityKitchenServiceReference.CKServiceSoapClient svc = new CharityKitchenServiceReference.CKServiceSoapClient();
            svc.AddMealLine(Convert.ToInt32(lblMealID.Text), Convert.ToInt32(drpIngredients.SelectedItem.Value), Convert.ToInt32(txtQuantity.Text), Unit);

            //will refresh the gridview
            ViewMealIngredients();
        }

        protected void DeleteIngredient(int _IngredientID)
        {
            CharityKitchenServiceReference.CKServiceSoapClient svc = new CharityKitchenServiceReference.CKServiceSoapClient();
            svc.DeleteMealLine(_IngredientID);
        }

        protected void DeleteMeal()
        {
            CharityKitchenServiceReference.CKServiceSoapClient svc = new CharityKitchenServiceReference.CKServiceSoapClient();
            svc.DeleteMeal(Convert.ToInt32(lblMealID.Text));
            GetAllMeals();
            if (CurrentIndex != 1)
            {
                GetPrevious();
            }
            else { GetNext(); }
            
        }
        #endregion methods

        protected void btnShowHelp_Click(object sender, EventArgs e)
        {
            txtHelp.Visible = true;
            HelpStrings help = new HelpStrings();
            txtHelp.Text = help.Meals;
        }
    }
}