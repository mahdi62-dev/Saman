using System;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using Saman.APP.SAC;
using Saman.UC;
using Saman.UC.DocReport;
using Saman.UC.Report;

namespace Saman.Form.FrmReport;

public partial class FrmReport : XtraForm
{
    private readonly string _FormName = "سیستم گزارش";
    private readonly SACPermission sacPermission = new();
    public byte _ModID = 98;

    public FrmReport()
    {
        InitializeComponent();
    }

    private void FrmReport_Load(object sender, EventArgs e)
    {
        barBtnStatus();
        CheckPermission();

        var ucDefult = new UcDefult
            { Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom, Dock = DockStyle.Fill };
        panUC.Controls.Add(ucDefult);
    }

    private void CheckPermission()
    {
        barBtnRepList.Enabled = sacPermission.CheckPermission(_ModID, 980001);
        barBtnReportDef.Enabled = sacPermission.CheckPermission(_ModID, 980002);
        barBtnDocSetting.Enabled = sacPermission.CheckPermission(_ModID, 980003);
    }

    private void barBtnStatus()
    {
        barBtnRepList.Enabled = false;
        barBtnDocSetting.Enabled = false;
        barBtnReportDef.Enabled = false;
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


    private void barBtnRepList_ItemClick(object sender, ItemClickEventArgs e)
    {
        var ucControl = new UcReportRun();
        ShowUC(ucControl);

        ActiveControl = ucControl;
    }

    private void barBtnReportDef_ItemClick(object sender, ItemClickEventArgs e)
    {
        var ucControl = new UcReportDef();
        ShowUC(ucControl);

        ActiveControl = ucControl;
    }

    private void barBtnEDocSetting_ItemClick(object sender, ItemClickEventArgs e)
    {
        var ucControl = new UcDocReportDef();
        ShowUC(ucControl);

        ActiveControl = ucControl;
    }
}