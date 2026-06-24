using System;
using System.Data;
using System.Windows.Forms;
using App;
using DAL;
using DevExpress.Data;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
using LIB;
using Saman.APP.Stimul;
using Saman.BLL.Percase;
using Stimulsoft.Report.Dictionary;

namespace Saman.UC;

public partial class UcDrPercase : XtraUserControl
{
    private readonly Stimul _appStimul = new();

    private readonly Percase _bllPercase = new();

    private readonly BLL.Report.Report _bllReport = new();

    private readonly SUI sui = new();
    private DataTable _dtCurDrPercaseReg;
    private DataTable _dtCurDrPercaseRegAll;

    private DataTable _dtDrPercaseSummary;
    private string _EndDate;
    private bool _IsSign, _IsSignAll;
    private int _RegID;

    private DataRowView _rowCurPercase;

    private DataRow _rowDrSelect;

    private string _StartDate;


    public UcDrPercase()
    {
        InitializeComponent();
    }

    public byte _ModID { get; set; }
    public int _DrID { get; set; }

    private void UcDrPercase_Load(object sender, EventArgs e)
    {
        DrPercaseRegLoad();

        if (_IsSign) btnCalcPercase.Enabled = false;

        btnSignPercase.Enabled = false;
        ShortKey();
    }

    private void ShortKey()
    {
        msiSign.Click += btnSignPercase_Click;
        msiCalcPercase.Click += btnCalcPercase_Click;
        msiDrSelect.Click += btnDrSelect_Click;


        panMain.Focus();
    }

    private void DrPercaseRegLoad()
    {
        UL.FillLookup(lueDrPercaseReg, "0", cSet.cnStringERP, false, "*", "vDrPercaseReg", "DrPercaseReg", null,
            "RegID Desc");

        CurDrPercaseRegSignCheck();
    }

    private void CurDrPercaseRegLoad()
    {
        _StartDate = datStartDate.Text;
        _EndDate = datEndDate.Text;

        _dtCurDrPercaseReg = _bllPercase.DrPercaseSummaryCheckExistance(_StartDate, _EndDate);

        if (_dtCurDrPercaseReg.Rows.Count > 0)
        {
            _RegID = Convert.ToInt32(_dtCurDrPercaseReg.Rows[0]["RegID"]);
            _IsSign = Convert.ToBoolean(_dtCurDrPercaseReg.Rows[0]["IsSign"]);
        }
    }

    private void CurDrPercaseRegSignCheck()
    {
        _IsSignAll = true;
        _dtCurDrPercaseRegAll = _bllPercase.DrPercaseSummaryCheckIsSignAll();

        if (_dtCurDrPercaseRegAll.Rows.Count > 0) _IsSignAll = false;
    }

    private void CurDrPercaseRegSignCheckByRegID()
    {
        _IsSignAll = true;
        _dtCurDrPercaseRegAll = _bllPercase.DrPercaseSummaryCheckIsSignAllByReg(_RegID);

        if (_dtCurDrPercaseRegAll.Rows.Count > 0) _IsSignAll = false;
    }


    private void btnCalcPercase_Click(object sender, EventArgs e)
    {
        var appSDate = new SDate();


        if (appSDate.SDateConvertFromString(datStartDate.Text, "00:00") >
            appSDate.SDateConvertFromString(datEndDate.Text, "23:59"))
            sui.ShowWarningMessageForm("خطا :", "تاریخ شروع بزرگتر از تاریخ پایان می باشد", true);

        CurDrPercaseRegLoad();

        if (_dtCurDrPercaseReg.Rows.Count > 0)
        {
            CurDrPercaseRegSignCheckByRegID();
            var strMessage1 = "";
            if (!_IsSignAll)
            {
                strMessage1 = "کارانه امضاء نشده در لیست می باشد. ";
                sui.ShowWarningMessageForm("هشدار", strMessage1, true);
            }
            else
            {
                var RegRange = _dtCurDrPercaseReg.Rows[0]["RegRange"].ToString();

                var sui = new SUI();

                var strMessage = "";
                if (_IsSign)
                {
                    strMessage = RegRange + "  " + "در تاریخ انتخاب شده کارانه محاسبه و امضاء شده است";
                    sui.ShowWarningMessageForm("هشدار", strMessage, true);

                    CheckDrPercaseSign();
                }

                else
                {
                    strMessage = RegRange + "  " + "در تاریخ انتخاب شده کارانه محاسبه شده است"
                                 + "\n"
                                 + "\nکارانه پزشک مجدداً محاسبه گردد؟";
                    ;
                    var result = sui.FormShowMessage("هشدار", strMessage, "محاسبه مجدد", "انصراف");

                    if (result == DialogResult.Yes)
                        _dtDrPercaseSummary = _bllPercase.CreateDrPercase(_StartDate, _EndDate, GL.UserPersID);

                    if (result == DialogResult.No) _dtDrPercaseSummary = _bllPercase.DrPercaseSummaryLoad(_RegID);

                    CheckDrPercaseSign();
                }

                CreateGridRecNo();
            }
        }
        else
        {
            CurDrPercaseRegSignCheck();

            var strMessage = "";
            if (!_IsSignAll)
            {
                strMessage = "کارانه امضاء نشده در لیست می باشد. ";
                sui.ShowWarningMessageForm("هشدار", strMessage, true);
            }
            else
            {
                _dtDrPercaseSummary = _bllPercase.CreateDrPercase(_StartDate, _EndDate, GL.UserPersID);
            }
        }

        grdDrPercaseSummery.DataSource = _dtDrPercaseSummary;
        DrPercaseRegLoad();
    }


    private void gridView1_CustomUnboundColumnData(object sender, CustomColumnDataEventArgs e)
    {
        if (e.IsGetData) e.Value = e.ListSourceRowIndex + 1;
    }


    private void lueDrPercaseReg_EditValueChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(lueDrPercaseReg.EditValue) == 0) return;

        _rowCurPercase =
            lueDrPercaseReg.Properties.GetDataSourceRowByKeyValue(lueDrPercaseReg.EditValue) as DataRowView;

        CreateGridRecNo();

        _RegID = Convert.ToInt32(_rowCurPercase.Row["RegID"]);
        _IsSign = Convert.ToBoolean(_rowCurPercase.Row["IsSign"]);
        _StartDate = Convert.ToString(_rowCurPercase.Row["StartDate"]);
        _EndDate = Convert.ToString(_rowCurPercase.Row["EndDate"]);

        _StartDate = _StartDate.Substring(0, 10);
        _EndDate = _EndDate.Substring(0, 10);

        datStartDate.DateTime = Convert.ToDateTime(_StartDate);
        datEndDate.DateTime = Convert.ToDateTime(_EndDate);


        CheckDrPercaseSign();

        grdDrPercaseSummery.DataSource = _bllPercase.DrPercaseSummaryLoad(_RegID);
        grdDrPercaseDetail.DataSource = null;
    }


    private void CreateGridRecNo()
    {
        var col = grvDrPercaseSummery.Columns.ColumnByFieldName("RecNo");
        col.Visible = true;
        col.UnboundType = UnboundColumnType.Integer;
        grvDrPercaseSummery.CustomUnboundColumnData += gridView1_CustomUnboundColumnData;

        var col1 = grvDrPercaseDetail.Columns.ColumnByFieldName("RecNo");
        col1.Visible = true;
        col1.UnboundType = UnboundColumnType.Integer;
        grvDrPercaseDetail.CustomUnboundColumnData += gridView1_CustomUnboundColumnData;
    }

    private void CheckDrPercaseSign()
    {
        if (_IsSign)
        {
            btnSignPercase.Text = "بازگشت از امضاء";
            btnSignPercase.Enabled = false;
        }
        else
        {
            btnSignPercase.Text = "امضاِی سند";
            btnSignPercase.Enabled = true;
        }
    }

    private void btnSignPercase_Click(object sender, EventArgs e)
    {
        _IsSign = !_IsSign;
        _bllPercase.DrPercaseRegSign(_RegID, _IsSign, GL.UserPersID);
        DrPercaseRegLoad();
        CurDrPercaseRegLoad();

        CheckDrPercaseSign();
    }


    private void btnDrSelect_Click(object sender, EventArgs e)
    {
        _rowDrSelect = grvDrPercaseSummery.GetDataRow(grvDrPercaseSummery.FocusedRowHandle);

        _DrID = Convert.ToInt32(_rowDrSelect["DrID"]);
        _RegID = Convert.ToInt32(_rowDrSelect["RegID"]);
        tabDrPercaseDetail.Visible = true;

        var col = grvDrPercaseSummery.Columns.ColumnByFieldName("RecNo");
        // col.FieldName = "RecNo";
        col.Visible = true;
        col.UnboundType = UnboundColumnType.Integer;
        grvDrPercaseDetail.CustomUnboundColumnData += gridView1_CustomUnboundColumnData;
        CreateGridRecNo();
        grdDrPercaseDetail.DataSource = _bllPercase.DrPercaseDetailLoad(_RegID, _DrID);
    }

    private void ShowDrNameinTitle()
    {
        if (grvDrPercaseSummery.FocusedRowHandle < 0) return;

        _rowDrSelect = grvDrPercaseSummery.GetDataRow(grvDrPercaseSummery.FocusedRowHandle);
        _DrID = Convert.ToInt32(_rowDrSelect["DrID"]);
        lblTitle2.Text = _rowDrSelect["DrName"] + " ";
    }

    private void grdDrPercaseSummery_Click(object sender, EventArgs e)
    {
        ShowDrNameinTitle();
    }

    private void grdDrPercaseSummery_KeyDown(object sender, KeyEventArgs e)
    {
        ShowDrNameinTitle();
    }

    private void grdDrPercaseSummery_KeyUp(object sender, KeyEventArgs e)
    {
        ShowDrNameinTitle();
    }

    private void barBtnReportDrPercaseSummary_ItemClick(object sender, ItemClickEventArgs e)
    {
        var tsqlvDrPercaseSummary =
            @"SELECT *
                FROM[SamanERP].[dbo].[vDrPercaseSummary]
                where RegId=@RegID";

        tsqlvDrPercaseSummary = tsqlvDrPercaseSummary.Replace("@RegID", _RegID.ToString());


        var tsqlvDrPercaseReg =
            @"SELECT *
                FROM[SamanERP].[dbo].[vDrPercaseReg]
                where RegId=@RegID ";

        tsqlvDrPercaseReg = tsqlvDrPercaseReg.Replace("@RegID", _RegID.ToString());


        var tsqlRptProperty =
            @"
               SELECT
               SamanHIS.dbo.SCurDateTime() AS PrintDate
               ,@UserPersName AS ReportUserName ";

        tsqlRptProperty = tsqlRptProperty.Replace("@UserPersName", "'" + GL.UserPersName + "'");

        var report = _bllReport.RepRptFileLoad(100010);

        var sti = new StiSqlDatabase("SamanServer", cSet.cnStringERP);
        report.Dictionary.Databases[0] = sti;

        ((StiSqlSource)report.Dictionary.DataSources["vDrPercaseSummary"]).SqlCommand = tsqlvDrPercaseSummary;
        ((StiSqlSource)report.Dictionary.DataSources["vDrPercaseReg"]).SqlCommand = tsqlvDrPercaseReg;
        ((StiSqlSource)report.Dictionary.DataSources["RptProperty"]).SqlCommand = tsqlRptProperty;

        report.Show();
    }

    private void barBtnReportDrPercaseDetail_ItemClick(object sender, ItemClickEventArgs e)
    {
        var tsqlvIncomeDetail =
            @"SELECT *
                FROM[SamanERP].[dbo].[vIncomeDetail]
                where RegId=@RegID";

        tsqlvIncomeDetail = tsqlvIncomeDetail.Replace("@RegID", _RegID.ToString());


        var tsqlvDrPercaseSummary =
            @"SELECT *
                FROM[SamanERP].[dbo].[vDrPercaseSummary]
                where RegId=@RegID";

        tsqlvDrPercaseSummary = tsqlvDrPercaseSummary.Replace("@RegID", _RegID.ToString());

        var tsqlvDrPercaseReg =
            @"SELECT *
                FROM[SamanERP].[dbo].[vDrPercaseReg]
                where RegId=@RegID ";

        tsqlvDrPercaseReg = tsqlvDrPercaseReg.Replace("@RegID", _RegID.ToString());

        var tsqlRptProperty =
            @"
               SELECT
               SamanHIS.dbo.SCurDateTime() AS PrintDate
               ,@UserPersName AS ReportUserName ";

        tsqlRptProperty = tsqlRptProperty.Replace("@UserPersName", "'" + GL.UserPersName + "'");

        var report = _bllReport.RepRptFileLoad(100011);

        var sti = new StiSqlDatabase("SamanServer", cSet.cnStringERP);
        report.Dictionary.Databases[0] = sti;

        ////var t = report.Dictionary.DataSources.Items;

        ((StiSqlSource)report.Dictionary.DataSources["vIncomeDetail"]).SqlCommand = tsqlvIncomeDetail;
        ((StiSqlSource)report.Dictionary.DataSources["vDrPercaseSummary"]).SqlCommand = tsqlvDrPercaseSummary;
        ((StiSqlSource)report.Dictionary.DataSources["vDrPercaseReg"]).SqlCommand = tsqlvDrPercaseReg;
        ((StiSqlSource)report.Dictionary.DataSources["RptProperty"]).SqlCommand = tsqlRptProperty;


        report.Show();
    }

    private void barBtnReportDrPercaseDetailDaraee_ItemClick(object sender, ItemClickEventArgs e)
    {
        var tsqlvIncomeDetail =
            @"SELECT *
                FROM[SamanERP].[dbo].[vIncomeDetail]
                where RegId=@RegID";

        tsqlvIncomeDetail = tsqlvIncomeDetail.Replace("@RegID", _RegID.ToString());


        var tsqlvDrPercaseSummary =
            @"SELECT *
                FROM[SamanERP].[dbo].[vDrPercaseSummary]
                where RegId=@RegID";

        tsqlvDrPercaseSummary = tsqlvDrPercaseSummary.Replace("@RegID", _RegID.ToString());

        var tsqlvDrPercaseReg =
            @"SELECT *
                FROM[SamanERP].[dbo].[vDrPercaseReg]
                where RegId=@RegID ";

        tsqlvDrPercaseReg = tsqlvDrPercaseReg.Replace("@RegID", _RegID.ToString());

        var tsqlRptProperty =
            @"
               SELECT
               SamanHIS.dbo.SCurDateTime() AS PrintDate
               ,@UserPersName AS ReportUserName ";

        tsqlRptProperty = tsqlRptProperty.Replace("@UserPersName", "'" + GL.UserPersName + "'");

        var report = _bllReport.RepRptFileLoad(100012);

        var sti = new StiSqlDatabase("SamanServer", cSet.cnStringERP);
        report.Dictionary.Databases[0] = sti;

        ////var t = report.Dictionary.DataSources.Items;

        ((StiSqlSource)report.Dictionary.DataSources["vIncomeDetail"]).SqlCommand = tsqlvIncomeDetail;
        ((StiSqlSource)report.Dictionary.DataSources["vDrPercaseSummary"]).SqlCommand = tsqlvDrPercaseSummary;
        ((StiSqlSource)report.Dictionary.DataSources["vDrPercaseReg"]).SqlCommand = tsqlvDrPercaseReg;
        ((StiSqlSource)report.Dictionary.DataSources["RptProperty"]).SqlCommand = tsqlRptProperty;


        report.Show();
    }

    private void barBtnReportSelectDrPercaseSummary_ItemClick(object sender, ItemClickEventArgs e)
    {
        var tsqlvIncomeDetail =
            @"SELECT *
                FROM[SamanERP].[dbo].[vIncomeDetail]
                where RegId=@RegID AND DrId=@DrID";

        tsqlvIncomeDetail = tsqlvIncomeDetail.Replace("@RegID", _RegID.ToString());
        tsqlvIncomeDetail = tsqlvIncomeDetail.Replace("@DrID", _DrID.ToString());

        var tsqlvDrPercaseSummary =
            @"SELECT *
                FROM[SamanERP].[dbo].[vDrPercaseSummary]
                where RegId=@RegID AND DrId=@DrID";

        tsqlvDrPercaseSummary = tsqlvDrPercaseSummary.Replace("@RegID", _RegID.ToString());
        tsqlvDrPercaseSummary = tsqlvDrPercaseSummary.Replace("@DrID", _DrID.ToString());


        var tsqlvDrPercaseReg =
            @"SELECT *
                FROM[SamanERP].[dbo].[vDrPercaseReg]
                where RegId=@RegID ";

        tsqlvDrPercaseReg = tsqlvDrPercaseReg.Replace("@RegID", _RegID.ToString());

        var tsqlRptProperty =
            @"
               SELECT
               SamanHIS.dbo.SCurDateTime() AS PrintDate
               ,@UserPersName AS ReportUserName ";

        tsqlRptProperty = tsqlRptProperty.Replace("@UserPersName", "'" + GL.UserPersName + "'");

        var report = _bllReport.RepRptFileLoad(100013);

        var sti = new StiSqlDatabase("SamanServer", cSet.cnStringERP);
        report.Dictionary.Databases[0] = sti;

        ////var t = report.Dictionary.DataSources.Items;

        ((StiSqlSource)report.Dictionary.DataSources["vIncomeDetail"]).SqlCommand = tsqlvIncomeDetail;
        ((StiSqlSource)report.Dictionary.DataSources["vDrPercaseSummary"]).SqlCommand = tsqlvDrPercaseSummary;
        ((StiSqlSource)report.Dictionary.DataSources["vDrPercaseReg"]).SqlCommand = tsqlvDrPercaseReg;
        ((StiSqlSource)report.Dictionary.DataSources["RptProperty"]).SqlCommand = tsqlRptProperty;

        report.Show();
    }
}