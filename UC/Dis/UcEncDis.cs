using System;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using App;
using DAL;
using DevExpress.Data;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting;
using LIB;
using Saman.APP.Stimul;
using Saman.BLL.Cash;
using Saman.BLL.Dis;
using Saman.BLL.Enc;
using Saman.Properties;
using Control = App.Control;

namespace Saman.UC.Dis;

public partial class UcEncDis : XtraUserControl
{
    private readonly Stimul _appStimul = new();

    private readonly BLL.Dis.Dis _bllDis = new();
    private readonly EncBill _bllEncBill = new();

    private readonly BLL.Report.Report _bllReport = new();
    private readonly DataTable _dtEncCash;

    private readonly SUI sui = new();
    public int _CurEncID;
    private DataTable _dtDisEncEndDateChange;
    private DataTable _dtEncBill;
    private DataTable _dtEncBillFull;
    private DataTable _dtEncBillSummary;
    private DataTable _dtEncCur;

    private DataTable _dtEncDis;

    private DataTable _EncCashDataList;

    public string _EndDate;

    private bool _IsSDrAcceptAmount;

    public int _PersID;

    private DataRow[] _rowEncCur;

    public UcEncDis()
    {
        InitializeComponent();
    }

    public int _ModID { get; set; }
    public int _DepID { get; set; }

    public int _EncStatus { get; set; }

    public bool _ReadOnly { get; set; }
    public int _EncID { get; set; }

    public int _PageID { get; set; }

    public int _EncType { get; set; }


    public int _CashID { get; set; }

    private void FormNull()
    {
        _EncID = 0;
        lblTitle.Text = null;
        lblTitle2.Text = null;
        lblTitleEncDate.Text = null;
        lblTitleEncStatus.Text = null;

        lblPageID.Text = null;
        lblDepName.Text = null;
        datEndData.Text = null;
        timeEndData.Time = new DateTime();
        lblDrName.Text = null;
        lblTotalAmount.Text = null;
        lblPatientAmount.Text = null;
        lblDiscountAmount.Text = null;
        lblResultAmount.Text = null;
        lblPayAmount.Text = null;
        lblRemainAmount.Text = null;
        lblCenterDiscountAmount.Text = null;
        lblCenterDiscountAmountA.Text = null;
        lblSDrDiscountAmount.Text = null;
        lblADrDiscountAmount.Text = null;
        lblSDrAmount.Text = null;
        lblADrAmount.Text = null;
        lblCenterAmount.Text = null;
        lblADrDiscountAmount.Text = null;
        grdEncCashData.DataSource = null;
        grdEncBill.DataSource = null;
        grdEncBillDiscount.DataSource = null;

        txtSDrAcceptAmount.Text = "0";
    }

    private void UcEncDis_Load(object sender, EventArgs e)
    {
        UL.LangChange(2);
        ActiveControl = txtEncID;

        _rowEncCur = GL.dtEncCur.Select("CurDepID=" + _DepID);

        if (_rowEncCur.Length > 0)
        {
            _EncID = Convert.ToInt32(_rowEncCur[0]["EncID"]);
            _CurEncID = _EncID;
        }

        if (_EncID > 0) EncSearch(_EncID);

        ShortKey();
    }

    private void ShortKey()
    {
        msiBackDis.Click += btnCancelDis_Click;
        msiSave.Click += btnSave_Click;
        msiDisSave.Click += btnDisSave3_Click;
        msiDisPrint.Click += btnPrintDis_Click;


        panMain.Focus();
    }

    private void EncSearch(int encID)
    {
        FormNull();
        txtEncID.ErrorText = null;

        var bllEnc = new EncList();
        _dtEncCur = bllEnc.EncSearch(encID, true);

        if (_dtEncCur.Rows.Count > 0)
        {
            _EncID = Convert.ToInt32(_dtEncCur.Rows[0]["EncID"].ToString());
            txtEncID.Text = _EncID.ToString();

            lblTitle.Text = _dtEncCur.Rows[0]["FirstName"] + " " + _dtEncCur.Rows[0]["LastName"];
            lblTitle2.Text = _dtEncCur.Rows[0]["Age"] + " سال - فرزند " + _dtEncCur.Rows[0]["FatherName"];
            lblTitleEncDate.Text = " تاریخ پذیرش : " + _dtEncCur.Rows[0]["StartDate"].ToString().Replace(' ', ' ');
            // lblEndData.Text= _dtEncCur.Rows[0]["EndDate"].ToString().Replace(' ', ' ');
            lblExplain.Text = _dtEncCur.Rows[0]["Explain"].ToString();

            picGender.Image = Convert.ToByte(_dtEncCur.Rows[0]["Gender"]) == 1 ? Resources.male : Resources.female;
            lblDepName.Text = _dtEncCur.Rows[0]["DepName"].ToString();
            lblDrName.Text = _dtEncCur.Rows[0]["DrName"].ToString();

            _IsSDrAcceptAmount = (bool)_dtEncCur.Rows[0]["IsSDrAcceptAmount"];
            lblSDrFixAmount.Text = Convert.ToInt64(_dtEncCur.Rows[0]["SDrFixAmount"]).ToString("n0");
            chkIsSDrAcceptAmount.EditValue = (bool)_dtEncCur.Rows[0]["IsSDrAcceptAmount"];

            if (_dtEncCur.Rows[0]["EndDate"].ToString() != "")
            {
                datEndData.DateTime = DateTime.Parse(_dtEncCur.Rows[0]["EndDate"].ToString());
                timeEndData.Time = DateTime.Parse(_dtEncCur.Rows[0]["EndDate"].ToString());
            }

            _EncStatus = Convert.ToByte(_dtEncCur.Rows[0]["EncStatus"]);
            if (_EncStatus < 3 && _CurEncID == 0)
            {
                _dtEncDis = _bllDis.DisEnc(_EncID, 2, GL.UserPersID);
                EncBillSummarySave();
                //string strMessage = _dtEncCur.Rows[0]["FirstName"] + " " + _dtEncCur.Rows[0]["LastName"]
                //                    + "\n"
                //                    + "\nترخیص یا برآورد هزینه؟";

                //SUI sui = new SUI();
                //var result = sui.FormShowMessage("کد پذیرش : " + _EncID, strMessage, "ترخیص گردد", "برآورد هزینه");

                //if (result == DialogResult.Yes)
                //    _dtEncDis = _bllDis.DisEnc(_EncID, _PersID, 3);

                //if (result == DialogResult.No)
                //    _dtEncDis = _bllDis.DisEnc(_EncID, _PersID, 2);

                //EncBillSummarySave();
            }

            if (_EncStatus < 3) lblTitleEncStatus.Text = "برآورد هزینه";

            if (_EncStatus == 3) lblTitleEncStatus.Text = "بیمار ترخیص گردیده است";

            if (_EncStatus == 4) lblTitleEncStatus.Text = "بیمار ترخیص نهایی گردیده است";

            if (_EncStatus == 5) lblTitleEncStatus.Text = "بیمار کنسل گردیده است";

            LoadEncBillData();
            GetEncOprData();
        }
        else
        {
            txtEncID.ErrorText = "بیماری یافت نشد";
            FormNull();
        }

        GL.SetEncCurVal(_DepID, _dtEncCur);

        CheckReadOnly();
    }

    private void btnDisSave3_Click(object sender, EventArgs e)
    {
        _dtEncDis = _bllDis.DisEnc(_EncID, 3, GL.UserPersID);
        EncBillSummarySave();
        LoadEncBillData();
        GetEncOprData();
        CheckReadOnly();

        EncSearch(_EncID);
        ucShowTextReturnDis.ShowText("بیمار ترخیص گردید", 5000);
    }

    private void CheckReadOnly()
    {
        chkIsCancel.EditValue = false;
        _ReadOnly = true;
        if (_EncStatus == 3)
        {
            _ReadOnly = false;
            btnCancelDis.Enabled = !_ReadOnly;
            //btnDisEncEndDateChange.Enabled = !_ReadOnly;
            // btnSave.Enabled = _ReadOnly;
            btnDisSave3.Enabled = _ReadOnly;
        }

        _ReadOnly = true;
        if (_EncStatus == 4)
        {
            _ReadOnly = false;
            btnCancelDis.Enabled = _ReadOnly;
            btnDisSave.Enabled = _ReadOnly;
            btnSave.Enabled = _ReadOnly;
            btnDisSave3.Enabled = _ReadOnly;
            btnDisSave3.Enabled = _ReadOnly;
            chkIsCancel.ReadOnly = true;
        }

        _ReadOnly = true;
        if (_EncStatus == 5)
        {
            chkIsCancel.EditValue = true;
            _ReadOnly = false;
            btnCancelDis.Enabled = _ReadOnly;
            btnDisSave.Enabled = _ReadOnly;
            btnSave.Enabled = _ReadOnly;
            btnDisSave3.Enabled = _ReadOnly;
            chkIsCancel.ReadOnly = true;
        }

        _ReadOnly = true;
        if (_EncStatus < 3)
        {
            _ReadOnly = false;
            btnSave.Enabled = !_ReadOnly;
            btnDisSave3.Enabled = !_ReadOnly;
            btnCancelDis.Enabled = _ReadOnly;
        }


        //if (_EncStatus != 3) _ReadOnly = true;

        //if (_EncStatus == 3) _ReadOnly = true;
        //btnDisSave3.Enabled = !_ReadOnly;
        //btnDisEncEndDateChange.Enabled = !_ReadOnly;
    }

    private void txtEncID_KeyPress(object sender, KeyPressEventArgs e)
    {
        _CurEncID = 0;
        if (e.KeyChar == 13 && !string.IsNullOrEmpty(txtEncID.Text)) EncSearch(Convert.ToInt32(txtEncID.Text));
    }

    private void GetEncOprData()
    {
        var dtEndOprData = _bllDis.GetEncOprData(_EncID);
        if (dtEndOprData.Rows.Count == 0) return;

        lblOprCategoryName.Text = dtEndOprData.Rows[0]["ChiefComplaint"].ToString();
        lblTotalOprTime.Text = dtEndOprData.Rows[0]["TotalOprTime"] + " دقیقه";
    }

    private void LoadEncBillData()
    {
        GetEncBillSummary();

        var bllEncBill = new EncBill();
        _dtEncBill = bllEncBill.GetEncBill(_EncID);
        if (_dtEncBill.Rows.Count > 0) grdEncBill.DataSource = _dtEncBill;

        var col = grvEncBill.Columns.ColumnByFieldName("RecNo");
        // col.FieldName = "RecNo";
        col.Visible = true;
        col.UnboundType = UnboundColumnType.Integer;
        grvEncBill.CustomUnboundColumnData += gridView1_CustomUnboundColumnData;

        void gridView1_CustomUnboundColumnData(object sender, CustomColumnDataEventArgs e)
        {
            if (e.IsGetData) e.Value = e.ListSourceRowIndex + 1;
        }

        _dtEncBillFull = bllEncBill.GetEncBillFull(_EncID);
        if (_dtEncBill.Rows.Count > 0)
        {
            grdEncBillDiscount.DataSource = _dtEncBillFull;
            UL.FillLookupRep(grdLueAccept, "4", cSet.cnStringA, true, "ID,Name", "vADR", "EncBillAmountCategory",
                "ADName='EncBillAmountCategory'", null);
        }


        var col1 = grvEncBillDiscount.Columns.ColumnByFieldName("RecNo");
        //col1.FieldName = "RecNo";
        col1.Visible = true;
        col1.UnboundType = UnboundColumnType.Integer;
        grvEncBillDiscount.CustomUnboundColumnData += gridView2_CustomUnboundColumnData;

        void gridView2_CustomUnboundColumnData(object sender, CustomColumnDataEventArgs e)
        {
            if (e.IsGetData) e.Value = e.ListSourceRowIndex + 1;
        }
    }

    private void GetEncBillSummary()
    {
        var bllEncBill = new EncBill();

        _dtEncBillSummary = bllEncBill.GetEncBillSummary(_EncID);
        if (_dtEncBillSummary.Rows.Count > 0)
        {
            lblTotalAmount.Text = Convert.ToInt64(_dtEncBillSummary.Rows[0]["TotalAmount"]).ToString("n0");
            lblPatientAmount.Text = Convert.ToInt64(_dtEncBillSummary.Rows[0]["PatientAmount"]).ToString("n0");
            lblDiscountAmount.Text = Convert.ToInt64(_dtEncBillSummary.Rows[0]["DiscountAmount"]).ToString("n0");
            lblCenterDiscountAmount.Text =
                Convert.ToInt64(_dtEncBillSummary.Rows[0]["CenterDiscountAmount"]).ToString("n0");
            lblCenterDiscountAmountA.Text =
                Convert.ToInt64(_dtEncBillSummary.Rows[0]["CenterDiscountAmountA"]).ToString("n0");
            lblSDrDiscountAmount.Text =
                Convert.ToInt64(_dtEncBillSummary.Rows[0]["SDrDiscountAmount"]).ToString("n0");
            lblADrDiscountAmount.Text =
                Convert.ToInt64(_dtEncBillSummary.Rows[0]["ADrDiscountAmount"]).ToString("n0");
            lblResultAmount.Text = Convert.ToInt64(_dtEncBillSummary.Rows[0]["ResultAmount"]).ToString("n0");
            lblPayAmount.Text = Convert.ToInt64(_dtEncBillSummary.Rows[0]["PayAmount"]).ToString("n0");
            lblRemainAmount.Text = Convert.ToInt64(_dtEncBillSummary.Rows[0]["RemainAmount"]).ToString("n0");

            lblSDrAmount.Text = Convert.ToInt64(_dtEncBillSummary.Rows[0]["SDrAmount"]).ToString("n0");
            lblSDrFeeAmount.Text = Convert.ToInt64(_dtEncBillSummary.Rows[0]["SDrFeeAmount"]).ToString("n0");
            lblADrAmount.Text = Convert.ToInt64(_dtEncBillSummary.Rows[0]["ADrAmount"]).ToString("n0");

            lblCenterAmount.Text = Convert.ToInt64(_dtEncBillSummary.Rows[0]["CenterAmount"]).ToString("n0");
            txtSDrAcceptAmount.EditValue = Convert.ToInt64(_dtEncBillSummary.Rows[0]["SDrAcceptAmount"]);


            var bllCash = new Cash();
            _EncCashDataList = bllCash.GetCashData(_EncID);

            grdEncCashData.DataSource = _EncCashDataList;
        }
    }


    private void grdEncBill_MouseDoubleClick(object sender, MouseEventArgs e)
    {
    }

    private void btnCancelDis_Click(object sender, EventArgs e)
    {
        var strMessage = _dtEncCur.Rows[0]["FirstName"] + " " + _dtEncCur.Rows[0]["LastName"]
                         + "\n"
                         + "\nاز ترخیص کنسل گردد؟";

        var sui = new SUI();
        var result = sui.FormShowMessage("کد پذیرش : " + _EncID, strMessage);

        if (result == DialogResult.No) return;

        var bllDis = new BLL.Dis.Dis();
        bllDis.DisEnc(_EncID, 2, GL.UserPersID);
        EncSearch(_EncID);
        ucShowTextReturnDis.ShowText("بیمار لغو ترخیص گردید", 5000);
    }

    private void btnDisSave_Click(object sender, EventArgs e)
    {
        if (_EncStatus == 3)
        {
            var strMessage = _dtEncCur.Rows[0]["FirstName"] + " " + _dtEncCur.Rows[0]["LastName"]
                             + "\n"
                             + "\nترخیص نهایی گردد؟";

            var sui = new SUI();
            var result = sui.FormShowMessage("کد پذیرش : " + _EncID, strMessage);

            if (result == DialogResult.No) return;

            var bllDis = new BLL.Dis.Dis();
            _dtEncDis = bllDis.DisEnc(_EncID, 4, GL.UserPersID);
        }

        EncSearch(_EncID);
        ucShowTextSave.ShowText("بیمار ترخیص گردید", 5000);
    }

    private void btnPrintDis_Click(object sender, EventArgs e)
    {
        if (!_ReadOnly) btnSave_Click(null, null);

        if (Convert.ToInt64(_dtEncBillSummary.Rows[0]["RemainAmount"]) > 0)
        {
            var strMessage = "بیمار به صندوق بدهکار میباشد";
            var sui = new SUI();
            var result = sui.ShowWarningMessageForm("کد پذیرش : " + _EncID, strMessage, true);
            //   return;
        }

        if (_EncStatus < 3)
        {
            var strMessage = "بیمار را ترخیص نمایید";
            var sui = new SUI();
            var result = sui.ShowWarningMessageForm("کد پذیرش : " + _EncID, strMessage, true);
            return;
        }


        var report = _bllReport.RepRptFileLoad(100001);
        _appStimul.PrintReport(cSet.cnStringA, report, "EncID", _EncID.ToString());

        _dtEncDis = _bllDis.DisEnc(_EncID, 4, GL.UserPersID);
        CheckReadOnly();
        EncSearch(_EncID);
    }


    private void panMain_Paint(object sender, PaintEventArgs e)
    {
        // App.Control.TextEditLine(lblTotalAmount, e);
    }

    private void btnPrintPreviewEncBill_Click(object sender, EventArgs e)
    {
        grdEncBill.ShowPrintPreview();
    }

    private void btnPrintPreviewEncDiscount_Click(object sender, EventArgs e)
    {
        grdEncBillDiscount.ShowPrintPreview();
    }

    private void btnEncBillDiscountSave_Click(object sender, EventArgs e)
    {
        //  EncBillDiscountSave();
    }

    private void EncBillDiscountSave()
    {
        var bllDiscount = new BLL.Dis.Discount();

        var EncBillID = 0;
        long DiscountAmount = 0;
        var AcceptCostCategory = 0;

        for (var i = 0; i < grvEncBillDiscount.RowCount; i++)
        {
            EncBillID = Convert.ToInt32(grvEncBillDiscount.GetRowCellValue(i, "EncBillID"));
            DiscountAmount = Convert.ToInt64(grvEncBillDiscount.GetRowCellValue(i, "DiscountAmount"));
            AcceptCostCategory = Convert.ToInt32(grvEncBillDiscount.GetRowCellValue(i, "AcceptCostCategory"));

            bllDiscount.EncBillDiscountSave(EncBillID, DiscountAmount, AcceptCostCategory);
        }

        EncBillSummarySave();
        GetEncBillSummary();
    }

    private void EncBillSummarySave()
    {
        long SDrAcceptAmount = 0;
        //if (_IsSDrAcceptAmount == true)
        //    txtSDrAcceptAmount.EditValue = Convert.ToInt64(lblRemainAmount.Text.Replace(",",string.Empty));


        SDrAcceptAmount = Convert.ToInt64(txtSDrAcceptAmount.EditValue);
        _bllEncBill.EncBillsummarySave(_EncID, _PersID, SDrAcceptAmount);
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
        EncBillDiscountSave();

        _dtEncDis = _bllDis.DisEncCalc(_EncID, _PersID);
        EncBillSummarySave();
        LoadEncBillData();
    }

    private void tab1_Paint(object sender, PaintEventArgs e)
    {
        Control.TextEditLine(txtSDrAcceptAmount, e);
    }

    private void btnPrintDrug_Click(object sender, EventArgs e)
    {
        var report = _bllReport.RepRptFileLoad(100002);
        _appStimul.PrintReport(cSet.cnStringA, report, "EncID", _EncID.ToString());
    }

    private void panelControl1_Paint(object sender, PaintEventArgs e)
    {
        Control.TextEditLine(txtEncID, e);
    }

    private void btnDisEncEndDateChange_Click(object sender, EventArgs e)
    {
        var appSDate = new SDate();

        var _EndDate = appSDate.SDateConvertFromString(datEndData.Text, timeEndData.Text);
        var _StartDate = Convert.ToDateTime(_dtEncCur.Rows[0]["StartDate"].ToString());
        if (_EndDate < _StartDate)
        {
            sui.ShowWarningMessageForm("خطا :", "تاریخ ترخیص کوچکتر از تاریخ پذیرش می باشد", true);
        }
        else

        {
            var bllDis = new BLL.Dis.Dis();
            _dtDisEncEndDateChange =
                bllDis.DisEncEndDateChange(_EncID, _PersID, datEndData.Text + " " + timeEndData.Text);


            EncSearch(_EncID);
            ucShowTextSave.ShowText("تغییر تاریخ ترخیص انجام گردید", 5000);
        }
    }

    private void chkIsCancel_CheckedChanged(object sender, EventArgs e)
    {
        if (chkIsCancel.Checked) return;

        if (Convert.ToInt64(_dtEncBillSummary.Rows[0]["PayAmount"]) > 0)
        {
            var strMessage = "لطفا با بیمار تسویه نمایید";
            var sui = new SUI();
            var result = sui.ShowWarningMessageForm("کد پذیرش : " + _EncID, strMessage, true);
            chkIsCancel.EditValue = false;
            return;
        }

        _dtEncDis = _bllDis.DisEnc(_EncID, 5, GL.UserPersID);


        var bllEnc = new EncList();
        _dtEncCur = bllEnc.EncSearch(_EncID, true);
        _EncStatus = Convert.ToByte(_dtEncCur.Rows[0]["EncStatus"]);

        ucShowTextReturnDis.ShowText("بیمار کنسل گردید", 5000);
        EncSearch(_EncID);
    }

    private void grvEncBill_RowCellStyle(object sender, RowCellStyleEventArgs e)
    {
        var view = sender as GridView;
        var ServiceStatus = Convert.ToInt32(view.GetRowCellValue(e.RowHandle, "ServiceStatus"));
        if (ServiceStatus == 1) e.Appearance.BackColor = Color.PaleVioletRed;
    }

    private void lblPrint_Click(object sender, EventArgs e)
    {
        grvEncCashData.BestFitColumns();
        grdEncCashData.ShowRibbonPrintPreview();
    }

    private void grvEncCashData_PrintInitialize(object sender, PrintInitializeEventArgs e)
    {
        var pb = e.PrintingSystem as PrintingSystemBase;
        var printableComponentLink = new PrintableComponentLink();
        pb.PageSettings.Landscape = true;
        pb.PageSettings.PaperKind = PaperKind.A4;
        pb.Document.AutoFitToPagesWidth = 1;
        pb.PageSettings.TopMargin = 100;
        pb.PageSettings.BottomMargin = 100;
        pb.PageSettings.LeftMargin = 40;
        pb.PageSettings.RightMargin = 40;

        grvEncCashData.OptionsPrint.PrintHeader = true;
        grvEncCashData.OptionsPrint.PrintFooter = true;

        (e.Link.PageHeaderFooter as PageHeaderFooter).Header.Content.Clear();
        (e.Link.PageHeaderFooter as PageHeaderFooter).Footer.Content.Clear();

        (e.Link.PageHeaderFooter as PageHeaderFooter).Header.LineAlignment = BrickAlignment.Center;
        (e.Link.PageHeaderFooter as PageHeaderFooter).Footer.LineAlignment = BrickAlignment.Center;

        (e.Link.PageHeaderFooter as PageHeaderFooter).Header.Font =
            new Font("IRANSansFaNum", 10, FontStyle.Regular);
        (e.Link.PageHeaderFooter as PageHeaderFooter).Footer.Font = new Font("IRANSansFaNum", 8);

        (e.Link.PageHeaderFooter as PageHeaderFooter).Header.Content.AddRange(new[]
            { grdEncCashData.Tag.ToString() });
        (e.Link.PageHeaderFooter as PageHeaderFooter).Footer.Content.AddRange(new[]
        {
            "[Date Printed]-[Time Printed] : تاریخ گزارش گیری  ", " [Pages #] از [Page #] : صفحه  ",
            "گزارش گیرنده : " + GL.UserPersName
        });
    }
}