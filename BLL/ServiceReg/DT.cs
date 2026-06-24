using System.Data;

namespace Saman.BLL.ServiceReg;

public class DT
{
    public void CreateTableServiceReg(DataTable dt)
    {
        dt.Columns.Add("RecNo", typeof(int));
        dt.Columns.Add("CRUD", typeof(int));
        dt.Columns.Add("SRID", typeof(int));
        dt.Columns["SRID"].DefaultValue = 0;
        dt.Columns.Add("PageID", typeof(int));
        dt.Columns.Add("DrID", typeof(int));
        dt.Columns["DrID"].DefaultValue = 0;
        dt.Columns.Add("DrName");
        dt.Columns.Add("ServiceID", typeof(int));
        dt.Columns.Add("ServiceCategory", typeof(int));

        dt.Columns.Add("Num", typeof(double));
        dt.Columns["Num"].DefaultValue = 0;


        dt.Columns.Add("TotalPrice", typeof(int));
        dt.Columns["TotalPrice"].DefaultValue = 0;

        dt.Columns.Add("IsManualPrice", typeof(bool));

        dt.Columns.Add("TotalAmount", typeof(double));
        dt.Columns["TotalAmount"].DefaultValue = 0;
        dt.Columns.Add("ServiceCode");
        dt.Columns.Add("ServiceName");

        dt.Columns.Add("Price", typeof(int));
        dt.Columns["Price"].DefaultValue = 0;

        dt.Columns.Add("ProAmount", typeof(int));
        dt.Columns["ProAmount"].DefaultValue = 0;
        dt.Columns.Add("TechAmount", typeof(int));
        dt.Columns["TechAmount"].DefaultValue = 0;
        dt.Columns.Add("PatientAmount", typeof(int));
        dt.Columns["PatientAmount"].DefaultValue = 0;
        dt.Columns.Add("DiscountAmount", typeof(int));
        dt.Columns["DiscountAmount"].DefaultValue = 0;
        dt.Columns.Add("ResultAmount", typeof(int));
        dt.Columns["ResultAmount"].DefaultValue = 0;
    }
}