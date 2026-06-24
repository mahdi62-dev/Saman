using System;
using DevExpress.XtraEditors;
using Saman.APP;

namespace Saman.Form.Attach;

public partial class FrmAttDisplay : XtraForm
{
    private readonly ImageLib _ImageLib = new();
    public byte[] _AttFile = null;
    public string _FileName = null;

    public FrmAttDisplay()
    {
        InitializeComponent();
    }

    private void FrmAttDisplay_Load(object sender, EventArgs e)
    {
        picAtt.Image = _ImageLib.ByteArrayToImage(_AttFile);
        Text = _FileName;
    }

    private void FrmAttDisplay_Deactivate(object sender, EventArgs e)
    {
        Close();
    }
}