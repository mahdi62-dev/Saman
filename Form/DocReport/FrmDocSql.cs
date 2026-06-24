using System;
using System.Data.SqlClient;
using DAL;

namespace Saman.Form.DocReport;

public partial class FrmDocSql : System.Windows.Forms.Form
{
    public FrmDocSql()
    {
        InitializeComponent();
    }

    private void button1_Click(object sender, EventArgs e)
    {
        var rtf = richEditControl1.RtfText;

        var tsql = @"SELECT *
FROM [SamanDC].[dbo].[DocReport]
  WHERE pageid IN (1244)
and rtf like '%" + rtf + "%'";

        var com = new SqlCommand();
        //com.Parameters.AddWithValue("@EncID", EncID);

        var dalData = new Data();
        var dt = dalData.GetDataTableByText(cSet.cnStringA, com, tsql);
        //return Convert.ToInt32(dt.Rows[0]["TotalPayAmount"]);
    }
}