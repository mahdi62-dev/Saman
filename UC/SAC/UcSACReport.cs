using System;
using System.Data;
using DAL;
using DevExpress.XtraEditors;
using LIB;
using Saman.BLL.SAC;

namespace Saman.UC.SAC;

public partial class UcSACReport : XtraUserControl
{
    private readonly SACReport _bllSACReport = new();

    private DataTable _dtSACReport;
    public int _PersID;
    public string _PersName;
    public int _RepID;


    public UcSACReport()
    {
        InitializeComponent();
    }

    public int _ModID { get; set; }

    private void UcSACReport_Load(object sender, EventArgs e)
    {
        UL.FillLookup(luePers, "0", cSet.cnStringA, false, "PersID, PersName", "vSACResuInfo", "SACResuInfo",
            "IsDisable=0", "PersID");
        UL.FillLookup(luePers1, "0", cSet.cnStringA, false, "PersID, PersName", "vSACResuInfo", "SACResuInfo",
            "IsDisable=0", null);


        TreeLoadData();
    }

    private void TreeLoadData()
    {
        var bllReport = new BLL.Report.Report();

        var dtTree = new DataTable();
        dtTree = bllReport.RepListAllLoad();

        treReport.KeyFieldName = "RepID";
        treReport.ParentFieldName = "ParentID";

        treReport.DataSource = dtTree;
        treReport.RowHeight = 23;

        treReport.Nodes.FirstNode.Expanded = true;
        treReport.Nodes.TreeList.ExpandAll();
    }

    private void btnSACReportCopy_Click(object sender, EventArgs e)
    {
        if (!CheckValidation()) return;

        lblPers1.Visible = true;
        luePers1.Visible = true;
    }


    private bool CheckValidation()
    {
        var isValid = true;

        if (luePers.EditValue.ToString() == "0" || string.IsNullOrEmpty(luePers.Text))
        {
            luePers.ErrorText = "لطفا پرسنل را انتخاب نمایید";
            isValid = false;
        }


        return isValid;
    }

    private void luePers_EditValueChanged(object sender, EventArgs e)
    {
        lblPers1.Visible = false;
        luePers1.Visible = false;

        _PersID = Convert.ToInt32(luePers.EditValue);
        _PersName = luePers.Text;

        lblTitle2.Text = _PersName;

        treReport.UncheckAll();
        SACReportLoad();
    }

    private void SACReportLoad()
    {
        _dtSACReport = _bllSACReport.SACReportLoad(_PersID);

        if (_dtSACReport.Rows.Count <= 1) return;

        foreach (DataRow row in _dtSACReport.Rows)
            treReport.FindNodeByKeyID(Convert.ToInt32(row["RepID"])).Checked = true;
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
        if (!CheckValidation()) return;

        var nodes = treReport.GetNodeList();
        foreach (var node in nodes)
            _bllSACReport.SACReportSave(Convert.ToInt32(node["RepID"]),
                _PersID, node.Checked, GL.UserPersID);


        //List<TreeListNode> list = treReport.GetAllCheckedNodes();

        //foreach (TreeListNode node in list)
        //{ 


        //    _bllSACReport.SACUserReport(0,_RepID, _PesrID, GL.UserPersID);

        //}


        //foreach (TreeListNode node in list)
        //{

        //    _dtSACReport = _bllSACReport.SACUserReport(1, _RepID, _PesrID, GL.UserPersID);
        //    treReport.DataSource = _dtSACReport;

        //    treReport.SetNodeCheckState(node, CheckState.Checked, true);


        //}
    }
}