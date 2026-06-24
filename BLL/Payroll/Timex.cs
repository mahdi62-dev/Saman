using System.Data;
using System.Data.SqlClient;
using DAL;

namespace Saman.BLL.Payroll;

public class Timex
{
    public DataTable TimexGetData(object CRUD, object Year, object Mon, object UserPersID)
    {
        var com = new SqlCommand();
        com.Parameters.Add("@CRUD", CRUD);
        com.Parameters.Add("@Year", Year);
        com.Parameters.Add("@Mon", Mon);
        com.Parameters.Add("@UserPersID", UserPersID);

        var dalData = new Data();
        return dalData.GetDataTableBySP(cSet.cnStringERP, com, "spTimexGetData");
    }

    public void DayOffSave
    (object CRUD, object Year, object Mon, object PersID, object DayOffType, object StarDate, object Endate,
        object val, object UserPersID)
    {
        var com = new SqlCommand();
        com.Parameters.AddWithValue("@CRUD", CRUD);
        com.Parameters.AddWithValue("@Year", Year);
        com.Parameters.AddWithValue("@Mon", Mon);
        com.Parameters.AddWithValue("@PersID", PersID);
        com.Parameters.AddWithValue("@DayOffType", DayOffType);
        com.Parameters.AddWithValue("@StartDate", StarDate);
        com.Parameters.AddWithValue("@EndDate", Endate);
        com.Parameters.AddWithValue("@val", val);
        com.Parameters.AddWithValue("@UserPersID", UserPersID);


        var dalData = new Data();
        dalData.ExecuteNonQueryBySP(cSet.cnStringERP, com, "spTimexDayOffSave");
    }

    public DataTable GetTimexDayOff(object Year, object Mon)
    {
        var tsql = @" SELECT 2 as CRUD,*
                          FROM [SamanERP].[dbo].[vTimexDayOff]
                          where Year=@Year AND Mon=@Mon
                            ORDER BY DayOffID
                        ";
        var com = new SqlCommand();
        com.Parameters.AddWithValue("@Year", Year);
        com.Parameters.AddWithValue("@Mon", Mon);


        var dalData = new Data();
        return dalData.GetDataTableByText(cSet.cnStringERP, com, tsql);
    }
}