using System.Data;
using System.Data.SqlClient;
using DAL;

namespace Saman.BLL.Timex;

public class Timex
{
    public DataTable TimexPersShiftLoad(int CRUD, int Year, int Mon, int DepId, object PersId, int UserPersID)
    {
        var com = new SqlCommand();

        com.Parameters.AddWithValue("@CRUD", CRUD);
        com.Parameters.AddWithValue("@Year", Year);
        com.Parameters.AddWithValue("@Mon", Mon);
        com.Parameters.AddWithValue("@DepId", DepId);
        com.Parameters.AddWithValue("@PersId", PersId);

        com.Parameters.AddWithValue("@UserPersID", UserPersID);

        var dalData = new Data();
        return dalData.GetDataTableBySP(cSet.cnStringERP, com, "spTimexShiftPers");
    }


    public DataTable TimexPersShiftSave(int CRUD, int Year, int Mon, int DepId, int PersId, int D1,
        int D2, int D3, int D4, int D5, int D6, int D7, int D8, int D9, int D10, int D11,
        int D12, int D13, int D14, int D15, int D16, int D17, int D18, int D19, int D20, int D21,
        int D22, int D23, int D24, int D25, int D26, int D27, int D28, int D29, int D30, int D31,
        int UserPersID)
    {
        var com = new SqlCommand();

        com.Parameters.AddWithValue("@CRUD", CRUD);
        com.Parameters.AddWithValue("@Year", Year);
        com.Parameters.AddWithValue("@Mon", Mon);
        com.Parameters.AddWithValue("@DepId", DepId);
        com.Parameters.AddWithValue("@PersId", PersId);
        com.Parameters.AddWithValue("@D1", D1);
        com.Parameters.AddWithValue("@D2", D2);
        com.Parameters.AddWithValue("@D3", D3);
        com.Parameters.AddWithValue("@D4", D4);
        com.Parameters.AddWithValue("@D5", D5);
        com.Parameters.AddWithValue("@D6", D6);
        com.Parameters.AddWithValue("@D7", D7);
        com.Parameters.AddWithValue("@D8", D8);
        com.Parameters.AddWithValue("@D9", D9);
        com.Parameters.AddWithValue("@D10", D10);
        com.Parameters.AddWithValue("@D11", D11);
        com.Parameters.AddWithValue("@D12", D12);
        com.Parameters.AddWithValue("@D13", D13);
        com.Parameters.AddWithValue("@D14", D14);
        com.Parameters.AddWithValue("@D15", D15);
        com.Parameters.AddWithValue("@D16", D16);
        com.Parameters.AddWithValue("@D17", D17);
        com.Parameters.AddWithValue("@D18", D18);
        com.Parameters.AddWithValue("@D19", D19);
        com.Parameters.AddWithValue("@D20", D20);
        com.Parameters.AddWithValue("@D21", D21);
        com.Parameters.AddWithValue("@D22", D22);
        com.Parameters.AddWithValue("@D23", D23);
        com.Parameters.AddWithValue("@D24", D24);
        com.Parameters.AddWithValue("@D25", D25);
        com.Parameters.AddWithValue("@D26", D26);
        com.Parameters.AddWithValue("@D27", D27);
        com.Parameters.AddWithValue("@D28", D28);
        com.Parameters.AddWithValue("@D29", D29);
        com.Parameters.AddWithValue("@D30", D30);
        com.Parameters.AddWithValue("@D31", D31);

        com.Parameters.AddWithValue("@UserPersID", UserPersID);

        var dalData = new Data();
        return dalData.GetDataTableBySP(cSet.cnStringERP, com, "spTimexShiftPers");
    }

    public DataTable DayOffLoad(object CRUD, int PersID)
    {
        var com = new SqlCommand();
        com.Parameters.Add("@CRUD", CRUD);
        com.Parameters.Add("@PersID", PersID);

        var dalData = new Data();
        return dalData.GetDataTableBySP(cSet.cnStringERP, com, "spTimexDayOffSave");
    }
}