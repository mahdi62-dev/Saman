using System.Data;
using System.Data.SqlClient;
using DAL;

namespace Saman.BLL.Cash;

public class Cash
{
    public DataTable GetCashData(long EncID)
    {
        var strTSQL = @"
                        SELECT *
                        FROM  vCash
                        WHERE
                        EncID=@EncID
                        ORDER BY CashDate DESC
                        ";

        var com = new SqlCommand();
        com.Parameters.Add("@EncID", EncID);


        var dalData = new Data();
        return dalData.GetDataTableByText(cSet.cnStringA, com, strTSQL);
    }

    public DataTable GetCashDataPage(long EncID, long PageID)
    {
        var strTSQL = @"
                        SELECT *
                        FROM  vCash
                        WHERE
                        EncID=@EncID AND PageID=@PageID
                        ORDER BY CashDate DESC
                        ";

        var com = new SqlCommand();
        com.Parameters.Add("@EncID", EncID);
        com.Parameters.Add("@PageID", PageID);


        var dalData = new Data();
        return dalData.GetDataTableByText(cSet.cnStringA, com, strTSQL);
    }

    public DataTable GetCashTotal(long EncID)
    {
        var strSQL = @"
                        SELECT 
                        ISNULL(SUM(CAST(PayAmount as bigint)) ,0) AS TotalPayAmount
                        ,ISNULL((SELECT RemainAmount FROM EncBillSummary WHERE EncID=@EncID),0) AS RemainAmount
                        FROM  Cash
                        WHERE EncID=@EncID
                        ";

        var com = new SqlCommand();
        com.Parameters.Add("@EncID", EncID);

        var dalData = new Data();
        return dalData.GetDataTableByText(cSet.cnStringA, com, strSQL);
    }

    public DataTable GetCashDataReport(string Date1, string Date2)
    {
        var strTSQL = @"
                SELECT *
                FROM [dbo].[vCashReport]
                  WHERE CashDate between @Date1 AND @Date2
                  Order by CashDate
                ";

        var com = new SqlCommand();
        com.Parameters.Add("@Date1", Date1);
        com.Parameters.Add("@Date2", Date2);

        var dalData = new Data();
        return dalData.GetDataTableByText(cSet.cnStringA, com, strTSQL);
    }


    public DataTable CashWaitListLoad(object CRUD, object CashType, bool IsOtherDays)
    {
        var com = new SqlCommand();

        com.Parameters.AddWithValue("@CRUD", CRUD);
        com.Parameters.AddWithValue("@CashType", CashType);
        com.Parameters.AddWithValue("@IsOtherDays", IsOtherDays);

        var dalData = new Data();
        return dalData.GetDataTableBySP(cSet.cnStringA, com, "spCashWait");
    }
}