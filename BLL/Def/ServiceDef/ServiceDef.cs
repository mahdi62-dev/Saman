using System.Data;
using System.Data.SqlClient;
using DAL;

namespace Saman.BLL.Def.ServiceDef;

public class ServiceDef
{
    public DataTable ServiceListLoad(int ServiceCategoryID, bool IsDisableLoad, bool IsExpiredLoad)
    {
        var IsDisable = IsDisableLoad ? "1" : "0";
        var IsExpire = IsExpiredLoad ? "1" : "0";

        var strTSQL = @"
                        SELECT *
                        FROM  [SamanHIS].[dbo].[vService]
                        WHERE 1=1

                        AND  ServiceCategory = @ServiceCategoryID
                        AND EndDate IS NULL
                        ORDER BY CAST(ServiceCode as int)
                        ";

        var com = new SqlCommand();
        com.Parameters.Add("@ServiceCategoryID", ServiceCategoryID);


        var dalData = new Data();
        return dalData.GetDataTableByText(cSet.cnStringA, com, strTSQL);
    }

    public DataTable ServiceLoad(int ServiceID)
    {
        var com = new SqlCommand();
        com.Parameters.AddWithValue("@ServiceID", ServiceID);

        var dalData = new Data();
        return dalData.GetDataTableByText(cSet.cnStringA, com, "spService");
    }

    public DataTable ServiceSave
    (int CRUD, int ServiceID, int ServiceType, int ServiceCategory, string ServiceCode, string ServiceCode2,
        string Name,
        string NameLong, int Price, int ServiceUnit, int ServiceStatus, bool IsDisable
    )
    {
        var com = new SqlCommand();
        com.Parameters.AddWithValue("@CRUD", CRUD);
        com.Parameters.AddWithValue("@ServiceID", ServiceID);
        com.Parameters.AddWithValue("@ServiceType", ServiceType);
        com.Parameters.AddWithValue("@ServiceCategory", ServiceCategory);
        com.Parameters.AddWithValue("@ServiceCode", ServiceCode);
        com.Parameters.AddWithValue("@ServiceCode2", ServiceCode2);
        com.Parameters.AddWithValue("@Name", Name);
        com.Parameters.AddWithValue("@NameLong", NameLong);
        com.Parameters.AddWithValue("@Price", Price);
        com.Parameters.AddWithValue("@ServiceUnit", ServiceUnit);
        com.Parameters.AddWithValue("@ServiceStatus", ServiceStatus);
        com.Parameters.AddWithValue("@IsDisable", IsDisable);

        var dalData = new Data();
        return dalData.GetDataTableByText(cSet.cnStringA, com, "spService");
    }
}