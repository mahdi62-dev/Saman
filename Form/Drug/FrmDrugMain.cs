using System;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using Saman.UC;
using Saman.UC.Drug;
using Saman.UC.Enc;
using Saman.UC.ServiceReg;

namespace Saman.Form.Drug;

public partial class FrmDrugMain : XtraForm
{
    public byte _DepID = 5;
    public byte _ModID = 5;

    public FrmDrugMain()
    {
        InitializeComponent();
    }

    private void FrmEncMain_Load(object sender, EventArgs e)
    {
        ////panUC.Controls.Clear();

        ////var ucEncCur = new UcEncCur() { Anchor = (AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom), Dock = DockStyle.Fill };
        ////ucEncCur._ModID = _ModID;
        ////panUC.Controls.Add(ucEncCur);
        ////this.ActiveControl = ucEncCur;

        ////// var panUc = (.Parent.f) as DevExpress.XtraEditors.PanelControl;
    }

    private void FrmDrugMain_Load(object sender, EventArgs e)
    {
        var ucControl = new UcEncCur { _ModID = _ModID };
        ShowUC(ucControl);
    }

    private void ShowUC(object ucControl)
    {
        panUC.Controls.Clear();
        var myUC = (UserControl)ucControl;
        myUC.Anchor = AnchorStyles.Top;
        myUC.Anchor = AnchorStyles.Right;
        myUC.Dock = DockStyle.Fill;

        Text = myUC.Tag.ToString();
        panUC.Controls.Add(myUC);
    }

    private void barBtnServiceRegDrug_ItemClick(object sender, ItemClickEventArgs e)
    {
        var ucControl = new UcServiceRegDrug { _ModID = _ModID, _DepID = _DepID };
        ShowUC(ucControl);
    }


    private int CheckCurEnc()
    {
        var CurEncID = 0;
        var rowEncCur = GL.dtEncCur.Select("CurDepID=" + _DepID);
        if (rowEncCur.Length > 0) CurEncID = Convert.ToInt32(rowEncCur[0]["EncID"]);

        return CurEncID;
    }

    private void barBtnEncNew_ItemClick(object sender, ItemClickEventArgs e)
    {
        // this.Text = "پذیرش سرپایی - سیستم پذیرش";
        panUC.Controls.Clear();

        var ucEncNew = new UcEncSave { Anchor = AnchorStyles.Top | AnchorStyles.Right, Dock = DockStyle.Fill };
        ucEncNew._ModID = _ModID;
        ucEncNew._EncType = 1;
        panUC.Controls.Add(ucEncNew);
    }


    private void barButtonItem4_ItemClick(object sender, ItemClickEventArgs e)
    {
        panUC.Controls.Clear();
    }

    private void barRegService_ItemClick(object sender, ItemClickEventArgs e)
    {
        if (CheckCurEnc() == 0) return;

        var UcServiceReg = new UcServiceReg
            { Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom, Dock = DockStyle.Fill };
        UcServiceReg._ModID = _ModID;
        UcServiceReg._DepID = _DepID;

        panUC.Controls.Clear();
        panUC.Controls.Add(UcServiceReg);
    }

    private void barBtnEncSearch_ItemClick(object sender, ItemClickEventArgs e)
    {
        var ucEncSearch = new UcEncSearch
            { Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom, Dock = DockStyle.Fill };
        panUC.Controls.Clear();
        panUC.Controls.Add(ucEncSearch);
    }

    private void barEnc_ItemClick(object sender, ItemClickEventArgs e)
    {
        var ucEncCur = new UcEncCur
            { Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom, Dock = DockStyle.Fill };
        panUC.Controls.Clear();
        ucEncCur._ModID = _ModID;
        panUC.Controls.Add(ucEncCur);
    }
}