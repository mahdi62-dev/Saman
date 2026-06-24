using System.Data.SqlClient;
using DAL;

namespace Saman.BLL.Enc;

public class EncDate
{
    public void EncChangeDate(int EncID, string EncStartDateNew, string EncEndDateNew,
        string PageDateNew, string CashDateNew, bool IsEncStartDate, bool IsEncEndDate,
        bool IsPageDate, bool IsCashDate, int UserPersID)
    {
        var com = new SqlCommand();
        com.Parameters.AddWithValue("@EncID", EncID);
        com.Parameters.AddWithValue("@EncStartDateNew", EncStartDateNew);
        com.Parameters.AddWithValue("@EncEndDateNew", EncEndDateNew);
        com.Parameters.AddWithValue("@PageDateNew", PageDateNew);
        com.Parameters.AddWithValue("@CashDateNew", CashDateNew);
        com.Parameters.AddWithValue("@IsEncStartDate", IsEncStartDate);
        com.Parameters.AddWithValue("@IsEncEndDate", IsEncEndDate);
        com.Parameters.AddWithValue("@IsPageDate", IsPageDate);
        com.Parameters.AddWithValue("@IsCashDate", IsCashDate);


        com.Parameters.AddWithValue("@UserPersID", UserPersID);

        var dalData = new Data();
        dalData.ExecuteNonQueryBySP(cSet.cnStringA, com, "spEncDate");
    }
}