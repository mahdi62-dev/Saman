using System.Data;
using System.Data.SqlClient;
using DAL;

namespace Saman.BLL.SAC;

public class SACReport
{
    public void SACReportSave(object RepID, object PersID, bool Permission, object UserPersID)
    {
        var com = new SqlCommand();
        com.Parameters.AddWithValue("@RepID", RepID);
        com.Parameters.AddWithValue("@PersID", PersID);
        com.Parameters.AddWithValue("@Permission", Permission);

        com.Parameters.AddWithValue("@UserPersID", UserPersID);

        var dalData = new Data();
        dalData.ExecuteNonQueryBySP(cSet.cnStringA, com, "spSACUserPermissionReport");
    }

    public DataTable SACReportLoad(int PersID)
    {
        var TSQL = @"SELECT RepID 
                            FROM SamanHIS.dbo.SACUserPermissionReport
                            WHERE PersID = @PersID";

        var com = new SqlCommand();
        com.Parameters.AddWithValue("@PersID", PersID);

        var dalData = new Data();
        return dalData.GetDataTableByText(cSet.cnStringA, com, TSQL);
    }
}