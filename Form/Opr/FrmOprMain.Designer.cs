namespace Saman.Form.Opr
{
    partial class FrmOprMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmOprMain));
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.barBtnHome = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnEncNew = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnEncSearch = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnEncStatus = new DevExpress.XtraBars.BarButtonItem();
            this.barbtnPageOpr = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnRegService = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnServerRegDrug = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnInvStock = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barButtonItem6 = new DevExpress.XtraBars.BarButtonItem();
            this.panUC = new DevExpress.XtraEditors.PanelControl();
            this.barButtonItem7 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem8 = new DevExpress.XtraBars.BarButtonItem();
            this.barEnc = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem3 = new DevExpress.XtraBars.BarButtonItem();
            this.barRegService = new DevExpress.XtraBars.BarButtonItem();
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
            this.barbtnPageOpr,
            this.barBtnEncSearch,
            this.barButtonItem6,
            this.barBtnHome,
            this.barBtnRegService,
            this.barBtnServerRegDrug,
            this.barBtnEncStatus,
            this.barBtnInvStock});
            this.barManager1.MaxItemId = 15;
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
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnEncNew),
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnEncSearch, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnEncStatus),
            new DevExpress.XtraBars.LinkPersistInfo(this.barbtnPageOpr),
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnRegService),
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnServerRegDrug),
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnInvStock)});
            this.bar1.OptionsBar.AllowQuickCustomization = false;
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
            this.barBtnHome.Tag = "30001";
            this.barBtnHome.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnHome_ItemClick);
            // 
            // barBtnEncNew
            // 
            this.barBtnEncNew.Caption = "پذیرش سرپایی";
            this.barBtnEncNew.Id = 0;
            this.barBtnEncNew.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barBtnEncNew.ImageOptions.SvgImage")));
            this.barBtnEncNew.ImageOptions.SvgImageSize = new System.Drawing.Size(32, 32);
            this.barBtnEncNew.Name = "barBtnEncNew";
            this.barBtnEncNew.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barBtnEncNew.Tag = "30002";
            this.barBtnEncNew.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnEncNew_ItemClick);
            // 
            // barBtnEncSearch
            // 
            this.barBtnEncSearch.Caption = "لیست بیماران";
            this.barBtnEncSearch.Id = 2;
            this.barBtnEncSearch.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barBtnEncSearch.ImageOptions.SvgImage")));
            this.barBtnEncSearch.ImageOptions.SvgImageSize = new System.Drawing.Size(32, 32);
            this.barBtnEncSearch.Name = "barBtnEncSearch";
            this.barBtnEncSearch.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barBtnEncSearch.Tag = "30003";
            this.barBtnEncSearch.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnEncSearch_ItemClick);
            // 
            // barBtnEncStatus
            // 
            this.barBtnEncStatus.Caption = "وضعیت بیماران";
            this.barBtnEncStatus.Id = 13;
            this.barBtnEncStatus.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barBtnEncStatus.ImageOptions.SvgImage")));
            this.barBtnEncStatus.ImageOptions.SvgImageSize = new System.Drawing.Size(32, 32);
            this.barBtnEncStatus.Name = "barBtnEncStatus";
            this.barBtnEncStatus.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barBtnEncStatus.Tag = "30004";
            this.barBtnEncStatus.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnEncStatus_ItemClick);
            // 
            // barbtnPageOpr
            // 
            this.barbtnPageOpr.Caption = "جراحی بیمار";
            this.barbtnPageOpr.Id = 1;
            this.barbtnPageOpr.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barbtnPageOpr.ImageOptions.SvgImage")));
            this.barbtnPageOpr.ImageOptions.SvgImageSize = new System.Drawing.Size(32, 32);
            this.barbtnPageOpr.Name = "barbtnPageOpr";
            this.barbtnPageOpr.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barbtnPageOpr.Tag = "30005";
            this.barbtnPageOpr.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barbtnPageOpr_ItemClick);
            // 
            // barBtnRegService
            // 
            this.barBtnRegService.Caption = "ثبت خدمات";
            this.barBtnRegService.Id = 11;
            this.barBtnRegService.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barBtnRegService.ImageOptions.SvgImage")));
            this.barBtnRegService.ImageOptions.SvgImageSize = new System.Drawing.Size(32, 32);
            this.barBtnRegService.Name = "barBtnRegService";
            this.barBtnRegService.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barBtnRegService.Tag = "30006";
            this.barBtnRegService.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnRegService_ItemClick);
            // 
            // barBtnServerRegDrug
            // 
            this.barBtnServerRegDrug.Caption = "ثبت دارو و لوازم";
            this.barBtnServerRegDrug.Id = 12;
            this.barBtnServerRegDrug.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barButtonItem10.ImageOptions.SvgImage")));
            this.barBtnServerRegDrug.ImageOptions.SvgImageSize = new System.Drawing.Size(32, 32);
            this.barBtnServerRegDrug.Name = "barBtnServerRegDrug";
            this.barBtnServerRegDrug.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barBtnServerRegDrug.Tag = "30007";
            this.barBtnServerRegDrug.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnServerRegDrug_ItemClick);
            // 
            // barBtnInvStock
            // 
            this.barBtnInvStock.Caption = "موجودی";
            this.barBtnInvStock.Id = 14;
            this.barBtnInvStock.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barBtnInvStock.ImageOptions.SvgImage")));
            this.barBtnInvStock.ImageOptions.SvgImageSize = new System.Drawing.Size(32, 32);
            this.barBtnInvStock.Name = "barBtnInvStock";
            this.barBtnInvStock.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barBtnInvStock.Tag = "30008";
            this.barBtnInvStock.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnInvStock_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Margin = new System.Windows.Forms.Padding(2);
            this.barDockControlTop.Size = new System.Drawing.Size(1503, 43);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 514);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Margin = new System.Windows.Forms.Padding(2);
            this.barDockControlBottom.Size = new System.Drawing.Size(1503, 0);
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
            this.barDockControlRight.Location = new System.Drawing.Point(1503, 43);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Margin = new System.Windows.Forms.Padding(2);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 471);
            // 
            // barButtonItem6
            // 
            this.barButtonItem6.Caption = "barButtonItem6";
            this.barButtonItem6.Id = 7;
            this.barButtonItem6.Name = "barButtonItem6";
            // 
            // panUC
            // 
            this.panUC.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.panUC.Appearance.Options.UseBackColor = true;
            this.panUC.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panUC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panUC.Location = new System.Drawing.Point(0, 43);
            this.panUC.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.panUC.Name = "panUC";
            this.panUC.Size = new System.Drawing.Size(1503, 471);
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
            // barEnc
            // 
            this.barEnc.Caption = "Home";
            this.barEnc.Id = 9;
            this.barEnc.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barEnc.ImageOptions.Image")));
            this.barEnc.Name = "barEnc";
            // 
            // barButtonItem3
            // 
            this.barButtonItem3.Caption = "Home";
            this.barButtonItem3.Id = 9;
            this.barButtonItem3.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem3.ImageOptions.Image")));
            this.barButtonItem3.Name = "barButtonItem3";
            // 
            // barRegService
            // 
            this.barRegService.Caption = "ثبت خدمات";
            this.barRegService.Id = 1;
            this.barRegService.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barRegService.ImageOptions.SvgImage")));
            this.barRegService.ImageOptions.SvgImageSize = new System.Drawing.Size(32, 32);
            this.barRegService.Name = "barRegService";
            this.barRegService.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // FrmOprMain
            // 
            this.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1503, 514);
            this.Controls.Add(this.panUC);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.IconOptions.Image = global::Saman.Properties.Resources.IconSamanNovin;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FrmOprMain";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Text = "اتاق عمل";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmOprMain_Load);
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
        private DevExpress.XtraBars.BarButtonItem barbtnPageOpr;
        private DevExpress.XtraBars.BarButtonItem barBtnEncSearch;
        private DevExpress.XtraEditors.PanelControl panUC;
        private DevExpress.XtraBars.BarButtonItem barButtonItem6;
        private DevExpress.XtraBars.BarButtonItem barButtonItem7;
        private DevExpress.XtraBars.BarButtonItem barButtonItem8;
        private DevExpress.XtraBars.BarButtonItem barBtnHome;
        private DevExpress.XtraBars.BarButtonItem barEnc;
        private DevExpress.XtraBars.BarButtonItem barButtonItem3;
        private DevExpress.XtraBars.BarButtonItem barRegService;
        private DevExpress.XtraBars.BarButtonItem barBtnRegService;
        private DevExpress.XtraBars.BarButtonItem barBtnServerRegDrug;
        private DevExpress.XtraBars.BarButtonItem barBtnEncStatus;
        private DevExpress.XtraBars.BarButtonItem barBtnInvStock;
    }
}