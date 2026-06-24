using System;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using DAL;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraPrinting;
using LIB;
using Saman.APP.SettingApp;
using Saman.BLL.Enc;
using Saman.Form.Enc;
using Saman.Properties;

namespace Saman.UC;

public partial class UcDiscount : XtraUserControl
{
    private readonly SettingApp _appSettingApp = new();
    private readonly BLL.Discount.Discount _bllDiscount = new();


    private readonly BLL.Page.Page _bllPage = new();

    private readonly DataTable _dtCashWait;

    private readonly DataTable _dtEncCash;

    private readonly long _RemainAmount;

    private readonly DataRow[] _rowEncCur;

    private readonly DataRow _rowEncSelect;

    public int _CashType = 1;

    public DataTable _DiscountDataList;


    public DataTable _dtEncCur;

    public DataTable _dtPage;
    private int _EncStatus;
    private string _str;

    public UcDiscount()
    {
        InitializeComponent();
    }

    public int _ModID { get; set; }
    public int _EncID { get; set; }
    public int _EncType { get; set; }

    public bool _ReadOnly { get; set; }

    public int _PageID { get; set; }
    public int _PageCode { get; set; }
    public int _DepID { get; set; }


    public int _CashID { get; set; }

    public int _Width { get; set; }
    public int _Height { get; set; }

    private void FormNull()
    {
        _EncID = 0;
        _PageID = 0;
        _EncType = -1;
        _CashID = 0;

        lblTitle.Text = null;
        lblTitle2.Text = null;
        lblTitle3.Text = null;
        lblTitleEncDate.Text = null;

        //lueDiscountReferType.EditValue = null;
        //lueDiscountActionTaken.EditValue = null;
        lueDiscountType.EditValue = "0";
        txtTotalAmount.Text = "0";
        txtInsuranceAmount.Text = "0";
        txtPatientAmount.Text = "0";
        txtTotalPayAmount.Text = "0";
        txtDiscountAmount.Text = "0";

        grdDiscount.DataSource = null;
    }

    private void UcDiscount_Load(object sender, EventArgs e)
    {
        UL.LangChange(2);
        ActiveControl = ragEncType;
        //txtPayAmount.Properties.Mask.EditMask = App.UI.MaskNumNegative;

        UL.FillLookup(lueDiscountReferType, "1", cSet.cnStringA, false, "ID,Name", "vADR", "DiscountReferType",
            "ADName='DiscountReferType'", null);
        UL.FillLookup(lueDiscountActionTaken, "1", cSet.cnStringA, false, "ID,Name", "vADR", "DiscountActionTaken",
            "ADName='DiscountActionTaken'", null);
        UL.FillLookup(lueDiscountType, "0", cSet.cnStringA, false, "ID,Name", "vADR", "DiscountType",
            "ADName='DiscountType' AND isDisable = 0 ", null);
    }

    private void EncSearch(int encID)
    {
        //FormNull();

        _EncID = 0;
        txtEncID.ErrorText = null;

        var bllEnc = new EncList();
        _dtEncCur = bllEnc.EncSearch(encID, true);

        if (_dtEncCur.Rows.Count > 0)
        {
            txtEncID.Text = _dtEncCur.Rows[0]["EncID"].ToString();
            _EncID = Convert.ToInt32(_dtEncCur.Rows[0]["EncID"]);

            _EncStatus = Convert.ToInt32(_dtEncCur.Rows[0]["EncStatus"]);
            _EncType = Convert.ToInt32(_dtEncCur.Rows[0]["EncType"]);

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

            lblTitle.Text = _dtEncCur.Rows[0]["FirstName"] + " " + _dtEncCur.Rows[0]["LastName"] + " - کد پذیرش " +
                            _EncID + " " + _str;
            lblTitle2.Text = _dtEncCur.Rows[0]["Age"] + " سال - فرزند " + _dtEncCur.Rows[0]["FatherName"] +
                             " - ملیت " + _dtEncCur.Rows[0]["CountryName"]
                             + " - علت مراجعه " + _dtEncCur.Rows[0]["ChiefComplaintName"] + " - بخش " +
                             _dtEncCur.Rows[0]["DepName"]
                             + " - سرویس " + _dtEncCur.Rows[0]["EncServiceName"] + " - بیمه " +
                             _dtEncCur.Rows[0]["InsuranceName"];

            lblTitle3.Text = " آدرس " + _dtEncCur.Rows[0]["AddressFull"] + " - موبایل " +
                             _dtEncCur.Rows[0]["Mobile"];
            lblTitleEncDate.Text = _dtEncCur.Rows[0]["StartDate"].ToString().Replace(' ', ' ');
            picGender.Image = Convert.ToByte(_dtEncCur.Rows[0]["Gender"]) == 1 ? Resources.male : Resources.female;
        }
        else
        {
            txtEncID.ErrorText = "بیماری یافت نشد";
        }

        GL.SetEncCurVal(_DepID, _dtEncCur);


        CheckReadOnly();
    }

    private void CheckReadOnly()
    {
        _ReadOnly = false;
        if (_EncStatus >= 4) _ReadOnly = true;

        btnSave.Enabled = !_ReadOnly;
    }

    private void txtEncID_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == 13 && !string.IsNullOrEmpty(txtEncID.Text)) EncSearch(Convert.ToInt32(txtEncID.Text));

        var sui = new SUI();
        if (_EncType != 1 && _EncType != 2) sui.ShowWarningMessageForm("پیام", "بیمار بسنری نمی باشد", true);
    }


    private bool CheckValidation()
    {
        var isValid = true;

        if (lueDiscountActionTaken.EditValue.ToString() == "0" || string.IsNullOrEmpty(lueDiscountActionTaken.Text))
        {
            lueDiscountActionTaken.ErrorText = "لطفا اقدام انجام شده را انتخاب نمایید";
            isValid = false;
        }

        if (lueDiscountReferType.EditValue.ToString() == "0" || string.IsNullOrEmpty(lueDiscountReferType.Text))
        {
            lueDiscountReferType.ErrorText = "لطفا دلیل مراجعه را انتخاب نمایید";
            isValid = false;
        }

        if (lueDiscountType.EditValue.ToString() == "0" || string.IsNullOrEmpty(lueDiscountType.Text))
        {
            lueDiscountType.ErrorText = "لطفا گروه تخفیف را انتخاب نمایید";
            isValid = false;
        }

        if (txtDiscountAmount.Text == "0")
        {
            txtDiscountAmount.ErrorText = "لطفا مبلغ تخفیف را وارد نمایید";
            isValid = false;
        }

        if (Convert.ToInt64(txtDiscountAmount.EditValue) > Convert.ToInt64(txtPayAmount.EditValue))
        {
            txtDiscountAmount.ErrorText = " مبلغ تخفیف از مبلغ قابل دریافت بیشتر است";
            isValid = false;
        }

        return isValid;
    }


    private void btnEsc_Click(object sender, EventArgs e)
    {
        var panUC = Parent as PanelControl;
        panUC.Controls.Clear();

        var ucDefult = new UcDefult
            { Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom, Dock = DockStyle.Fill };
        panUC.Controls.Add(ucDefult);
    }


    private void txtPageCode_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == 13 && !string.IsNullOrEmpty(txtPageDepID.Text) && !string.IsNullOrEmpty(txtPageCode.Text))
        {
            _PageCode = Convert.ToInt32(txtPageCode.Text);
            _DepID = Convert.ToInt32(txtPageDepID.Text);

            PageLoad(_PageCode, _DepID);
            ActiveControl = lueDiscountType;
        }
    }

    private void PageLoad(int PageCode, int DepID)
    {
        FormNull();

        var sui = new SUI();

        _dtPage = _bllPage.PageCashLoad(0, DepID, PageCode);

        if (_dtPage.Rows.Count == 0)
        {
            sui.ShowWarningMessageForm("پیام", "بیماری یافت نشد", true);
            return;
        }

        _EncID = Convert.ToInt32(_dtPage.Rows[0]["EncID"]);
        _PageID = Convert.ToInt32(_dtPage.Rows[0]["PageID"]);
        _EncType = Convert.ToInt32(_dtPage.Rows[0]["EncType"]);
        _PageCode = Convert.ToInt32(_dtPage.Rows[0]["PageCode"]);
        _DepID = Convert.ToInt32(_dtPage.Rows[0]["DepID"]);


        if (_EncType != 0)
        {
            sui.ShowWarningMessageForm("پیام", "بیمار سرپایی نمی باشد", true);
            return;
        }

        txtPageCode.Text = _PageCode.ToString();
        txtPageDepID.Text = _DepID.ToString();

        EncSearch(_EncID);
        PageDataLoad();

        _DiscountDataList = _bllDiscount.DiscountLoad(1, _PageID);
        grdDiscount.DataSource = _DiscountDataList;
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

            //long DiscountAmount = Convert.ToInt64(_dtPage.Rows[0]["DiscountAmount"]);
            //txtDiscountAmount.Text = DiscountAmount.ToString("n0");

            var RemainAmount = Convert.ToInt64(_dtPage.Rows[0]["RemainAmount"]);
            txtPayAmount.EditValue = RemainAmount.ToString();
            //  txtDiscountAmount.EditValue = RemainAmount.ToString();
        }
        else
        {
            FormNull();
            txtPageCode.ErrorText = "بیماری یافت نشد";
        }

        CheckReadOnly();
    }

    private void ragEncType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ragEncType.SelectedIndex == 1)
        {
            lblListIP.Enabled = true;
            lblListOP.Enabled = false;

            panPageCode.Visible = false;
            panEncID.Visible = true;
            FormNull();
            ActiveControl = txtEncID;
        }

        if (ragEncType.SelectedIndex == 0)
        {
            lblListIP.Enabled = false;
            lblListOP.Enabled = true;

            panPageCode.Visible = true;
            panEncID.Visible = false;
            FormNull();
            ActiveControl = txtPageDepID;
        }
    }

    private void panMain_SizeChanged(object sender, EventArgs e)
    {
        _Width = panMain.Width;
        _Height = panMain.Height;
    }

    private void lblListOP_Click(object sender, EventArgs e)
    {
        var frm = new FrmEncPageSearch
        {
            Width = _Width,
            Height = _Height,

            StartPosition = FormStartPosition.Manual
        };
        var panelCenterPosition = panMain.PointToScreen(new Point(panMain.Width / 2, panMain.Height / 2));
        frm.SetDesktopLocation(panelCenterPosition.X - frm.Width / 2, panelCenterPosition.Y - frm.Height / 2);

        frm._SearchType = 0;
        var result = frm.ShowDialog();

        if (result == DialogResult.Yes) PageLoad(frm._PageCode, frm._PageDepID);
    }

    private void lblListIP_Click(object sender, EventArgs e)
    {
        var frm = new FrmEncPageSearch
        {
            Width = _Width,
            Height = _Height,

            StartPosition = FormStartPosition.Manual
        };
        var panelCenterPosition = panMain.PointToScreen(new Point(panMain.Width / 2, panMain.Height / 2));
        frm.SetDesktopLocation(panelCenterPosition.X - frm.Width / 2, panelCenterPosition.Y - frm.Height / 2);

        frm._SearchType = 2;
        frm.ShowDialog();
    }

    private void btnBillLoad_Click(object sender, EventArgs e)
    {
        if (ragEncType.SelectedIndex == -1) return;

        var frm = new FrmEncPageSearch
        {
            Width = _Width,
            Height = _Height,

            StartPosition = FormStartPosition.Manual
        };
        var panelCenterPosition = panMain.PointToScreen(new Point(panMain.Width / 2, panMain.Height / 2));
        frm.SetDesktopLocation(panelCenterPosition.X - frm.Width / 2, panelCenterPosition.Y - frm.Height / 2);
        frm.ShowDialog();
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
        if (!CheckValidation()) return;

        var IsDisable = false;

        _bllDiscount.DiscountSave(0, _ModID, _EncID, _PageID, IsDisable, _EncType
            , Convert.ToInt32(lueDiscountReferType.EditValue), Convert.ToInt32(lueDiscountActionTaken.EditValue)
            , Convert.ToInt32(lueDiscountType.EditValue), _DepID, Convert.ToInt64(txtDiscountAmount.EditValue)
            , txtNote.Text, GL.UserPersID);


        ucShowTextSave.ShowText("تخفیف با موفقیت ذخیره گردید", 5000);

        PageLoad(_PageCode, _DepID);
    }

    private void lblPrint_Click(object sender, EventArgs e)
    {
        grvDiscount.BestFitColumns();
        grdDiscount.ShowRibbonPrintPreview();
    }

    private void grvDiscount_PrintInitialize(object sender, PrintInitializeEventArgs e)
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

        grvDiscount.OptionsPrint.PrintHeader = true;
        grvDiscount.OptionsPrint.PrintFooter = true;

        (e.Link.PageHeaderFooter as PageHeaderFooter).Header.Content.Clear();
        (e.Link.PageHeaderFooter as PageHeaderFooter).Footer.Content.Clear();

        (e.Link.PageHeaderFooter as PageHeaderFooter).Header.LineAlignment = BrickAlignment.Center;
        (e.Link.PageHeaderFooter as PageHeaderFooter).Footer.LineAlignment = BrickAlignment.Center;

        (e.Link.PageHeaderFooter as PageHeaderFooter).Header.Font =
            new Font("IRANSansFaNum", 10, FontStyle.Regular);
        (e.Link.PageHeaderFooter as PageHeaderFooter).Footer.Font = new Font("IRANSansFaNum", 8);

        (e.Link.PageHeaderFooter as PageHeaderFooter).Header.Content.AddRange(new[] { grdDiscount.Tag.ToString() });
        (e.Link.PageHeaderFooter as PageHeaderFooter).Footer.Content.AddRange(new[]
        {
            "[Date Printed]-[Time Printed] : تاریخ گزارش گیری  ", " [Pages #] از [Page #] : صفحه  ",
            "گزارش گیرنده : " + GL.UserPersName
        });
    }
}