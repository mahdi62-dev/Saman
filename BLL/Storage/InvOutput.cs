using System;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace Saman.BLL.Storage;

public class InvOutput
{
    public DataTable PageDataLoad
        (int StorageID, int InvPageCode)
    {
        var tsql = @"
                        SELECT *
                        FROM [SamanERP].[dbo].[InvPage]
                        WHERE 1=1
                        AND StorageID=@StorageID
                        AND InvPageCode=@InvPageCode AND InvType=2
                        ";

        var com = new SqlCommand();
        com.Parameters.AddWithValue("@StorageID", StorageID);
        com.Parameters.AddWithValue("@InvPageCode", InvPageCode);


        var dalData = new Data();
        return dalData.GetDataTableByText(cSet.cnStringERP, com, tsql);
    }


    public DataTable InvPageSave
    (object InvPageID, object InvCategoryType, object StorageID,
        object StoragePeriodID, object OrderCode, object Date2, object TransfereeID, object DiscountAmount2,
        object OtherAmount, object DeliverPersID, object DestDepID, object Description, object UserPersID,
        object IsAuto)

    {
        var com = new SqlCommand();
        com.Parameters.AddWithValue("@CRUD", 0);
        com.Parameters.AddWithValue("@InvPageID", InvPageID);
        com.Parameters.AddWithValue("@InvType", 2);
        com.Parameters.AddWithValue("@InvCategoryType", InvCategoryType);
        com.Parameters.AddWithValue("@StorageID", StorageID);
        com.Parameters.AddWithValue("@StoragePeriodID", StoragePeriodID);
        com.Parameters.AddWithValue("@OrderCode", OrderCode);
        com.Parameters.AddWithValue("@Date2", Date2);
        com.Parameters.AddWithValue("@TransfereeID", TransfereeID);
        com.Parameters.AddWithValue("@DiscountAmount2", DiscountAmount2);
        com.Parameters.AddWithValue("@OtherAmount", OtherAmount);
        com.Parameters.AddWithValue("@DeliverPersID", DeliverPersID);
        com.Parameters.AddWithValue("@DestDepID", DestDepID);
        com.Parameters.AddWithValue("@Description", Description);
        com.Parameters.AddWithValue("@UserPersID", UserPersID);
        com.Parameters.AddWithValue("@IsAuto", IsAuto);
        var dalData = new Data();
        return dalData.GetDataTableBySP(cSet.cnStringERP, com, "spInvPageSave");
    }

    public void InvPageAmountSave
        (object InvPageID, object UserPersID)
    {
        var com = new SqlCommand();
        com.Parameters.AddWithValue("@InvPageId", InvPageID);
        com.Parameters.AddWithValue("@UserPersID", UserPersID);


        var dalData = new Data();
        dalData.GetDataTableBySP(cSet.cnStringERP, com, "spInvPageAmountSave");
    }


    public DataTable ServiceRegLoad(int InvPageID)
    {
        var tql = @"
SELECT 
ROW_NUMBER() OVER (PARTITION BY InvPageID ORDER BY InvDID) AS RecNO,
2 AS CRUD,
InvDID,
ipi.ServiceID,
s.ServiceCode,
s.Name AS ServiceName,
Num2,
Unit2,
unit2.Name AS Unit2Name,
UnitKNum,
ipi.Unit,
unit.Name AS UnitName,
Num,
StockNum,
StockNumAll,
IsBonus,
BuyFeeAmount2,
BuyAmount2,
SellPer,
SellFeeAmount,
SellFeeAmount2,
BuyFeeAmount,
BuyAmount,
DiscountAmount,
TaxAmount,
BuyAmountLast,
SellAmount,
SellAmount2,
SellAmountLast,
ResultAmount


FROM SamanERP.dbo.InvPageItem ipi
INNER JOIN SamanHIS.dbo.Service s on s.ServiceID=ipi.ServiceID
INNER JOIN SamanERP.dbo.vADR unit on unit.ID = s.ServiceUnit
INNER JOIN SamanERP.dbo.vADR unit2 on unit2.ID = ipi.Unit2

WHERE 1=1
AND InvPageID=@InvPageID
AND unit.ADName='Unit'
AND unit2.ADName='Unit'

                        ";

        var com = new SqlCommand();
        com.Parameters.AddWithValue("@InvPageID", InvPageID);

        var dalData = new Data();
        return dalData.GetDataTableByText(cSet.cnStringERP, com, tql);
    }

    public int InvPageCodeLast(object StorageID)
    {
        const string tsql = @"
                                SELECT TOP(1) InvPageCode
                                FROM SamanERP.dbo.InvPage
                                WHERE StorageID = @StorageID AND InvType=2
                                ORDER BY InvPageID DESC";

        var com = new SqlCommand();
        com.Parameters.AddWithValue("@StorageID", StorageID);

        var dalData = new Data();
        var dt = dalData.GetDataTableByText(cSet.cnStringERP, com, tsql);

        var invPageCode = 0;
        if (dt.Rows.Count > 0) invPageCode = Convert.ToInt32(dt.Rows[0]["InvPageCode"]);

        return invPageCode;
    }


    public void InvPageItemSave
    (object CRUD, object InvPageID, object InvDID, object InvCategoryType, object StorageID, object StoragePeriodID,
        object ServiceID, object Num2, object Unit2, object UnitKNum, object Unit, object Num,
        object StockNum, object StockNumAll, object IsBonus, object BuyFeeAmount2, object BuyAmount2,
        object SellPer,
        object SellFeeAmount, object SellFeeAmount2, object BuyFeeAmount, object BuyAmount,
        object DiscountAmount, object TaxAmount, object BuyAmountLast, object SellAmount,
        object SellAmount2, object SellAmountLast, object ResultAmount)
    {
        var com = new SqlCommand();
        com.Parameters.AddWithValue("@CRUD", CRUD);
        com.Parameters.AddWithValue("@InvPageID", InvPageID);
        com.Parameters.AddWithValue("@InvDID", InvDID);
        com.Parameters.AddWithValue("@InvType", 2);
        com.Parameters.AddWithValue("@InvCategoryType", InvCategoryType);
        com.Parameters.AddWithValue("@StorageID", StorageID);
        com.Parameters.AddWithValue("@StoragePeriodID", StoragePeriodID);

        com.Parameters.AddWithValue("@ServiceID", ServiceID);
        com.Parameters.AddWithValue("@Num2", Num2);
        com.Parameters.AddWithValue("@Unit2", Unit2);
        com.Parameters.AddWithValue("@UnitKNum", UnitKNum);
        com.Parameters.AddWithValue("@Unit", Unit);
        com.Parameters.AddWithValue("@Num", Num);
        com.Parameters.AddWithValue("@StockNum", StockNum);
        com.Parameters.AddWithValue("@StockNumAll", StockNumAll);
        com.Parameters.AddWithValue("@IsBonus", IsBonus);
        com.Parameters.AddWithValue("@BuyFeeAmount2", BuyFeeAmount2);
        com.Parameters.AddWithValue("@BuyAmount2", BuyAmount2);
        com.Parameters.AddWithValue("@SellPer", SellPer);
        com.Parameters.AddWithValue("@SellFeeAmount", SellFeeAmount);
        com.Parameters.AddWithValue("@SellFeeAmount2", SellFeeAmount2);
        com.Parameters.AddWithValue("@BuyFeeAmount", BuyFeeAmount);
        com.Parameters.AddWithValue("@BuyAmount", BuyAmount);
        com.Parameters.AddWithValue("@DiscountAmount", DiscountAmount);
        com.Parameters.AddWithValue("@TaxAmount", TaxAmount);
        com.Parameters.AddWithValue("@BuyAmountLast", BuyAmountLast);
        com.Parameters.AddWithValue("@SellAmount", SellAmount);
        com.Parameters.AddWithValue("@SellAmount2", SellAmount2);
        com.Parameters.AddWithValue("@SellAmountLast", SellAmountLast);
        com.Parameters.AddWithValue("@ResultAmount", ResultAmount);

        var dalData = new Data();
        dalData.ExecuteNonQueryBySP(cSet.cnStringERP, com, "spInvPageItemSave");
    }
}