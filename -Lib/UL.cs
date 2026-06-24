using System;
using System.Data;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using DAL;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using static System.Windows.Forms.InputLanguage;

namespace LIB;

internal class UL
{
    public static void CultureChange(byte languageCode)
    {
        var culture = CultureInfo.CreateSpecificCulture("fa-IR");

        switch (languageCode)
        {
            case 1:
                culture = CultureInfo.CreateSpecificCulture("en-Us");
                break;

            case 2:
                CultureInfo.CreateSpecificCulture("fa-IR");
                break;
        }


        // The following line provides localization for the application's user interface. 
        Thread.CurrentThread.CurrentUICulture = culture;

        // The following line provides localization for data formats.  
        Thread.CurrentThread.CurrentCulture = culture;

        // Set this culture as the default culture for all threads in this application.  
        // Note: The following properties are supported in the .NET Framework 4.5+ 
        CultureInfo.DefaultThreadCurrentCulture = culture;
        CultureInfo.DefaultThreadCurrentUICulture = culture;
    }


    public static void LangChange(byte languageCode)
    {
        switch (languageCode)
        {
            case 1:


                CurrentInputLanguage = FromCulture(new CultureInfo("en-us"));
                break;

            case 2:
                CurrentInputLanguage = FromCulture(new CultureInfo("fa-ir"));
                break;
        }
    }

    public static void FillLookup(LookUpEdit lue, string defaultVal, string cnString, bool isCache, string field,
        string dbTable, string dsTable, string where, string sortField)
    {
        Lib.LoadDataToDs(cnString, isCache, GL.ds, field, dbTable, dsTable, where, sortField);

        var dt = GL.ds.Tables[dsTable];
        var src = new BindingSource { DataSource = dt };

        lue.Properties.DataSource = src;

        if (defaultVal != null) lue.EditValue = Convert.ToInt32(defaultVal);
    }

    public static void SetLookupValueByText(LookUpEdit lue, string val)
    {
        var dt = new DataTable();

        var source = lue.Properties.DataSource;
        while (source is BindingSource) source = ((BindingSource)source).DataSource;

        if (source is DataTable) dt = (DataTable)source;

        var filter = lue.Properties.DisplayMember;
        filter = filter + "='" + val + "'";

        lue.EditValue = Convert.ToInt32(dt.Select(filter)[0][lue.Properties.ValueMember]);
    }

    public static void FillLookupBar(RepositoryItemLookUpEdit lue, BarEditItem barItem, string defaultVal,
        string cnString, bool isCache, string field, string dbTable, string dsTable, string where, string sortField)
    {
        Lib.LoadDataToDs(cnString, isCache, GL.ds, field, dbTable, dsTable, where, sortField);

        var dt = GL.ds.Tables[dsTable];
        var src = new BindingSource { DataSource = dt };

        lue.DataSource = src;

        if (defaultVal != null) barItem.EditValue = Convert.ToInt32(defaultVal);
    }

    public static void FillLookupRep(RepositoryItemLookUpEdit lue, string defaultVal, string cnString, bool isCache,
        string field, string dbTable, string dsTable, string where, string sortField)
    {
        Lib.LoadDataToDs(cnString, isCache, GL.ds, field, dbTable, dsTable, where, sortField);

        var dt = GL.ds.Tables[dsTable];
        var src = new BindingSource { DataSource = dt };

        lue.DataSource = src;

        //if (defaultVal != null)
        //    grdItem.EditValue = Convert.ToInt32(defaultVal);
    }
    //public static void LoadLookupFromDb(DevExpress.XtraEditors.LookUpEdit lue, string tableName, string fields,string where, string sortField,  bool isCache, bool isAD) 
    //{

    //    DAL.Lib.LoadDataToDs(isCache, DAL.GL.ds, fields, tableName, tableName,
    //        where, true, sortField);

    //    lue.Properties.DataSource = DAL.GL.ds.Tables[tableName];
    //    lue.Properties.ForceInitialize();
    //    lue.Properties.PopulateColumns();
    //    lue.Properties.DisplayMember = "Name";
    //    lue.Properties.ValueMember = "DrID";
    //    lue.Properties.PopupFilterMode = PopupFilterMode.Contains;


    //}

    //public static void FillCombo(DevExpress.XtraEditors.ComboBoxEdit cbo, string strWhere)
    //{
    //    const string fields = "*";
    //    DAL.List.LoadListToDs(GL.ds, fields, cbo.ToolTipTitle, strWhere, cbo.ToolTipTitle);
    //    App.UI.LoadComboList(GL.ds, cbo.ToolTipTitle, cbo);
    //}
}