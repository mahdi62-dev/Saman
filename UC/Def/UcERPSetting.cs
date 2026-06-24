using System;
using System.Drawing;
using System.Data;
using System.Data.SqlClient;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Windows.Forms;
using DevExpress.Data;
using DevExpress.XtraGrid.Columns;
using Saman.BLL.Report;
using DevExpress.Utils;
using DevExpress.Utils.Drawing;
using DevExpress.Utils.ScrollAnnotations;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting;
using DevExpress.XtraGrid;

namespace Saman.UC
{
    public partial class UcERPSetting : DevExpress.XtraEditors.XtraUserControl
    {
        private int _RepID;
        private string _RepName;
        private string _RepFullName;
        private string _RepTitle;
        private string _StartDate;
        private string _EndDate;
        private string _Date1;
        private string _Time1;
        private string _Date2;
        private string _Time2;



        BLL.Report.Report _bllReport = new BLL.Report.Report();
        DataTable _dtRep = new DataTable();
        DataTable _dtRepFilter = new DataTable();
        DataTable _dtRepResult = new DataTable();

        private string _TSQL;

        private string _PaperSize;
        private bool _IsLandscape;

        private string _RepFilter;
        public int _Year { get; set; }
        public int _Mon { get; set; }
        public string _MonName { get; set; }


        public UcERPSetting()
        {
            InitializeComponent();
        }

        private void UcERPSetting_Load(object sender, EventArgs e)
        {
            //_RepID = 2;

            TreeLoadData();

        }

        private void TreeLoadData()
        {
            DataTable dtTree = new DataTable();
            var tsql = @"
SELECT r.*
  FROM SamanReport.dbo.vReport r
inner JOIN dbo.SACUserPermissionReport sacr ON sacr.RepID= r.RepID
inner JOIN 

(	

SELECT sacr.PersID, r.RepID
	  FROM SamanReport.dbo.vReport r
	inner JOIN dbo.SACUserPermissionReport sacr ON sacr.RepID= r.RepID
GROUP BY sacr.PersID, r.RepID
UNION ALL
SELECT @PersID,0

) p ON p.RepID=r.ParentID AND p.PersID=sacr.PersID
WHERE sacr.PersID=@PersID
";

            var com = new SqlCommand();
            com.Parameters.AddWithValue("@PersID", GL.UserPersID);

            var dalData = new DAL.Data();
            dtTree = dalData.GetDataTableByText(DAL.cSet.cnStringA, com, tsql);







            treReport.KeyFieldName = "RepID";
            treReport.ParentFieldName = "ParentID";

            treReport.DataSource = dtTree;
            treReport.RowHeight = 23;

            if (dtTree.Rows.Count > 0)
            {
                treReport.Nodes.FirstNode.Expanded = true;
                treReport.Nodes.TreeList.ExpandAll();
            }

        }

        private void SetFilterControls()
        {
            string ControlName;

            foreach (DataRow row in _dtRepFilter.Rows)
            {
                ControlName = row["ControlName"].ToString();

                if (ControlName == "e.StartDate") panStartDate.Visible = true;
                if (ControlName == "e.EndDate") panEndDate.Visible = true;
                if (ControlName == "T.YearMon")
                {
                    panYearMon.Visible = true;

                    var curDateTime = App.SDate.ShamsiDate();
                    string year, mon;

                    year = (curDateTime.Substring(0, 4));
                    mon = (curDateTime.Substring(5, 2));

                    LIB.UL.FillLookup(lueYear, year, DAL.cSet.cnStringA, false, "ID,Name", "vADR", "Year",
                        "ADName='Year'", null);
                    LIB.UL.FillLookup(lueMon, mon, DAL.cSet.cnStringA, false, "ID,Name", "vADR", "mon",
                        "ADName='Month'", null);

                    _Year = Convert.ToInt32(lueYear.EditValue);
                    _Mon = Convert.ToInt32(lueMon.EditValue);
                }

                if (ControlName == "T.YearAllMon")
                {
                    panYearAllMon.Visible = true;

                    var curDateTime = App.SDate.ShamsiDate();
                    string year, mon;

                    year = (curDateTime.Substring(0, 4));
                    mon = (curDateTime.Substring(5, 2));

                    LIB.UL.FillLookup(lueYear1, year, DAL.cSet.cnStringA, false, "ID,Name", "vADR", "Year",
                        "ADName='Year'", null);
                    LIB.UL.FillLookup(lueMon1, mon, DAL.cSet.cnStringA, false, "ID,Name", "vADR", "mon",
                        "ADName='Month'", null);

                    _Year = Convert.ToInt32(lueYear1.EditValue);
                    _Mon = Convert.ToInt32(lueMon1.EditValue);
                }



            }

        }
        private void lueMon_EditValueChanged(object sender, EventArgs e)
        {
            _Mon = Convert.ToInt32(lueMon.EditValue);
            _MonName = lueMon.Text +" "+ "ماه";
        }

        private void lueYear_EditValueChanged(object sender, EventArgs e)
        {
            _Year = Convert.ToInt32(lueYear.EditValue);
        }

        private void GetFilterControls()
        {
            _RepFilter = null;

            if (panStartDate.Visible)
            {
                _RepFilter += " AND e.StartDate BETWEEN '" + datStartDate1.Text + "' AND '" + datStartDate2.Text + "'";

                _Date1 = (datStartDate1.Text.Substring(0, 10));
                _Time1 = (datStartDate1.Text.Substring(11, 5));

                _Date2 = (datStartDate2.Text.Substring(0, 10));
                _Time2 = (datStartDate2.Text.Substring(11, 5));

                _StartDate = "تاریخ پذیرش از :" + _Time1 + " " + _Date1 + " تا " + _Time2 + " " + _Date2;
            }

            if (panEndDate.Visible)
            {
                _RepFilter += " AND e.EndDate BETWEEN '" + datEndDate1.Text + "' AND '" + datEndDate2.Text + "'";

                _Date1 = (datEndDate1.Text.Substring(0, 10));
                _Time1 = (datEndDate1.Text.Substring(11, 5));

                _Date2 = (datEndDate2.Text.Substring(0, 10));
                _Time2 = (datEndDate2.Text.Substring(11, 5));

                _EndDate = "تاریخ ترخیص از :" + _Time1 + " " + _Date1 + " تا " + _Time2 + " " + _Date2;

            }

            if (panYearMon.Visible)
                _RepFilter += " AND T.Year = " + _Year + " AND T.Mon = " + _Mon;

            if (panYearAllMon.Visible)
                _RepFilter += " AND T.Year = " + _Year + " AND T.Mon <= " + _Mon;
        }

        private void SetGridFormat()
        {
            foreach(GridColumn col in grvRepRun.Columns)
            {
                col.DisplayFormat.FormatType = FormatType.Numeric;
                col.DisplayFormat.FormatString = "n0";
            }
            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

          
        }
        private void grvRepRun_PrintInitialize(object sender, DevExpress.XtraGrid.Views.Base.PrintInitializeEventArgs e)
        {
            PrintingSystemBase pb = e.PrintingSystem as PrintingSystemBase;
            DevExpress.XtraPrinting.PrintableComponentLink printableComponentLink = new DevExpress.XtraPrinting.PrintableComponentLink();
            pb.PageSettings.Landscape = _IsLandscape;
            pb.PageSettings.PaperKind = PaperKind.A4;
            pb.Document.AutoFitToPagesWidth = 1;
            pb.PageSettings.TopMargin = 100;
            pb.PageSettings.BottomMargin = 100;
            pb.PageSettings.LeftMargin = 40;
            pb.PageSettings.RightMargin = 40;
            
            grvRepRun.OptionsPrint.PrintHeader = true;
            grvRepRun.OptionsPrint.PrintFooter = true;


            (e.Link.PageHeaderFooter as PageHeaderFooter).Header.Content.Clear();
            (e.Link.PageHeaderFooter as PageHeaderFooter).Footer.Content.Clear();

            (e.Link.PageHeaderFooter as PageHeaderFooter).Header.LineAlignment = BrickAlignment.Center;
            (e.Link.PageHeaderFooter as PageHeaderFooter).Footer.LineAlignment = BrickAlignment.Center;

            (e.Link.PageHeaderFooter as PageHeaderFooter).Header.Font = new Font("IRANSansFaNum", 9, FontStyle.Regular);
            (e.Link.PageHeaderFooter as PageHeaderFooter).Footer.Font = new Font("IRANSansFaNum", 8);

            (e.Link.PageHeaderFooter as PageHeaderFooter).Header.Content.AddRange(new string[] { _RepName + " " + _MonName, _RepTitle, _EndDate + _StartDate });
            (e.Link.PageHeaderFooter as PageHeaderFooter).Footer.Content.AddRange(new string[] { "[Date Printed]-[Time Printed] : تاریخ گزارش گیری  ", " [Pages #] از [Page #] : صفحه  ", "گزارش گیرنده : " + GL.UserPersName });


            #region ExportSetting

            if (chkPageHeaderRemove.Checked)
            {
                (e.Link.PageHeaderFooter as PageHeaderFooter).Header.Content.Clear();
                (e.Link.PageHeaderFooter as PageHeaderFooter).Footer.Content.Clear();
            }

            if (chkPageColNameRemove.Checked)
            {
                grvRepRun.OptionsPrint.PrintHeader = false;
            }

            if (chkPageColSummaryRemove.Checked)
            {
                grvRepRun.OptionsPrint.PrintFooter = false;
            }
            #endregion


     
        }

        private void btnRepPrint_Click(object sender, EventArgs e)
        {
           //grdRepRun.ShowPrintPreview();
           grvRepRun.ShowRibbonPrintPreview();

        }

        private void btnRepRun_Click(object sender, EventArgs e)
        {
            GetFilterControls();

            _TSQL = _dtRep.Rows[0]["TSQL"].ToString();
            _TSQL = _TSQL.Replace("--filter--", _RepFilter);



            _dtRepResult = _bllReport.RepRun(_TSQL);

            _IsLandscape = Convert.ToBoolean(_dtRep.Rows[0]["IsLandscape"]);
            _PaperSize = _dtRep.Rows[0]["PaperSize"].ToString();
            grdRepRun.DataSource = _dtRepResult;




            SetGridFormat();

            GridViewSummary();

           tab2.Show();
        }

        private void GridViewSummary()
        {
            int colCount = grvRepRun.Columns.Count;
            //grvRepRun.Columns[0].GroupIndex = 1;
            //grvRepRun.Columns[1].GroupIndex = 2;

            string colFieldName;


            for (int i = 0; i < colCount; i++)
            {
                colFieldName = grvRepRun.Columns[i].FieldName;

                grvRepRun.Columns[i].Summary.Clear();
                grvRepRun.Columns[i].Summary.Add(DevExpress.Data.SummaryItemType.Sum, colFieldName, "{0:n0}");


                if (colFieldName.Contains("میانگین"))
                {
                    grvRepRun.Columns[i].Summary.Clear();
                    grvRepRun.Columns[i].Summary.Add(DevExpress.Data.SummaryItemType.Average, colFieldName, "میانگین {0:F0}");
                    //grvRepRun.Columns[i].SummaryItem.
                }

                if (colFieldName.Contains("تعداد"))
                {
                    grvRepRun.Columns[i].Summary.Clear();
                    grvRepRun.Columns[i].Summary.Add(DevExpress.Data.SummaryItemType.Sum, colFieldName, "{0:n0}");
                    grvRepRun.Columns[i].Summary.View.Appearance.UpdateRightToLeft(false);
                }
               
            }
            

            grvRepRun.BestFitColumns();
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            tab1.Show();
        }


        private void panYearMon_Paint(object sender, PaintEventArgs e)
        {
            App.Control.LookupEditLine(lueMon, e);
            App.Control.LookupEditLine(lueYear, e);
        }

        private void treReport_DoubleClick(object sender, EventArgs e)
        {
            DataRowView rowView = (DataRowView)treReport.GetRow(treReport.FocusedNode.Id);
            DataRow row = rowView.Row;

            int RepType = Convert.ToInt32(row["RepType"]);

            string RepName = null;

            if (RepType > 0)
                 _RepName = row["Name"].ToString();
                 _RepFullName = row["FullName"].ToString();
                 _RepTitle = row["CenterName"].ToString();
            
                _RepID = Convert.ToInt32(row["RepID"].ToString());
                 lblTitle1.Text = _RepFullName;

        
            ReportLoad();
        }

        private void ReportLoad()
        {
            FormNull();
            tab1.Show();

            _dtRep = _bllReport.RepLoad(_RepID);
            _dtRepFilter = _bllReport.RepFilterLoad(_RepID);
            SetFilterControls();

        }

        private void FormNull()
        {
            panStartDate.Visible = false;
            panEndDate.Visible = false;
            panYearMon.Visible = false;
            panYearAllMon.Visible = false;

            grdRepRun.DataSource = null;
            _MonName = null;
            grvRepRun.Columns.Clear();
         
        }

        private void grvRepRun_CustomDrawFooter(object sender, DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventArgs e)
        {
        
        }

        private void grvRepRun_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView view = sender as GridView;
            e.Column.BestFit();
        }

        private void grvRepRun_CustomDrawFooterCell(object sender, FooterCellCustomDrawEventArgs e)
        {
            e.Bounds.Inflate(-1, -1);
            e.Appearance.TextOptions.HAlignment = HorzAlignment.Near;
            e.DefaultDraw();
            e.Cache.DrawRectangle(e.Cache.GetPen(Color.CornflowerBlue, 1), e.Bounds);
            e.Handled = true;

        }
      
    }
}
