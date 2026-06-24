using System;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using Saman.APP.SAC;
using Saman.UC;
using Saman.UC.Acc.Bond;

namespace Saman.Form.Acc.Bond;

public partial class FrmBond : XtraForm
{
    private readonly string _FormName = "مدیریت ضمانت نامه";

    private readonly SACPermission sacPermission = new();
    public byte _ModID = 106;

    public FrmBond()
    {
        InitializeComponent();
    }

    public int _Year { get; set; }
    public int _Mon { get; set; }

    private void FrmBond_Load(object sender, EventArgs e)
    {
        barBtnStatus();
        CheckPermission();

        var ucDefult = new UcDefult
            { Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom, Dock = DockStyle.Fill };
        panUC.Controls.Add(ucDefult);
    }

    private void CheckPermission()
    {
        barbtnAccBond.Enabled = sacPermission.CheckPermission(_ModID, 106001);
        barBtnAccSide.Enabled = sacPermission.CheckPermission(_ModID, 106002);
        barBtnAccBranch.Enabled = sacPermission.CheckPermission(_ModID, 106003);
        barBtnBondList.Enabled = sacPermission.CheckPermission(_ModID, 106004);
        barBtnReport.Enabled = sacPermission.CheckPermission(_ModID, 106005);
    }

    private void barBtnStatus()
    {
        barbtnAccBond.Enabled = false;
        barBtnAccSide.Enabled = false;
        barBtnAccBranch.Enabled = false;
        barBtnBondList.Enabled = false;
        barBtnReport.Enabled = false;
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

    private void barbtnAccBond_ItemClick(object sender, ItemClickEventArgs e)
    {
        var ucControl = new UcBond { _ModID = _ModID };
        ShowUC(ucControl);
    }

    private void barBtnAccSide_ItemClick(object sender, ItemClickEventArgs e)
    {
        var ucControl = new UcAccSide { _ModID = _ModID };
        ShowUC(ucControl);
    }

    private void barBtnBondList_ItemClick(object sender, ItemClickEventArgs e)
    {
        var ucControl = new UcBondSearch { _ModID = _ModID };
        ShowUC(ucControl);
    }
}