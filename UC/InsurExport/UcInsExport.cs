using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using DAL;
using DevExpress.Data;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraTab;
using Saman.APP;
using Saman.BLL.InsExport;
using List = Saman.BLL.Common.List;

namespace Saman.UC;

public partial class UcInsurExport : XtraUserControl
{
    private readonly InsExport _bllInsExport = new();

    private readonly List _bllList = new();

    private readonly XMLTag _tag = new();
    private DataTable _dtInsExportPage;
    private DataTable _dtInsExportServiceReg;

    private DataTable _dtInsHeaderInfo;
    private int _InsID;
    private string _InsName;

    //BLL.Report.Report _bllReport = new BLL.Report.Report();
    //DataTable _dtRep = new DataTable();
    //DataTable _dtRepFilter = new DataTable();
    //DataTable _dtRepResult = new DataTable();

    //private string _TSQL;

    //private string _PaperSize;
    //private bool _IsLandscape;

    private string _RepFilter, _DateFilter1;


    public UcInsurExport()
    {
        InitializeComponent();
    }

    public int _Year { get; set; }
    public int _Mon { get; set; }
    public string _MonName { get; set; }

    private void UcInsurExport_Load(object sender, EventArgs e)
    {
        TreeLoadData();
        //FormNull();
        //ucDateFilter1.DateFilterType = 4;

        ucglueDep._DataSource = _bllList.LoadTable("DepID,Name", "SamanHIS.dbo.vDep", null);
    }

    private void TreeLoadData()
    {
        var dtTree = new DataTable();
        var tsql = @"
            SELECT *
              FROM [SamanHIS].[dbo].[vInsurance]
              WHERE IsDisable<>1 AND GroupCode NOT IN (10,1)
            ";

        var com = new SqlCommand();
        //com.Parameters.AddWithValue("@PersID", GL.UserPersID);

        var dalData = new Data();
        dtTree = dalData.GetDataTableByText(cSet.cnStringA, com, tsql);

        treReport.KeyFieldName = "InsuranceID";
        treReport.ParentFieldName = "ParentID";

        treReport.DataSource = dtTree;
        treReport.RowHeight = 23;

        if (dtTree.Rows.Count > 0)
        {
            treReport.Nodes.FirstNode.Expanded = true;
            treReport.Nodes.TreeList.ExpandAll();
        }
    }


    private void GetFilterControls()
    {
        _RepFilter = null;
        _DateFilter1 = null;

        if (panDate1.Visible) _DateFilter1 = ucDateFilter1._Filter;
    }

    private void SetGridFormat()
    {
        foreach (GridColumn col in grvRepRun.Columns)
        {
            col.DisplayFormat.FormatType = FormatType.Numeric;
            col.DisplayFormat.FormatString = "n0";
        }
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
    }

    private void grvRepRun_PrintInitialize(object sender, PrintInitializeEventArgs e)
    {
        //PrintingSystemBase pb = e.PrintingSystem as PrintingSystemBase;
        //DevExpress.XtraPrinting.PrintableComponentLink printableComponentLink = new DevExpress.XtraPrinting.PrintableComponentLink();
        //pb.PageSettings.Landscape = _IsLandscape;
        //pb.PageSettings.PaperKind = PaperKind.A4;
        //pb.Document.AutoFitToPagesWidth = 1;
        //pb.PageSettings.TopMargin = 100;
        //pb.PageSettings.BottomMargin = 100;
        //pb.PageSettings.LeftMargin = 40;
        //pb.PageSettings.RightMargin = 40;

        //grvRepRun.OptionsPrint.PrintHeader = true;
        //grvRepRun.OptionsPrint.PrintFooter = true;


        //(e.Link.PageHeaderFooter as PageHeaderFooter).Header.Content.Clear();
        //(e.Link.PageHeaderFooter as PageHeaderFooter).Footer.Content.Clear();

        //(e.Link.PageHeaderFooter as PageHeaderFooter).Header.LineAlignment = BrickAlignment.Center;
        //(e.Link.PageHeaderFooter as PageHeaderFooter).Footer.LineAlignment = BrickAlignment.Center;

        //(e.Link.PageHeaderFooter as PageHeaderFooter).Header.Font = new Font("IRANSansFaNum", 9, FontStyle.Regular);
        //(e.Link.PageHeaderFooter as PageHeaderFooter).Footer.Font = new Font("IRANSansFaNum", 8);

        //(e.Link.PageHeaderFooter as PageHeaderFooter).Header.Content.AddRange(new string[] { _InsName + " " + _MonName, _RepTitle, _EndDate + _StartDate });
        //(e.Link.PageHeaderFooter as PageHeaderFooter).Footer.Content.AddRange(new string[] { "[Date Printed]-[Time Printed] : تاریخ گزارش گیری  ", " [Pages #] از [Page #] : صفحه  ", "گزارش گیرنده : " + GL.UserPersName });


        //#region ExportSetting

        //if (chkPageHeaderRemove.Checked)
        //{
        //    (e.Link.PageHeaderFooter as PageHeaderFooter).Header.Content.Clear();
        //    (e.Link.PageHeaderFooter as PageHeaderFooter).Footer.Content.Clear();
        //}

        //if (chkPageColNameRemove.Checked)
        //{
        //    grvRepRun.OptionsPrint.PrintHeader = false;
        //}

        //if (chkPageColSummaryRemove.Checked)
        //{
        //    grvRepRun.OptionsPrint.PrintFooter = false;
        //}
        //#endregion
    }

    private void btnRepPrint_Click(object sender, EventArgs e)
    {
        grvRepRun.ShowRibbonPrintPreview();
    }

    private void btnRepRun_Click(object sender, EventArgs e)
    {
        GetFilterControls();

        var rowView = (DataRowView)treReport.GetRow(treReport.FocusedNode.Id);
        var row = rowView.Row;

        var InsuranceID = row["InsuranceID"].ToString();

        string strDep = null;
        if (ucglueDep._SelectedValues != null) strDep = ucglueDep._SelectedValuesStr;

        _dtInsExportPage = _bllInsExport.InsExportPageLoad(0, _DateFilter1, InsuranceID, strDep);
        _dtInsExportPage.TableName = "Page";
        //grdRepRun.DataSource = _dtInsExportPage;

        _dtInsExportServiceReg = _bllInsExport.InsExportServiceRegLoad(0, _DateFilter1, InsuranceID, strDep);
        _dtInsExportServiceReg.TableName = "ServiceReg";

        GridSet();

        string selectedDepID = null;
        if (ucglueDep._SelectedValues != null)
            if (ucglueDep._SelectedValues.Length == 1)
                selectedDepID = ucglueDep._SelectedValues[0].ToString();

        _dtInsHeaderInfo = _bllInsExport.InsHeaderInfo(selectedDepID);

        tabShowRep.Show();
        panInsExportToolbar.Visible = true;
    }

    private void GridSet()
    {
        var dsSet = new DataSet();
        dsSet.Tables.Add(_dtInsExportPage);
        dsSet.Tables.Add(_dtInsExportServiceReg);


        //Set up a master-detail relationship between the DataTables
        var keyColumn = dsSet.Tables["Page"].Columns["PageID"];
        var foreignKeyColumn = dsSet.Tables["ServiceReg"].Columns["PageID"];

        dsSet.Relations.Add("PageServiceReg", keyColumn, foreignKeyColumn);

        grdRepRun.DataSource = dsSet.Tables["Page"];
        grdRepRun.ForceInitialize();

        //Assign a CardView to the relationship


        grdRepRun.LevelTree.Nodes.Add("PageServiceReg", grvRepRunDetail);
        //Specify text to be displayed within detail tabs.
        grvRepRunDetail.ViewCaption = "سرویس ها";
    }

    private void GridViewSummary()
    {
        var colCount = grvRepRun.Columns.Count;
        string colFieldName;


        for (var i = 0; i < colCount; i++)
        {
            colFieldName = grvRepRun.Columns[i].FieldName;

            grvRepRun.Columns[i].Summary.Clear();
            grvRepRun.Columns[i].Summary.Add(SummaryItemType.Sum, colFieldName, "{0:n0}");


            if (colFieldName.Contains("میانگین"))
            {
                grvRepRun.Columns[i].Summary.Clear();
                grvRepRun.Columns[i].Summary.Add(SummaryItemType.Average, colFieldName, "میانگین {0:F0}");
                //grvRepRun.Columns[i].SummaryItem.
            }

            if (colFieldName.Contains("تعداد"))
            {
                grvRepRun.Columns[i].Summary.Clear();
                grvRepRun.Columns[i].Summary.Add(SummaryItemType.Sum, colFieldName, "{0:n0}");
                grvRepRun.Columns[i].Summary.View.Appearance.UpdateRightToLeft(false);
            }

            if (colFieldName.Contains("کد"))
            {
                grvRepRun.Columns[i].Summary.Clear();
                grvRepRun.Columns[i].Summary.Add(SummaryItemType.Count, colFieldName, "{0:n0}");
                grvRepRun.Columns[i].Summary.View.Appearance.UpdateRightToLeft(false);
            }
        }


        grvRepRun.BestFitColumns();
    }

    private void btnReturn_Click(object sender, EventArgs e)
    {
        tabRep.Show();
    }


    private void treReport_DoubleClick(object sender, EventArgs e)
    {
        btnRepRun.Enabled = false;

        var rowView = (DataRowView)treReport.GetRow(treReport.FocusedNode.Id);
        var row = rowView.Row;

        _InsID = Convert.ToInt32(row["InsuranceID"]);

        if (_InsID > 0)
        {
            _InsName = row["Name"].ToString();

            btnRepRun.Enabled = true;
            lblTitle2.Text = _InsName;

            InsReportLoad();
        }
    }

    private void InsReportLoad()
    {
        //FormNull();
        tabFilter.Show();
    }

    private void FormNull()
    {
        panDate1.Visible = false;
        panDep.Visible = false;
        panExportSetting.Visible = false;

        grdRepRun.DataSource = null;
        grvRepRun.Columns.Clear();
    }

    private void grvRepRun_CustomDrawFooter(object sender, RowObjectCustomDrawEventArgs e)
    {
    }

    private void grvRepRun_CellValueChanged(object sender, CellValueChangedEventArgs e)
    {
        var view = sender as GridView;
        e.Column.BestFit();
    }

    private void grvRepRun_CustomDrawFooterCell(object sender, FooterCellCustomDrawEventArgs e)
    {
        e.Bounds.Inflate(-1, -1);
        e.Appearance.TextOptions.HAlignment = HorzAlignment.Near;
        e.DefaultDraw();
        e.Cache.DrawRectangle(e.Cache.GetPen(Color.CornflowerBlue, 1), e.Bounds);
        e.Handled = true;
    }

    private string TagPage
    (
        string RecNo, string PageDate, string PageDate2, string InsExpDate, string InsMapID, string InsSerial,
        string InsPageNo, string GenderMapID, string DrRefRegID, string DrExpertType, string DrTechAssist,
        string Diagnose, string TotalAmount, string InsuranceAmount, string PatientAmount
    )
    {
        string strTag = null;
        strTag += _tag.TagStartGenerator("PH", null, true);

        strTag += _tag.TagGenerator("SQ", RecNo, false);
        strTag += _tag.TagGenerator("ND", PageDate, false);
        strTag += _tag.TagGenerator("RD", PageDate2, false);
        strTag += _tag.TagGenerator("VD", InsExpDate, false);

        strTag += _tag.TagGenerator("PT", InsMapID, false);
        strTag += _tag.TagGenerator("SN", InsSerial, false);
        strTag += _tag.TagGenerator("GR", GenderMapID, false);
        strTag += _tag.TagGenerator("RN", InsPageNo, false);
        strTag += _tag.TagGenerator("PG", DrExpertType, false);
        strTag += _tag.TagGenerator("PC", DrRefRegID, false);
        strTag += _tag.TagGenerator("DD", Diagnose, false);

        strTag += _tag.TagGenerator("PP", TotalAmount, false);
        strTag += _tag.TagGenerator("IS", InsuranceAmount, false);

        strTag += _tag.TagGenerator("MC", DrTechAssist, false);

        strTag += _tag.TagGenerator("PS", PatientAmount, false);


        strTag += _tag.TagEndGenerator("PH", null, true);


        return strTag;
    }

    private string TagServiceReg
    (
        string ServiceGroup, string ServiceCode, string ServiceNum, string TotalAmount,
        string InsuranceAmount, string FranchiseAmount, string ServiceNum2
    )
    {
        string strTag = null;

        strTag += _tag.TagStartGenerator("MH", null, false);
        strTag += _tag.TagGenerator("SG", ServiceGroup, false);
        strTag += _tag.TagGenerator("MG", ServiceCode, false);
        strTag += _tag.TagGenerator("MD", ServiceNum, false);
        strTag += _tag.TagGenerator("MP", TotalAmount, false);
        strTag += _tag.TagGenerator("MI", InsuranceAmount, false);
        strTag += _tag.TagGenerator("MS", FranchiseAmount, false);
        strTag += _tag.TagGenerator("MR", ServiceNum2, false);


        strTag += _tag.TagEndGenerator("MH", null, true);


        return strTag;
    }

    private void grdRepRun_Click(object sender, EventArgs e)
    {
    }

    private void lblInsExport_Click(object sender, EventArgs e)
    {
        string strTag = null;
        string CenterID, CenterName, PageCount, StartDate, EndDate, CompanyTel;

        CenterID = _dtInsHeaderInfo.Rows[0]["CenterID"].ToString();
        CenterName = _dtInsHeaderInfo.Rows[0]["CenterName"].ToString();
        PageCount = _dtInsExportPage.Rows.Count.ToString();
        StartDate = ucDateFilter1._StartDate.Replace("/", "").Substring(0, 8);
        EndDate = ucDateFilter1._EndDate.Replace("/", "").Substring(0, 8);
        CompanyTel = _dtInsHeaderInfo.Rows[0]["CompanyTel"].ToString();


        strTag += _tag.TagStartGenerator("Y", null, true);
        strTag += _tag.TagStartGenerator("HR", null, true);

        strTag += _tag.TagGenerator("DC", CenterID, true);
        strTag += _tag.TagGenerator("DN", CenterName, true);
        strTag += _tag.TagGenerator("RC", PageCount, true);
        strTag += _tag.TagGenerator("FD", StartDate, true);
        strTag += _tag.TagGenerator("TD", EndDate, true);
        strTag += _tag.TagGenerator("CR", CompanyTel, true);

        strTag += _tag.TagEndGenerator("HR", null, true);
        strTag += Environment.NewLine;


        int PageID;
        string RecNo,
            PageDate,
            PageDate2,
            InsExpDate,
            OPExportID,
            InsSerial,
            InsurancePageNumber,
            GenderMapIDA,
            DrRefRegID,
            DrExpertType,
            DrTechAssist,
            Diagnosis,
            TotalAmount,
            InsureanceAmount,
            FrenchiseAmount;

        for (var i = 0; i < _dtInsExportPage.Rows.Count; i++)
        {
            PageID = Convert.ToInt32(_dtInsExportPage.Rows[i]["PageID"]);

            RecNo = _dtInsExportPage.Rows[i]["RowNum"].ToString();
            PageDate = _dtInsExportPage.Rows[i]["Date"].ToString().Replace("/", "").Substring(0, 8);
            PageDate2 = _dtInsExportPage.Rows[i]["Date2"].ToString().Replace("/", "").Substring(0, 8);
            InsExpDate = _dtInsExportPage.Rows[i]["InsuranceExpireDate"].ToString().Replace("/", "")
                .Substring(0, 8);

            OPExportID = _dtInsExportPage.Rows[i]["OPExportID"].ToString();
            InsSerial = _dtInsExportPage.Rows[i]["InsuranceNumber"].ToString();
            InsurancePageNumber = _dtInsExportPage.Rows[i]["InsurancePageNumber"].ToString();
            GenderMapIDA = _dtInsExportPage.Rows[i]["GenderMapIDA"].ToString();
            DrRefRegID = _dtInsExportPage.Rows[i]["RefDrID"].ToString();
            DrExpertType = _dtInsExportPage.Rows[i]["DrExpertMapIDA"].ToString();
            DrTechAssist = _dtInsExportPage.Rows[i]["DrTechAssistRegNumber"].ToString();
            Diagnosis = _dtInsExportPage.Rows[i]["Diagnosis"].ToString();
            TotalAmount = _dtInsExportPage.Rows[i]["TotalAmount"].ToString();
            InsureanceAmount = _dtInsExportPage.Rows[i]["InsuranceAmount"].ToString();
            FrenchiseAmount = _dtInsExportPage.Rows[i]["FranchiseAmount"].ToString();

            strTag += _tag.TagStartGenerator("X", null, true);

            strTag += TagPage(
                RecNo, PageDate, PageDate2, InsExpDate, OPExportID,
                InsSerial, InsurancePageNumber, GenderMapIDA, DrRefRegID, DrExpertType, DrTechAssist,
                Diagnosis, TotalAmount, InsureanceAmount, FrenchiseAmount);

            strTag += _tag.TagStartGenerator("BY", null, true);

            strTag += ServiceReg(PageID);

            strTag += _tag.TagEndGenerator("BY", null, true);
            strTag += _tag.TagEndGenerator("X", null, true);
        }

        strTag += _tag.TagEndGenerator("Y", null, true);


        if (sfdReportFile.ShowDialog() == DialogResult.OK)
        {
            using (var sw = File.CreateText(sfdReportFile.FileName))
            {
                sw.WriteLine(strTag);
            }

            ucShowTextSave.ShowText("فایل ذخیره گردید", 5000);
        }
    }

    private void tabRep_SelectedPageChanged(object sender, TabPageChangedEventArgs e)
    {
        var tabName = tabRep.SelectedTabPage.Name;
        if (tabName == "tabFilter") panInsExportToolbar.Visible = false;

        if (tabName == "tabShowRep") panInsExportToolbar.Visible = true;
    }

    private string ServiceReg(int PageID)
    {
        string strTag = null;

        var dtSR = _dtInsExportServiceReg.Select("PageID=" + PageID).CopyToDataTable();

        string ServiceGroup,
            ServiceCode,
            ServiceNum,
            TotalAmount,
            InsuranceAmount,
            FranchiseAmount,
            ServiceNum2;

        for (var i = 0; i < dtSR.Rows.Count; i++)
        {
            ServiceGroup = dtSR.Rows[i]["InsOpMapID"].ToString();
            ServiceCode = dtSR.Rows[i]["MapIDA"].ToString();

            ServiceNum = dtSR.Rows[i]["Num"].ToString();
            TotalAmount = dtSR.Rows[i]["TotalAmount"].ToString();
            InsuranceAmount = dtSR.Rows[i]["InsuranceAmount"].ToString();
            FranchiseAmount = dtSR.Rows[i]["FranchiseAmount"].ToString();
            ServiceNum2 = dtSR.Rows[i]["Num2"].ToString();

            strTag += TagServiceReg(ServiceGroup, ServiceCode, ServiceNum, TotalAmount,
                InsuranceAmount, FranchiseAmount, ServiceNum2);
        }


        return strTag;
    }
}