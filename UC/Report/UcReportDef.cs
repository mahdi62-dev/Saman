using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
using DAL;
using DevExpress.XtraEditors;
using LIB;

namespace Saman.UC.Report;

public partial class UcReportDef : XtraUserControl
{
    private readonly BLL.Report.Report _bllRepRpt = new();

    private DataTable _dtRepRpt;

    private string _FileName;
    private string _ReportFilePath;
    private byte[] _RptFile;

    public UcReportDef()
    {
        InitializeComponent();
    }

    private void UcReportDef_Load(object sender, EventArgs e)
    {
        UL.FillLookup(lueReport, "0", cSet.cnStringReport, false, "RepID,Name", "vReport", "vReport", "RepType=2",
            null);
    }

    private void btnReportBrowse_Click(object sender, EventArgs e)
    {
        if (ofdReportFile.ShowDialog() == DialogResult.OK)
        {
            _ReportFilePath = ofdReportFile.FileName;
            txtReportPath.Text = _ReportFilePath;
        }
    }

    private void btnReportSaveToDb_Click(object sender, EventArgs e)
    {
        byte[] file;
        using (var stream = new FileStream(_ReportFilePath, FileMode.Open, FileAccess.Read))
        {
            using (var reader = new BinaryReader(stream))
            {
                file = reader.ReadBytes((int)stream.Length);
            }
        }

        var RepID = Convert.ToInt32(txtRepID.Text);
        _bllRepRpt.RepRptFileSave(RepID, txtFileName.Text, file, GL.UserPersID);
    }

    private void btnReportBrowse1_Click(object sender, EventArgs e)
    {
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

    private void txtRepID1_EditValueChanged(object sender, EventArgs e)
    {
        var RepID1 = Convert.ToInt32(txtRepID1.Text);

        _dtRepRpt = _bllRepRpt.RepRptFileLoadDB(RepID1);
        if (_dtRepRpt.Rows.Count == 0) return;

        txtFileName1.Text = _dtRepRpt.Rows[0]["Name"].ToString();
        _FileName = _dtRepRpt.Rows[0]["FileName"].ToString();
        _RptFile = (byte[])_dtRepRpt.Rows[0]["RptFile"];

        txtReportPath1.Text = " ";
    }

    private void lueReport_EditValueChanged(object sender, EventArgs e)
    {
        var RepID1 = Convert.ToInt32(lueReport.EditValue);

        _dtRepRpt = _bllRepRpt.RepRptFileLoadDB(RepID1);
        if (_dtRepRpt.Rows.Count == 0) return;

        txtFileName1.Text = _dtRepRpt.Rows[0]["RepID"] + "-" + _dtRepRpt.Rows[0]["Name"];
        _FileName = _dtRepRpt.Rows[0]["FileName"].ToString();
        _RptFile = (byte[])_dtRepRpt.Rows[0]["RptFile"];

        txtReportPath1.Text = " ";
    }
}