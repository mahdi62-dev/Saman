using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using LIB;

namespace Saman;

public partial class FrmPCommentMain : XtraForm
{
    public FrmPCommentMain()
    {
        InitializeComponent();
    }

    private void FrmPCommentMain_Load(object sender, EventArgs e)
    {
        UL.LangChange(1);
    }

    private void FrmLogin_Paint(object sender, PaintEventArgs e)
    {
        //panMain.Location = new Point(
        //    ClientSize.Width / 2 - panMain.Size.Width / 2,
        //    ClientSize.Height / 2 - panMain.Size.Height / 2);
        //panMain.Anchor = AnchorStyles.None;
    }


    private void btnEnter_Click(object sender, EventArgs e)
    {
        var nextForm = new FrmPComment1();
        Hide();
        nextForm.ShowDialog();
        Close();
    }


    private void btnPower_Click(object sender, EventArgs e)
    {
        Application.Exit();
    }

    private void panMain_Click(object sender, EventArgs e)
    {
        var nextForm = new FrmPComment1();
        Hide();
        nextForm.ShowDialog();
        Close();
    }

    private void labelControl1_Click(object sender, EventArgs e)
    {
        var nextForm = new FrmPComment1();
        Hide();
        nextForm.ShowDialog();
        Close();
    }

    private void panStart_Click(object sender, EventArgs e)
    {
        var nextForm = new FrmPComment1();
        Hide();
        nextForm.ShowDialog();
        Close();
    }
}