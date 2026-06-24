namespace Saman.UC.UI
{
    partial class UcDate
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
            this.dat = new FarsiLibrary.Win.DevExpress.XtraFADatePicker();
            ((System.ComponentModel.ISupportInitialize)(this.dat.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // dat
            // 
            this.dat.Location = new System.Drawing.Point(0, -1);
            this.dat.Name = "dat";
            this.dat.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.dat.Properties.Appearance.Options.UseBackColor = true;
            this.dat.Properties.Appearance.Options.UseForeColor = true;
            this.dat.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(236)))), ((int)(((byte)(237)))));
            this.dat.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.dat.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.dat.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dat.Properties.LookAndFeel.SkinName = "Blue";
            this.dat.Properties.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dat.Properties.PopupSizeable = false;
            this.dat.Properties.ShowPopupCloseButton = false;
            this.dat.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.dat.Properties.UseAdvancedMode = DevExpress.Utils.DefaultBoolean.True;
            this.dat.Size = new System.Drawing.Size(140, 24);
            this.dat.TabIndex = 0;
            this.dat.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(this.dat_EditValueChanging);
            // 
            // UcDate
            // 
            this.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dat);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "UcDate";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Size = new System.Drawing.Size(140, 24);
            this.Load += new System.EventHandler(this.UcDateTime_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.UcDateTime_Paint);
            ((System.ComponentModel.ISupportInitialize)(this.dat.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private FarsiLibrary.Win.DevExpress.XtraFADatePicker dat;
    }
}
