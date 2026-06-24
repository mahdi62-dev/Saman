using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Saman.BLL.PComment;
using Saman.Properties;

namespace Saman.UC.UI;

public partial class UcPComment : XtraUserControl
{
    private readonly Comment _bllComment = new();
    public int _commentPatientID;
    public int _DepID;
    public string _NationalCode;
    public string _PersCode;
    public string _PersType;
    public Image _PicPers;
    public int _Val1;


    public UcPComment()
    {
        InitializeComponent();
    }

    public DataTable PCommentDissatisfaction
    {
        set => SetPCommentDissatisfaction(value);
    }

    private void SetPCommentDissatisfaction(DataTable dt)
    {
        chkList.DataSource = dt;
        //chkList.SelectedItem = null;
        chkList.RightToLeft = RightToLeft.Yes;
    }

    private void UcPComment_Load(object sender, EventArgs e)
    {
        picPers.Image = _PicPers;
        //LoadChkList();
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

        flpMain.ShowPopup();
    }

    private void PCommentSave(int point)
    {
        var dt = _bllComment.PCommentSave(0, 0, 3, _NationalCode, _DepID, Convert.ToInt32(_PersCode), point, 0);
        if (dt.Rows.Count > 0) _commentPatientID = Convert.ToInt32(dt.Rows[0]["CommentPatientID"]);
    }

    private void lblPersID_Paint(object sender, PaintEventArgs e)
    {
        var Name = "" + _PersCode + " " + ":کد";
        var g = e.Graphics;
        g.DrawString(Name, new Font("IRANSansFaNum", 14), Brushes.White, 0, 0,
            new StringFormat(StringFormatFlags.DirectionVertical));
    }

    private void lblPersTypeName_Paint(object sender, PaintEventArgs e)
    {
        var Name = _PersType;
        var g = e.Graphics;
        g.DrawString(Name, new Font("IRANSansFaNum", 14), Brushes.White, 0, 0,
            new StringFormat(StringFormatFlags.DirectionVertical));
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
        //_bllComment.PcommentItemDel(_commentPatientID);

        for (var i = 0; i < chkList.CheckedItems.Count; i++)
        {
            var itemVal = Convert.ToInt32(chkList.CheckedItems[i]);
            _bllComment.PCommentSave(0, _commentPatientID, 4, _NationalCode, _DepID, Convert.ToInt32(_PersCode),
                3, itemVal);
        }


        flpMain.HidePopup();
    }
}