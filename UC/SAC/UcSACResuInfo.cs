using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Saman.UC.Def;

namespace Saman.UC.SAC;

public partial class UcSACResuInfo : XtraUserControl
{
    public UcSACResuInfo()
    {
        InitializeComponent();
    }

    public int _ModID { get; set; }

    private void UcServiceDefMain_Load(object sender, EventArgs e)
    {
        //var bllServiceDef = new BLL.Def.ServiceDef.ServiceDef();
        //grdResu.DataSource = bllServiceDef.ServiceLoad(0);
    }

    private void btnSelect_Click(object sender, EventArgs e)
    {
        var rowGrid = grvResu.GetDataRow(grvResu.FocusedRowHandle);

        if (rowGrid == null) return;

        var ucControl = new UcServiceDef
            { Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom, Dock = DockStyle.Fill };
        panDetail.Controls.Add(ucControl);
        panDetail.Dock = DockStyle.Fill;

        //var ucEncCur = new UcEncCur
        //{
        //    Anchor = (AnchorStyles.Top | AnchorStyles.Right),
        //    Dock = DockStyle.Fill,
        //    _EncID = Convert.ToInt32(_rowEncSearch["EncID"]),

        //};


        //panUc.Controls.Clear();
        //panUc.Controls.Add(ucEncCur);
    }
}