using System;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace Saman.BLL.Storage;

public class Storage
{
    public int StoragePeriodIDLoad
        (int StorageID)
    {
        var tsql = @"
SELECT TOP(1) StoragePeriodID
FROM [SamanERP].[dbo].[InvStoragePeriod]
WHERE IsDisable=0
AND EndDate IS null
AND StorageID=@StorageID
";

        var com = new SqlCommand();
        com.Parameters.AddWithValue("@StorageID", StorageID);


        var dalData = new Data();
        var dt = dalData.GetDataTableByText(cSet.cnStringERP, com, tsql);

        var StoragePeriodID = 0;
        if (dt.Rows.Count > 0) StoragePeriodID = Convert.ToInt32(dt.Rows[0]["StoragePeriodId"]);

        return StoragePeriodID;
    }

    public bool IsCheckStockNum(int StorageID)
    {
        var tsql = @"
                        SELECT IsCheckStockNum
                        FROM SamanERP.dbo.InvStorage
                        WHERE StorageID=@StorageID
                        ";

        var com = new SqlCommand();
        com.Parameters.AddWithValue("@StorageID", StorageID);


        var dalData = new Data();
        var dt = dalData.GetDataTableByText(cSet.cnStringERP, com, tsql);

        var IsCheckStockNum = false;
        if (dt.Rows.Count > 0) IsCheckStockNum = Convert.ToBoolean(dt.Rows[0]["IsCheckStockNum"]);

        return IsCheckStockNum;
    }


    public DataTable GetItemData
        (int StorageID, int StoragePeriodID, object ServiceCategory, object ServiceCode)
    {
        var tsql = @"
SELECT 
ISNULL( SamanERP.dbo.InvStockNum(@StorageID,@StoragePeriodID, @ServiceCategory, @ServiceCode),0) AS StockNum
,ISNULL( SamanERP.dbo.InvBuyAmountLast(@ServiceCategory, @ServiceCode) , 0)  AS BuyAmountLast
,s.Price AS SellAmountLast

FROM SamanHIS.dbo.vService s
WHERE 1=1
AND s.ServiceType=2
AND s.EndDate IS NULL
AND s.ServiceCategory=@ServiceCategory AND s.ServiceCode=@ServiceCode
                            ";

        var con = new SqlConnection { ConnectionString = cSet.cnString };

        var cmd = new SqlCommand(tsql)
        {
            Connection = con,
            CommandType = CommandType.Text
        };
        cmd.Parameters.AddWithValue("@StorageID", StorageID);
        cmd.Parameters.AddWithValue("@StoragePeriodID", StoragePeriodID);
        cmd.Parameters.AddWithValue("@ServiceCategory", ServiceCategory);
        cmd.Parameters.AddWithValue("@ServiceCode", ServiceCode);

        var dalData = new Data();

        return dalData.GetDataTable(cmd);
    }

    public DataTable StorageStockLoad(object StorageID, object StoragePeriodID)
    {
        var tsql = @"SELECT *
FROM [SamanERP].[dbo].[vInvStockFull]
WHERE StorageID=@StorageID And StoragePeriodID=@StoragePeriodID
  ORDER BY serviceName
";

        var con = new SqlConnection { ConnectionString = cSet.cnString };

        var cmd = new SqlCommand(tsql)
        {
            Connection = con,
            CommandType = CommandType.Text
        };
        cmd.Parameters.AddWithValue("@StorageID", StorageID);
        cmd.Parameters.AddWithValue("@StoragePeriodID", StoragePeriodID);

        var dalData = new Data();

        return dalData.GetDataTable(cmd);
    }

    public void InvPageSign(object InvPageID)
    {
        var com = new SqlCommand();
        com.Parameters.AddWithValue("@InvPageID", InvPageID);

        var dalData = new Data();
        dalData.ExecuteNonQueryBySP(cSet.cnStringERP, com, "spInvPageSign");
    }

    public DataTable CardexLoad(object StorageID, object ServiceCode, string date1, string date2)
    {
        var com = new SqlCommand();
        com.Parameters.AddWithValue("@StorageID", StorageID);
        com.Parameters.AddWithValue("@ServiceCode", ServiceCode);
        com.Parameters.AddWithValue("@Date1", date1);
        com.Parameters.AddWithValue("@Date2", date2);

        var dalData = new Data();
        return dalData.GetDataTableBySP(cSet.cnStringERP, com, "spInvCardex");
    }
}