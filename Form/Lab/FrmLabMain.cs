using System;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using Saman.UC;
using Saman.UC.Enc;
using Saman.UC.ServiceReg;

namespace Saman.Form.Lab;

public partial class FrmLabMain : XtraForm
{
    public FrmLabMain()
    {
        InitializeComponent();
    }

    private void FrmEncMain_Load(object sender, EventArgs e)
    {
        panUC.Controls.Clear();

        //var ucEncCur = new UcEncCur() { Anchor = (AnchorStyles.Top | AnchorStyles.Right), Dock = DockStyle.Fill };
        //panUC.Controls.Add(ucEncCur);
        var ucEncCur = new UcEncCur
            { Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom, Dock = DockStyle.Fill };
        panUC.Controls.Add(ucEncCur);

        // var panUc = (.Parent.f) as DevExpress.XtraEditors.PanelControl;
    }

    private void barBtnEncNew_ItemClick(object sender, ItemClickEventArgs e)
    {
        panUC.Controls.Clear();

        var ucEncNew = new UcEncSave { Anchor = AnchorStyles.Top | AnchorStyles.Right, Dock = DockStyle.Fill };
        panUC.Controls.Add(ucEncNew);
    }


    private void barButtonItem4_ItemClick(object sender, ItemClickEventArgs e)
    {
    }

    private void barRegService_ItemClick(object sender, ItemClickEventArgs e)
    {
        panUC.Controls.Clear();

        var UcServiceReg = new UcServiceReg
            { Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom, Dock = DockStyle.Fill };
        panUC.Controls.Add(UcServiceReg);
    }
}