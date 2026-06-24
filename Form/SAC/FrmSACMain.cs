using System;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using Saman.APP.SAC;
using Saman.UC;
using Saman.UC.Def;
using Saman.UC.SAC;

namespace Saman.Form.SAC;

public partial class FrmSACMain : XtraForm
{
    private readonly string _FormName = "مدیریت ERP";


    private readonly SACPermission sacPermission = new();
    public int _ModID = 26;

    public FrmSACMain()
    {
        InitializeComponent();
    }

    private void FrmSACMain_Load(object sender, EventArgs e)
    {
        BarBtnCheckPermission();

        var ucControl = new UcDefult();
        ShowUC(ucControl);

        ActiveControl = ucControl;
    }

    private void BarBtnCheckPermission()
    {
        var per = sacPermission.CheckPermission(1, 1001);

        if (sacPermission.CheckPermission(_ModID, 1001)) barBtnSACResuInfo.Visibility = BarItemVisibility.Always;
    }

    private void barBtnServiceDef_ItemClick(object sender, ItemClickEventArgs e)
    {
        panUC.Controls.Clear();
        //var ucControl = new UcDefult() { Anchor = (AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom), Dock = DockStyle.Fill };
        //panUC.Controls.Add(ucControl);

        var ucControl = new UcServiceDef
            { Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom, Dock = DockStyle.Fill };
        panUC.Controls.Add(ucControl);
    }

    private void ShowUC(object ucControl)
    {
        panUC.Controls.Clear();
        var myUC = (UserControl)ucControl;
        myUC.Anchor = AnchorStyles.Top;
        myUC.Anchor = AnchorStyles.Right;
        myUC.Dock = DockStyle.Fill;

        Text += " - " + myUC.Tag;
        panUC.Controls.Add(myUC);
    }

    private void barBtnSACResuInfo_ItemClick(object sender, ItemClickEventArgs e)
    {
        //var ucControl = new UcSACResuInfo() { _ModID = _ModID };
        //ShowUC(ucControl);
        var ucControl = new UcSACUserPermission(); //{ _ModID = _ModID };
        ShowUC(ucControl);
    }

    private void barBtnSACReport_ItemClick(object sender, ItemClickEventArgs e)
    {
        var ucControl = new UcSACReport { _ModID = _ModID };
        ShowUC(ucControl);
    }
}