using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Saman.Form.RegService;
using Saman.UC;
using Saman.UC.Enc;



namespace Saman.Form.DrugManager

{
    public partial class FrmDrugManageMain : DevExpress.XtraEditors.XtraForm
    {
        public byte _Mod = 5;
        public FrmDrugManageMain()
        {
            InitializeComponent();
        }

        private void FrmDrugManageMain_Load(object sender, EventArgs e)
        {
            panUC.Controls.Clear();
            var ucControl = new UcDefult() { Anchor = (AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom), Dock = DockStyle.Fill };
            panUC.Controls.Add(ucControl);
        }

       private void ShowUC(object ucControl)
        {
            panUC.Controls.Clear();
            var myUC = (UserControl)ucControl;
            myUC.Anchor = AnchorStyles.Top;
            myUC.Anchor = AnchorStyles.Right;
            myUC.Dock = DockStyle.Fill;

            this.Text = myUC.Tag.ToString();
            panUC.Controls.Add(myUC);

        }

        private void barBtnDrugManag_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var ucControl = new UcDrgMange { _Mod = _Mod };
            ShowUC(ucControl);

        }
    }
}