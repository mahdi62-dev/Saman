using System.Data;
using System.Data.SqlClient;
using DAL;

namespace Saman.BLL.Dis;

public class Dis
{
    public DataTable DisEnc(int EncID, int EncStatus, int PersID)
    {
        var com = new SqlCommand();
        com.Parameters.AddWithValue("@EncID", EncID);
        com.Parameters.AddWithValue("@EncStatus", EncStatus);
        com.Parameters.AddWithValue("@PersID", PersID);

        var dalData = new Data();
        return dalData.GetDataTableBySP(cSet.cnStringA, com, "spDisEncSave");
    }

    public DataTable DisEncCalc(int EncID, int PersID)
    {
        var com = new SqlCommand();
        com.Parameters.AddWithValue("@EncID", EncID);
        //com.Parameters.AddWithValue("@EncStatus", EncStatus);
        com.Parameters.AddWithValue("@PersID", PersID);

        var dalData = new Data();
        return dalData.GetDataTableBySP(cSet.cnStringA, com, "spDisEncCalcSave");
    }


    public DataTable GetEncOprData(int EncID)
    {
        var TSQL = @"
SELECT 
MAX(cc.Name) AS ChiefComplaint
,ISNULL(SUM(po.OprTime),0) AS TotalOprTime
FROM dbo.Enc e
INNER JOIN dbo.vADR cc ON cc.ID=e.ChiefComplaint
LEFT OUTER JOIN dbo.vPageOpr po ON po.EncID=e.EncID

WHERE cc.ADName='ChiefComplaint'
AND e.EncID=@EncID

GROUP BY e.EncID

            ";

        var com = new SqlCommand();
        com.Parameters.AddWithValue("@EncID", EncID);

        var dalData = new Data();
        return dalData.GetDataTableByText(cSet.cnStringA, com, TSQL);
    }

    public DataTable DisEncEndDateChange(int EncID, int PersID, string EndDate)
    {
        var com = new SqlCommand();
        com.Parameters.AddWithValue("@EncID", EncID);
        com.Parameters.AddWithValue("@EndDate", EndDate);
        com.Parameters.AddWithValue("@PersID", PersID);

        var dalData = new Data();
        return dalData.GetDataTableBySP(cSet.cnStringA, com, "spDisEncEndDateChange");
    }
}