namespace Saman.Form.Storage
{ 
    partial class frmStorageMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmStorageMain));
            this.barMain = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.lueStorage = new DevExpress.XtraBars.BarEditItem();
            this.barLueStorage = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.barBtnInvInput = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnInvOutput = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnInvStock = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnInvFromServiceReg = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnWarehouseCardex = new DevExpress.XtraBars.BarButtonItem();
            this.batBtnWarehouseHandling = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnpurchaseRequest = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barButtonItem6 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem3 = new DevExpress.XtraBars.BarButtonItem();
            this.panUC = new DevExpress.XtraEditors.PanelControl();
            this.barButtonItem7 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem8 = new DevExpress.XtraBars.BarButtonItem();
            ((System.ComponentModel.ISupportInitialize)(this.barMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barLueStorage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panUC)).BeginInit();
            this.SuspendLayout();
            // 
            // barMain
            // 
            this.barMain.AllowCustomization = false;
            this.barMain.AllowMoveBarOnToolbar = false;
            this.barMain.AllowQuickCustomization = false;
            this.barMain.AllowShowToolbarsPopup = false;
            this.barMain.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1});
            this.barMain.DockControls.Add(this.barDockControlTop);
            this.barMain.DockControls.Add(this.barDockControlBottom);
            this.barMain.DockControls.Add(this.barDockControlLeft);
            this.barMain.DockControls.Add(this.barDockControlRight);
            this.barMain.Form = this;
            this.barMain.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barBtnInvInput,
            this.barBtnWarehouseCardex,
            this.barBtnInvStock,
            this.batBtnWarehouseHandling,
            this.barBtnpurchaseRequest,
            this.barButtonItem6,
            this.barBtnInvOutput,
            this.barButtonItem3,
            this.lueStorage,
            this.barBtnInvFromServiceReg});
            this.barMain.MaxItemId = 12;
            this.barMain.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.barLueStorage});
            this.barMain.ShowScreenTipsInToolbars = false;
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.lueStorage),
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnInvInput),
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnInvOutput),
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnInvStock),
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnInvFromServiceReg),
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnWarehouseCardex),
            new DevExpress.XtraBars.LinkPersistInfo(this.batBtnWarehouseHandling),
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnpurchaseRequest)});
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.DrawBorder = false;
            this.bar1.Text = "Tools";
            // 
            // lueStorage
            // 
            this.lueStorage.CaptionAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lueStorage.Edit = this.barLueStorage;
            this.lueStorage.EditWidth = 210;
            this.lueStorage.Id = 10;
            this.lueStorage.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("lueStorage.ImageOptions.SvgImage")));
            this.lueStorage.ImageOptions.SvgImageSize = new System.Drawing.Size(32, 32);
            this.lueStorage.Name = "lueStorage";
            this.lueStorage.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.lueStorage.Tag = "40001";
            this.lueStorage.EditValueChanged += new System.EventHandler(this.lueStorage_EditValueChanged);
            // 
            // barLueStorage
            // 
            this.barLueStorage.AccessibleDescription = "انبار";
            this.barLueStorage.AccessibleName = "انبار";
            this.barLueStorage.AutoHeight = false;
            this.barLueStorage.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.barLueStorage.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("StorageID", "کد", 5, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "نام"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("StorageType", "Type")});
            this.barLueStorage.DisplayMember = "Name";
            this.barLueStorage.KeyMember = "StorageID";
            this.barLueStorage.Name = "barLueStorage";
            this.barLueStorage.NullText = "";
            this.barLueStorage.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.barLueStorage.ValueMember = "StorageID";
            // 
            // barBtnInvInput
            // 
            this.barBtnInvInput.Caption = "ورودی انبار";
            this.barBtnInvInput.Hint = "\"لطفا انبار را انتخاب نمایید\"";
            this.barBtnInvInput.Id = 0;
            this.barBtnInvInput.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barBtnInvInput.ImageOptions.SvgImage")));
            this.barBtnInvInput.ImageOptions.SvgImageSize = new System.Drawing.Size(32, 32);
            this.barBtnInvInput.Name = "barBtnInvInput";
            this.barBtnInvInput.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barBtnInvInput.Tag = "40002";
            this.barBtnInvInput.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnInvInput_ItemClick);
            // 
            // barBtnInvOutput
            // 
            this.barBtnInvOutput.Caption = "خروجی انبار";
            this.barBtnInvOutput.Hint = "\"لطفا انبار را انتخاب نمایید\"";
            this.barBtnInvOutput.Id = 8;
            this.barBtnInvOutput.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barBtnInvOutput.ImageOptions.SvgImage")));
            this.barBtnInvOutput.ImageOptions.SvgImageSize = new System.Drawing.Size(32, 32);
            this.barBtnInvOutput.Name = "barBtnInvOutput";
            this.barBtnInvOutput.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barBtnInvOutput.Tag = "40003";
            this.barBtnInvOutput.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnInvOutput_ItemClick);
            // 
            // barBtnInvStock
            // 
            this.barBtnInvStock.Caption = "موجودی";
            this.barBtnInvStock.Id = 2;
            this.barBtnInvStock.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barBtnInvStock.ImageOptions.SvgImage")));
            this.barBtnInvStock.ImageOptions.SvgImageSize = new System.Drawing.Size(32, 32);
            this.barBtnInvStock.Name = "barBtnInvStock";
            this.barBtnInvStock.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barBtnInvStock.Tag = "40004";
            this.barBtnInvStock.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnInvStock_ItemClick);
            // 
            // barBtnInvFromServiceReg
            // 
            this.barBtnInvFromServiceReg.Caption = "حواله خروج مصرفی";
            this.barBtnInvFromServiceReg.Id = 11;
            this.barBtnInvFromServiceReg.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barBtnInvFromServiceReg.ImageOptions.LargeImage")));
            this.barBtnInvFromServiceReg.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barBtnInvFromServiceReg.ImageOptions.SvgImage")));
            this.barBtnInvFromServiceReg.ImageOptions.SvgImageSize = new System.Drawing.Size(32, 32);
            this.barBtnInvFromServiceReg.Name = "barBtnInvFromServiceReg";
            this.barBtnInvFromServiceReg.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barBtnInvFromServiceReg.Tag = "40005";
            this.barBtnInvFromServiceReg.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnInvFromServiceReg_ItemClick);
            // 
            // barBtnWarehouseCardex
            // 
            this.barBtnWarehouseCardex.Caption = "کاردکس انبار";
            this.barBtnWarehouseCardex.Id = 1;
            this.barBtnWarehouseCardex.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barBtnWarehouseCardex.ImageOptions.SvgImage")));
            this.barBtnWarehouseCardex.ImageOptions.SvgImageSize = new System.Drawing.Size(32, 32);
            this.barBtnWarehouseCardex.Name = "barBtnWarehouseCardex";
            this.barBtnWarehouseCardex.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barBtnWarehouseCardex.Tag = "40006";
            this.barBtnWarehouseCardex.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnWarehouseCardex_ItemClick);
            // 
            // batBtnWarehouseHandling
            // 
            this.batBtnWarehouseHandling.Caption = "انبارگردانی";
            this.batBtnWarehouseHandling.Id = 3;
            this.batBtnWarehouseHandling.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("batBtnWarehouseHandling.ImageOptions.SvgImage")));
            this.batBtnWarehouseHandling.ImageOptions.SvgImageSize = new System.Drawing.Size(32, 32);
            this.batBtnWarehouseHandling.Name = "batBtnWarehouseHandling";
            this.batBtnWarehouseHandling.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.batBtnWarehouseHandling.Tag = "40007";
            // 
            // barBtnpurchaseRequest
            // 
            this.barBtnpurchaseRequest.Caption = "درخواست خرید";
            this.barBtnpurchaseRequest.Id = 6;
            this.barBtnpurchaseRequest.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barBtnpurchaseRequest.ImageOptions.SvgImage")));
            this.barBtnpurchaseRequest.ImageOptions.SvgImageSize = new System.Drawing.Size(32, 32);
            this.barBtnpurchaseRequest.Name = "barBtnpurchaseRequest";
            this.barBtnpurchaseRequest.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barBtnpurchaseRequest.Tag = "40008";
            this.barBtnpurchaseRequest.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnpurchaseRequest_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barMain;
            this.barDockControlTop.Margin = new System.Windows.Forms.Padding(2);
            this.barDockControlTop.Size = new System.Drawing.Size(1392, 43);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 514);
            this.barDockControlBottom.Manager = this.barMain;
            this.barDockControlBottom.Margin = new System.Windows.Forms.Padding(2);
            this.barDockControlBottom.Size = new System.Drawing.Size(1392, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 43);
            this.barDockControlLeft.Manager = this.barMain;
            this.barDockControlLeft.Margin = new System.Windows.Forms.Padding(2);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 471);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1392, 43);
            this.barDockControlRight.Manager = this.barMain;
            this.barDockControlRight.Margin = new System.Windows.Forms.Padding(2);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 471);
            // 
            // barButtonItem6
            // 
            this.barButtonItem6.Caption = "barButtonItem6";
            this.barButtonItem6.Id = 7;
            this.barButtonItem6.Name = "barButtonItem6";
            // 
            // barButtonItem3
            // 
            this.barButtonItem3.Caption = "انبار";
            this.barButtonItem3.Id = 9;
            this.barButtonItem3.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem3.ImageOptions.Image")));
            this.barButtonItem3.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItem3.ImageOptions.LargeImage")));
            this.barButtonItem3.ImageOptions.SvgImageSize = new System.Drawing.Size(32, 32);
            this.barButtonItem3.Name = "barButtonItem3";
            this.barButtonItem3.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
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
            this.panUC.Size = new System.Drawing.Size(1392, 471);
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
            // frmStorageMain
            // 
            this.Appearance.BackColor = System.Drawing.Color.White;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1392, 514);
            this.Controls.Add(this.panUC);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frmStorageMain";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Text = "سیستم انبار";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmStorageMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barLueStorage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panUC)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarManager barMain;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem barBtnInvInput;
        private DevExpress.XtraBars.BarButtonItem barBtnWarehouseCardex;
        private DevExpress.XtraBars.BarButtonItem barBtnInvStock;
        private DevExpress.XtraBars.BarButtonItem batBtnWarehouseHandling;
        private DevExpress.XtraEditors.PanelControl panUC;
        private DevExpress.XtraBars.BarButtonItem barBtnpurchaseRequest;
        private DevExpress.XtraBars.BarButtonItem barButtonItem6;
        private DevExpress.XtraBars.BarButtonItem barBtnInvOutput;
        private DevExpress.XtraBars.BarButtonItem barButtonItem7;
        private DevExpress.XtraBars.BarButtonItem barButtonItem8;
        private DevExpress.XtraBars.BarButtonItem barButtonItem3;
        private DevExpress.XtraBars.BarEditItem lueStorage;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit barLueStorage;
        private DevExpress.XtraBars.BarButtonItem barBtnInvFromServiceReg;
    }
}