namespace Saman.Form.Enc
{
    partial class FrmEncSearch
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmEncSearch));
            this.barButtonItem7 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem8 = new DevExpress.XtraBars.BarButtonItem();
            this.btnPatientNew = new DevExpress.XtraEditors.SimpleButton();
            this.grdSearch = new DevExpress.XtraGrid.GridControl();
            this.grvSearch = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn14 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnPatientSelect = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.grdSearch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvSearch)).BeginInit();
            this.SuspendLayout();
            // 
            // barButtonItem7
            // 
            this.barButtonItem7.Caption = "پذیرش";
            this.barButtonItem7.Id = 0;
            this.barButtonItem7.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barButtonItem7.ImageOptions.SvgImage")));
            this.barButtonItem7.ImageOptions.SvgImageSize = new System.Drawing.Size(32, 32);
            this.barButtonItem7.Name = "barButtonItem7";
            this.barButtonItem7.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // barButtonItem8
            // 
            this.barButtonItem8.Caption = "پذیرش";
            this.barButtonItem8.Id = 0;
            this.barButtonItem8.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barButtonItem8.ImageOptions.SvgImage")));
            this.barButtonItem8.ImageOptions.SvgImageSize = new System.Drawing.Size(32, 32);
            this.barButtonItem8.Name = "barButtonItem8";
            this.barButtonItem8.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // btnPatientNew
            // 
            this.btnPatientNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPatientNew.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(90)))), ((int)(((byte)(158)))));
            this.btnPatientNew.Appearance.ForeColor = System.Drawing.Color.White;
            this.btnPatientNew.Appearance.Options.UseBackColor = true;
            this.btnPatientNew.Appearance.Options.UseForeColor = true;
            this.btnPatientNew.AppearanceHovered.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.btnPatientNew.AppearanceHovered.Font = new System.Drawing.Font("IRANSansFaNum", 9F, System.Drawing.FontStyle.Bold);
            this.btnPatientNew.AppearanceHovered.Options.UseBackColor = true;
            this.btnPatientNew.AppearanceHovered.Options.UseFont = true;
            this.btnPatientNew.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnPatientNew.Location = new System.Drawing.Point(267, 417);
            this.btnPatientNew.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            this.btnPatientNew.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btnPatientNew.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.btnPatientNew.Name = "btnPatientNew";
            this.btnPatientNew.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btnPatientNew.Size = new System.Drawing.Size(150, 35);
            this.btnPatientNew.TabIndex = 447;
            this.btnPatientNew.Text = "بیمار جدید";
            this.btnPatientNew.Click += new System.EventHandler(this.btnPatientNew_Click);
            // 
            // grdSearch
            // 
            this.grdSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdSearch.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.grdSearch.Location = new System.Drawing.Point(41, 14);
            this.grdSearch.MainView = this.grvSearch;
            this.grdSearch.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.grdSearch.Name = "grdSearch";
            this.grdSearch.Size = new System.Drawing.Size(865, 375);
            this.grdSearch.TabIndex = 0;
            this.grdSearch.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvSearch});
            // 
            // grvSearch
            // 
            this.grvSearch.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn5,
            this.gridColumn9,
            this.gridColumn10,
            this.gridColumn11,
            this.gridColumn12,
            this.gridColumn13,
            this.gridColumn14});
            this.grvSearch.DetailHeight = 395;
            this.grvSearch.FixedLineWidth = 3;
            this.grvSearch.GridControl = this.grdSearch;
            this.grvSearch.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.grvSearch.Name = "grvSearch";
            this.grvSearch.OptionsBehavior.Editable = false;
            this.grvSearch.OptionsBehavior.ReadOnly = true;
            this.grvSearch.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.False;
            this.grvSearch.OptionsView.ShowFooter = true;
            this.grvSearch.OptionsView.ShowGroupPanel = false;
            this.grvSearch.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "کد ملی";
            this.gridColumn5.FieldName = "NationalCode";
            this.gridColumn5.MinWidth = 39;
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 0;
            this.gridColumn5.Width = 139;
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "نام";
            this.gridColumn9.FieldName = "FirstName";
            this.gridColumn9.MinWidth = 39;
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 2;
            this.gridColumn9.Width = 102;
            // 
            // gridColumn10
            // 
            this.gridColumn10.Caption = "نام خانوادگی";
            this.gridColumn10.FieldName = "LastName";
            this.gridColumn10.MinWidth = 39;
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 3;
            this.gridColumn10.Width = 208;
            // 
            // gridColumn11
            // 
            this.gridColumn11.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn11.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.gridColumn11.Caption = "کد بیمار";
            this.gridColumn11.FieldName = "PatientID";
            this.gridColumn11.MinWidth = 31;
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.Visible = true;
            this.gridColumn11.VisibleIndex = 1;
            this.gridColumn11.Width = 137;
            // 
            // gridColumn12
            // 
            this.gridColumn12.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn12.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.gridColumn12.Caption = "سن";
            this.gridColumn12.FieldName = "Age";
            this.gridColumn12.MinWidth = 31;
            this.gridColumn12.Name = "gridColumn12";
            this.gridColumn12.Visible = true;
            this.gridColumn12.VisibleIndex = 5;
            this.gridColumn12.Width = 43;
            // 
            // gridColumn13
            // 
            this.gridColumn13.Caption = "نام پدر";
            this.gridColumn13.FieldName = "FatherName";
            this.gridColumn13.MinWidth = 25;
            this.gridColumn13.Name = "gridColumn13";
            this.gridColumn13.Visible = true;
            this.gridColumn13.VisibleIndex = 4;
            this.gridColumn13.Width = 119;
            // 
            // gridColumn14
            // 
            this.gridColumn14.Caption = "موبایل";
            this.gridColumn14.FieldName = "Mobile";
            this.gridColumn14.MinWidth = 25;
            this.gridColumn14.Name = "gridColumn14";
            this.gridColumn14.Visible = true;
            this.gridColumn14.VisibleIndex = 6;
            this.gridColumn14.Width = 80;
            // 
            // btnPatientSelect
            // 
            this.btnPatientSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPatientSelect.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(90)))), ((int)(((byte)(158)))));
            this.btnPatientSelect.Appearance.ForeColor = System.Drawing.Color.White;
            this.btnPatientSelect.Appearance.Options.UseBackColor = true;
            this.btnPatientSelect.Appearance.Options.UseForeColor = true;
            this.btnPatientSelect.AppearanceHovered.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.btnPatientSelect.AppearanceHovered.Font = new System.Drawing.Font("IRANSansFaNum", 9F, System.Drawing.FontStyle.Bold);
            this.btnPatientSelect.AppearanceHovered.Options.UseBackColor = true;
            this.btnPatientSelect.AppearanceHovered.Options.UseFont = true;
            this.btnPatientSelect.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnPatientSelect.Location = new System.Drawing.Point(504, 417);
            this.btnPatientSelect.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            this.btnPatientSelect.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btnPatientSelect.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.btnPatientSelect.Name = "btnPatientSelect";
            this.btnPatientSelect.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btnPatientSelect.Size = new System.Drawing.Size(150, 35);
            this.btnPatientSelect.TabIndex = 556;
            this.btnPatientSelect.Text = "انتخاب بیمار";
            this.btnPatientSelect.Click += new System.EventHandler(this.btnPatientSelect_Click);
            // 
            // FrmEncSearch
            // 
            this.Appearance.BackColor = System.Drawing.Color.White;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(976, 466);
            this.Controls.Add(this.btnPatientSelect);
            this.Controls.Add(this.btnPatientNew);
            this.Controls.Add(this.grdSearch);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Name = "FrmEncSearch";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "پیغام";
            this.Load += new System.EventHandler(this.FrmMessage_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmMessage_KeyPress);
            ((System.ComponentModel.ISupportInitialize)(this.grdSearch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvSearch)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraBars.BarButtonItem barButtonItem7;
        private DevExpress.XtraBars.BarButtonItem barButtonItem8;
        private DevExpress.XtraEditors.SimpleButton btnPatientNew;
        private DevExpress.XtraGrid.GridControl grdSearch;
        private DevExpress.XtraGrid.Views.Grid.GridView grvSearch;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn12;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn13;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn14;
        private DevExpress.XtraEditors.SimpleButton btnPatientSelect;
    }
}