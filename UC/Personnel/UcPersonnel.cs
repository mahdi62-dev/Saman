using System;
using System.Data;
using System.Windows.Forms;
using DevExpress.Data;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
using Saman.BLL.Personnel;

namespace Saman.UC;

public partial class UcPersonnel : XtraUserControl
{
    private readonly EmpAction _bllEmpAction = new();
    private readonly BLL.Personnel.Personnel bllPersonnel = new();
    private DataTable _dtEmpAction;
    private DataTable _PersonnnelLoadList;
    private DataRow _rowPersSelect;

    public UcPersonnel()
    {
        InitializeComponent();
    }

    public byte _ModID { get; set; }
    public int _PersID { get; set; }

    public int _EmpActionID { get; set; }

    private void grdPersonnelLoad_Load(object sender, EventArgs e)
    {
        PersLoad();
        ShortKey();
    }

    private void ShortKey()
    {
        msiEditEmpAction.Click += btnEditEmpAction_Click;
        msiNewEmpAction.Click += btnNewEmpAction_Click;

        panMain.Focus();
    }

    private void PersLoad()
    {
        var col = grvPersonnelLoad.Columns.ColumnByFieldName("RecNo");
        col.UnboundType = UnboundColumnType.Integer;
        grvPersonnelLoad.CustomUnboundColumnData += gridView1_CustomUnboundColumnData;

        void gridView1_CustomUnboundColumnData(object sender, CustomColumnDataEventArgs e)
        {
            if (e.IsGetData) e.Value = e.ListSourceRowIndex + 1;
        }

        _PersonnnelLoadList = bllPersonnel.PerssonelLoad();
        grdPersonnelLoad.DataSource = _PersonnnelLoadList;
    }

    ////Int32[] rows = grvServiceReg.GetSelectedRows();
    ////    foreach (Int32 i in rows)
    ////{
    ////    grvServiceReg.GetDataRow(i);

    ////    int NO = Convert.ToInt32(grvServiceReg.GetDataRow(i)["RecNo"].ToString());

    ////    DataRow row = _dtServiceReg.Select("RecNo=" + NO)[0];

    ////    if (Convert.ToInt32(row["SRID"]) == 0) 
    ////        row.Delete();
    ////    else
    ////        row["CRUD"] = 4;
    ////}

    private void btnEditEmpAction_Click(object sender, EventArgs e)
    {
        _rowPersSelect = grvEmpAction.GetDataRow(grvEmpAction.FocusedRowHandle);

        var PersID = Convert.ToInt32(_rowPersSelect["PersID"]);
        _EmpActionID = Convert.ToInt32(_rowPersSelect["EmpActionID"]);
        GL.ErpCurPersID = PersID;

        var panUc = Parent as PanelControl;

        var ucEmpAction = new UcEmpAction
        {
            Anchor = AnchorStyles.Top | AnchorStyles.Right,
            Dock = DockStyle.Fill,
            _PersID = PersID,
            _ModID = _ModID,
            _EmpActionID = _EmpActionID
        };

        panUc.Controls.Clear();

        panUc.Controls.Add(ucEmpAction);
    }

    private void grdPersonnelLoad_Click(object sender, EventArgs e)
    {
        if (grvPersonnelLoad.FocusedRowHandle < 0) return;

        _rowPersSelect = grvPersonnelLoad.GetDataRow(grvPersonnelLoad.FocusedRowHandle);


        var PersID = Convert.ToInt32(_rowPersSelect["PersID"]);
        _PersID = PersID;
        _dtEmpAction = _bllEmpAction.PersEmpActionLoad(_PersID);

        grdEmpAction.DataSource = _dtEmpAction;
        if (_dtEmpAction == null) return;

        lblTitle.Text = _rowPersSelect["FirstName"] + " " + _rowPersSelect["LastName"];
    }

    private void grdPersonnelLoad_KeyDown(object sender, KeyEventArgs e)
    {
        _rowPersSelect = grvPersonnelLoad.GetDataRow(grvPersonnelLoad.FocusedRowHandle);
        var PersID = Convert.ToInt32(_rowPersSelect["PersID"]);
        _PersID = PersID;
        _dtEmpAction = _bllEmpAction.PersEmpActionLoad(_PersID);

        grdEmpAction.DataSource = _dtEmpAction;
        if (_dtEmpAction == null) return;

        lblTitle.Text = _rowPersSelect["FirstName"] + " " + _rowPersSelect["LastName"];
    }

    private void grdPersonnelLoad_KeyUp(object sender, KeyEventArgs e)
    {
        _rowPersSelect = grvPersonnelLoad.GetDataRow(grvPersonnelLoad.FocusedRowHandle);
        var PersID = Convert.ToInt32(_rowPersSelect["PersID"]);
        _PersID = PersID;
        _dtEmpAction = _bllEmpAction.PersEmpActionLoad(_PersID);

        grdEmpAction.DataSource = _dtEmpAction;
        if (_dtEmpAction == null) return;

        lblTitle.Text = _rowPersSelect["FirstName"] + " " + _rowPersSelect["LastName"];
    }

    private void grdEmpAction_DoubleClick(object sender, EventArgs e)
    {
        _rowPersSelect = grvEmpAction.GetDataRow(grvEmpAction.FocusedRowHandle);

        var PersID = Convert.ToInt32(_rowPersSelect["PersID"]);
        _EmpActionID = Convert.ToInt32(_rowPersSelect["EmpActionID"]);
        GL.ErpCurPersID = PersID;

        var panUc = Parent as PanelControl;

        var ucEmpAction = new UcEmpAction
        {
            Anchor = AnchorStyles.Top | AnchorStyles.Right,
            Dock = DockStyle.Fill,
            _PersID = PersID,
            _ModID = _ModID,
            _EmpActionID = _EmpActionID
        };

        panUc.Controls.Clear();

        panUc.Controls.Add(ucEmpAction);
    }

    private void btnNewEmpAction_Click(object sender, EventArgs e)
    {
        var PersID = Convert.ToInt32(_rowPersSelect["PersID"]);
        GL.ErpCurPersID = PersID;

        var panUc = Parent as PanelControl;

        var ucEmpAction = new UcEmpAction
        {
            Anchor = AnchorStyles.Top | AnchorStyles.Right,
            Dock = DockStyle.Fill,
            _PersID = PersID,
            _ModID = _ModID,
            _EmpActionID = 0
        };

        panUc.Controls.Clear();

        panUc.Controls.Add(ucEmpAction);
    }
}