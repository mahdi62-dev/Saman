using System.Data;
using System.Data.SqlClient;
using DAL;

namespace Saman.BLL.SAC;

public class Permission
{
    public bool HasPermission(int modID, string action)
    {
        var hasPermission = false;
        var filter = "ModID=" + modID + " AND ActionName='" + action + "'";
        if (DAL.GL.ds.Tables["UserPermission"].Select(filter).Length > 0) hasPermission = true;

        return hasPermission;
    }

    public void GetUserPermission(int UserID)
    {
        var query = @"
SELECT [UserID]
      ,[ModID]
      ,[ActionName]
  FROM SamanHIS.[dbo].[vSACPermission]
WHERE UserID=@UserID
";
        var con = new SqlConnection { ConnectionString = cSet.cnString };
        var cmd = new SqlCommand(query)
        {
            Connection = con,
            CommandType = CommandType.Text
        };
        cmd.Parameters.AddRange(new[]
        {
            new SqlParameter("@UserID", UserID)
        });

        var dalData = new Data();
        var dtUserPermission = dalData.GetDataTable(cmd);
        dtUserPermission.TableName = "UserPermission";
        DAL.GL.ds.Tables.Add(dtUserPermission);
    }

    public DataTable SACLoad()
    {
        var tsql = @"
                        SELECT *
                        FROM SamanHIS.dbo.vSACPermission
                        --WHERE ActionID>0
                        ";

        var com = new SqlCommand();
        //com.Parameters.AddWithValue("@RepID", RepID);

        var dalData = new Data();
        return dalData.GetDataTableByText(cSet.cnStringReport, com, tsql);
    }

    public DataTable SACPersLoad(int PersID)
    {
        var TSQL = @"SELECT ActionID 
                            FROM SamanHIS.dbo.vSACUserPermissionFull
                            WHERE PersID = @PersID";

        var com = new SqlCommand();
        com.Parameters.AddWithValue("@PersID", PersID);

        var dalData = new Data();
        return dalData.GetDataTableByText(cSet.cnStringA, com, TSQL);
    }


    public void SACSave(object ActionID, object ResuID, object ModID, bool Permission, object UserPersID)
    {
        var com = new SqlCommand();
        com.Parameters.AddWithValue("@ActionID", ActionID);
        com.Parameters.AddWithValue("@ResuID", ResuID);
        com.Parameters.AddWithValue("@ModID", ModID);
        com.Parameters.AddWithValue("@Permission", Permission);
        com.Parameters.AddWithValue("@UserPersID", UserPersID);


        var dalData = new Data();
        dalData.ExecuteNonQueryBySP(cSet.cnStringA, com, "spSACUserPermission");
    }
}