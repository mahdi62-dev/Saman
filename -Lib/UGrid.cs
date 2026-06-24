using System.Data;
using DAL;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;

namespace UI;

internal class UGrid
{
    public static void GetTbFromRow(DataRow row, string field, TextEdit tb)
    {
        tb.Text = row[field].ToString();
    }

    public static int GetIntFromRow(DataRow row, string field)
    {
        return int.Parse(row[field].ToString());
    }

    public static string GetStrFromRow(DataRow row, string field)
    {
        return row[field].ToString();
    }


    public static void SetGridDataSource(string tableName, GridControl grid)
    {
        List.LoadListToDs(GL.ds, "*", tableName, tableName);
        grid.DataSource = GL.ds.Tables[tableName];
    }

    public static void SetGridDataSource(string tableName, string where, string dsTable, GridControl grid)
    {
        List.LoadListToDs(GL.ds, "*", tableName, where, dsTable);
        grid.DataSource = GL.ds.Tables[tableName];
    }
}