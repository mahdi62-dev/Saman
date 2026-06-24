
namespace Saman.Form.Attach
{
    partial class FrmAttDisplay
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAttDisplay));
            this.picAtt = new DevExpress.XtraEditors.PictureEdit();
            ((System.ComponentModel.ISupportInitialize)(this.picAtt.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // picAtt
            // 
            this.picAtt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picAtt.Location = new System.Drawing.Point(0, 0);
            this.picAtt.Name = "picAtt";
            this.picAtt.Properties.AllowScrollOnMouseWheel = DevExpress.Utils.DefaultBoolean.True;
            this.picAtt.Properties.AllowScrollViaMouseDrag = true;
            this.picAtt.Properties.AllowZoom = DevExpress.Utils.DefaultBoolean.True;
            this.picAtt.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.picAtt.Properties.ShowEditMenuItem = DevExpress.Utils.DefaultBoolean.True;
            this.picAtt.Properties.ShowScrollBars = true;
            this.picAtt.Properties.ShowZoomSubMenu = DevExpress.Utils.DefaultBoolean.True;
            this.picAtt.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Squeeze;
            this.picAtt.Properties.ZoomingOperationMode = DevExpress.XtraEditors.Repository.ZoomingOperationMode.MouseWheel;
            this.picAtt.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.picAtt.Size = new System.Drawing.Size(948, 668);
            this.picAtt.TabIndex = 0;
            // 
            // FrmAttDisplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(948, 668);
            this.Controls.Add(this.picAtt);
            this.IconOptions.Image = ((System.Drawing.Image)(resources.GetObject("FrmAttDisplay.IconOptions.Image")));
            this.Name = "FrmAttDisplay";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TopMost = true;
            this.Deactivate += new System.EventHandler(this.FrmAttDisplay_Deactivate);
            this.Load += new System.EventHandler(this.FrmAttDisplay_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picAtt.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PictureEdit picAtt;
    }
}