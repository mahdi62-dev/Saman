using System;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace Saman.BLL.FormSetting;

public class FormSetting
{
    public DataTable FormSettingLoad(int FormID)
    {
        var com = new SqlCommand();

        com.Parameters.AddWithValue("@CRUD", 1);
        com.Parameters.AddWithValue("@FormID", FormID);

        var dalData = new Data();
        return dalData.GetDataTableBySP(cSet.cnStringERP, com, "spFormSetting");
    }


    public int GetVal1(DataTable dtFormSetting, string UiName)
    {
        var val = 0;

        string filter;
        filter = "UiName='" + UiName + "'";

        if (dtFormSetting.Select(filter).Length > 0)
        {
            DataTable dt;
            dt = dtFormSetting.Select(filter).CopyToDataTable();

            if (dt.Rows.Count > 0) val = Convert.ToInt32(dt.Rows[0]["Val1"]);
        }

        return val;
    }


    public bool UiIsRequied(DataTable dtFormSetting, string UiName)
    {
        var val = false;

        string filter;
        filter = "UiName='" + UiName + "'";

        if (dtFormSetting.Select(filter).Length > 0)
        {
            DataTable dt;
            dt = dtFormSetting.Select(filter).CopyToDataTable();

            if (dt.Rows.Count > 0) val = Convert.ToBoolean(dt.Rows[0]["IsRequired"]);
        }

        return val;
    }

    public bool UiIsReadOnly(DataTable dtFormSetting, string UiName)
    {
        var val = false;

        string filter;
        filter = "UiName='" + UiName + "'";

        if (dtFormSetting.Select(filter).Length > 0)
        {
            DataTable dt;
            dt = dtFormSetting.Select(filter).CopyToDataTable();

            if (dt.Rows.Count > 0) val = Convert.ToBoolean(dt.Rows[0]["IsReadOnly"]);
        }

        return val;
    }

    public int UiDefaultValueInt(DataTable dtFormSetting, string UiName)
    {
        string val = null;

        string filter;
        filter = "UiName='" + UiName + "'";

        if (dtFormSetting.Select(filter).Length > 0)
        {
            DataTable dt;
            dt = dtFormSetting.Select(filter).CopyToDataTable();

            if (dt.Rows.Count > 0) val = dt.Rows[0]["DefaultValue"].ToString();
        }

        if (string.IsNullOrEmpty(val)) val = "0";

        return Convert.ToInt32(val);
    }

    public bool UiDefaultValueBoolean(DataTable dtFormSetting, string UiName)
    {
        string val = null;

        string filter;
        filter = "UiName='" + UiName + "'";

        if (dtFormSetting.Select(filter).Length > 0)
        {
            DataTable dt;
            dt = dtFormSetting.Select(filter).CopyToDataTable();

            if (dt.Rows.Count > 0) val = dt.Rows[0]["DefaultValue"].ToString();
        }

        if (string.IsNullOrEmpty(val)) val = "0";

        return Convert.ToBoolean(val);
    }

    public string UiDefaultValueString(DataTable dtFormSetting, string UiName)
    {
        string val = null;

        string filter;
        filter = "UiName='" + UiName + "'";

        if (dtFormSetting.Select(filter).Length > 0)
        {
            DataTable dt;
            dt = dtFormSetting.Select(filter).CopyToDataTable();

            if (dt.Rows.Count > 0) val = dt.Rows[0]["DefaultValue"].ToString();
        }

        //if (string.IsNullOrEmpty(val))
        //    val = "";

        return val;
    }
}