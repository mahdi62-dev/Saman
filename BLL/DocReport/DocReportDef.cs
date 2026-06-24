using System.Data;
using System.Data.SqlClient;
using DAL;

namespace Saman.BLL.DocReport;

public class DocReportDef
{
    public void DocReportFileSave(int TemplateID, string Name, byte[] Doc, int UserPersID)
    {
        var tsql = @"
                        DECLARE @SCurdatetime nchar(16)
                        SELECT @SCurdatetime=SamanHIS.dbo.SCurdatetime()

                        UPDATE dbo.DocTemplate
                          SET Name=@Name,
                              Doc=@Doc,
                              UserPersID=@UserPersID,
							  ActionDate=@SCurdatetime
                          WHERE TemplateID=@TemplateID
                        ";

        var com = new SqlCommand();
        com.Parameters.Add("@TemplateID", TemplateID);
        com.Parameters.Add("@Name", Name);
        com.Parameters.Add("@Doc", Doc);
        com.Parameters.Add("@UserPersID", UserPersID);

        var dalData = new Data();
        dalData.ExecuteNonQueryByText(cSet.cnStringReport, com, tsql);
    }

    public void DicFileSave(int DicID, byte[] Dic, int UserPersID)
    {
        var tsql = @"
                        DECLARE @SCurdatetime nchar(16)
                        SELECT @SCurdatetime=SamanHIS.dbo.SCurdatetime()

                        UPDATE dbo.DicFile
                          SET Dic=@Dic,
                              UserPersID=@UserPersID,
							  ActionDate=@SCurdatetime
                          WHERE DicID=@DicID
                        ";

        var com = new SqlCommand();
        com.Parameters.Add("@DicID", DicID);
        //com.Parameters.Add("@Name", Name);
        com.Parameters.Add("@Dic", Dic);
        com.Parameters.Add("@UserPersID", UserPersID);

        var dalData = new Data();
        dalData.ExecuteNonQueryByText(cSet.cnStringReport, com, tsql);
    }

    public byte[] DocReportFileLoad(int TemplateID)
    {
        var tsql = @"
                        SELECT DOC
                        FROM dbo.DocTemplate
                        WHERE TemplateID=@TemplateID
                        ";

        var com = new SqlCommand();
        com.Parameters.Add("@TemplateID", TemplateID);

        var dalData = new Data();
        var dt = dalData.GetDataTableByText(cSet.cnStringReport, com, tsql);
        return (byte[])dt.Rows[0]["Doc"];
    }

    public DataTable FileDicLoadDB(int FileID1)
    {
        var TSQL = @"
                        SELECT * 
                        FROM dbo.vDicFile
                        WHERE ID=@ID
                         ";

        var com = new SqlCommand();
        com.Parameters.Add("@ID", FileID1);

        var dalData = new Data();
        return dalData.GetDataTableByText(cSet.cnStringReport, com, TSQL);
    }

    public DataTable FileDocLoadDB(int FileID1)
    {
        var TSQL = @"
                        SELECT * 
                        FROM dbo.vDocTemplate
                        WHERE ID=@ID
                         ";

        var com = new SqlCommand();
        com.Parameters.Add("@ID", FileID1);

        var dalData = new Data();
        return dalData.GetDataTableByText(cSet.cnStringReport, com, TSQL);
    }
}