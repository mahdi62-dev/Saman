using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using DAL;
using DevExpress.XtraEditors;
using LIB;
using Saman.BLL.PComment;
using Saman.Form.PComment;
using Saman.Properties;
using Saman.UC.UI;

namespace Saman;

public partial class FrmPComment3 : XtraForm
{
    private readonly Comment _bllComment = new();
    private readonly int _DefaultTimer = 60;
    public int _DepID;
    private DataTable _dtPage;
    public string _FullName, _NationalCode, _DepName;
    private int _Timer;
    public int _Val1;


    public FrmPComment3()
    {
        InitializeComponent();
    }


    [DllImport("winmm.dll")]
    private static extern int mciSendString(string MciComando, string MciRetorno, int MciRetornoLeng, int CallBack);

    private void FrmPComment3_Load(object sender, EventArgs e)
    {
        UL.LangChange(1);
        VoiceLoad();
        lblTitle.Text = "\r\n" + "لطفا به کادر بخش " + _DepName + " امتیاز دهید";
        lblTitleDep.Text = "لطفا به بخش " + _DepName + "\r\n" + " امتیاز دهید";

        _dtPage = _bllComment.PersSearch(_NationalCode, _DepID);

        _Timer = _DefaultTimer;
        lblTimer.Text = _Timer.ToString();

        var dalList = new List();
        var dtPCommentDissatisfaction = dalList.LoadListToDataTable(
            cSet.cnStringA,
            "ID,Name",
            "vADR",
            "ADName='PCommentDissatisfaction' AND ID>0",
            null);

        Control[] ctrl;
        Image PicPers = null;
        int recNo = 0, persId = 0;
        string PersType;
        FlowLayoutPanel flp = null;
        flp = flpMain;
        for (var i = 0; i < _dtPage.Rows.Count; i++)
        {
            //recNo = Convert.ToInt32(_dtPage.Rows[i]["RecNo"]);
            persId = Convert.ToInt32(_dtPage.Rows[i]["PersID"]);
            PersType = _dtPage.Rows[i]["PersTypeName"].ToString();
            if (!string.IsNullOrEmpty(_dtPage.Rows[i]["Photo"].ToString()))
                PicPers = byteArrayToImage((byte[])_dtPage.Rows[i]["Photo"]);

            flp.Controls.Add(new UcPComment
            {
                _PersCode = persId.ToString(),
                _PicPers = PicPers,
                _PersType = PersType,
                _NationalCode = _NationalCode,
                _DepID = _DepID,
                PCommentDissatisfaction = dtPCommentDissatisfaction
            });
        }
    }

    private void VoiceLoad()
    {
        var musica = Application.StartupPath;
        musica = musica + @"\files\3.wav";
        mciSendString("play " + musica, null, 0, 0);
    }

    public Image byteArrayToImage(byte[] byteArrayIn)
    {
        var ms = new MemoryStream(byteArrayIn);
        var returnImage = Image.FromStream(ms);
        return returnImage;
    }

    private void FrmLogin_Paint(object sender, PaintEventArgs e)
    {
        panMain.Location = new Point(
            ClientSize.Width / 2 - panMain.Size.Width / 2,
            ClientSize.Height / 2 - panMain.Size.Height / 2);
        panMain.Anchor = AnchorStyles.None;
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

    private void btnRecord_Click(object sender, EventArgs e)
    {
        timControl.Stop();

        var nextForm = new FrmPCommentVoiceRecord();
        nextForm._DepID = _DepID;
        nextForm._NationalCode = _NationalCode;

        nextForm.ShowDialog();
    }

    private void FrmPComment3_Activated(object sender, EventArgs e)
    {
        timControl.Start();
        _Timer = _DefaultTimer;
    }

    private void btn1_Click(object sender, EventArgs e)
    {
        btn1.BackgroundImage = Resources._21;
        _Val1 = 1;
        btn2.BackgroundImage = Resources._33;
        btn3.BackgroundImage = Resources._55;
        PCommentSave(1);
    }

    private void btn2_Click(object sender, EventArgs e)
    {
        btn2.BackgroundImage = Resources._31;
        _Val1 = 2;
        btn1.BackgroundImage = Resources._22;
        btn3.BackgroundImage = Resources._55;
        PCommentSave(2);
    }

    private void btn3_Click(object sender, EventArgs e)
    {
        btn3.BackgroundImage = Resources._5;
        _Val1 = 3;
        btn1.BackgroundImage = Resources._22;
        btn2.BackgroundImage = Resources._33;
        PCommentSave(3);
    }

    private void PCommentSave(int point)
    {
        _bllComment.PCommentSave(0, 0, 2, _NationalCode, _DepID, 0, point, 0);
    }

    private void lblBack_Click(object sender, EventArgs e)
    {
        timControl.Enabled = false;
        var nextForm = new FrmPComment2();
        nextForm._FullName = _FullName;
        nextForm._NationalCode = _NationalCode;
        Hide();
        nextForm.ShowDialog();
        Close();
    }

    private void lblEnd_Click(object sender, EventArgs e)
    {
        timControl.Enabled = false;
        var nextForm = new FrmPComment1();
        Hide();
        nextForm.ShowDialog();
        Close();
    }
}