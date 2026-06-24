using System;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using Saman.UC;

namespace Saman.Form.Def;

public partial class FrmDefMain : XtraForm
{
    public byte _ModID = 1;

    public FrmDefMain()
    {
        InitializeComponent();
    }

    private void FrmDefMain_Load(object sender, EventArgs e)
    {
        panUC.Controls.Clear();
        //var ucControl = new UcDefult() { Anchor = (AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom), Dock = DockStyle.Fill };
        //panUC.Controls.Add(ucControl);

        var ucControl = new UcDefult
            { Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom, Dock = DockStyle.Fill };
        panUC.Controls.Add(ucControl);
    }

    private void barBtnServiceDef_ItemClick(object sender, ItemClickEventArgs e)
    {
        panUC.Controls.Clear();
        var ucControl = new UcServiceDefMain
            { Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom, Dock = DockStyle.Fill };
        panUC.Controls.Add(ucControl);
    }

    private void barBtnHISSetting_ItemClick(object sender, ItemClickEventArgs e)
    {
        panUC.Controls.Clear();
        //var ucControl = new UcDefult() { Anchor = (AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom), Dock = DockStyle.Fill };
        //panUC.Controls.Add(ucControl);

        var ucControl = new UcHISSetting
            { Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom, Dock = DockStyle.Fill };
        panUC.Controls.Add(ucControl);
    }

    private void barButtonItem10_ItemClick(object sender, ItemClickEventArgs e)
    {
    }
}