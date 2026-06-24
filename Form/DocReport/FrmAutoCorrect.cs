using System;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Saman.BLL.DocReport;
using static System.Windows.Forms.InputLanguage;
using Control = App.Control;


namespace Saman.Form.DocReport;

public partial class FrmAutoCorrect : XtraForm
{
    private readonly AutoCorrect _bllAutoCorrect = new();

    private readonly bool _IsEn;


    private DataTable _dtAutoCorrect;
    public string _FormName;
    public int _ID, _ModID, _ModIDAutoCorrect, _CurrentLanguage;

    public bool _ReadOnly;

    private DataRow _rowAutoCorrectSelect;


    public FrmAutoCorrect()
    {
        InitializeComponent();
    }

    private void FrmAutoCorrect_Load(object sender, EventArgs e)
    {
        ActiveControl = txtReplace;
        InputLanguage();
        AutoCorrectLoad();
        Text = _FormName;
        var a = _ModID;
    }

    private void InputLanguage()
    {
        var Key = CurrentInputLanguage.LayoutName;

        if (Key == "US")
        {
            _CurrentLanguage = 1;
            _FormName = "English(United States)";
            txtReplace.Properties.Mask.EditMask = App.UI.MaskEnglishWithNumChar;
            txtWith.Properties.Mask.EditMask = App.UI.MaskEnglishWithNumChar;
        }

        else
        {
            _CurrentLanguage = 2;
            _FormName = "Persian(Iran)";
            txtReplace.Properties.Mask.EditMask = App.UI.MaskPersianWithNum;
            txtWith.Properties.Mask.EditMask = App.UI.MaskPersianWithNum;
        }
    }


    private void FrmAutoCorrect_Paint(object sender, PaintEventArgs e)
    {
        Control.TextEditLine(txtReplace, e);
        Control.TextEditLine(txtWith, e);
    }

    private void AutoCorrectLoad()
    {
        _dtAutoCorrect = _bllAutoCorrect.AutoCorrectAction(1, null, null, _CurrentLanguage, null, null);
        grdAuroCorrect.DataSource = _dtAutoCorrect;

        FormNull();
    }

    private void FormNull()
    {
        txtWith.Text = null;
        txtReplace.Text = null;
        _ID = 0;
    }

    private void grvAutoCorrect_Click(object sender, EventArgs e)
    {
        if (grvAutoCorrect.FocusedRowHandle < 0) return;

        _rowAutoCorrectSelect = grvAutoCorrect.GetDataRow(grvAutoCorrect.FocusedRowHandle);


        _ModIDAutoCorrect = Convert.ToInt32(_rowAutoCorrectSelect["ModID"]);

        if (_ModID == _ModIDAutoCorrect)
        {
            _ID = Convert.ToInt32(_rowAutoCorrectSelect["ID"]);
            txtReplace.Text = _rowAutoCorrectSelect["BeforeText"].ToString();
            txtWith.Text = _rowAutoCorrectSelect["AfterText"].ToString();
        }
    }

    private void lblNew_Click(object sender, EventArgs e)
    {
        FormNull();
    }

    private void lblAdd_Click(object sender, EventArgs e)
    {
        if (_ID > 0)
            _bllAutoCorrect.AutoCorrectAction(2, _ID, _ModID, _CurrentLanguage, txtReplace.Text, txtWith.Text);
        else
            _bllAutoCorrect.AutoCorrectAction(0, null, _ModID, _CurrentLanguage, txtReplace.Text, txtWith.Text);

        CheckReadOnly(false);
        AutoCorrectLoad();
    }

    private void lblDel_Click(object sender, EventArgs e)
    {
        _bllAutoCorrect.AutoCorrectAction(3, _ID, null, null, null, null);
        CheckReadOnly(false);
        AutoCorrectLoad();
    }

    private void txtReplace_EditValueChanged(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtReplace.Text) || string.IsNullOrEmpty(txtWith.Text))
            CheckReadOnly(false);
        else
            CheckReadOnly(true);
    }

    private void txtWith_EditValueChanged(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtReplace.Text) || string.IsNullOrEmpty(txtWith.Text))
            CheckReadOnly(false);
        else
            CheckReadOnly(true);
    }

    private void CheckReadOnly(bool IsReadOnly)
    {
        lblAdd.Enabled = IsReadOnly;
        lblDel.Enabled = IsReadOnly;
    }
}