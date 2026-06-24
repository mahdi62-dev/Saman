using System.Data;
using System.Data.SqlClient;
using DAL;

namespace Saman.BLL.Storage;

public class InvFromServiceReg
{
    public DataTable InvServiceDrugConsumeLoad
        (object StorageID, object StoragePeriodID, object DepID, object StartDate, object EndDate)
    {
        var com = new SqlCommand();
        com.Parameters.Add("@StorageID", StorageID);
        com.Parameters.Add("@StoragePeriodID", StoragePeriodID);
        com.Parameters.Add("@DepID", DepID);
        com.Parameters.Add("@StartDate", StartDate);
        com.Parameters.Add("@EndDate", EndDate);

        var dalData = new Data();
        return dalData.GetDataTableBySP(cSet.cnStringERP, com, "spInvServiceDrugConsumeLoad");
    }

    public void InvOutputApplyStatusSave
        (object DepID, object StartDate, object EndDate, object InvPageID, object InvOutputApplyStatus)
    {
        var com = new SqlCommand();
        com.Parameters.Add("@DepID", DepID);
        com.Parameters.Add("@StartDate", StartDate);
        com.Parameters.Add("@EndDate", EndDate);
        com.Parameters.Add("@InvPageID", InvPageID);
        com.Parameters.Add("@InvOutputApplyStatus", InvOutputApplyStatus);

        var dalData = new Data();
        dalData.ExecuteNonQueryBySP(cSet.cnStringERP, com, "spInvServiceDrugConsumeSave");
    }

    public DataTable GetServiceReg(string Date1, string Date2, int DepID)
    {
        var tsql = @"
SELECT        
e.EncID,
p.DepID,
d.Name AS DepName,
e.FullName AS PatientName, 
sr.Date,
sr.PageID, 
sr.SRID,
p.InvOutputApplyStatus AS PageInvOutputApplyStatus, 
s.ServiceCode,
s.Name AS ServiceName,
sr.Num, 
UnitList.Name AS UnitName, 
p.InvOutputApplyStatus AS PageInvOutputApplyStatus,
sr.InvOutputApplyStatus AS ServiceRegInvOutputApplyStatus

FROM SamanHIS.dbo.vEncFullName AS e 
INNER JOIN SamanHIS.dbo.Page AS p ON p.EncID = e.EncID
INNER JOIN SamanHIS.dbo.vServiceReg AS sr ON sr.PageID = p.PageID 
INNER JOIN SamanHIS.dbo.vServiceDrug AS s ON s.ServiceID = sr.ServiceID
INNER JOIN SamanERP.dbo.vADR AS UnitList ON UnitList.ID = s.ServiceUnit
INNER JOIN SamanHIS.dbo.vDep AS d ON d.DepID = p.DepID
WHERE 1 = 1 
AND UnitList.ADName = 'Unit'

AND p.InvOutputApplyStatus=0
AND p.Date BETWEEN @Date1 AND @Date2
AND p.DepID=@DepID

                        ";

        var com = new SqlCommand();
        com.Parameters.Add("@Date1", Date1);
        com.Parameters.Add("@Date2", Date2);
        com.Parameters.AddWithValue("@DepID", DepID);


        var dalData = new Data();
        return dalData.GetDataTableByText(cSet.cnStringERP, com, tsql);
    }
}