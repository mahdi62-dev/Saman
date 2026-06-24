using System;
using System.Data;
using App;
using DAL;
using DevExpress.XtraEditors;
using DevExpress.XtraTab;
using LIB;
using Saman.BLL.Def.ServiceDef;
using List = Saman.BLL.Common.List;

namespace Saman.UC;

public partial class UcServiceDefMain : XtraUserControl
{
    private readonly List _bllList = new();

    private readonly ServiceDef _bllServiceList = new();
    private readonly string _Date1;
    private readonly string _Date2;
    private readonly DataTable _dtRepFilter = new();
    private readonly DataTable _dtRepResult = new();
    private readonly string _EndDate;
    private readonly bool _IsLandscape;

    private readonly string _PaperSize;

    private readonly string _RepFilter, _RepFilterDescription;


    private readonly string _RepFullName;
    private readonly string _RepTitle;
    private readonly string _StartDate;
    private readonly string _Time1;
    private readonly string _Time2;

    private readonly string _TSQL;
    private DataTable _dtServiceList = new();
    private DataRow _rowServiceSelect;
    private int _ServiceCategoryID, _ServiceType, _ServiceID;
    private string _ServiceCategoryName, _ServiceName, _ServiceNameLong, _ServiceCode, _ServiceCode2;
    private string _strCurDate;

    private string _strCurDateTime;
    private string _strCurTime;


    public UcServiceDefMain()
    {
        InitializeComponent();
    }

    public int _Year { get; set; }
    public int _Mon { get; set; }
    public string _MonName { get; set; }
    public string _YearText { get; set; }

    private void UcServiceDefMain_Load(object sender, EventArgs e)
    {
        ServiceDefMAinTreeLoad(false);
        FillLookupLoad();
        ShortKey();
        MaskSet();
    }

    private void MaskSet()
    {
        txtProK.Properties.Mask.EditMask = App.UI.MaskNumWithDecimal;
        txtTechK.Properties.Mask.EditMask = App.UI.MaskNumWithDecimal;
        txtAnesK.Properties.Mask.EditMask = App.UI.MaskNumWithDecimal;
        txtPrice.Properties.Mask.EditMask = App.UI.MaskNum;
    }

    private void FillLookupLoad()
    {
        UL.FillLookup(lueServiceType, "0", cSet.cnStringA, false, "ID,Name", "vADR", "ServiceType",
            "ADName='ServiceType'", null);
        UL.FillLookup(lueServiceCategory, "0", cSet.cnStringA, false, "ID,Name", "vServiceCategory",
            "ServiceCategory", null, null);
        UL.FillLookup(lueServiceUnit, "0", cSet.cnStringA, false, "ID,Name", "vADR", "Unit", "ADName='Unit'", null);
        UL.FillLookup(lueServiceUnit2, "0", cSet.cnStringA, false, "ID,Name", "vADR", "Unit", "ADName='Unit'",
            null);
    }

    private void ShortKey()
    {
        //msiCancel.Click += btnCancel_Click;
        //msiRepPrint.Click += btnRepPrint_Click;
        //msiRepRun.Click += btnRepRun_Click;
        //msiReturn.Click += btnReturn_Click;


        panMain.Focus();
    }

    private void grvServiceList_Click(object sender, EventArgs e)
    {
        if (grvServiceList.FocusedRowHandle < 0) return;

        _rowServiceSelect = grvServiceList.GetDataRow(grvServiceList.FocusedRowHandle);
        lblTitle2.Text = _rowServiceSelect["Name"] + " قیمت : " + _rowServiceSelect["Price"];
    }

    private void tabmain_SelectedPageChanged(object sender, TabPageChangedEventArgs e)
    {
        var tabName = tabmain.SelectedTabPage.Name;
        if (tabName == "tabServiceList")
        {
            lblEdit.Visible = false;
            lblSave.Visible = false;
            chkIsDisableShow.Visible = true;
        }

        if (tabName == "tabServiceProp")
        {
            lblEdit.Visible = true;
            lblSave.Visible = true;
            chkIsDisableShow.Visible = false;
        }
    }

    private void lblEdit_Click(object sender, EventArgs e)
    {
        flpServiceItemMain.Enabled = true;
    }

    private void ServiceDefMAinTreeLoad(bool IsDisableLoad)
    {
        var IsDisable = IsDisableLoad ? "1" : "0";

        var dtTree = _bllList.LoadTable(
            "*",
            "SamanHIS.dbo.vServiceCategory",
            "IsDisable=" + IsDisable
        );


        treServiceMainCategory.KeyFieldName = "ID";
        //treReport.ParentFieldName = "ParentID";

        treServiceMainCategory.DataSource = dtTree;
        treServiceMainCategory.RowHeight = 23;

        if (dtTree.Rows.Count > 0)
        {
            treServiceMainCategory.Nodes.FirstNode.Expanded = true;
            treServiceMainCategory.Nodes.TreeList.ExpandAll();
        }

        var appSDate = new SDate();
        _strCurDateTime = appSDate.ShamsiDate();
        _strCurDate = _strCurDateTime.Substring(0, 10);
        _strCurTime = _strCurDateTime.Substring(11, 5);
    }


    private void treServiceMainCategory_SelectionChanged(object sender, EventArgs e)
    {
        ServiceListLoad();
    }

    private void treServiceMainCategory_Click(object sender, EventArgs e)
    {
        ServiceListLoad();
        tabServiceList.Show();
    }

    private void ServiceListLoad()
    {
        FormNull();

        var rowView = (DataRowView)treServiceMainCategory.GetRow(treServiceMainCategory.FocusedNode.Id);
        var row = rowView.Row;

        _ServiceCategoryID = Convert.ToInt32(row["ID"].ToString());
        _ServiceCategoryName = row["Name"].ToString();
        lblTitle1.Text = _ServiceCategoryName;

        _dtServiceList = _bllServiceList.ServiceListLoad(_ServiceCategoryID, false, false);
        grdServiceList.DataSource = _dtServiceList;

        if (_ServiceCategoryID != 0)
            panServiceToolbar.Visible = true;
        else
            panServiceToolbar.Visible = false;
    }

    private void FormNull()
    {
        lblTitle2.Text = null;
    }

    private void grvServiceList_DoubleClick(object sender, EventArgs e)
    {
        ServiceItemSelect();
        tabServiceProp.Show();
        flpServiceItemMain.Enabled = false;
    }

    private void ServiceItemSelect()
    {
        if (grvServiceList.FocusedRowHandle < 0) return;

        DataRow rowSelect;

        rowSelect = grvServiceList.GetDataRow(grvServiceList.FocusedRowHandle);

        _ServiceID = Convert.ToInt32(rowSelect["ServiceID"]);
        lueServiceType.EditValue = _ServiceType = Convert.ToInt32(rowSelect["ServiceType"]);
        lueServiceCategory.EditValue = _ServiceCategoryID = Convert.ToInt32(rowSelect["ServiceCategory"]);

        txtServiceName.Text = _ServiceName = rowSelect["Name"].ToString();
        txtServiceNameLong.Text = _ServiceNameLong = rowSelect["NameLong"].ToString();

        txtServiceCode.Text = _ServiceCode = rowSelect["ServiceCode"].ToString();
        txtServiceCode2.Text = _ServiceCode2 = rowSelect["ServiceCode2"].ToString();

        txtProK.Text = rowSelect["ProK"].ToString();
        txtTechK.Text = rowSelect["TechK"].ToString();
        txtAnesK.Text = rowSelect["AnesK"].ToString();
        txtPrice.Text = rowSelect["Price"].ToString();

        chkIsDisable.EditValue = (bool)rowSelect["IsDisable"];
        chkIsManualPrice.EditValue = (bool)rowSelect["IsManualPrice"];

        lueServiceUnit.EditValue = Convert.ToInt32(rowSelect["ServiceUnit"]);
        lueServiceUnit2.EditValue = Convert.ToInt32(rowSelect["ServiceUnit2"]);
    }
}