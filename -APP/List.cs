using System.Data;

namespace App;

public class List
{
    //=========================================================================
    public static void LoadList(DataSet ds, string tableName)
    {
        const string fields = "Code, Name";
        //DAL.List.LoadListToDs(ds, fields, tableName, tableName);
        DAL.List.LoadListToDs(ds, fields, tableName, tableName);
    }

    //=========================================================================
    public static void LoadList(DataSet ds, string tableName, string dsTableName)
    {
        const string fields = "Code, Name, Explain";
        DAL.List.LoadListToDs(ds, fields, tableName, "Code>0", dsTableName);
    }
    //=========================================================================


    #region ServiceProp

    public static void LoadServiceTypeList(DataSet ds)
    {
        const string fields = "code, Name";
        DAL.List.LoadListToDs(ds, fields, "ServiceTypeList", "ServiceTypeList");
    }
    //=========================================================================

    public static void LoadServiceGroupList(DataSet ds)
    {
        const string fields = "code, Name";
        DAL.List.LoadListToDs(ds, fields, "ServiceGroupList", "ServiceGroupList");
    }

    //=========================================================================
    public static void LoadBimehPayList(DataSet ds)
    {
        const string fields = "code, Name";
        DAL.List.LoadListToDs(ds, fields, "BimehPayList", "BimehPayList");
    }

    //=========================================================================
    public static void LoadPayList(DataSet ds)
    {
        const string fields = "code, Name";
        DAL.List.LoadListToDs(ds, fields, "PayList", "PayList");
    }
    //=========================================================================

    #endregion
}