using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using App;
using DAL;
using DevExpress.Data;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting;
using DevExpress.XtraTab;
using LIB;
using Control = App.Control;
using List = Saman.BLL.Common.List;

namespace Saman.UC;

public partial class UcReportRun : XtraUserControl
{
    private readonly List _bllList = new();

    private readonly BLL.Report.Report _bllReport = new();
    private readonly RepositoryItem r;
    private string _Date1;
    private string _Date2;
    private DataTable _dtRep = new();
    private DataTable _dtRepFilter = new();
    private DataTable _dtRepResult = new();
    private string _EndDate;
    private bool _IsLandscape;

    private string _PaperSize;

    private string _RepFilter, _RepFilterDescription;
    private string _RepFullName;
    private int _RepID;
    private string _RepName;

    private RepositoryItemMemoEdit _RepositoryItemMemoEdit;
    private string _RepTitle;
    private string _StartDate;
    private string _strCurDate;

    private string _strCurDateTime;
    private string _strCurTime;
    private string _Time1;
    private string _Time2;

    private string _TSQL;


    public UcReportRun()
    {
        InitializeComponent();
    }

    public int _Year { get; set; }
    public int _Mon { get; set; }
    public string _MonName { get; set; }
    public string _YearText { get; set; }

    private void UcReportRun_Load(object sender, EventArgs e)
    {
        //_RepID = 2;

        TreeLoadData();

        ShortKey();
    }

    private void ShortKey()
    {
        msiCancel.Click += btnCancel_Click;
        msiRepPrint.Click += btnRepPrint_Click;
        msiRepRun.Click += btnRepRun_Click;
        msiReturn.Click += btnReturn_Click;


        panMain.Focus();
    }

    private void TreeLoadData()
    {
        var dtTree = new DataTable();
        var tsql = @"
                            SELECT r.*
                              FROM SamanReport.dbo.vReport r
                            inner JOIN dbo.SACUserPermissionReport sacr ON sacr.RepID= r.RepID
                            inner JOIN 

                            (	

                            SELECT sacr.PersID, r.RepID
	                              FROM SamanReport.dbo.vReport r
	                            inner JOIN dbo.SACUserPermissionReport sacr ON sacr.RepID= r.RepID
                            GROUP BY sacr.PersID, r.RepID
                            UNION ALL
                            SELECT @PersID,0

                            ) p ON p.RepID=r.ParentID AND p.PersID=sacr.PersID
                            WHERE sacr.PersID=@PersID
                        ";

        var com = new SqlCommand();
        com.Parameters.AddWithValue("@PersID", GL.UserPersID);

        var dalData = new Data();
        dtTree = dalData.GetDataTableByText(cSet.cnStringA, com, tsql);

        treReport.KeyFieldName = "RepID";
        treReport.ParentFieldName = "ParentID";

        treReport.DataSource = dtTree;
        treReport.RowHeight = 23;

        if (dtTree.Rows.Count > 0)
        {
            treReport.Nodes.FirstNode.Expanded = true;
            treReport.Nodes.TreeList.ExpandAll();
        }

        var appSDate = new SDate();
        _strCurDateTime = appSDate.ShamsiDate();
        _strCurDate = _strCurDateTime.Substring(0, 10);
        _strCurTime = _strCurDateTime.Substring(11, 5);
    }

    private void SetFilterControls()
    {
        string ControlName;

        foreach (DataRow row in _dtRepFilter.Rows)
        {
            ControlName = row["ControlName"].ToString();

            if (ControlName == "e.ActionDate") panDate.Visible = true;

            if (ControlName == "Date") panDate.Visible = true;

            if (ControlName == "e.StartDate") panStartDate.Visible = true;

            if (ControlName == "e.EndDate") panEndDate.Visible = true;

            if (ControlName == "T.YearMon")
            {
                panYearMon.Visible = true;

                var appSDate = new SDate();
                var curDateTime = appSDate.ShamsiDate();
                string year, mon;

                year = curDateTime.Substring(0, 4);
                mon = curDateTime.Substring(5, 2);

                UL.FillLookup(lueYear, year, cSet.cnStringA, false, "ID,Name", "vADR", "Year",
                    "ADName='Year'", null);
                UL.FillLookup(lueMon, mon, cSet.cnStringA, false, "ID,Name", "vADR", "mon",
                    "ADName='Month'", null);

                _Year = Convert.ToInt32(lueYear.EditValue);
                _Mon = Convert.ToInt32(lueMon.EditValue);
            }

            if (ControlName == "T.YearAllMon")
            {
                panYearAllMon.Visible = true;

                var appSDate = new SDate();
                var curDateTime = appSDate.ShamsiDate();
                string year, mon;

                year = curDateTime.Substring(0, 4);
                mon = curDateTime.Substring(5, 2);

                UL.FillLookup(lueYear1, year, cSet.cnStringA, false, "ID,Name", "vADR", "Year",
                    "ADName='Year'", null);
                UL.FillLookup(lueMon1, mon, cSet.cnStringA, false, "ID,Name", "vADR", "mon",
                    "ADName='Month'", null);

                _Year = Convert.ToInt32(lueYear1.EditValue);
                _Mon = Convert.ToInt32(lueMon1.EditValue);
            }


            if (ControlName == "DiscountType")
            {
                panDiscountType.Visible = true;
                ucglueDiscountType._DataSource =
                    _bllList.LoadTable("ID,Name", "SamanHIS.dbo.vADR", "ADName='DiscountType'");
            }

            if (ControlName == "Insurance")
            {
                panInsurance.Visible = true;
                uclueInsurance._DataSource =
                    _bllList.LoadTable("InsuranceID,Name", "SamanHIS.dbo.vInsurance", "IsDisable = 0");
            }

            if (ControlName == "EncID") panEncID.Visible = true;
        }
    }

    private void lueMon1_EditValueChanged(object sender, EventArgs e)
    {
        _Mon = Convert.ToInt32(lueMon1.EditValue);
        _MonName = " - " + "لغایت" + " " + lueMon1.Text + " " + "ماه";
    }

    private void lueMon_EditValueChanged(object sender, EventArgs e)
    {
        _Mon = Convert.ToInt32(lueMon.EditValue);
        _MonName = " - " + lueMon.Text + " " + "ماه";
    }

    private void lueYear1_EditValueChanged(object sender, EventArgs e)
    {
        _Year = Convert.ToInt32(lueYear1.EditValue);
        _YearText = " سال " + _Year;
    }

    private void lueYear_EditValueChanged(object sender, EventArgs e)
    {
        _Year = Convert.ToInt32(lueYear.EditValue);
        _YearText = " سال " + _Year;
    }

    private void GetFilterControls()
    {
        _RepFilter = null;
        _RepFilterDescription = null;
        if (panDate.Visible)
        {
            _RepFilter += " AND e.ActionDate BETWEEN '" + datDate1.Text + "' AND '" + datDate2.Text + "'";

            _Date1 = datDate1.Text.Substring(0, 10);
            _Time1 = datDate1.Text.Substring(11, 5);

            _Date2 = datDate2.Text.Substring(0, 10);
            _Time2 = datDate2.Text.Substring(11, 5);

            _StartDate = "تاریخ از :" + _Time1 + " " + _Date1 + " تا " + _Time2 + " " + _Date2;

            //_RepFilterDescription += "<تاریخ ارائه تخفیف>" + "تاریخ از :" + _Time1 + " " + _Date1 + " تا " +
            //                         _Time2 + " " + _Date2;
        }

        if (panStartDate.Visible)
        {
            _RepFilter += " AND e.StartDate BETWEEN '" + datStartDate1.Text + "' AND '" + datStartDate2.Text + "'";

            _Date1 = datStartDate1.Text.Substring(0, 10);
            _Time1 = datStartDate1.Text.Substring(11, 5);

            _Date2 = datStartDate2.Text.Substring(0, 10);
            _Time2 = datStartDate2.Text.Substring(11, 5);

            _StartDate = "تاریخ پذیرش از :" + _Time1 + " " + _Date1 + " تا " + _Time2 + " " + _Date2;
        }

        if (panEndDate.Visible)
        {
            _RepFilter += " AND e.EndDate BETWEEN '" + datEndDate1.Text + "' AND '" + datEndDate2.Text + "'";

            _Date1 = datEndDate1.Text.Substring(0, 10);
            _Time1 = datEndDate1.Text.Substring(11, 5);

            _Date2 = datEndDate2.Text.Substring(0, 10);
            _Time2 = datEndDate2.Text.Substring(11, 5);

            _EndDate = "تاریخ ترخیص از :" + _Time1 + " " + _Date1 + " تا " + _Time2 + " " + _Date2;
        }

        if (panYearMon.Visible) _RepFilter += " AND T.Year = " + _Year + " AND T.Mon = " + _Mon;

        if (panYearAllMon.Visible) _RepFilter += " AND T.Year = " + _Year + " AND T.Mon <= " + _Mon;

        if (panDiscountType.Visible && ucglueDiscountType._SelectedValues != null)
        {
            string strSelectedID = null;
            strSelectedID = ucglueDiscountType._SelectedValuesStr;
            _RepFilter += " AND DiscountTypeList.ID IN(" + strSelectedID + ")";

            _RepFilterDescription += "<نوع تخفیف>" + ucglueDiscountType._SelectdText;
        }

        if (panInsurance.Visible && uclueInsurance._SelectedValues != null)
        {
            string strSelectedID = null;
            strSelectedID = uclueInsurance._SelectedValuesStr;
            _RepFilter += " AND e.Insurance IN(" + strSelectedID + ")";

            _RepFilterDescription += "<گروه بیمه>" + uclueInsurance._SelectdText;
        }

        if (panEncID.Visible) _RepFilter += " AND e.EncID = " + txtEncID.EditValue;
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
        var pb = e.PrintingSystem as PrintingSystemBase;
        var printableComponentLink = new PrintableComponentLink();
        pb.PageSettings.Landscape = _IsLandscape;
        pb.PageSettings.PaperKind = PaperKind.A4;
        pb.Document.AutoFitToPagesWidth = 1;
        pb.PageSettings.TopMargin = 100;
        pb.PageSettings.BottomMargin = 100;
        pb.PageSettings.LeftMargin = 40;
        pb.PageSettings.RightMargin = 40;

        grvRepRun.OptionsPrint.PrintHeader = true;
        grvRepRun.OptionsPrint.PrintFooter = true;


        //(e.Link.PageHeaderFooter as PageHeaderFooter).Header.Content.Clear();
        //(e.Link.PageHeaderFooter as PageHeaderFooter).Footer.Content.Clear();

        //(e.Link.PageHeaderFooter as PageHeaderFooter).Header.LineAlignment = BrickAlignment.Center;
        //(e.Link.PageHeaderFooter as PageHeaderFooter).Footer.LineAlignment = BrickAlignment.Center;

        //(e.Link.PageHeaderFooter as PageHeaderFooter).Header.Font = new Font("IRANSansFaNum", 9, FontStyle.Regular);
        //(e.Link.PageHeaderFooter as PageHeaderFooter).Footer.Font = new Font("IRANSansFaNum", 8);

        //(e.Link.PageHeaderFooter as PageHeaderFooter).Header.Content.AddRange(new string[] { _RepName + " " + _MonName + _YearText, _RepTitle, _EndDate + _StartDate });
        //(e.Link.PageHeaderFooter as PageHeaderFooter).Footer.Content.AddRange(new string[] { "[Date Printed]-[Time Printed] : تاریخ گزارش گیری  ", " [Pages #] از [Page #] : صفحه  ", "گزارش گیرنده : " + GL.UserPersName });


        #region ExportSetting

        if (chkPageHeaderRemove.Checked)
        {
            (e.Link.PageHeaderFooter as PageHeaderFooter).Header.Content.Clear();
            (e.Link.PageHeaderFooter as PageHeaderFooter).Footer.Content.Clear();
        }

        if (chkPageColNameRemove.Checked) grvRepRun.OptionsPrint.PrintHeader = false;

        if (chkPageColSummaryRemove.Checked) grvRepRun.OptionsPrint.PrintFooter = false;

        #endregion
    }

    private void pclRepRun_CreateMarginalHeaderArea(object sender, CreateAreaEventArgs e)
    {
        if (!chkPageHeaderRemove.Checked)
        {
            var tbRepCenterName = new TextBrick
            {
                Text = _RepTitle,
                Font = new Font("IRANSansFaNum", 14, FontStyle.Bold),
                HorzAlignment = HorzAlignment.Center,
                Rect = new RectangleF(0, 0, e.Graph.ClientPageSize.Width, 200),
                BorderWidth = 0,
                BackColor = Color.Transparent
            };
            e.Graph.DrawBrick(tbRepCenterName);

            var tbRepName = new TextBrick
            {
                Text = _RepName + " " + _MonName + _YearText,
                Font = new Font("IRANSansFaNum", 12, FontStyle.Bold),
                HorzAlignment = HorzAlignment.Center,
                Rect = new RectangleF(0, 30, e.Graph.ClientPageSize.Width, 200),
                BorderWidth = 0,
                BackColor = Color.Transparent
            };
            e.Graph.DrawBrick(tbRepName);

            var tbRepDate = new TextBrick
            {
                Text = "تاریخ گزارش گیری : " + _strCurTime + " " + _strCurDate,
                Font = new Font("IRANSansFaNum", 9),
                HorzAlignment = HorzAlignment.Far,
                Rect = new RectangleF(0, 40, e.Graph.ClientPageSize.Width, 200),
                BorderWidth = 0,
                BackColor = Color.Transparent
            };
            e.Graph.DrawBrick(tbRepDate);

            var tbRepFilterDate = new TextBrick
            {
                Text = _EndDate + _StartDate,
                Font = new Font("IRANSansFaNum", 9),
                HorzAlignment = HorzAlignment.Near,
                Rect = new RectangleF(0, 40, e.Graph.ClientPageSize.Width, 200),
                BorderWidth = 0,
                BackColor = Color.Transparent
            };
            e.Graph.DrawBrick(tbRepFilterDate);
        }
    }


    private void pclRepRun_CreateMarginalFooterArea(object sender, CreateAreaEventArgs e)
    {
        if (!chkPageHeaderRemove.Checked)
        {
            var tbRepUser = new TextBrick
            {
                Text = "گزارش گیرنده : " + GL.UserPersName,
                Font = new Font("IRANSansFaNum", 8),
                HorzAlignment = HorzAlignment.Near,
                Rect = new RectangleF(0, 25, e.Graph.ClientPageSize.Width, 200),
                BorderWidth = 0,
                BackColor = Color.Transparent
            };
            e.Graph.DrawBrick(tbRepUser);

            if (_RepFilterDescription == null) return;

            var tbRepFilterDescription = new TextBrick
            {
                Text = "فیلتر : " + _RepFilterDescription,
                Font = new Font("IRANSansFaNum", 8),
                HorzAlignment = HorzAlignment.Far,
                Rect = new RectangleF(0, 50, e.Graph.ClientPageSize.Width, 200),
                BorderWidth = 0,
                BackColor = Color.Transparent
            };
            e.Graph.DrawBrick(tbRepFilterDescription);
        }
    }

    private void btnRepPrint_Click(object sender, EventArgs e)
    {
        //grdRepRun.ShowPrintPreview();
        //grvRepRun.ShowRibbonPrintPreview();
        ShowPrintPreview();
    }


    private void btnRepRun_Click(object sender, EventArgs e)
    {
        GetFilterControls();

        _TSQL = _dtRep.Rows[0]["TSQL"].ToString();
        _TSQL = _TSQL.Replace("--filter--", _RepFilter);

        _Date1 = _Date1 + " " + _Time1;
        _Date2 = _Date2 + " " + _Time2;

        _dtRepResult = _bllReport.RepRun(1, _Date1, _Date2, _Year, _Mon, _TSQL);

        _IsLandscape = Convert.ToBoolean(_dtRep.Rows[0]["IsLandscape"]);
        _PaperSize = _dtRep.Rows[0]["PaperSize"].ToString();
        grdRepRun.DataSource = _dtRepResult;


        grdRepRun.DataSource = _dtRepResult;
        grvRepRun.AppearancePrint.Row.TextOptions.WordWrap = WordWrap.Wrap;
        grvRepRun.OptionsView.RowAutoHeight = true;
        _RepositoryItemMemoEdit = new RepositoryItemMemoEdit();
        grdRepRun.RepositoryItems.Add(_RepositoryItemMemoEdit);


        SetGridFormat();

        GridViewSummary();

        lblTitle2.Text = _MonName + _YearText + _EndDate + _StartDate;

        tab2.Show();
    }

    private void ShowPrintPreview()
    {
        grdRepRun.MainView.BeginDataUpdate();

        foreach (GridColumn col in grvRepRun.Columns)
        {
            col.ColumnEdit = _RepositoryItemMemoEdit;
            col.AppearanceCell.TextOptions.VAlignment = VertAlignment.Top;
            col.AppearanceCell.TextOptions.RightToLeft = true;
            col.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Far;
            col.DisplayFormat.FormatType = FormatType.Numeric;
            col.DisplayFormat.FormatString = "n0";
        }
        // grdRepRun.ShowRibbonPrintPreview();


        //pclRepRun.Landscape = _IsLandscape;
        //pclRepRun.PaperKind = PaperKind.A4;
        //pclRepRun.Margins.Top = 100;
        //pclRepRun.Margins.Bottom = 100;
        //pclRepRun.Margins.Right = 30;
        //pclRepRun.Margins.Left = 30;
        pclRepRun.CreateDocument();
        pclRepRun.ShowPreview();

        grdRepRun.MainView.EndDataUpdate();
    }

    private void pclRepRun_AfterCreateAreas(object sender, EventArgs e)
    {
        foreach (GridColumn col in grvRepRun.Columns)
        {
            col.ColumnEdit = _RepositoryItemMemoEdit;
            col.AppearanceCell.TextOptions.VAlignment = VertAlignment.Top;
            col.AppearanceCell.TextOptions.RightToLeft = true;
            col.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Near;
            col.DisplayFormat.FormatType = FormatType.Numeric;
            col.DisplayFormat.FormatString = "n0";
        }
    }


    private void GridViewSummary()
    {
        var colCount = grvRepRun.Columns.Count;
        //grvRepRun.Columns[0].GroupIndex = 1;
        //grvRepRun.Columns[1].GroupIndex = 2;

        string colFieldName;


        for (var i = 0; i < colCount; i++)
        {
            colFieldName = grvRepRun.Columns[i].FieldName;

            grvRepRun.Columns[i].Summary.Clear();
            grvRepRun.Columns[i].Summary.View.Appearance.UpdateRightToLeft(true);
            grvRepRun.Columns[i].Summary.View.AppearancePrint.UpdateRightToLeft(true);

            if (colFieldName.Contains("avg"))
            {
                grvRepRun.Columns[i].Summary.Add(SummaryItemType.Average, colFieldName, "میانگین {0:F0}");
                grvRepRun.Columns[i].Caption = colFieldName.Replace("avg", "");
                ;
            }

            if (colFieldName.Contains("sum"))
            {
                grvRepRun.Columns[i].Summary.Add(SummaryItemType.Sum, colFieldName, "جمع {0:F0}");
                grvRepRun.Columns[i].Caption = colFieldName.Replace("sum", "");
                ;
            }

            if (colFieldName.Contains("count"))
            {
                grvRepRun.Columns[i].Summary.Add(SummaryItemType.Count, colFieldName, "تعداد {0:F0}");
                grvRepRun.Columns[i].Caption = colFieldName.Replace("count", "");
                ;
            }
        }

        grvRepRun.BestFitColumns();
    }

    private void btnReturn_Click(object sender, EventArgs e)
    {
        tab1.Show();
    }


    private void panYearMon_Paint(object sender, PaintEventArgs e)
    {
        Control.LookupEditLine(lueMon, e);
        Control.LookupEditLine(lueYear, e);
    }

    private void treReportLoad()
    {
        var rowView = (DataRowView)treReport.GetRow(treReport.FocusedNode.Id);
        var row = rowView.Row;

        var RepType = Convert.ToInt32(row["RepType"]);

        if (RepType > 0) _RepName = row["Name"].ToString();

        _RepFullName = row["FullName"].ToString();
        _RepTitle = row["CenterName"].ToString();

        _RepID = Convert.ToInt32(row["RepID"].ToString());
        lblTitle1.Text = _RepFullName;


        ReportLoad();
    }

    private void treReport_DoubleClick(object sender, EventArgs e)
    {
        treReportLoad();
    }

    private void treReport_KeyDown(object sender, KeyEventArgs e)
    {
        treReportLoad();
    }

    private void treReport_KeyUp(object sender, KeyEventArgs e)
    {
        treReportLoad();
    }


    private void ReportLoad()
    {
        FormNull();
        tab1.Show();

        _dtRep = _bllReport.RepLoad(_RepID);
        _dtRepFilter = _bllReport.RepFilterLoad(_RepID);
        SetFilterControls();
    }

    private void FormNull()
    {
        panStartDate.Visible = false;
        panEndDate.Visible = false;
        panYearMon.Visible = false;
        panYearAllMon.Visible = false;
        panDate.Visible = false;
        panDiscountType.Visible = false;
        panInsurance.Visible = false;
        panEncID.Visible = false;


        grdRepRun.DataSource = null;
        _MonName = null;
        grvRepRun.Columns.Clear();
    }

    private void grvRepRun_CustomDrawFooter(object sender, RowObjectCustomDrawEventArgs e)
    {
        var stringFormat = new StringFormat
        {
            Alignment = StringAlignment.Near,
            LineAlignment = StringAlignment.Center
        };
        var rect = e.Bounds;
        rect.X += 10;
        e.DefaultDraw();
        e.Handled = true;
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


    private void tabRep_SelectedPageChanged(object sender, TabPageChangedEventArgs e)
    {
    }
}