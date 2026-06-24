namespace Saman.Form.Ward
{ 
    partial class FrmWardMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmWardMain));
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.barBtnHome = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnEncDepRec = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnEncSearch = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnEncStatus = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnServiceReg = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnServerRegDrug = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnStock = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnSch = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnScan = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnReport = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barBtnEncNew = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem6 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem9 = new DevExpress.XtraBars.BarButtonItem();
            this.panUC = new DevExpress.XtraEditors.PanelControl();
            this.barButtonItem7 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem8 = new DevExpress.XtraBars.BarButtonItem();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
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
            this.barBtnEncNew,
            this.barBtnServiceReg,
            this.barBtnEncSearch,
            this.barBtnSch,
            this.barBtnReport,
            this.barBtnScan,
            this.barButtonItem6,
            this.barButtonItem9,
            this.barBtnServerRegDrug,
            this.barBtnHome,
            this.barBtnEncStatus,
            this.barBtnEncDepRec,
            this.barBtnStock});
            this.barManager1.MaxItemId = 14;
            this.barManager1.ShowScreenTipsInToolbars = false;
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnHome),
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnEncDepRec),
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnEncSearch),
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnEncStatus),
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnServiceReg),
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnServerRegDrug),
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnStock),
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnSch),
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnScan),
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnReport)});
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.DrawBorder = false;
            this.bar1.Text = "Tools";
            // 
            // barBtnHome
            // 
            this.barBtnHome.Caption = "صفحه اصلی";
            this.barBtnHome.Id = 10;
            this.barBtnHome.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barBtnHome.ImageOptions.SvgImage")));
            this.barBtnHome.ImageOptions.SvgImageSize = new System.Drawing.Size(32, 32);
            this.barBtnHome.Name = "barBtnHome";
            this.barBtnHome.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barBtnHome.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnHome_ItemClick);
            // 
            // barBtnEncDepRec
            // 
            this.barBtnEncDepRec.Caption = "پرونده بخش";
            this.barBtnEncDepRec.Id = 12;
            this.barBtnEncDepRec.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barBtnEncDepRec.ImageOptions.SvgImage")));
            this.barBtnEncDepRec.ImageOptions.SvgImageSize = new System.Drawing.Size(32, 32);
            this.barBtnEncDepRec.Name = "barBtnEncDepRec";
            this.barBtnEncDepRec.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barBtnEncDepRec.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnEncDepRec_ItemClick);
            // 
            // barBtnEncSearch
            // 
            this.barBtnEncSearch.Caption = "لیست بیماران";
            this.barBtnEncSearch.Id = 2;
            this.barBtnEncSearch.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barBtnEncSearch.ImageOptions.SvgImage")));
            this.barBtnEncSearch.ImageOptions.SvgImageSize = new System.Drawing.Size(32, 32);
            this.barBtnEncSearch.Name = "barBtnEncSearch";
            this.barBtnEncSearch.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barBtnEncSearch.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnEncSearch_ItemClick);
            // 
            // barBtnEncStatus
            // 
            this.barBtnEncStatus.Caption = "وضعیت بیماران";
            this.barBtnEncStatus.Id = 11;
            this.barBtnEncStatus.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barBtnEncStatus.ImageOptions.SvgImage")));
            this.barBtnEncStatus.ImageOptions.SvgImageSize = new System.Drawing.Size(32, 32);
            this.barBtnEncStatus.Name = "barBtnEncStatus";
            this.barBtnEncStatus.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barBtnEncStatus.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnEncStatus_ItemClick);
            // 
            // barBtnServiceReg
            // 
            this.barBtnServiceReg.Caption = "ثبت خدمات";
            this.barBtnServiceReg.Id = 1;
            this.barBtnServiceReg.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barBtnServiceReg.ImageOptions.SvgImage")));
            this.barBtnServiceReg.ImageOptions.SvgImageSize = new System.Drawing.Size(32, 32);
            this.barBtnServiceReg.Name = "barBtnServiceReg";
            this.barBtnServiceReg.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barBtnServiceReg.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnServiceReg_ItemClick);
            // 
            // barBtnServerRegDrug
            // 
            this.barBtnServerRegDrug.Caption = "ثبت دارو و لوازم";
            this.barBtnServerRegDrug.Id = 9;
            this.barBtnServerRegDrug.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barBtnServerRegDrug.ImageOptions.SvgImage")));
            this.barBtnServerRegDrug.ImageOptions.SvgImageSize = new System.Drawing.Size(32, 32);
            this.barBtnServerRegDrug.Name = "barBtnServerRegDrug";
            this.barBtnServerRegDrug.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barBtnServerRegDrug.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnServerRegDrug_ItemClick);
            // 
            // barBtnStock
            // 
            this.barBtnStock.Caption = "موجودی";
            this.barBtnStock.Id = 13;
            this.barBtnStock.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barBtnStock.ImageOptions.SvgImage")));
            this.barBtnStock.ImageOptions.SvgImageSize = new System.Drawing.Size(32, 32);
            this.barBtnStock.Name = "barBtnStock";
            this.barBtnStock.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barBtnStock.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnStock_ItemClick);
            // 
            // barBtnSch
            // 
            this.barBtnSch.Caption = "نوبت دهی";
            this.barBtnSch.Id = 3;
            this.barBtnSch.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barBtnSch.ImageOptions.SvgImage")));
            this.barBtnSch.ImageOptions.SvgImageSize = new System.Drawing.Size(32, 32);
            this.barBtnSch.Name = "barBtnSch";
            this.barBtnSch.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // barBtnScan
            // 
            this.barBtnScan.Caption = "اسکن مدارک";
            this.barBtnScan.Id = 6;
            this.barBtnScan.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barBtnScan.ImageOptions.SvgImage")));
            this.barBtnScan.ImageOptions.SvgImageSize = new System.Drawing.Size(32, 32);
            this.barBtnScan.Name = "barBtnScan";
            this.barBtnScan.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // barBtnReport
            // 
            this.barBtnReport.Caption = "گزارشات";
            this.barBtnReport.Id = 5;
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
            this.barDockControlTop.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.barDockControlTop.Size = new System.Drawing.Size(1472, 53);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 649);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.barDockControlBottom.Size = new System.Drawing.Size(1472, 0);
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
            this.barDockControlRight.Location = new System.Drawing.Point(1472, 53);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 596);
            // 
            // barBtnEncNew
            // 
            this.barBtnEncNew.Caption = "پذیرش سرپایی";
            this.barBtnEncNew.Id = 0;
            this.barBtnEncNew.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barBtnEncNew.ImageOptions.SvgImage")));
            this.barBtnEncNew.ImageOptions.SvgImageSize = new System.Drawing.Size(32, 32);
            this.barBtnEncNew.Name = "barBtnEncNew";
            this.barBtnEncNew.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barBtnEncNew.VisibleInSearchMenu = false;
            // 
            // barButtonItem6
            // 
            this.barButtonItem6.Caption = "barButtonItem6";
            this.barButtonItem6.Id = 7;
            this.barButtonItem6.Name = "barButtonItem6";
            // 
            // barButtonItem9
            // 
            this.barButtonItem9.Caption = "پذیرش بستری";
            this.barButtonItem9.Id = 8;
            this.barButtonItem9.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barButtonItem9.ImageOptions.SvgImage")));
            this.barButtonItem9.ImageOptions.SvgImageSize = new System.Drawing.Size(32, 32);
            this.barButtonItem9.Name = "barButtonItem9";
            this.barButtonItem9.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // panUC
            // 
            this.panUC.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.panUC.Appearance.Options.UseBackColor = true;
            this.panUC.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panUC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panUC.Location = new System.Drawing.Point(0, 53);
            this.panUC.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.panUC.Name = "panUC";
            this.panUC.Size = new System.Drawing.Size(1472, 596);
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
            // FrmWardMain
            // 
            this.Appearance.BackColor = System.Drawing.Color.White;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1472, 649);
            this.Controls.Add(this.panUC);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.IconOptions.Image = global::Saman.Properties.Resources.IconSamanNovin;
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Name = "FrmWardMain";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Text = "بخش مراقبت روزانه";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmWardMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
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
        private DevExpress.XtraBars.BarButtonItem barBtnEncNew;
        private DevExpress.XtraBars.BarButtonItem barBtnServiceReg;
        private DevExpress.XtraBars.BarButtonItem barBtnEncSearch;
        private DevExpress.XtraBars.BarButtonItem barBtnSch;
        private DevExpress.XtraEditors.PanelControl panUC;
        private DevExpress.XtraBars.BarButtonItem barBtnScan;
        private DevExpress.XtraBars.BarButtonItem barBtnReport;
        private DevExpress.XtraBars.BarButtonItem barButtonItem6;
        private DevExpress.XtraBars.BarButtonItem barButtonItem9;
        private DevExpress.XtraBars.BarButtonItem barButtonItem7;
        private DevExpress.XtraBars.BarButtonItem barButtonItem8;
        private DevExpress.XtraBars.BarButtonItem barBtnServerRegDrug;
        private DevExpress.XtraBars.BarButtonItem barBtnHome;
        private DevExpress.XtraBars.BarButtonItem barBtnEncStatus;
        private DevExpress.XtraBars.BarButtonItem barBtnEncDepRec;
        private DevExpress.XtraBars.BarButtonItem barBtnStock;
    }
}