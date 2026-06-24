using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using DAL;
using DevExpress.XtraEditors;
using LIB;
using Saman.APP.SettingApp;
using Saman.APP.Stimul;
using Control = App.Control;

namespace Saman.UC;

public partial class UcCashOther : XtraUserControl
{
    private readonly SettingApp _appSettingApp = new();
    private readonly Stimul _appStimul = new();

    private readonly BLL.Report.Report _bllReport = new();

    public UcCashOther()
    {
        InitializeComponent();
    }

    public int _ModID { get; set; }
    public int _DepID { get; set; }

    public int _EncID { get; set; }

    public int _PageID { get; set; }

    public int _CashID { get; set; }

    private void FormNull()
    {
        lblTitle.Text = null;
        lblTitle2.Text = null;
        lblTitleEncDate.Text = null;
        txtPayAmount.EditValue = null;
        lueTransType.EditValue = 0;
        luePayType.EditValue = 0;
        luePayGroup.EditValue = 0;
        lueDr.EditValue = 0;
        lueBank.EditValue = 0;
        txtBankNumber.Text = null;
        txtBankDate.Text = null;
        txtReceiver.Text = null;
        lblCashID.Text = null;
        lblCashDate.Text = null;
        ucMessageCashSave1.Visible = false;
        ucMessageCashNotSave1.Visible = false;
        _CashID = 0;

        datCashDate.Text = null;
        timCashTime.EditValue = "00:00";
    }

    private void UcCashOp_Load(object sender, EventArgs e)
    {
        FormNull();
        UL.LangChange(2);

        ActiveControl = txtPayAmount;

        UL.FillLookup(luePayType, "0", cSet.cnStringA, false, "ID,Name", "vADR", "PayType", "ADName='PayType'",
            null);
        UL.FillLookup(lueBank, "0", cSet.cnStringA, false, "ID,Name", "vADR", "Bank",
            "ADName='Bank' and IsDisable=0", null);
        UL.FillLookup(lueTransType, "1", cSet.cnStringA, false, "ID,Name", "vADR", "TransType",
            "ADName='TransType'", null);
        UL.FillLookup(luePayGroup, "0", cSet.cnStringA, false, "ID,Name", "vADR", "PayGroup", "ADName='PayGroup'",
            null);
        UL.FillLookup(lueAbout, "0", cSet.cnStringA, false, "ID,Name", "vADR", "CashAboutType",
            "ADName='CashAboutType'", null);
        UL.FillLookup(lueDr, "0", cSet.cnStringA, false, "*", "vDr", "Dr", null, null);
        txtReceiver.Properties.Mask.EditMask = App.UI.MaskPersianWithNum;

        ShortKey();
    }

    private void ShortKey()
    {
        msiSavePrint.Click += btnCashSave_Click;
        msiClose.Click += btnEsc_Click;
        msiNewCash.Click += lblCashNew_Click;

        panMain.Focus();
    }


    private void panMain_Paint(object sender, PaintEventArgs e)
    {
        Control.LookupEditLine(luePayType, e);
        Control.LookupEditLine(lueBank, e);
        Control.LookupEditLine(lueTransType, e);
        Control.LookupEditLine(luePayGroup, e);
        Control.LookupEditLine(lueAbout, e);
        Control.TextEditLine(txtBankDate, e);
        Control.TextEditLine(txtReceiver, e);
        Control.TextEditLine(txtBankNumber, e);
        Control.TextEditLine(txtPayAmount, e);
        Control.TextEditLine(txtNote, e);
    }


    private bool CheckValidation()
    {
        var isValid = true;

        if (_CashID > 0)
        {
            ucMessageCashSave1.Visible = false;
            ucMessageCashNotSave1.Visible = true;
            isValid = false;
        }

        if (txtPayAmount.Text == "0" || string.IsNullOrEmpty(txtPayAmount.Text))
        {
            txtPayAmount.ErrorText = "لطفا مبلغ راوارد نمایید";
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


        if (lueTransType.EditValue.ToString() == "0" || string.IsNullOrEmpty(lueTransType.Text))
        {
            lueTransType.ErrorText = "لطفا نوع تراکنش را انتخاب نمایید";
            isValid = false;
        }

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

        var con = new SqlConnection { ConnectionString = cSet.cnString };
        var cmd = new SqlCommand("spCash")
        {
            Connection = con,
            CommandType = CommandType.StoredProcedure
        };

        cmd.Parameters.AddWithValue("@PersID", GL.UserPersID);
        cmd.Parameters.AddWithValue("@ModID", _ModID);
        cmd.Parameters.AddWithValue("@DepID", _DepID);
        cmd.Parameters.AddWithValue("@PayAmount", Convert.ToInt64(txtPayAmount.EditValue));
        cmd.Parameters.AddWithValue("@PageID", _PageID);
        cmd.Parameters.AddWithValue("@CashType", 4);
        cmd.Parameters.AddWithValue("@PayGroup", luePayGroup.EditValue);
        cmd.Parameters.AddWithValue("@PayType", luePayType.EditValue);
        cmd.Parameters.AddWithValue("@Bank", lueBank.EditValue);
        cmd.Parameters.AddWithValue("@TransType", lueTransType.EditValue);
        cmd.Parameters.AddWithValue("@BankNumber", txtBankNumber.Text);
        cmd.Parameters.AddWithValue("@BankDate", txtBankDate.Text);
        cmd.Parameters.AddWithValue("@Note", txtNote.Text);
        cmd.Parameters.AddWithValue("@Receiver", txtReceiver.Text);
        cmd.Parameters.AddWithValue("@About", lueAbout.EditValue);
        cmd.Parameters.AddWithValue("@DrID", lueDr.EditValue);

        if (datCashDate.Enabled) cmd.Parameters.AddWithValue("@CashDate", datCashDate.Text + " " + timCashTime.Text);

        var dalData = new Data();
        var dtCash = dalData.GetDataTable(cmd);

        _CashID = (int)dtCash.Rows[0]["CashID"];

        lblCashID.Text = dtCash.Rows[0]["CashID"].ToString();
        lblCashDate.Text = dtCash.Rows[0]["CashDate"].ToString();

        datCashDate.EditValue = Convert.ToDateTime(dtCash.Rows[0]["CashDate"]);
        timCashTime.EditValue = DateTime.Parse(Convert.ToString(dtCash.Rows[0]["CashDate"]));


        var report = _bllReport.RepRptFileLoad(100004);
        _appStimul.PrintReport(cSet.cnStringA, report, "CashID", _CashID.ToString());

        ucMessageCashSave1.Visible = true;
    }

    private void btnEsc_Click(object sender, EventArgs e)
    {
        var panUC = Parent as PanelControl;
        panUC.Controls.Clear();

        var ucDefult = new UcDefult
            { Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom, Dock = DockStyle.Fill };
        panUC.Controls.Add(ucDefult);
    }

    private void luePayGroup_EditValueChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(luePayGroup.EditValue) == 2 || Convert.ToInt32(luePayGroup.EditValue) == 3)
        {
            lueDr.Visible = true;
            lblDr.Visible = true;
        }
        else
        {
            lueDr.Visible = false;
            lblDr.Visible = false;
        }
    }


    private void lblCashNew_Click(object sender, EventArgs e)
    {
        FormNull();
    }

    private void lueTransType_EditValueChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(lueTransType.EditValue) == 1)
        {
            lblPayAmountRial.ForeColor = Color.Red;
            txtPayAmount.ForeColor = Color.Red;
        }
        else
        {
            lblPayAmountRial.ForeColor = Color.Black;
            txtPayAmount.ForeColor = Color.Black;
        }
    }
}