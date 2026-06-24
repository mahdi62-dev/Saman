using System.Data;
using System.Data.SqlClient;
using DAL;

namespace Saman.BLL.Opr;

public class Page
{
    public void PageOprSave
    (int CRUD, int PageID, object OprServiceID, object OprCategory, object OprCategory2, object ADrID,
        object AnesType, object OprTime,
        object OprStartTime, object OprEndTime, int AnesTime, string AnesStartTime, string AnesEndTime,
        object RecoveryTime, string RecoveryStartTime, string RecoveryEndTime, int AndoTime, double TotalOprK,
        double TotalAnesK, double TotalOprRoomK, int UserPersID)
    {
        var com = new SqlCommand();
        com.Parameters.AddWithValue("@CRUD", CRUD);
        com.Parameters.AddWithValue("@PageID", PageID);
        com.Parameters.AddWithValue("@OprServiceID", OprServiceID);
        com.Parameters.AddWithValue("@OprCategory", OprCategory);
        com.Parameters.AddWithValue("@OprCategory2", OprCategory2);
        com.Parameters.AddWithValue("@ADrID", ADrID);

        com.Parameters.AddWithValue("@AnesType", AnesType);
        com.Parameters.AddWithValue("@OprTime", OprTime);
        com.Parameters.AddWithValue("@OprStartTime", OprStartTime);
        com.Parameters.AddWithValue("@OprEndTime", OprEndTime);
        com.Parameters.AddWithValue("@AnesTime", AnesTime);
        com.Parameters.AddWithValue("@AnesStartTime", AnesStartTime);
        com.Parameters.AddWithValue("@AnesEndTime", AnesEndTime);
        com.Parameters.AddWithValue("@RecoveryTime", RecoveryTime);
        com.Parameters.AddWithValue("@RecoveryStartTime", RecoveryStartTime);
        com.Parameters.AddWithValue("@RecoveryEndTime", RecoveryEndTime);
        com.Parameters.AddWithValue("@AndoTime", AndoTime);

        com.Parameters.AddWithValue("@TotalOprK", TotalOprK);
        com.Parameters.AddWithValue("@TotalAnesK", TotalAnesK);
        com.Parameters.AddWithValue("@TotalOprRoomK", TotalOprRoomK);
        com.Parameters.AddWithValue("@UserPersID", UserPersID);

        var dalData = new Data();
        dalData.ExecuteNonQueryBySP(cSet.cnStringA, com, "spPageOprSave");
    }

    public DataTable PageOprLoad(int PageID)
    {
        var query = @"

SELECT 
2 AS CRUD
,*
FROM PageOpr
WHERE PageID = @PageID
";

        var com = new SqlCommand();
        com.Parameters.AddWithValue("@PageID", PageID);


        var dalData = new Data();
        return dalData.GetDataTableByText(cSet.cnStringA, com, query);
    }
}