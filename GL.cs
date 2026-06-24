using System.Data;

namespace Saman;

internal class GL
{
    public static bool IsDevelopStatus = false;

    public static DataSet ds = new();

    public static DataTable dtEncCur = new();
    public static DataTable dtPersCur = new();

    public static int ErpCurPersID = 0;

    public static DataTable dtResuInfo = new();
    public static int UserID = 0;
    public static int UserPersID = 0;
    public static string UserPersName = "";
    public static string ResuName = "";
    public static string Drowssap = "";
    public static int UserIsDisable = 0;

    public static DataTable dtSACUserPermission = new();
    public static DataTable dtSettingApp = new();

    public static string key = "b14cz5898a4e4138bbce2ea2315x1916";

    public static void EncCurStart()
    {
        var colDepID = new DataColumn("CurDepID", typeof(int)) { DefaultValue = 0 };
        dtEncCur.Columns.Add(colDepID);
        colDepID.SetOrdinal(0);

        if (!dtEncCur.Columns.Contains("CurDepID"))
        {
            dtEncCur.Columns.Add(colDepID);
            colDepID.SetOrdinal(0);
        }
    }

    public static void SetEncCurVal(int CurDepID, DataTable TableEncCur)
    {
        if (!TableEncCur.Columns.Contains("CurDepID"))
        {
            var colDepID = new DataColumn("CurDepID", typeof(int)) { DefaultValue = CurDepID };
            TableEncCur.Columns.Add(colDepID);
            colDepID.SetOrdinal(0);
        }


        if (!dtEncCur.Columns.Contains("CurDepID"))
        {
            var colDepID = new DataColumn("CurDepID", typeof(int)) { DefaultValue = CurDepID };
            dtEncCur.Columns.Add(colDepID);
            colDepID.SetOrdinal(0);
        }


        if (dtEncCur.Rows.Count == 0)
        {
            dtEncCur = TableEncCur.Copy();
        }
        else
        {
            var rows = dtEncCur.Select("CurDepID=" + CurDepID);

            foreach (var row in rows) row.Delete();

            dtEncCur.AcceptChanges();

            foreach (DataRow dr in TableEncCur.Rows) dtEncCur.Rows.Add(dr.ItemArray);
        }
    }

    public static void PersCurStart()
    {
        var colModID = new DataColumn("CurModID", typeof(int)) { DefaultValue = 0 };
        dtPersCur.Columns.Add(colModID);
        colModID.SetOrdinal(0);

        if (!dtPersCur.Columns.Contains("CurModID"))
        {
            dtPersCur.Columns.Add(colModID);
            colModID.SetOrdinal(0);
        }
    }
}