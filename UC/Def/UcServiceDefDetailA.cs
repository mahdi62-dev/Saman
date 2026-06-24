using System;
using System.Data;
using System.Windows.Forms;
using DAL;
using DevExpress.XtraEditors;
using LIB;
using Control = App.Control;

namespace Saman.UC.Def;

public partial class UcServiceDefDetailA : XtraUserControl
{
    private readonly DataTable _dtService;
    public byte _DrugOrEquip;
    public int _ServiceID;
    public int _ServiceType;


    public UcServiceDefDetailA()
    {
        InitializeComponent();
    }

    private void UcServiceDefInfo_Load(object sender, EventArgs e)
    {
        _DrugOrEquip = 1;

        FillLookup();

        if (_DrugOrEquip == 1)
        {
            lblDrugBrandOrType.Text = "برند";
            lblDrugFormOrKind.Text = "شکل";
            lblDrugDoseOrSize.Text = "دوز";
        }

        if (_DrugOrEquip == 2)
        {
            lblDrugBrandOrType.Text = "نوع";
            lblDrugFormOrKind.Text = "جنس";
            lblDrugDoseOrSize.Text = "سایز";
        }

        ServiceLoad();
    }

    private void panMain_Paint(object sender, PaintEventArgs e)
    {
        Control.TextEditLine(lueServiceCategory, e);
        Control.TextEditLine(txtServiceName, e);
        Control.TextEditLine(txtServiceCode, e);
        Control.TextEditLine(txtServicePrice, e);
        Control.TextEditLine(lueServiceUnit, e);
        Control.TextEditLine(lueServiceStatus, e);
    }

    private void panDrug_Paint(object sender, PaintEventArgs e)
    {
        Control.TextEditLine(txtDrugBrandOrType, e);
        Control.TextEditLine(txtDrugFormOrKind, e);
        Control.TextEditLine(txtDrugDoseOrSize, e);
        Control.TextEditLine(txtDrugCompany, e);
    }

    private void FillLookup()
    {
        UL.FillLookup(lueServiceCategory, "0", cSet.cnStringA, false, "ID,Name", "vServiceCategory",
            "ServiceCategory", null, null);
        UL.FillLookup(lueServiceUnit, "0", cSet.cnStringA, false, "ID,Name", "vADR", "Unit", "ADName='Unit'", null);
    }

    private void ServiceLoad()
    {
        lueServiceCategory.EditValue = Convert.ToInt32(_dtService.Rows[0]["ServiceCategory"]);
        txtServiceName.Text = _dtService.Rows[0]["Name"].ToString();
        txtServiceCode.Text = _dtService.Rows[0]["ServiceCode"].ToString();
        txtServiceCode2.Text = _dtService.Rows[0]["ServiceCode2"].ToString();
        txtServicePrice.Text = _dtService.Rows[0]["ServiceCode2"].ToString();
    }

    private void btnServiceSave_Click(object sender, EventArgs e)
    {
    }
}