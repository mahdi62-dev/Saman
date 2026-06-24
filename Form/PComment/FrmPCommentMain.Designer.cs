namespace Saman
{
    partial class FrmPCommentMain
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
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btnStart = new DevExpress.XtraEditors.SimpleButton();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelControl1.Appearance.Font = new System.Drawing.Font("IRANSansFaNum", 30F, System.Drawing.FontStyle.Bold);
            this.labelControl1.Appearance.ForeColor = System.Drawing.Color.White;
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Appearance.Options.UseForeColor = true;
            this.labelControl1.Appearance.Options.UseTextOptions = true;
            this.labelControl1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControl1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical;
            this.labelControl1.Location = new System.Drawing.Point(382, 88);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(698, 156);
            this.labelControl1.TabIndex = 425;
            this.labelControl1.Text = "بیمارستان حضرت فاطمه دماوند\r\nسنجش رضایت بیماران";
            this.labelControl1.Click += new System.EventHandler(this.btnEnter_Click);
            // 
            // btnStart
            // 
            this.btnStart.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnStart.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(216)))), ((int)(((byte)(58)))));
            this.btnStart.Appearance.Font = new System.Drawing.Font("IRANSansFaNum", 20F, System.Drawing.FontStyle.Bold);
            this.btnStart.Appearance.ForeColor = System.Drawing.Color.Black;
            this.btnStart.Appearance.Options.UseBackColor = true;
            this.btnStart.Appearance.Options.UseFont = true;
            this.btnStart.Appearance.Options.UseForeColor = true;
            this.btnStart.AppearanceHovered.BackColor = System.Drawing.Color.White;
            this.btnStart.AppearanceHovered.Font = new System.Drawing.Font("IRANSansFaNum", 20F);
            this.btnStart.AppearanceHovered.Options.UseBackColor = true;
            this.btnStart.AppearanceHovered.Options.UseFont = true;
            this.btnStart.Location = new System.Drawing.Point(525, 671);
            this.btnStart.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat;
            this.btnStart.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btnStart.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.btnStart.Name = "btnStart";
            this.btnStart.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btnStart.Size = new System.Drawing.Size(411, 88);
            this.btnStart.TabIndex = 426;
            this.btnStart.Text = "شروع";
            this.btnStart.Click += new System.EventHandler(this.btnEnter_Click);
            // 
            // FrmPCommentMain
            // 
            this.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayoutStore = System.Windows.Forms.ImageLayout.Stretch;
            this.BackgroundImageStore = global::Saman.Properties.Resources.PComment_bg;
            this.ClientSize = new System.Drawing.Size(1461, 907);
            this.ControlBox = false;
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.labelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FrmPCommentMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmPCommentMain_Load);
            this.Click += new System.EventHandler(this.btnEnter_Click);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.FrmLogin_Paint);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton btnStart;
    }
}