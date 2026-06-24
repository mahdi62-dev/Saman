using System;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using Saman.APP.SAC;
using Saman.UC;
using Saman.UC.Loan;

namespace Saman.Form.Loan;

public partial class FrmLoan : XtraForm
{
    private readonly string _FormName = "سیستم وام";

    private readonly SACPermission sacPermission = new();
    public byte _ModID = 104;

    public FrmLoan()
    {
        InitializeComponent();
    }

    public int _Year { get; set; }
    public int _Mon { get; set; }

    private void FrmLoan_Load(object sender, EventArgs e)
    {
        barBtnStatus();
        CheckPermission();

        var ucDefult = new UcDefult
            { Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom, Dock = DockStyle.Fill };
        panUC.Controls.Add(ucDefult);
    }

    private void CheckPermission()
    {
        barbtnLoanAccount.Enabled = sacPermission.CheckPermission(_ModID, 1040001);
        barBtnLoan.Enabled = sacPermission.CheckPermission(_ModID, 1040002);
        barBtnLoanAssign.Enabled = sacPermission.CheckPermission(_ModID, 1040003);
        barBtnLoanPay.Enabled = sacPermission.CheckPermission(_ModID, 1040004);
    }

    private void barBtnStatus()
    {
        barbtnLoanAccount.Enabled = false;
        barBtnLoan.Enabled = false;
        barBtnLoanAssign.Enabled = false;
        barBtnLoanPay.Enabled = false;
    }


    private void ShowUC(object ucControl)
    {
        panUC.Controls.Clear();
        var myUC = (UserControl)ucControl;
        myUC.Anchor = AnchorStyles.Top;
        myUC.Anchor = AnchorStyles.Right;
        myUC.Dock = DockStyle.Fill;

        Text = _FormName + " - " + myUC.Tag;
        panUC.Controls.Add(myUC);
    }

    private void barBtnLoanAssign_ItemClick(object sender, ItemClickEventArgs e)
    {
        var ucControl = new UcLoanAssign { _ModID = _ModID };
        ShowUC(ucControl);
    }
}