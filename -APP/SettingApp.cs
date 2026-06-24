using System;
using System.Data;

namespace Saman.APP.SettingApp;

public class SettingApp
{
    public bool SettingAppValBool(int ModID, int DepID, int SetID)
    {
        var val = false;

        if (GL.dtSettingApp.Rows.Count == 0) return false;

        string filter;
        filter = "ModID=" + ModID + " AND DepID=" + DepID + " AND SetID=" + SetID;

        if (GL.dtSettingApp.Select(filter).Length > 0)
        {
            DataTable dt;
            dt = GL.dtSettingApp.Select(filter).CopyToDataTable();

            if (dt.Rows.Count > 0) val = Convert.ToBoolean(dt.Rows[0]["Val"]);
        }

        return val;
    }

    public int SettingAppValInt(int ModID, int DepID, int SetID)
    {
        var val = 0;

        if (GL.dtSettingApp.Rows.Count == 0) return 0;

        string filter;
        filter = "ModID=" + ModID + " AND DepID=" + DepID + " AND SetID=" + SetID;

        if (GL.dtSettingApp.Select(filter).Length > 0)
        {
            DataTable dt;
            dt = GL.dtSettingApp.Select(filter).CopyToDataTable();

            if (dt.Rows.Count > 0) val = Convert.ToInt32(dt.Rows[0]["Val"]);
        }

        return val;
    }
}