using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Saman.UC.Def;

public partial class UcServiceDef : XtraUserControl
{
    public UcServiceDef()
    {
        InitializeComponent();
    }

    private void UcServiceDef_Load(object sender, EventArgs e)
    {
    }


    private void lstServiceUc_Click(object sender, EventArgs e)
    {
    }

    private void lblServiceDefInfo_Click(object sender, EventArgs e)
    {
        panServiceUC.Controls.Clear();
        var ucControl = new UcServiceDefDetailA
            { Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom, Dock = DockStyle.Fill };
        panServiceUC.Controls.Add(ucControl);
    }
}