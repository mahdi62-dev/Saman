using System;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace Saman.BLL.InsExport;

public class InsExport
{
    public DataTable InsExportPageLoad
        (int CRUD, string DateFilter1, string Insurance, string StrDep)
    {
        var com = new SqlCommand();

        //com.Parameters.Add("@CRUD", CRUD);
        com.Parameters.Add("@DateFilter1", DateFilter1);
        com.Parameters.Add("@Insurance", Insurance);
        com.Parameters.Add("@DepID", StrDep);

        //com.Parameters.AddWithValue("@UserPersID", UserPersID);

        var dalData = new Data();
        return dalData.GetDataTableBySP(cSet.cnStringA, com, "spInsExportPage");
    }

    public DataTable InsExportServiceRegLoad
        (int CRUD, string DateFilter1, string Insurance, string StrDep)
    {
        var com = new SqlCommand();

        //com.Parameters.Add("@CRUD", CRUD);
        com.Parameters.Add("@DateFilter1", DateFilter1);
        com.Parameters.Add("@Insurance", Insurance);
        com.Parameters.Add("@DepID", StrDep);


        //com.Parameters.AddWithValue("@UserPersID", UserPersID);

        var dalData = new Data();
        return dalData.GetDataTableBySP(cSet.cnStringA, com, "spInsExportServiceReg");
    }

    public DataTable InsHeaderInfo(string strDepID)
    {
        var tsql = @"
                        SELECT ISNULL( (SELECT CenterIDA FROM SamanHIS.dbo.Dep WHERE DepID=@DepID)
                        ,CenterID) AS CenterID, CenterName, CompanyTel
                        FROM SamanERP.dbo.Setting
                        ";

        var com = new SqlCommand();
        com.Parameters.Add("@DepID", strDepID == null ? DBNull.Value : strDepID);

        var dalData = new Data();
        return dalData.GetDataTableByText(cSet.cnStringERP, com, tsql);
    }
}