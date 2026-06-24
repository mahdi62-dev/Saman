using System;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using Saman.APP.SAC;
using Saman.UC;

namespace Saman.Form.Percase;

public partial class FrmPercase : XtraForm
{
    private readonly string _FormName = "سیستم کارانه";

    private readonly SACPermission sacPermission = new();
    public byte _ModID = 102;

    public FrmPercase()
    {
        InitializeComponent();
    }

    private void FrmPercase_Load(object sender, EventArgs e)
    {
        barBtnStatus();
        CheckPermission();

        panUC.Controls.Clear();

        var ucDefult = new UcDefult
            { Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom, Dock = DockStyle.Fill };
        panUC.Controls.Add(ucDefult);
        ActiveControl = ucDefult;
    }

    private void CheckPermission()
    {
        barBtnDrPercase.Enabled = sacPermission.CheckPermission(_ModID, 1020001);
        barBtnADrPercase.Enabled = sacPermission.CheckPermission(_ModID, 1020002);
        barBtnPersPercase.Enabled = sacPermission.CheckPermission(_ModID, 1020003);
    }

    private void barBtnStatus()
    {
        barBtnDrPercase.Enabled = false;
        barBtnADrPercase.Enabled = false;
        barBtnPersPercase.Enabled = false;
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

    private void barBtnDrPercase_ItemClick(object sender, ItemClickEventArgs e)
    {
        var ucControl = new UcDrPercase { _ModID = _ModID };
        ShowUC(ucControl);
    }

    private void barBtnADrPercase_ItemClick(object sender, ItemClickEventArgs e)
    {
        var ucControl = new UcADrPercase { _ModID = _ModID };
        ShowUC(ucControl);
    }

    private void barBtnPersPercase_ItemClick(object sender, ItemClickEventArgs e)
    {
        var ucControl = new UcPersonPercase { _ModID = _ModID };
        ShowUC(ucControl);
    }
}