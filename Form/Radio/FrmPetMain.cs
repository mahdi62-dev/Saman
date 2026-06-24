using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using Saman.APP.SAC;
using Saman.BLL.Dep;
using Saman.Form.Attach;
using Saman.UC;
using Saman.UC.Attach;
using Saman.UC.DocReport;
using Saman.UC.Enc;
using Saman.UC.Page;
using Saman.UC.Pet;
using Saman.UC.sch;
using Saman.UC.ServiceReg;

namespace Saman.Form.Pet;

public partial class FrmPetMain : XtraForm
{
    private readonly Dep _bllDep = new();
    private readonly string _FormName = "PET Scan";

    private readonly SACPermission sacPermission = new();
    public int _DepID = 30;

    private DataTable _dtDep = new();
    public int _ModID = 30;

    public int _StorageID, _StoragePeriodID;

    public FrmPetMain()
    {
        InitializeComponent();
    }


    private void FrmPetMain_Load(object sender, EventArgs e)
    {
        barBtnStatus();
        CheckPermission();
        TempDocClean();

        _dtDep = _bllDep.DepDataLoad(_DepID);
        _StorageID = Convert.ToInt32(_dtDep.Rows[0]["StorageID"]);
        _StoragePeriodID = Convert.ToInt32(_dtDep.Rows[0]["StoragePeriodID"]);

        if (barBtnHome.Enabled)
        {
            var ucControl = new UcEncCur { _ModID = _ModID, _DepID = _DepID };
            ShowUC(ucControl);
        }
        else
        {
            var ucControl = new UcDefult();
            ShowUC(ucControl);
        }
    }

    private void CheckPermission()
    {
        barBtnHome.Enabled = sacPermission.CheckPermission(_ModID, 300001);
        barBtnEncNewDayCare.Enabled = sacPermission.CheckPermission(_ModID, 300002);
        barBtnEncSearch.Enabled = sacPermission.CheckPermission(_ModID, 300003);
        barBtnEncInfo.Enabled = sacPermission.CheckPermission(_ModID, 300004);
        barRegService.Enabled = sacPermission.CheckPermission(_ModID, 300005);
        barBtnSch.Enabled = sacPermission.CheckPermission(_ModID, 300006);
        barBtnScan.Enabled = sacPermission.CheckPermission(_ModID, 300007);
        barBtnPageAnsPet.Enabled = sacPermission.CheckPermission(_ModID, 300008);
        barBtnPageAnsRich.Enabled = sacPermission.CheckPermission(_ModID, 300009);
        barBtnAttach.Enabled = sacPermission.CheckPermission(_ModID, 300010);
        barBtnProtocol.Enabled = sacPermission.CheckPermission(_ModID, 300011);
        barBtnResearch.Enabled = sacPermission.CheckPermission(_ModID, 300012);
    }

    private void barBtnStatus()
    {
        barBtnHome.Enabled = false;
        barBtnEncNewDayCare.Enabled = false;
        barBtnEncSearch.Enabled = false;
        barBtnEncInfo.Enabled = false;
        barRegService.Enabled = false;
        barBtnSch.Enabled = false;
        barBtnScan.Enabled = false;
        barBtnPageAnsPet.Enabled = false;
        barBtnPageAnsRich.Enabled = false;
        barBtnAttach.Enabled = false;
        barBtnProtocol.Enabled = false;
        barBtnResearch.Enabled = false;
    }

    private void TempDocClean()
    {
        var _FileName = Path.GetTempPath();
        _FileName = _FileName + @"SamanDocTemp";

        if (Directory.Exists(_FileName)) Directory.Delete(_FileName);
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


    private int CheckCurEnc()
    {
        var CurEncID = 0;
        var rowEncCur = GL.dtEncCur.Select("CurDepID=" + _DepID);
        if (rowEncCur.Length > 0) CurEncID = Convert.ToInt32(rowEncCur[0]["EncID"]);

        return CurEncID;
    }

    private void barEnc_ItemClick(object sender, ItemClickEventArgs e)
    {
        var ucControl = new UcEncCur { _ModID = _ModID, _DepID = _DepID };
        ShowUC(ucControl);
    }

    private void barBtnEncNewOP_ItemClick(object sender, ItemClickEventArgs e)
    {
    }


    private void barBtnEncNewDayCare_ItemClick(object sender, ItemClickEventArgs e)
    {
        var ucControl = new UcEncSave { _ModID = _ModID, _DepID = _DepID, _EncType = 0 };
        ShowUC(ucControl);
    }

    private void barBtnEncStatus_ItemClick(object sender, ItemClickEventArgs e)
    {
        var ucControl = new UcEncInfo { _ModID = _ModID, _DepID = _DepID };
        ShowUC(ucControl);
    }

    private void barBtnEncSearch_ItemClick(object sender, ItemClickEventArgs e)
    {
        var ucControl = new UcEncSearch { _ModID = _ModID, _DepID = _DepID };
        ShowUC(ucControl);
    }

    private void barBtnSch_ItemClick(object sender, ItemClickEventArgs e)
    {
        var ucControl = new UcSch { _ModID = _ModID, _DepID = _DepID };
        ShowUC(ucControl);
    }

    private void FrmPetMain_Activated(object sender, EventArgs e)
    {
        var ctrl = panUC.Controls.Find("UcPageAns", false);
        if (ctrl.Length > 0)
        {
            var uc = (UcPageAnsPet)ctrl[0];
            uc.PageAnsSeach();
        }
    }


    private void barBtnAttach_ItemClick(object sender, ItemClickEventArgs e)
    {
        var frmAtt = new FrmAttach();
        frmAtt.ShowDialog();
    }

    private void barBtnPageAnsRich_ItemClick(object sender, ItemClickEventArgs e)
    {
        var ucControl = new UcPageAnsPet { _ModID = _ModID, _DepID = _DepID };
        ucControl.Name = "UcPageAns";
        ShowUC(ucControl);
    }

    private void barBtnPageAnsPet_ItemClick(object sender, ItemClickEventArgs e)
    {
        var ucControl = new UcPageAnsPet { _ModID = _ModID, _DepID = _DepID };
        ucControl.Name = "UcPageAns";
        ShowUC(ucControl);
    }

    private void barBtnScan_ItemClick(object sender, ItemClickEventArgs e)
    {
        if (CheckCurEnc() == 0) return;

        var ucControl = new UcAttach
        {
            _ModID = _ModID,
            _DepID = _DepID
        };

        ShowUC(ucControl);
    }

    private void barBtnResearch_ItemClick(object sender, ItemClickEventArgs e)
    {
        var ucControl = new UcDocReportExtract { _ModID = _ModID, _DepID = _DepID };
        ucControl.Name = "UcDocReportExtract";
        ShowUC(ucControl);
    }

    private void barBtnProtocol_ItemClick(object sender, ItemClickEventArgs e)
    {
        var ucControl = new UcPetProtocol { _ModID = _ModID, _DepID = _DepID };
        ucControl.Name = "UcPetProtoco";
        ShowUC(ucControl);
    }

    private void barRegService_ItemClick(object sender, ItemClickEventArgs e)
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
}