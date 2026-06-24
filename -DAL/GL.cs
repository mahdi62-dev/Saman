using System.Data;
using System.Data.SqlClient;

namespace DAL;

internal class GL
{
    public static DataSet ds = new();
    public static SqlConnection con = new(cSet.cnString);

    public static void OpenCon()
    {
        con.Open();
    }
}