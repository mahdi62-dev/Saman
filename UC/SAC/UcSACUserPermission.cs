using System;
using System.Data;
using DAL;
using DevExpress.XtraEditors;
using LIB;
using Saman.BLL.SAC;

namespace Saman.UC.SAC;

public partial class UcSACUserPermission : XtraUserControl
{
    private readonly Permission _bllSACLoad = new();
    private DataTable _dtSAC;
    public int _PersID, _ResuID;
    public string _PersName;
    public int _RepID;

    public UcSACUserPermission()
    {
        InitializeComponent();
    }

    public int _ModID { get; set; }

    private void UcSACUserPermission_Load(object sender, EventArgs e)
    {
        UL.FillLookup(luePers, "0", cSet.cnStringA, false, "PersID, PersName, ResuID", "vSACResuInfo",
            "SACResuInfo", "IsDisable=0", "PersID");
        UL.FillLookup(luePers1, "0", cSet.cnStringA, false, "PersID, PersName, ResuID", "vSACResuInfo",
            "SACResuInfo", "IsDisable=0", null);

        SacLoadData();
    }

    private void SacLoadData()
    {
        var bllReport = new Permission();

        var dtTree = new DataTable();
        dtTree = bllReport.SACLoad();

        treSac.KeyFieldName = "ActionID";
        treSac.ParentFieldName = "ParentID";

        treSac.DataSource = dtTree;
        treSac.RowHeight = 23;

        // treSac.Nodes.FirstNode.Expanded = true;
        // treSac.Nodes.TreeList.ExpandAll();
    }

    private void SACLoad()
    {
        _dtSAC = _bllSACLoad.SACPersLoad(_PersID);

        if (_dtSAC.Rows.Count <= 0) return;

        int actionID;
        foreach (DataRow row in _dtSAC.Rows)
        {
            if (row["ActionID"] == null)
                return;

            treSac.FindNodeByKeyID(Convert.ToInt32(row["ActionID"])).Checked = true;
            if (treSac.FindNodeByKeyID(Convert.ToInt32(row["ActionID"])).Checked)
                treSac.FindNodeByKeyID(Convert.ToInt32(row["ActionID"])).Expanded = true;
        }
    }

    private void luePers_EditValueChanged(object sender, EventArgs e)
    {
        lblPers1.Visible = false;
        luePers1.Visible = false;


        _PersID = Convert.ToInt32(luePers.EditValue);
        _PersName = luePers.Text;
        _ResuID = Convert.ToInt32(luePers.GetColumnValue("ResuID"));

        lblTitle2.Text = _PersName;
        treSac.UncheckAll();
        treSac.Nodes.TreeList.CollapseAll();
        SACLoad();
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

    private void btnSave_Click(object sender, EventArgs e)
    {
        if (!CheckValidation()) return;

        var nodes = treSac.GetNodeList();
        foreach (var node in nodes)
            _bllSACLoad.SACSave(Convert.ToInt32(node["ActionID"]), _ResuID, _ModID = Convert.ToInt32(node["ModID"])
                , node.Checked, GL.UserPersID);
    }
}