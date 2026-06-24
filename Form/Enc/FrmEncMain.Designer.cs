namespace Saman.Form.Enc
{
    partial class FrmEncMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmEncMain));
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.barBtnHome = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnEncNewOP = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnEncNewDayCare = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnEncNewIP = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnEncSearch = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnEncInfo = new DevExpress.XtraBars.BarButtonItem();
            this.barRegService = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnSch = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnScan = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem2 = new DevExpress.XtraBars.BarButtonItem();
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
            this.barBtnHome,
            this.barBtnEncNewDayCare,
            this.barRegService,
            this.barBtnEncSearch,
            this.barBtnSch,
            this.barButtonItem1,
            this.barButtonItem2,
            this.barBtnScan,
            this.barBtnEncNewIP,
            this.barBtnEncInfo,
            this.barBtnEncNewOP});
            this.barManager1.MaxItemId = 12;
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
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.None, false, this.barBtnEncNewOP, false),
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnEncNewDayCare),
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnEncNewIP),
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnEncSearch),
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnEncInfo),
            new DevExpress.XtraBars.LinkPersistInfo(this.barRegService),
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnSch),
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnScan)});
            this.bar1.OptionsBar.DrawBorder = false;
            this.bar1.Text = "Tools";
            // 
            // barBtnHome
            // 
            this.barBtnHome.Caption = "barButtonItem10";
            this.barBtnHome.Id = 9;
            this.barBtnHome.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barBtnHome.ImageOptions.SvgImage")));
            this.barBtnHome.ImageOptions.SvgImageSize = new System.Drawing.Size(32, 32);
            this.barBtnHome.Name = "barBtnHome";
            this.barBtnHome.Tag = "10001";
            this.barBtnHome.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barEnc_ItemClick);
            // 
            // barBtnEncNewOP
            // 
            this.barBtnEncNewOP.Caption = "پذیرش سرپایی";
            this.barBtnEncNewOP.Id = 11;
            this.barBtnEncNewOP.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barBtnEncNewOP.ImageOptions.SvgImage")));
            this.barBtnEncNewOP.ImageOptions.SvgImageSize = new System.Drawing.Size(32, 32);
            this.barBtnEncNewOP.Name = "barBtnEncNewOP";
            this.barBtnEncNewOP.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barBtnEncNewOP.Tag = "10002";
            this.barBtnEncNewOP.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnEncNewOP_ItemClick);
            // 
            // barBtnEncNewDayCare
            // 
            this.barBtnEncNewDayCare.Caption = "پذیرش DayCare";
            this.barBtnEncNewDayCare.Id = 0;
            this.barBtnEncNewDayCare.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barBtnEncNewDayCare.ImageOptions.SvgImage")));
            this.barBtnEncNewDayCare.ImageOptions.SvgImageSize = new System.Drawing.Size(32, 32);
            this.barBtnEncNewDayCare.Name = "barBtnEncNewDayCare";
            this.barBtnEncNewDayCare.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barBtnEncNewDayCare.Tag = "10003";
            this.barBtnEncNewDayCare.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnEncNewDayCare_ItemClick);
            // 
            // barBtnEncNewIP
            // 
            this.barBtnEncNewIP.Caption = "پذیرش بستری";
            this.barBtnEncNewIP.Id = 8;
            this.barBtnEncNewIP.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barBtnEncNewIP.ImageOptions.SvgImage")));
            this.barBtnEncNewIP.ImageOptions.SvgImageSize = new System.Drawing.Size(32, 32);
            this.barBtnEncNewIP.Name = "barBtnEncNewIP";
            this.barBtnEncNewIP.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barBtnEncNewIP.Tag = "10004";
            this.barBtnEncNewIP.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnEncNewIP_ItemClick);
            // 
            // barBtnEncSearch
            // 
            this.barBtnEncSearch.Caption = "لیست بیماران";
            this.barBtnEncSearch.Id = 2;
            this.barBtnEncSearch.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barBtnEncSearch.ImageOptions.SvgImage")));
            this.barBtnEncSearch.ImageOptions.SvgImageSize = new System.Drawing.Size(32, 32);
            this.barBtnEncSearch.Name = "barBtnEncSearch";
            this.barBtnEncSearch.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barBtnEncSearch.Tag = "10005";
            this.barBtnEncSearch.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnEncSearch_ItemClick);
            // 
            // barBtnEncInfo
            // 
            this.barBtnEncInfo.Caption = "وضعیت بیماران";
            this.barBtnEncInfo.Id = 10;
            this.barBtnEncInfo.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barBtnEncInfo.ImageOptions.SvgImage")));
            this.barBtnEncInfo.ImageOptions.SvgImageSize = new System.Drawing.Size(32, 32);
            this.barBtnEncInfo.Name = "barBtnEncInfo";
            this.barBtnEncInfo.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barBtnEncInfo.Tag = "10006";
            this.barBtnEncInfo.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnEncStatus_ItemClick);
            // 
            // barRegService
            // 
            this.barRegService.Caption = "ثبت خدمات";
            this.barRegService.Id = 1;
            this.barRegService.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barRegService.ImageOptions.SvgImage")));
            this.barRegService.ImageOptions.SvgImageSize = new System.Drawing.Size(32, 32);
            this.barRegService.Name = "barRegService";
            this.barRegService.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barRegService.Tag = "10007";
            this.barRegService.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barRegService_ItemClick);
            // 
            // barBtnSch
            // 
            this.barBtnSch.Caption = "وقت دهی";
            this.barBtnSch.Id = 3;
            this.barBtnSch.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barBtnSch.ImageOptions.SvgImage")));
            this.barBtnSch.ImageOptions.SvgImageSize = new System.Drawing.Size(32, 32);
            this.barBtnSch.Name = "barBtnSch";
            this.barBtnSch.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barBtnSch.Tag = "10008";
            this.barBtnSch.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnSch_ItemClick);
            // 
            // barBtnScan
            // 
            this.barBtnScan.Caption = "اسکن مدارک";
            this.barBtnScan.Id = 6;
            this.barBtnScan.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barBtnScan.ImageOptions.SvgImage")));
            this.barBtnScan.ImageOptions.SvgImageSize = new System.Drawing.Size(32, 32);
            this.barBtnScan.Name = "barBtnScan";
            this.barBtnScan.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barBtnScan.Tag = "10009";
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Margin = new System.Windows.Forms.Padding(2);
            this.barDockControlTop.Size = new System.Drawing.Size(1016, 43);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 514);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Margin = new System.Windows.Forms.Padding(2);
            this.barDockControlBottom.Size = new System.Drawing.Size(1016, 0);
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
            this.barDockControlRight.Location = new System.Drawing.Point(1016, 43);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Margin = new System.Windows.Forms.Padding(2);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 471);
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
            // barButtonItem2
            // 
            this.barButtonItem2.Caption = "گزارشات";
            this.barButtonItem2.Id = 5;
            this.barButtonItem2.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barButtonItem2.ImageOptions.SvgImage")));
            this.barButtonItem2.ImageOptions.SvgImageSize = new System.Drawing.Size(32, 32);
            this.barButtonItem2.Name = "barButtonItem2";
            this.barButtonItem2.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
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
            this.panUC.Size = new System.Drawing.Size(1016, 471);
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
            // FrmEncMain
            // 
            this.Appearance.BackColor = System.Drawing.Color.White;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1016, 514);
            this.Controls.Add(this.panUC);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.IconOptions.Image = global::Saman.Properties.Resources.IconSamanNovin;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FrmEncMain";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Tag = "پذیرش";
            this.Text = "سیستم پذیرش";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmEncMain_Load);
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
        private DevExpress.XtraBars.BarButtonItem barBtnEncNewDayCare;
        private DevExpress.XtraBars.BarButtonItem barRegService;
        private DevExpress.XtraBars.BarButtonItem barBtnEncSearch;
        private DevExpress.XtraBars.BarButtonItem barBtnSch;
        private DevExpress.XtraEditors.PanelControl panUC;
        private DevExpress.XtraBars.BarButtonItem barBtnScan;
        private DevExpress.XtraBars.BarButtonItem barButtonItem2;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.BarButtonItem barBtnEncNewIP;
        private DevExpress.XtraBars.BarButtonItem barButtonItem7;
        private DevExpress.XtraBars.BarButtonItem barButtonItem8;
        private DevExpress.XtraBars.BarButtonItem barBtnHome;
        private DevExpress.XtraBars.BarButtonItem barBtnEncInfo;
        private DevExpress.XtraBars.BarButtonItem barBtnEncNewOP;
    }
}