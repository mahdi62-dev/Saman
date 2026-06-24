using System.Windows.Forms;
using Saman.Form.FrmMessage;
using Saman.Form.FrmWarningMessage;

namespace Saman;

public class SUI
{
    public DialogResult FormShowMessage(string Title, string Message)
    {
        var frmMessage = new FrmMessage
        {
            Text = Title
        };
        frmMessage.lblMessage.Text = Message;
        return frmMessage.ShowDialog();
    }

    public DialogResult FormShowMessage(string Title, string Message, string strYes, string strNo)
    {
        var frmMessage = new FrmMessage
        {
            Text = Title
        };
        frmMessage.lblMessage.Text = Message;
        frmMessage.btnYes.Text = strYes;
        frmMessage.btnNo.Text = strNo;

        return frmMessage.ShowDialog();
    }


    public DialogResult FormShowDialog(string Title, string Message)
    {
        var frmMessage = new FrmMessage
        {
            Text = Title
        };
        frmMessage.lblMessage.Text = Message;
        return frmMessage.ShowDialog();
    }


    //==============================================================================================
    public DialogResult ShowWarningMessageForm(string Title, string Message, bool HasYes)
    {
        var frmWarningMessage = new FrmWarningMessage
        {
            Text = Title
        };
        frmWarningMessage.lblMessage.Text = Message;
        frmWarningMessage.btnOK.Visible = HasYes;
        return frmWarningMessage.ShowDialog();
    }
    //==============================================================================================
}