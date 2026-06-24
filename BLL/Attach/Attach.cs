using System.Data;
using System.Data.SqlClient;
using DAL;

namespace Saman.BLL.Attach;

public class Attach
{
    public DataTable AttFile(int CRUD, int EncID, int PageID, int DepID, int EAID, string FileName, object AttFileType,
        object AttTag, byte[] AttFile, int UserPersID)
    {
        var com = new SqlCommand();
        com.Parameters.Add("@CRUD", CRUD);
        com.Parameters.Add("@EncID", EncID);
        com.Parameters.Add("@PageID", PageID);
        com.Parameters.Add("@DepID", DepID);
        com.Parameters.Add("@EAID", EAID);
        com.Parameters.Add("@FileName", FileName);
        com.Parameters.Add("@AttFileType", AttFileType);
        com.Parameters.Add("@AttTag", AttTag);
        com.Parameters.Add("@AttFile", AttFile);
        com.Parameters.Add("@UserPersID", UserPersID);

        var dalData = new Data();
        return dalData.GetDataTableBySP(cSet.cnStringERP, com, "spAttFile");
    }


    public DataTable AttFileListByEncID(int EncID)
    {
        var TSQL = @"

                    SELECT  [EAID]
                    FROM [SamanDC].[dbo].[AttFile]
                    WHERE EncID = @EncID

            ";

        var com = new SqlCommand();
        com.Parameters.Add("@EncID", EncID);


        var dalData = new Data();
        return dalData.GetDataTableByText(cSet.cnStringERP, com, TSQL);
    }

    public DataTable AttFileByEAID(long EAID)
    {
        var TSQL = @"

                    SELECT  *
                    FROM [SamanDC].[dbo].[AttFile]
                    WHERE EAID = @EAID

            ";

        var com = new SqlCommand();
        com.Parameters.Add("@EAID", EAID);


        var dalData = new Data();
        return dalData.GetDataTableByText(cSet.cnStringERP, com, TSQL);
    }
}