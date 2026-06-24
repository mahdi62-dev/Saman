using System.Data;
using System.Data.SqlClient;
using DAL;

namespace Saman.BLL.Dep;

public class Dep
{
    public DataTable DepDataLoad(int DepID)
    {
        var tsql = @"


SELECT 
dep.DepID,
dep.Name,
dep.StorageID,
ISNULL(StoragePeriod.StoragePeriodID,0) AS StoragePeriodID
FROM
(
SELECT 
MAX(d.DepID) AS DepID,
MAX(d.Name) AS Name,
CASE WHEN COUNT(StorageID)=0 THEN 0 ELSE MAX(StorageID) END AS StorageID
FROM SamanHIS.dbo.vDep d
LEFT outer JOIN SamanERP.dbo.InvStorageDep isd ON isd.DepID=d.DepID
WHERE d.DepID=@DepID
) AS Dep
LEFT OUTER JOIN
(SELECT StorageID,
MAX(StoragePeriodID) AS StoragePeriodID
  FROM [SamanERP].[dbo].[InvStoragePeriod]
  WHERE IsDisable=0 AND EndDate IS NULL
  GROUP BY StorageID
) StoragePeriod ON StoragePeriod.StorageID = dep.StorageID
            ";

        var com = new SqlCommand();
        com.Parameters.Add("@DepID", DepID);


        var dalData = new Data();
        return dalData.GetDataTableByText(cSet.cnStringA, com, tsql);
    }
}