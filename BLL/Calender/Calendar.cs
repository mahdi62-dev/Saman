using System.Data;
using System.Data.SqlClient;
using DAL;

namespace Saman.BLL.Calendar;

public class Calendar
{
    public DataTable CalendarData(int CRUD, int Year, int Mon)
    {
        var com = new SqlCommand();

        com.Parameters.AddWithValue("@CRUD", CRUD);
        com.Parameters.AddWithValue("@Year", Year);
        com.Parameters.AddWithValue("@Mon", Mon);


        var dalData = new Data();
        return dalData.GetDataTableBySP(cSet.cnStringERP, com, "spCalendar");
    }
}