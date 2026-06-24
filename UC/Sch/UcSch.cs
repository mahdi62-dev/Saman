using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Timers;
using System.Windows.Forms;
using App;
using DAL;
using DevExpress.Data;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraTab;
using LIB;
using Saman.APP.Stimul;
using Saman.BLL.Sch;
using Saman.Form.Sch;
using Saman.UC.Enc;
using Stimulsoft.Report.Dictionary;
using Control = App.Control;

namespace Saman.UC.sch;

public partial class UcSch : XtraUserControl
{
    //private int autoscroll = 0;
    //bool initialrun = false;

    private const string OrderFieldName = "RecNo";
    private readonly Stimul _appStimul = new();


    private readonly BLL.Report.Report _bllReport = new();

    private readonly Sch _bllSch = new();
    private readonly int _CRUD;
    private readonly bool _IsSchPaste;
    private readonly string _OldSchTime;


    private readonly int _PersID;

    private readonly DataRow _rowSelect;
    private readonly bool initialrun = false;

    private DataTable _dtSch = new();


    private DataTable _dtSchDef;

    private bool _IsConfirm;
    private bool _IsRecNoEdit, _IsSchTimeEdit;
    private bool _IsSchCopy;
    private bool _IsSchCut;
    private bool _IsSchSearch;

    private int _OldRecNo, _NewRecNo, _NewSchID, _OldSchID;

    private int _RecNo,
        _SchDefTotalCapacity,
        _SchID,
        _EncID;

    private DataRow _rowSchDefSelect;

    private DataRow _rowSchSelect;

    private int autoscroll;


    //--Drag and Drop
    private GridHitInfo downHitInfo;

    public UcSch()
    {
        InitializeComponent();
    }

    public int _ModID { get; set; }
    public int _DepID { get; set; }
    public int _SchDefID { get; set; }

    public bool _ReadOnly { get; set; }

    private void UcSch_Load(object sender, EventArgs e)
    {
        UL.FillLookup(lueDep, "30", cSet.cnStringA, false, "DepID,Name", "vDep", "Dep", null, null);
        UL.FillLookup(lueDr, "1", cSet.cnStringA, false, "*", "vDr", "Dr", null, null);

        var appSDate = new SDate();
        var curDateTime = DateTime.Parse(appSDate.ShamsiDate());
        datStartDate.DateTime = curDateTime;
        datEndDate.DateTime = curDateTime.AddDays(30);

        SchDefLoad();


        //grvSch.PopulateColumns();
        grvSch.Columns[OrderFieldName].SortOrder = ColumnSortOrder.Ascending;
        grvSch.OptionsCustomization.AllowSort = true;
        grvSch.OptionsView.ShowGroupPanel = false;

        MaskSet();
    }

    private void MaskSet()
    {
        txtFirstName.Properties.Mask.EditMask = App.UI.MaskPersian;
        txtLastName.Properties.Mask.EditMask = App.UI.MaskPersian;
        txtNationalCode.Properties.Mask.EditMask = App.UI.MaskNum;
        txtMobile.Properties.Mask.EditMask = App.UI.MaskNum;
        repTxtRecNo.Mask.EditMask = App.UI.MaskNum;
        //repTimSchTime.Mask.EditMask = App.UI.MaskNum;
    }

    private void FormNull()
    {
        txtFirstName.Text = null;
        txtLastName.Text = null;
        txtMobile.Text = null;
        txtNationalCode.Text = null;
    }

    private bool CheckValidation()
    {
        var isValid = true;

        return isValid;
    }

    private void btnEsc_Click(object sender, EventArgs e)
    {
        var panUC = Parent as PanelControl;
        panUC.Controls.Clear();

        var ucDefult = new UcDefult
            { Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom, Dock = DockStyle.Fill };
        panUC.Controls.Add(ucDefult);
    }


    private void SchSearch()
    {
        var com = new SqlCommand();
        com.Parameters.AddRange(new[]
        {
            new SqlParameter("@NationalCode", txtNationalCode.Text),
            new SqlParameter("@FirstName", txtFirstName.Text),
            new SqlParameter("@LastName", txtLastName.Text),
            new SqlParameter("@Mobile", txtMobile.Text)
        });

        var dalData = new Data();
        _dtSch = dalData.GetDataTableBySP(cSet.cnStringA, com, "spSchSearch");
        grdSch.DataSource = _dtSch;


        if (_IsSchSearch)
        {
            //grvSch_Click(null, null);
            _rowSchDefSelect = grvSch.GetDataRow(grvSch.FocusedRowHandle);
            if (_rowSchDefSelect == null) return;

            _SchDefID = Convert.ToInt32(_rowSchDefSelect["SchDefID"]);

            _dtSchDef = _bllSch.SchDef(1, null, null, null, null
                , null, null, null, null, null, null, null, null, null, null, _SchDefID, null);


            grdSchDef.DataSource = _dtSchDef;


            if (_dtSchDef == null) return;

            _rowSchDefSelect = grvSchDef.GetDataRow(grvSch.FocusedRowHandle);
            lblTitle2.Text = _rowSchDefSelect["Date"].ToString();

            tabmain.SelectedTabPage = tabSch;
            //lblTitle.Text = _rowSchDefSelect["FirstName"] + " " + _rowSchDefSelect["LastName"];
            //datStartDate.EditValue = Convert.ToDateTime(_dtSchDef.Rows[0]["Date"]);
        }
    }

    private void gridView1_CustomUnboundColumnData(object sender, CustomColumnDataEventArgs e)
    {
        if (e.IsGetData) e.Value = e.ListSourceRowIndex + 1;
    }

    private void SchDefLoad()
    {
        if (!CheckValidation()) return;

        _dtSchDef = _bllSch.SchDef(1, lueDep.EditValue, lueDr.EditValue, datStartDate.Text, datEndDate.Text
            , null, null, null, null, null, null, null, null, null, null, null, null);

        var col = grvSchDef.Columns.ColumnByFieldName("RecNo");
        col.UnboundType = UnboundColumnType.Integer;
        grvSchDef.CustomUnboundColumnData += gridView1_CustomUnboundColumnData;

        grdSchDef.DataSource = _dtSchDef;
        var rowSelectgrv = grvSch.GetFocusedDataSourceRowIndex();
        grvSch.SelectRow(rowSelectgrv);
        grvSch.FocusedRowHandle = rowSelectgrv;

        //grvSchDef_DoubleClick(null, null);

        _IsSchSearch = false;

        FormNull();
    }

    private void datStartDate_EditValueChanged(object sender, EventArgs e)
    {
        SchDefLoad();
    }

    private void datEndDate_EditValueChanged(object sender, EventArgs e)
    {
        SchDefLoad();
    }

    private void lueDep_EditValueChanged(object sender, EventArgs e)
    {
        SchDefLoad();
    }

    private void lueDr_EditValueChanged(object sender, EventArgs e)
    {
        SchDefLoad();
    }

    private void panelControl1_Paint(object sender, PaintEventArgs e)
    {
        Control.TextEditLine(lueDep, e);
        Control.TextEditLine(datStartDate, e);
        Control.TextEditLine(datEndDate, e);
        Control.TextEditLine(lueDr, e);
    }

    private void panSchSearch_Paint(object sender, PaintEventArgs e)
    {
        Control.TextEditLine(txtFirstName, e);
        Control.TextEditLine(txtLastName, e);
        Control.TextEditLine(txtMobile, e);
        Control.TextEditLine(txtNationalCode, e);
    }

    private void grvSch_Click(object sender, EventArgs e)
    {
        if (_IsSchSearch)
        {
            _rowSchDefSelect = grvSch.GetDataRow(grvSch.FocusedRowHandle);
            if (_rowSchDefSelect == null) return;

            _SchDefID = Convert.ToInt32(_rowSchDefSelect["SchDefID"]);

            _dtSchDef = _bllSch.SchDef(1, null, null, null, null
                , null, null, null, null, null, null, null, null, null, null, _SchDefID, null);


            grdSchDef.DataSource = _dtSchDef;
            //grvSch_Click(null, null);


            if (_dtSchDef == null) return;

            _rowSchSelect = grvSch.GetDataRow(grvSch.FocusedRowHandle);
            lblTitle2.Text = _rowSchSelect["Date"].ToString();

            //lblTitle.Text = _rowSchDefSelect["FirstName"] + " " + _rowSchDefSelect["LastName"];
            //datStartDate.EditValue = Convert.ToDateTime(_dtSchDef.Rows[0]["Date"]);
        }
    }

    private void grvSchDef_DoubleClick(object sender, EventArgs e)
    {
        SchDefSelect();
        tabmain.SelectedTabPage = tabSch;
        panSchToolbar.Visible = true;
    }

    private void btnRefreshSchDef_Click(object sender, EventArgs e)
    {
        SchDefLoad();
        tabmain.SelectedTabPage = tabSchDef;
    }

    private void SchDefSelect()
    {
        if (grvSchDef.FocusedRowHandle < 0) return;

        DataRow rowSelect;

        rowSelect = grvSchDef.GetDataRow(grvSchDef.FocusedRowHandle);

        _SchDefID = Convert.ToInt32(rowSelect["SchDefID"]);
        _SchDefTotalCapacity = Convert.ToInt32(rowSelect["Capacity"]) + Convert.ToInt32(rowSelect["ExtraNum"]);


        var _Date = rowSelect["Date"].ToString();

        SchLoad();
        var rowSelectgrv = grvSch.GetFocusedDataSourceRowIndex();
        grvSch.SelectRow(rowSelectgrv);
        grvSch.FocusedRowHandle = rowSelectgrv;
        lblTitle2.Text = _Date;
    }

    private void SchLoad()
    {
        _dtSch = _bllSch.SchLoad(1, _SchDefID);

        if (!chkCancelShow.Checked)
            grdSch.DataSource = _dtSch.Select("RecNo > 0").CopyToDataTable();
        else
            grdSch.DataSource = _dtSch;
    }


    private void lblSchDel_Click(object sender, EventArgs e)
    {
        if (grvSch.FocusedRowHandle < 0) return;

        var rowSelect = grvSch.GetDataRow(grvSch.FocusedRowHandle);

        _SchID = Convert.ToInt32(rowSelect["SchID"]);
        _EncID = Convert.ToInt32(rowSelect["EncID"]);

        var sui = new SUI();
        if (_EncID > 0)
        {
            sui.ShowWarningMessageForm("پیام", "مراجعه کننده دارای پذیرش است", true);
            return;
        }

        if (_SchID == 0) return;

        var FullName = rowSelect["FirstName"] + " " + rowSelect["LastName"];


        var result = sui.FormShowMessage("حذف وقت", "وقت << " + FullName + " >> حذف گردد؟", "بله", "خیر");

        if (result == DialogResult.Yes)
        {
            _bllSch.SchRecDel(4, _SchID);
            SchLoad();
            var rowSelectgrv = grvSch.GetFocusedDataSourceRowIndex();
            grvSch.SelectRow(rowSelectgrv);
            grvSch.FocusedRowHandle = rowSelectgrv;
        }
    }

    private void lblChkConfirm_Click(object sender, EventArgs e)
    {
        SchConfirm(false);
    }

    private void lblChkConfirmAll_Click(object sender, EventArgs e)
    {
        SchConfirm(true);
    }


    private void SchConfirm(bool ConfirmAll)
    {
        if (grvSch.FocusedRowHandle < 0) return;

        var rowSelect = grvSch.GetDataRow(grvSch.FocusedRowHandle);
        var rowSelectgrv = grvSch.GetFocusedDataSourceRowIndex();

        _SchID = Convert.ToInt32(rowSelect["SchID"]);
        _IsConfirm = Convert.ToBoolean(rowSelect["IsConfirm"]);
        _EncID = Convert.ToInt32(rowSelect["EncID"]);

        var sui = new SUI();
        if (_EncID > 0)
        {
            sui.ShowWarningMessageForm("پیام", "مراجعه کننده دارای پذیرش است", true);
            return;
        }


        if (_IsConfirm && !ConfirmAll)
        {
            string FullName1;
            string title1, message1;
            FullName1 = rowSelect["FirstName"] + " " + rowSelect["LastName"];
            title1 = "لغو تائید وقت";
            message1 = "تائید وقت  << " + FullName1 + " >> لغو شود ؟";
            var result1 = sui.FormShowMessage(title1, message1, "بله", "خیر");

            if (result1 == DialogResult.Yes)
            {
                _bllSch.SchConfirm(-1, _SchDefID, _SchID, false);

                SchLoad();
                grvSch.SelectRow(rowSelectgrv);
                grvSch.FocusedRowHandle = rowSelectgrv;
            }

            return;
        }


        if (_SchID == 0 && !ConfirmAll) return;

        string FullName;

        string title, message;
        if (!ConfirmAll)
        {
            FullName = rowSelect["FirstName"] + " " + rowSelect["LastName"];
            title = "تائید وقت";
            message = "تائید وقت << " + FullName + " >> ؟";
        }
        else
        {
            title = "تائید همگی";
            message = "تائید همگی وقتها ؟ ";
        }


        var result = sui.FormShowMessage(title, message, "بله", "خیر");

        if (result == DialogResult.Yes)
        {
            if (!ConfirmAll)
                _bllSch.SchConfirm(-1, _SchDefID, _SchID, true);
            else
                _bllSch.SchConfirm(-1, _SchDefID, 0, true);

            SchLoad();
            grvSch.SelectRow(rowSelectgrv);
            grvSch.FocusedRowHandle = rowSelectgrv;
        }
    }

    private void EncSch()
    {
        if (grvSch.FocusedRowHandle < 0) return;

        DataRow rowSelect;

        rowSelect = grvSch.GetDataRow(grvSch.FocusedRowHandle);

        _RecNo = Convert.ToInt32(rowSelect["RecNo"]);
        _SchID = Convert.ToInt32(rowSelect["SchID"]);
        _EncID = Convert.ToInt32(rowSelect["EncID"]);
        _IsConfirm = Convert.ToBoolean(rowSelect["IsConfirm"]);


        var sui = new SUI();
        if (!_IsConfirm)
        {
            sui.ShowWarningMessageForm("پیام", "وقت تائید نشده است", true);
            return;
        }

        if (_EncID > 0)
        {
            sui.ShowWarningMessageForm("پیام", "مراجعه کننده دارای پذیرش است", true);
            return;
        }

        if (_SchID == 0) return;

        var panUc = Parent as PanelControl;
        panUc.Controls.Clear();

        var ucEncNew = new UcEncSave
        {
            Anchor = AnchorStyles.Top | AnchorStyles.Right,
            Dock = DockStyle.Fill,
            _ModID = _ModID,
            _DepID = _DepID,
            _IsFromSch = true,
            _SchID = _SchID
        };
        panUc.Controls.Add(ucEncNew);
    }

    private void grvSch_MouseDown(object sender, MouseEventArgs e)
    {
        if (_IsSchSearch) return;

        var view = sender as GridView;
        downHitInfo = null;
        var hitInfo = view.CalcHitInfo(new Point(e.X, e.Y));
        if (ModifierKeys != Keys.None) return;

        if (e.Button == MouseButtons.Left && hitInfo.InRow) downHitInfo = hitInfo;
    }

    private void grvSch_MouseMove(object sender, MouseEventArgs e)
    {
        var view = sender as GridView;
        if (e.Button == MouseButtons.Left && downHitInfo != null)
        {
            var dragSize = SystemInformation.DragSize;
            var dragRect = new Rectangle(new Point(downHitInfo.HitPoint.X - dragSize.Width / 2,
                downHitInfo.HitPoint.Y - dragSize.Height / 2), dragSize);

            if (!dragRect.Contains(new Point(e.X, e.Y)))
            {
                view.GridControl.DoDragDrop(downHitInfo, DragDropEffects.All);
                downHitInfo = null;
            }
        }
    }

    private void grdSch_DragOver(object sender, DragEventArgs e)
    {
        e.Effect = DragDropEffects.None;

        var downHitInfo = e.Data.GetData(typeof(GridHitInfo)) as GridHitInfo;
        if (downHitInfo != null)
        {
            var grid = sender as GridControl;
            var view = grid.MainView as GridView;
            var pt = grid.PointToClient(new Point(e.X, e.Y));
            var hitInfo = view.CalcHitInfo(pt);
            if (hitInfo.HitTest == GridHitTest.RowCell)
            {
                var gvi = grvSch.GetViewInfo() as GridViewInfo;
                if (gvi.RowsLoadInfo.TopVisibleRowIndex == hitInfo.RowHandle && hitInfo.InRow)
                {
                    autoscroll = -1;
                    return;
                }

                var max_vi = -1;
                var max_rh = -1;
                var pre_max_rh = -1;
                for (var i = 0; i < gvi.RowsLoadInfo.ResultRows.Count; i++)
                    if (max_vi < gvi.RowsLoadInfo.ResultRows[i].VisibleIndex)
                    {
                        pre_max_rh = max_rh;
                        max_rh = gvi.RowsLoadInfo.ResultRows[i].RowHandle;
                        max_vi = gvi.RowsLoadInfo.ResultRows[i].VisibleIndex;
                    }

                if (pre_max_rh <= hitInfo.RowHandle && hitInfo.InRow)
                {
                    autoscroll = 1;
                    return;
                }
            }

            if (hitInfo.HitTest == GridHitTest.VScrollBar)
            {
                var scrollrect = (view.GetViewInfo() as GridViewInfo).ViewRects.Scroll;
                autoscroll = pt.Y - scrollrect.Top < scrollrect.Bottom - pt.Y ? -1 : 1;
            }

            if (hitInfo.InRow && hitInfo.RowHandle != downHitInfo.RowHandle) e.Effect = DragDropEffects.Move;
        }
    }

    private void grdSch_DragDrop(object sender, DragEventArgs e)
    {
        var grid = sender as GridControl;
        var view = grid.MainView as GridView;
        var downHitInfo = e.Data.GetData(typeof(GridHitInfo)) as GridHitInfo;
        var hitInfo = view.CalcHitInfo(grid.PointToClient(new Point(e.X, e.Y)));
        var sourceRow = downHitInfo.RowHandle;
        var targetRow = hitInfo.RowHandle;
        MoveRow(sourceRow, targetRow);
    }

    private void MoveRow(int sourceRow, int targetRow)
    {
        if (sourceRow == targetRow || sourceRow == targetRow + 1) return;

        var view = grvSch;
        var row1 = view.GetDataRow(targetRow);
        var row2 = view.GetDataRow(targetRow + 1);
        var dragRow = view.GetDataRow(sourceRow);
        var val1 = (int)row1[OrderFieldName];
        if (row2 == null)
        {
            dragRow[OrderFieldName] = val1 + 1;
        }
        else
        {
            var val2 = (int)row2[OrderFieldName];
            dragRow[OrderFieldName] = (val1 + val2) / 2;
            SchDragDropSort();
        }
    }

    private void grdSch_MouseClick(object sender, MouseEventArgs e)
    {
        //SchDragDropSort();
    }

    private void SchDragDropSort()
    {
        var view = grvSch;
        var c = view.RowCount;
        DataRow row;
        for (var i = 0; i < c; i++)
        {
            row = view.GetDataRow(i);
            row["RecNo"] = i + 1;
        }
    }


    private void DoScrollView()
    {
        if (autoscroll == 0) return;

        grvSch.TopRowIndex += autoscroll;
        autoscroll = 0;
    }

    private void lblSchSearch_Click(object sender, EventArgs e)
    {
        panSchSearch.Visible = !panSchSearch.Visible;
    }

    private void timer1_Elapsed(object sender, ElapsedEventArgs e)
    {
        // this.BeginInvoke(new MethodInvoker(DoScrollView));
    }

    private void btnSchSearch_Click(object sender, EventArgs e)
    {
        _IsSchSearch = true;
        SchSearch();
    }

    private void lblSchTimeSave_Click(object sender, EventArgs e)
    {
        //if (_IsSchSearch == true) return;

        //GridView view = grvSch;
        //int c = view.RowCount;

        //int schID = 0, recNo = 0;
        //string schTime = "00:00";

        //DataRow row;
        //for (int i = 0; i < c; i++)
        //{
        //    row = view.GetDataRow(i);
        //    schID = Convert.ToInt32(row["SchID"]);
        //    recNo = Convert.ToInt32(row["RecNo"]);
        //    schTime = row["SchTime"].ToString();

        //    if (schID > 0)
        //        _bllSch.SchGridSave(schID, recNo, schTime);
        //}

        //SchLoad();
    }

    private void txtFirstName_KeyPress(object sender, KeyPressEventArgs e)
    {
        _IsSchSearch = true;

        if (e.KeyChar == 13) SchSearch();
    }

    private void txtLastName_KeyPress(object sender, KeyPressEventArgs e)
    {
        _IsSchSearch = true;

        if (e.KeyChar == 13) SchSearch();
    }

    private void txtNationalCode_KeyPress(object sender, KeyPressEventArgs e)
    {
        _IsSchSearch = true;
        if (e.KeyChar == 13) SchSearch();
    }

    private void txtMobile_KeyPress(object sender, KeyPressEventArgs e)
    {
        _IsSchSearch = true;
        if (e.KeyChar == 13) SchSearch();
    }

    private void grvSch_DoubleClick(object sender, EventArgs e)
    {
        if (grvSch.FocusedRowHandle < 0) return;

        DataRow rowSelect;

        rowSelect = grvSch.GetDataRow(grvSch.FocusedRowHandle);
        var rowSelectgrv = grvSch.GetFocusedDataSourceRowIndex();

        _RecNo = Convert.ToInt32(rowSelect["RecNo"]);
        _SchID = Convert.ToInt32(rowSelect["SchID"]);
        _EncID = Convert.ToInt32(rowSelect["EncID"]);
        _IsConfirm = Convert.ToBoolean(rowSelect["IsConfirm"]);

        var sui = new SUI();

        if (_EncID > 0)
        {
            sui.ShowWarningMessageForm("پیام", "مراجعه کننده دارای پذیرش است", true);
            return;
        }


        var frm = new FrmSchPatient
        {
            _SchDefID = _SchDefID,
            _RecNo = _RecNo,
            _SchID = _SchID
        };

        var result = frm.ShowDialog();


        if (result == DialogResult.Yes)
        {
            SchLoad();
            grvSch.SelectRow(rowSelectgrv);
            grvSch.FocusedRowHandle = rowSelectgrv;
        }
    }

    private void grvSch_RowCellStyle(object sender, RowCellStyleEventArgs e)
    {
        var view = sender as GridView;

        var RecNo = Convert.ToInt32(view.GetRowCellValue(e.RowHandle, "RecNo"));

        if (!(view.GetRowCellValue(e.RowHandle, "Emergency") is DBNull))
        {
            var Emr = Convert.ToBoolean(view.GetRowCellValue(e.RowHandle, "Emergency"));

            if (Emr is true) e.Appearance.BackColor = Color.DarkOrange;
        }

        if (!(view.GetRowCellValue(e.RowHandle, "Priority") is DBNull))
        {
            var Pri = Convert.ToBoolean(view.GetRowCellValue(e.RowHandle, "Priority"));

            if (Pri is true) e.Appearance.BackColor = Color.LimeGreen;
        }

        if (RecNo == -1) e.Appearance.BackColor = Color.PaleVioletRed;
    }

    private void lblSchCancel_Click(object sender, EventArgs e)
    {
        if (grvSch.FocusedRowHandle < 0) return;

        var rowSelect = grvSch.GetDataRow(grvSch.FocusedRowHandle);

        _SchID = Convert.ToInt32(rowSelect["SchID"]);
        _EncID = Convert.ToInt32(rowSelect["EncID"]);
        _IsConfirm = Convert.ToBoolean(rowSelect["IsConfirm"]);

        var sui = new SUI();
        if (_EncID > 0)
        {
            sui.ShowWarningMessageForm("پیام", "مراجعه کننده دارای پذیرش است", true);
            return;
        }

        if (_IsConfirm)
        {
            sui.ShowWarningMessageForm("پیام", "وقت مراجعه کننده تائید شده است", true);
            return;
        }

        if (_SchID == 0) return;

        var FullName = rowSelect["FirstName"] + " " + rowSelect["LastName"];


        var result = sui.FormShowMessage("لغو وقت", "وقت << " + FullName + " >> لغو گردد؟", "بله", "خیر");

        if (result == DialogResult.Yes)
        {
            _bllSch.SchCancel(_SchID);
            SchLoad();
            var rowSelectgrv = grvSch.GetFocusedDataSourceRowIndex();
            grvSch.SelectRow(rowSelectgrv);
            grvSch.FocusedRowHandle = rowSelectgrv;
        }
    }

    private void batBtnSchList_ItemClick(object sender, ItemClickEventArgs e)
    {
        var tsqlvSch =
            @"select * from vSch
                where SchDefID=@SchDefID
                AND RecNo>0
                Order By RecNo";

        tsqlvSch = tsqlvSch.Replace("@SchDefID", _SchDefID.ToString());

        var tsqlvSchDef =
            @"select * from vSchDef";


        var tsqlRptProperty =
            @"
               SELECT
               SamanHIS.dbo.SCurDateTime() AS PrintDate
               ,@UserPersName AS ReportUserName ";

        tsqlRptProperty = tsqlRptProperty.Replace("@UserPersName", "'" + GL.UserPersName + "'");

        var report = _bllReport.RepRptFileLoad(100018);

        var sti = new StiSqlDatabase("SamanServer", cSet.cnStringA);
        report.Dictionary.Databases[0] = sti;

        ////var t = report.Dictionary.DataSources.Items;

        ((StiSqlSource)report.Dictionary.DataSources["vSch"]).SqlCommand = tsqlvSch;
        ((StiSqlSource)report.Dictionary.DataSources["vSchDef"]).SqlCommand = tsqlvSchDef;
        ((StiSqlSource)report.Dictionary.DataSources["RptProperty"]).SqlCommand = tsqlRptProperty;


        report.Show();
    }

    private void batBtnSchListIsConfirm_ItemClick(object sender, ItemClickEventArgs e)
    {
        var tsqlvSch =
            @"select * from vSch
                where SchDefID=@SchDefID
                AND RecNo>0 AND IsConfirm = 1
                Order By RecNo";

        tsqlvSch = tsqlvSch.Replace("@SchDefID", _SchDefID.ToString());

        var tsqlvSchDef =
            @"select * from vSchDef";


        var tsqlRptProperty =
            @"
               SELECT
               SamanHIS.dbo.SCurDateTime() AS PrintDate
               ,@UserPersName AS ReportUserName ";

        tsqlRptProperty = tsqlRptProperty.Replace("@UserPersName", "'" + GL.UserPersName + "'");

        var report = _bllReport.RepRptFileLoad(100018);

        var sti = new StiSqlDatabase("SamanServer", cSet.cnStringA);
        report.Dictionary.Databases[0] = sti;

        ////var t = report.Dictionary.DataSources.Items;

        ((StiSqlSource)report.Dictionary.DataSources["vSch"]).SqlCommand = tsqlvSch;
        ((StiSqlSource)report.Dictionary.DataSources["vSchDef"]).SqlCommand = tsqlvSchDef;
        ((StiSqlSource)report.Dictionary.DataSources["RptProperty"]).SqlCommand = tsqlRptProperty;


        report.Show();
    }

    private void chkCancelShow_CheckedChanged(object sender, EventArgs e)
    {
        SchLoad();
    }

    private void grvSch_CustomRowCellEditForEditing(object sender, CustomRowCellEditEventArgs e)
    {
        _IsRecNoEdit = false;
        _IsSchTimeEdit = false;

        var rowSelect = grvSch.GetDataRow(grvSch.FocusedRowHandle);

        if (e.Column.Name == "gcRecNo")
        {
            _OldRecNo = Convert.ToInt32(rowSelect["RecNo"]);
            _OldSchID = Convert.ToInt32(rowSelect["SchID"]);

            if (_OldSchID == 0)
            {
                gcRecNo.OptionsColumn.AllowEdit = false;
                return;
            }

            e.RepositoryItem = repTxtRecNo;
        }
    }

    private void repChkConfirm_CheckedChanged(object sender, EventArgs e)
    {
        if (true) SchConfirm(false);
    }

    private void grvSch_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
    {
        if (grvSch.FocusedRowHandle < 0) return;

        var rowSelect = grvSch.GetDataRow(grvSch.FocusedRowHandle);
        _OldSchID = Convert.ToInt32(rowSelect["SchID"]);

        if (_OldSchID > 0)
            gcRecNo.OptionsColumn.AllowEdit = true;

        //repTxtRecNo.MaskSettings.MaskExpression
        else
            gcRecNo.OptionsColumn.AllowEdit = false;
    }

    private void grvSch_CellValueChanged(object sender, CellValueChangedEventArgs e)
    {
        if (e.Column.Name == "gcSchTime")
        {
            DataRow rowSelect;
            rowSelect = grvSch.GetDataRow(grvSch.FocusedRowHandle);
            var schID = Convert.ToInt32(rowSelect["SchID"]);

            if (schID > 0)
            {
                var drSchID =
                    _dtSch.Select("SchID = " + schID)
                        .FirstOrDefault(); // finds all rows with id==2 and selects first or null if haven't found any
                if (drSchID != null) drSchID["SchTime"] = rowSelect["Schtime"].ToString();
            }
        }

        if (e.Column.Name == "gcRecNo")
        {
            if (_OldSchID == 0) return;

            {
                DataRow rowSelect;
                rowSelect = grvSch.GetDataRow(grvSch.FocusedRowHandle);
                _NewRecNo = Convert.ToInt32(rowSelect["RecNo"]);

                if (_NewRecNo == _OldRecNo) return;

                //پیدا کردن شماره وقت ردیف جدید
                var drNewShID =
                    _dtSch.Select("RecNo = " + _NewRecNo)
                        .FirstOrDefault(); // finds all rows with id==2 and selects first or null if haven't found any
                if (drNewShID != null) _NewSchID = Convert.ToInt32(drNewShID["SchID"]);

                //انتقال به ردیف خالی
                if (_NewSchID == 0)
                {
                    var dr2 = _dtSch.Select("SchID = " + _OldSchID)
                        .FirstOrDefault(); // finds all rows with id==2 and selects first or null if haven't found any
                    if (dr2 != null) dr2["RecNo"] = _NewRecNo;
                }

                //--------------------------------------------------------------------
                //جابجا کردن ردیفهای بیشتر از یک پرشده-ردیف جدید بزگرتر از قبلی
                if (_NewSchID > 0 && _OldRecNo > 1 && _NewRecNo > _OldRecNo)
                {
                    for (var i = _NewRecNo; i > _OldRecNo; i--)
                    {
                        var
                            dr = _dtSch.Select("RecNo = " + i)
                                .FirstOrDefault(); // finds all rows with id==2 and selects first or null if haven't found any
                        if (dr != null) dr["RecNo"] = i - 1;
                    }


                    var dr2 = _dtSch.Select("SchID = " + _OldSchID)
                        .FirstOrDefault(); // finds all rows with id==2 and selects first or null if haven't found any
                    if (dr2 != null) dr2["RecNo"] = _NewRecNo;
                }


                //--------------------------------------------------------------------
                //جابجا کردن ردیفهای بیشتر از یک پرشده-ردیف جدید بزگرتر از قبلی و خالی است
                if (_NewSchID == 0 && _OldRecNo > 1 && _NewRecNo > _OldRecNo)
                {
                    for (var i = _OldRecNo + 1; i < _NewRecNo; i++)
                    {
                        var
                            dr = _dtSch.Select("RecNo = " + i)
                                .FirstOrDefault(); // finds all rows with id==2 and selects first or null if haven't found any
                        if (dr != null) dr["RecNo"] = i - 1;
                    }


                    var dr2 = _dtSch.Select("SchID = " + _OldSchID)
                        .FirstOrDefault(); // finds all rows with id==2 and selects first or null if haven't found any
                    if (dr2 != null) dr2["RecNo"] = _NewRecNo;
                }

                //--------------------------------------------------------------------
                //جابجا کردن ردیفهای بیشتر از یک پرشده-ردیف جدید کمتر از قبلی
                if (_NewSchID > 0 && _OldRecNo > 1 && _NewRecNo < _OldRecNo)
                {
                    for (var i = _OldRecNo - 1; i >= _NewRecNo; i--)
                    {
                        var
                            dr = _dtSch.Select("RecNo = " + i)
                                .FirstOrDefault(); // finds all rows with id==2 and selects first or null if haven't found any
                        if (dr != null) dr["RecNo"] = i + 1;
                    }


                    var dr2 = _dtSch.Select("SchID = " + _OldSchID)
                        .FirstOrDefault(); // finds all rows with id==2 and selects first or null if haven't found any
                    if (dr2 != null) dr2["RecNo"] = _NewRecNo;
                }

                //--------------------------------------------------------------------
                //جابجا کردن اولین ردیف
                if (_NewSchID > 0 && _OldRecNo == 1)
                {
                    for (var i = _NewRecNo; i >= _OldRecNo; i--)
                    {
                        var
                            dr = _dtSch.Select("RecNo = " + i)
                                .FirstOrDefault(); // finds all rows with id==2 and selects first or null if haven't found any
                        if (dr != null) dr["RecNo"] = i - 1;
                    }


                    var dr2 = _dtSch.Select("SchID = " + _OldSchID)
                        .FirstOrDefault(); // finds all rows with id==2 and selects first or null if haven't found any
                    if (dr2 != null) dr2["RecNo"] = _NewRecNo;
                }

                ////--------------------------------------------------------------------
                //جابجا کردن آخرین ردیف
                if (_NewSchID > 0 && _OldRecNo == 1)
                {
                    for (var i = _NewRecNo; i >= _OldRecNo; i--)
                    {
                        var
                            dr = _dtSch.Select("RecNo = " + i)
                                .FirstOrDefault(); // finds all rows with id==2 and selects first or null if haven't found any
                        if (dr != null) dr["RecNo"] = i - 1;
                    }


                    var dr2 = _dtSch.Select("SchID = " + _OldSchID)
                        .FirstOrDefault(); // finds all rows with id==2 and selects first or null if haven't found any
                    if (dr2 != null) dr2["RecNo"] = _NewRecNo;
                }
            }
        }

        SchDataTableSave();
        //grdSch.DataSource = _dtSch.Select("RecNo > 0").CopyToDataTable();

        var rowSelectgrv = _NewRecNo - 1;
        grvSch.SelectRow(rowSelectgrv);
        grvSch.FocusedRowHandle = rowSelectgrv;
    }

    private void tabmain_SelectedPageChanged(object sender, TabPageChangedEventArgs e)
    {
        var tabName = tabmain.SelectedTabPage.Name;
        if (tabName == "tabSchDef") panSchToolbar.Visible = false;

        if (tabName == "tabSch") panSchToolbar.Visible = true;
    }

    private void repTxtRecNo_EditValueChanging(object sender, ChangingEventArgs e)
    {
        if (e.NewValue.ToString().Length == 0) return;

        var newVal = Convert.ToInt32(e.NewValue);

        if (newVal >= _SchDefTotalCapacity) e.NewValue = _OldRecNo;
    }

    private void SchDataTableSave()
    {
        var c = _dtSch.Rows.Count;

        int schID = 0, recNo = 0;
        var schTime = "00:00";

        for (var i = 0; i < c; i++)
        {
            schID = Convert.ToInt32(_dtSch.Rows[i]["SchID"]);
            recNo = Convert.ToInt32(_dtSch.Rows[i]["RecNo"]);
            schTime = _dtSch.Rows[i]["SchTime"].ToString();

            if (schID > 0) _bllSch.SchGridSave(schID, recNo, schTime);
        }

        SchLoad();
    }

    private void grvSch_RowCellClick(object sender, RowCellClickEventArgs e)
    {
        if (e.Column.Name == "gcEncID")
        {
            if (_ModID == 29)
                MessageBox.Show("امکان پذیرش در این ماژول وجود ندارد");
            else
                EncSch();
        }
    }

    private void lblSchCut_Click(object sender, EventArgs e)
    {
        if (grvSch.FocusedRowHandle < 0) return;

        var rowSelect = grvSch.GetDataRow(grvSch.FocusedRowHandle);
        var rowSelectgrv = grvSch.GetFocusedDataSourceRowIndex();

        _SchID = Convert.ToInt32(rowSelect["SchID"]);
        _EncID = Convert.ToInt32(rowSelect["EncID"]);
        _IsConfirm = Convert.ToBoolean(rowSelect["IsConfirm"]);

        var sui = new SUI();
        if (_EncID > 0)
        {
            sui.ShowWarningMessageForm("پیام", "مراجعه کننده دارای پذیرش است", true);
            return;
        }

        if (_IsConfirm)
        {
            sui.ShowWarningMessageForm("پیام", "وقت مورد نظر تائید شده است", true);
            return;
        }

        _IsSchCut = true;
        _IsSchCopy = false;
        lblSchPaste.Enabled = true;

        grvSch.SelectRow(rowSelectgrv);
        grvSch.FocusedRowHandle = rowSelectgrv;
    }

    private void lblSchCopy_Click(object sender, EventArgs e)
    {
        if (grvSch.FocusedRowHandle < 0) return;

        var rowSelect = grvSch.GetDataRow(grvSch.FocusedRowHandle);
        var rowSelectgrv = grvSch.GetFocusedDataSourceRowIndex();

        _SchID = Convert.ToInt32(rowSelect["SchID"]);

        _IsSchCut = false;
        _IsSchCopy = true;
        lblSchPaste.Enabled = true;

        grvSch.SelectRow(rowSelectgrv);
        grvSch.FocusedRowHandle = rowSelectgrv;
    }


    private void lblSchPaste_Click(object sender, EventArgs e)
    {
        if (grvSch.FocusedRowHandle < 0) return;

        var rowSelect = grvSch.GetDataRow(grvSch.FocusedRowHandle);
        var rowSelectgrv = grvSch.GetFocusedDataSourceRowIndex();


        _RecNo = Convert.ToInt32(rowSelect["RecNo"]);
        //_SchID = Convert.ToInt32(rowSelect["SchID"]);
        _EncID = Convert.ToInt32(rowSelect["EncID"]);
        _IsConfirm = Convert.ToBoolean(rowSelect["IsConfirm"]);

        var sui = new SUI();
        if (_EncID > 0)
        {
            sui.ShowWarningMessageForm("پیام", "مراجعه کننده دارای پذیرش است", true);
            return;
        }

        _bllSch.SchRecPaste(-1, _SchDefID, _SchID, _RecNo, _IsSchCut, _IsSchCopy);

        _IsSchCut = false;
        _IsSchCopy = false;
        lblSchPaste.Enabled = false;

        SchLoad();
        grvSch.SelectRow(rowSelectgrv);
        grvSch.FocusedRowHandle = rowSelectgrv;
    }
}