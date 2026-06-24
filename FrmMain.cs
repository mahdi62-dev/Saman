using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Docking2010.Views;
using DevExpress.XtraBars.Navigation;
using DevExpress.XtraEditors;
using Saman.APP.SAC;
using Saman.Form.Acc.Bond;
using Saman.Form.Cash;
using Saman.Form.Clinic;
using Saman.Form.DayCare;
using Saman.Form.Def;
using Saman.Form.DefErp;
using Saman.Form.Dis;
using Saman.Form.Discount;
using Saman.Form.Enc;
using Saman.Form.FrmInsuranceExport;
using Saman.Form.FrmReport;
using Saman.Form.FrmSch;
using Saman.Form.Lab;
using Saman.Form.Loan;
using Saman.Form.Opr;
using Saman.Form.Payroll;
using Saman.Form.Percase;
using Saman.Form.Personnel;
using Saman.Form.Pet;
using Saman.Form.SAC;
using Saman.Form.Storage;
using Saman.Form.Timex;
using Saman.Form.Ward;
using Saman.UC.Sys;

namespace Saman;

public partial class FrmMain : XtraForm
{
    private readonly SACPermission sacPermission = new();

    public FrmMain()
    {
        InitializeComponent();
    }

    protected override void WndProc(ref Message m)
    {
        const int WM_SYSCOMMAND = 0x0112;
        const int SC_MOVE = 0xF010;
        const int WM_NCLBUTTONDBLCLK = 0x00A3; //double click on a title bar a.k.a. non-client area of the form

        switch (m.Msg)
        {
            case WM_SYSCOMMAND: //preventing the form from being moved by the mouse.
                var command = m.WParam.ToInt32() & 0xfff0;
                if (command == SC_MOVE) return;

                break;
        }

        if (m.Msg == WM_NCLBUTTONDBLCLK) //preventing the form being resized by the mouse double click on the title bar.
        {
            m.Result = IntPtr.Zero;
            return;
        }

        base.WndProc(ref m);
    }

    private void FrmMain_Load(object sender, EventArgs e)
    {
        //foreach (System.Windows.Forms.Form openForm in Application.OpenForms)
        //{
        //    if (openForm.Name == "FrmLogin")
        //    {

        //    }
        //}
        Text += "    " + "کاربر: " + GL.UserPersName;

        //string screenWidth = Screen.PrimaryScreen.Bounds.Width.ToString();
        //string screenHeight = Screen.PrimaryScreen.Bounds.Height.ToString();

        //this.MaximumSize = new System.Drawing.Size(Convert.ToInt32(screenWidth), Convert.ToInt32(screenHeight));
        //this.Location = new Point(0, 0);
        MaximizeBox = false;
        //this.MinimizeBox = false;


        aceEncMain.Visible = sacPermission.CheckPermission(1, 1000);
        aceClinicMain.Visible = sacPermission.CheckPermission(9, 9000);
        aceSchMain.Visible = sacPermission.CheckPermission(29, 29000);
        if (aceEncMain.Visible || aceClinicMain.Visible || aceSchMain.Visible) aceEncGroup.Visible = true;

        aceOprMain.Visible = sacPermission.CheckPermission(3, 3000);
        aceLabMain.Visible = sacPermission.CheckPermission(10, 10000);
        acePatMain.Visible = sacPermission.CheckPermission(11, 11000);
        acePhisioMain.Visible = sacPermission.CheckPermission(12, 12000);
        acePetMain.Visible = sacPermission.CheckPermission(30, 30000);
        aceDentalMain.Visible = sacPermission.CheckPermission(14, 14000);
        aceDialysisMain.Visible = sacPermission.CheckPermission(15, 15000);
        aceDrugMain.Visible = sacPermission.CheckPermission(16, 16000);

        if (aceOprMain.Visible || aceLabMain.Visible || acePatMain.Visible ||
            acePhisioMain.Visible || acePetMain.Visible || aceDentalMain.Visible ||
            aceDialysisMain.Visible || aceDrugMain.Visible)
            aceParaClinicGroup.Visible = true;

        acePetMain.Visible = sacPermission.CheckPermission(30, 30000);
        if (acePetMain.Visible) aceRadioMainGroup.Visible = true;

        aceCashMain.Visible = sacPermission.CheckPermission(2, 2000);
        aceDisMain.Visible = sacPermission.CheckPermission(6, 6000);
        aceDiscountMain.Visible = sacPermission.CheckPermission(17, 17000);
        aceIncomeMain.Visible = sacPermission.CheckPermission(18, 18000);
        aceStorageMain.Visible = sacPermission.CheckPermission(4, 4000);
        aceInsuranceExport.Visible = sacPermission.CheckPermission(97, 97000);

        if (aceCashMain.Visible || aceDisMain.Visible || aceDiscountMain.Visible ||
            aceIncomeMain.Visible || aceStorageMain.Visible || aceInsuranceExport.Visible)
            aceFinancialGroup.Visible = true;

        aceEmergMain.Visible = sacPermission.CheckPermission(20, 20000);
        aceWardDayCareMain.Visible = sacPermission.CheckPermission(7, 7000);
        aceWardMain.Visible = sacPermission.CheckPermission(8, 8000);
        aceNutritionMain.Visible = sacPermission.CheckPermission(21, 21000);
        aceMedicalRecordMain.Visible = sacPermission.CheckPermission(22, 22000);
        aceNursingOfficeMain.Visible = sacPermission.CheckPermission(23, 23000);
        aceBedManagerMain.Visible = sacPermission.CheckPermission(24, 24000);

        if (aceEmergMain.Visible || aceWardDayCareMain.Visible || aceWardMain.Visible ||
            aceNutritionMain.Visible || aceMedicalRecordMain.Visible || aceNursingOfficeMain.Visible ||
            aceBedManagerMain.Visible)
            aceWardGroup.Visible = true;

        aceHisDefMain.Visible = sacPermission.CheckPermission(25, 25000);

        if (aceHisDefMain.Visible) aceHISManageGroup.Visible = true;

        acePersonnelMain.Visible = sacPermission.CheckPermission(100, 100000);
        acePayrollMain.Visible = sacPermission.CheckPermission(101, 101000);
        acePercaseMain.Visible = sacPermission.CheckPermission(102, 102000);
        aceLoanMain.Visible = sacPermission.CheckPermission(104, 104000);
        aceTimexMain.Visible = sacPermission.CheckPermission(105, 105000);


        if (acePersonnelMain.Visible || acePayrollMain.Visible || acePercaseMain.Visible || aceLoanMain.Visible ||
            aceTimexMain.Visible) aceERPGroup.Visible = true;

        aceERPDefMain.Visible = sacPermission.CheckPermission(103, 103000);
        aceSACMain.Visible = sacPermission.CheckPermission(26, 26000);

        if (aceERPDefMain.Visible || aceSACMain.Visible) aceERPManageGroup.Visible = true;

        aceDashManMain.Visible = sacPermission.CheckPermission(99, 99000);
        aceReportMain.Visible = sacPermission.CheckPermission(98, 98000);

        if (aceDashManMain.Visible || aceReportMain.Visible) aceDashboardGroup.Visible = true;

        aceBond.Visible = sacPermission.CheckPermission(106, 106000);

        if (aceBond.Visible) aceFinacialGroupSystem.Visible = true;
        // aceTimexMain_Click(null,null);
    }

    private void ShowForm(System.Windows.Forms.Form frm)
    {
        accSystem.OptionsMinimizing.State = AccordionControlState.Minimized;
        frm.MdiParent = this;
        if (Application.OpenForms[frm.Name] == null) frm.Show();
    }

    private void aceEnc_Click(object sender, EventArgs e)
    {
        ShowForm(new FrmEncMain());
    }

    private void aceSchMain_Click(object sender, EventArgs e)
    {
        ShowForm(new FrmSch());
    }

    private void aceClinicMain_Click(object sender, EventArgs e)
    {
        ShowForm(new FrmClinicMain());
    }

    private void aceOprMain_Click(object sender, EventArgs e)
    {
        ShowForm(new FrmOprMain());
    }

    private void aceLabMain_Click(object sender, EventArgs e)
    {
        ShowForm(new FrmLabMain());
    }

    private void acePatMain_Click(object sender, EventArgs e)
    {
        //ShowForm(new FrmLabMain());
    }

    private void acePhisioMain_Click(object sender, EventArgs e)
    {
    }

    private void aceRadioMain_Click(object sender, EventArgs e)
    {
    }

    private void aceDentalMain_Click(object sender, EventArgs e)
    {
    }

    private void aceDialysisMain_Click(object sender, EventArgs e)
    {
    }

    private void aceDrugMain_Click(object sender, EventArgs e)
    {
    }

    private void aceCashMain_Click(object sender, EventArgs e)
    {
        ShowForm(new FrmCashMain());
    }

    private void aceDisMain_Click(object sender, EventArgs e)
    {
        ShowForm(new FrmDisMain());
    }

    private void aceDiscountMain_Click(object sender, EventArgs e)
    {
        ShowForm(new FrmDiscountMain());
    }

    private void aceIncomeMain_Click(object sender, EventArgs e)
    {
    }

    private void acePercase_Click(object sender, EventArgs e)
    {
    }

    private void aceStorageMain_Click(object sender, EventArgs e)
    {
        ShowForm(new frmStorageMain());
    }

    private void aceEmergMain_Click(object sender, EventArgs e)
    {
    }

    private void aceWardMain_Click(object sender, EventArgs e)
    {
        ShowForm(new FrmWardMain());
    }

    private void aceWardDayCareMain_Click(object sender, EventArgs e)
    {
        ShowForm(new FrmDayCareMain());
    }

    private void aceNutritionMain_Click(object sender, EventArgs e)
    {
    }

    private void aceMedicalRecordMain_Click(object sender, EventArgs e)
    {
    }

    private void aceBedManagerMain_Click(object sender, EventArgs e)
    {
    }

    private void aceDefMain_Click(object sender, EventArgs e)
    {
        ShowForm(new FrmDefMain());
    }

    private void aceSACMain_Click(object sender, EventArgs e)
    {
        ShowForm(new FrmSACMain());
    }

    private void aceDashboardMain_Click(object sender, EventArgs e)
    {
    }

    private void aceEncGroup_Click(object sender, EventArgs e)
    {
    }

    private void acePersonnelMain_Click(object sender, EventArgs e)
    {
        ShowForm(new FrmPersonnel());
    }

    private void aceTimexMain_Click(object sender, EventArgs e)
    {
        ShowForm(new FrmTimex());
    }

    private void acePayMain_Click(object sender, EventArgs e)
    {
        ShowForm(new FrmPayroll());
    }

    private void acePercaseMain_Click(object sender, EventArgs e)
    {
        ShowForm(new FrmPercase());
    }

    private void aceDefERPMain_Click(object sender, EventArgs e)
    {
        ShowForm(new FrmDefErpMain());
    }

    private void aceLoanMain_Click(object sender, EventArgs e)
    {
        ShowForm(new FrmLoan());
    }

    private void aceReportMain_Click(object sender, EventArgs e)
    {
        ShowForm(new FrmReport());
    }

    private void aceInsuranceExport_Click(object sender, EventArgs e)
    {
        ShowForm(new FrmInsuranceExport());
    }

    private void acePetMain_Click(object sender, EventArgs e)
    {
        ShowForm(new FrmPetMain());
    }

    private void aceBond_Click(object sender, EventArgs e)
    {
        ShowForm(new FrmBond());
    }

    private void batBtnChangeSsap_ItemClick(object sender, ItemClickEventArgs e)
    {
        var ucControl = new UcChangeSsap();
        ShowUC(ucControl);
    }


    private void ShowUC(object ucControl)
    {
        var myUC = (UserControl)ucControl;

        foreach (var item in Controls.OfType<Control>().ToList())
            if (item.Name == myUC.Name)
                Controls.Remove(item);

        var vvv = myUC.Name;
        myUC.Location = new Point(ClientSize.Width / 2 - myUC.Size.Width / 2,
            ClientSize.Height / 2 - myUC.Size.Height / 2);
        Controls.Add(myUC);
        myUC.Anchor = AnchorStyles.None;
    }

    private void accSystem_Click(object sender, EventArgs e)
    {
        dropDownButton2.Visible = false;

        var ucControl = new UcChangeSsap();
        UserControl myUC = ucControl;

        foreach (var item in Controls.OfType<Control>().ToList())
            if (item.Name == myUC.Name)
                Controls.Remove(item);
    }

    public void ShowWelcomeControls()
    {
        if (Application.OpenForms.Count == 2)
            dropDownButton2.Visible = true;
        else
            dropDownButton2.Visible = false;
    }

    private void tabbedViewMain_DocumentClosed(object sender, DocumentEventArgs e)
    {
        ShowWelcomeControls();
    }
}