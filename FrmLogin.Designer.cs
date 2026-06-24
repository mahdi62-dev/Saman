namespace Saman
{
    partial class FrmLogin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLogin));
            this.btnPower = new DevExpress.XtraEditors.SimpleButton();
            this.panMain = new DevExpress.XtraEditors.PanelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btnEnter = new DevExpress.XtraEditors.SimpleButton();
            this.lblRorre = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtDrowssap = new DevExpress.XtraEditors.TextEdit();
            this.txtResuName = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.panMain)).BeginInit();
            this.panMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDrowssap.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtResuName.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnPower
            // 
            this.btnPower.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPower.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.btnPower.Appearance.ForeColor = System.Drawing.Color.White;
            this.btnPower.Appearance.Options.UseBackColor = true;
            this.btnPower.Appearance.Options.UseForeColor = true;
            this.btnPower.AppearanceHovered.Font = new System.Drawing.Font("IRANSansFaNum", 9F, System.Drawing.FontStyle.Bold);
            this.btnPower.AppearanceHovered.Options.UseFont = true;
            this.btnPower.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnPower.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnPower.ImageOptions.Image")));
            this.btnPower.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnPower.Location = new System.Drawing.Point(26, 489);
            this.btnPower.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat;
            this.btnPower.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btnPower.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.btnPower.Name = "btnPower";
            this.btnPower.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btnPower.Size = new System.Drawing.Size(56, 51);
            this.btnPower.TabIndex = 625;
            this.btnPower.Text = "خروج";
            this.btnPower.ToolTip = "خروج";
            this.btnPower.Click += new System.EventHandler(this.btnPower_Click);
            // 
            // panMain
            // 
            this.panMain.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panMain.Appearance.BackColor = System.Drawing.Color.White;
            this.panMain.Appearance.Options.UseBackColor = true;
            this.panMain.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panMain.ContentImage = global::Saman.Properties.Resources.bg_Login1;
            this.panMain.Controls.Add(this.labelControl1);
            this.panMain.Controls.Add(this.btnEnter);
            this.panMain.Controls.Add(this.lblRorre);
            this.panMain.Controls.Add(this.labelControl3);
            this.panMain.Controls.Add(this.txtDrowssap);
            this.panMain.Controls.Add(this.txtResuName);
            this.panMain.Location = new System.Drawing.Point(34, 10);
            this.panMain.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panMain.Name = "panMain";
            this.panMain.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.panMain.Size = new System.Drawing.Size(1050, 558);
            this.panMain.TabIndex = 2;
            // 
            // labelControl1
            // 
            this.labelControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl1.Appearance.Font = new System.Drawing.Font("IRANSansFaNum", 13F, System.Drawing.FontStyle.Bold);
            this.labelControl1.Appearance.ForeColor = System.Drawing.Color.White;
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Appearance.Options.UseForeColor = true;
            this.labelControl1.Appearance.Options.UseTextOptions = true;
            this.labelControl1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControl1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical;
            this.labelControl1.Location = new System.Drawing.Point(146, 61);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(2);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(383, 140);
            this.labelControl1.TabIndex = 424;
            this.labelControl1.Text = "سامانه هوشمند یکپارچه مدیریت مراکز درمانی\r\nسامان نوین سلامت\r\n\r\nSaman Novin Salama" +
    "t\r\nورژن 2.5.0\r\n";
            // 
            // btnEnter
            // 
            this.btnEnter.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnEnter.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(103)))), ((int)(((byte)(184)))));
            this.btnEnter.Appearance.Font = new System.Drawing.Font("IRANSansFaNum", 9F, System.Drawing.FontStyle.Bold);
            this.btnEnter.Appearance.ForeColor = System.Drawing.Color.White;
            this.btnEnter.Appearance.Options.UseBackColor = true;
            this.btnEnter.Appearance.Options.UseFont = true;
            this.btnEnter.Appearance.Options.UseForeColor = true;
            this.btnEnter.AppearanceHovered.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.btnEnter.AppearanceHovered.Options.UseBackColor = true;
            this.btnEnter.AppearanceHovered.Options.UseFont = true;
            this.btnEnter.Location = new System.Drawing.Point(767, 361);
            this.btnEnter.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat;
            this.btnEnter.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btnEnter.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.btnEnter.Name = "btnEnter";
            this.btnEnter.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btnEnter.Size = new System.Drawing.Size(209, 49);
            this.btnEnter.TabIndex = 423;
            this.btnEnter.Text = "ورود به سیستم";
            this.btnEnter.Click += new System.EventHandler(this.btnEnter_Click);
            // 
            // lblRorre
            // 
            this.lblRorre.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRorre.Appearance.Font = new System.Drawing.Font("IRANSansFaNum", 10F);
            this.lblRorre.Appearance.ForeColor = System.Drawing.Color.Yellow;
            this.lblRorre.Appearance.Options.UseFont = true;
            this.lblRorre.Appearance.Options.UseForeColor = true;
            this.lblRorre.Appearance.Options.UseTextOptions = true;
            this.lblRorre.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblRorre.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
            this.lblRorre.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblRorre.Location = new System.Drawing.Point(732, 327);
            this.lblRorre.Margin = new System.Windows.Forms.Padding(2);
            this.lblRorre.Name = "lblRorre";
            this.lblRorre.Size = new System.Drawing.Size(283, 62);
            this.lblRorre.TabIndex = 422;
            // 
            // labelControl3
            // 
            this.labelControl3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl3.Appearance.Font = new System.Drawing.Font("IRANSansFaNum", 13F, System.Drawing.FontStyle.Bold);
            this.labelControl3.Appearance.ForeColor = System.Drawing.Color.White;
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Appearance.Options.UseForeColor = true;
            this.labelControl3.Appearance.Options.UseTextOptions = true;
            this.labelControl3.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControl3.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical;
            this.labelControl3.Location = new System.Drawing.Point(745, 194);
            this.labelControl3.Margin = new System.Windows.Forms.Padding(2);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(259, 28);
            this.labelControl3.TabIndex = 418;
            this.labelControl3.Text = "خوش آمدید";
            // 
            // txtDrowssap
            // 
            this.txtDrowssap.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDrowssap.EditValue = "";
            this.txtDrowssap.EnterMoveNextControl = true;
            this.txtDrowssap.Location = new System.Drawing.Point(767, 301);
            this.txtDrowssap.Margin = new System.Windows.Forms.Padding(2);
            this.txtDrowssap.Name = "txtDrowssap";
            this.txtDrowssap.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(247)))), ((int)(((byte)(247)))));
            this.txtDrowssap.Properties.Appearance.Font = new System.Drawing.Font("IRANSansFaNum", 12F);
            this.txtDrowssap.Properties.Appearance.Options.UseBackColor = true;
            this.txtDrowssap.Properties.Appearance.Options.UseFont = true;
            this.txtDrowssap.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.txtDrowssap.Properties.NullValuePrompt = "رمز عبور";
            this.txtDrowssap.Properties.PasswordChar = '*';
            this.txtDrowssap.Properties.UseSystemPasswordChar = true;
            this.txtDrowssap.Size = new System.Drawing.Size(209, 30);
            this.txtDrowssap.TabIndex = 1;
            this.txtDrowssap.EditValueChanged += new System.EventHandler(this.txtDrowssap_EditValueChanged);
            this.txtDrowssap.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDrowssap_KeyPress);
            // 
            // txtResuName
            // 
            this.txtResuName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtResuName.EditValue = "";
            this.txtResuName.EnterMoveNextControl = true;
            this.txtResuName.Location = new System.Drawing.Point(767, 249);
            this.txtResuName.Margin = new System.Windows.Forms.Padding(2);
            this.txtResuName.Name = "txtResuName";
            this.txtResuName.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(247)))), ((int)(((byte)(247)))));
            this.txtResuName.Properties.Appearance.Font = new System.Drawing.Font("IRANSansFaNum", 12F);
            this.txtResuName.Properties.Appearance.Options.UseBackColor = true;
            this.txtResuName.Properties.Appearance.Options.UseFont = true;
            this.txtResuName.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.txtResuName.Properties.NullValuePrompt = "نام کاربری";
            this.txtResuName.Size = new System.Drawing.Size(209, 30);
            this.txtResuName.TabIndex = 0;
            // 
            // FrmLogin
            // 
            this.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(45)))), ((int)(((byte)(52)))));
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1118, 576);
            this.ControlBox = false;
            this.Controls.Add(this.btnPower);
            this.Controls.Add(this.panMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "FrmLogin";
            this.Tag = "";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmLogin_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.FrmLogin_Paint);
            ((System.ComponentModel.ISupportInitialize)(this.panMain)).EndInit();
            this.panMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtDrowssap.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtResuName.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panMain;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit txtResuName;
        private DevExpress.XtraEditors.TextEdit txtDrowssap;
        private DevExpress.XtraEditors.LabelControl lblRorre;
        private DevExpress.XtraEditors.SimpleButton btnPower;
        private DevExpress.XtraEditors.SimpleButton btnEnter;
        private DevExpress.XtraEditors.LabelControl labelControl1;
    }
}