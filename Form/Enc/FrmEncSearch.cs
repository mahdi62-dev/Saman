using System;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Saman.Form.Enc;

public partial class FrmEncSearch : XtraForm
{
    public DataTable _dtEncSearch;
    public int _PatientID;

    public FrmEncSearch()
    {
        InitializeComponent();
    }

    private void FrmMessage_Load(object sender, EventArgs e)
    {
        _PatientID = 0;
        grdSearch.DataSource = _dtEncSearch;

        ActiveControl = btnPatientSelect;
    }


    private void FrmMessage_KeyPress(object sender, KeyPressEventArgs e)
    {
    }

    private void btnPatientSelect_Click(object sender, EventArgs e)
    {
        var row = grvSearch.GetFocusedDataRow();
        _PatientID = Convert.ToInt32(row["PatientID"]);
        DialogResult = DialogResult.Yes;
        Close();
    }

    private void btnPatientNew_Click(object sender, EventArgs e)
    {
    }
}