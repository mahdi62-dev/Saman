using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;

namespace Saman.UC.UI;

public partial class UcGridLookup : XtraUserControl
{
    private readonly string[] _SelectdTexts;
    private DataTable _dtUC;


    public UcGridLookup()
    {
        InitializeComponent();
    }

    public int _Width
    {
        get => Width;
        set => Width = value;
    }


    public string _Caption
    {
        get => lblCaption.Text;
        set => lblCaption.Text = value;
    }

    public DataTable _DataSource
    {
        get => _dtUC;
        set
        {
            _dtUC = value;
            SetDataSource();
        }
    }

    public string _ValueMember
    {
        get => gluGrid.Properties.ValueMember;
        set => gluGrid.Properties.ValueMember = value;
    }

    public string _DisplayMember
    {
        get => gluGrid.Properties.DisplayMember;
        set => gluGrid.Properties.DisplayMember = value;
    }

    public int[] _SelectedValues { get; private set; }

    public string _SelectdText => GetSelectedText();

    public string _SelectedValuesStr { get; private set; }

    private void SetDataSource()
    {
        var dt = _DataSource;
        var src = new BindingSource { DataSource = dt };

        gluGrid.Properties.DataSource = src;
    }

    private void UcGridLookup_Load(object sender, EventArgs e)
    {
        gluGrid.Properties.ValueMember = _ValueMember;
        gluGrid.Properties.DisplayMember = _DisplayMember;

        //var curDateTime = DateTime.Parse(App.SDate.ShamsiDate());
        //datOprEndDate.DateTime = datOprStartDate.DateTime = curDateTime;
    }

    private void panUc_Paint(object sender, PaintEventArgs e)
    {
    }

    private void gluGrid_CustomDisplayText(object sender,
        CustomDisplayTextEventArgs e)
    {
        var editor = sender as GridLookUpEdit;
        var view = editor.Properties.View;
        var selectedRowsCount = view.GetSelectedRows().Count();

        if (selectedRowsCount > 0)
        {
            var selfacrows = view.GetSelectedRows();

            string strText = null;
            foreach (var item in selfacrows)
            {
                strText += gluView.GetRowCellValue(item, gluGrid.Properties.DisplayMember);
                strText += ",";
            }

            strText = strText.TrimEnd(',');

            e.DisplayText = strText;


            GetSelectedValue(selfacrows);
        }
    }

    private void GetSelectedValue(int[] selectedrows)
    {
        string selectedID = null;
        string strVal = null;
        var selectedVal = new int[selectedrows.Length];
        var arrIndex = 0;

        foreach (var item in selectedrows)
        {
            selectedID = gluView.GetRowCellValue(item, _ValueMember).ToString();


            selectedVal[arrIndex] = Convert.ToInt32(selectedID);
            arrIndex += 1;


            strVal += selectedID;
            strVal += ",";
        }


        strVal = strVal.TrimEnd(',');

        _SelectedValuesStr = strVal;
        _SelectedValues = selectedVal;
    }

    private string GetSelectedText()
    {
        var selectedrows = gluView.GetSelectedRows();
        string selectedText = null;
        string strVal = null;
        var selectedVal = new string[selectedrows.Length];
        var arrIndex = 0;

        foreach (var item in selectedrows)
        {
            selectedText = gluView.GetRowCellValue(item, _DisplayMember).ToString();


            selectedVal[arrIndex] = selectedText;
            arrIndex += 1;


            strVal += selectedText;
            strVal += ",";
        }


        strVal = strVal.TrimEnd(',');

        //_SelectValsStr = strVal;
        //_SelectdTexts = selectedVal;

        return strVal;
    }


    private void gluGrid_Closed(object sender, ClosedEventArgs e)
    {
        (sender as GridLookUpEdit).LookAndFeel.UpdateStyleSettings();
    }
}