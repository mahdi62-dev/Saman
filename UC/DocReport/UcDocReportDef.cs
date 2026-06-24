using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
using DAL;
using DevExpress.XtraEditors;
using LIB;
using Saman.BLL.DocReport;

namespace Saman.UC.DocReport;

public partial class UcDocReportDef : XtraUserControl
{
    private readonly DocReportDef _bllDocReportDef = new();

    private DataTable _dtRepRpt;

    private string _FileName;
    private string _ReportFilePath;
    private byte[] _RptFile;

    public UcDocReportDef()
    {
        InitializeComponent();
    }

    private void UcDocReportDef_Load(object sender, EventArgs e)
    {
        UL.FillLookup(lueReport, "0", cSet.cnStringReport, false, "ID,Name", "vDocTemplate", "vDocTemplate", null,
            null);
    }

    private void btnReportBrowse_Click(object sender, EventArgs e)
    {
        if (chkDic.Checked) ofdReportFile.Filter = "Dictionary (*.dic) |*.dic";

        if (ofdReportFile.ShowDialog() == DialogResult.OK)
        {
            _ReportFilePath = ofdReportFile.FileName;
            txtReportPath.Text = _ReportFilePath;
        }
    }

    private void btnReportSaveToDb_Click(object sender, EventArgs e)
    {
        if (chkDic.Checked)
        {
            byte[] fileDic;
            using (var stream = new FileStream(_ReportFilePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = new BinaryReader(stream))
                {
                    fileDic = reader.ReadBytes((int)stream.Length);
                }
            }

            var DicID = Convert.ToInt32(txtRepID.Text);
            _bllDocReportDef.DicFileSave(DicID, fileDic, GL.UserPersID);
        }

        byte[] file;
        using (var stream = new FileStream(_ReportFilePath, FileMode.Open, FileAccess.Read))
        {
            using (var reader = new BinaryReader(stream))
            {
                file = reader.ReadBytes((int)stream.Length);
            }
        }

        var TemplateID = Convert.ToInt32(txtRepID.Text);
        _bllDocReportDef.DocReportFileSave(TemplateID, txtFileName.Text, file, GL.UserPersID);
    }

    private void btnReportBrowse1_Click(object sender, EventArgs e)
    {
        if (chkDic1.Checked) sfdReportFile.Filter = "Dictionary (*.dic) |*.dic";

        sfdReportFile.FileName = _FileName;
        if (sfdReportFile.ShowDialog() == DialogResult.OK)
        {
            _ReportFilePath = sfdReportFile.FileName;
            txtReportPath1.Text = _ReportFilePath;

            using (Stream file = File.OpenWrite(_ReportFilePath))
            {
                file.Write(_RptFile, 0, _RptFile.Length);
                ucShowTextSave.ShowText("گزارش ذخیره گردید", 5000);
            }
        }
    }


    private void lueReport_EditValueChanged(object sender, EventArgs e)
    {
        var FileID1 = Convert.ToInt32(lueReport.EditValue);

        if (chkDic1.Checked)
            _dtRepRpt = _bllDocReportDef.FileDicLoadDB(FileID1);
        else
            _dtRepRpt = _bllDocReportDef.FileDocLoadDB(FileID1);

        if (_dtRepRpt.Rows.Count == 0) return;

        txtFileName1.Text = _dtRepRpt.Rows[0]["ID"] + "-" + _dtRepRpt.Rows[0]["Name"];
        _FileName = _dtRepRpt.Rows[0]["Name"].ToString();
        _RptFile = (byte[])_dtRepRpt.Rows[0]["Doc"];

        txtReportPath1.Text = " ";
    }

    private void chkDic1_CheckedChanged(object sender, EventArgs e)
    {
        if (chkDic1.Checked)
            UL.FillLookup(lueReport, "0", cSet.cnStringReport, false, "ID,Name", "vDicFile", "vDicFile", null,
                null);
        else
            UL.FillLookup(lueReport, "0", cSet.cnStringReport, false, "ID,Name", "vDocTemplate", "vDocTemplate",
                null, null);

        txtFileName1.Text = null;
    }
}