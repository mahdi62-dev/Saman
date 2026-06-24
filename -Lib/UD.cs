using System.Data.SqlClient;
using System.Windows.Forms;
using DAL;
using DevExpress.XtraEditors;

namespace UI;

internal class UD
{
    public static void GetTbFromDr(SqlDataReader dr, string field, TextEdit tb)
    {
        tb.Text = dr[field].ToString();
    }

    public static void GetTbFromDr(SqlDataReader dr, string field, TextBox tb)
    {
        tb.Text = dr[field].ToString();
    }

    public static void GetLblFromDr(SqlDataReader dr, string field, Label lbl)
    {
        lbl.Text = dr[field].ToString();
    }

    public static int GetIntFromDr(SqlDataReader dr, string field)
    {
        return int.Parse(dr[field].ToString());
    }

    public static byte GetByteFromDr(SqlDataReader dr, string field)
    {
        return byte.Parse(dr[field].ToString());
    }


    public static string GetStringFromTable(string table, int row, string field)
    {
        return GL.ds.Tables[table].Rows[row][field].ToString();
    }

    public static int GetIntFromTable(string table, int row, string field)
    {
        return int.Parse(GL.ds.Tables[table].Rows[row][field].ToString());
    }


    public static int GetIndexFromTable(string tableName, string searchField, string text)
    {
        for (var i = 0; i < GL.ds.Tables[tableName].Rows.Count; i++)
        {
            var Name = GL.ds.Tables[tableName].Rows[i][searchField].ToString();
            if (Name == text) return i;
        }

        return -1;
    }
}