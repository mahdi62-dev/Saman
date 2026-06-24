using System.Data;
using System.Data.SqlClient;
using DAL;

namespace Saman.BLL.Enc;

public class EncInfo
{
    public DataTable GetEncInfo(string Date1, string Date2)
    {
        var tsql = @"
SELECT 
e.EncID,
p.LastName + '_' + p.FirstName AS FullName,
e.Age,
 dbo.Dep.Name AS DepName,
dr.Name AS DrName,
InfoStatus,
InfoStatusName,
--REPLACE(SUBSTRING(ei.StartDate,3,14),' ',' ') AS StartDate,
SUBSTRING(ei.StartDate,3,14) AS StartDate,
SUBSTRING(ei.EndDate,3,14) AS EndDate,
ei.EndDate,
ei.IsDisable,
e.PersID,
WardDrug,
WardService,
Opr,
OprDrug,
e.Description,
cc.Name AS ChiefComplaintName,
e.EncStatus

FROM dbo.vEncInfo ei
INNER JOIN dbo.enc e ON e.EncID=ei.EncID
INNER JOIN dbo.Patient AS p WITH (NOLOCK) ON p.PatientID = e.PatientID
INNER JOIN dbo.Dep WITH (NOLOCK) ON dbo.Dep.DepID = e.DepID
INNER JOIN dbo.vADR cc ON cc.ID=e.ChiefComplaint
 INNER JOIN dbo.vDr AS dr WITH (NOLOCK) ON dr.DrID = e.DrID
where ei.IsDisable=0
AND cc.ADName='ChiefComplaint'
AND ei.StartDate BETWEEN @Date1 and @Date2
ORDER BY ei.StartDate Desc

                        ";
        var com = new SqlCommand();
        com.Parameters.AddWithValue("@Date1", Date1);
        com.Parameters.AddWithValue("@Date2", Date2);


        var dalData = new Data();
        return dalData.GetDataTableByText(cSet.cnStringA, com, tsql);
    }

    public void EncInfoSave(int EncID, int InfoStatus)
    {
        var com = new SqlCommand();
        com.Parameters.AddWithValue("@EncID", EncID);
        com.Parameters.AddWithValue("@InfoStatus", InfoStatus);

        var dalData = new Data();
        dalData.ExecuteNonQueryBySP(cSet.cnStringA, com, "spEncInfoSave");
    }
}