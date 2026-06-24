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
using Saman.BLL.Loan;
using Control = App.Control;

namespace Saman.UC.Loan;

public partial class UcLoanAssign : XtraUserControl
{
    private readonly LoanAssign _bllLoanAssign = new();
    private readonly LoanAssign _bllLoanInstallAssign = new();

    private readonly DataTable _dtLoanAssign;
    private int _CRUD;

    private DataTable _dtLoan = new();

    private DataTable _dtPersLoanAssign;
    private DataTable _dtPersLoanInstallAssign;

    private int _LoanAssignID;
    private int _PersID;

    public UcLoanAssign()
    {
        InitializeComponent();
    }

    public int _ModID { get; set; }
    public int _DepID { get; set; }


    private int _PersLoanRecNo { get; set; }

    public bool _ReadOnly { get; set; }

    private void FormNull()
    {
        _LoanAssignID = 0;
        lblLoanAssignID.Text = null;
        datLoanAssignDate.Text = null;
        datLoanAssignStartDate.Text = null;
        lblInstallPayCount.Text = null;
        lblInstallPayAmount.Text = null;
        lblInstallRemainCount.Text = null;
        lblInstallRemainAmount.Text = null;
        lueLoan.EditValue = 0;
        txtLoanBaseAmount.Text = "0";
        txtLoanFeePer.Text = "0";
        lblLoanAmount.Text = "0";
        txtInstallCount.Text = "0";
        lblInstallAmount.Text = null;
        lblInstallLastAmount.Text = null;
        txtDelayFineAmount.Text = "0";
        txtNote.Text = null;
        _PersLoanRecNo = 0;
    }

    private void UcLoanAssign_Load(object sender, EventArgs e)
    {
        UL.LangChange(2);

        ActiveControl = luePers;

        UL.FillLookup(luePers, "0", cSet.cnStringERP, false, "PersID,Name", "vPersonnel", "Personnel", null, null);

        lblCancelLoan.Enabled = false;
        ShortKey();
    }

    private void ShortKey()
    {
        msiSave.Click += btnLoanSave_Click;
        msiClose.Click += btnEsc_Click;

        panMain.Focus();
    }


    private void luePers_EditValueChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(luePers.EditValue) == 0) return;

        _PersID = Convert.ToInt32(luePers.EditValue);
        FormNull();
        PersLoanAssignLoad(_PersID);
    }

    private void PersLoanAssignLoad(int PersID)
    {
        _dtPersLoanAssign = _bllLoanAssign.PersLoanLoad(_PersID, _LoanAssignID);

        _PersLoanRecNo = _dtPersLoanAssign.Rows.Count;

        if (_PersLoanRecNo == 0) lblPageList.Text = " ";

        if (_PersLoanRecNo > 0)
        {
            _LoanAssignID = Convert.ToInt32(_dtPersLoanAssign.Rows[_PersLoanRecNo - 1]["LoanAssignID"].ToString());
            LoanAssignLoad(_LoanAssignID);
            lblPageList.Text = "صفحه " + _PersLoanRecNo + " از " + _dtPersLoanAssign.Rows.Count;
        }

        _dtPersLoanInstallAssign = _bllLoanInstallAssign.PersLoanInstallLoad(_PersID, _LoanAssignID);
        grdPersLoanInstallAssign.DataSource = _dtPersLoanInstallAssign;
    }

    private void LoanAssignLoad(int LoanAssignID)
    {
        var rowLoanAssign = _dtPersLoanAssign.Select("LoanAssignID=" + _LoanAssignID)[0];
        lblLoanAssignID.Text = _LoanAssignID.ToString();
        datLoanAssignDate.DateTime = Convert.ToDateTime(rowLoanAssign["AssignDate"].ToString());
        datLoanAssignStartDate.DateTime = Convert.ToDateTime(rowLoanAssign["InstallStartDate"].ToString());
        lblInstallPayCount.Text = rowLoanAssign["InstallPayCount"].ToString();
        lblInstallPayAmount.Text = rowLoanAssign["InstallPayAmount"].ToString();
        lblInstallRemainCount.Text = rowLoanAssign["InstallRemainCount"].ToString();
        lblInstallRemainAmount.Text = rowLoanAssign["InstallRemainAmount"].ToString();
        lblInstallRemainAmount.Text = rowLoanAssign["InstallRemainAmount"].ToString();
        lblInstallRemainAmount.Text = rowLoanAssign["InstallRemainAmount"].ToString();
        lueLoan.EditValue = Convert.ToInt32(rowLoanAssign["LoanID"].ToString());
        txtLoanBaseAmount.Text = rowLoanAssign["LoanBaseAmount"].ToString();
        txtLoanFeePer.Text = rowLoanAssign["LoanFeePer"].ToString();
        lblLoanAmount.Text = rowLoanAssign["LoanAmount"].ToString();
        txtInstallCount.Text = rowLoanAssign["InstallCount"].ToString();
        lblInstallAmount.Text = rowLoanAssign["InstallAmount"].ToString();
        lblInstallLastAmount.Text = rowLoanAssign["InstallLastAmount"].ToString();
        txtDelayFineAmount.Text = rowLoanAssign["DelayFineAmount"].ToString();
        txtNote.Text = rowLoanAssign["Description"].ToString();


        CheckReadOnly();

        _dtPersLoanInstallAssign = _bllLoanInstallAssign.PersLoanInstallLoad(_PersID, _LoanAssignID);
        grdPersLoanInstallAssign.DataSource = _dtPersLoanInstallAssign;
    }

    private void PageNavigate(int navigate)
    {
        if (_dtPersLoanAssign == null || _dtPersLoanAssign.Rows.Count == 0 || _LoanAssignID == 0) return;

        _PersLoanRecNo = Convert.ToInt32(_dtPersLoanAssign.Select("LoanAssignID=" + _LoanAssignID)[0]["RecNo"]);
        _PersLoanRecNo = _PersLoanRecNo + navigate;

        if ((_PersLoanRecNo >= 1 && _PersLoanRecNo <= _dtPersLoanAssign.Rows.Count && _PersLoanRecNo > 0)
            || (_PersLoanRecNo <= _dtPersLoanAssign.Rows.Count && _PersLoanRecNo > 0 && _PersLoanRecNo > 0))
        {
            _LoanAssignID = Convert.ToInt32(_dtPersLoanAssign.Rows[_PersLoanRecNo - 1]["LoanAssignID"].ToString());

            if (_LoanAssignID > 0) LoanAssignLoad(_LoanAssignID);

            lblPageList.Text = "صفحه " + _PersLoanRecNo + " از " + _dtPersLoanAssign.Rows.Count;
        }
    }

    private void lblPagePreRec_Click(object sender, EventArgs e)
    {
        PageNavigate(-1);
    }


    private void lblPageNextRec_Click(object sender, EventArgs e)
    {
        PageNavigate(1);
    }

    private void CheckReadOnly()
    {
        _ReadOnly = false;
        if (_PersLoanRecNo > 0) _ReadOnly = true;

        datLoanAssignDate.ReadOnly = _ReadOnly;
        datLoanAssignStartDate.ReadOnly = _ReadOnly;
        lueLoan.ReadOnly = _ReadOnly;
        txtLoanBaseAmount.ReadOnly = _ReadOnly;
        txtLoanFeePer.ReadOnly = _ReadOnly;
        txtInstallCount.ReadOnly = _ReadOnly;
        txtDelayFineAmount.ReadOnly = _ReadOnly;
        txtNote.ReadOnly = _ReadOnly;
    }

    private void panMain_Paint(object sender, PaintEventArgs e)
    {
        Control.TextEditLine(lueLoan, e);
        Control.TextEditLine(txtLoanBaseAmount, e);
        Control.TextEditLine(txtLoanFeePer, e);
        Control.TextEditLine(txtInstallCount, e);
        Control.TextEditLine(txtDelayFineAmount, e);
        Control.TextEditLine(txtNote, e);
    }


    private void panelControl1_Paint(object sender, PaintEventArgs e)
    {
        Control.TextEditLine(luePers, e);
        Control.TextEditLine(datLoanAssignDate, e);
        Control.TextEditLine(datLoanAssignStartDate, e);
    }

    private bool CheckValidation()
    {
        var isValid = true;

        if (luePers.EditValue.ToString() == "0" || string.IsNullOrEmpty(luePers.Text))
        {
            luePers.ErrorText = "لطفا پرسنل را انتخاب نمایید";
            isValid = false;
        }

        if (string.IsNullOrEmpty(datLoanAssignDate.Text))
        {
            datLoanAssignDate.ErrorText = "لطفا تاریخ را وارد نمایید";
            isValid = false;
        }

        if (string.IsNullOrEmpty(datLoanAssignStartDate.Text))
        {
            datLoanAssignStartDate.ErrorText = "لطفا تاریخ را وارد نمایید";
            isValid = false;
        }

        //if (lueLoan.EditValue.ToString() == "0" || string.IsNullOrEmpty(lueLoan.Text))
        //{
        //    lueLoan.ErrorText = "لطفا نوع وام را انتخاب نمایید";
        //    isValid = false;
        //}

        if (string.IsNullOrEmpty(txtLoanBaseAmount.Text) || txtLoanBaseAmount.EditValue.ToString() == "0")
        {
            txtLoanBaseAmount.ErrorText = "لطفا مبلغ وام را وارد نمایید";
            isValid = false;
        }

        if (string.IsNullOrEmpty(txtInstallCount.Text) || txtInstallCount.EditValue.ToString() == "0")
        {
            txtInstallCount.ErrorText = "لطفا تعداد اقساط را وارد نمایید";
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

    private void btnLoanSave_Click(object sender, EventArgs e)
    {
        if (!CheckValidation()) return;

        _CRUD = 0;
        if (_dtPersLoanAssign.Rows.Count != 0)
        {
            _CRUD = 1;
            ucShowTextSave.ShowText(" وام ذخیره گردیده است", 9000);
        }
        else
        {
            var bllLoan = new LoanAssign();
            _dtLoan = bllLoan.LoanSave(_CRUD, _PersID, datLoanAssignDate.Text + " 00:00", 0, "الفا",
                txtLoanBaseAmount.EditValue,
                txtLoanFeePer.Text, txtInstallCount.Text, Convert.ToInt64(lblLoanAmount.Text),
                Convert.ToInt64(lblInstallAmount.Text), Convert.ToInt64(lblInstallLastAmount.Text),
                txtDelayFineAmount.EditValue, datLoanAssignStartDate.Text + " 00:00",
                Convert.ToString(txtNote.Text), GL.UserPersID);

            luePers.EditValue = _PersID;
            PersLoanAssignLoad(_PersID);

            ucShowTextSave.ShowText(" وام با موفقیت ذخیره گردید", 9000);
            lblCancelLoan.Enabled = false;
        }
    }

    private void txtLoanFeePer_EditValueChanged(object sender, EventArgs e)
    {
        var _BaseAount = Convert.ToInt64(txtLoanBaseAmount.EditValue);
        float _LoanFeePer = Convert.ToInt64(txtLoanFeePer.EditValue);
        _LoanFeePer = _LoanFeePer / 100;
        _BaseAount = _BaseAount + (long)(_BaseAount * _LoanFeePer);
        lblLoanAmount.Text = _BaseAount.ToString();
    }

    private void txtLoanBaseAmount_EditValueChanged(object sender, EventArgs e)
    {
        var _BaseAount = Convert.ToInt64(txtLoanBaseAmount.EditValue);
        float _LoanFeePer = Convert.ToInt64(txtLoanFeePer.EditValue);
        _LoanFeePer = _LoanFeePer / 100;
        _BaseAount = _BaseAount + (long)(_BaseAount * _LoanFeePer);
        lblLoanAmount.Text = _BaseAount.ToString();
    }

    private void txtInstallCount_EditValueChanged(object sender, EventArgs e)
    {
        var _BaseAount = Convert.ToInt64(lblLoanAmount.Text);
        var _InstallCount = Convert.ToInt64(txtInstallCount.EditValue);
        if (_BaseAount > 0 && _InstallCount > 0)
        {
            var _InstallAmount = _BaseAount / _InstallCount;

            lblInstallAmount.Text = _InstallAmount.ToString();
            lblInstallLastAmount.Text = _InstallAmount.ToString();
        }
    }

    private void lblPageLoan_Click(object sender, EventArgs e)
    {
        AddNewLoan();
    }

    private void lblCancelLoan_Click(object sender, EventArgs e)
    {
        _PersID = Convert.ToInt32(luePers.EditValue);
        FormNull();
        PersLoanAssignLoad(_PersID);
        lblCancelLoan.Enabled = false;
    }

    private void AddNewLoan()
    {
        FormNull();

        datLoanAssignDate.ReadOnly = false;
        datLoanAssignStartDate.ReadOnly = false;
        lueLoan.ReadOnly = false;
        txtLoanBaseAmount.ReadOnly = false;
        txtLoanFeePer.ReadOnly = false;
        txtInstallCount.ReadOnly = false;
        txtDelayFineAmount.ReadOnly = false;
        txtNote.ReadOnly = false;
        lblCancelLoan.Enabled = true;


        _dtPersLoanAssign.Clear();

        if (_dtLoanAssign != null) _dtLoanAssign.Clear();

        if (_dtPersLoanInstallAssign == null) return;

        _dtPersLoanInstallAssign.Clear();
        grdPersLoanInstallAssign.DataSource = _dtPersLoanInstallAssign;

        //lblPageID.Text = null;
    }

    private void lblPrint_Click(object sender, EventArgs e)
    {
        grvPersLoanInstallAssign.BestFitColumns();
        grdPersLoanInstallAssign.ShowRibbonPrintPreview();
    }

    private void grvPersLoanInstallAssign_PrintInitialize(object sender, PrintInitializeEventArgs e)
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

        grvPersLoanInstallAssign.OptionsPrint.PrintHeader = true;
        grvPersLoanInstallAssign.OptionsPrint.PrintFooter = true;

        (e.Link.PageHeaderFooter as PageHeaderFooter).Header.Content.Clear();
        (e.Link.PageHeaderFooter as PageHeaderFooter).Footer.Content.Clear();

        (e.Link.PageHeaderFooter as PageHeaderFooter).Header.LineAlignment = BrickAlignment.Center;
        (e.Link.PageHeaderFooter as PageHeaderFooter).Footer.LineAlignment = BrickAlignment.Center;

        (e.Link.PageHeaderFooter as PageHeaderFooter).Header.Font =
            new Font("IRANSansFaNum", 10, FontStyle.Regular);
        (e.Link.PageHeaderFooter as PageHeaderFooter).Footer.Font = new Font("IRANSansFaNum", 8);

        (e.Link.PageHeaderFooter as PageHeaderFooter).Header.Content.AddRange(new[]
            { grdPersLoanInstallAssign.Tag.ToString() });
        (e.Link.PageHeaderFooter as PageHeaderFooter).Footer.Content.AddRange(new[]
        {
            "[Date Printed]-[Time Printed] : تاریخ گزارش گیری  ", " [Pages #] از [Page #] : صفحه  ",
            "گزارش گیرنده : " + GL.UserPersName
        });
    }
}