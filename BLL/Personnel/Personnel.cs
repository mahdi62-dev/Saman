using System.Data;
using System.Data.SqlClient;
using DAL;

namespace Saman.BLL.Personnel;

public class Personnel
{
    public DataTable PersSearch(int persId)
    {
        var com = new SqlCommand();
        com.Parameters.AddRange(new[]
        {
            new SqlParameter("@PersID", persId)
        });
        var dalData = new Data();
        return dalData.GetDataTableBySP(cSet.cnStringERP, com, "spPersSearch");
    }

    public DataTable PerssonelLoad()
    {
        var query = @"
            SELECT 
            p.*
            FROM dbo.vPersonnelFull p
            ";

        var com = new SqlCommand();

        var dalData = new Data();
        return dalData.GetDataTableByText(cSet.cnStringERP, com, query);
    }
}