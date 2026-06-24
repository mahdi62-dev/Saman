using System;
using System.Data;
using System.Windows.Forms;
using DAL;
using DevExpress.XtraEditors;
using LIB;
using Saman.BLL.Def;

namespace Saman.Form.Def;

public partial class FrmDefADR : XtraForm
{
    private readonly DefADR _bllADR = new();
    public int _ADID, _ID;


    private DataTable _dtADR;

    public bool _ReadOnly;

    private DataRow _rowADRSelect;


    public FrmDefADR()
    {
        InitializeComponent();
    }

    private void FrmDefADR_Load(object sender, EventArgs e)
    {
        UL.LangChange(2);

        ActiveControl = lueAD;

        var DefaultValue = 0;
        if (_ADID > 0)
        {
            DefaultValue = _ADID;
            lueAD.Enabled = false;
        }

        UL.FillLookup(lueAD, DefaultValue.ToString(), cSet.cnStringA, false, "*", "AD", "AD", null, null);
    }


    private void btnClose_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.No;
        Close();
    }

    private void lueAD_EditValueChanged(object sender, EventArgs e)
    {
        _ADID = Convert.ToInt32(lueAD.EditValue);
        CheckReadOnly(false);
        FormNull();

        ADRLoad();
    }

    private void ADRLoad()
    {
        _dtADR = _bllADR.ADRLoad(1, _ADID);
        grdADR.DataSource = _dtADR;
        FormNull();
    }

    private void FormNull()
    {
        txtID.Text = null;
        txtName.Text = null;
        chkIsDisable.Checked = false;
    }

    private void grdADR_Click(object sender, EventArgs e)
    {
        if (grvADR.FocusedRowHandle < 0) return;

        _rowADRSelect = grvADR.GetDataRow(grvADR.FocusedRowHandle);

        _ID = Convert.ToInt32(_rowADRSelect["ID"]);
        txtID.Text = _ID.ToString();
        txtName.Text = _rowADRSelect["Name"].ToString();
        chkIsDisable.EditValue = _rowADRSelect["isDisable"];

        panADRID.Enabled = false;
        panADRName.Enabled = true;
        panADRIsDisable.Enabled = true;
    }


    private void CheckReadOnly(bool IsReadOnly)
    {
        panADRID.Enabled = IsReadOnly;
        panADRName.Enabled = IsReadOnly;
        panADRIsDisable.Enabled = IsReadOnly;
    }

    private void lblADRNew_Click(object sender, EventArgs e)
    {
        grvADR.FocusedRowHandle = 0;
        if (_dtADR.Rows.Count == 0) return;

        FormNull();
        if (_ADID != 53)
        {
            _ID = Convert.ToInt32(_dtADR.Compute("MAX([ID])", string.Empty)) + 1;
            txtID.Text = _ID.ToString();
        }
        else
        {
            panADRID.Enabled = true;
        }

        panADRName.Enabled = true;
        panADRIsDisable.Enabled = true;

        grvADR.FocusedRowHandle = grvADR.RowCount - 1;
    }

    private void lblAdd_Click(object sender, EventArgs e)
    {
        var rowSelectgrv = grvADR.GetFocusedDataSourceRowIndex();


        _bllADR.ADRSave(0, _ADID, txtID.Text, txtName.Text, chkIsDisable.Checked);

        CheckReadOnly(false);
        ADRLoad();

        grvADR.SelectRow(rowSelectgrv);
        grvADR.FocusedRowHandle = rowSelectgrv;
    }

    private void lblDel_Click(object sender, EventArgs e)
    {
        var rowSelectgrv = grvADR.GetFocusedDataSourceRowIndex();

        _bllADR.ADRSave(4, _ADID, txtID.Text, txtName.Text, chkIsDisable.Checked);
        CheckReadOnly(false);
        ADRLoad();

        grvADR.SelectRow(rowSelectgrv - 1);
        grvADR.FocusedRowHandle = rowSelectgrv - 1;
    }
}