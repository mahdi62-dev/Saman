using System.Data;

namespace Saman.BLL.Opr;

public class DT
{
    public void CreateTableServiceReg(DataTable dt)
    {
        if (dt == null) return;

        dt.Columns.Add(new DataColumn("CRUD", typeof(int)) { DefaultValue = 0 });
        dt.Columns.Add(new DataColumn("SRID", typeof(int)) { DefaultValue = 0 });
        dt.Columns.Add(new DataColumn("ParentSRID", typeof(int)) { DefaultValue = 0 });
        dt.Columns.Add(new DataColumn("RecNo", typeof(int)) { DefaultValue = 0 });
        dt.Columns.Add(new DataColumn("ServiceID", typeof(int)) { DefaultValue = 0 });
        dt.Columns.Add(new DataColumn("SMID", typeof(int)) { DefaultValue = 0 });
        dt.Columns.Add(new DataColumn("SMCode", typeof(int)) { DefaultValue = 0 });
        dt.Columns.Add(new DataColumn("ServiceCode", typeof(int)) { DefaultValue = 0 });
        dt.Columns.Add(new DataColumn("Name", typeof(string)));
        dt.Columns.Add(new DataColumn("Num", typeof(int)) { DefaultValue = 1 });
        dt.Columns.Add(new DataColumn("KBase", typeof(float)) { DefaultValue = 0 });
        dt.Columns.Add(new DataColumn("KFinal", typeof(float)) { DefaultValue = 0 });
        dt.Columns.Add(new DataColumn("KPer", typeof(int)) { DefaultValue = 0 });
        dt.Columns.Add(new DataColumn("OprItem", typeof(int)) { DefaultValue = 0 });
        dt.Columns.Add(new DataColumn("OprItemName", typeof(string)));
        dt.Columns.Add(new DataColumn("DrID", typeof(int)) { DefaultValue = 0 });
        dt.Columns.Add(new DataColumn("DrName", typeof(string)));
        dt.Columns.Add(new DataColumn("HDrID", typeof(int)) { DefaultValue = 0 });
        dt.Columns.Add(new DataColumn("IsCosmetic", typeof(bool)) { DefaultValue = false });
        dt.Columns.Add(new DataColumn("BookAnesK", typeof(double)) { DefaultValue = 0 });
    }

    public void CreateTablePageTeam(DataTable dt)
    {
        if (dt != null)
        {
            dt.Columns.Add("CRUD", typeof(int));
            dt.Columns["CRUD"].DefaultValue = 0;
            dt.Columns.Add("PersID", typeof(int));
            dt.Columns.Add("PersName");
            dt.Columns.Add("RoleID", typeof(int));
            dt.Columns.Add("RoleName");
        }
    }

    public void CreateTableModifier(DataTable dt)
    {
        if (dt != null)
        {
            dt.Columns.Add("RecNo");
            dt.Columns.Add("SMID", typeof(int));
            dt.Columns.Add("Code");
            dt.Columns.Add("Name");
            dt.Columns.Add("DrID");
            dt.Columns.Add("DrName");

            dt.Columns.Add("KType");

            dt.Columns.Add("KVal");
        }
    }
}