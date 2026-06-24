using System.Data;
using System.Data.SqlClient;
using DAL;

namespace Saman.BLL.Enc;

public class EncServiceControl
{
    public DataTable EncServiceControlGetData(int EncID)
    {
        var tql = "SELECT * FROM EncServiceControl WHERE EncID=@EncID";


        var com = new SqlCommand();
        com.Parameters.Add("@EncID", EncID);

        var dalData = new Data();
        return dalData.GetDataTableByText(cSet.cnStringA, com, tql);
    }


    public void EncServiceControlSave(int EncID, int ControlID, bool Val)
    {
        var com = new SqlCommand();
        com.Parameters.Add("@EncID", EncID);
        com.Parameters.Add("@ControlID", ControlID);
        com.Parameters.Add("@Val", Val);


        var dalData = new Data();
        dalData.ExecuteNonQueryBySP(cSet.cnStringA, com, "spEncServiceControlSave");
    }
}