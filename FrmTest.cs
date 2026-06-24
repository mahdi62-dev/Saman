using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using DAL;
using DevExpress.XtraBars.ToastNotifications;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraRichEdit;
using DevExpress.XtraRichEdit.API.Native;
using Tulpep.NotificationWindow;
using static System.Windows.Forms.InputLanguage;

namespace Saman;

public partial class FrmTest : XtraForm
{
    private readonly DataTable _dtCashTotal;


    private readonly DataTable dataTable1 = new();
    private readonly GridControl gridControl;
    private readonly GridView gvConnections = new();
    private RepositoryItemLookUpEdit repositoryItem = new();

    public FrmTest()
    {
        InitializeComponent();
    }

    public int CashID { get; private set; }

    private void XtraForm1_Load(object sender, EventArgs e)
    {
        // TODO: This line of code loads data into the 'samanHISDataSet.vBed' table. You can move, or remove it, as needed.
        //ucDate1.ShowFirstDayOfMonth = true;
        //ucDate1.YearOnlyInput = true;

        //DynamicContentModule();
    }

    private static void InsertDocVariableField(Document document)
    {
        document.Fields.Create(document.Range.End, "DOCVARIABLE company");
    }

    private void richEditControl_CalculateDocumentVariable(object sender, CalculateDocumentVariableEventArgs e)
    {
        //Retrieve the field by its ID
        if (e.VariableName == "company")
        {
            //Retain the last paragraph of the inserted document in the resulting document
            e.KeepLastParagraph = true;

            //Use the method result as the field's value
            e.Value = "مهندس نوری";
            if (e.Value != null)
                //Keep the current field value
                e.Handled = true;

            //Suppress the field update
            e.FieldLocked = true;
        }


        //Retrieve the field by its ID
        if (e.VariableName == "managername")
        {
            //Retain the last paragraph of the inserted document in the resulting document
            e.KeepLastParagraph = true;

            //Use the method result as the field's value
            e.Value = "مدیریت سامان نوین";
            if (e.Value != null)
                //Keep the current field value
                e.Handled = true;

            //Suppress the field update
            e.FieldLocked = true;
        }
    }

    public void DynamicContentModule()
    {
        ////    Load a document template
        ////    richEditControl1.LoadDocument(@"d:\test.docx");

        ////    Insert the DOCVARIABLE field
        ////    InsertDocVariableField(richEditControl1.Document);

        ////    Subscribe to the CalculateDocumentVariable event
        ////    richEditControl1.CalculateDocumentVariable += richEditControl_CalculateDocumentVariable;

        ////Update all document fields
        ////    richEditControl1.Document.Fields.Update();
    }


    private void button1_Click(object sender, EventArgs e)

    {
        var t = CurrentInputLanguage.LayoutName;


        //if (Regex.IsMatch(textEdit1.Text, "^[a-zA-Z0-9]*$"))
        //{
        //    /* کد شما */
        //    int i;
        //    i = 2;
        //}

        //if (Regex.IsMatch(textEdit1.Text, @"^[\u0600-\u06FF\uFB8A\u067E\u0686\u06AF\u200C\u200F ]+$"))
        //{
        //    /* کد شما */
        //    int i;
        //    i = 2;
        //}
    }

    public RepositoryItemLookUpEdit InitRepositoryItem()
    {
        Lib.LoadDataToDs(cSet.cnStringA, true, DAL.GL.ds, "*", "vADR", "PayGroup", "ADNAME='PayGroup'",
            null);

        var dt = DAL.GL.ds.Tables["PayGroup"];
        var src = new BindingSource { DataSource = dt };

        repositoryItem.DataSource = src;


        repositoryItem.DisplayMember = "Name";
        repositoryItem.ValueMember = "Name";
        return repositoryItem;
    }

    private void button2_Click(object sender, EventArgs e)
    {
    }

    private void gridView1_CustomRowCellEditForEditing(object sender, CustomRowCellEditEventArgs e)
    {
        repositoryItem = InitRepositoryItem();

        if (e.Column.FieldName == "PayGroupName") e.RepositoryItem = repositoryItem;
    }

    private void btnSaveReport_Click(object sender, EventArgs e)
    {
        //            var FilePath = txtReportPath.Text;

        //            byte[] file;
        //            using (var stream = new FileStream(FilePath, FileMode.Open, FileAccess.Read))
        //            {
        //                using (var reader = new BinaryReader(stream))
        //                {
        //                    file = reader.ReadBytes((int) stream.Length);
        //                }
        //            }

        //            var TSQL = @"
        //  UPDATE dbo.ReportFile
        //  SET RptFile=@RptFile
        //  WHERE ID=1
        //";

        //            var com = new SqlCommand();
        //            com.Parameters.Add("@RptFile", file);

        //            var dalData = new DAL.Data();
        //            dalData.ExecuteNonQueryByText(DAL.cSet.cnStringReport, com, TSQL);
    }

    private void btnShowReportFromDB_Click(object sender, EventArgs e)
    {
        //            var TSQL = @"
        //SELECT * FROM dbo.ReportFile
        //WHERE ID=1
        //";
        //            var com = new SqlCommand();
        //            //com.Parameters.Add("@RptFile", RptFile);

        //            DataTable dt = new DataTable();
        //            var dalData = new DAL.Data();
        //            dt = dalData.GetDataTableByText(DAL.cSet.cnStringReport, com, TSQL);

        //            StiReport report = new StiReport(); 
        //            byte[] rptFileBytes = (byte[])dt.Rows[0]["RptFile"];
        //            report.Load(rptFileBytes);

        //            report.Show();
    }

    private void btnSaveToFile_Click(object sender, EventArgs e)
    {
        var TSQL = @"
SELECT * FROM dbo.ReportFile
WHERE ID=1
";
        var com = new SqlCommand();
        //com.Parameters.Add("@RptFile", RptFile);

        var dt = new DataTable();
        var dalData = new Data();
        dt = dalData.GetDataTableByText(cSet.cnStringReport, com, TSQL);

        var blob = (byte[])dt.Rows[0]["RptFile"];

        using (var fs = new FileStream(@"c:\rep.mrt", FileMode.Create, FileAccess.Write))
        {
            fs.Write(blob, 0, blob.Length);
        }
    }


    private void button3_Click(object sender, EventArgs e)
    {
        //string nima = richEditControl1.HtmlText;


        //string example = nima;
        //@"{\rtf1\ansi\ansicpg1252\deff0\deflang1033{\fonttbl{\f0\fnil\fcharset0 Verdana;}}  \viewkind4\uc1\pard\f0\fs18 This is \b bold\par  \par  \b0 This is\b  \b0\i italics\par  \par  \i0 This is \ul underline\ulnone\par  }";

        //richTextBox1.Rtf = example;
        //string output = "";

        //bool b = false;
        //bool i = false;
        //bool u = false;

        //for (richTextBox1.SelectionStart = 0;
        //    richTextBox1.SelectionStart < richTextBox1.TextLength - 1;
        //    richTextBox1.SelectionStart++)
        //{
        //    richTextBox1.SelectionLength = 1;

        //    var s = richTextBox1.SelectionFont.Style;

        //    if ((s & FontStyle.Bold) != 0 != b)
        //    {
        //        output += "[B]";
        //        b = !b;
        //    }

        //    if ((s & FontStyle.Italic) != 0 != i)
        //    {
        //        output += "[I]";
        //        i = !i;
        //    }

        //    if ((s & FontStyle.Underline) != 0 != u)
        //    {
        //        output += "[U]";
        //        u = !u;
        //    }

        //    output += richTextBox1.SelectedText;
        //}
        //richEditControl1.SaveDocument(
        //    System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "d:\\Test.docx"),
        //    DevExpress.XtraRichEdit.DocumentFormat.OpenXml);
    }

    private void button4_Click(object sender, EventArgs e)
    {
        //richEditControl1.SaveDocument(
        //    System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "d:\\nnnnn.docx"),
        //    DevExpress.XtraRichEdit.DocumentFormat.OpenXml);

        //var example =
        //    @"{\rtf1\ansi\ansicpg1252\deff0\deflang1033{\fonttbl{\f0\fnil\fcharset0 Verdana;}}  \viewkind4\uc1\pard\f0\fs18 This is \b bold\par  \par  \b0 This is\b  \b0\i italics\par  \par  \i0 This is \ul underline\ulnone\par  }";

        //richTextBox1.Rtf = example;
        //string output = "";

        //bool b = false;
        //bool i = false;
        //bool u = false;

        //for (richTextBox1.SelectionStart = 0;
        //    richTextBox1.SelectionStart < richTextBox1.TextLength - 1;
        //    richTextBox1.SelectionStart++)
        //{
        //    richTextBox1.SelectionLength = 1;

        //    var s = richTextBox1.SelectionFont.Style;

        //    if ((s & FontStyle.Bold) != 0 != b)
        //    {
        //        output += "[B]";
        //        b = !b;
        //    }

        //    if ((s & FontStyle.Italic) != 0 != i)
        //    {
        //        output += "[I]";
        //        i = !i;
        //    }

        //    if ((s & FontStyle.Underline) != 0 != u)
        //    {
        //        output += "[U]";
        //        u = !u;
        //    }

        //    output += richTextBox1.SelectedText;
        //}
    }

    private void button5_Click(object sender, EventArgs e)
    {
        //APP.SDate.Shamsi2Miladi("1400/02/31 23:10");

        var pc = new PersianCalendar();
        var i = pc.GetDaysInMonth(1399, 12);


        i = pc.GetDaysInMonth(1400, 12);
    }

    private void button5_Click_1(object sender, EventArgs e)
    {
        var p = new PersianCalendar();
        var x = p.ToDateTime(1400, 4, 10, 10, 20, 0, 0);
        int y, m, d;
        y = x.Year;
        m = x.Month;
        d = x.Day;
    }

    private void button6_Click(object sender, EventArgs e)
    {
        var PopNot = new PopupNotifier
        {
            TitleText = "سی شارپ یار",
            ContentText = "مرجع پروژه های برنامه نویسی به زبان سی شارپ",
            IsRightToLeft = true,
            ImageSize = new Size(48, 48),
            //PopNot.TitleFont = new Font(new FontFamily("IRANSans Light"), 12.0f);
            AnimationDuration = 2000,
            BodyColor = Color.Snow,
            BorderColor = Color.LightGray,
            //PopNot.ContentFont = new Font(new FontFamily("IRANSans Light"), 10.0f);
            ContentPadding = new Padding(10, 10, 10, 10),
            HeaderColor = Color.LightSkyBlue,
            HeaderHeight = 15,
            ImagePadding = new Padding(15, 15, 15, 15),
            ShowCloseButton = false,
            ShowGrip = false,
            Size = new Size(400, 100),
            TitleColor = Color.Blue
        };
        PopNot.Popup();
    }

    private void button7_Click(object sender, EventArgs e)
    {
        toastNotificationsManager1.ShowNotification("0216d08b-b8fa-453a-894a-ee231c41d3c3");
        //toastNotificationsManager1.ShowNotification(toastNotificationsManager1.Notifications[3]);
    }

    private void toastNotificationsManager1_Activated(object sender, ToastNotificationEventArgs e)
    {
        switch (e.NotificationID.ToString())
        {
            case "0216d08b-b8fa-453a-894a-ee231c41d3c3":
                MessageBox.Show("Notification #1 Clicked");
                break;
            case "66501f90-ac6b-440d-bf73-483c5ab22143":
                MessageBox.Show("Notification #2 Clicked");
                break;
        }
    }

    private void richEditControl1_Click(object sender, EventArgs e)
    {
        //Document doc = richEditControl1.Document;
        //int intPos = 0;
        //intPos = doc.CaretPosition.ToInt();

        //if (intPos > 1)
        //    intPos -= 1;

        // DocumentPosition pos1 = doc.CreatePosition(intPos);

        // //doc.InsertText(pos1, "The Word ");

        // DocumentRange r1 = doc.CreateRange(pos1 , 1);
        // string str = doc.GetText(r1);

        // if(str.Length ==0 ) return;

        // char c = str[0];

        // int ascii = (int) c;

        // if (ascii >= 1570 && ascii <= 1641)
        // {
        //     LIB.UL.LangChange(2);

        // }
        // else
        // {
        //     LIB.UL.LangChange(1);

        // }
    }

    private void button2_Click_1(object sender, EventArgs e)
    {
    }

    private void richEditControl1_SelectionChanged(object sender, EventArgs e)
    {
        CaretPositionLanguageCheck();
    }

    private void CaretPositionLanguageCheck()
    {
        //Document doc = richEditControl1.Document;
        //int intPos = 0;
        //intPos = doc.CaretPosition.ToInt();

        //if (intPos > 1)
        //    intPos -= 1;

        //DocumentPosition pos1 = doc.CreatePosition(intPos);

        //DocumentRange r1 = doc.CreateRange(pos1 , 1);
        //string str = doc.GetText(r1);

        //if(str.Length ==0 ) return;

        //char c = str[0];
        //int ascii = (int) c;

        //if (ascii >= 1570 && ascii <= 1641) 
        //    LIB.UL.LangChange(2);
        //else
        //    LIB.UL.LangChange(1);
    }


    private void button8_Click(object sender, EventArgs e)
    {
    }

    private void simpleButton1_Click(object sender, EventArgs e)
    {
    }
}