using System;
using System.Data;

namespace DAL.App;

public class ShamsiDate
{
    //====================================================================================================================
    public static DateTime GetServerDate()
    {
        var curDateTime = SqlHelper.ExecuteScalar(cSet.cnString, CommandType.Text,
            "Select GetDate()").ToString();
        var serverDate = DateTime.Parse(curDateTime);
        return serverDate;
    }

    //====================================================================================================================

    public static string GetServerDateTime()
    {
        var curDateTime = SqlHelper.ExecuteScalar(cSet.cnString, CommandType.Text,
            "Select dbo.SCurDateTime()").ToString();
        // DateTime serverDate = DateTime.Parse(curDateTime);
        return curDateTime;
    }
}