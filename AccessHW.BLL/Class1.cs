using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.OleDb;

namespace AccessHW.BLL
{
    public class Class1
    {
        private static string connectionString = ConfigurationManager
        .ConnectionStrings["AccessHW.BLL.Properties.Settings.ADOConnectionString"]
        .ConnectionString;
        private static OleDbConnection con = null;
        public List<TablesManufacturer> dataManuf = GetManufacturer();
        public List<TablesModel> dataModel = GetModel();
        public Class1(string conStr)
        {
            connectionString = conStr;
        }
        public static List<TablesManufacturer> GetManufacturer()
        {
            con = new OleDbConnection(connectionString);
            con.Open();
                List<TablesManufacturer> manufacturers = new List<TablesManufacturer>();
                OleDbCommand cmd = new OleDbCommand();
                cmd.Connection = con;
                cmd.CommandText = "select * from TablesManufacturer";
                var data = cmd.ExecuteReader();
                while (data.Read())
                {
                    manufacturers.Add(new TablesManufacturer(data));
                }
            con.Close();
                return manufacturers;
        }
        public static List<TablesModel> GetModel()
        {
            con = new OleDbConnection(connectionString);
            con.Open();
            List<TablesModel> model = new List<TablesModel>();
            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = con;
            cmd.CommandText = "select * from TablesModel";
            var data = cmd.ExecuteReader();
            while (data.Read())
            {
                model.Add(new TablesModel(data));
            }
            con.Close();
            return model;
        }
        public bool Connect(out string message)
        {
            try
            {
                con = new OleDbConnection(connectionString);
                con.Open();
                message = "Подключение открыто";
                return true;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return false;
            }
        }
        public bool Disconnect(out string message)
        {
            try
            {
                if (con.State != System.Data.ConnectionState.Closed) con.Close();
                message = "Соединение закрыто";
                return true;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return false;
            }
        }
        public void GetCountManufacturer(out string message)
        {
            try
            {
                OleDbCommand cmd = new OleDbCommand();
                message = dataManuf.Count().ToString();
            }
            catch(Exception ex)
            {
                message = ex.Message;
            }
            
        }
        public bool AddDataManufacturer(out string message, TablesManufacturer manufacturer)
        {
            try
            {
                OleDbCommand cmd = new OleDbCommand();
                cmd.Connection = con;
                cmd.CommandText = $"insert into TablesManufacturer" +
                            $"(intManufacturerID, strName)" +
                            $"values('{manufacturer.intManufacturer}', " +
                            $"'{manufacturer.strName}')";
                int add = cmd.ExecuteNonQuery();
                message = "Успешно добавлено";
                dataManuf = GetManufacturer();
                return true;
            }
            catch(Exception ex)
            {
                message = ex.Message;
                return false;
            }
        }
        public bool RemoveDataManufacturer(out string message, int id)
        {
            try
            {
                OleDbCommand cmd = new OleDbCommand();
                cmd.Connection = con;
                cmd.CommandText = $"DELETE FROM TablesManufacturer where intManufacturerID = {id}";
                cmd.ExecuteNonQuery();
                message = "Успешно удалено";
                dataManuf = GetManufacturer();
                return true;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return false;
            }
        }
        public void GetCountModel(out string message)
        {
            try
            {
                OleDbCommand cmd = new OleDbCommand();
                message = dataModel.Count().ToString();
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }

        }
        public bool AddDataModel(out string message, TablesModel model)
        {
            try
            {
                OleDbCommand cmd = new OleDbCommand();
                cmd.Connection = con;
                cmd.CommandText = $"insert into TablesModel" +
                            $"(intModelID, strName, intManufacturerID, intSMCSFamilyID, strImage)" +
                            $"values('{model.intModelID}', " +
                            $"'{model.strName}'," +
                            $"'{model.intManufacturerID}'," +
                            $"'{model.intSMCSFamilyID}'," +
                            $"'{model.strImage}')";
                int add = cmd.ExecuteNonQuery();
                message = "Успешно добавлено";
                dataModel = GetModel();
                return true;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return false;
            }
        }
        public bool RemoveDataModel(out string message, int id)
        {
            try
            {
                OleDbCommand cmd = new OleDbCommand();
                cmd.Connection = con;
                cmd.CommandText = $"DELETE FROM TablesModel where intModelID = {id}";
                cmd.ExecuteNonQuery();
                message = "Успешно удалено";
                dataModel = GetModel();
                return true;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return false;
            }
        }
    }
}

