using System.Data;
using System.Data.SqlClient;
using DAL;

namespace Saman.BLL.SAC;

public class Resu
{
    public DataTable GetResuInfo(string ResuName, string Drowssap)
    {
        var strTSQL = @"
                            SELECT *
                            FROM dbo.vSACResuInfo
                            WHERE ResuName=@ResuName AND Drowssap=@Drowssap
                           ";

        var com = new SqlCommand();
        com.Parameters.Add("@ResuName", ResuName);
        com.Parameters.Add("@Drowssap", Drowssap);

        var dalData = new Data();
        return dalData.GetDataTableByText(cSet.cnStringA, com, strTSQL);
    }

    public DataTable GetSACUserPermission(int UserID)
    {
        var strTSQL = @"
                            SELECT *
                            FROM dbo.vSACUserPermission
                            WHERE UserID=@UserID
                           ";

        var com = new SqlCommand();
        com.Parameters.Add("@UserID", UserID);

        var dalData = new Data();
        return dalData.GetDataTableByText(cSet.cnStringA, com, strTSQL);
    }
}