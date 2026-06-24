using System.Data;
using System.Data.SqlClient;
using DAL;

namespace Saman.BLL.Common;

public class List
{
    public DataTable LoadTable(string Fields, string TableName, string WHERE)
    {
        string TSQL = null;

        if (Fields != null) TSQL = TSQL + " SELECT " + Fields + " ";

        if (TableName != null) TSQL = TSQL + " FROM " + TableName + " ";

        if (WHERE != null) TSQL = TSQL + " WHERE " + WHERE;

        var com = new SqlCommand();

        var dalData = new Data();
        return dalData.GetDataTableByText(cSet.cnStringA, com, TSQL);
    }
}