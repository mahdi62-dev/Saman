using System;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using Saman.APP.SAC;
using Saman.UC;
using Saman.UC.Personnel;

namespace Saman.Form.Personnel;

public partial class FrmPersonnel : XtraForm
{
    private readonly string _FormName = "پرسنلی";

    private readonly SACPermission sacPermission = new();
    public byte _ModID = 100;

    public FrmPersonnel()
    {
        InitializeComponent();
    }

    private void FrmPersonnel_Load(object sender, EventArgs e)
    {
        barBtnStatus();
        CheckPermission();

        if (barBtnPers.Enabled)
        {
            var ucControl = new UcPersonnel { _ModID = _ModID };
            ShowUC(ucControl);
        }
        else
        {
            var ucControl = new UcDefult();
            ShowUC(ucControl);
        }
    }

    private void CheckPermission()
    {
        barBtnPers.Enabled = sacPermission.CheckPermission(_ModID, 1000001);
        barEmpAction.Enabled = sacPermission.CheckPermission(_ModID, 1000002);
    }

    private void barBtnStatus()
    {
        barBtnPers.Enabled = false;
        barEmpAction.Enabled = false;
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

    private void barBtnEncNew_ItemClick(object sender, ItemClickEventArgs e)
    {
        var ucControl = new UcPersonnel { _ModID = _ModID };
        ShowUC(ucControl);
    }

    private void barEmpAction_ItemClick(object sender, ItemClickEventArgs e)
    {
        var ucControl = new UcEmpAction { _ModID = _ModID };
        ShowUC(ucControl);
    }

    private void barBtnPersNew_ItemClick(object sender, ItemClickEventArgs e)
    {
        var ucControl = new UcPersonNew();
        ShowUC(ucControl);
    }
}