using System;
using System.Data;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using LIB;
using Saman.BLL.PComment;

namespace Saman;

public partial class FrmPComment1 : XtraForm
{
    private readonly Comment _bllComment = new();
    private int _AdmCount;
    private DataTable _dtEncSearch;
    private string _FullName, _NationalCode;

    private int _Timer;

    //string musica = "";

    public FrmPComment1()
    {
        InitializeComponent();
    }


    [DllImport("winmm.dll")]
    private static extern int mciSendString(string MciComando, string MciRetorno, int MciRetornoLeng, int CallBack);

    private void FrmPComment1_Load(object sender, EventArgs e)
    {
        var logoPath = Application.StartupPath;
        logoPath = logoPath + @"\files\logo.png";

        picLogo.Image = Image.FromFile(logoPath);

        _Timer = 60;
        lblTimer.Text = _Timer.ToString();

        VoiceLoad();
        UL.LangChange(1);
        _FullName = "";
        _NationalCode = "";

        //btnContinue_Click(null, null);
        //lblNationalCode.Text = "2031906755";
    }

    private void VoiceLoad()
    {
        var musica = Application.StartupPath;
        musica = musica + @"\files\1.wav";
        mciSendString("play " + musica, null, 0, 0);
    }

    private void btn1_Click(object sender, EventArgs e)
    {
        _NationalCode += 1;
        NationalCodeSet();
    }

    private void NationalCodeSet()
    {
        lblNationalCode.Text = _NationalCode;
    }

    private void btn2_Click(object sender, EventArgs e)
    {
        _NationalCode += 2;
        NationalCodeSet();
    }

    private void btn3_Click(object sender, EventArgs e)
    {
        _NationalCode += 3;
        NationalCodeSet();
    }

    private void btn4_Click(object sender, EventArgs e)
    {
        _NationalCode += 4;
        NationalCodeSet();
    }

    private void btn5_Click(object sender, EventArgs e)
    {
        _NationalCode += 5;
        NationalCodeSet();
    }

    private void btn6_Click(object sender, EventArgs e)
    {
        _NationalCode += 6;
        NationalCodeSet();
    }

    private void btn7_Click(object sender, EventArgs e)
    {
        _NationalCode += 7;
        NationalCodeSet();
    }

    private void btn8_Click(object sender, EventArgs e)
    {
        _NationalCode += 8;
        NationalCodeSet();
    }

    private void btn9_Click(object sender, EventArgs e)
    {
        _NationalCode += 9;
        NationalCodeSet();
    }

    private void btn0_Click(object sender, EventArgs e)
    {
        _NationalCode += 0;
        NationalCodeSet();
    }

    private void btnDel_Click(object sender, EventArgs e)
    {
        FormNull();
    }

    private void FormNull()
    {
        lblError.Text = "";
        _NationalCode = null;
        lblNationalCode.Text = "کد ملی خود را وارد نمایید";

        _Timer = 30;
    }

    private void btnBackspace_Click(object sender, EventArgs e)
    {
        lblError.Text = "";
        var len = 0;

        if (_NationalCode != "" && _NationalCode != null)
        {
            len = _NationalCode.Length;


            if (len == 0)
            {
                lblNationalCode.Text = "کد ملی خود را وارد نمایید";
            }
            else
            {
                _NationalCode = _NationalCode.Substring(0, len - 1);
                NationalCodeSet();
            }
        }
    }

    private void btnContinue_Click(object sender, EventArgs e)
    {
        if (!CheckValidation())
        {
            lblError.Text = "کد ملی صحیح نمی باشد";
            return;
        }

        _NationalCode = lblNationalCode.Text;
        Search(_NationalCode);


        if (_AdmCount > 0)
        {
            timControl.Enabled = false;

            var nextForm = new FrmPComment2();
            nextForm._FullName = _FullName;
            nextForm._NationalCode = _NationalCode;

            Hide();
            nextForm.ShowDialog();
            Close();
        }

        else
        {
            lblError.Text = "مراجعه ای در روزهای اخیر یافت نشد";
        }
    }

    private bool CheckValidation()
    {
        var isValid = true;

        if (lblNationalCode.Text.Length > 0 &&
            !App.UI.NationalCodeCValidate(lblNationalCode.Text, out var errorText))
        {
            // e.Cancel = true;
            lblNationalCode.ToolTip = errorText;
            isValid = false;
        }

        return isValid;
    }

    private void lblNationalCode_TextChanged(object sender, EventArgs e)
    {
        lblError.Text = "";
    }

    private void timControl_Tick(object sender, EventArgs e)
    {
        if (_Timer > 0) _Timer -= 1;

        if (_Timer == 0 && timControl.Enabled) FormNull();

        lblTimer.Text = _Timer.ToString();
    }

    private void Search(string NationalCode)
    {
        _dtEncSearch = _bllComment.EncSearch(_NationalCode);
        _AdmCount = Convert.ToInt32(_dtEncSearch.Rows[0]["AdmCount"]);
        _FullName = _dtEncSearch.Rows[0]["FullName"].ToString();
    }
}