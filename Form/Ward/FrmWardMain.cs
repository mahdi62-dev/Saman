using System;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using Saman.APP.SAC;
using Saman.BLL.Dep;
using Saman.UC;
using Saman.UC.Drug;
using Saman.UC.Enc;
using Saman.UC.ServiceReg;
using Saman.UC.Ward;

namespace Saman.Form.Ward;

public partial class FrmWardMain : XtraForm
{
    private readonly Dep _bllDep = new();
    private readonly string _FormName = "بخش";
    private readonly SACPermission sacPermission = new();
    public byte _DepID = 7;

    private DataTable _dtDep = new();
    public byte _ModID = 8;
    public int _StorageID, _StoragePeriodID;

    public FrmWardMain()
    {
        InitializeComponent();
    }

    private void FrmWardMain_Load(object sender, EventArgs e)
    {
        barBtnStatus();
        CheckPermission();

        _dtDep = _bllDep.DepDataLoad(_DepID);
        _StorageID = Convert.ToInt32(_dtDep.Rows[0]["StorageID"]);
        _StoragePeriodID = Convert.ToInt32(_dtDep.Rows[0]["StoragePeriodID"]);

        panUC.Controls.Clear();

        //if (barBtnHome.Enabled != false)
        //{
        //    var ucControl = new UcEncCur() { _ModID = _ModID, _DepID = _DepID };
        //    ShowUC(ucControl);
        //    this.ActiveControl = ucControl;
        //}
        //else
        //{
        //    var ucControl = new UcDefult();
        //    ShowUC(ucControl);
        //    this.ActiveControl = ucControl;

        //}

        var ucControl = new UcWardBed(); // { _ModID = _ModID, _DepID = _DepID };
        ShowUC(ucControl);
        ActiveControl = ucControl;
    }

    private void CheckPermission()
    {
        barBtnHome.Enabled = sacPermission.CheckPermission(_ModID, 70001);
        barBtnEncDepRec.Enabled = sacPermission.CheckPermission(_ModID, 70002);
        barBtnEncSearch.Enabled = sacPermission.CheckPermission(_ModID, 70003);
        barBtnEncStatus.Enabled = sacPermission.CheckPermission(_ModID, 70004);
        barBtnServiceReg.Enabled = sacPermission.CheckPermission(_ModID, 70005);
        barBtnServerRegDrug.Enabled = sacPermission.CheckPermission(_ModID, 70006);
        barBtnStock.Enabled = sacPermission.CheckPermission(_ModID, 70007);
        barBtnSch.Enabled = sacPermission.CheckPermission(_ModID, 70008);
        barBtnScan.Enabled = sacPermission.CheckPermission(_ModID, 70009);
        barBtnReport.Enabled = sacPermission.CheckPermission(_ModID, 700010);
    }

    private void barBtnStatus()
    {
        barBtnHome.Enabled = false;
        barBtnEncDepRec.Enabled = false;
        barBtnEncSearch.Enabled = false;
        barBtnEncStatus.Enabled = false;
        barBtnServiceReg.Enabled = false;
        barBtnServerRegDrug.Enabled = false;
        barBtnStock.Enabled = false;
        barBtnSch.Enabled = false;
        barBtnScan.Enabled = false;
        barBtnReport.Enabled = false;
    }


    private int CheckCurEnc()
    {
        var CurEncID = 0;
        var rowEncCur = GL.dtEncCur.Select("CurDepID=" + _DepID);
        if (rowEncCur.Length > 0) CurEncID = Convert.ToInt32(rowEncCur[0]["EncID"]);

        return CurEncID;
    }

    private void barBtnServerRegDrug_ItemClick(object sender, ItemClickEventArgs e)
    {
        if (CheckCurEnc() == 0) return;

        var ucControl = new UcServiceRegDrug
        {
            _ModID = _ModID,
            _DepID = _DepID,
            _StorageID = _StorageID,
            _StoragePeriodID = _StoragePeriodID
        };

        ShowUC(ucControl);
    }

    private void ShowUC(object ucControl)
    {
        panUC.Controls.Clear();
        var myUC = (UserControl)ucControl;
        myUC.Anchor = AnchorStyles.Top;
        myUC.Anchor = AnchorStyles.Right;
        myUC.Dock = DockStyle.Fill;

        Text = _FormName + " - " + myUC.Tag;
        panUC.Controls.Add(myUC);
    }

    private void barBtnHome_ItemClick(object sender, ItemClickEventArgs e)
    {
        //var ucEncCur = new UcEncCur() { Anchor = (AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom), Dock = DockStyle.Fill };
        //panUC.Controls.Clear();
        //ucEncCur._ModID = _ModID;
        //panUC.Controls.Add(ucEncCur);

        var ucControl = new UcEncCur { _ModID = _ModID, _DepID = _DepID };
        ShowUC(ucControl);
    }

    private void barBtnEncSearch_ItemClick(object sender, ItemClickEventArgs e)
    {
        var ucControl = new UcEncSearch { _ModID = _ModID, _DepID = _DepID };
        ShowUC(ucControl);
    }

    private void barBtnEncStatus_ItemClick(object sender, ItemClickEventArgs e)
    {
        var ucControl = new UcEncInfo { _ModID = _ModID, _DepID = _DepID, _EncType = 1 };
        ShowUC(ucControl);
    }

    private void barBtnStock_ItemClick(object sender, ItemClickEventArgs e)
    {
        //if (!CheckValidation())
        //    return;

        var ucStock = new UcStock
            { _ModID = _ModID, _DepID = _DepID }; //, _StorageID = _StorageID , _StoragePeriodID = _StoragePeriodID
        ShowUC(ucStock);

        ActiveControl = ucStock;
    }

    private void barBtnServiceReg_ItemClick(object sender, ItemClickEventArgs e)
    {
        if (CheckCurEnc() == 0) return;

        var ucControl = new UcServiceReg
        {
            _ModID = _ModID,
            _DepID = _DepID,
            _StorageID = _StorageID,
            _StoragePeriodID = _StoragePeriodID
        };

        ShowUC(ucControl);
    }


    private void barBtnEncDepRec_ItemClick(object sender, ItemClickEventArgs e)
    {
        if (CheckCurEnc() == 0) return;

        var ucControl = new UcEncDepRec { _ModID = _ModID, _DepID = _DepID };
        ShowUC(ucControl);
    }
}