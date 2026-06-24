using System.Data;

namespace Saman.BLL.Storage;

public class DT
{
    public void CreateTableServiceReg(DataTable dt)
    {
        dt.Columns.Add(new DataColumn("RecNo", typeof(int)) { DefaultValue = 0 });

        dt.Columns.Add(new DataColumn("CRUD", typeof(int)) { DefaultValue = 0 });
        dt.Columns.Add(new DataColumn("InvDID", typeof(int)) { DefaultValue = 0 });
        dt.Columns.Add(new DataColumn("ServiceID", typeof(int)) { DefaultValue = 0 });
        dt.Columns.Add("ServiceCode");
        dt.Columns.Add("ServiceName");


        dt.Columns.Add(new DataColumn("Num2", typeof(double)) { DefaultValue = 0 });
        dt.Columns.Add(new DataColumn("Unit2", typeof(double)) { DefaultValue = 0 });
        dt.Columns.Add("Unit2Name");

        dt.Columns.Add(new DataColumn("UnitKNum", typeof(double)) { DefaultValue = 0 });
        dt.Columns.Add(new DataColumn("Unit", typeof(int)) { DefaultValue = 0 });
        dt.Columns.Add("UnitName");

        dt.Columns.Add(new DataColumn("Num", typeof(double)) { DefaultValue = 0 });
        dt.Columns.Add(new DataColumn("ExpirationDate", typeof(string)) { DefaultValue = 0 });


        dt.Columns.Add(new DataColumn("IsBonus", typeof(bool)) { DefaultValue = false });

        dt.Columns.Add(new DataColumn("BuyFeeAmount2", typeof(long)) { DefaultValue = 0 });
        dt.Columns.Add(new DataColumn("BuyAmount2", typeof(long)) { DefaultValue = 0 });
        dt.Columns.Add(new DataColumn("SellPer", typeof(int)) { DefaultValue = 0 });
        dt.Columns.Add(new DataColumn("SellFeeAmount", typeof(long)) { DefaultValue = 0 });
        dt.Columns.Add(new DataColumn("SellFeeAmount2", typeof(long)) { DefaultValue = 0 });

        dt.Columns.Add(new DataColumn("BuyFeeAmount", typeof(long)) { DefaultValue = 0 });
        dt.Columns.Add(new DataColumn("BuyAmount", typeof(long)) { DefaultValue = 0 });

        dt.Columns.Add(new DataColumn("DiscountAmount", typeof(long)) { DefaultValue = 0 });
        dt.Columns.Add(new DataColumn("TaxAmount", typeof(long)) { DefaultValue = 0 });


        dt.Columns.Add(new DataColumn("BuyAmountLast", typeof(long)) { DefaultValue = 0 });
        dt.Columns.Add(new DataColumn("StockNum", typeof(long)) { DefaultValue = 0 });
        dt.Columns.Add(new DataColumn("StockNumAll", typeof(long)) { DefaultValue = 0 });

        dt.Columns.Add(new DataColumn("SellAmount", typeof(long)) { DefaultValue = 0 });
        dt.Columns.Add(new DataColumn("SellAmount2", typeof(long)) { DefaultValue = 0 });

        dt.Columns.Add(new DataColumn("SellAmountLast", typeof(long)) { DefaultValue = 0 });
        dt.Columns.Add(new DataColumn("ResultAmount", typeof(long)) { DefaultValue = 0 });
    }
}