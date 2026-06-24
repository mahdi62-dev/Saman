using System.Data;

namespace Saman.BLL.Payroll;

public class DT
{
    public void CreateTableDayOff(DataTable dt)
    {
        if (dt == null) return;

        dt.Columns.Add(new DataColumn("CRUD", typeof(int)) { DefaultValue = 0 });
        dt.Columns.Add(new DataColumn("Year", typeof(int)) { DefaultValue = 0 });
        dt.Columns.Add(new DataColumn("Mon", typeof(int)) { DefaultValue = 0 });
        dt.Columns.Add(new DataColumn("PersID", typeof(int)) { DefaultValue = 0 });
        dt.Columns.Add(new DataColumn("PersName", typeof(string)) { DefaultValue = 0 });
        dt.Columns.Add(new DataColumn("DayOffType", typeof(int)) { DefaultValue = 0 });
        dt.Columns.Add(new DataColumn("DayOffTypeName", typeof(string)) { DefaultValue = 0 });
        dt.Columns.Add(new DataColumn("StartDate", typeof(string)) { DefaultValue = 0 });
        dt.Columns.Add(new DataColumn("EndDate", typeof(string)) { DefaultValue = 0 });
        dt.Columns.Add(new DataColumn("Val", typeof(int)) { DefaultValue = 0 });
    }
}