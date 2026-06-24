using System;
using System.IO;
using System.Runtime.InteropServices;
using DevExpress.XtraEditors;
using LIB;
using Saman.BLL.PComment;

namespace Saman.Form.PComment;

public partial class FrmPCommentVoiceRecord : XtraForm
{
    private readonly Comment _bllComment = new();
    public int _DepID;
    public string _NationalCode;
    private int _Timer;
    private int hours;
    private int minutes;

    private string musica = "";
    private int seconds;
    private DateTime StartTime;

    public FrmPCommentVoiceRecord()
    {
        InitializeComponent();
    }

    [DllImport("winmm.dll")]
    private static extern int mciSendString(string MciComando, string MciRetorno, int MciRetornoLeng, int CallBack);

    private void FrmPCommentVoiceRecord_Load(object sender, EventArgs e)
    {
        UL.LangChange(2);

        _Timer = 180;
        StartTime = DateTime.Now;
        timControl.Start();
        mciSendString("open new type waveaudio alias Som", null, 0, 0);
        mciSendString("record Som", null, 0, 0);
    }


    private void timControl_Tick(object sender, EventArgs e)
    {
        //if (timControl.Enabled == true)
        //{
        //    _Timer -= 1;
        //    picRecording.Visible = !picRecording.Visible;
        //}

        //if (_Timer == 0 && timControl.Enabled == true)
        //{
        //    timControl.Enabled = false;

        //    FrmPComment1 nextForm = new FrmPComment1();
        //    this.Hide();
        //    nextForm.ShowDialog();
        //    this.Close();
        //    return;
        //}

        //TimeSpan dt = DateTime.Now - StartTime;
        //lblTimer.Text = dt.Hours + ":" + dt.Minutes + ":" + dt.Seconds;

        // lblTimer.Text = _Timer.ToString() + " ثانیه";


        if (seconds == 60)
        {
            seconds = 0;
            if (minutes == 60)
            {
                minutes = 0;
                hours++;
            }
            else
            {
                minutes++;
            }
        }
        else
        {
            seconds++;
        }
        //lblTimer.Text = (hours > 9 ? hours + "" : "0" + hours) + ":"
        //                                                       + (minutes > 9 ? minutes + "" : "0" + minutes) + ":"
        //                                                       + (seconds > 9 ? seconds + "" : "0" + seconds);

        lblTimer.Text = (minutes > 9 ? minutes + "" : "0" + minutes) + ":"
                                                                     + (seconds > 9 ? seconds + "" : "0" + seconds);
    }

    private void btnSaveStop_Click(object sender, EventArgs e)
    {
        var tempFolder = Path.GetTempPath();
        var uniqueFileName = $@"{Guid.NewGuid()}.mp3";

        var audioFile = tempFolder + uniqueFileName;


        mciSendString("save Som " + audioFile, null, 0, 0);
        mciSendString("close Som", null, 0, 0);

        var finfo = new FileInfo(audioFile);
        var fstream = new FileStream(audioFile, FileMode.Open, FileAccess.Read);
        var br = new BinaryReader(fstream);

        var VoiceData = br.ReadBytes(Convert.ToInt32(finfo.Length));

        _bllComment.PCommentSaveVoice(0, 5, _NationalCode, _DepID, VoiceData);

        br.Close();
        fstream.Close();
        fstream.Dispose();
        File.Delete(audioFile);


        Close();
        //Play
        //if (musica == "")
        // {
        // OpenFileDialog open = new OpenFileDialog();
        // open.Filter = "Wave|*.wav";
        // if (open.ShowDialog() == DialogResult.OK) { musica = open.FileName; }
        // }
        // mciSendString("play " + musica, null, 0, 0);
    }
}