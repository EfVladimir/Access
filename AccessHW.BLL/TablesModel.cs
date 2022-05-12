using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;

namespace AccessHW.BLL
{
    public class TablesModel
    {
        public TablesModel(OleDbDataReader data)
        {
            this.intModelID = Convert.ToInt32(data["intModelID"]);
            this.strName = data["strName"].ToString();
            this.intManufacturerID = Convert.ToInt32(data["intManufacturerID"]);
            this.intSMCSFamilyID = Convert.ToInt32(data["intSMCSFamilyID"]);
            this.strImage = data["strImage"].ToString();
        }
        public TablesModel(string _intModelID, string _strName, string _intManufID, string _intSMCS, string _strImage)
        {
            this.intModelID = Convert.ToInt32(_intModelID);
            this.strName = _strName;
            this.intManufacturerID = Convert.ToInt32(_intManufID);
            this.intSMCSFamilyID = Convert.ToInt32(_intSMCS);
            this.strImage = _strImage;
        }
        public int intModelID { get; set; }
        public string strName { get; set; }
        public int intManufacturerID { get; set; }
        public int intSMCSFamilyID { get; set; }
        public string strImage { get; set; }
    }
}
