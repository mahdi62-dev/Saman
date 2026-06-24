using System;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using Saman.APP.SAC;
using Saman.UC;
using Saman.UC.sch;

namespace Saman.Form.FrmSch;

public partial class FrmSch : XtraForm
{
    private readonly string _FormName = "سیستم وقت دهی";
    private readonly SACPermission sacPermission = new();
    public byte _ModID = 29;

    public FrmSch()
    {
        InitializeComponent();
    }

    private void FrmSch_Load(object sender, EventArgs e)
    {
        barBtnStatus();
        CheckPermission();
        var ucDefult = new UcDefult
            { Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom, Dock = DockStyle.Fill };
        panUC.Controls.Add(ucDefult);
    }

    private void CheckPermission()
    {
        barBtnSchItem.Enabled = sacPermission.CheckPermission(_ModID, 290001);
        barBtnSchDef.Enabled = sacPermission.CheckPermission(_ModID, 290002);
        barBtnSchList.Enabled = sacPermission.CheckPermission(_ModID, 290003);
        barBtnSchReport.Enabled = sacPermission.CheckPermission(_ModID, 290004);
    }

    private void barBtnStatus()
    {
        barBtnSchItem.Enabled = false;
        barBtnSchDef.Enabled = false;
        barBtnSchList.Enabled = false;
        barBtnSchReport.Enabled = false;
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

    private void barBtnSchDef_ItemClick(object sender, ItemClickEventArgs e)
    {
        //if (!CheckValidation())
        //    return;

        var ucControl = new UcSchDef { _ModID = _ModID };
        ShowUC(ucControl);
    }

    private void barBtnSchItem_ItemClick(object sender, ItemClickEventArgs e)
    {
        //if (!CheckValidation())
        //    return;

        var ucControl = new UcSch { _ModID = _ModID };
        ShowUC(ucControl);
    }
}