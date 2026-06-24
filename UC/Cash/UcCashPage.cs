using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using DAL;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using LIB;
using Saman.APP.SAC;
using Saman.APP.SettingApp;
using Saman.APP.Stimul;
using Saman.BLL.Cash;
using Saman.BLL.Enc;
using Saman.Properties;
using Stimulsoft.Report.Dictionary;
using Control = App.Control;

namespace Saman.UC;

public partial class UcCashPage : XtraUserControl
{
    private readonly SettingApp _appSettingApp = new();


    private readonly Stimul _appStimul = new();

    private readonly Cash _bllCashWaitList = new();
    private readonly BLL.Page.Page _bllPage = new();
    private readonly BLL.Report.Report _bllReport = new();

    private readonly DataTable _dtEncCash;

    private readonly DataRow[] _rowEncCur;

    private readonly Cash bllCash = new();
    private readonly SACPermission sacPermission = new();

    public int _CashType = 1;

    private DataTable _dtCashWait;
    public DataTable _dtEncCur;

    public DataTable _dtPage, _dtCash;

    public DataTable _EncCashDataList;

    private int _EncStatus;


    private long _RemainAmount;

    private DataRow _rowEncSelect;
    private string _str;


    public UcCashPage()
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
        txtCashID.Text = null;

        datCashDate.Text = null;
        timCashTime.EditValue = "00:00";

        luePayGroup.EditValue = null;
        luePayType.EditValue = null;
        lueBank.EditValue = null;
        txtTotalAmount.Text = "0";
        txtInsuranceAmount.Text = "0";
        txtPatientAmount.Text = "0";
        txtTotalPayAmount.Text = "0";
        txtDiscountAmount.Text = "0";

        grdEncCashData.DataSource = null;
    }

    private void UcCashPage_Load(object sender, EventArgs e)
    {
        UL.LangChange(2);

        ActiveControl = txtEncID;
        CheckPermission();

        if (_EncType == 1 || _EncType == 2) return;

        EncCashNew();

        UL.FillLookup(luePayType, "1", cSet.cnStringA, false, "ID,Name", "vADR", "PayType", "ADName='PayType'",
            null);
        UL.FillLookup(lueBank, "0", cSet.cnStringA, false, "ID,Name", "vADR", "Bank",
            "ADName='Bank' and IsDisable=0", null);
        UL.FillLookup(luePayGroup, "0", cSet.cnStringA, false, "ID,Name", "vADR", "PayGroup", "ADName='PayGroup'",
            null);
    }

    private void CheckPermission()
    {
        txtCashID.Enabled = lblCashID.Enabled = sacPermission.CheckPermission(_ModID, 200011);
    }

    private void EncSearch(int encID)
    {
        _EncID = 0;
        txtEncID.ErrorText = null;
        _str = null;
        lblTitle.ForeColor = Color.White;

        var bllEnc = new EncList();
        _dtEncCur = bllEnc.EncSearch(encID, true);

        if (_dtEncCur.Rows.Count > 0)
        {
            txtEncID.Text = _dtEncCur.Rows[0]["EncID"].ToString();
            _EncID = Convert.ToInt32(_dtEncCur.Rows[0]["EncID"]);
            _EncType = Convert.ToInt32(_dtEncCur.Rows[0]["EncType"].ToString());

            if (_EncType == 1 || _EncType == 2)
            {
                FormNull();
                var strMessage = _dtEncCur.Rows[0]["FirstName"] + " " + _dtEncCur.Rows[0]["LastName"]
                                 + "\n"
                                 + "\n بیمار سرپایی نمی باشد";
                var sui = new SUI();
                var result = sui.ShowWarningMessageForm("کد پذیرش : " + _EncID, strMessage, true);
                return;
            }

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
            lblTitleEncDate.Text = _dtEncCur.Rows[0]["StartDate"].ToString().Replace(' ', ' ');
            picGender.Image = Convert.ToByte(_dtEncCur.Rows[0]["Gender"]) == 1 ? Resources.male : Resources.female;
            lblPageDepName.Text = _dtEncCur.Rows[0]["DepName"].ToString();
        }
        else
        {
            FormNull();
            txtEncID.ErrorText = "بیماری یافت نشد";
        }

        GL.SetEncCurVal(_DepID, _dtEncCur);


        CheckReadOnly();
    }

    private void PageLoad(int PageID)
    {
        _dtPage = _bllPage.PageCashLoad(_PageID, 0, 0);
        PageDataLoad();
        EncSearch(_EncID);
    }

    private void PageDataLoad()
    {
        if (_dtPage.Rows.Count > 0)
        {
            txtEncID.Text = _dtPage.Rows[0]["EncID"].ToString();
            _EncID = Convert.ToInt32(_dtPage.Rows[0]["EncID"]);
            txtPageCode.Text = _dtPage.Rows[0]["PageCode"].ToString();

            var TotalAmount = Convert.ToInt64(_dtPage.Rows[0]["TotalAmount"]);
            txtTotalAmount.Text = TotalAmount.ToString("n0");

            var InsuranceAmount = Convert.ToInt64(_dtPage.Rows[0]["InsuranceAmount"]);
            txtInsuranceAmount.Text = InsuranceAmount.ToString("n0");

            var PatientAmount = Convert.ToInt64(_dtPage.Rows[0]["PatientAmount"]);
            txtPatientAmount.Text = PatientAmount.ToString("n0");

            var PayAmount = Convert.ToInt64(_dtPage.Rows[0]["PayAmount"]);
            txtTotalPayAmount.EditValue = PayAmount.ToString("n0");

            var DiscountAmount = Convert.ToInt64(_dtPage.Rows[0]["DiscountAmount"]);
            txtDiscountAmount.Text = DiscountAmount.ToString("n0");

            var RemainAmount = Convert.ToInt64(_dtPage.Rows[0]["RemainAmount"]);
            txtPayAmount.EditValue = RemainAmount.ToString();
        }
        else
        {
            FormNull();
            txtPageCode.ErrorText = "بیماری یافت نشد";
        }

        CheckReadOnly();
    }

    private void CashDataLoad()
    {
        var CashPayAmount = Convert.ToInt64(_dtCash.Rows[0]["PayAmount"]);
        txtPayAmount.EditValue = CashPayAmount.ToString();
        luePayGroup.EditValue = _dtCash.Rows[0]["PayGroup"];
        luePayType.EditValue = _dtCash.Rows[0]["PayType"];
        lueBank.EditValue = _dtCash.Rows[0]["Bank"];
        txtNote.Text = _dtCash.Rows[0]["Note"].ToString();

        datCashDate.EditValue = Convert.ToDateTime(_dtCash.Rows[0]["CashDate"]);
        timCashTime.EditValue = DateTime.Parse(Convert.ToString(_dtCash.Rows[0]["CashDate"]));

        CheckReadOnly();
    }

    private void CheckReadOnly()
    {
        _ReadOnly = false;
        if (_EncStatus >= 4)
        {
            _ReadOnly = true;
            btnCashSave.Enabled = !_ReadOnly;
        }

        if (_CashID != 0)
        {
            _ReadOnly = true;
            txtPayAmount.Enabled = !_ReadOnly;
        }
        else
        {
            txtPayAmount.Enabled = true;
            txtCashID.Text = null;
            datCashDate.Text = null;
            timCashTime.EditValue = "00:00";
        }
    }

    private void panMain_Paint(object sender, PaintEventArgs e)
    {
    }

    private void txtEncID_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == 13 && !string.IsNullOrEmpty(txtEncID.Text))
        {
            EncCashNew();

            if (_EncID == 0) return;

            var dtCashTotal = bllCash.GetCashTotal(_EncID);
            var TotalPayAmount = Convert.ToInt64(dtCashTotal.Rows[0]["TotalPayAmount"]);

            _RemainAmount = Convert.ToInt64(dtCashTotal.Rows[0]["RemainAmount"]);

            if (_EncStatus < 3)
            {
            }
        }
    }


    private void EncCashNew()
    {
        FormNull();
        if (_EncID == 0) return;

        _EncCashDataList = bllCash.GetCashDataPage(_EncID, _PageID);
        grdEncCashData.DataSource = _EncCashDataList;
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

        //if (!string.IsNullOrEmpty(txtCashID.Text))
        //{
        //    //ucMessageCashNotSave1.Visible = true;

        //    isValid = false;
        //}

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

        if (luePayType.EditValue.ToString() == "0" || string.IsNullOrEmpty(luePayType.Text))
        {
            luePayType.ErrorText = "لطفا نوع پرداخت را انتخاب نمایید";
            isValid = false;
        }

        if (luePayGroup.EditValue.ToString() == "0" || string.IsNullOrEmpty(luePayGroup.Text))
        {
            luePayGroup.ErrorText = "لطفا گروه پرداخت را انتخاب نمایید";
            isValid = false;
        }

        if (lueBank.EditValue.ToString() == "0" || string.IsNullOrEmpty(lueBank.Text))
        {
            lueBank.ErrorText = "لطفا بانک را انتخاب نمایید";
            isValid = false;
        }


        return isValid;
    }

    private void btnCashSave_Click(object sender, EventArgs e)
    {
        if (!CheckValidation()) return;

        var com = new SqlCommand();
        com.Parameters.AddWithValue("@CashType", 1);
        com.Parameters.AddWithValue("@PersID", GL.UserPersID);
        com.Parameters.AddWithValue("@ModID", _ModID);
        com.Parameters.AddWithValue("@DepID", _DepID);
        com.Parameters.AddWithValue("@EncID", txtEncID.Text);
        com.Parameters.AddWithValue("@PageID", _PageID);
        com.Parameters.AddWithValue("@PayGroup", luePayGroup.EditValue);
        com.Parameters.AddWithValue("@PayType", luePayType.EditValue);
        com.Parameters.AddWithValue("@Bank", lueBank.EditValue);
        com.Parameters.AddWithValue("@Note", txtNote.Text);
        //com.Parameters.AddWithValue("@PayAmount", Convert.ToInt64(value: txtPayAmount.Text.Replace(",","")));
        com.Parameters.AddWithValue("@PayAmount", Convert.ToInt64(txtPayAmount.EditValue));

        //com.Parameters.AddWithValue("@TransType", lueTransType.EditValue);
        //com.Parameters.AddWithValue("@BankNumber", txtBankNumber.Text);
        //com.Parameters.AddWithValue("@BankDate", txtBankDate.Text);

        if (datCashDate.Enabled) com.Parameters.AddWithValue("@CashDate", datCashDate.Text + " " + timCashTime.Text);

        if (_CashID > 0)
        {
            com.Parameters.AddWithValue("@CRUD", 2);
            com.Parameters.AddWithValue("@CashID", _CashID);
        }

        var dalData = new Data();
        var dtCash = dalData.GetDataTableBySP(cSet.cnStringA, com, "spCash");

        var CashID = (int)dtCash.Rows[0]["CashID"];
        txtCashID.Text = dtCash.Rows[0]["CashID"].ToString();
        datCashDate.EditValue = Convert.ToDateTime(dtCash.Rows[0]["CashDate"]);
        timCashTime.EditValue = DateTime.Parse(Convert.ToString(dtCash.Rows[0]["CashDate"]));
        var totalPayAmount = Convert.ToInt32(dtCash.Rows[0]["TotalPayAmount"]);

        ucShowTextSave.ShowText("قبض با موفقیت ذخیره گردید", 5000);

        _EncCashDataList = bllCash.GetCashDataPage(_EncID, _PageID);
        grdEncCashData.DataSource = _EncCashDataList;

        if (_CashID == 0) CashPrint(CashID.ToString());
        // EncCashNew();
        //Stimulsoft.Report.StiReport report = _bllReport.RepRptFileLoad(100003);
        //_appStimul.PrintReport(cSet.cnStringA, report, "CashID", CashID.ToString());
        PageLoad(_PageID);
        grvCashWait_Click(null, null);

        //_dtCash = _bllPage.CashLoadByCashID(_CashID);

        //if (_CashID > 0)
        //{
        //    _dtCash = _bllPage.CashLoadByCashID(_CashID);
        //    CashDataLoad();
        //    return;
        //}
    }


    private void btnEsc_Click(object sender, EventArgs e)
    {
        var panUC = Parent as PanelControl;
        panUC.Controls.Clear();

        var ucDefult = new UcDefult
            { Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom, Dock = DockStyle.Fill };
        panUC.Controls.Add(ucDefult);
    }

    private void lblRefreshListWiat_Click(object sender, EventArgs e)
    {
        LoadCashWaitLiast();
    }

    private void chkOtherDays_CheckedChanged(object sender, EventArgs e)
    {
        LoadCashWaitLiast();
    }

    private void LoadCashWaitLiast()
    {
        _dtCashWait = _bllCashWaitList.CashWaitListLoad(1, _CashType, chkOtherDays.Checked);

        grdCashWait.DataSource = _dtCashWait;
        var rowSelectgrv = grvCashWait.GetFocusedDataSourceRowIndex();
        grvCashWait.SelectRow(rowSelectgrv);
        grvCashWait.FocusedRowHandle = rowSelectgrv;
    }

    private void grvCashWait_GotFocus(object sender, EventArgs e)
    {
        LoadCashWaitLiast();
    }

    private void txtPageDepID_KeyPress(object sender, KeyPressEventArgs e)
    {
    }

    private void txtPageCode_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == 13 && !string.IsNullOrEmpty(txtPageDepID.Text) && !string.IsNullOrEmpty(txtPageCode.Text))
        {
            _CashID = 0;
            CheckReadOnly();

            var DepID = Convert.ToInt32(txtPageDepID.EditValue);
            var PageCode = Convert.ToInt32(txtPageCode.EditValue);

            _dtPage = _bllPage.PageCashLoad(0, DepID, PageCode);

            if (_dtPage.Rows.Count == 0) return;

            PageDataLoad();

            _EncID = Convert.ToInt32(_dtPage.Rows[0]["EncID"]);
            _PageID = Convert.ToInt32(_dtPage.Rows[0]["PageID"]);

            EncSearch(_EncID);


            _EncCashDataList = bllCash.GetCashDataPage(_EncID, _PageID);
            grdEncCashData.DataSource = _EncCashDataList;
        }
    }

    private void grvCashWait_Click(object sender, EventArgs e)
    {
        _rowEncSelect = grvCashWait.GetDataRow(grvCashWait.FocusedRowHandle);
        if (_rowEncSelect == null) return;

        txtPageDepID.Text = _rowEncSelect["DepID"].ToString();

        _EncID = Convert.ToInt32(_rowEncSelect["EncID"]);
        _PageID = Convert.ToInt32(_rowEncSelect["PageID"]);

        _EncCashDataList = bllCash.GetCashDataPage(_EncID, _PageID);
        grdEncCashData.DataSource = _EncCashDataList;

        PageLoad(_PageID);
        EncSearch(_EncID);

        _CashID = 0;
        CheckReadOnly();
    }

    private void CashPrint(string cashid)
    {
        var tsqlvCash =
            @"select * 
                        from vCash
                        INNER JOIN SamanHIS.dbo.vEncFullName e ON e.EncID = vCash.EncID
                        WHERE CashID=@CashID";

        tsqlvCash = tsqlvCash.Replace("@CashID", cashid);


        var report = _bllReport.RepRptFileLoad(100003);

        var sti = new StiSqlDatabase("SamanServer", cSet.cnStringA);
        report.Dictionary.Databases[0] = sti;

        ((StiSqlSource)report.Dictionary.DataSources["vCash"]).SqlCommand = tsqlvCash;
        report.Print();
    }

    private void barBtnDetailServiceReg_ItemClick(object sender, ItemClickEventArgs e)
    {
        var tsqlvServiceRegFull =
            @"select * from vServiceRegFull
                  WHERE EncID=@EncID AND ServiceCategory not in (31,32,33,34) AND ServiceType<>2";

        tsqlvServiceRegFull = tsqlvServiceRegFull.Replace("@EncID", _EncID.ToString());

        var tsqlvEncFullName =
            @"  select * 
                    FROM [SamanHIS].[dbo].vEncFullName e
                    LEFT OUTER JOIN [SamanHIS].[dbo].[vEncInsurance2] ei ON ei.EncID = e.EncID
                    WHERE e.EncID=@EncID";

        tsqlvEncFullName = tsqlvEncFullName.Replace("@EncID", _EncID.ToString());


        var tsqlvPage =
            @"select * from vPage
                  WHERE EncID=@EncID";

        tsqlvPage = tsqlvPage.Replace("@EncID", _EncID.ToString());

        var tsqlRptProperty =
            @"
               SELECT
               SamanHIS.dbo.SCurDateTime() AS PrintDate
               ,@UserPersName AS ReportUserName ";

        tsqlRptProperty = tsqlRptProperty.Replace("@UserPersName", "'" + GL.UserPersName + "'");

        var report = _bllReport.RepRptFileLoad(100019);

        var sti = new StiSqlDatabase("SamanServer", cSet.cnStringA);
        report.Dictionary.Databases[0] = sti;

        ////var t = report.Dictionary.DataSources.Items;

        ((StiSqlSource)report.Dictionary.DataSources["vServiceRegFull"]).SqlCommand = tsqlvServiceRegFull;
        ((StiSqlSource)report.Dictionary.DataSources["vEncFullName"]).SqlCommand = tsqlvEncFullName;
        ((StiSqlSource)report.Dictionary.DataSources["vPage"]).SqlCommand = tsqlvPage;
        ((StiSqlSource)report.Dictionary.DataSources["RptProperty"]).SqlCommand = tsqlRptProperty;

        report.Show();
    }

    private void barBtnDetailServiceRegWithoutAmount_ItemClick(object sender, ItemClickEventArgs e)
    {
        var tsqlvServiceRegFull =
            @"select * from vServiceRegFull
                  WHERE EncID=@EncID AND ServiceCategory not in (31,32,33,34) AND ServiceType<>2";

        tsqlvServiceRegFull = tsqlvServiceRegFull.Replace("@EncID", _EncID.ToString());

        var tsqlvEncFullName =
            @"select * from vEncFullName
                  WHERE EncID=@EncID";

        tsqlvEncFullName = tsqlvEncFullName.Replace("@EncID", _EncID.ToString());


        var tsqlRptProperty =
            @"
               SELECT
               SamanHIS.dbo.SCurDateTime() AS PrintDate
               ,@UserPersName AS ReportUserName ";

        tsqlRptProperty = tsqlRptProperty.Replace("@UserPersName", "'" + GL.UserPersName + "'");

        var report = _bllReport.RepRptFileLoad(100020);

        var sti = new StiSqlDatabase("SamanServer", cSet.cnStringA);
        report.Dictionary.Databases[0] = sti;

        ////var t = report.Dictionary.DataSources.Items;

        ((StiSqlSource)report.Dictionary.DataSources["vServiceRegFull"]).SqlCommand = tsqlvServiceRegFull;
        ((StiSqlSource)report.Dictionary.DataSources["vEncFullName"]).SqlCommand = tsqlvEncFullName;
        ((StiSqlSource)report.Dictionary.DataSources["RptProperty"]).SqlCommand = tsqlRptProperty;

        report.Show();
    }

    private void txtCashID_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == 13 && !string.IsNullOrEmpty(txtCashID.Text))
        {
            _CashID = Convert.ToInt32(txtCashID.Text);
            _dtCash = _bllPage.CashLoadByCashID(_CashID);

            if (_dtCash.Rows.Count == 0)
            {
                _CashID = 0;
                return;
            }

            _EncID = Convert.ToInt32(_dtCash.Rows[0]["EncID"]);
            _PageID = Convert.ToInt32(_dtCash.Rows[0]["PageID"]);

            _dtPage = _bllPage.PageCashLoad(_PageID, 0, 0);

            PageDataLoad();
            EncSearch(_EncID);
            CashDataLoad();

            _EncCashDataList = bllCash.GetCashDataPage(_EncID, _PageID);
            grdEncCashData.DataSource = _EncCashDataList;


            //   grvEncCashData.FocusedRowHandle = 3;
        }
    }
}