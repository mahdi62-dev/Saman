using System;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using Saman.APP.SAC;
using Saman.UC;
using Saman.UC.Personnel;

namespace Saman.Form.DefErp;

public partial class FrmDefErpMain : XtraForm
{
    private readonly string _FormName = "پرسنلی";
    //public byte _ModID = 1;

    private readonly SACPermission sacPermission = new();

    public FrmDefErpMain()
    {
        InitializeComponent();
    }

    private void FrmDefErpMain_Load(object sender, EventArgs e)
    {
        panUC.Controls.Clear();
        //var ucControl = new UcDefult() { Anchor = (AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom), Dock = DockStyle.Fill };
        //panUC.Controls.Add(ucControl);

        var ucControl = new UcDefult
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

        Text = _FormName + " - " + myUC.Tag;
        panUC.Controls.Add(myUC);
    }

    private void barBtnPersNew_ItemClick(object sender, ItemClickEventArgs e)
    {
        var ucControl = new UcPersonNew();
        ShowUC(ucControl);
    }

    private void barBtnERPSetting_ItemClick(object sender, ItemClickEventArgs e)
    {
    }
}