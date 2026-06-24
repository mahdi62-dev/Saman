namespace Saman.UC.UI
{
    partial class UcGridLookup
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
            this.panUc = new System.Windows.Forms.Panel();
            this.gluGrid = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gluView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.lblUnderLine = new System.Windows.Forms.Label();
            this.lblCaption = new DevExpress.XtraEditors.LabelControl();
            this.label1 = new System.Windows.Forms.Label();
            this.panUc.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gluGrid.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gluView)).BeginInit();
            this.SuspendLayout();
            // 
            // panUc
            // 
            this.panUc.Controls.Add(this.gluGrid);
            this.panUc.Controls.Add(this.lblUnderLine);
            this.panUc.Controls.Add(this.lblCaption);
            this.panUc.Controls.Add(this.label1);
            this.panUc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panUc.Location = new System.Drawing.Point(0, 0);
            this.panUc.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panUc.Name = "panUc";
            this.panUc.Size = new System.Drawing.Size(130, 48);
            this.panUc.TabIndex = 6;
            this.panUc.Tag = "";
            this.panUc.Paint += new System.Windows.Forms.PaintEventHandler(this.panUc_Paint);
            // 
            // gluGrid
            // 
            this.gluGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gluGrid.EditValue = "";
            this.gluGrid.Location = new System.Drawing.Point(0, 19);
            this.gluGrid.Name = "gluGrid";
            this.gluGrid.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(236)))), ((int)(((byte)(237)))));
            this.gluGrid.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.gluGrid.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.gluGrid.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.gluGrid.Properties.NullText = "";
            this.gluGrid.Properties.PopupView = this.gluView;
            this.gluGrid.Size = new System.Drawing.Size(127, 24);
            this.gluGrid.TabIndex = 422;
            this.gluGrid.Closed += new DevExpress.XtraEditors.Controls.ClosedEventHandler(this.gluGrid_Closed);
            this.gluGrid.CustomDisplayText += new DevExpress.XtraEditors.Controls.CustomDisplayTextEventHandler(this.gluGrid_CustomDisplayText);
            // 
            // gluView
            // 
            this.gluView.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gluView.Name = "gluView";
            this.gluView.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gluView.OptionsSelection.MultiSelect = true;
            this.gluView.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.gluView.OptionsView.ShowAutoFilterRow = true;
            this.gluView.OptionsView.ShowColumnHeaders = false;
            this.gluView.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.gluView.OptionsView.ShowGroupPanel = false;
            // 
            // lblUnderLine
            // 
            this.lblUnderLine.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblUnderLine.Location = new System.Drawing.Point(-9, 33);
            this.lblUnderLine.Name = "lblUnderLine";
            this.lblUnderLine.Size = new System.Drawing.Size(152, 15);
            this.lblUnderLine.TabIndex = 421;
            this.lblUnderLine.Text = "_________________________________________________________________________________" +
    "________________________________";
            // 
            // lblCaption
            // 
            this.lblCaption.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCaption.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblCaption.Location = new System.Drawing.Point(2, 2);
            this.lblCaption.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lblCaption.Name = "lblCaption";
            this.lblCaption.Size = new System.Drawing.Size(127, 19);
            this.lblCaption.TabIndex = 2;
            this.lblCaption.Text = "-";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(-11, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(149, 15);
            this.label1.TabIndex = 423;
            this.label1.Text = "____________________________";
            // 
            // UcGridLookup
            // 
            this.Appearance.BackColor = System.Drawing.Color.White;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panUc);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "UcGridLookup";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Size = new System.Drawing.Size(130, 48);
            this.Load += new System.EventHandler(this.UcGridLookup_Load);
            this.panUc.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gluGrid.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gluView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panUc;
        private System.Windows.Forms.Label lblUnderLine;
        private DevExpress.XtraEditors.LabelControl lblCaption;
        private DevExpress.XtraEditors.GridLookUpEdit gluGrid;
        private DevExpress.XtraGrid.Views.Grid.GridView gluView;
        private System.Windows.Forms.Label label1;
    }
}
