using System.Data;
using System.Data.SqlClient;
using DAL;

namespace Saman.BLL.ServiceReg;

public class ServicePrice
{
    public DataTable ServicePriceLoad(object RegDate, int ServiceID, int EncID, int DrID, bool IsOIC,
        long ManualPrice, bool IsOpr)
    {
        var TSQL =
            @" SELECT * FROM dbo.fntServicePrice (@RegDate,@ServiceID,@EncID,@DrID,@IsOIC,@ManualPrice,@IsOpr) ";


        var com = new SqlCommand();
        com.Parameters.Add("@RegDate", RegDate);
        com.Parameters.Add("@ServiceID", ServiceID);
        com.Parameters.Add("@EncID", EncID);
        com.Parameters.Add("@DrID", DrID);
        com.Parameters.Add("@IsOIC", IsOIC);
        com.Parameters.Add("@ManualPrice", ManualPrice);
        com.Parameters.Add("@IsOpr", IsOpr);


        var dalData = new Data();
        return dalData.GetDataTableByText(cSet.cnStringA, com, TSQL);
    }
}