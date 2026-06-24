namespace Saman.UC.UI
{
    partial class UcDateTime
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
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions1 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
            this.tim = new DevExpress.XtraEditors.TimeEdit();
            this.Date = new FarsiLibrary.Win.DevExpress.XtraFADatePicker();
            ((System.ComponentModel.ISupportInitialize)(this.tim.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Date.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // tim
            // 
            this.tim.EditValue = new System.DateTime(2020, 5, 24, 0, 0, 0, 0);
            this.tim.EnterMoveNextControl = true;
            this.tim.Location = new System.Drawing.Point(99, -1);
            this.tim.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tim.Name = "tim";
            this.tim.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.tim.Properties.Appearance.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(237)))), ((int)(((byte)(241)))));
            this.tim.Properties.Appearance.Options.UseBackColor = true;
            this.tim.Properties.Appearance.Options.UseTextOptions = true;
            this.tim.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.tim.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.tim.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, true, false, false, editorButtonImageOptions1, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, serializableAppearanceObject2, serializableAppearanceObject3, serializableAppearanceObject4, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.tim.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.tim.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.DateTimeMaskManager));
            this.tim.Properties.MaskSettings.Set("useAdvancingCaret", true);
            this.tim.Properties.MaskSettings.Set("mask", "HH:mm");
            this.tim.Size = new System.Drawing.Size(42, 24);
            this.tim.TabIndex = 2;
            this.tim.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(this.tim_EditValueChanging);
            // 
            // Date
            // 
            this.Date.Location = new System.Drawing.Point(1, 0);
            this.Date.Name = "Date";
            this.Date.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.Date.Properties.Appearance.Options.UseBackColor = true;
            this.Date.Properties.Appearance.Options.UseForeColor = true;
            this.Date.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(236)))), ((int)(((byte)(237)))));
            this.Date.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.Date.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.Date.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.Date.Properties.LookAndFeel.SkinName = "Blue";
            this.Date.Properties.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Date.Properties.PopupSizeable = false;
            this.Date.Properties.ShowPopupCloseButton = false;
            this.Date.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.Date.Properties.UseAdvancedMode = DevExpress.Utils.DefaultBoolean.True;
            this.Date.Size = new System.Drawing.Size(99, 24);
            this.Date.TabIndex = 3;
            this.Date.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(this.Date_EditValueChanging);
            // 
            // UcDateTime
            // 
            this.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Date);
            this.Controls.Add(this.tim);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "UcDateTime";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Size = new System.Drawing.Size(140, 24);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.UcDateTime_Paint);
            ((System.ComponentModel.ISupportInitialize)(this.tim.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Date.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.TimeEdit tim;
        private FarsiLibrary.Win.DevExpress.XtraFADatePicker Date;
    }
}
