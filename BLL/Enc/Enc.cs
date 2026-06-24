using System;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace Saman.BLL.Enc;

public class EncList
{
    public DataTable EncSearch(int encId)
    {
        var com = new SqlCommand();
        com.Parameters.AddRange(new[]
        {
            new SqlParameter("@EncID", encId)
        });
        var dalData = new Data();
        return dalData.GetDataTableBySP(cSet.cnStringA, com, "spEncSearch");
    }

    public DataTable EncSearch(long encId, bool IsContactDisplay)
    {
        var com = new SqlCommand();
        com.Parameters.Add("@EncID", encId);
        com.Parameters.Add("@IsContactDisplay", IsContactDisplay);


        var dalData = new Data();
        return dalData.GetDataTableBySP(cSet.cnStringA, com, "spEncSearch");
    }


    public int EncIDLast()
    {
        var encIDLast = 0;

        var tsql = "SELECT ISNULL(MAX(EncID),0) AS EncID FROM Enc";
        var com = new SqlCommand();

        var dalData = new Data();
        var dt = dalData.GetDataTableByText(cSet.cnStringA, com, tsql);

        if (dt.Rows.Count > 0)
            encIDLast = Convert.ToInt32(dt.Rows[0]["EncID"]);

        return encIDLast;
    }
}