using System.Data;
using System.Data.SqlClient;
using DAL;
using Stimulsoft.Report;

namespace Saman.BLL.Report;

public class Report
{
    public DataTable RepListAllLoad()
    {
        var tsql = @"
                        SELECT *
                        FROM SamanReport.dbo.vReport
                        WHERE 1=1
                        AND IsRep=1
                        AND RepID>0
                        ";

        var com = new SqlCommand();
        //com.Parameters.AddWithValue("@RepID", RepID);

        var dalData = new Data();
        return dalData.GetDataTableByText(cSet.cnStringReport, com, tsql);
    }

    public DataTable RepLoad(int RepID)
    {
        var tsql = @"
                        SELECT *
                        FROM SamanReport.dbo.vReport
                        WHERE 1=1
                        AND IsRep=1
                        AND RepID=@RepID
                        ";

        var com = new SqlCommand();
        com.Parameters.AddWithValue("@RepID", RepID);

        var dalData = new Data();
        return dalData.GetDataTableByText(cSet.cnStringReport, com, tsql);
    }

    public DataTable RepRun(int RepType, string Date1, string Date2, int Year, int Mon, object TSQL)
    {
        var com = new SqlCommand();
        com.Parameters.AddWithValue("@RepType", RepType);

        com.Parameters.AddWithValue("@Date1", Date1);
        com.Parameters.AddWithValue("@Date2", Date2);

        com.Parameters.AddWithValue("@Year", Year);
        com.Parameters.AddWithValue("@Mon", Mon);

        com.Parameters.AddWithValue("@TSQL", TSQL);

        var dalData = new Data();
        return dalData.GetDataTableBySP(cSet.cnStringReport, com, "spRunTSQL");
    }

    public DataTable RepFilterLoad(int RepID)
    {
        var tsql = @"
                        SELECT *
                        FROM SamanReport.dbo.vReportFilter
                        WHERE 1=1
                        --AND IsRep=1
                        AND RepID=@RepID
                        ";

        var com = new SqlCommand();
        com.Parameters.AddWithValue("@RepID", RepID);

        var dalData = new Data();
        return dalData.GetDataTableByText(cSet.cnStringReport, com, tsql);
    }

    public void RepRptFileSave(int RepID, string FileName, byte[] file, int UserPersID)
    {
        var tsql = @"
                        DECLARE @SCurdatetime nchar(16)
                        SELECT @SCurdatetime=SamanHIS.dbo.SCurdatetime()

                        UPDATE dbo.ReportFile
                          SET FileName=@FileName,
                              RptFile=@RptFile,
                              UserPersID=@UserPersID,
							  ActionDate=@SCurdatetime
                          WHERE RepID=@RepID
                        ";

        var com = new SqlCommand();
        com.Parameters.Add("@RepID", RepID);
        com.Parameters.Add("@FileName", FileName);
        com.Parameters.Add("@RptFile", file);
        com.Parameters.Add("@UserPersID", UserPersID);

        var dalData = new Data();
        dalData.ExecuteNonQueryByText(cSet.cnStringReport, com, tsql);
    }

    public StiReport RepRptFileLoad(int RepID)
    {
        var TSQL = @"
                        SELECT * 
                        FROM dbo.ReportFile
                        WHERE RepID=@RepID";

        var com = new SqlCommand();
        com.Parameters.Add("@RepID", RepID);


        var dalData = new Data();
        var dt = new DataTable();
        dt = dalData.GetDataTableByText(cSet.cnStringReport, com, TSQL);

        var report = new StiReport();

        report.Load((byte[])dt.Rows[0]["RptFile"]);

        return report;
    }

    public DataTable RepRptFileLoadDB(int RepID1)
    {
        var TSQL = @"
                        SELECT * 
                        FROM dbo.vReport
                        WHERE RepID=@RepID
                        AND RepType=2 ";

        var com = new SqlCommand();
        com.Parameters.Add("@RepID", RepID1);

        var dalData = new Data();
        return dalData.GetDataTableByText(cSet.cnStringReport, com, TSQL);
    }
}