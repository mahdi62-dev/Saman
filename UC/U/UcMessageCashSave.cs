using System;
using DevExpress.XtraEditors;

namespace Saman.UC.U;

public partial class UcMessageCashSave : XtraUserControl
{
    public UcMessageCashSave()
    {
        InitializeComponent();
    }

    private void UcEncInfo_Load(object sender, EventArgs e)
    {
    }

    private void timer1_Tick(object sender, EventArgs e)
    {
        timer1.Interval = 9000;
        timer1.Enabled = true;
        labelControl1.Visible = false;
        labelControl8.Visible = false;
    }
}