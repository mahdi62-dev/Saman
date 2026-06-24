//using CrystalDecisions.Shared;

namespace UI;

internal class UCrysPrint
{
    public static string ServerName;
    public static string DbName;
    public static string UserId;
    public static string Password;

    //==============================================================================================
    public static void Print(string rPath, string selExpert)
    {
        //var reportDocument = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
        //reportDocument.Load(rPath);
        //for (int i = 0; i < reportDocument.Database.Tables.Count; i++)
        //{
        //    TableLogOnInfo tableLogOnInfo = reportDocument.Database.Tables[i].LogOnInfo;

        //    ConnectionInfo conInfo = tableLogOnInfo.ConnectionInfo;
        //    conInfo.ServerName = ServerName;
        //    conInfo.DatabaseName = DbName;
        //    conInfo.UserID = UserId;
        //    conInfo.Password = Password;
        //    reportDocument.Database.Tables[i].ApplyLogOnInfo(tableLogOnInfo);
        //}

        //reportDocument.RecordSelectionFormula = selExpert;
        //reportDocument.PrintToPrinter(1, false, 0, 0);
    }
    //==============================================================================================
}