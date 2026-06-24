using System.Data;
using System.Data.SqlClient;
using DAL;

namespace Saman.BLL.Dis;

public class Discount
{
    public void CenterDiscountASave(int EncID, object CenterDiscountAmountA, int PersID)
    {
        var tsql = @"
                        UPDATE dbo.EncBillSummary
                        SET CenterDiscountAmountA=@CenterDiscountAmountA                    
                        WHERE EncID=@EncID


                        UPDATE dbo.EncBillSummary
                        SET RemainAmount = RemainAmount - CenterDiscountAmountA                   
                        WHERE EncID=@EncID

                        UPDATE dbo.EncBillSummary
                        SET PersID = @PersID                  
                        WHERE EncID=@EncID


                        ";

        var com = new SqlCommand();
        com.Parameters.AddWithValue("@EncID", EncID);
        com.Parameters.AddWithValue("@CenterDiscountAmountA", CenterDiscountAmountA);
        com.Parameters.AddWithValue("@PersID", PersID);

        var dalData = new Data();
        dalData.ExecuteNonQueryByText(cSet.cnStringA, com, tsql);
    }


    public DataTable GetEncBillSummary(int EncID)
    {
        var query = @"
                        SELECT TOP (1000) [BillID]
                              ,[EncID]
                              ,[TotalAmount]
                              ,[InsuranceAmount]
                              ,[PayAmount]
                              ,[DiscountAmount]
                              ,[OICAmount]
                              ,[PatientAmount]
                              ,[ResultAmount]
                              ,[RemainAmount]
                              ,[AcceptCostCategory]
                          FROM [EncBill]
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
                        SELECT *
                        FROM vServiceRegFull
                        WHERE EncID=@EncID
                        ";
        var com = new SqlCommand();
        com.Parameters.AddWithValue("@EncID", EncID);

        var dalData = new Data();
        return dalData.GetDataTableByText(cSet.cnStringA, com, query);
    }

    public void EncBillDiscountSave(int EncBillID, long DiscountAmount, int AcceptCostCategory)
    {
        var tsql = @"
                        UPDATE dbo.EncBill
                        SET DiscountAmount=@DiscountAmount
                        , AcceptCostCategory=@AcceptCostCategory
                        WHERE EncBillID=@EncBillID
                        ";

        var com = new SqlCommand();
        com.Parameters.AddWithValue("@EncBillID", EncBillID);
        com.Parameters.AddWithValue("@DiscountAmount", DiscountAmount);
        com.Parameters.AddWithValue("@AcceptCostCategory", AcceptCostCategory);

        var dalData = new Data();
        dalData.ExecuteNonQueryByText(cSet.cnStringA, com, tsql);
    }
}