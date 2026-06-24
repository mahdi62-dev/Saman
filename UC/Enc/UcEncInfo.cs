using System;
using System.Data;
using System.Windows.Forms;
using App;
using DevExpress.Data;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using Saman.BLL.Enc;
using Saman.Form.Ward;

namespace Saman.UC.Enc;

public partial class UcEncInfo : XtraUserControl
{
    private readonly EncInfo _bllEncInfo = new();
    private int _curInfoStatus;
    private DataTable _dtEncList;


    private int _EncID;
    public int _EncStatus;
    private GridColumn column;

    private int rowHandle;

    public UcEncInfo()
    {
        InitializeComponent();
    }

    public int _ModID { get; set; }
    public int _DepID { get; set; }
    public bool _ReadOnly { get; set; }

    public byte _EncType { get; set; }

    private void UcEncStatus_Load(object sender, EventArgs e)
    {
        var appSDate = new SDate();

        datDate2.DateTime = datDate1.DateTime = DateTime.Parse(appSDate.ShamsiDate());
        PatientSearch();
    }

    private void PatientSearch()
    {
        var bllEncInfoStatus = new EncInfo();
        _dtEncList = bllEncInfoStatus.GetEncInfo(datDate1.Text + " 00:00", datDate2.Text + " 23:59");
        grdEncInfo.DataSource = _dtEncList;
        var col = grvEncInfo.Columns.ColumnByFieldName("RecNo");
        // col.FieldName = "RecNo";
        col.Visible = true;
        col.UnboundType = UnboundColumnType.Integer;
        grvEncInfo.CustomUnboundColumnData += gridView1_CustomUnboundColumnData;

        //var ind = grdEncInfo.EmbeddedNavigator.Buttons.CustomButtons.Name(Print);
        ////   ind.ImageIndex = Bottom ;
    }

    private void gridView1_CustomUnboundColumnData(object sender, CustomColumnDataEventArgs e)
    {
        if (e.IsGetData) e.Value = e.ListSourceRowIndex + 1;
    }

    private void grvEncInfo_ShowGridMenu(object sender, GridMenuEventArgs e)
    {
        PopMnuInfoStatusNull();


        var view = sender as GridView;
        var hitInfo = view.CalcHitInfo(e.Point);

        if (hitInfo.InRowCell)
        {
            view.FocusedRowHandle = rowHandle = hitInfo.RowHandle;
            column = hitInfo.Column;
            var colName = column.FieldName;


            var row = grvEncInfo.GetDataRow(rowHandle);
            _EncID = Convert.ToInt32(row["EncID"]);
            _EncStatus = Convert.ToInt32(row["EncStatus"]);

            _curInfoStatus = Convert.ToInt32(row["InfoStatus"]);


            if (colName == "InfoStatusName")
            {
                PopMnuInfoStatusNull();

                if (_ModID != 7 && _ModID != 8) return;

                popMenuInfoStatus.ShowPopup(barManager1, view.GridControl.PointToScreen(e.Point));
            }


            //if (colName == "EncID")
            //    popupMenu2.ShowPopup(barManager1, view.GridControl.PointToScreen(e.Point));
        }
    }

    private void PopMnuInfoStatusNull()
    {
        if ((_ModID == 7 || _ModID == 8) && _curInfoStatus == 200) //بخش با وضعیت ارسال به بخش
        {
            barMnuInfoSendOpr.Visibility = BarItemVisibility.Never;
            barMnuInfoSendDis.Visibility = BarItemVisibility.Never;
            barMnuInfoWardEnter.Visibility = BarItemVisibility.Always;
        }

        if ((_ModID == 7 || _ModID == 8) && _curInfoStatus == 201) //بحش با وضعیت حضور در بخش
        {
            barMnuInfoWardEnter.Visibility = BarItemVisibility.Never;
            barMnuInfoSendOpr.Visibility = BarItemVisibility.Always;
            barMnuInfoSendDis.Visibility = BarItemVisibility.Always;
        }

        if (_ModID == 3 && _curInfoStatus == 300) //اتاق عمل با وضعیت ارسال به اتاق عمل
        {
            barMnuInfoSendOpr.Visibility = BarItemVisibility.Never;
            barMnuInfoWardEnter.Visibility = BarItemVisibility.Never;
            barMnuInfoSendWard.Visibility = BarItemVisibility.Always;
        }
    }

    private void gridSearch_MouseClick(object sender, MouseEventArgs e)
    {
        var focusedColumn = grvEncInfo.FocusedColumn.Name;

        if (e.Button == MouseButtons.Right)
        {
            //popupMenu1.ShowPopup(new Point(e.X, e.Y));

            //ContextMenu contextmenu = new ContextMenu();
            //contextmenu.RightToLeft = RightToLeft.Yes;
            //contextmenu.MenuItems.Add(new MenuItem("Cut"));
            //contextmenu.MenuItems.Add(new MenuItem("Copy"));
            //contextmenu.MenuItems.Add(new MenuItem("Paste"));

            //int currentMouseOverRow =popupMenu1.HitTest(e.X, e.Y).RowIndex;

            //if (currentMouseOverRow >= 0)
            //{
            //    contextmenu.MenuItems.Add(new MenuItem(string.Format("Do something to row {0}", currentMouseOverRow.ToString())));
            //}

            //contextmenu.Show((grdSearch, new Point(e.X, e.Y));
        }
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        var panUC = Parent as PanelControl;
        panUC.Controls.Clear();

        var ucEncCur = new UcEncCur
            { Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom, Dock = DockStyle.Fill };
        panUC.Controls.Add(ucEncCur);
    }

    private void datDate1_EditValueChanged(object sender, EventArgs e)
    {
        PatientSearch();
    }

    private void datDate2_EditValueChanged(object sender, EventArgs e)
    {
        PatientSearch();
    }

    private void grdSearch_DoubleClick(object sender, EventArgs e)
    {
    }

    private void grvSearch_DoubleClick(object sender, EventArgs e)
    {
        if (grvEncInfo.FocusedRowHandle < 0) return;

        var focusedColumn = grvEncInfo.FocusedColumn.Name;

        var row = grvEncInfo.GetDataRow(grvEncInfo.FocusedRowHandle);
        var encID = Convert.ToInt32(row["EncID"]);
        _EncStatus = Convert.ToInt32(row["EncStatus"]);

        //lueService.EditValue = row["ServiceID"];
        var fullName = row["FullName"].ToString();
        var infoStatus = Convert.ToInt32(row["InfoStatus"]);


        if (focusedColumn == "grcEncInfoStatus")
        {
            var frmEncInfo = new FrmEncInfo
                { _EncID = encID, _FullName = fullName, _InfoStatus = infoStatus, _EncStatus = _EncStatus };
            frmEncInfo.ShowDialog();
        }

        PatientSearch();
        //string a = grvSearch.ro
    }


    private void barMnuInfoWardEnter_ItemClick(object sender, ItemClickEventArgs e)
    {
        _bllEncInfo.EncInfoSave(_EncID, 201);
        PatientSearch();
    }

    private void barMnuInfoSendOpr_ItemClick(object sender, ItemClickEventArgs e)
    {
        _bllEncInfo.EncInfoSave(_EncID, 300);
        PatientSearch();
    }

    private void barMnuInfoSendWard_ItemClick(object sender, ItemClickEventArgs e)
    {
        _bllEncInfo.EncInfoSave(_EncID, 200);
        PatientSearch();
    }

    private void barMnuInfoSendDis_ItemClick(object sender, ItemClickEventArgs e)
    {
        _bllEncInfo.EncInfoSave(_EncID, 400);
        PatientSearch();
    }
}