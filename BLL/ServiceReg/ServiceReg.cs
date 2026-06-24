using System.Data;
using System.Data.SqlClient;
using DAL;

namespace Saman.BLL.ServiceReg;

public class ServiceReg
{
    public void ServiceRegSave
    (int CRUD, int EncID, int PageID, object StorageID, object StoragePeriodID, object DrID, object SRID,
        object ServiceID,
        object Num, object ServiceCategory, int TotalAmountManual, int UserPersID)
    {
        var com = new SqlCommand();
        com.Parameters.Add("@CRUD", CRUD);
        com.Parameters.Add("@EncID", EncID);
        com.Parameters.Add("@PageID", PageID);
        com.Parameters.Add("@StorageID", StorageID);
        com.Parameters.Add("@StoragePeriodID", StoragePeriodID);
        com.Parameters.Add("@DrID", DrID);
        com.Parameters.Add("@SRID", SRID);
        com.Parameters.Add("@ServiceID", ServiceID);
        com.Parameters.Add("@Num", Num);
        com.Parameters.Add("@ServiceCategory", ServiceCategory);
        com.Parameters.Add("@TotalAmountManual", TotalAmountManual);
        com.Parameters.AddWithValue("@UserPersID", UserPersID);


        var dalData = new Data();
        dalData.ExecuteNonQueryBySP(cSet.cnStringA, com, "spServiceRegSave");
    }

    public DataTable ServiceRegLoad(int pageID)
    {
        var query = @"
SELECT
ROW_NUMBER() OVER (PARTITION BY PageID ORDER BY SRID) AS RecNO
,2 as 'CRUD'
,[SRID]
,sr.DrID
,dr.Name AS DrName
,[PageID]
,[ServiceID]
,[ServiceCategory]
,Price
,[Num]
,[ServiceCode]
,[ServiceName]
,[TotalAmount]
,[ProAmount]
,[TechAmount]
,[PatientAmount]
,[DiscountAmount]
,[ResultAmount]
,IsManualPrice

FROM SamanHIS.[dbo].[vServiceReg] sr
INNER JOIN vDr dr on Dr.DrID=sr.DrID

WHERE PageID=@PageID AND sr.ServiceCategory not in (31,32,33,34)
";

        var com = new SqlCommand();
        com.Parameters.AddRange(new[]
        {
            new SqlParameter("@PageID", pageID)
        });
        var dalData = new Data();
        return dalData.GetDataTableByText(cSet.cnStringA, com, query);
    }

    public DataTable ServiceRegWithoutDrugLoad(int pageID)
    {
        var query = @"
SELECT
ROW_NUMBER() OVER (PARTITION BY PageID ORDER BY SRID) AS RecNO
,2 as 'CRUD'
,[SRID]
,sr.DrID
,dr.Name AS DrName
,[PageID]
,[ServiceID]
,[ServiceCategory]
--,Price
,[Num]
,[TotalPrice]
,[ServiceCode]
,[ServiceName]
,[TotalAmount]
,[ProAmount]
,[TechAmount]
,[PatientAmount]
,[DiscountAmount]
,[ResultAmount]
,IsManualPrice

FROM SamanHIS.[dbo].[vServiceReg] sr
INNER JOIN vDr dr on Dr.DrID=sr.DrID

WHERE PageID=@PageID AND sr.ServiceCategory not in (31,32,33,34) AND sr.ServiceType<>2
";

        var com = new SqlCommand();
        com.Parameters.AddRange(new[]
        {
            new SqlParameter("@PageID", pageID)
        });
        var dalData = new Data();
        return dalData.GetDataTableByText(cSet.cnStringA, com, query);
    }

    public DataTable ServiceRegDrugLoad(int PageID)
    {
        var query = @"
SELECT
ROW_NUMBER() OVER (PARTITION BY PageID ORDER BY SRID) AS RecNO
,2 as 'CRUD'
,[SRID]
,sr.DrID
,dr.Name AS DrName
,[PageID]
,[ServiceID]
,ServiceCategory
,Price
,[Num]
,[ServiceCode]
,[ServiceName]
,[TotalAmount]
,[ProAmount]
,[TechAmount]
,[PatientAmount]
,[DiscountAmount]
,[ResultAmount]
,IsManualPrice

FROM SamanHIS.[dbo].[vServiceReg] sr
INNER JOIN vDr dr on Dr.DrID=sr.DrID

WHERE PageID=@PageID AND sr.ServiceType=2
";

        var com = new SqlCommand();
        com.Parameters.Add("@PageID", PageID);

        var dalData = new Data();
        return dalData.GetDataTableByText(cSet.cnStringA, com, query);
    }


    public DataTable ServicePakageItemLoad(int ServicePackageID)
    {
        var query = @"
SELECT [PackageID],[ServiceCode],[ServiceNum]
FROM [SamanHIS].[dbo].[ServicePackageItem]
WHERE PackageID = @PackageID
";

        var com = new SqlCommand();
        com.Parameters.AddRange(new[]
        {
            new SqlParameter("@PackageID", ServicePackageID)
        });
        var dalData = new Data();
        return dalData.GetDataTableByText(cSet.cnStringA, com, query);
    }
}