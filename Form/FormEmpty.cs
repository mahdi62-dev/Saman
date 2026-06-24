using System;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using Saman.UC;

namespace Saman.Form.FormEmpty;

public partial class FormEmpty : XtraForm
{
    public byte _ModID = 1;

    public FormEmpty()
    {
        InitializeComponent();
    }

    private void FormEmpty_Load(object sender, EventArgs e)
    {
        panUC.Controls.Clear();

        //var ucEncCur = new UcEncCur() { Anchor = (AnchorStyles.Top | AnchorStyles.Right), Dock = DockStyle.Fill };
        //panUC.Controls.Add(ucEncCur);
        var ucEncCur = new UcEncCur
            { Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom, Dock = DockStyle.Fill };
        ucEncCur._ModID = _ModID;
        panUC.Controls.Add(ucEncCur);
        ActiveControl = ucEncCur;

        // var panUc = (.Parent.f) as DevExpress.XtraEditors.PanelControl;
    }

    private void barBtnEncNew_ItemClick(object sender, ItemClickEventArgs e)
    {
    }


    private void barButtonItem4_ItemClick(object sender, ItemClickEventArgs e)
    {
    }

    private void barRegService_ItemClick(object sender, ItemClickEventArgs e)
    {
    }

    private void barBtnEncSearch_ItemClick(object sender, ItemClickEventArgs e)
    {
    }

    private void barButtonItem9_ItemClick(object sender, ItemClickEventArgs e)
    {
    }
}