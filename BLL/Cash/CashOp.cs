using System.Data;
using System.Data.SqlClient;
using DAL;

namespace Saman.BLL.Cash;

public class CashOp
{
    public DataTable GetEncData(int encId)
    {
        var query = @"--DECLARE @EncID int=114

SELECT top 1
ISNULL(Cash.CashID,0) AS CashID, Cash.CashDate , Dep.DepID,Dep.Name as DepName, p.PageID, p.PageCode, p.TotalAmount, p.ResultAmount, p.PayAmount, p.RemainAmount, ef.EncID, ef.PatientID, ef.EncType, ef.EncTypeName, ef.StartDate, ef.DrName,
	ef.InsuranceName, ef.FirstName, ef.LastName, ef.FatherName, ef.NationalCode, ef.Age
FROM  dbo.Page p
INNER JOIN dbo.vEncFullName ef ON p.EncID = ef.EncID
INNER JOIN dbo.Dep ON Dep.DepID = p.DepID
LEFT OUTER JOIN dbo.Cash ON Cash.PageID = p.PageID

WHERE Ef.EncType=1 AND p.EncID = @EncID
order by p.PageID desc
                        ";


        var con = new SqlConnection { ConnectionString = cSet.cnString };

        var cmd = new SqlCommand(query)
        {
            Connection = con,
            CommandType = CommandType.Text
        };

        cmd.Parameters.AddRange(new[]
        {
            new SqlParameter("@EncID", encId)
        });

        var dalData = new Data();

        return dalData.GetDataTable(cmd);
    }
}