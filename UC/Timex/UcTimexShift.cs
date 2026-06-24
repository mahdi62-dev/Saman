using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using DAL;
using DevExpress.Data;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
using LIB;
using Saman.BLL.Calendar;
using Saman.BLL.Personnel;
using Saman.BLL.SAC;
using Saman.BLL.Timex;

namespace Saman.UC
{
    public partial class UcTimexShift : XtraUserControl
    {
        public int _Year { get; set; }
        public int _Mon { get; set; }
        public int _IsSign { get; set; }
        public byte _ModID { get; set; }
        public int _PersID { get; set; }


        private DataTable _dtEmpAction;
        private DataTable _PersonnnelLoadList;
        private DataRow _rowPersSelect;
        private readonly BLL.Personnel.Personnel bllPersonnel = new BLL.Personnel.Personnel();

        public UcTimexShift()
        {
            InitializeComponent();
        }

        private void UcTimexShift_Load(object sender, EventArgs e)
        {

            ShortKey();
            CheckPermission();
            TreeLoadData();
            UL.FillLookup(lueDep, "1", cSet.cnStringA, false, "DepID,Name", "vDep", "Dep", null, null);

            GridHeaderLoad();
        }

        private void GridHeaderLoad()
        {
            var bllCalendar = new BLL.Calendar.Calendar();
            var dtCalendar = bllCalendar.CalendarData(1, _Year, _Mon);

            int c = 0, WeekDay, IsHoliday;
            string weekDayName, day , header;


            for (int i = 0; i < dtCalendar.Rows.Count; i++)
            {
                c = i + 3;

                weekDayName =  dtCalendar.Rows[i]["WeekDayName"].ToString();
                WeekDay =  Convert.ToInt32(dtCalendar.Rows[i]["WeekDay"]);
                IsHoliday =  Convert.ToInt32(dtCalendar.Rows[i]["IsHoliday"]);

                day =  dtCalendar.Rows[i]["day"].ToString();
                header = weekDayName + "\r\n" + day;
                grvTimexShiftPers.Columns[c].Caption = header;
                grvTimexShiftPers.Columns[c].Visible = true;

                if (WeekDay == 6 || IsHoliday == 1)
                {
                    //grvTimexShiftPers.Columns[c].AppearanceCell.BackColor = Color.Red;
                    grvTimexShiftPers.Columns[c].AppearanceHeader.BackColor = Color.PaleVioletRed;
                }
                    
            }

        }
        private void ShortKey()
        {
            //msiEditEmpAction.Click += btnEditEmpAction_Click;
            //msiNewEmpAction.Click += btnNewEmpAction_Click;

            //panMain.Focus();
        }
        private void CheckPermission()
        {
            //msiEditEmpAction.Click += btnEditEmpAction_Click;
            //msiNewEmpAction.Click += btnNewEmpAction_Click;

            //panMain.Focus();
        }
        private void TreeLoadData()
        {
            BLL.Timex.Timex bllDepAndPers = new Timex();

            DataTable dtTree = new DataTable();
            dtTree = bllDepAndPers.DepAndPersLoad();

            treListPers.KeyFieldName = "ActionID";
            treListPers.ParentFieldName = "ParentID";

            treListPers.DataSource = dtTree;
            treListPers.RowHeight = 23;

            if (dtTree.Rows.Count > 0)
            {
                treListPers.Nodes.FirstNode.Expanded = true;
                treListPers.Nodes.TreeList.ExpandAll();
            }
        }

    }
}