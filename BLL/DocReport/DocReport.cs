using System.Data.SqlClient;
using DAL;
using DevExpress.XtraRichEdit;

namespace Saman.BLL.DocReport;

public class DocReport
{
    public void DocReportSave(int CRUD, int PageID, byte[] Doc, object Rtf, object Txt, object Htm, byte[] PDF,
        string UrlAns, int UserPersID)
    {
        var com = new SqlCommand();
        com.Parameters.Add("@CRUD", CRUD);
        com.Parameters.Add("@PageID", PageID);
        com.Parameters.Add("@Doc", Doc);
        com.Parameters.Add("@Rtf", Rtf);
        com.Parameters.Add("@Txt", Txt);
        com.Parameters.Add("@Htm", Htm);
        com.Parameters.Add("@PDF", PDF);
        com.Parameters.Add("@UrlAns", UrlAns);

        com.Parameters.Add("@UserPersID", UserPersID);

        var dalData = new Data();
        dalData.ExecuteNonQueryBySP(cSet.cnStringA, com, "spDocReport");
    }

    public byte[] DocReportLoad(int CRUD, int PageID)
    {
        var com = new SqlCommand();
        com.Parameters.Add("@CRUD", CRUD);
        com.Parameters.Add("@PageID", PageID);


        var dalData = new Data();
        var dt = dalData.GetDataTableBySP(cSet.cnStringA, com, "spDocReport");

        return (byte[])dt.Rows[0]["Doc"];
    }

    public void DocReportDel(int PageID)
    {
        var com = new SqlCommand();
        com.Parameters.Add("@CRUD", 4);
        com.Parameters.Add("@PageID", PageID);

        var dalData = new Data();
        dalData.ExecuteNonQueryBySP(cSet.cnStringReport, com, "spDocReport");
    }

    public byte[] DicFileLoad(int DicID)
    {
        var tsql = @"
                        SELECT Dic
                        FROM dbo.DicFile
                        WHERE DicID=@DicID
                        ";


        //var dalData = new DAL.Data();
        //DataTable dt = dalData.GetDataTableByText(DAL.cSet.cnStringReport, com, tsql);
        //return (byte[])dt.Rows[0]["Dic"];

        var con = new SqlConnection { ConnectionString = cSet.cnStringReport };
        //com.Connection = con;
        //com.CommandType = CommandType.StoredProcedure;
        //com.CommandText = comText;

        con.Open();
        //com.ExecuteNonQuery();
        //con.Close();


        var com = new SqlCommand(tsql, con);

        com.Parameters.Add("@DicID", DicID);
        var reader = com.ExecuteReader();

        byte[] buffer = { };
        if (reader.Read() != null) buffer = (byte[])reader["Dic"];

        con.Close();
        return buffer;
    }

    public AutoCorrectReplaceInfoCollection AutoCorrectLoad(int ModID)
    {
        var tsql = @"
                        SELECT BeforeText, AfterText
                        FROM dbo.AutoCorrect
                        WHERE ModID=0 OR ModID=@ModID
                        ";


        var con = new SqlConnection { ConnectionString = cSet.cnStringReport };
        con.Open();


        var com = new SqlCommand(tsql, con);

        com.Parameters.Add("@ModID", ModID);

        var reader = com.ExecuteReader();

        var coll = new AutoCorrectReplaceInfoCollection();


        if (reader.HasRows)
            while (reader.Read())
            {
                var acrInfo = new AutoCorrectReplaceInfo(reader.GetString(0), reader.GetString(1));
                coll.Add(acrInfo);
            }

        reader.Close();
        con.Close();

        return coll;
    }
}