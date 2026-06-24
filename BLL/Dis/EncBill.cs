using System.Data;
using System.Data.SqlClient;
using DAL;

namespace Saman.BLL.Dis;

public class EncBill
{
    public DataTable GetEncBillSummary(int EncID)
    {
        var query = @"
                        SELECT *
                          FROM SamanHIS.[dbo].[vEncBillSummary]
                        WHERE EncID=@EncID
                        ";
        var com = new SqlCommand();
        com.Parameters.AddWithValue("@EncID", EncID);

        var dalData = new Data();
        return dalData.GetDataTableByText(cSet.cnStringA, com, query);
    }

    public DataTable GetEncBill(int EncID)
    {
        var query = @"
                        SELECT *,
                        CASE WHEN ot.ServiceCode IS NULL THEN 0 ELSE 1 END AS ServiceStatus
                        FROM vServiceRegFull sr
                        LEFT OUTER JOIN dbo.OprType ot ON ot.ServiceCode=sr.ServiceCode
                        WHERE EncID=@EncID
                        ";
        var com = new SqlCommand();
        com.Parameters.AddWithValue("@EncID", EncID);

        var dalData = new Data();
        return dalData.GetDataTableByText(cSet.cnStringA, com, query);
    }

    public DataTable GetEncBillFull(int EncID)
    {
        var query = @"
                        SELECT *
                        FROM vEncBillFull
                        WHERE EncID=@EncID
                        ";
        var com = new SqlCommand();
        com.Parameters.AddWithValue("@EncID", EncID);

        var dalData = new Data();
        return dalData.GetDataTableByText(cSet.cnStringA, com, query);
    }


    public DataTable EncBillsummarySave(int EncID, int PersID, long SDrAcceptAmount)
    {
        var com = new SqlCommand();
        com.Parameters.AddWithValue("@EncID", EncID);
        com.Parameters.AddWithValue("@SDrAcceptAmount", SDrAcceptAmount);

        com.Parameters.AddWithValue("@PersID", PersID);

        var dalData = new Data();
        return dalData.GetDataTableBySP(cSet.cnStringA, com, "spEncBillSummarySave");
    }
}