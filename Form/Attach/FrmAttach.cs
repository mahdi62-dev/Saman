using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Saman.BLL.Attach;

namespace Saman.Form.Attach;

public partial class FrmAttach : XtraForm
{
    private readonly DataTable _dtFileList = new();
    public int _DepID;

    public int _EncID, _PageID;


    public int _ModID;


    public FrmAttach()
    {
        InitializeComponent();
    }

    private void FrmAttach_Load(object sender, EventArgs e)
    {
        var bllAttachTableFileList = new DT();
        bllAttachTableFileList.CreateTableFileList(_dtFileList);
    }

    private void btnOpen_Click(object sender, EventArgs e)
    {
        //var lstItem =new lis
        var dr = ofdOpen.ShowDialog();

        if (dr == DialogResult.OK)
        {
            // Read the files
            foreach (var file in ofdOpen.FileNames)
            {
                var fileInfo = new FileInfo(file);

                var size = fileInfo.Length / 1024;
                var extension = fileInfo.Extension;

                var rowFile = _dtFileList.NewRow();
                picPreview.Image = Image.FromFile(file);

                rowFile["FileName"] = file;
                rowFile["FileSize"] = size.ToString("n0") + " KB";
                rowFile["FileExtension"] = extension;


                _dtFileList.Rows.Add(rowFile);
            }

            grdFileList.DataSource = _dtFileList;

            //// Create a PictureBox.
            //try
            //{
            //    PictureBox pb = new PictureBox();
            //    Image loadedImage = Image.FromFile(file);
            //    pb.Height = loadedImage.Height;
            //    pb.Width = loadedImage.Width;
            //    pb.Image = loadedImage;
            //    flowLayoutPanel1.Controls.Add(pb);
            //}
            //catch (SecurityException ex)
            //{
            //    // The user lacks appropriate permissions to read files, discover paths, etc.
            //    MessageBox.Show("Security error. Please contact your administrator for details.\n\n" +
            //                    "Error message: " + ex.Message + "\n\n" +
            //                    "Details (send to Support):\n\n" + ex.StackTrace
            //    );
            //}
            //catch (Exception ex)
            //{
            //    // Could not load the image - probably related to Windows file system permissions.
            //    MessageBox.Show("Cannot display the image: " + file.Substring(file.LastIndexOf('\\'))
            //                                                 + ". You may not have permission to read the file, or " +
            //                                                 "it may be corrupt.\n\nReported error: " +
            //                                                 ex.Message);
            //        //}
            //    }
            //}
        }
    }

    private void btnClose_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.No;
        Close();
    }
}