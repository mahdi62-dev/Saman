namespace Saman.Form.Payroll
{ 
    partial class FrmPayroll
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPayroll));
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.lueYear = new DevExpress.XtraBars.BarEditItem();
            this.BarLueYear = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.lueMonth = new DevExpress.XtraBars.BarEditItem();
            this.BarLueMonth = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.barbtnTimex = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnDayOff = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnSalaryItem = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnSalaryCalc = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barRegService = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem4 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.repositoryItemMarqueeProgressBar1 = new DevExpress.XtraEditors.Repository.RepositoryItemMarqueeProgressBar();
            this.panUC = new DevExpress.XtraEditors.PanelControl();
            this.barButtonItem7 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem8 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem3 = new DevExpress.XtraBars.BarButtonItem();
            this.bar2 = new DevExpress.XtraBars.Bar();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BarLueYear)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BarLueMonth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMarqueeProgressBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panUC)).BeginInit();
            this.SuspendLayout();
            // 
            // barManager1
            // 
            this.barManager1.AllowCustomization = false;
            this.barManager1.AllowMoveBarOnToolbar = false;
            this.barManager1.AllowQuickCustomization = false;
            this.barManager1.AllowShowToolbarsPopup = false;
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barBtnSalaryItem,
            this.barRegService,
            this.barButtonItem4,
            this.barButtonItem1,
            this.barbtnTimex,
            this.barBtnSalaryCalc,
            this.barBtnDayOff,
            this.lueYear,
            this.lueMonth});
            this.barManager1.MaxItemId = 17;
            this.barManager1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.BarLueYear,
            this.repositoryItemTextEdit1,
            this.repositoryItemMarqueeProgressBar1,
            this.BarLueMonth});
            this.barManager1.ShowScreenTipsInToolbars = false;
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.lueYear, DevExpress.XtraBars.BarItemPaintStyle.Caption),
            new DevExpress.XtraBars.LinkPersistInfo(this.lueMonth),
            new DevExpress.XtraBars.LinkPersistInfo(this.barbtnTimex),
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnDayOff),
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnSalaryItem),
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnSalaryCalc)});
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.DrawBorder = false;
            this.bar1.Text = "Tools";
            // 
            // lueYear
            // 
            this.lueYear.Caption = "سال";
            this.lueYear.CaptionAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lueYear.CaptionToEditorIndent = 0;
            this.lueYear.Edit = this.BarLueYear;
            this.lueYear.EditWidth = 100;
            this.lueYear.Id = 13;
            this.lueYear.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("lueYear.ImageOptions.SvgImage")));
            this.lueYear.ImageOptions.SvgImageSize = new System.Drawing.Size(32, 32);
            this.lueYear.Name = "lueYear";
            this.lueYear.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionInMenu;
            this.lueYear.Tag = "1010001";
            this.lueYear.EditValueChanged += new System.EventHandler(this.lueYear_EditValueChanged);
            // 
            // BarLueYear
            // 
            this.BarLueYear.Appearance.Font = new System.Drawing.Font("IRANSansFaNum", 11F);
            this.BarLueYear.Appearance.Options.UseFont = true;
            this.BarLueYear.AutoHeight = false;
            this.BarLueYear.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.BarLueYear.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.BarLueYear.DisplayMember = "Name";
            this.BarLueYear.KeyMember = "ID";
            this.BarLueYear.Name = "BarLueYear";
            this.BarLueYear.PopupWidth = 20;
            this.BarLueYear.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.BarLueYear.ValueMember = "ID";
            // 
            // lueMonth
            // 
            this.lueMonth.Caption = "ماه";
            this.lueMonth.CaptionAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lueMonth.Edit = this.BarLueMonth;
            this.lueMonth.EditWidth = 120;
            this.lueMonth.Id = 16;
            this.lueMonth.Name = "lueMonth";
            this.lueMonth.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.Caption;
            this.lueMonth.Tag = "1010002";
            this.lueMonth.EditValueChanged += new System.EventHandler(this.lueMonth_EditValueChanged);
            // 
            // BarLueMonth
            // 
            this.BarLueMonth.Appearance.Font = new System.Drawing.Font("IRANSansFaNum", 11F);
            this.BarLueMonth.Appearance.Options.UseFont = true;
            this.BarLueMonth.AutoHeight = false;
            this.BarLueMonth.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.BarLueMonth.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.BarLueMonth.DisplayMember = "Name";
            this.BarLueMonth.KeyMember = "ID";
            this.BarLueMonth.Name = "BarLueMonth";
            this.BarLueMonth.ValueMember = "ID";
            // 
            // barbtnTimex
            // 
            this.barbtnTimex.Caption = "تایمکس";
            this.barbtnTimex.Id = 8;
            this.barbtnTimex.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barbtnTimex.ImageOptions.SvgImage")));
            this.barbtnTimex.ImageOptions.SvgImageSize = new System.Drawing.Size(32, 32);
            this.barbtnTimex.Name = "barbtnTimex";
            this.barbtnTimex.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barbtnTimex.Tag = "1010003";
            this.barbtnTimex.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barbtnTimex_ItemClick);
            // 
            // barBtnDayOff
            // 
            this.barBtnDayOff.Caption = "ثبت مرخصی ";
            this.barBtnDayOff.Id = 10;
            this.barBtnDayOff.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barBtnDayOff.ImageOptions.SvgImage")));
            this.barBtnDayOff.ImageOptions.SvgImageSize = new System.Drawing.Size(32, 32);
            this.barBtnDayOff.Name = "barBtnDayOff";
            this.barBtnDayOff.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barBtnDayOff.Tag = "1010004";
            this.barBtnDayOff.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnDayOff_ItemClick);
            // 
            // barBtnSalaryItem
            // 
            this.barBtnSalaryItem.Caption = " ثبت اقلام حقوق و دستمزد";
            this.barBtnSalaryItem.Id = 0;
            this.barBtnSalaryItem.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barBtnSalaryItem.ImageOptions.SvgImage")));
            this.barBtnSalaryItem.ImageOptions.SvgImageSize = new System.Drawing.Size(32, 32);
            this.barBtnSalaryItem.Name = "barBtnSalaryItem";
            this.barBtnSalaryItem.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barBtnSalaryItem.Tag = "1010005";
            this.barBtnSalaryItem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnSalaryItem_ItemClick);
            // 
            // barBtnSalaryCalc
            // 
            this.barBtnSalaryCalc.Caption = "محاسبه حقوق";
            this.barBtnSalaryCalc.Id = 9;
            this.barBtnSalaryCalc.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barBtnSalaryCalc.ImageOptions.SvgImage")));
            this.barBtnSalaryCalc.ImageOptions.SvgImageSize = new System.Drawing.Size(32, 32);
            this.barBtnSalaryCalc.Name = "barBtnSalaryCalc";
            this.barBtnSalaryCalc.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barBtnSalaryCalc.Tag = "1010006";
            this.barBtnSalaryCalc.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnSalaryCalc_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.barDockControlTop.Size = new System.Drawing.Size(1218, 53);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 649);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.barDockControlBottom.Size = new System.Drawing.Size(1218, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 53);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 596);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1218, 53);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 596);
            // 
            // barRegService
            // 
            this.barRegService.Caption = "ثبت خدمات";
            this.barRegService.Id = 1;
            this.barRegService.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barRegService.ImageOptions.SvgImage")));
            this.barRegService.ImageOptions.SvgImageSize = new System.Drawing.Size(32, 32);
            this.barRegService.Name = "barRegService";
            this.barRegService.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barRegService.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barRegService_ItemClick);
            // 
            // barButtonItem4
            // 
            this.barButtonItem4.Id = 11;
            this.barButtonItem4.Name = "barButtonItem4";
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "تنظیمات";
            this.barButtonItem1.Id = 4;
            this.barButtonItem1.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barButtonItem1.ImageOptions.SvgImage")));
            this.barButtonItem1.ImageOptions.SvgImageSize = new System.Drawing.Size(32, 32);
            this.barButtonItem1.Name = "barButtonItem1";
            this.barButtonItem1.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            // 
            // repositoryItemMarqueeProgressBar1
            // 
            this.repositoryItemMarqueeProgressBar1.Name = "repositoryItemMarqueeProgressBar1";
            // 
            // panUC
            // 
            this.panUC.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.panUC.Appearance.Options.UseBackColor = true;
            this.panUC.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panUC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panUC.Location = new System.Drawing.Point(0, 53);
            this.panUC.Margin = new System.Windows.Forms.Padding(6);
            this.panUC.Name = "panUC";
            this.panUC.Size = new System.Drawing.Size(1218, 596);
            this.panUC.TabIndex = 10;
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
            // barButtonItem3
            // 
            this.barButtonItem3.Caption = "کسورات حقوق";
            this.barButtonItem3.Id = 8;
            this.barButtonItem3.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barButtonItem3.ImageOptions.SvgImage")));
            this.barButtonItem3.ImageOptions.SvgImageSize = new System.Drawing.Size(32, 32);
            this.barButtonItem3.Name = "barButtonItem3";
            this.barButtonItem3.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // bar2
            // 
            this.bar2.BarName = "Custom 3";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 1;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.Text = "Custom 3";
            // 
            // FrmPayroll
            // 
            this.Appearance.BackColor = System.Drawing.Color.White;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1218, 649);
            this.Controls.Add(this.panUC);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.IconOptions.Image = global::Saman.Properties.Resources.IconSamanNovin;
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Name = "FrmPayroll";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Text = "سیستم حقوق و دستمزد";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmPayroll_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BarLueYear)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BarLueMonth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMarqueeProgressBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panUC)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem barBtnSalaryItem;
        private DevExpress.XtraBars.BarButtonItem barRegService;
        private DevExpress.XtraBars.BarButtonItem barButtonItem4;
        private DevExpress.XtraEditors.PanelControl panUC;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.BarButtonItem barbtnTimex;
        private DevExpress.XtraBars.BarButtonItem barButtonItem7;
        private DevExpress.XtraBars.BarButtonItem barButtonItem8;
        private DevExpress.XtraBars.BarButtonItem barBtnSalaryCalc;
        private DevExpress.XtraBars.BarButtonItem barButtonItem3;
        private DevExpress.XtraBars.BarButtonItem barBtnDayOff;
        private DevExpress.XtraBars.BarEditItem lueYear;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit BarLueYear;
        private DevExpress.XtraBars.BarEditItem lueMonth;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit BarLueMonth;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemMarqueeProgressBar repositoryItemMarqueeProgressBar1;
        private DevExpress.XtraBars.Bar bar2;
    }
}