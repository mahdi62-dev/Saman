namespace Saman.UC.SAC
{
    partial class UcSACUserPermission
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UcSACUserPermission));
            this.panMain = new DevExpress.XtraEditors.PanelControl();
            this.treSac = new DevExpress.XtraTreeList.TreeList();
            this.treColSACName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl5 = new DevExpress.XtraEditors.PanelControl();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.labelControl13 = new DevExpress.XtraEditors.LabelControl();
            this.lblTitle2 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.lblPers1 = new DevExpress.XtraEditors.LabelControl();
            this.luePers1 = new DevExpress.XtraEditors.LookUpEdit();
            this.btnSACReportCopy = new DevExpress.XtraEditors.SimpleButton();
            this.luePers = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.panDetail = new System.Windows.Forms.Panel();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.panMain)).BeginInit();
            this.panMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treSac)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.luePers1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.luePers.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // panMain
            // 
            this.panMain.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.panMain.Appearance.BackColor = System.Drawing.Color.White;
            this.panMain.Appearance.Options.UseBackColor = true;
            this.panMain.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panMain.Controls.Add(this.treSac);
            this.panMain.Controls.Add(this.btnCancel);
            this.panMain.Controls.Add(this.btnSave);
            this.panMain.Controls.Add(this.labelControl4);
            this.panMain.Controls.Add(this.panelControl5);
            this.panMain.Controls.Add(this.panelControl3);
            this.panMain.Controls.Add(this.panelControl1);
            this.panMain.Location = new System.Drawing.Point(60, 10);
            this.panMain.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panMain.Name = "panMain";
            this.panMain.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.panMain.Size = new System.Drawing.Size(1160, 700);
            this.panMain.TabIndex = 1;
            // 
            // treSac
            // 
            this.treSac.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treSac.Appearance.FocusedCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.treSac.Appearance.FocusedCell.Options.UseBackColor = true;
            this.treSac.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.treSac.BandPanelRowHeight = 0;
            this.treSac.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.treSac.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treColSACName});
            this.treSac.Location = new System.Drawing.Point(40, 115);
            this.treSac.Name = "treSac";
            this.treSac.OptionsBehavior.AllowExpandAnimation = DevExpress.Utils.DefaultBoolean.True;
            this.treSac.OptionsBehavior.Editable = false;
            this.treSac.OptionsBehavior.ReadOnly = true;
            this.treSac.OptionsFilter.ExpandNodesOnFiltering = true;
            this.treSac.OptionsFind.AllowIncrementalSearch = true;
            this.treSac.OptionsFind.Behavior = DevExpress.XtraEditors.FindPanelBehavior.Search;
            this.treSac.OptionsFind.ExpandNodesOnIncrementalSearch = true;
            this.treSac.OptionsFind.FindMode = DevExpress.XtraTreeList.FindMode.Always;
            this.treSac.OptionsFind.FindNullPrompt = "نام گزارش .....";
            this.treSac.OptionsFind.ShowSearchNavButtons = false;
            this.treSac.OptionsNavigation.AutoMoveRowFocus = true;
            this.treSac.OptionsScrollAnnotations.ShowFocusedRow = DevExpress.Utils.DefaultBoolean.True;
            this.treSac.OptionsSelection.MultiSelect = true;
            this.treSac.OptionsSelection.SelectNodesOnRightClick = true;
            this.treSac.OptionsSelection.UseIndicatorForSelection = true;
            this.treSac.OptionsView.CheckBoxStyle = DevExpress.XtraTreeList.DefaultNodeCheckBoxStyle.Check;
            this.treSac.OptionsView.FocusRectStyle = DevExpress.XtraTreeList.DrawFocusRectStyle.RowFullFocus;
            this.treSac.OptionsView.ShowAutoFilterRow = true;
            this.treSac.OptionsView.ShowBandsMode = DevExpress.Utils.DefaultBoolean.False;
            this.treSac.OptionsView.ShowTreeLines = DevExpress.Utils.DefaultBoolean.True;
            this.treSac.OptionsView.TreeLineStyle = DevExpress.XtraTreeList.LineStyle.Dark;
            this.treSac.OptionsView.WaitAnimationOptions = DevExpress.XtraEditors.WaitAnimationOptions.Indicator;
            this.treSac.ShowButtonMode = DevExpress.XtraTreeList.ShowButtonModeEnum.ShowForFocusedRow;
            this.treSac.Size = new System.Drawing.Size(878, 531);
            this.treSac.TabIndex = 444;
            this.treSac.ViewStyle = DevExpress.XtraTreeList.TreeListViewStyle.TreeView;
            // 
            // treColSACName
            // 
            this.treColSACName.Caption = "نام";
            this.treColSACName.FieldName = "ActionName";
            this.treColSACName.MinWidth = 17;
            this.treColSACName.Name = "treColSACName";
            this.treColSACName.Visible = true;
            this.treColSACName.VisibleIndex = 0;
            this.treColSACName.Width = 66;
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
            this.btnCancel.Location = new System.Drawing.Point(38, 651);
            this.btnCancel.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            this.btnCancel.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btnCancel.Size = new System.Drawing.Size(131, 28);
            this.btnCancel.TabIndex = 443;
            this.btnCancel.Text = "انصراف";
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(90)))), ((int)(((byte)(158)))));
            this.btnSave.Appearance.ForeColor = System.Drawing.Color.White;
            this.btnSave.Appearance.Options.UseBackColor = true;
            this.btnSave.Appearance.Options.UseForeColor = true;
            this.btnSave.AppearanceHovered.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.btnSave.AppearanceHovered.Font = new System.Drawing.Font("IRANSansFaNum", 9F, System.Drawing.FontStyle.Bold);
            this.btnSave.AppearanceHovered.Options.UseBackColor = true;
            this.btnSave.AppearanceHovered.Options.UseFont = true;
            this.btnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnSave.Location = new System.Drawing.Point(177, 651);
            this.btnSave.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            this.btnSave.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btnSave.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.btnSave.Name = "btnSave";
            this.btnSave.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btnSave.Size = new System.Drawing.Size(131, 28);
            this.btnSave.TabIndex = 442;
            this.btnSave.Text = "ذخیره";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // labelControl4
            // 
            this.labelControl4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl4.Appearance.Font = new System.Drawing.Font("IRANSansFaNum", 9F, System.Drawing.FontStyle.Bold);
            this.labelControl4.Appearance.ForeColor = System.Drawing.Color.Gray;
            this.labelControl4.Appearance.Options.UseFont = true;
            this.labelControl4.Appearance.Options.UseForeColor = true;
            this.labelControl4.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical;
            this.labelControl4.Location = new System.Drawing.Point(307, 85);
            this.labelControl4.Margin = new System.Windows.Forms.Padding(2);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(611, 19);
            this.labelControl4.TabIndex = 441;
            this.labelControl4.Text = "لیست دسترسی";
            // 
            // panelControl5
            // 
            this.panelControl5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelControl5.Appearance.BackColor = System.Drawing.Color.White;
            this.panelControl5.Appearance.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.panelControl5.Appearance.Options.UseBackColor = true;
            this.panelControl5.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl5.Location = new System.Drawing.Point(40, 106);
            this.panelControl5.Margin = new System.Windows.Forms.Padding(2);
            this.panelControl5.Name = "panelControl5";
            this.panelControl5.Size = new System.Drawing.Size(880, 4);
            this.panelControl5.TabIndex = 440;
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
            this.panelControl3.Controls.Add(this.labelControl3);
            this.panelControl3.Controls.Add(this.labelControl13);
            this.panelControl3.Controls.Add(this.lblTitle2);
            this.panelControl3.Location = new System.Drawing.Point(36, 0);
            this.panelControl3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(882, 75);
            this.panelControl3.TabIndex = 2;
            // 
            // labelControl13
            // 
            this.labelControl13.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl13.Appearance.ForeColor = System.Drawing.Color.White;
            this.labelControl13.Appearance.Options.UseForeColor = true;
            this.labelControl13.Appearance.Options.UseTextOptions = true;
            this.labelControl13.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labelControl13.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical;
            this.labelControl13.Location = new System.Drawing.Point(126, 48);
            this.labelControl13.Margin = new System.Windows.Forms.Padding(2);
            this.labelControl13.Name = "labelControl13";
            this.labelControl13.Size = new System.Drawing.Size(205, 0);
            this.labelControl13.TabIndex = 419;
            // 
            // lblTitle2
            // 
            this.lblTitle2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTitle2.Appearance.ForeColor = System.Drawing.Color.White;
            this.lblTitle2.Appearance.Options.UseForeColor = true;
            this.lblTitle2.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical;
            this.lblTitle2.Location = new System.Drawing.Point(581, 48);
            this.lblTitle2.Margin = new System.Windows.Forms.Padding(2);
            this.lblTitle2.Name = "lblTitle2";
            this.lblTitle2.Size = new System.Drawing.Size(275, 0);
            this.lblTitle2.TabIndex = 418;
            // 
            // panelControl1
            // 
            this.panelControl1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(247)))), ((int)(((byte)(247)))));
            this.panelControl1.Appearance.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(237)))), ((int)(((byte)(241)))));
            this.panelControl1.Appearance.Options.UseBackColor = true;
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.lblPers1);
            this.panelControl1.Controls.Add(this.luePers1);
            this.panelControl1.Controls.Add(this.btnSACReportCopy);
            this.panelControl1.Controls.Add(this.luePers);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.panelControl2);
            this.panelControl1.Controls.Add(this.labelControl5);
            this.panelControl1.Controls.Add(this.pictureBox1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelControl1.Location = new System.Drawing.Point(950, 0);
            this.panelControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(210, 700);
            this.panelControl1.TabIndex = 1;
            // 
            // lblPers1
            // 
            this.lblPers1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPers1.Location = new System.Drawing.Point(135, 240);
            this.lblPers1.Margin = new System.Windows.Forms.Padding(2);
            this.lblPers1.Name = "lblPers1";
            this.lblPers1.Size = new System.Drawing.Size(46, 19);
            this.lblPers1.TabIndex = 711;
            this.lblPers1.Text = "نام پرسنل";
            this.lblPers1.Visible = false;
            // 
            // luePers1
            // 
            this.luePers1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.luePers1.EnterMoveNextControl = true;
            this.luePers1.Location = new System.Drawing.Point(23, 263);
            this.luePers1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.luePers1.Name = "luePers1";
            this.luePers1.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(247)))), ((int)(((byte)(247)))));
            this.luePers1.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(70)))), ((int)(((byte)(68)))));
            this.luePers1.Properties.Appearance.Options.UseBackColor = true;
            this.luePers1.Properties.Appearance.Options.UseForeColor = true;
            this.luePers1.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(236)))), ((int)(((byte)(237)))));
            this.luePers1.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.luePers1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.luePers1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.luePers1.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("PersID", "کد"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("PersName", "نام", 30, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default)});
            this.luePers1.Properties.DisplayMember = "PersName";
            this.luePers1.Properties.NullText = "";
            this.luePers1.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            this.luePers1.Properties.PopupSizeable = false;
            this.luePers1.Properties.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.AutoSearch;
            this.luePers1.Properties.ValueMember = "PersID";
            this.luePers1.Size = new System.Drawing.Size(158, 24);
            this.luePers1.TabIndex = 710;
            this.luePers1.Visible = false;
            // 
            // btnSACReportCopy
            // 
            this.btnSACReportCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSACReportCopy.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(90)))), ((int)(((byte)(158)))));
            this.btnSACReportCopy.Appearance.ForeColor = System.Drawing.Color.White;
            this.btnSACReportCopy.Appearance.Options.UseBackColor = true;
            this.btnSACReportCopy.Appearance.Options.UseForeColor = true;
            this.btnSACReportCopy.AppearanceDisabled.BackColor = System.Drawing.Color.Gray;
            this.btnSACReportCopy.AppearanceDisabled.Options.UseBackColor = true;
            this.btnSACReportCopy.AppearanceHovered.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.btnSACReportCopy.AppearanceHovered.Font = new System.Drawing.Font("IRANSansFaNum", 9F, System.Drawing.FontStyle.Bold);
            this.btnSACReportCopy.AppearanceHovered.Options.UseBackColor = true;
            this.btnSACReportCopy.AppearanceHovered.Options.UseFont = true;
            this.btnSACReportCopy.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnSACReportCopy.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnSACReportCopy.ImageOptions.Image")));
            this.btnSACReportCopy.Location = new System.Drawing.Point(23, 187);
            this.btnSACReportCopy.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            this.btnSACReportCopy.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btnSACReportCopy.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.btnSACReportCopy.Name = "btnSACReportCopy";
            this.btnSACReportCopy.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btnSACReportCopy.Size = new System.Drawing.Size(157, 28);
            this.btnSACReportCopy.TabIndex = 709;
            this.btnSACReportCopy.Text = "کپی دسترسی";
            // 
            // luePers
            // 
            this.luePers.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.luePers.EnterMoveNextControl = true;
            this.luePers.Location = new System.Drawing.Point(23, 138);
            this.luePers.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.luePers.Name = "luePers";
            this.luePers.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(247)))), ((int)(((byte)(247)))));
            this.luePers.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(70)))), ((int)(((byte)(68)))));
            this.luePers.Properties.Appearance.Options.UseBackColor = true;
            this.luePers.Properties.Appearance.Options.UseForeColor = true;
            this.luePers.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(236)))), ((int)(((byte)(237)))));
            this.luePers.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.luePers.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.luePers.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.luePers.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("PersID", "کد"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("PersName", "نام", 30, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ResuID", "کد دسترسی")});
            this.luePers.Properties.DisplayMember = "PersName";
            this.luePers.Properties.NullText = "";
            this.luePers.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            this.luePers.Properties.PopupSizeable = false;
            this.luePers.Properties.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.AutoSearch;
            this.luePers.Properties.ValueMember = "PersID";
            this.luePers.Size = new System.Drawing.Size(158, 24);
            this.luePers.TabIndex = 708;
            this.luePers.EditValueChanged += new System.EventHandler(this.luePers_EditValueChanged);
            // 
            // labelControl1
            // 
            this.labelControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl1.Appearance.Font = new System.Drawing.Font("IRANSansFaNum", 9F, System.Drawing.FontStyle.Bold);
            this.labelControl1.Appearance.ForeColor = System.Drawing.Color.Gray;
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Appearance.Options.UseForeColor = true;
            this.labelControl1.Location = new System.Drawing.Point(152, 87);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(2);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(29, 19);
            this.labelControl1.TabIndex = 707;
            this.labelControl1.Text = "پرسنل";
            // 
            // panelControl2
            // 
            this.panelControl2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelControl2.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(247)))), ((int)(((byte)(247)))));
            this.panelControl2.Appearance.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(73)))));
            this.panelControl2.Appearance.Options.UseBackColor = true;
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl2.Location = new System.Drawing.Point(25, 108);
            this.panelControl2.Margin = new System.Windows.Forms.Padding(2);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(156, 4);
            this.panelControl2.TabIndex = 706;
            // 
            // labelControl5
            // 
            this.labelControl5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl5.Location = new System.Drawing.Point(135, 115);
            this.labelControl5.Margin = new System.Windows.Forms.Padding(2);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(46, 19);
            this.labelControl5.TabIndex = 705;
            this.labelControl5.Text = "نام پرسنل";
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
            // panDetail
            // 
            this.panDetail.Location = new System.Drawing.Point(0, 45);
            this.panDetail.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panDetail.Name = "panDetail";
            this.panDetail.Size = new System.Drawing.Size(0, 0);
            this.panDetail.TabIndex = 420;
            // 
            // labelControl3
            // 
            this.labelControl3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl3.Appearance.Font = new System.Drawing.Font("IRANSansFaNum", 12F, System.Drawing.FontStyle.Bold);
            this.labelControl3.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(252)))), ((int)(((byte)(100)))));
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Appearance.Options.UseForeColor = true;
            this.labelControl3.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical;
            this.labelControl3.Location = new System.Drawing.Point(581, 12);
            this.labelControl3.Margin = new System.Windows.Forms.Padding(2);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(275, 25);
            this.labelControl3.TabIndex = 420;
            this.labelControl3.Text = "دسترسی کاربران";
            // 
            // UcSACUserPermission
            // 
            this.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(174)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panDetail);
            this.Controls.Add(this.panMain);
            this.Controls.Add(this.pictureBox3);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "UcSACUserPermission";
            this.Size = new System.Drawing.Size(1280, 720);
            this.Tag = "دسترسی کاربران";
            this.Load += new System.EventHandler(this.UcSACUserPermission_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panMain)).EndInit();
            this.panMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treSac)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.luePers1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.luePers.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panMain;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private DevExpress.XtraEditors.LabelControl labelControl13;
        private DevExpress.XtraEditors.LabelControl lblTitle2;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Panel panDetail;
        private DevExpress.XtraTreeList.TreeList treSac;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treColSACName;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.PanelControl panelControl5;
        private DevExpress.XtraEditors.LabelControl lblPers1;
        private DevExpress.XtraEditors.LookUpEdit luePers1;
        private DevExpress.XtraEditors.SimpleButton btnSACReportCopy;
        private DevExpress.XtraEditors.LookUpEdit luePers;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl3;
    }
}
