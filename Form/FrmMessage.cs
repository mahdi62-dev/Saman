using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Saman.Form.FrmMessage;

public partial class FrmMessage : XtraForm
{
    //public byte _ModID = 1;
    public FrmMessage()
    {
        InitializeComponent();
    }

    private void FrmMessage_Load(object sender, EventArgs e)
    {
    }

    private void btnYes_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.Yes;
        Close();
    }

    private void btnNo_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.No;
        Close();
    }

    private void FrmMessage_KeyPress(object sender, KeyPressEventArgs e)
    {
        //if (e.KeyChar == Keys.Escape )
        //{
        //    yourTextBox.Text = string.Empty;
        //}
    }
}