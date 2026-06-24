using System.Data;
using System.Data.SqlClient;

namespace DAL;

public class List
{
    //====================================================================================================================

    //====================================================================================================================

    public static void LoadListToDs(DataSet ds, string fields, string fromTable,
        string dsTable)

    {
        if (ds.Tables[dsTable] != null) ds.Tables[dsTable].Clear();

        SqlHelper.FillDataset(cSet.cnString, CommandType.Text,
            "SELECT " + fields +
            " FROM " + fromTable,
            ds, new[] { dsTable });
    }
    //====================================================================================================================

    public static void LoadListToDs(DataSet ds, string fields, string fromTable, string strWhere,
        string dsTable)
    {
        if (ds.Tables[dsTable] != null) ds.Tables[dsTable].Clear();

        var strSel = "SELECT " + fields +
                     " FROM " + fromTable +
                     " WHERE " + strWhere;


        SqlHelper.FillDataset(cSet.cnString, CommandType.Text, strSel,
            ds, new[] { dsTable });
    }
    //====================================================================================================================


    public DataTable LoadListToDataTable
        (string cnString, string fields, string fromTable, string strWhere, string sortField)
    {
        var TSQL = "SELECT " + fields + " FROM " + fromTable;

        if (strWhere != null) TSQL = TSQL + " WHERE " + strWhere;

        if (sortField != null) TSQL = TSQL + " Order by " + sortField;


        var con = new SqlConnection { ConnectionString = cSet.cnString };

        var com = new SqlCommand();
        com.Connection = con;
        com.CommandType = CommandType.Text;
        com.CommandText = TSQL;

        var adapt = new SqlDataAdapter { SelectCommand = com };
        var dt = new DataTable();
        adapt.Fill(dt);

        return dt;
    }
    //====================================================================================================================
}