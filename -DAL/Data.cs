using System.Data;
using System.Data.SqlClient;

namespace DAL;

public class Data
{
    public void GetDsDataTableByComSP(DataSet ds, string dsTable, SqlCommand com)
    {
        if (ds.Tables[dsTable] != null) ds.Tables.Remove(dsTable);

        var con = new SqlConnection { ConnectionString = cSet.cnString };
        com.Connection = con;
        com.CommandType = CommandType.StoredProcedure;


        var adapt = new SqlDataAdapter { SelectCommand = com };

        var dt = new DataTable();
        adapt.Fill(dt);
        dt.TableName = dsTable;
        ds.Tables.Add(dt);
    }

    //====================================================================================================================
    public DataTable GetDataTableByCommand(SqlCommand com)
    {
        var con = new SqlConnection { ConnectionString = cSet.cnString };
        com.Connection = con;
        com.CommandType = CommandType.Text;

        var adapt = new SqlDataAdapter { SelectCommand = com };
        var dt = new DataTable();
        adapt.Fill(dt);

        return dt;
    }

    ////====================================================================================================================
    public DataTable GetDataTable(SqlCommand com)
    {
        //var con = new SqlConnection { ConnectionString = cnString };
        //com.Connection = con;

        var adapt = new SqlDataAdapter { SelectCommand = com };
        var dt = new DataTable();
        adapt.Fill(dt);

        return dt;
    }

    ////====================================================================================================================
    public DataTable GetDataTableBySP(string cnString, SqlCommand com, string comText)
    {
        var con = new SqlConnection { ConnectionString = cnString };
        com.Connection = con;
        com.CommandType = CommandType.StoredProcedure;
        com.CommandTimeout = 600;
        com.CommandText = comText;

        var adapt = new SqlDataAdapter { SelectCommand = com };
        var dt = new DataTable();
        adapt.Fill(dt);

        return dt;
    }

    ////====================================================================================================================
    public DataTable GetDataTableByText(string cnString, SqlCommand com, string comText)
    {
        var con = new SqlConnection { ConnectionString = cnString };
        com.Connection = con;
        com.CommandType = CommandType.Text;
        com.CommandText = comText;

        var adapt = new SqlDataAdapter { SelectCommand = com };
        var dt = new DataTable();
        adapt.Fill(dt);

        return dt;
    }
    ////====================================================================================================================

    public object GetScalerObjectBySP(SqlCommand com)
    {
        var con = new SqlConnection { ConnectionString = cSet.cnString };
        com.Connection = con;
        com.CommandType = CommandType.StoredProcedure;
        object objOutput;
        con.Open();
        objOutput = com.ExecuteScalar();
        con.Close();

        return objOutput;
    }

    public object GetScalerObject(SqlCommand com)
    {
        var con = new SqlConnection { ConnectionString = cSet.cnString };
        com.Connection = con;
        object objOutput;
        con.Open();
        objOutput = com.ExecuteScalar();
        con.Close();

        return objOutput;
    }

    //====================================================================================================================

    public void ExecuteNonQueryByCommand(SqlCommand com)
    {
        var con = new SqlConnection { ConnectionString = cSet.cnString };
        com.Connection = con;

        con.Open();
        com.ExecuteNonQuery();
        con.Close();
    }

    //====================================================================================================================
    public void ExecuteNonQueryBySP(string cnString, SqlCommand com, string comText)
    {
        var con = new SqlConnection { ConnectionString = cnString };
        com.Connection = con;
        com.CommandType = CommandType.StoredProcedure;
        com.CommandText = comText;

        con.Open();
        com.ExecuteNonQuery();
        con.Close();
    }

    public void ExecuteNonQueryByText(string cnString, SqlCommand com, string comText)
    {
        var con = new SqlConnection { ConnectionString = cnString };
        com.Connection = con;
        com.CommandType = CommandType.Text;
        com.CommandText = comText;

        con.Open();
        com.ExecuteNonQuery();
        con.Close();
    }
    //public DataTable GetDataTableBySP(string cnString, SqlCommand com, string comText)
    //{
    //    var con = new SqlConnection { ConnectionString = cnString };
    //    com.Connection = con;
    //    com.CommandType = CommandType.StoredProcedure;
    //    com.CommandText = comText;

    //    var adapt = new SqlDataAdapter { SelectCommand = com };
    //    var dt = new DataTable();
    //    adapt.Fill(dt);

    //    return dt;
    //}
    //====================================================================================================================

    public SqlDataReader DataReaderCommand(SqlCommand com)
    {
        var con = new SqlConnection { ConnectionString = cSet.cnString };
        com.Connection = con;
        con.Open();

        var dataReader = com.ExecuteReader(CommandBehavior.CloseConnection);

        return dataReader;
    }


    public SqlDataReader GetDataReaderByParm(string query, SqlParameter[] parm)
    {
        return SqlHelper.ExecuteReader(cSet.cnString, CommandType.Text, query, parm);
    }

    public static string GetScaler(string field, string fromTable)
    {
        var strSel = "SELECT " + field + " FROM " + fromTable;
        return SqlHelper.ExecuteScalar(cSet.cnString, CommandType.Text, strSel).ToString();
    }

    public static string GetScaler(string query)
    {
        return SqlHelper.ExecuteScalar(cSet.cnString, CommandType.Text, query).ToString();
    }

    public static string GetScaler(string field, string fromTable, string strWhere)
    {
        var strSel = "SELECT " + field + " FROM " + fromTable + " WHERE " + strWhere;
        return SqlHelper.ExecuteScalar(cSet.cnString, CommandType.Text, strSel).ToString();
    }

    public static int GetScalerInt(string field, string fromTable, string strWhere)
    {
        var strSel = "SELECT Count(*) FROM " + fromTable + " WHERE " + strWhere;
        var count = int.Parse(SqlHelper.ExecuteScalar(cSet.cnString, CommandType.Text, strSel).ToString());

        if (count > 0)
        {
            strSel = "SELECT " + field + " FROM " + fromTable + " WHERE " + strWhere;
            return int.Parse(SqlHelper.ExecuteScalar(cSet.cnString, CommandType.Text, strSel).ToString());
        }

        return 0;
    }

    public static int GetScalerInt(string field, string fromTable)
    {
        var strSel = "SELECT Count(*) FROM " + fromTable;
        var count = int.Parse(SqlHelper.ExecuteScalar(cSet.cnString, CommandType.Text, strSel).ToString());

        if (count > 0)
        {
            strSel = "SELECT " + field + " FROM " + fromTable;
            return int.Parse(SqlHelper.ExecuteScalar(cSet.cnString, CommandType.Text, strSel).ToString());
        }

        return 0;
    }

    public static byte[] GetScalerByte(string field, string fromTable, string strWhere)
    {
        var strSel = "SELECT " + field + " FROM " + fromTable + " WHERE " + strWhere;

        if (SqlHelper.ExecuteScalar(cSet.cnString, CommandType.Text, strSel).ToString().Length > 0)
            return (byte[])SqlHelper.ExecuteScalar(cSet.cnString, CommandType.Text, strSel);

        return null;
    }

    //============================================================================================================
    public static void Update(string tableName, string strSet, string strWhere)
    {
        var strUpd = "UPDATE " + tableName + " SET " + strSet + " WHERE " + strWhere;
        SqlHelper.ExecuteNonQuery(cSet.cnString, CommandType.Text, strUpd);
    }

    //============================================================================================================
    public static void Insert(string tableName, string fields, string selValues)
    {
        var strIns = "Insert Into " + tableName + " (" + fields + ") SELECT " + selValues;
        SqlHelper.ExecuteNonQuery(cSet.cnString, CommandType.Text, strIns);
    }

    //============================================================================================================
    public static void Delete(string tableName, string strWhere)
    {
        var strDel = "DELETE FROM " + tableName + " WHERE " + strWhere;
        SqlHelper.ExecuteNonQuery(cSet.cnString, CommandType.Text, strDel);
    }
    //============================================================================================================

    public static void ExecuteNonQuery(string query)
    {
        SqlHelper.ExecuteNonQuery(cSet.cnString, CommandType.Text, query);
    }
    //============================================================================================================
}