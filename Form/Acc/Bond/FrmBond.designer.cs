namespace Saman.Form.Acc.Bond
{
    partial class FrmBond
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmBond));
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.barbtnAccBond = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnAccSide = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnAccBranch = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnBondList = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnReport = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barRegService = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem4 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.lueYear = new DevExpress.XtraBars.BarEditItem();
            this.lueMonth = new DevExpress.XtraBars.BarEditItem();
            this.barSubItem1 = new DevExpress.XtraBars.BarSubItem();
            this.BarLueYear = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.repositoryItemMarqueeProgressBar1 = new DevExpress.XtraEditors.Repository.RepositoryItemMarqueeProgressBar();
            this.BarLueMonth = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.panUC = new DevExpress.XtraEditors.PanelControl();
            this.barButtonItem7 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem8 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem3 = new DevExpress.XtraBars.BarButtonItem();
            this.bar2 = new DevExpress.XtraBars.Bar();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BarLueYear)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMarqueeProgressBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BarLueMonth)).BeginInit();
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
            this.barBtnAccBranch,
            this.barRegService,
            this.barButtonItem4,
            this.barButtonItem1,
            this.barbtnAccBond,
            this.barBtnBondList,
            this.barBtnAccSide,
            this.lueYear,
            this.lueMonth,
            this.barSubItem1,
            this.barBtnReport});
            this.barManager1.MaxItemId = 22;
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
            new DevExpress.XtraBars.LinkPersistInfo(this.barbtnAccBond),
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnAccSide),
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnAccBranch),
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnBondList),
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnReport)});
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.DrawBorder = false;
            this.bar1.Text = "Tools";
            // 
            // barbtnAccBond
            // 
            this.barbtnAccBond.Caption = "ثبت ضمانت نامه";
            this.barbtnAccBond.Id = 8;
            this.barbtnAccBond.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barbtnAccBond.ImageOptions.SvgImage")));
            this.barbtnAccBond.ImageOptions.SvgImageSize = new System.Drawing.Size(32, 32);
            this.barbtnAccBond.Name = "barbtnAccBond";
            this.barbtnAccBond.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barbtnAccBond.Tag = "1040001";
            this.barbtnAccBond.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barbtnAccBond_ItemClick);
            // 
            // barBtnAccSide
            // 
            this.barBtnAccSide.Caption = "طرف حساب";
            this.barBtnAccSide.Id = 10;
            this.barBtnAccSide.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barBtnAccSide.ImageOptions.SvgImage")));
            this.barBtnAccSide.ImageOptions.SvgImageSize = new System.Drawing.Size(32, 32);
            this.barBtnAccSide.Name = "barBtnAccSide";
            this.barBtnAccSide.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barBtnAccSide.Tag = "1040002";
            this.barBtnAccSide.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnAccSide_ItemClick);
            // 
            // barBtnAccBranch
            // 
            this.barBtnAccBranch.Caption = "شعب";
            this.barBtnAccBranch.Id = 0;
            this.barBtnAccBranch.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barBtnAccBranch.ImageOptions.SvgImage")));
            this.barBtnAccBranch.ImageOptions.SvgImageSize = new System.Drawing.Size(32, 32);
            this.barBtnAccBranch.Name = "barBtnAccBranch";
            this.barBtnAccBranch.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barBtnAccBranch.Tag = "1040003";
            // 
            // barBtnBondList
            // 
            this.barBtnBondList.Caption = "لیست ضمانت نامه";
            this.barBtnBondList.Id = 9;
            this.barBtnBondList.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barBtnBondList.ImageOptions.SvgImage")));
            this.barBtnBondList.ImageOptions.SvgImageSize = new System.Drawing.Size(32, 32);
            this.barBtnBondList.Name = "barBtnBondList";
            this.barBtnBondList.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barBtnBondList.Tag = "1040004";
            this.barBtnBondList.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnBondList_ItemClick);
            // 
            // barBtnReport
            // 
            this.barBtnReport.Caption = "گزارشات";
            this.barBtnReport.Id = 21;
            this.barBtnReport.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barBtnReport.ImageOptions.SvgImage")));
            this.barBtnReport.ImageOptions.SvgImageSize = new System.Drawing.Size(32, 32);
            this.barBtnReport.Name = "barBtnReport";
            this.barBtnReport.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Margin = new System.Windows.Forms.Padding(2);
            this.barDockControlTop.Size = new System.Drawing.Size(806, 43);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 514);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Margin = new System.Windows.Forms.Padding(2);
            this.barDockControlBottom.Size = new System.Drawing.Size(806, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 43);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Margin = new System.Windows.Forms.Padding(2);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 471);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(806, 43);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Margin = new System.Windows.Forms.Padding(2);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 471);
            // 
            // barRegService
            // 
            this.barRegService.Id = 17;
            this.barRegService.Name = "barRegService";
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
            // lueYear
            // 
            this.lueYear.Edit = null;
            this.lueYear.Id = 18;
            this.lueYear.Name = "lueYear";
            // 
            // lueMonth
            // 
            this.lueMonth.Edit = null;
            this.lueMonth.Id = 19;
            this.lueMonth.Name = "lueMonth";
            // 
            // barSubItem1
            // 
            this.barSubItem1.Caption = "barSubItem1";
            this.barSubItem1.Id = 20;
            this.barSubItem1.Name = "barSubItem1";
            // 
            // BarLueYear
            // 
            this.BarLueYear.Name = "BarLueYear";
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
            // BarLueMonth
            // 
            this.BarLueMonth.Name = "BarLueMonth";
            // 
            // panUC
            // 
            this.panUC.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.panUC.Appearance.Options.UseBackColor = true;
            this.panUC.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panUC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panUC.Location = new System.Drawing.Point(0, 43);
            this.panUC.Margin = new System.Windows.Forms.Padding(5);
            this.panUC.Name = "panUC";
            this.panUC.Size = new System.Drawing.Size(806, 471);
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
            // FrmBond
            // 
            this.Appearance.BackColor = System.Drawing.Color.White;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(806, 514);
            this.Controls.Add(this.panUC);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.IconOptions.Image = global::Saman.Properties.Resources.IconSamanNovin;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FrmBond";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Text = "مدیریت ضمانت نامه";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmBond_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BarLueYear)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMarqueeProgressBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BarLueMonth)).EndInit();
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
        private DevExpress.XtraBars.BarButtonItem barBtnAccBranch;
        private DevExpress.XtraBars.BarButtonItem barRegService;
        private DevExpress.XtraBars.BarButtonItem barButtonItem4;
        private DevExpress.XtraEditors.PanelControl panUC;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.BarButtonItem barbtnAccBond;
        private DevExpress.XtraBars.BarButtonItem barButtonItem7;
        private DevExpress.XtraBars.BarButtonItem barButtonItem8;
        private DevExpress.XtraBars.BarButtonItem barBtnBondList;
        private DevExpress.XtraBars.BarButtonItem barButtonItem3;
        private DevExpress.XtraBars.BarButtonItem barBtnAccSide;
        private DevExpress.XtraBars.BarEditItem lueYear;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit BarLueYear;
        private DevExpress.XtraBars.BarEditItem lueMonth;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit BarLueMonth;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemMarqueeProgressBar repositoryItemMarqueeProgressBar1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarButtonItem barBtnReport;
        private DevExpress.XtraBars.BarSubItem barSubItem1;
    }
}