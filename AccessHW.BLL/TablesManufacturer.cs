using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessHW.BLL
{
    public class TablesManufacturer
    {
        public TablesManufacturer(OleDbDataReader data)
        {
            this.intManufacturer = Convert.ToInt32(data["intManufacturerID"]);
            this.strName = data["strName"].ToString();
        }

        public TablesManufacturer(int intManufacturer, string strName)
        {
            this.intManufacturer = intManufacturer;
            this.strName = strName;
        }

        public int intManufacturer { get; set; }
        public string strName { get; set; }
    }
}
