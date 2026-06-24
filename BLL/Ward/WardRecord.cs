using System.Data;
using System.Data.SqlClient;
using DAL;

namespace Saman.BLL.Ward;

public class WardRecord
{
    public void EncTeamSave(int CRUD, int EncID, object PersID, int RoleID, int UserPersID)
    {
        var com = new SqlCommand();
        com.Parameters.AddWithValue("@UserPersID", UserPersID);
        com.Parameters.AddWithValue("@CRUD", CRUD);
        com.Parameters.AddWithValue("@EncID", EncID);
        com.Parameters.AddWithValue("@PersID", PersID);
        com.Parameters.AddWithValue("@RoleID", RoleID);

        var dalData = new Data();
        dalData.ExecuteNonQueryBySP(cSet.cnStringA, com, "spEncTeamSave");
    }


    public DataTable EncTeamLoad(int EncID)
    {
        var tsql = @"SELECT 
                            EncID, MAX(NurseDay) AS NurseDAy, MAX(NurseEvening) AS NurseEvening , MAX(NurseNight) AS NurseNight
                            FROM
                            (
                            SELECT 
                            EncID
                            ,CASE WHEN RoleID=18 THEN MAX(PersID) ELSE 0 END AS NurseDay
                            ,CASE WHEN RoleID=28 THEN MAX(PersID) ELSE 0 END AS NurseEvening
                            ,CASE WHEN RoleID=19 THEN MAX(PersID) ELSE 0 END AS NurseNight

                              FROM dbo.EncTeam
                              where EncID=@EncID
                              GROUP BY EncID,RoleID
                            ) AS T
                            GROUP BY EncID";
        var com = new SqlCommand();
        com.Parameters.AddWithValue("@EncID", EncID);

        var dalData = new Data();
        return dalData.GetDataTableByText(cSet.cnStringA, com, tsql);
    }
}