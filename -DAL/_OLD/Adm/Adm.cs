using System.Data;
using System.Data.SqlClient;

namespace DAL;

public class Adm
{
    //====================================================================================================================
    public DataTable GetAdmRec(string lName, string FName, string father)
    {
        var con = new SqlConnection { ConnectionString = cSet.cnString };
        var com = new SqlCommand { Connection = con };

        com.Parameters.Add("@LName", lName);
        com.Parameters.Add("@FName", FName);
        com.Parameters.Add("@Father", father);

        com.CommandType = CommandType.StoredProcedure;

        com.CommandText = "spAdmRecSearch";

        var adapt = new SqlDataAdapter { SelectCommand = com };
        var dt = new DataTable();
        adapt.Fill(dt);

        return dt;
    }
    //====================================================================================================================
}