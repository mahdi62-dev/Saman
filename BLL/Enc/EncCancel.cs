using System.Data;
using System.Data.SqlClient;
using DAL;

namespace Saman.BLL.Enc;

public class EncCancel
{
    public DataTable GetEncFinancialData(int EncID)
    {
        var tsql = @"
SELECT ISNULL(SUM(PayAmount),0) AS TotalPayAmount, ISNULL(SUM(DiscountAmount),0) AS TotalDiscountAmount, 0 AS PageDrugCount
FROM dbo.Page p
WHERE EncID=@EncID

UNION ALL

SELECT PayAmount,DiscountAmount, 0 AS PageDrugCount
FROM EncBillsummary
WHERE EncID=@EncID


UNION

SELECT 0 AS PayAmount,0 AS DiscountAmount, COUNT(p.PageID) AS PageDrugCount
FROM dbo.Page p
INNER JOIN dbo.ServiceReg sr ON sr.PageID=p.PageID
INNER JOIN dbo.Service s ON s.ServiceID=sr.ServiceID

WHERE EncId=@EncID 
AND ( (p.ModID IN (5,28) AND p.TotalAmount>0) OR s.ServiceType=2)

                        ";
        var com = new SqlCommand();
        com.Parameters.AddWithValue("@EncID", EncID);


        var dalData = new Data();
        return dalData.GetDataTableByText(cSet.cnStringA, com, tsql);
    }

    public void EncCancelSave(bool IsCancel, int EncID, int PersID)
    {
        var com = new SqlCommand();
        com.Parameters.AddWithValue("@IsCancel", IsCancel);
        com.Parameters.AddWithValue("@EncID", EncID);
        com.Parameters.AddWithValue("@PersID", PersID);

        var dalData = new Data();
        dalData.ExecuteNonQueryBySP(cSet.cnStringA, com, "spEncCancel");
    }
}