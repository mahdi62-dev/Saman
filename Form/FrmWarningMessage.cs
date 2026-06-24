using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Saman.Form.FrmWarningMessage;

public partial class FrmWarningMessage : XtraForm
{
    //public byte _ModID = 1;
    public FrmWarningMessage()
    {
        InitializeComponent();
    }

    private void FrmWarningMessage_Load(object sender, EventArgs e)
    {
    }

    private void btnOK_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.OK;
        Close();
    }


    private void FrmWarningMessage_KeyPress(object sender, KeyPressEventArgs e)
    {
        //if (e.KeyChar == Keys.Escape )
        //{
        //    yourTextBox.Text = string.Empty;
        //}
    }
}