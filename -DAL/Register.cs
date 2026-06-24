using System.Data;
using System.Data.SqlClient;

namespace DAL;

public class Register
{
    public SqlDataReader AtendeeData(int atendeeId)
    {
        var parm = new SqlParameter[3];
        parm[0] = new SqlParameter("@AtendeeId", atendeeId);

        const string strSel = "Select *   FROM [vAtendee] WHERE AtendeeId=@AtendeeId";

        var drDrData = SqlHelper.ExecuteReader(cSet.cnString, CommandType.Text, strSel, parm);
        return drDrData;
    }
}