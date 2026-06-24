using System;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using Saman.APP.SAC;
using Saman.UC;

namespace Saman.Form.FrmInsuranceExport;

public partial class FrmInsuranceExport : XtraForm
{
    private readonly string _FormName = "دیسکت بیمه";

    private readonly SACPermission sacPermission = new();
    public byte _ModID = 97;

    public FrmInsuranceExport()
    {
        InitializeComponent();
    }

    private void FrmInsuranceExport_Load(object sender, EventArgs e)
    {
        barBtnStatus();
        //   CheckPermission();

        if (barBtnInsurExport.Enabled)
        {
            var ucControl = new UcInsurExport();
            ShowUC(ucControl);

            ActiveControl = ucControl;
        }
        else
        {
            var ucControl = new UcDefult();
            ShowUC(ucControl);

            ActiveControl = ucControl;
        }
    }

    private void CheckPermission()
    {
        barBtnInsurExport.Enabled = sacPermission.CheckPermission(_ModID, 970001);
        barBtnInsurExportSetting.Enabled = sacPermission.CheckPermission(_ModID, 970002);
    }

    private void barBtnStatus()
    {
        barBtnInsurExport.Enabled = true;
        barBtnInsurExportSetting.Enabled = true;
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


    private void barBtnInsurExport_ItemClick(object sender, ItemClickEventArgs e)
    {
        var ucControl = new UcInsurExport();
        ShowUC(ucControl);

        ActiveControl = ucControl;
    }
}