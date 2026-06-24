using System;
using System.Data.SqlClient;

namespace DAL;

public class cSet
{
    public static int AppID;

    public static string cnString = "Data source=.; Database=SamanHIS;Integrated Security=True; Connect Timeout=180";
    public static string cnStringA = "Data source=.; Database=SamanHIS;Integrated Security=True; Connect Timeout=180;";

    public static string cnStringERP =
        "Data source=.; Database=SamanHIS;Integrated Security=True; Connect Timeout=180;";

    public static string cnStringReport =
        "Data source=.; Database=SamanHIS;Integrated Security=True; Connect Timeout=180;";

    public static string cnStringPortal =
        "Data source=.; Database=SamanPortal;Integrated Security=True; Connect Timeout=180;";


    public static string _Drowssap;

    private static void DrowssapSet()
    {
        string myStr = null;

        switch (AppID)
        {
            case 1:
                myStr = "Alfa@2020#";
                break;

            case 2:
                myStr = "Zahra@2020#";
                break;

            case 3:
                myStr = "Besharati@2020#";
                break;

            case 4:
                myStr = "Petct@2020";
                break;
        }

        _Drowssap = myStr;
    }

    public static void SetCon(string serverName)
    {
        DrowssapSet();

        cnString = "Data source=" + serverName +
                   "; Database=SamanHIS;" +
                   "Integrated Security=False;Persist Security Info=True;User ID=sa; Password=" + _Drowssap +
                   "; Connect Timeout=180;";
    }


    public static void SetConA(string serverName)
    {
        DrowssapSet();

        cnStringA = "Data source=" + serverName +
                    "; Database=SamanHIS;" +
                    "Integrated Security=False;Persist Security Info=True;User ID=sa; Password=" + _Drowssap +
                    "; Connect Timeout=180;";
    }


    public static void SetConERP(string serverName)
    {
        DrowssapSet();

        cnStringERP = "Data source=" + serverName +
                      "; Database=SamanERP;" +
                      "Integrated Security=False;Persist Security Info=True;User ID=sa; Password=" + _Drowssap +
                      "; Connect Timeout=180;";
    }

    public static void SetConReport(string serverName)
    {
        DrowssapSet();

        cnStringReport = "Data source=" + serverName +
                         "; Database=SamanReport;" +
                         "Integrated Security=False;Persist Security Info=True;User ID=sa; Password=" + _Drowssap +
                         "; Connect Timeout=180;";
    }

    public static void SetConDC(string serverName)
    {
        DrowssapSet();

        cnStringReport = "Data source=" + serverName +
                         "; Database=SamanDC;" +
                         "Integrated Security=False;Persist Security Info=True;User ID=sa; Password=" + _Drowssap +
                         "; Connect Timeout=180;";
    }

    public static void SetConPortal(string serverName)
    {
        DrowssapSet();

        cnStringReport = "Data source=" + serverName +
                         "; Database=SamanPortal;" +
                         "Integrated Security=False;Persist Security Info=True;User ID=sa; Password=" + _Drowssap +
                         "; Connect Timeout=180;";
    }

    private static void OpenSqlConnectioncnString()
    {
        var connectionString = cnString;
        using (var connection = new SqlConnection(connectionString))
        {
            connection.Open();
            Console.WriteLine("State: {0}", connection.State);
            Console.WriteLine("ConnectionTimeout: {0}",
                connection.ConnectionTimeout);
        }
    }
}