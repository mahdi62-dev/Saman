namespace Saman.UC.UI
{
    partial class UcPComment
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UcPComment));
            this.panMain = new System.Windows.Forms.Panel();
            this.flpMain = new DevExpress.Utils.FlyoutPanel();
            this.chkList = new DevExpress.XtraEditors.CheckedListBoxControl();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.lblPersTypeName = new DevExpress.XtraEditors.LabelControl();
            this.picPers = new System.Windows.Forms.PictureBox();
            this.lblPersID = new DevExpress.XtraEditors.LabelControl();
            this.btn2 = new DevExpress.XtraEditors.SimpleButton();
            this.btn1 = new DevExpress.XtraEditors.SimpleButton();
            this.btn3 = new DevExpress.XtraEditors.SimpleButton();
            this.panMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.flpMain)).BeginInit();
            this.flpMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPers)).BeginInit();
            this.SuspendLayout();
            // 
            // panMain
            // 
            this.panMain.BackColor = System.Drawing.Color.Transparent;
            this.panMain.Controls.Add(this.flpMain);
            this.panMain.Controls.Add(this.lblPersTypeName);
            this.panMain.Controls.Add(this.picPers);
            this.panMain.Controls.Add(this.lblPersID);
            this.panMain.Controls.Add(this.btn2);
            this.panMain.Controls.Add(this.btn1);
            this.panMain.Controls.Add(this.btn3);
            this.panMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panMain.Location = new System.Drawing.Point(0, 0);
            this.panMain.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panMain.Name = "panMain";
            this.panMain.Size = new System.Drawing.Size(296, 256);
            this.panMain.TabIndex = 568;
            // 
            // flpMain
            // 
            this.flpMain.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.flpMain.Appearance.Options.UseBackColor = true;
            this.flpMain.Controls.Add(this.chkList);
            this.flpMain.Controls.Add(this.btnSave);
            this.flpMain.Location = new System.Drawing.Point(0, 0);
            this.flpMain.Margin = new System.Windows.Forms.Padding(0);
            this.flpMain.MaximumSize = new System.Drawing.Size(296, 256);
            this.flpMain.Name = "flpMain";
            this.flpMain.Options.AnimationType = DevExpress.Utils.Win.PopupToolWindowAnimation.Fade;
            this.flpMain.Options.CloseOnOuterClick = true;
            this.flpMain.OptionsBeakPanel.BackColor = System.Drawing.Color.Transparent;
            this.flpMain.OptionsButtonPanel.AppearanceButton.Hovered.Font = new System.Drawing.Font("IRANSansFaNum", 16F, System.Drawing.FontStyle.Bold);
            this.flpMain.OptionsButtonPanel.AppearanceButton.Hovered.Options.UseFont = true;
            this.flpMain.OptionsButtonPanel.AppearanceButton.Normal.Font = new System.Drawing.Font("IRANSansFaNum", 14F, System.Drawing.FontStyle.Bold);
            this.flpMain.OptionsButtonPanel.AppearanceButton.Normal.Options.UseFont = true;
            this.flpMain.OptionsButtonPanel.ButtonPanelContentAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.flpMain.OptionsButtonPanel.ButtonPanelLocation = DevExpress.Utils.FlyoutPanelButtonPanelLocation.Bottom;
            this.flpMain.OwnerControl = this;
            this.flpMain.Size = new System.Drawing.Size(296, 256);
            this.flpMain.TabIndex = 717;
            // 
            // chkList
            // 
            this.chkList.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(51)))), ((int)(((byte)(68)))));
            this.chkList.Appearance.Font = new System.Drawing.Font("IRANSansFaNum", 16F);
            this.chkList.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(216)))), ((int)(((byte)(58)))));
            this.chkList.Appearance.Options.UseBackColor = true;
            this.chkList.Appearance.Options.UseFont = true;
            this.chkList.Appearance.Options.UseForeColor = true;
            this.chkList.CheckOnClick = true;
            this.chkList.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.UserDefined;
            this.chkList.DisplayMember = "Name";
            this.chkList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkList.HotTrackSelectMode = DevExpress.XtraEditors.HotTrackSelectMode.SelectItemOnClick;
            this.chkList.ItemPadding = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this.chkList.Location = new System.Drawing.Point(0, 0);
            this.chkList.Name = "chkList";
            this.chkList.PictureChecked = ((System.Drawing.Image)(resources.GetObject("chkList.PictureChecked")));
            this.chkList.PictureUnchecked = ((System.Drawing.Image)(resources.GetObject("chkList.PictureUnchecked")));
            this.chkList.Size = new System.Drawing.Size(296, 207);
            this.chkList.TabIndex = 452;
            this.chkList.ValueMember = "ID";
            // 
            // btnSave
            // 
            this.btnSave.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(216)))), ((int)(((byte)(58)))));
            this.btnSave.Appearance.Font = new System.Drawing.Font("IRANSansFaNum", 16F, System.Drawing.FontStyle.Bold);
            this.btnSave.Appearance.ForeColor = System.Drawing.Color.Black;
            this.btnSave.Appearance.Options.UseBackColor = true;
            this.btnSave.Appearance.Options.UseFont = true;
            this.btnSave.Appearance.Options.UseForeColor = true;
            this.btnSave.AppearanceHovered.BackColor = System.Drawing.Color.White;
            this.btnSave.AppearanceHovered.Font = new System.Drawing.Font("IRANSansFaNum", 20F);
            this.btnSave.AppearanceHovered.Options.UseBackColor = true;
            this.btnSave.AppearanceHovered.Options.UseFont = true;
            this.btnSave.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnSave.Location = new System.Drawing.Point(0, 207);
            this.btnSave.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Style3D;
            this.btnSave.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btnSave.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.btnSave.Name = "btnSave";
            this.btnSave.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btnSave.Size = new System.Drawing.Size(296, 49);
            this.btnSave.TabIndex = 451;
            this.btnSave.Text = "تائید";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lblPersTypeName
            // 
            this.lblPersTypeName.Appearance.ForeColor = System.Drawing.Color.White;
            this.lblPersTypeName.Appearance.Options.UseForeColor = true;
            this.lblPersTypeName.Appearance.Options.UseTextOptions = true;
            this.lblPersTypeName.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lblPersTypeName.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblPersTypeName.LineOrientation = DevExpress.XtraEditors.LabelLineOrientation.Vertical;
            this.lblPersTypeName.Location = new System.Drawing.Point(52, 12);
            this.lblPersTypeName.Name = "lblPersTypeName";
            this.lblPersTypeName.Size = new System.Drawing.Size(29, 174);
            this.lblPersTypeName.TabIndex = 716;
            this.lblPersTypeName.Paint += new System.Windows.Forms.PaintEventHandler(this.lblPersTypeName_Paint);
            // 
            // picPers
            // 
            this.picPers.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.picPers.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picPers.Location = new System.Drawing.Point(83, 12);
            this.picPers.Name = "picPers";
            this.picPers.Size = new System.Drawing.Size(135, 174);
            this.picPers.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picPers.TabIndex = 434;
            this.picPers.TabStop = false;
            // 
            // lblPersID
            // 
            this.lblPersID.Appearance.ForeColor = System.Drawing.Color.White;
            this.lblPersID.Appearance.Options.UseForeColor = true;
            this.lblPersID.Appearance.Options.UseTextOptions = true;
            this.lblPersID.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lblPersID.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblPersID.LineOrientation = DevExpress.XtraEditors.LabelLineOrientation.Vertical;
            this.lblPersID.Location = new System.Drawing.Point(210, 12);
            this.lblPersID.Name = "lblPersID";
            this.lblPersID.Size = new System.Drawing.Size(69, 174);
            this.lblPersID.TabIndex = 715;
            this.lblPersID.Paint += new System.Windows.Forms.PaintEventHandler(this.lblPersID_Paint);
            // 
            // btn2
            // 
            this.btn2.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.btn2.Appearance.Font = new System.Drawing.Font("IRANSansFaNum", 20F, System.Drawing.FontStyle.Bold);
            this.btn2.Appearance.ForeColor = System.Drawing.Color.Black;
            this.btn2.Appearance.Options.UseBackColor = true;
            this.btn2.Appearance.Options.UseFont = true;
            this.btn2.Appearance.Options.UseForeColor = true;
            this.btn2.AppearanceHovered.Font = new System.Drawing.Font("IRANSansFaNum", 20F);
            this.btn2.AppearanceHovered.Options.UseFont = true;
            this.btn2.BackgroundImage = global::Saman.Properties.Resources._33;
            this.btn2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn2.Location = new System.Drawing.Point(112, 194);
            this.btn2.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat;
            this.btn2.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btn2.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.btn2.Name = "btn2";
            this.btn2.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btn2.Size = new System.Drawing.Size(76, 52);
            this.btn2.TabIndex = 714;
            this.btn2.Click += new System.EventHandler(this.btn2_Click);
            // 
            // btn1
            // 
            this.btn1.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.btn1.Appearance.Font = new System.Drawing.Font("IRANSansFaNum", 20F, System.Drawing.FontStyle.Bold);
            this.btn1.Appearance.ForeColor = System.Drawing.Color.Black;
            this.btn1.Appearance.Options.UseBackColor = true;
            this.btn1.Appearance.Options.UseFont = true;
            this.btn1.Appearance.Options.UseForeColor = true;
            this.btn1.AppearanceHovered.Font = new System.Drawing.Font("IRANSansFaNum", 20F);
            this.btn1.AppearanceHovered.Options.UseFont = true;
            this.btn1.BackgroundImage = global::Saman.Properties.Resources._22;
            this.btn1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn1.Location = new System.Drawing.Point(196, 194);
            this.btn1.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat;
            this.btn1.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btn1.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.btn1.Name = "btn1";
            this.btn1.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btn1.Size = new System.Drawing.Size(76, 52);
            this.btn1.TabIndex = 713;
            this.btn1.Click += new System.EventHandler(this.btn1_Click);
            // 
            // btn3
            // 
            this.btn3.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.btn3.Appearance.Font = new System.Drawing.Font("IRANSansFaNum", 20F, System.Drawing.FontStyle.Bold);
            this.btn3.Appearance.ForeColor = System.Drawing.Color.Black;
            this.btn3.Appearance.Options.UseBackColor = true;
            this.btn3.Appearance.Options.UseFont = true;
            this.btn3.Appearance.Options.UseForeColor = true;
            this.btn3.AppearanceHovered.Font = new System.Drawing.Font("IRANSansFaNum", 20F);
            this.btn3.AppearanceHovered.Options.UseFont = true;
            this.btn3.BackgroundImage = global::Saman.Properties.Resources._55;
            this.btn3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn3.Location = new System.Drawing.Point(28, 194);
            this.btn3.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat;
            this.btn3.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btn3.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.btn3.Name = "btn3";
            this.btn3.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btn3.Size = new System.Drawing.Size(76, 52);
            this.btn3.TabIndex = 712;
            this.btn3.Click += new System.EventHandler(this.btn3_Click);
            // 
            // UcPComment
            // 
            this.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.Appearance.BorderColor = System.Drawing.Color.White;
            this.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.Appearance.Options.UseBackColor = true;
            this.Appearance.Options.UseBorderColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Controls.Add(this.panMain);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "UcPComment";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Size = new System.Drawing.Size(296, 256);
            this.Load += new System.EventHandler(this.UcPComment_Load);
            this.panMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.flpMain)).EndInit();
            this.flpMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPers)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panMain;
        private DevExpress.XtraEditors.SimpleButton btn2;
        private DevExpress.XtraEditors.SimpleButton btn1;
        private DevExpress.XtraEditors.SimpleButton btn3;
        private System.Windows.Forms.PictureBox picPers;
        private DevExpress.XtraEditors.LabelControl lblPersID;
        private DevExpress.XtraEditors.LabelControl lblPersTypeName;
        private DevExpress.Utils.FlyoutPanel flpMain;
        private DevExpress.XtraEditors.CheckedListBoxControl chkList;
        private DevExpress.XtraEditors.SimpleButton btnSave;
    }
}
