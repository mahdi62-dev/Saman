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
    public partial class UcTimexPersDep : XtraUserControl
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

        public UcTimexPersDep()
        {
            InitializeComponent();
        }

        private void UcTimexPersDep_Load(object sender, EventArgs e)
        {

            ShortKey();
            CheckPermission();
            UL.FillLookup(lueDep, "0", cSet.cnStringA, false, "DepID,Name", "vDep", "Dep", null, null);

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
                    grvTimexShiftPers.Columns[c].AppearanceHeader.BackColor = Color.PaleVioletRed;
                    grvTimexShiftPers.Columns[c].AppearanceCell.BackColor = Color.PaleVioletRed;
                    grvTimexShiftPers.Columns[c].AppearanceCell.ForeColor = Color.White;

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

    }
}