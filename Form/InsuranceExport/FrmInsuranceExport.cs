using System;
using System.Windows.Forms;
using Saman.UC;


namespace Saman.Form.FrmInsuranceExport
{
    public partial class FrmInsuranceExport : DevExpress.XtraEditors.XtraForm
    {
        public byte _ModID = 97;
        private string _FormName = "دیسکت بیمه";

        APP.SAC.SACPermission sacPermission = new APP.SAC.SACPermission();

        public FrmInsuranceExport()
        {
            InitializeComponent();
        }
        private void FrmInsuranceExport_Load(object sender, EventArgs e)
        {
            barBtnStatus();
         //   CheckPermission();

            if (barBtnInsurExport.Enabled != false)
            {
                var ucControl = new UcInsurExport();
                ShowUC(ucControl);

                this.ActiveControl = ucControl;

            }
            else
            {
                var ucControl = new UcDefult();
                ShowUC(ucControl);

                this.ActiveControl = ucControl;
            }

        }
        private void CheckPermission()
        {
            barBtnInsurExport.Enabled = sacPermission.CheckPermission(_ModID, 970001);
            barBtnInsurExportSetting.Enabled = sacPermission.CheckPermission(_ModID, 970002);
        }
        private void barBtnStatus()
        {
            barBtnInsurExport.Enabled = true;
            barBtnInsurExportSetting.Enabled = true;

        }
        private void ShowUC(object ucControl)
        {
            panUC.Controls.Clear();
            var myUC = (UserControl)ucControl;
            myUC.Anchor = AnchorStyles.Top;
            myUC.Anchor = AnchorStyles.Right;
            myUC.Dock = DockStyle.Fill;

            this.Text = _FormName + " - " + myUC.Tag.ToString();
            panUC.Controls.Add(myUC);
        }


        private void barBtnInsurExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var ucControl = new UcInsurExport();
            ShowUC(ucControl);

            this.ActiveControl = ucControl;

        }
    }
}