using System.Data;
using DAL;

namespace WDAL;

public class wList
{
    //====================================================================================================================
    public void LoadListToDs(DataSet ds, string fields, string fromTable,
        string dsTable, bool isSort, string sortField)
    {
        if (ds.Tables[dsTable] != null) ds.Tables[dsTable].Clear();

        string strSel;
        if (isSort)
            strSel = "SELECT " + fields + " FROM " + fromTable + " Order by " + sortField;
        else
            strSel = "SELECT " + fields + " FROM " + fromTable + " Order by " + sortField;

        SqlHelper.FillDataset(cSet.cnString, CommandType.Text,
            strSel, ds, new[] { dsTable });
    }

    //====================================================================================================================
    public void LoadListToDs(DataSet ds, string fields, string fromTable,
        string dsTable)
    {
        if (ds.Tables[dsTable] != null) ds.Tables[dsTable].Clear();

        SqlHelper.FillDataset(cSet.cnString, CommandType.Text,
            "SELECT " + fields +
            " FROM " + fromTable,
            ds, new[] { dsTable });
    }

    //====================================================================================================================
    public void LoadListToDs(DataSet ds, string fields, string fromTable, string strWhere,
        string dsTable)
    {
        if (ds.Tables[dsTable] != null) ds.Tables[dsTable].Clear();

        var strSel = "SELECT " + fields +
                     " FROM " + fromTable +
                     " WHERE " + strWhere;


        SqlHelper.FillDataset(cSet.cnString, CommandType.Text, strSel,
            ds, new[] { dsTable });
    }
    //====================================================================================================================

    public void LoadListToDs(DataSet ds, string fields, string fromTable, string strWhere,
        string dsTable, string sortField)
    {
        if (ds.Tables[dsTable] != null) ds.Tables[dsTable].Clear();

        var strSel = "SELECT " + fields +
                     " FROM " + fromTable +
                     " WHERE " + strWhere +
                     " ORDER BY " + sortField;


        SqlHelper.FillDataset(cSet.cnString, CommandType.Text, strSel,
            ds, new[] { dsTable });
    }
    //
}