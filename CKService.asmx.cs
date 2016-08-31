using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace WebService
{
    /// <summary>
    /// Summary description for CKService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class CKService : System.Web.Services.WebService
    {
        static string CONNECTION_STRING = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|CharityKitchen.accdb;Persist Security Info=True";

        #region init
        [WebMethod]
        public DataTable GetStateList()
        {
            try
            {
                string query = "SELECT StateID, StateName FROM tblStates";
                DataTable StateList = new DataTable("StateList");
                OleDbDataAdapter adapter = new OleDbDataAdapter(query, CONNECTION_STRING);
                adapter.Fill(StateList);
                return StateList;
            }
            catch (Exception e)
            {
               // Server.Transfer("GeneralError.aspx");
                return new DataTable();
            }

        }
        #endregion init
        //string query = "INSERT INTO tblOrders (OrderDate, OrderUser) " + " VALUES (\"{0}\", \"{1}\")";
        //query = string.Format(query, date, user);

        //OleDbConnection dbconn = new OleDbConnection(CONNECTION_STRING);
        //OleDbCommand dbcmd = new OleDbCommand(query, dbconn);
        //dbconn.Open();
        //    dbcmd.ExecuteNonQuery();
        //    dbconn.Close();
        #region clients
        [WebMethod]
        public void AddNewClient(string FirstName, string LastName, DateTime DOB, string PhoneNumber, string Email, string Address, int State, string Suburb, string Postcode)
        {
            try
            {
                string query = "INSERT INTO tblClients(ClientFirstName, ClientLastName, ClientDOB, ClientPhoneNumber, ClientEmail, ClientAddress, ClientState, ClientSuburb, ClientPostcode) " + " VALUES (\"{0}\", \"{1}\", \"{2}\", \"{3}\", \"{4}\", \"{5}\", \"{6}\", \"{7}\", \"{8}\")";
                query = string.Format(query, FirstName, LastName, DOB, PhoneNumber, Email, Address, State, Suburb, Postcode);
                OleDbConnection dbConn = new OleDbConnection(CONNECTION_STRING);
                dbConn.Open();
                OleDbCommand dbCmd = new OleDbCommand(query, dbConn);
                dbCmd.ExecuteNonQuery();
                dbConn.Close();
            }
            catch
            {
                //Server.Transfer("GeneralError.aspx");
            }
        }

       [WebMethod]
        public DataTable GetClientList()
        {
            try
            {
                DataTable ClientList = new DataTable("ClientList");
                OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT ClientID, ClientFirstName, ClientLastName, ClientDOB, ClientPhoneNumber FROM tblClients ORDER BY ClientID DESC", CONNECTION_STRING);
                adapter.Fill(ClientList);
                return ClientList;
            }
            catch
            {
               // Server.Transfer("GeneralError.aspx");
                return new DataTable();
            }
        }

        [WebMethod]
        public bool ValidateClientID(int _clientID)
        {
            try
            {
                OleDbConnection dbConn = new OleDbConnection(CONNECTION_STRING);
                dbConn.Open();
                string Query = "SELECT * FROM tblClients WHERE ClientID={0}";
                Query = string.Format(Query, _clientID);
                OleDbCommand dbCmd = new OleDbCommand(Query, dbConn);
                OleDbDataReader reader = dbCmd.ExecuteReader();
                reader.Read();
                if (reader["ClientID"] is DBNull)
                {
                    dbConn.Close();
                    return false;
                }
                else
                {
                    dbConn.Close();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        [WebMethod]
        public Object[] GetClientInfo(int _clientID)
        {
            Object[] _ClientInfo = new Object[10];
            try
            {                
                OleDbConnection dbConn = new OleDbConnection(CONNECTION_STRING);
                dbConn.Open();
                string Query = "SELECT * FROM tblClients WHERE ClientID={0}";
                Query = string.Format(Query, _clientID);
                OleDbCommand dbCmd = new OleDbCommand(Query, dbConn);
                OleDbDataReader reader = dbCmd.ExecuteReader();
                while (reader.Read())
                {
                    _ClientInfo[0] = (((int)reader["ClientID"]));
                    _ClientInfo[1] = (reader["ClientFirstName"].ToString());
                    _ClientInfo[2] = (reader["ClientLastName"].ToString());
                    _ClientInfo[3] = ((DateTime)reader["ClientDOB"]);
                    _ClientInfo[4] = (reader["ClientPhoneNumber"].ToString());
                    _ClientInfo[5] = (reader["ClientEmail"].ToString());
                    _ClientInfo[6] = (reader["ClientAddress"].ToString());
                    _ClientInfo[7] = ((int)reader["ClientState"]);
                    _ClientInfo[8] = (reader["ClientSuburb"].ToString());
                    _ClientInfo[9] = (reader["ClientPostcode"].ToString());
                }
                dbConn.Close();
                return _ClientInfo;
            }
            catch
            {
                //Server.Transfer("GeneralError.aspx");
                return _ClientInfo;
            }
        }

        [WebMethod]
        public void SaveClientEdit(string FirstName, string LastName, DateTime DOB, string Phone, string Email, string Address, int State , string Suburb, string Postcode, int ID)
        {
            // string query = "UPDATE tblClients SET ClientFirstName=\"{0}\", ClientLastName=\"{1}\", ClientPhoneNumber=\"{2}\", ClientEmail=\"{3}\", ClientAddress=\"{4}\", ClientState={5}, ClientSuburb=\"{6}\", ClientPostcode=\"{7}\" WHERE ClientID={8}";

            // query = string.Format(query, FirstName, LastName, PhoneNumber, Email, Address, State, Suburb, Postcode, ID);
            string query = "UPDATE tblClients SET ClientFirstName = @fn, ClientLastName = @ln, ClientDOB = @dob, ClientPhoneNumber = @ph, ClientEmail = @em, ClientAddress = @ad, ClientState = @state, ClientSuburb = @sub, ClientPostcode = @pos WHERE ClientID = @id";
            OleDbConnection dbConn = new OleDbConnection(CONNECTION_STRING);
                dbConn.Open();
                OleDbCommand dbCmd = new OleDbCommand(query, dbConn);

                dbCmd.Parameters.AddWithValue("@fn", FirstName);
                dbCmd.Parameters.AddWithValue("@ln", LastName);
                dbCmd.Parameters.AddWithValue("@dob", DOB);
                dbCmd.Parameters.AddWithValue("@ph", Phone);
                dbCmd.Parameters.AddWithValue("@em", Email);
                dbCmd.Parameters.AddWithValue("@ad", Address);
                dbCmd.Parameters.AddWithValue("@state", State);
                dbCmd.Parameters.AddWithValue("@sub", Suburb);
                dbCmd.Parameters.AddWithValue("@pos", Postcode);
                dbCmd.Parameters.AddWithValue("@id", ID);
            
                
                dbCmd.ExecuteNonQuery();
                dbConn.Close();
        }

        [WebMethod]
        public void DeleteClient(int ID)
        {
            string query = "DELETE FROM tblClients WHERE ClientID = @id";
            OleDbConnection dbConn = new OleDbConnection(CONNECTION_STRING);
            dbConn.Open();
            OleDbCommand dbCmd = new OleDbCommand(query, dbConn);
            dbCmd.Parameters.AddWithValue("@id", ID);
            dbCmd.ExecuteNonQuery();
            dbConn.Close();
        }

        [WebMethod]
        public DataTable SearchClient(string FirstName, string LastName)
        {
            string query = "SELECT ClientID, ClientFirstName, ClientLastName, ClientDOB, ClientPhoneNumber FROM tblClients WHERE ClientFirstName=\"{0}\" AND ClientLastName=\"{1}\"";
            query = string.Format(query, FirstName, LastName);
            DataTable SearchResult = new DataTable("SearchList");
            OleDbDataAdapter adapter = new OleDbDataAdapter(query, CONNECTION_STRING);
            adapter.Fill(SearchResult);
            return SearchResult;
            //OleDbConnection dbConn = new OleDbConnection(CONNECTION_STRING);
            //dbConn.Open();
            //OleDbCommand dbCmd = new OleDbCommand(query, dbConn);
            //dbCmd.Parameters.AddWithValue("@fn", FirstName);
            //dbCmd.Parameters.AddWithValue("@ln", LastName);
            //dbCmd.ExecuteNonQuery();
            //dbConn.Close();
        }
        #endregion clients

        #region stock
        [WebMethod]
        public DataTable GetMeasurementUnits()
        {
            try
            {
                string query = "SELECT MeasurementUnitID, Shorthand FROM tblMeasurementUnits";
                DataTable UnitList = new DataTable("UnitList");
                OleDbDataAdapter adapter = new OleDbDataAdapter(query, CONNECTION_STRING);
                adapter.Fill(UnitList);
                return UnitList;
            }
            catch (Exception e)
            {
                // Server.Transfer("GeneralError.aspx");
                return new DataTable();
            }
        }

        [WebMethod]
        public DataTable GetStockList()
        {
            try
            {
                DataTable StockList = new DataTable("StockList");
                OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT StockID, StockName, StockQuantity, StockDescription FROM tblStock ORDER BY StockID DESC", CONNECTION_STRING);
                adapter.Fill(StockList);
                return StockList;
            }
            catch
            {
                // Server.Transfer("GeneralError.aspx");
                return new DataTable();
            }
        }

        [WebMethod]
        public void AddNewStock(string StockName, int StockQuantity, int Unit, string Desc)
        {
            try
            {
                string query = "INSERT INTO tblStock(StockName, StockQuantity, StockMeasurementUnit, StockDescription) " + " VALUES (\"{0}\", {1}, \"{2}\", \"{3}\")";
                query = string.Format(query, StockName, StockQuantity, Unit, Desc);
                OleDbConnection dbConn = new OleDbConnection(CONNECTION_STRING);
                dbConn.Open();
                OleDbCommand dbCmd = new OleDbCommand(query, dbConn);
                dbCmd.ExecuteNonQuery();
                dbConn.Close();
            }
            catch
            {
                //Server.Transfer("GeneralError.aspx");
            }
        }

        [WebMethod]
        public void QuickEditStockQty(int qty, int id)
        {
            string query = "UPDATE tblStock SET StockQuantity = @qty WHERE StockID = @id";
            OleDbConnection dbConn = new OleDbConnection(CONNECTION_STRING);
            dbConn.Open();
            OleDbCommand dbCmd = new OleDbCommand(query, dbConn);
            dbCmd.Parameters.AddWithValue("@qty", qty);
            dbCmd.Parameters.AddWithValue("@id", id);
            dbCmd.ExecuteNonQuery();
            dbConn.Close();
        }

        [WebMethod]
        public DataTable SearchStock(string StockName)
        {
            string query = "SELECT StockID, StockName, StockQuantity, StockDescription FROM tblStock WHERE StockName=\"{0}\"";
            query = string.Format(query, StockName);
            DataTable SearchResult = new DataTable("SearchList");
            OleDbDataAdapter adapter = new OleDbDataAdapter(query, CONNECTION_STRING);
            adapter.Fill(SearchResult);
            return SearchResult;
        }

        [WebMethod]
        public bool ValidateStockID(int _stockID)
        {
            try
            {
                OleDbConnection dbConn = new OleDbConnection(CONNECTION_STRING);
                dbConn.Open();
                string Query = "SELECT * FROM tblStock WHERE StockID={0}";
                Query = string.Format(Query, _stockID);
                OleDbCommand dbCmd = new OleDbCommand(Query, dbConn);
                OleDbDataReader reader = dbCmd.ExecuteReader();
                reader.Read();
                if (reader["StockID"] is DBNull)
                {
                    dbConn.Close();
                    return false;
                }
                else
                {
                    dbConn.Close();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        [WebMethod]
        public Object[] GetStockInfo(int _stockID)
        {
            Object[] stockInfo = new Object[4];
            try
            {
                OleDbConnection dbConn = new OleDbConnection(CONNECTION_STRING);
                dbConn.Open();
                string Query = "SELECT * FROM tblStock WHERE StockID={0}";
                Query = string.Format(Query, _stockID);
                OleDbCommand dbCmd = new OleDbCommand(Query, dbConn);
                OleDbDataReader reader = dbCmd.ExecuteReader();
                while (reader.Read())
                {
                    stockInfo[0] = reader["StockName"].ToString();
                    stockInfo[1] = (int)reader["StockQuantity"];
                    stockInfo[2] = (int)reader["StockMeasurementUnit"];
                    stockInfo[3] = reader["StockDescription"].ToString();
                }
                dbConn.Close();
                return stockInfo;
            }
            catch
            {
                //Server.Transfer("GeneralError.aspx");
                return stockInfo;
            }
        }

        [WebMethod]
        public void DeleteStock(int ID)
        {
            string query = "DELETE FROM tblStock WHERE StockID = @id";
            OleDbConnection dbConn = new OleDbConnection(CONNECTION_STRING);
            dbConn.Open();
            OleDbCommand dbCmd = new OleDbCommand(query, dbConn);
            dbCmd.Parameters.AddWithValue("@id", ID);
            dbCmd.ExecuteNonQuery();
            dbConn.Close();
        }

        [WebMethod]
        public void SaveStockEdit(string _name, int _qty, int _unitType, string _description, int _id)
        {         
            string query = "UPDATE tblStock SET StockName = @name, StockQuantity = @qty, StockMeasurementUnit = @mu, StockDescription = @desc WHERE StockID = @id";
            OleDbConnection dbConn = new OleDbConnection(CONNECTION_STRING);
            dbConn.Open();
            OleDbCommand dbCmd = new OleDbCommand(query, dbConn);

            dbCmd.Parameters.AddWithValue("@name", _name);
            dbCmd.Parameters.AddWithValue("@qty", _qty);
            dbCmd.Parameters.AddWithValue("@mu", _unitType);
            dbCmd.Parameters.AddWithValue("@desc", _description);
            dbCmd.Parameters.AddWithValue("@id", _id);
    
            dbCmd.ExecuteNonQuery();
            dbConn.Close();
        }
        #endregion stock

        #region meals
        [WebMethod]
        public int ReturnStockUnit(string _id)
        {
            int Unit = 1;

            string query = "SELECT StockMeasurementUnit FROM tblStock WHERE StockID=@id";
            OleDbConnection dbConn = new OleDbConnection(CONNECTION_STRING);
            dbConn.Open();
            OleDbCommand dbCmd = new OleDbCommand(query, dbConn);

            dbCmd.Parameters.AddWithValue("@id", _id);
            OleDbDataReader reader = dbCmd.ExecuteReader();
            while (reader.Read())
            {
                Unit = (int)reader["StockMeasurementUnit"];
            }
                dbConn.Close();

            return Unit;
        }

        [WebMethod]
        public DataTable GetMealNames()
        {
                try
                {
                    DataTable MealNames = new DataTable("MealNames");
                    OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT MealID, MealName FROM tblMeals ORDER BY MealName", CONNECTION_STRING);
                    adapter.Fill(MealNames);
                    return MealNames;
                }
                catch
                {
                    // Server.Transfer("GeneralError.aspx");
                    return new DataTable();
                }            
        }

        [WebMethod]
        public List<Meal> GetAllMeals()
        {
            List<Meal> AllMeals = new List<Meal>();

            string query = "SELECT * FROM tblMeals";
            OleDbConnection dbConn = new OleDbConnection(CONNECTION_STRING);
            dbConn.Open();
            OleDbCommand dbCmd = new OleDbCommand(query, dbConn);
            OleDbDataReader reader = dbCmd.ExecuteReader();

            while (reader.Read())
            {
                Meal meal = new Meal();

                meal.ID = (int)reader["MealID"];
                meal.Name = reader["MealName"].ToString();
                meal.Price = (decimal)reader["MealPrice"];
                meal.Description = reader["MealDescription"].ToString();

                AllMeals.Add(meal);
            }

            dbConn.Close();
            return AllMeals;
        }

        [WebMethod]
        public DataTable GetMealLinesByID(int id)//Needs to run the query from Access in order for tables to join
        {           
            try
            {
            // string query = "SELECT * FROM tblMealLines WHERE MealID=" + id;
                string query = "SELECT tblMealLines.MealLineID, tblMeals.MealID, tblStock.StockName, tblMealLines.Quantity, tblMeasurementUnits.Shorthand FROM tblMeasurementUnits INNER JOIN (tblStock INNER JOIN(tblMeals INNER JOIN tblMealLines ON tblMeals.MealID = tblMealLines.MealID) ON tblStock.StockID = tblMealLines.StockID) ON(tblMeasurementUnits.MeasurementUnitID = tblMealLines.MeasurementUnit) AND(tblMeasurementUnits.MeasurementUnitID = tblStock.StockMeasurementUnit) WHERE tblMeals.MealID=" + id;

                DataTable MealLines = new DataTable ("MealTable");
                MealLines.TableName = "MealLineList";
                OleDbDataAdapter adapter = new OleDbDataAdapter(query, CONNECTION_STRING);
                adapter.Fill(MealLines);
                return MealLines;
            }
            catch (Exception e)
            {
                // Server.Transfer("GeneralError.aspx");
                return new DataTable();
            }
        }

        [WebMethod]
        public void SaveMeal(string _mealName, decimal _price, string _mealDesc)
        {
            try
            {
                string query = "INSERT INTO tblMeals(MealName, MealPrice, MealDescription) " + " VALUES (\"{0}\", {1}, \"{2}\")";
                query = string.Format(query, _mealName, _price, _mealDesc);
                OleDbConnection dbConn = new OleDbConnection(CONNECTION_STRING);
                dbConn.Open();
                OleDbCommand dbCmd = new OleDbCommand(query, dbConn);
                dbCmd.ExecuteNonQuery();
                dbConn.Close();
            }
            catch
            {

            }
        }

        [WebMethod]
        public void UpdateMeal(string _mealName, decimal _price, string _mealDesc, int _id)
        {
            try
            {
                string query = "UPDATE tblMeals SET MealName = @name, MealPrice = @price, MealDescription = @desc WHERE MealID = @id";
                OleDbConnection dbConn = new OleDbConnection(CONNECTION_STRING);
                dbConn.Open();
                OleDbCommand dbCmd = new OleDbCommand(query, dbConn);

                dbCmd.Parameters.AddWithValue("@name", _mealName);
                dbCmd.Parameters.AddWithValue("@price", _price);
                dbCmd.Parameters.AddWithValue("@desc", _mealDesc);
                dbCmd.Parameters.AddWithValue("@id", _id);

                dbCmd.ExecuteNonQuery();
                dbConn.Close();
            }
            catch
            {

            }
        }

        [WebMethod]
        public void AddMealLine(int _mealID, int _stockID, int _qty, int _unit)
        {
            try
            {
                string query = "INSERT INTO tblMealLines(MealID, StockID, Quantity, MeasurementUnit) " + " VALUES ({0}, {1}, {2}, {3})";
                query = string.Format(query, _mealID, _stockID, _qty, _unit);
                OleDbConnection dbConn = new OleDbConnection(CONNECTION_STRING);
                dbConn.Open();
                OleDbCommand dbCmd = new OleDbCommand(query, dbConn);
                dbCmd.ExecuteNonQuery();
                dbConn.Close();
            }
            catch
            { }
        }

        [WebMethod]
        public void DeleteMealLine(int id)
        {
            try
            {
                string query = "DELETE FROM tblMealLines WHERE MealLineID=" + id;
                query = string.Format(query, id);
                OleDbConnection dbConn = new OleDbConnection(CONNECTION_STRING);
                dbConn.Open();
                OleDbCommand dbCmd = new OleDbCommand(query, dbConn);
                dbCmd.ExecuteNonQuery();
                dbConn.Close();
            }
            catch { }
        }

        [WebMethod]
        public void DeleteMeal(int id)
        {
            try
            {
                string query = "DELETE FROM tblMeals WHERE MealID=" + id;
                query = string.Format(query, id);
                OleDbConnection dbConn = new OleDbConnection(CONNECTION_STRING);
                dbConn.Open();
                OleDbCommand dbCmd = new OleDbCommand(query, dbConn);
                dbCmd.ExecuteNonQuery();
                dbConn.Close();
            }
            catch { }
        }
        #endregion meals

        #region orders
        [WebMethod]
        public string GetMealPrice(int id)
        {
            string price="";
            try
            {
                string query = "SELECT MealPrice from tblMeals WHERE MealID=" + id;
                query = string.Format(query, id);
                OleDbConnection dbConn = new OleDbConnection(CONNECTION_STRING);
                dbConn.Open();
                OleDbCommand dbCmd = new OleDbCommand(query, dbConn);

                OleDbDataReader reader = dbCmd.ExecuteReader();
                while (reader.Read())
                {
                    price = reader["MealPrice"].ToString();
                }
            }
            catch
            {
                price = "Unknown Value";
            }

            return price;
        }

        [WebMethod]
        public DataTable GetAllOrders()
        {
            try
            {
                string query = "SELECT * FROM tblOrders";
                DataTable OrderList = new DataTable("OrderList");
                OleDbDataAdapter adapter = new OleDbDataAdapter(query, CONNECTION_STRING);
                adapter.Fill(OrderList);
                return OrderList;
            }
            catch (Exception e)
            {
                // Server.Transfer("GeneralError.aspx");
                return new DataTable();
            }
        }

        [WebMethod]
        public void AddOrderLine(int _orderID, int _mealID, decimal _mealPrice)
        {
            string query = "INSERT INTO tblOrderLines(OrderID, MealID, MealPrice) " + " VALUES (\"{0}\", \"{1}\", \"{2}\")";
            query = string.Format(query, _orderID, _mealID, _mealPrice);
            OleDbConnection dbConn = new OleDbConnection(CONNECTION_STRING);
            dbConn.Open();
            OleDbCommand dbCmd = new OleDbCommand(query, dbConn);
            dbCmd.ExecuteNonQuery();
            dbConn.Close();
        }


        [WebMethod]
        public void AddOrder(DateTime _date, int _clientID, decimal _total)
        {
            string query = "INSERT INTO tblOrders(OrderDate, ClientID, OrderTotal) " + " VALUES (\"{0}\", \"{1}\", \"{2}\")";
            query = string.Format(query, _date, _clientID, _total);
            OleDbConnection dbConn = new OleDbConnection(CONNECTION_STRING);
            dbConn.Open();
            OleDbCommand dbCmd = new OleDbCommand(query, dbConn);
            dbCmd.ExecuteNonQuery();
            dbConn.Close();
        }

        [WebMethod]
        public string GetLastOrderID()
        {
            string query = "SELECT TOP 1 OrderID FROM tblOrders ORDER BY OrderID DESC";
            string OrderID="";
            OleDbConnection dbConn = new OleDbConnection(CONNECTION_STRING);
            dbConn.Open();
            OleDbCommand dbCmd = new OleDbCommand(query, dbConn);

            OleDbDataReader reader = dbCmd.ExecuteReader();
            while (reader.Read())
            {
                OrderID = reader["OrderID"].ToString();
            }

            dbConn.Close();
            return OrderID;           
        }

        [WebMethod]//WON'T DELETE UNLESS YOU DELETE THE RELATED ORDERLINES
        public void DeleteOrder(int _id)
        {
            string query = "DELETE FROM tblOrders WHERE OrderID=" + _id;
            query = string.Format(query, _id);
            OleDbConnection dbConn = new OleDbConnection(CONNECTION_STRING);
            dbConn.Open();
            OleDbCommand dbCmd = new OleDbCommand(query, dbConn);
            dbCmd.ExecuteNonQuery();
            dbConn.Close();
        }

        [WebMethod]
        public void DeleteOrderLines(int _id)
        {
            string query = "DELETE FROM tblOrderLines WHERE OrderID=" + _id;
            query = string.Format(query, _id);
            OleDbConnection dbConn = new OleDbConnection(CONNECTION_STRING);
            dbConn.Open();
            OleDbCommand dbCmd = new OleDbCommand(query, dbConn);
            dbCmd.ExecuteNonQuery();
            dbConn.Close();
        }
        #endregion orders
    }
}

