using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using DAL;
using DevExpress.Data;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraPrinting;
using LIB;
using Saman.APP.SettingApp;
using Saman.APP.Stimul;
using Saman.BLL.Cash;
using Saman.BLL.Enc;
using Saman.Properties;
using Control = App.Control;

namespace Saman.UC;

public partial class UcCashEnc : XtraUserControl
{
    private readonly SettingApp _appSettingApp = new();
    private readonly Stimul _appStimul = new();
    private readonly BLL.Report.Report _bllReport = new();
    private readonly DataTable _dtEncCash;
    private readonly Cash bllCash = new();

    public DataTable _dtEncCur;

    private DataTable _EncCashDataList;


    private int _EncStatus;

    private long _RemainAmount;

    private DataRow[] _rowEncCur;
    private string _str;

    public UcCashEnc()
    {
        InitializeComponent();
    }

    public int _ModID { get; set; }
    public int _DepID { get; set; }
    public int _EncID { get; set; }
    public int _EncType { get; set; }
    public bool _ReadOnly { get; set; }

    public int _PageID { get; set; }
    public int _CashID { get; set; }

    private void FormNull()
    {
        _EncID = 0;
        _CashID = 0;
        lblTitle.Text = null;
        lblTitle2.Text = null;
        lblTitleEncDate.Text = null;
        txtCashDate.Text = null;
        lblCashID.Text = null;

        luePayType.EditValue = 1;
        luePayGroup.EditValue = 0;
        lueTransType.EditValue = 0;
        lueBank.EditValue = 0;
        txtBankNumber.Text = null;
        txtBankDate.Text = null;
        txtNote.Text = null;
        datCashDate.Text = null;
        timCashTime.EditValue = "00:00";
    }

    private void UcCashOp_Load(object sender, EventArgs e)
    {
        UL.LangChange(2);

        ActiveControl = txtEncID;


        _rowEncCur = GL.dtEncCur.Select("CurDepID=" + 1);
        if (_rowEncCur.Length > 0) _EncID = Convert.ToInt32(_rowEncCur[0]["EncID"]);

        if (_EncID > 0)
        {
            EncSearch(_EncID);
        }
        else
        {
            var bllEncList = new EncList();
            EncSearch(bllEncList.EncIDLast());
        }

        if (_EncType == 0) return;

        EncCashNew();


        UL.FillLookup(luePayType, "1", cSet.cnStringA, false, "ID,Name", "vADR", "PayType", "ADName='PayType'",
            null);
        UL.FillLookup(lueBank, "0", cSet.cnStringA, false, "ID,Name", "vADR", "Bank",
            "ADName='Bank' and IsDisable=0", null);
        UL.FillLookup(lueTransType, "1", cSet.cnStringA, false, "ID,Name", "vADR", "TransType",
            "ADName='TransType'", null);
        UL.FillLookup(luePayGroup, "0", cSet.cnStringA, false, "ID,Name", "vADR", "PayGroup", "ADName='PayGroup'",
            null);

        ShortKey();
    }

    private void ShortKey()
    {
        msiSavePrint.Click += btnCashSave_Click;
        msiClose.Click += btnEsc_Click;

        panMain.Focus();
    }

    private void EncSearch(int encID)
    {
        _EncID = 0;
        txtEncID.ErrorText = null;

        var bllEnc = new EncList();
        _dtEncCur = bllEnc.EncSearch(encID, true);
        _EncType = Convert.ToInt32(_dtEncCur.Rows[0]["EncType"].ToString());

        if (_EncType == 0)
        {
            FormNull();
            var strMessage = _dtEncCur.Rows[0]["FirstName"] + " " + _dtEncCur.Rows[0]["LastName"]
                             + "\n"
                             + "\n بیمار بستری نمی باشد";
            var sui = new SUI();
            var result = sui.ShowWarningMessageForm("کد پذیرش : " + _EncID, strMessage, true);
            return;
        }

        if (_dtEncCur.Rows.Count > 0)
        {
            txtEncID.Text = _dtEncCur.Rows[0]["EncID"].ToString();
            _EncID = Convert.ToInt32(_dtEncCur.Rows[0]["EncID"]);
            _EncStatus = Convert.ToInt32(_dtEncCur.Rows[0]["EncStatus"].ToString());
            txtEncID.Text = _EncID.ToString();

            if (_EncStatus == 5)
            {
                _str = " کنسل گردیده است";
                lblTitle.ForeColor = Color.DarkRed;
            }

            if (_EncStatus == 4)
            {
                _str = " ترخیص نهایی گردیده است";
                lblTitle.ForeColor = Color.Lime;
            }

            lblTitle.Text = _dtEncCur.Rows[0]["FirstName"] + " " + _dtEncCur.Rows[0]["LastName"] + "    " + _str;
            lblTitle2.Text = _dtEncCur.Rows[0]["Age"] + " سال - فرزند " + _dtEncCur.Rows[0]["FatherName"];
            ;
            lblTitleEncDate.Text = _dtEncCur.Rows[0]["StartDate"].ToString().Replace(' ', ' ');
            picGender.Image = Convert.ToByte(_dtEncCur.Rows[0]["Gender"]) == 1 ? Resources.male : Resources.female;
            lblPageDepName.Text = _dtEncCur.Rows[0]["DepName"].ToString();
            txtPayAmount.ReadOnly = false;
            txtPayAmount.EditValue = 0;
        }
        else
        {
            FormNull();
            txtEncID.ErrorText = "بیماری یافت نشد";
        }

        GL.SetEncCurVal(_DepID, _dtEncCur);


        CheckReadOnly();
    }


    private void CheckReadOnly()
    {
        _ReadOnly = false;
        if (_EncStatus >= 4) _ReadOnly = true;

        btnCashSave.Enabled = !_ReadOnly;
    }

    private void panMain_Paint(object sender, PaintEventArgs e)
    {
        Control.LookupEditLine(luePayType, e);
        Control.LookupEditLine(lueBank, e);
        //App.Control.LookupEditLine(lueTransType, e);
        Control.LookupEditLine(luePayGroup, e);
        Control.TextEditLine(txtBankDate, e);
        Control.TextEditLine(txtNote, e);
        Control.TextEditLine(txtBankNumber, e);
        Control.TextEditLine(txtPayAmount, e);
    }

    private void txtEncID_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == 13 && !string.IsNullOrEmpty(txtEncID.Text))
        {
            EncCashNew();

            if (_EncID == 0) return;

            var dtCashTotal = bllCash.GetCashTotal(_EncID);
            var TotalPayAmount = Convert.ToInt64(dtCashTotal.Rows[0]["TotalPayAmount"]);
            lblTotalPayAmount.Text = TotalPayAmount.ToString("n0");

            _RemainAmount = Convert.ToInt64(dtCashTotal.Rows[0]["RemainAmount"]);
            lblRemainAmount.Text = _RemainAmount.ToString("n0");


            if (_EncStatus < 3) return;

            txtPayAmount.EditValue = _RemainAmount.ToString("n0");
        }
    }


    private void EncCashNew()
    {
        FormNull();
        _EncID = Convert.ToInt32(txtEncID.Text);
        EncSearch(_EncID);

        if (_EncID == 0) return;

        var CashTotal = Convert.ToInt64(bllCash.GetCashTotal(_EncID).Rows[0]["TotalPayAmount"]);
        lblTotalPayAmount.Text = CashTotal.ToString("n0");

        _EncCashDataList = bllCash.GetCashData(_EncID);
        grdEncCashData.DataSource = _EncCashDataList;

        var col = grvEncCashData.Columns.ColumnByFieldName("RecNo");
        col.UnboundType = UnboundColumnType.Integer;
        grvEncCashData.CustomUnboundColumnData += gridView1_CustomUnboundColumnData;

        void gridView1_CustomUnboundColumnData(object sender, CustomColumnDataEventArgs e)
        {
            if (e.IsGetData) e.Value = e.ListSourceRowIndex + 1;
        }

        ActiveControl = txtPayAmount;
    }


    private void panelControl1_Paint(object sender, PaintEventArgs e)
    {
        Control.TextEditLine(txtEncID, e);
    }

    private bool CheckValidation()
    {
        var isValid = true;

        if (string.IsNullOrEmpty(txtEncID.Text))
        {
            txtEncID.ErrorText = "لطفا کد را وارد نمایید";

            isValid = false;
        }

        if (luePayType.EditValue.ToString() == "0" || string.IsNullOrEmpty(luePayType.Text))
        {
            luePayType.ErrorText = "لطفا نوع پرداخت را انتخاب نمایید";
            isValid = false;
        }

        if ((luePayType.EditValue.ToString() == "1" || luePayType.EditValue.ToString() == "3") &&
            (lueBank.EditValue.ToString() == "0" || string.IsNullOrEmpty(lueBank.Text)))
        {
            lueBank.ErrorText = "لطفا بانک را انتخاب نمایید";
            isValid = false;
        }

        if (luePayGroup.EditValue.ToString() == "0" || string.IsNullOrEmpty(luePayGroup.Text))
        {
            luePayGroup.ErrorText = "لطفا گروه پرداخت را انتخاب نمایید";
            isValid = false;
        }

        if (!string.IsNullOrEmpty(lblCashID.Text))
            //ucMessageCashNotSave1.Visible = true;
            isValid = false;

        if (datCashDate.Enabled && string.IsNullOrEmpty(datCashDate.Text))
        {
            datCashDate.ErrorText = "لطفا تاریخ را به صورت صحیح وارد نمایید";
            isValid = false;
        }

        if (timCashTime.Enabled && timCashTime.Text == "00:00")
        {
            timCashTime.ErrorText = "لطفا ساعت را به صورت صحیح وارد نمایید";
            isValid = false;
        }


        return isValid;
    }

    private void btnCashSave_Click(object sender, EventArgs e)
    {
        if (!CheckValidation()) return;

        var com = new SqlCommand();
        com.Parameters.AddWithValue("@PersID", GL.UserPersID);
        com.Parameters.AddWithValue("@ModID", _ModID);
        com.Parameters.AddWithValue("@DepID", _DepID);
        com.Parameters.AddWithValue("@PayAmount", Convert.ToInt64(txtPayAmount.EditValue));
        com.Parameters.AddWithValue("@EncID", txtEncID.Text);
        com.Parameters.AddWithValue("@PageID", _PageID);
        com.Parameters.AddWithValue("@CashType", 2);
        com.Parameters.AddWithValue("@PayGroup", luePayGroup.EditValue);
        com.Parameters.AddWithValue("@PayType", luePayType.EditValue);
        com.Parameters.AddWithValue("@Bank", lueBank.EditValue);
        com.Parameters.AddWithValue("@TransType", lueTransType.EditValue);
        com.Parameters.AddWithValue("@BankNumber", txtBankNumber.Text);
        com.Parameters.AddWithValue("@BankDate", txtBankDate.Text);
        com.Parameters.AddWithValue("@Note", txtNote.Text);

        if (datCashDate.Enabled) com.Parameters.AddWithValue("@CashDate", datCashDate.Text + " " + timCashTime.Text);

        var dalData = new Data();
        var dtCash = dalData.GetDataTableBySP(cSet.cnStringA, com, "spCash");


        _CashID = (int)dtCash.Rows[0]["CashID"];

        lblCashID.Text = dtCash.Rows[0]["CashID"].ToString();
        // txtCashDate.Text = dtCash.Rows[0]["CashDate"].ToString();
        datCashDate.EditValue = Convert.ToDateTime(dtCash.Rows[0]["CashDate"]);
        timCashTime.EditValue = DateTime.Parse(Convert.ToString(dtCash.Rows[0]["CashDate"]));


        // txtDiscountAmount.Text = dtCash.Rows[0]["RemainAmount"].ToString();
        var totalPayAmount = Convert.ToInt64(dtCash.Rows[0]["TotalPayAmount"]);
        lblTotalPayAmount.Text = totalPayAmount.ToString("n0");

        txtPayAmount.ReadOnly = true;

        ucShowTextSave.ShowText("قبض با موفقیت ذخیره گردید", 5000);

        _EncCashDataList = bllCash.GetCashData(_EncID);
        grdEncCashData.DataSource = _EncCashDataList;

        var report = _bllReport.RepRptFileLoad(100003);
        _appStimul.PrintReport(cSet.cnStringA, report, "CashID", _CashID.ToString());

        EncCashNew();
    }


    private void btnEsc_Click(object sender, EventArgs e)
    {
        var panUC = Parent as PanelControl;
        panUC.Controls.Clear();

        var ucDefult = new UcDefult
            { Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom, Dock = DockStyle.Fill };
        panUC.Controls.Add(ucDefult);
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