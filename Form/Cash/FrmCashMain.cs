using System;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using Saman.APP.SAC;
using Saman.UC;

namespace Saman.Form.Cash;

public partial class FrmCashMain : XtraForm
{
    private readonly string _FormName = "صندوق";

    private readonly SACPermission sacPermission = new();
    public int _DepID = 3;
    public int _ModID = 2;

    public FrmCashMain()
    {
        InitializeComponent();
    }

    private void FrmCashMain_Load(object sender, EventArgs e)
    {
        barBtnStatus();
        CheckPermission();
        panUC.Controls.Clear();
        var ucControl = new UcDefult
            { Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom, Dock = DockStyle.Fill };
        panUC.Controls.Add(ucControl);
    }

    private void CheckPermission()
    {
        barBtnCashPage.Enabled = sacPermission.CheckPermission(_ModID, 20001);
        barBtnCashEnc.Enabled = sacPermission.CheckPermission(_ModID, 20002);
        barBtnCashOther.Enabled = sacPermission.CheckPermission(_ModID, 20003);
        barBtnReportDP.Enabled = sacPermission.CheckPermission(_ModID, 20004);
        barBtnCashReport.Enabled = sacPermission.CheckPermission(_ModID, 20005);
        barBtnCashOtherSearch.Enabled = sacPermission.CheckPermission(_ModID, 20006);
        barBtnHesabdari.Enabled = sacPermission.CheckPermission(_ModID, 20007);
        barBtnCPS.Enabled = sacPermission.CheckPermission(_ModID, 20008);
        barBtnReport.Enabled = sacPermission.CheckPermission(_ModID, 20009);
    }

    private void barBtnStatus()
    {
        barBtnCashPage.Enabled = false;
        barBtnCashEnc.Enabled = false;
        barBtnCashOther.Enabled = false;
        barBtnReportDP.Enabled = false;
        barBtnCashReport.Enabled = false;
        barBtnCashOtherSearch.Enabled = false;
        barBtnHesabdari.Enabled = false;
        barBtnCPS.Enabled = false;
        barBtnReport.Enabled = false;
    }

    private void barBtnCashOther_ItemClick(object sender, ItemClickEventArgs e)
    {
        var ucControl = new UcCashOther { _ModID = _ModID, _DepID = _DepID };
        ShowUC(ucControl);
    }


    private void barBtnCashOtherSearch_ItemClick(object sender, ItemClickEventArgs e)
    {
        var ucControl = new UcCashReceiptSearch { _ModID = _ModID };
        ShowUC(ucControl);
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

    private void barBtnReportDP_ItemClick(object sender, ItemClickEventArgs e)
    {
        var ucControl = new UcCashReceiptSearch { _ModID = _ModID };
        ShowUC(ucControl);
    }

    private void barBtnCashReport_ItemClick(object sender, ItemClickEventArgs e)
    {
        var ucControl = new UcCashReport { _ModID = _ModID };
        ShowUC(ucControl);
    }

    private void barBtnCashPage_ItemClick(object sender, ItemClickEventArgs e)
    {
        var ucControl = new UcCashPage { _ModID = _ModID, _DepID = _DepID };
        ShowUC(ucControl);
    }

    private void barBtnCashEnc_ItemClick(object sender, ItemClickEventArgs e)
    {
        var ucControl = new UcCashEnc { _ModID = _ModID }; //, _DepID = _DepID };
        ShowUC(ucControl);
    }
}