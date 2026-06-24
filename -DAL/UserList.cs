using System.Data;
using System.Data.SqlClient;

namespace DAL;

public class UserList
{
    //==============================================================================================================
    public static void DisableUser
        (int userId)
    {
        var parm = new SqlParameter[1];
        parm[0] = new SqlParameter("@UserId", userId);

        var strUpd = "UPDATE UserList Set isDisable=1 WHERE UserId=@UserId";

        SqlHelper.ExecuteNonQuery(cSet.cnString, CommandType.Text, strUpd, parm);
    }

    //====================================================================================================================
    public static void UpdRecord
    (int userId, string userName, string name, string password, bool atendee, bool atendeeList, bool define,
        bool register)
    {
        var parm = new SqlParameter[10];
        parm[0] = new SqlParameter("@UserId", userId);
        parm[1] = new SqlParameter("@UserName", userName);
        parm[2] = new SqlParameter("@Name", name);
        parm[3] = new SqlParameter("@Password", password);
        parm[4] = new SqlParameter("@Atendee", atendee);
        parm[5] = new SqlParameter("@AtendeeList", atendeeList);
        parm[6] = new SqlParameter("@Define", define);
        parm[7] = new SqlParameter("@Register", register);

        var strUpdate =
            "Update UserList Set UserName=@UserName, Name=@Name, Password=@Password, Atendee=@Atendee, " +
            "AtendeeList=@AtendeeList, Define=@Define, Register=@Register WHERE UserId=@UserId";
        SqlHelper.ExecuteNonQuery(cSet.cnString, CommandType.Text, strUpdate, parm);
    }

    //====================================================================================================================
    public static int GetMaxCode()
    {
        const string strSelect = "SELECT IsNull(Max(UserId)+1, 0) FROM UserList";

        var maxCode = int.Parse(SqlHelper.ExecuteScalar(cSet.cnString,
            CommandType.Text,
            strSelect).ToString());
        return maxCode;
    }

    //====================================================================================================================
    public static void InsRecord
    (int userId, string userName, string name, string password, bool atendee, bool atendeeList, bool define,
        bool register)
    {
        var parm = new SqlParameter[10];
        parm[0] = new SqlParameter("@UserId", userId);
        parm[1] = new SqlParameter("@UserName", userName);
        parm[2] = new SqlParameter("@Name", name);
        parm[3] = new SqlParameter("@Password", password);
        parm[4] = new SqlParameter("@Atendee", atendee);
        parm[5] = new SqlParameter("@AtendeeList", atendeeList);
        parm[6] = new SqlParameter("@Define", define);
        parm[7] = new SqlParameter("@Register", register);

        var strIns = "Insert Into UserList " +
                     "(UserId, UserName, Name, Password) " +
                     "Select @UserId, @UserName, @Name, @Password";

        SqlHelper.ExecuteNonQuery(cSet.cnString, CommandType.Text,
            strIns, parm);
    }
}