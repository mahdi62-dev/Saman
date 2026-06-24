namespace Saman.UC
{
    partial class UcInvFromServiceReg
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UcInvFromServiceReg));
            DevExpress.XtraGrid.GridFormatRule gridFormatRule1 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleExpression formatConditionRuleExpression1 = new DevExpress.XtraEditors.FormatConditionRuleExpression();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panMain = new DevExpress.XtraEditors.PanelControl();
            this.pangrd = new System.Windows.Forms.Panel();
            this.lblPrint = new DevExpress.XtraEditors.LabelControl();
            this.grdServiceConsume = new DevExpress.XtraGrid.GridControl();
            this.grvServiceConsume = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.RepTxtConfirmNum = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.repChkHas = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnConvertToInvOutput = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl5 = new DevExpress.XtraEditors.PanelControl();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.lblTitle2 = new DevExpress.XtraEditors.LabelControl();
            this.lblTitle1 = new DevExpress.XtraEditors.LabelControl();
            this.lblTitle = new DevExpress.XtraEditors.LabelControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.lueDep = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl14 = new DevExpress.XtraEditors.LabelControl();
            this.btnRefreshInvFromServiceReg = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.datEndServiceReg = new Saman.UC.UI.UcDateTime();
            this.datStartServiceReg = new Saman.UC.UI.UcDateTime();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl4 = new DevExpress.XtraEditors.PanelControl();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.panMain)).BeginInit();
            this.panMain.SuspendLayout();
            this.pangrd.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdServiceConsume)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvServiceConsume)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RepTxtConfirmNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repChkHas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lueDep.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // gridColumn2
            // 
            this.gridColumn2.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn2.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.gridColumn2.Caption = "موجودی";
            this.gridColumn2.FieldName = "StockNum";
            this.gridColumn2.MinWidth = 22;
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.ReadOnly = true;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 4;
            this.gridColumn2.Width = 82;
            // 
            // panMain
            // 
            this.panMain.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.panMain.Appearance.BackColor = System.Drawing.Color.White;
            this.panMain.Appearance.Options.UseBackColor = true;
            this.panMain.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panMain.Controls.Add(this.pangrd);
            this.panMain.Controls.Add(this.grdServiceConsume);
            this.panMain.Controls.Add(this.btnCancel);
            this.panMain.Controls.Add(this.btnConvertToInvOutput);
            this.panMain.Controls.Add(this.panelControl5);
            this.panMain.Controls.Add(this.panelControl3);
            this.panMain.Controls.Add(this.panelControl1);
            this.panMain.Location = new System.Drawing.Point(60, 10);
            this.panMain.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panMain.Name = "panMain";
            this.panMain.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.panMain.Size = new System.Drawing.Size(1160, 700);
            this.panMain.TabIndex = 1;
            this.panMain.Paint += new System.Windows.Forms.PaintEventHandler(this.panMain_Paint);
            // 
            // pangrd
            // 
            this.pangrd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pangrd.BackColor = System.Drawing.Color.Transparent;
            this.pangrd.Controls.Add(this.lblPrint);
            this.pangrd.ForeColor = System.Drawing.Color.Transparent;
            this.pangrd.Location = new System.Drawing.Point(37, 76);
            this.pangrd.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pangrd.Name = "pangrd";
            this.pangrd.Size = new System.Drawing.Size(278, 32);
            this.pangrd.TabIndex = 730;
            // 
            // lblPrint
            // 
            this.lblPrint.AppearanceHovered.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(152)))), ((int)(((byte)(204)))), ((int)(((byte)(253)))));
            this.lblPrint.AppearanceHovered.Options.UseBackColor = true;
            this.lblPrint.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("lblPrint.ImageOptions.Image")));
            this.lblPrint.Location = new System.Drawing.Point(4, -1);
            this.lblPrint.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lblPrint.Name = "lblPrint";
            this.lblPrint.Size = new System.Drawing.Size(32, 32);
            this.lblPrint.TabIndex = 692;
            this.lblPrint.ToolTip = "پرینت";
            this.lblPrint.Click += new System.EventHandler(this.lblPrint_Click);
            // 
            // grdServiceConsume
            // 
            this.grdServiceConsume.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdServiceConsume.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.grdServiceConsume.Location = new System.Drawing.Point(36, 112);
            this.grdServiceConsume.LookAndFeel.UseDefaultLookAndFeel = false;
            this.grdServiceConsume.MainView = this.grvServiceConsume;
            this.grdServiceConsume.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.grdServiceConsume.Name = "grdServiceConsume";
            this.grdServiceConsume.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.RepTxtConfirmNum,
            this.repChkHas});
            this.grdServiceConsume.Size = new System.Drawing.Size(882, 537);
            this.grdServiceConsume.TabIndex = 706;
            this.grdServiceConsume.Tag = "حواله خروج مصرفی";
            this.grdServiceConsume.UseEmbeddedNavigator = true;
            this.grdServiceConsume.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvServiceConsume});
            // 
            // grvServiceConsume
            // 
            this.grvServiceConsume.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.grvServiceConsume.Appearance.FocusedRow.Options.UseBackColor = true;
            this.grvServiceConsume.Appearance.HeaderPanel.Font = new System.Drawing.Font("IRANSansFaNum", 10F);
            this.grvServiceConsume.Appearance.HeaderPanel.Options.UseFont = true;
            this.grvServiceConsume.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.grvServiceConsume.Appearance.HideSelectionRow.Options.UseBackColor = true;
            this.grvServiceConsume.Appearance.OddRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(230)))), ((int)(((byte)(247)))));
            this.grvServiceConsume.Appearance.OddRow.Options.UseBackColor = true;
            this.grvServiceConsume.Appearance.SelectedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.grvServiceConsume.Appearance.SelectedRow.Options.UseBackColor = true;
            this.grvServiceConsume.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn7,
            this.gridColumn5,
            this.gridColumn4,
            this.gridColumn8,
            this.gridColumn2,
            this.gridColumn1});
            this.grvServiceConsume.DetailHeight = 313;
            this.grvServiceConsume.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFullFocus;
            this.grvServiceConsume.FooterPanelHeight = 4;
            gridFormatRule1.ApplyToRow = true;
            gridFormatRule1.Column = this.gridColumn2;
            gridFormatRule1.ColumnApplyTo = this.gridColumn2;
            gridFormatRule1.Name = "Format0";
            formatConditionRuleExpression1.AllowAnimation = DevExpress.Utils.DefaultBoolean.True;
            formatConditionRuleExpression1.Appearance.BackColor = System.Drawing.Color.Red;
            formatConditionRuleExpression1.Appearance.Options.UseBackColor = true;
            formatConditionRuleExpression1.Expression = "ToFloat([StockNum]) < ToFloat([ConfirmNum])";
            gridFormatRule1.Rule = formatConditionRuleExpression1;
            this.grvServiceConsume.FormatRules.Add(gridFormatRule1);
            this.grvServiceConsume.GridControl = this.grdServiceConsume;
            this.grvServiceConsume.GroupPanelText = "جهت گروه بندی ، نام ستون را به این قسمت انتقال دهید";
            this.grvServiceConsume.Name = "grvServiceConsume";
            this.grvServiceConsume.OptionsMenu.EnableFooterMenu = false;
            this.grvServiceConsume.OptionsPrint.AutoWidth = false;
            this.grvServiceConsume.OptionsPrint.PrintFooter = false;
            this.grvServiceConsume.OptionsPrint.PrintGroupFooter = false;
            this.grvServiceConsume.OptionsPrint.RtfPageFooter = resources.GetString("grvServiceConsume.OptionsPrint.RtfPageFooter");
            this.grvServiceConsume.OptionsPrint.RtfPageHeader = resources.GetString("grvServiceConsume.OptionsPrint.RtfPageHeader");
            this.grvServiceConsume.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.grvServiceConsume.OptionsView.ColumnAutoWidth = false;
            this.grvServiceConsume.OptionsView.EnableAppearanceOddRow = true;
            this.grvServiceConsume.OptionsView.ShowAutoFilterRow = true;
            this.grvServiceConsume.PrintInitialize += new DevExpress.XtraGrid.Views.Base.PrintInitializeEventHandler(this.grvServiceConsume_PrintInitialize);
            // 
            // gridColumn7
            // 
            this.gridColumn7.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn7.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn7.Caption = "ردیف";
            this.gridColumn7.FieldName = "RecNo";
            this.gridColumn7.MinWidth = 22;
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.OptionsColumn.AllowEdit = false;
            this.gridColumn7.OptionsColumn.ReadOnly = true;
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 0;
            this.gridColumn7.Width = 44;
            // 
            // gridColumn5
            // 
            this.gridColumn5.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn5.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.gridColumn5.Caption = "نام";
            this.gridColumn5.FieldName = "ServiceName";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.ReadOnly = true;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 1;
            this.gridColumn5.Width = 234;
            // 
            // gridColumn4
            // 
            this.gridColumn4.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn4.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.gridColumn4.Caption = "تعداد";
            this.gridColumn4.FieldName = "Num";
            this.gridColumn4.MinWidth = 22;
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            this.gridColumn4.OptionsColumn.ReadOnly = true;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 2;
            this.gridColumn4.Width = 89;
            // 
            // gridColumn8
            // 
            this.gridColumn8.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn8.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.gridColumn8.Caption = "واحد";
            this.gridColumn8.FieldName = "UnitName";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.OptionsColumn.ReadOnly = true;
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 3;
            this.gridColumn8.Width = 90;
            // 
            // gridColumn1
            // 
            this.gridColumn1.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.gridColumn1.Caption = "تعداد تایید";
            this.gridColumn1.ColumnEdit = this.RepTxtConfirmNum;
            this.gridColumn1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn1.FieldName = "ConfirmNum";
            this.gridColumn1.MinWidth = 22;
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 5;
            this.gridColumn1.Width = 151;
            // 
            // RepTxtConfirmNum
            // 
            this.RepTxtConfirmNum.AutoHeight = false;
            this.RepTxtConfirmNum.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.RepTxtConfirmNum.Name = "RepTxtConfirmNum";
            // 
            // repChkHas
            // 
            this.repChkHas.AutoHeight = false;
            this.repChkHas.Name = "repChkHas";
            this.repChkHas.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(90)))), ((int)(((byte)(158)))));
            this.btnCancel.Appearance.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Appearance.Options.UseBackColor = true;
            this.btnCancel.Appearance.Options.UseForeColor = true;
            this.btnCancel.AppearanceHovered.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.btnCancel.AppearanceHovered.Font = new System.Drawing.Font("IRANSansFaNum", 9F, System.Drawing.FontStyle.Bold);
            this.btnCancel.AppearanceHovered.Options.UseBackColor = true;
            this.btnCancel.AppearanceHovered.Options.UseFont = true;
            this.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnCancel.Location = new System.Drawing.Point(36, 653);
            this.btnCancel.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            this.btnCancel.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btnCancel.Size = new System.Drawing.Size(131, 28);
            this.btnCancel.TabIndex = 699;
            this.btnCancel.Text = "بستن";
            // 
            // btnConvertToInvOutput
            // 
            this.btnConvertToInvOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConvertToInvOutput.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(90)))), ((int)(((byte)(158)))));
            this.btnConvertToInvOutput.Appearance.ForeColor = System.Drawing.Color.White;
            this.btnConvertToInvOutput.Appearance.Options.UseBackColor = true;
            this.btnConvertToInvOutput.Appearance.Options.UseForeColor = true;
            this.btnConvertToInvOutput.AppearanceDisabled.BackColor = System.Drawing.Color.Gray;
            this.btnConvertToInvOutput.AppearanceDisabled.Options.UseBackColor = true;
            this.btnConvertToInvOutput.AppearanceHovered.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.btnConvertToInvOutput.AppearanceHovered.Font = new System.Drawing.Font("IRANSansFaNum", 9F, System.Drawing.FontStyle.Bold);
            this.btnConvertToInvOutput.AppearanceHovered.Options.UseBackColor = true;
            this.btnConvertToInvOutput.AppearanceHovered.Options.UseFont = true;
            this.btnConvertToInvOutput.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnConvertToInvOutput.Location = new System.Drawing.Point(175, 653);
            this.btnConvertToInvOutput.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            this.btnConvertToInvOutput.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btnConvertToInvOutput.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.btnConvertToInvOutput.Name = "btnConvertToInvOutput";
            this.btnConvertToInvOutput.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btnConvertToInvOutput.Size = new System.Drawing.Size(131, 28);
            this.btnConvertToInvOutput.TabIndex = 698;
            this.btnConvertToInvOutput.Text = "تبدیل به سند خروجی";
            this.btnConvertToInvOutput.Click += new System.EventHandler(this.btnConvertToInvOutput_Click);
            // 
            // panelControl5
            // 
            this.panelControl5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelControl5.Appearance.BackColor = System.Drawing.Color.White;
            this.panelControl5.Appearance.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.panelControl5.Appearance.Options.UseBackColor = true;
            this.panelControl5.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl5.Location = new System.Drawing.Point(36, 108);
            this.panelControl5.Margin = new System.Windows.Forms.Padding(2);
            this.panelControl5.Name = "panelControl5";
            this.panelControl5.Size = new System.Drawing.Size(882, 4);
            this.panelControl5.TabIndex = 414;
            // 
            // panelControl3
            // 
            this.panelControl3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelControl3.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.panelControl3.Appearance.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(140)))), ((int)(((byte)(140)))));
            this.panelControl3.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.panelControl3.Appearance.Options.UseBackColor = true;
            this.panelControl3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl3.Controls.Add(this.lblTitle2);
            this.panelControl3.Controls.Add(this.lblTitle1);
            this.panelControl3.Controls.Add(this.lblTitle);
            this.panelControl3.Location = new System.Drawing.Point(36, 0);
            this.panelControl3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(882, 75);
            this.panelControl3.TabIndex = 2;
            // 
            // lblTitle2
            // 
            this.lblTitle2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTitle2.Appearance.Font = new System.Drawing.Font("IRANSansFaNum", 10F);
            this.lblTitle2.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(252)))), ((int)(((byte)(100)))));
            this.lblTitle2.Appearance.Options.UseFont = true;
            this.lblTitle2.Appearance.Options.UseForeColor = true;
            this.lblTitle2.Appearance.Options.UseTextOptions = true;
            this.lblTitle2.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.lblTitle2.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical;
            this.lblTitle2.Location = new System.Drawing.Point(651, 48);
            this.lblTitle2.Margin = new System.Windows.Forms.Padding(2);
            this.lblTitle2.Name = "lblTitle2";
            this.lblTitle2.Size = new System.Drawing.Size(205, 0);
            this.lblTitle2.TabIndex = 420;
            // 
            // lblTitle1
            // 
            this.lblTitle1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTitle1.Appearance.Font = new System.Drawing.Font("IRANSansFaNum", 10F);
            this.lblTitle1.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(252)))), ((int)(((byte)(100)))));
            this.lblTitle1.Appearance.Options.UseFont = true;
            this.lblTitle1.Appearance.Options.UseForeColor = true;
            this.lblTitle1.Appearance.Options.UseTextOptions = true;
            this.lblTitle1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lblTitle1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical;
            this.lblTitle1.Location = new System.Drawing.Point(26, 13);
            this.lblTitle1.Margin = new System.Windows.Forms.Padding(2);
            this.lblTitle1.Name = "lblTitle1";
            this.lblTitle1.Size = new System.Drawing.Size(205, 0);
            this.lblTitle1.TabIndex = 419;
            // 
            // lblTitle
            // 
            this.lblTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTitle.Appearance.Font = new System.Drawing.Font("IRANSansFaNum", 12F);
            this.lblTitle.Appearance.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Appearance.Options.UseFont = true;
            this.lblTitle.Appearance.Options.UseForeColor = true;
            this.lblTitle.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical;
            this.lblTitle.Location = new System.Drawing.Point(581, 12);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(2);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(275, 25);
            this.lblTitle.TabIndex = 417;
            this.lblTitle.Text = " حواله خروج مصرفی";
            // 
            // panelControl1
            // 
            this.panelControl1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(247)))), ((int)(((byte)(247)))));
            this.panelControl1.Appearance.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(237)))), ((int)(((byte)(241)))));
            this.panelControl1.Appearance.Options.UseBackColor = true;
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.lueDep);
            this.panelControl1.Controls.Add(this.labelControl14);
            this.panelControl1.Controls.Add(this.btnRefreshInvFromServiceReg);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.datEndServiceReg);
            this.panelControl1.Controls.Add(this.datStartServiceReg);
            this.panelControl1.Controls.Add(this.labelControl8);
            this.panelControl1.Controls.Add(this.panelControl4);
            this.panelControl1.Controls.Add(this.pictureBox1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelControl1.Location = new System.Drawing.Point(950, 0);
            this.panelControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(210, 700);
            this.panelControl1.TabIndex = 1;
            // 
            // lueDep
            // 
            this.lueDep.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lueDep.EnterMoveNextControl = true;
            this.lueDep.Location = new System.Drawing.Point(27, 244);
            this.lueDep.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lueDep.Name = "lueDep";
            this.lueDep.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(236)))), ((int)(((byte)(237)))));
            this.lueDep.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.lueDep.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.lueDep.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueDep.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("DepID", "کد", 40, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "نام", 120, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default)});
            this.lueDep.Properties.DisplayMember = "Name";
            this.lueDep.Properties.NullText = "";
            this.lueDep.Properties.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.AutoSearch;
            this.lueDep.Properties.ValueMember = "DepID";
            this.lueDep.Size = new System.Drawing.Size(156, 24);
            this.lueDep.TabIndex = 703;
            this.lueDep.EditValueChanged += new System.EventHandler(this.lueDep_EditValueChanged);
            // 
            // labelControl14
            // 
            this.labelControl14.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl14.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl14.Appearance.Options.UseForeColor = true;
            this.labelControl14.Location = new System.Drawing.Point(156, 221);
            this.labelControl14.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.labelControl14.Name = "labelControl14";
            this.labelControl14.Size = new System.Drawing.Size(28, 19);
            this.labelControl14.TabIndex = 702;
            this.labelControl14.Text = "بخش";
            // 
            // btnRefreshInvFromServiceReg
            // 
            this.btnRefreshInvFromServiceReg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefreshInvFromServiceReg.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(90)))), ((int)(((byte)(158)))));
            this.btnRefreshInvFromServiceReg.Appearance.ForeColor = System.Drawing.Color.White;
            this.btnRefreshInvFromServiceReg.Appearance.Options.UseBackColor = true;
            this.btnRefreshInvFromServiceReg.Appearance.Options.UseForeColor = true;
            this.btnRefreshInvFromServiceReg.AppearanceDisabled.BackColor = System.Drawing.Color.Gray;
            this.btnRefreshInvFromServiceReg.AppearanceDisabled.Options.UseBackColor = true;
            this.btnRefreshInvFromServiceReg.AppearanceHovered.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.btnRefreshInvFromServiceReg.AppearanceHovered.Font = new System.Drawing.Font("IRANSansFaNum", 9F, System.Drawing.FontStyle.Bold);
            this.btnRefreshInvFromServiceReg.AppearanceHovered.Options.UseBackColor = true;
            this.btnRefreshInvFromServiceReg.AppearanceHovered.Options.UseFont = true;
            this.btnRefreshInvFromServiceReg.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnRefreshInvFromServiceReg.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnRefreshInvFromServiceReg.ImageOptions.Image")));
            this.btnRefreshInvFromServiceReg.Location = new System.Drawing.Point(27, 299);
            this.btnRefreshInvFromServiceReg.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            this.btnRefreshInvFromServiceReg.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btnRefreshInvFromServiceReg.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.btnRefreshInvFromServiceReg.Name = "btnRefreshInvFromServiceReg";
            this.btnRefreshInvFromServiceReg.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btnRefreshInvFromServiceReg.Size = new System.Drawing.Size(157, 28);
            this.btnRefreshInvFromServiceReg.TabIndex = 700;
            this.btnRefreshInvFromServiceReg.Text = "بازیابی اطلاعات";
            this.btnRefreshInvFromServiceReg.Click += new System.EventHandler(this.btnRefreshInvFromServiceReg_Click);
            // 
            // labelControl2
            // 
            this.labelControl2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl2.Location = new System.Drawing.Point(148, 169);
            this.labelControl2.Margin = new System.Windows.Forms.Padding(2);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(36, 19);
            this.labelControl2.TabIndex = 419;
            this.labelControl2.Text = "تا تاریخ";
            // 
            // labelControl1
            // 
            this.labelControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl1.Location = new System.Drawing.Point(148, 117);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(2);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(36, 19);
            this.labelControl1.TabIndex = 418;
            this.labelControl1.Text = "از تاریخ";
            // 
            // datEndServiceReg
            // 
            this.datEndServiceReg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.datEndServiceReg.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.datEndServiceReg.Appearance.Options.UseBackColor = true;
            this.datEndServiceReg.Location = new System.Drawing.Point(27, 193);
            this.datEndServiceReg.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.datEndServiceReg.Name = "datEndServiceReg";
            this.datEndServiceReg.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.datEndServiceReg.Size = new System.Drawing.Size(157, 23);
            this.datEndServiceReg.TabIndex = 417;
            // 
            // datStartServiceReg
            // 
            this.datStartServiceReg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.datStartServiceReg.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.datStartServiceReg.Appearance.Options.UseBackColor = true;
            this.datStartServiceReg.Location = new System.Drawing.Point(27, 141);
            this.datStartServiceReg.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.datStartServiceReg.Name = "datStartServiceReg";
            this.datStartServiceReg.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.datStartServiceReg.Size = new System.Drawing.Size(157, 23);
            this.datStartServiceReg.TabIndex = 416;
            // 
            // labelControl8
            // 
            this.labelControl8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl8.Appearance.Font = new System.Drawing.Font("IRANSansFaNum", 9F, System.Drawing.FontStyle.Bold);
            this.labelControl8.Appearance.ForeColor = System.Drawing.Color.Gray;
            this.labelControl8.Appearance.Options.UseFont = true;
            this.labelControl8.Appearance.Options.UseForeColor = true;
            this.labelControl8.Location = new System.Drawing.Point(163, 87);
            this.labelControl8.Margin = new System.Windows.Forms.Padding(2);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(20, 19);
            this.labelControl8.TabIndex = 415;
            this.labelControl8.Text = "فیلتر";
            this.labelControl8.Visible = false;
            // 
            // panelControl4
            // 
            this.panelControl4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelControl4.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(247)))), ((int)(((byte)(247)))));
            this.panelControl4.Appearance.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(73)))));
            this.panelControl4.Appearance.Options.UseBackColor = true;
            this.panelControl4.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl4.Location = new System.Drawing.Point(30, 108);
            this.panelControl4.Margin = new System.Windows.Forms.Padding(2);
            this.panelControl4.Name = "panelControl4";
            this.panelControl4.Size = new System.Drawing.Size(156, 4);
            this.panelControl4.TabIndex = 414;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = global::Saman.Properties.Resources.male;
            this.pictureBox1.Location = new System.Drawing.Point(69, 12);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(70, 63);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox3.Image = global::Saman.Properties.Resources.bg_01;
            this.pictureBox3.Location = new System.Drawing.Point(0, 0);
            this.pictureBox3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(1280, 720);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox3.TabIndex = 419;
            this.pictureBox3.TabStop = false;
            // 
            // UcInvFromServiceReg
            // 
            this.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(174)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panMain);
            this.Controls.Add(this.pictureBox3);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "UcInvFromServiceReg";
            this.Size = new System.Drawing.Size(1280, 720);
            this.Tag = "حواله خروج هوشمند";
            this.Load += new System.EventHandler(this.UcInvFromServiceReg_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panMain)).EndInit();
            this.panMain.ResumeLayout(false);
            this.pangrd.ResumeLayout(false);
            this.pangrd.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdServiceConsume)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvServiceConsume)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RepTxtConfirmNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repChkHas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lueDep.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panMain;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.PanelControl panelControl4;
        private DevExpress.XtraEditors.PanelControl panelControl5;
        private DevExpress.XtraEditors.LabelControl lblTitle1;
        private DevExpress.XtraEditors.LabelControl lblTitle;
        private System.Windows.Forms.PictureBox pictureBox3;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnConvertToInvOutput;
        private DevExpress.XtraGrid.GridControl grdServiceConsume;
        private DevExpress.XtraGrid.Views.Grid.GridView grvServiceConsume;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraEditors.LabelControl lblTitle2;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit RepTxtConfirmNum;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repChkHas;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private UI.UcDateTime datEndServiceReg;
        private UI.UcDateTime datStartServiceReg;
        private DevExpress.XtraEditors.SimpleButton btnRefreshInvFromServiceReg;
        private DevExpress.XtraEditors.LabelControl labelControl14;
        private DevExpress.XtraEditors.LookUpEdit lueDep;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private System.Windows.Forms.Panel pangrd;
        private DevExpress.XtraEditors.LabelControl lblPrint;
    }
}
