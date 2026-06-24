using System.Data;
using System.Data.SqlClient;
using DAL;

namespace Saman.BLL.Enc;

public class EncInfoStatus
{
    public DataTable GetEncInfoStatus(string Date1, string Date2)
    {
        var tsql = @" SELECT 
e.[EncID]
,ef.FirstName
,ef.LastName
,ef.DepName
,ef.DrName
,es.[InfoStatus]
,a.Name as InfoStatusName
,es.[StartDate]
,es.[EndDate]
,es.[IsDisable]
,es.[PersID]

FROM Enc e WITH(NOLOCK)
INNER JOIN EncInfoStatus es WITH(NOLOCK) on es.EncID=e.EncID
INNER JOIN [dbo].[vEncFullName] ef WITH(NOLOCK) on ef.EncID=e.EncID
INNER JOIN [dbo].[ADR] a WITH(NOLOCK) on a.ID = es.InfoStatus
where a.ADID=43
and es.StartDate between @Date1 and @Date2

                        ";
        var com = new SqlCommand();
        com.Parameters.AddWithValue("@Date1", Date1);
        com.Parameters.AddWithValue("@Date2", Date2);


        var dalData = new Data();
        return dalData.GetDataTableByText(cSet.cnStringA, com, tsql);
    }
}