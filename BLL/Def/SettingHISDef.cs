using System.Data;
using System.Data.SqlClient;
using DAL;

namespace Saman.BLL.Def;

public class SettingHISDef
{
    public DataTable SettingListAllLoad()
    {
        var tsql = @"
                        SELECT *
                        FROM SamanReport.dbo.vReport
                        WHERE 1=1
                        AND IsRep=1
                        AND RepID>0
                        ";

        var com = new SqlCommand();
        //com.Parameters.AddWithValue("@RepID", RepID);

        var dalData = new Data();
        return dalData.GetDataTableByText(cSet.cnStringReport, com, tsql);
    }
}