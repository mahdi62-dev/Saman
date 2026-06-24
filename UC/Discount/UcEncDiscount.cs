using System;
using System.Data;
using System.Windows.Forms;
using DAL;
using DevExpress.Data;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
using LIB;
using Saman.BLL.Cash;
using Saman.BLL.Dis;
using Saman.BLL.Enc;
using Saman.Properties;
using Control = App.Control;

namespace Saman.UC.Discount;

public partial class UcEncDiscount : XtraUserControl
{
    private readonly BLL.Dis.Dis _bllDis = new();
    private readonly EncBill _bllEncBill = new();
    private readonly DataTable _dtEncCash;

    private readonly DataTable _dtEncDis;
    public int _CurEncID;
    private DataTable _dtEncBill;
    private DataTable _dtEncBillFull;
    private DataTable _dtEncBillSummary;
    private DataTable _dtEncCur;

    private DataTable _EncCashDataList;

    private bool _IsSDrAcceptAmount;

    public int _PersID;

    private DataRow[] _rowEncCur;

    public UcEncDiscount()
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
        lblTitleEncStatus.Visible = false;

        lblPageID.Text = null;
        lblDepName.Text = null;
        lblEndData.Text = null;
        lblDrName.Text = null;
        lblTotalAmount.Text = null;
        lblPatientAmount.Text = null;
        lblDiscountAmount.Text = null;
        lblResultAmount.Text = null;
        lblPayAmount.Text = null;
        lblRemainAmount.Text = null;
        lblSDrAmount.Text = null;
        lblADrAmount.Text = null;
        lblCenterAmount.Text = null;
        lblCenterDiscountAmountA.Text = null;
        grdEncBillDiscount.DataSource = null;
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
        msiSave.Click += btnDisSave_Click;
        msiClose.Click += btnCancel_Click;

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
            ;
            lblTitleEncDate.Text = _dtEncCur.Rows[0]["StartDate"].ToString().Replace(' ', ' ');
            lblEndData.Text = _dtEncCur.Rows[0]["EndDate"].ToString().Replace(' ', ' ');
            picGender.Image = Convert.ToByte(_dtEncCur.Rows[0]["Gender"]) == 1 ? Resources.male : Resources.female;
            lblDepName.Text = _dtEncCur.Rows[0]["DepName"].ToString();
            lblDrName.Text = _dtEncCur.Rows[0]["DrName"].ToString();

            _IsSDrAcceptAmount = (bool)_dtEncCur.Rows[0]["IsSDrAcceptAmount"];
            lblSDrFixAmount.Text = Convert.ToInt32(_dtEncCur.Rows[0]["SDrFixAmount"]).ToString("n0");
            chkIsSDrAcceptAmount.EditValue = (bool)_dtEncCur.Rows[0]["IsSDrAcceptAmount"];

            _EncStatus = Convert.ToByte(_dtEncCur.Rows[0]["EncStatus"]);

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

    private void CheckReadOnly()
    {
        _ReadOnly = false;
        if (_EncStatus >= 4) _ReadOnly = true;

        btnDisSave.Enabled = !_ReadOnly;
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
            lblTotalAmount.Text = Convert.ToInt32(_dtEncBillSummary.Rows[0]["TotalAmount"]).ToString("n0");
            lblPatientAmount.Text = Convert.ToInt32(_dtEncBillSummary.Rows[0]["PatientAmount"]).ToString("n0");
            lblDiscountAmount.Text = Convert.ToInt32(_dtEncBillSummary.Rows[0]["DiscountAmount"]).ToString("n0");
            lblResultAmount.Text = Convert.ToInt32(_dtEncBillSummary.Rows[0]["ResultAmount"]).ToString("n0");
            lblPayAmount.Text = Convert.ToInt32(_dtEncBillSummary.Rows[0]["PayAmount"]).ToString("n0");
            lblRemainAmount.Text = Convert.ToInt32(_dtEncBillSummary.Rows[0]["RemainAmount"]).ToString("n0");

            lblSDrAmount.Text = Convert.ToInt32(_dtEncBillSummary.Rows[0]["SDrAmount"]).ToString("n0");
            lblSDrFeeAmount.Text = Convert.ToInt32(_dtEncBillSummary.Rows[0]["SDrFeeAmount"]).ToString("n0");
            lblADrAmount.Text = Convert.ToInt32(_dtEncBillSummary.Rows[0]["ADrAmount"]).ToString("n0");

            lblCenterAmount.Text = Convert.ToInt32(_dtEncBillSummary.Rows[0]["CenterAmount"]).ToString("n0");


            txtCenterDiscountAmountA.Text =
                Convert.ToInt32(_dtEncBillSummary.Rows[0]["RemainAmount"]).ToString("n0");

            lblCenterDiscountAmountA.Text =
                Convert.ToInt32(_dtEncBillSummary.Rows[0]["CenterDiscountAmountA"]).ToString("n0");


            var bllCash = new Cash();
            _EncCashDataList = bllCash.GetCashData(_EncID);
        }
    }


    private void btnDisSave_Click(object sender, EventArgs e)
    {
        _PersID = GL.UserPersID;
        var bllDisDiscount = new BLL.Dis.Discount();
        bllDisDiscount.CenterDiscountASave(_EncID, Convert.ToInt64(txtCenterDiscountAmountA.Text.Replace(",", "")),
            _PersID);

        ucShowTextSave.ShowText("تخفیف اعمال گردید", 5000);
    }


    private void btnPrintPreviewEncDiscount_Click(object sender, EventArgs e)
    {
        grdEncBillDiscount.ShowPrintPreview();
    }


    private void tab1_Paint(object sender, PaintEventArgs e)
    {
        Control.TextEditLine(txtCenterDiscountAmountA, e);
        Control.TextEditLine(txtNote, e);
    }

    private void panelControl1_Paint(object sender, PaintEventArgs e)
    {
        Control.TextEditLine(txtEncID, e);
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
    }
}