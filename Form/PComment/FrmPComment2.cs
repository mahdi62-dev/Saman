using System;
using System.Data;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using LIB;
using Saman.BLL.PComment;

namespace Saman;

public partial class FrmPComment2 : XtraForm
{
    private readonly Comment _bllComment = new();
    private DataTable _dtPage;
    public string _FullName, _NationalCode;
    private int _Timer;

    public FrmPComment2()
    {
        InitializeComponent();
    }

    [DllImport("winmm.dll")]
    private static extern int mciSendString(string MciComando, string MciRetorno, int MciRetornoLeng, int CallBack);

    private void FrmPComment2_Load(object sender, EventArgs e)
    {
        UL.LangChange(1);
        VoiceLoad();
        lblPatientName.Text = "مراجعه کننده محترم " + _FullName + "";
        _dtPage = _bllComment.PageSearch(_NationalCode);

        // _AdmCount = Convert.ToInt32(_dtEncSearch.Rows[0]["AdmCount"]);
        var logoPath = Application.StartupPath;
        logoPath = logoPath + @"\files\logo.png";
        picLogo.Image = Image.FromFile(logoPath);

        _Timer = 60;
        lblTimer.Text = _Timer.ToString();

        Control[] ctrl;
        string uiCaption;
        int recNo = 0, depId = 0, modId = 0;
        FlowLayoutPanel flp = null;
        flp = flpMain;

        for (var i = 0; i < _dtPage.Rows.Count; i++)
        {
            recNo = Convert.ToInt32(_dtPage.Rows[i]["RecNo"]);
            uiCaption = _dtPage.Rows[i]["DepName"].ToString();
            depId = Convert.ToInt32(_dtPage.Rows[i]["DepID"]);
            modId = Convert.ToInt32(_dtPage.Rows[i]["ModID"]);

            ctrl = flp.Controls.Find("panDep" + recNo, false);

            if (ctrl.Length == 0) continue;

            ctrl[0].Enabled = true;
            ctrl[0].Visible = true;
            ctrl[0].Width = flpMain.Width / 3 - 10;

            var btnCaption = ctrl[0].Controls.Find("btnDep" + recNo, false);
            btnCaption[0].Text = uiCaption;
            btnCaption[0].Tag = depId;

            var btn = btnCaption[0] as SimpleButton;
            var btnIcon = Application.StartupPath;

            switch (modId)
            {
                case 1:
                    btnIcon = btnIcon + @"\files\ADM.png";
                    btn.ImageOptions.Image = Image.FromFile(btnIcon);
                    break;
                case 2:
                    btnIcon = btnIcon + @"\files\Cash.png";
                    btn.ImageOptions.Image = Image.FromFile(btnIcon);
                    break;
                case 5:
                    btnIcon = btnIcon + @"\files\Drug.png";
                    btn.ImageOptions.Image = Image.FromFile(btnIcon);
                    break;
                case 9:
                    btnIcon = btnIcon + @"\files\Clinic.png";
                    btn.ImageOptions.Image = Image.FromFile(btnIcon);
                    break;
                case 10:
                    btnIcon = btnIcon + @"\files\Lab.png";
                    btn.ImageOptions.Image = Image.FromFile(btnIcon);
                    break;
                case 13:
                    btnIcon = btnIcon + @"\files\Imaging.png";
                    btn.ImageOptions.Image = Image.FromFile(btnIcon);
                    break;
                case 20:
                    btnIcon = btnIcon + @"\files\Emergency.png";
                    btn.ImageOptions.Image = Image.FromFile(btnIcon);
                    break;
                case 22:
                    btnIcon = btnIcon + @"\files\Guard.png";
                    btn.ImageOptions.Image = Image.FromFile(btnIcon);
                    break;
            }
        }
    }

    private void VoiceLoad()
    {
        var musica = Application.StartupPath;
        musica = musica + @"\files\2.wav";
        mciSendString("play " + musica, null, 0, 0);
    }

    private void FrmLogin_Paint(object sender, PaintEventArgs e)
    {
        panMain.Location = new Point(
            ClientSize.Width / 2 - panMain.Size.Width / 2,
            ClientSize.Height / 2 - panMain.Size.Height / 2);
        panMain.Anchor = AnchorStyles.None;
    }

    private void SelectDep(int DepID, string DepName)
    {
        timControl.Enabled = false;

        var nextForm = new FrmPComment3();
        nextForm._DepID = DepID;
        nextForm._DepName = DepName;
        nextForm._FullName = _FullName;
        nextForm._NationalCode = _NationalCode;

        Hide();
        nextForm.ShowDialog();
        Close();
    }

    private void btnDep1_Click(object sender, EventArgs e)
    {
        var DepName = btnDep1.Text;
        var depID = Convert.ToInt32(((SimpleButton)sender).Tag);
        SelectDep(depID, DepName);
    }

    private void btnDep2_Click(object sender, EventArgs e)
    {
        var DepName = btnDep2.Text;
        var depID = Convert.ToInt32(((SimpleButton)sender).Tag);
        SelectDep(depID, DepName);
    }

    private void btnDep3_Click(object sender, EventArgs e)
    {
        var DepName = btnDep3.Text;
        var depID = Convert.ToInt32(((SimpleButton)sender).Tag);
        SelectDep(depID, DepName);
    }

    private void btnDep4_Click(object sender, EventArgs e)
    {
        var DepName = btnDep4.Text;
        var depID = Convert.ToInt32(((SimpleButton)sender).Tag);
        SelectDep(depID, DepName);
    }

    private void btnDep5_Click(object sender, EventArgs e)
    {
        var DepName = btnDep5.Text;
        var depID = Convert.ToInt32(((SimpleButton)sender).Tag);
        SelectDep(depID, DepName);
    }

    private void btnDep6_Click(object sender, EventArgs e)
    {
        var DepName = btnDep6.Text;
        var depID = Convert.ToInt32(((SimpleButton)sender).Tag);
        SelectDep(depID, DepName);
    }

    private void btnDep7_Click(object sender, EventArgs e)
    {
        var DepName = btnDep7.Text;
        var depID = Convert.ToInt32(((SimpleButton)sender).Tag);
        SelectDep(depID, DepName);
    }

    private void btnDep8_Click(object sender, EventArgs e)
    {
        var DepName = btnDep8.Text;
        var depID = Convert.ToInt32(((SimpleButton)sender).Tag);
        SelectDep(depID, DepName);
    }

    private void btnDep9_Click(object sender, EventArgs e)
    {
        var DepName = btnDep9.Text;
        var depID = Convert.ToInt32(((SimpleButton)sender).Tag);
        SelectDep(depID, DepName);
    }

    private void btnDep10_Click(object sender, EventArgs e)
    {
        var DepName = btnDep10.Text;
        var depID = Convert.ToInt32(((SimpleButton)sender).Tag);
        SelectDep(depID, DepName);
    }

    private void btnDep11_Click(object sender, EventArgs e)
    {
        var DepName = btnDep11.Text;
        var depID = Convert.ToInt32(((SimpleButton)sender).Tag);
        SelectDep(depID, DepName);
    }

    private void btnDep12_Click(object sender, EventArgs e)
    {
        var DepName = btnDep12.Text;
        var depID = Convert.ToInt32(((SimpleButton)sender).Tag);
        SelectDep(depID, DepName);
    }

    private void btnDep13_Click(object sender, EventArgs e)
    {
        var DepName = btnDep13.Text;
        var depID = Convert.ToInt32(((SimpleButton)sender).Tag);
        SelectDep(depID, DepName);
    }

    private void btnDep14_Click(object sender, EventArgs e)
    {
        var DepName = btnDep14.Text;
        var depID = Convert.ToInt32(((SimpleButton)sender).Tag);
        SelectDep(depID, DepName);
    }

    private void btnDep15_Click(object sender, EventArgs e)
    {
        var DepName = btnDep15.Text;
        var depID = Convert.ToInt32(((SimpleButton)sender).Tag);
        SelectDep(depID, DepName);
    }

    private void timControl_Tick(object sender, EventArgs e)
    {
        if (_Timer > 0) _Timer -= 1;

        if (_Timer == 0 && timControl.Enabled)
        {
            timControl.Enabled = false;

            var nextForm = new FrmPComment1();
            Hide();
            nextForm.ShowDialog();
            Close();
            return;
        }

        lblTimer.Text = _Timer.ToString();
    }
}