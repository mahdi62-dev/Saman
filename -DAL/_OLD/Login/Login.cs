using System;
using System.Data;
using System.Data.SqlClient;

namespace DAL.Login;

public class Login
{
    //====================================================================================================================
    public static bool CheckConnection
        (string serverName)
    {
        try
        {
            var con = new SqlConnection("Data Source=" + serverName +
                                        "; Database=HIS; Integrated Security=True; Connect Timeout=5");
            con.Open();
            return true;
        }

        catch (Exception)
        {
            return false;
        }
    }

    //====================================================================================================================
    public static string GetPassword
        (string userName)
    {
        var password = "";
        var parm = new SqlParameter[3];

        userName = userName.ToUpper();
        parm[0] = new SqlParameter("@fName", userName);

        try
        {
            password = SqlHelper.ExecuteScalar(cSet.cnString, CommandType.Text,
                "Select password From UserList  Where fName=@fName", parm).ToString();
        }
        catch (Exception)
        {
            password = null;
        }

        return password;
    }
    //====================================================================================================================

    public static int LoadUserId
        (string userName)
    {
        var parm = new SqlParameter[3];


        parm[0] = new SqlParameter("@code", SqlDbType.SmallInt);
        parm[1] = new SqlParameter("@userName", userName);
        parm[0].Direction = ParameterDirection.Output;

        var strSel = "Select @code=code From UserList Where fName=@userName";

        SqlHelper.ExecuteNonQuery(cSet.cnString, CommandType.Text,
            strSel, parm);

        // return Convert.ToInt32(parm[0].Value);

        return int.Parse(parm[0].Value.ToString());
    }
    //====================================================================================================================
}