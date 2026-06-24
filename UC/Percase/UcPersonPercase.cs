using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using App;
using DAL;
using DevExpress.Data;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using LIB;
using Saman.APP.Stimul;
using Saman.BLL.Percase;
using Stimulsoft.Report.Dictionary;
using Control = App.Control;

namespace Saman.UC;

public partial class UcPersonPercase : XtraUserControl
{
    private readonly Stimul _appStimul = new();


    private readonly PersonPercase _bllPersonPercase = new();


    private readonly BLL.Report.Report _bllReport = new();
    private readonly string _SignDate;
    private DataTable _dtPersonPercase;

    private DataTable _dtPersonPercaseReg;

    private long _ManagerAmount, _SDrAmount, _ADrAmount;
    private int _Mon;
    private int _RegID;
    private string _SignUserPersName;
    private int _Year;


    public UcPersonPercase()
    {
        InitializeComponent();
    }

    public byte _ModID { get; set; }
    public int _IsSign { get; set; }

    private void UcPersonPercase_Load(object sender, EventArgs e)
    {
        var appSDate = new SDate();
        var curDateTime = appSDate.ShamsiDate();
        string year, mon;

        year = curDateTime.Substring(0, 4);
        mon = curDateTime.Substring(5, 2);

        UL.FillLookup(lueYear, year, cSet.cnStringA, false, "ID,Name", "vADR", "Year",
            "ADName='Year'", null);
        UL.FillLookup(lueMon, mon, cSet.cnStringA, false, "ID,Name", "vADR", "mon",
            "ADName='Month'", null);


        MaskSet();

        ShortKey();
    }

    private void ShortKey()
    {
        msiCalcPercase.Click += btnCalcPercase_Click;
        msiClose.Click += btnCancel_Click;
        msiSign.Click += btnSignPercase_Click;
        msiSave.Click += btnSave_Click;

        panMain.Focus();
    }

    private void MaskSet()
    {
        txtSDrAmount.Properties.Mask.EditMask = App.UI.MaskNum;
        txtADrAmount.Properties.Mask.EditMask = App.UI.MaskNum;
        txtManagerAmount.Properties.Mask.EditMask = App.UI.MaskNum;
        RepTxtItemVal.Properties.Mask.EditMask = App.UI.MaskNumWithDecimal;
    }

    private void FormNull()
    {
        _RegID = 0;
        txtADrAmount.Text = "0";
        txtSDrAmount.Text = "0";
        txtManagerAmount.Text = "0";


        ActiveControl = lueMon;

        if (_dtPersonPercase == null) return;

        _dtPersonPercase.Clear();
        grdPersonPercase.DataSource = _dtPersonPercase;
    }

    private bool CheckValidation()
    {
        var isValid = true;

        _ManagerAmount = Convert.ToInt64(txtManagerAmount.Text.Replace(",", ""));
        _SDrAmount = Convert.ToInt64(txtSDrAmount.Text.Replace(",", ""));
        _ADrAmount = Convert.ToInt64(txtADrAmount.Text.Replace(",", ""));

        if (_ManagerAmount + _SDrAmount + _ADrAmount == 0)
        {
            txtSDrAmount.ErrorText = "لطفا مبلغ را وارد نمایید";
            isValid = false;
        }

        return isValid;
    }


    private void panelControl1_Paint(object sender, PaintEventArgs e)
    {
        Control.TextEditLine(lueMon, e);
        Control.TextEditLine(lueYear, e);
    }

    private void grvPersonPercase_CustomUnboundColumnData(object sender, CustomColumnDataEventArgs e)
    {
        if (e.IsGetData) e.Value = e.ListSourceRowIndex + 1;
    }

    private void lueYear_EditValueChanged(object sender, EventArgs e)
    {
        _Year = Convert.ToInt32(lueYear.EditValue);
        PersonPercaseLoadData();
        CheckPersonPercaseSign();
    }


    private void lueMon_EditValueChanged(object sender, EventArgs e)
    {
        _Mon = Convert.ToInt32(lueMon.EditValue);
        PersonPercaseLoadData();
        CheckPersonPercaseSign();
    }

    private void PersonPercaseLoadData()
    {
        FormNull();

        var col = grvPersonPercase.Columns.ColumnByFieldName("RecNo");
        col.Visible = true;
        col.UnboundType = UnboundColumnType.Integer;
        grvPersonPercase.CustomUnboundColumnData += grvPersonPercase_CustomUnboundColumnData;

        _dtPersonPercase = _bllPersonPercase.SPPersonPercase(1, _Year, _Mon, 0, 0, 0, 0, GL.UserPersID);

        grdPersonPercase.DataSource = _dtPersonPercase;

        if (_dtPersonPercase.Rows.Count > 0)
        {
            _RegID = Convert.ToInt32(_dtPersonPercase.Rows[0]["RegID"]);

            _dtPersonPercaseReg = _bllPersonPercase.PersonPercaseReg(1, _RegID, null, null);

            _IsSign = Convert.ToInt32(_dtPersonPercaseReg.Rows[0]["IsSign"]);
        }

        if (_RegID == 0)
        {
            PersonPercaseAmountFromDrPercase();
            _IsSign = 0;
        }
        else
        {
            RegData();
        }
    }

    private void PersonPercaseAmountFromDrPercase()
    {
        var dtPersonPercaseAmountFromDrPercase = _bllPersonPercase.PersonPercaseAmountFromDrPercase(_Year, _Mon);

        _SDrAmount = Convert.ToInt64(dtPersonPercaseAmountFromDrPercase.Rows[0]["SDrAmount"]);
        _ADrAmount = Convert.ToInt64(dtPersonPercaseAmountFromDrPercase.Rows[0]["ADrAmount"]);

        txtSDrAmount.Text = _SDrAmount.ToString("n0");
        txtADrAmount.Text = _ADrAmount.ToString("n0");
    }

    private void RegData()
    {
        _dtPersonPercaseReg = _bllPersonPercase.PersonPercaseReg(1, _RegID, null, null);


        _ManagerAmount = Convert.ToInt64(_dtPersonPercaseReg.Rows[0]["ManagerAmount"]);
        _SDrAmount = Convert.ToInt64(_dtPersonPercaseReg.Rows[0]["SDrAmount"]);
        _ADrAmount = Convert.ToInt64(_dtPersonPercaseReg.Rows[0]["ADrAmount"]);

        _IsSign = Convert.ToInt32(_dtPersonPercaseReg.Rows[0]["IsSign"]);
        _SignUserPersName = _dtPersonPercaseReg.Rows[0]["SignUserPersName"].ToString();

        txtManagerAmount.Text = _ManagerAmount.ToString("n0");
        txtSDrAmount.Text = _SDrAmount.ToString("n0");
        txtADrAmount.Text = _ADrAmount.ToString("n0");
    }

    private void btnCalcPercase_Click(object sender, EventArgs e)
    {
        if (!CheckValidation()) return;

        if (_IsSign == 1)
        {
            var sui = new SUI();

            var strMessage = "";

            strMessage = "در تاریخ انتخاب شده کارانه محاسبه و امضاء شده است";
            sui.ShowWarningMessageForm("هشدار", strMessage, true);
        }
        else
        {
            _ManagerAmount = Convert.ToInt64(txtManagerAmount.Text.Replace(",", ""));
            _SDrAmount = Convert.ToInt64(txtSDrAmount.Text.Replace(",", ""));
            _ADrAmount = Convert.ToInt64(txtADrAmount.Text.Replace(",", ""));


            _bllPersonPercase.SPPersonPercase(0, _Year, _Mon, _RegID, _ManagerAmount, _SDrAmount,
                _ADrAmount, GL.UserPersID);

            _dtPersonPercase = _bllPersonPercase.SPPersonPercase(1, _Year, _Mon, _RegID, null, null,
                null, GL.UserPersID);

            _RegID = Convert.ToInt32(_dtPersonPercase.Rows[0]["RegID"]);
            RegData();

            PersonPercaseItemSave();

            _dtPersonPercase = _bllPersonPercase.SPPersonPercase(0, _Year, _Mon, _RegID, _ManagerAmount, _SDrAmount,
                _ADrAmount, GL.UserPersID);
            grdPersonPercase.DataSource = _dtPersonPercase;


            PersonPercaseLoadData();
        }
    }

    private void PersonPercaseItemSave()
    {
        var PersID = 0;
        double ItemVal = 0;
        var ItemID = 0;

        long ModifierAmount = 0;

        for (var i = 0; i < grvPersonPercase.RowCount; i++)
        {
            PersID = Convert.ToInt32(grvPersonPercase.GetRowCellValue(i, "PersID"));
            ItemID = 0;
            ItemVal = Convert.ToDouble(grvPersonPercase.GetRowCellValue(i, "PerformanceVal"));

            ModifierAmount = Convert.ToInt64(grvPersonPercase.GetRowCellValue(i, "ModifierAmount"));
            // ResultAmount = Convert.ToInt64(grvPersonPercase.GetRowCellValue(i, "ResultAmount"));

            _bllPersonPercase.PersonPercaseItemSave(2, _RegID, PersID, ItemID, ItemVal, ModifierAmount,
                GL.UserPersID);
        }

        SpPersonPercase2();


        _dtPersonPercase = _bllPersonPercase.SPPersonPercase(1, _Year, _Mon, _RegID, null, null,
            null, GL.UserPersID);

        grdPersonPercase.DataSource = _dtPersonPercase;
    }

    private void CheckPersonPercaseSign()
    {
        if (_IsSign == 1)
        {
            //  btnSignPercase.Text = "بازگشت از امضاء";
            btnSignPercase.Enabled = false;
            btnCalcPercase.Enabled = false;
            btnSave.Enabled = false;
        }
        else
        {
            btnSignPercase.Text = "امضای سند";
            btnSignPercase.Enabled = true;
            btnCalcPercase.Enabled = true;
            btnSave.Enabled = true;
        }
    }

    private void btnSignPercase_Click(object sender, EventArgs e)
    {
        //_IsSign = !_IsSign;
        _bllPersonPercase.PersonPercaseReg(0, _RegID, 1, GL.UserPersID);
        PersonPercaseLoadData();
        CheckPersonPercaseSign();
    }

    private void barBtnReportPersonPercase_ItemClick(object sender, ItemClickEventArgs e)
    {
        var tsqlvPersonPercase =
            @"select * from vPersonPercase
                where Year=@Year AND Mon=@Mon";

        tsqlvPersonPercase = tsqlvPersonPercase.Replace("@Year", _Year.ToString());
        tsqlvPersonPercase = tsqlvPersonPercase.Replace("@Mon", _Mon.ToString());


        var tsqlRptProperty =
            @"
               SELECT
               SamanHIS.dbo.SCurDateTime() AS PrintDate
               ,@UserPersName AS ReportUserName ";

        tsqlRptProperty = tsqlRptProperty.Replace("@UserPersName", "'" + GL.UserPersName + "'");

        var report = _bllReport.RepRptFileLoad(100014);

        var sti = new StiSqlDatabase("SamanServer", cSet.cnStringERP);
        report.Dictionary.Databases[0] = sti;

        ((StiSqlSource)report.Dictionary.DataSources["vPersonPercase"]).SqlCommand = tsqlvPersonPercase;
        ((StiSqlSource)report.Dictionary.DataSources["RptProperty"]).SqlCommand = tsqlRptProperty;

        report.Show();
    }

    private void grvPersonPercase_CellValueChanged(object sender, CellValueChangedEventArgs e)
    {
        if (e.RowHandle < 0) return;

        if (e.Column.FieldName != "ModifierAmount") return;

        var row = grvPersonPercase.GetDataRow(e.RowHandle);

        var ModifierAmount = Convert.ToInt64(row["ModifierAmount"]);
        var PercaseAmount = Convert.ToInt64(row["PercaseAmount"]);

        var ResultAmount = PercaseAmount + ModifierAmount;

        grvPersonPercase.SetRowCellValue(e.RowHandle, "ResultAmount", ResultAmount);
        grvPersonPercase.UpdateCurrentRow();
    }

    private void SpPersonPercase2()
    {
        _bllPersonPercase.SPPersonPercase2(_Year, _Mon, _RegID, GL.UserPersID);
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
        PersonPercaseItemSave();
    }

    private void grvPersonPercase_RowCellStyle(object sender, RowCellStyleEventArgs e)
    {
        var view = sender as GridView;
        if (e.Column.FieldName == "ModifierAmount")
        {
            var cellValue = Convert.ToInt64(e.CellValue);
            if (cellValue < 0) e.Appearance.ForeColor = Color.Red;
        }
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
    }
}