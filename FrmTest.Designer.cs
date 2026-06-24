namespace Saman
{
    partial class FrmTest
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmTest));
            this.button1 = new System.Windows.Forms.Button();
            this.btnSaveReport = new System.Windows.Forms.Button();
            this.btnShowReportFromDB = new System.Windows.Forms.Button();
            this.btnSaveToFile = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.richEditBarController1 = new DevExpress.XtraRichEdit.UI.RichEditBarController(this.components);
            this.txtReportPath = new DevExpress.XtraEditors.TextEdit();
            this.textEdit1 = new DevExpress.XtraEditors.TextEdit();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.toastNotificationsManager1 = new DevExpress.XtraBars.ToastNotifications.ToastNotificationsManager(this.components);
            this.button7 = new System.Windows.Forms.Button();
            this.sqlDataSource1 = new DevExpress.DataAccess.Sql.SqlDataSource(this.components);
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.vBedBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colRoomID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRoomName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBedID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBedName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.timeEdit1 = new DevExpress.XtraEditors.TimeEdit();
            this.button8 = new System.Windows.Forms.Button();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.richEditBarController1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtReportPath.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.toastNotificationsManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.vBedBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeEdit1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(84, -1);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(105, 25);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnSaveReport
            // 
            this.btnSaveReport.Location = new System.Drawing.Point(643, -1);
            this.btnSaveReport.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSaveReport.Name = "btnSaveReport";
            this.btnSaveReport.Size = new System.Drawing.Size(105, 25);
            this.btnSaveReport.TabIndex = 2;
            this.btnSaveReport.Text = "SaveReport";
            this.btnSaveReport.UseVisualStyleBackColor = true;
            this.btnSaveReport.Click += new System.EventHandler(this.btnSaveReport_Click);
            // 
            // btnShowReportFromDB
            // 
            this.btnShowReportFromDB.Location = new System.Drawing.Point(1020, -6);
            this.btnShowReportFromDB.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnShowReportFromDB.Name = "btnShowReportFromDB";
            this.btnShowReportFromDB.Size = new System.Drawing.Size(152, 25);
            this.btnShowReportFromDB.TabIndex = 3;
            this.btnShowReportFromDB.Text = "ShowReportFromDB";
            this.btnShowReportFromDB.UseVisualStyleBackColor = true;
            this.btnShowReportFromDB.Click += new System.EventHandler(this.btnShowReportFromDB_Click);
            // 
            // btnSaveToFile
            // 
            this.btnSaveToFile.Location = new System.Drawing.Point(444, -4);
            this.btnSaveToFile.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSaveToFile.Name = "btnSaveToFile";
            this.btnSaveToFile.Size = new System.Drawing.Size(122, 25);
            this.btnSaveToFile.TabIndex = 4;
            this.btnSaveToFile.Text = "Save To File";
            this.btnSaveToFile.UseVisualStyleBackColor = true;
            this.btnSaveToFile.Click += new System.EventHandler(this.btnSaveToFile_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(34, 111);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 7;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(34, 216);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 8;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(34, 171);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 9;
            this.button4.Text = "button4";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // txtReportPath
            // 
            this.txtReportPath.EditValue = "D:\\Report.mrt";
            this.txtReportPath.Location = new System.Drawing.Point(754, -4);
            this.txtReportPath.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtReportPath.Name = "txtReportPath";
            this.txtReportPath.Size = new System.Drawing.Size(261, 26);
            this.txtReportPath.TabIndex = 1;
            // 
            // textEdit1
            // 
            this.textEdit1.Location = new System.Drawing.Point(194, -3);
            this.textEdit1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Size = new System.Drawing.Size(244, 26);
            this.textEdit1.TabIndex = 0;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(34, 432);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 10;
            this.button5.Text = "button5";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click_1);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(34, 306);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(75, 23);
            this.button6.TabIndex = 11;
            this.button6.Text = "button6";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // toastNotificationsManager1
            // 
            this.toastNotificationsManager1.ApplicationId = "1";
            this.toastNotificationsManager1.Notifications.AddRange(new DevExpress.XtraBars.ToastNotifications.IToastNotificationProperties[] {
            new DevExpress.XtraBars.ToastNotifications.ToastNotification("0216d08b-b8fa-453a-894a-ee231c41d3c3", ((System.Drawing.Image)(resources.GetObject("toastNotificationsManager1.Notifications"))), "مسیح", "محمد", "نوری", DevExpress.XtraBars.ToastNotifications.ToastNotificationSound.SMS, DevExpress.XtraBars.ToastNotifications.ToastNotificationDuration.Default, DevExpress.XtraBars.ToastNotifications.ToastNotificationTemplate.ImageAndText04)});
            this.toastNotificationsManager1.Activated += new System.EventHandler<DevExpress.XtraBars.ToastNotifications.ToastNotificationEventArgs>(this.toastNotificationsManager1_Activated);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(34, 380);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(75, 23);
            this.button7.TabIndex = 12;
            this.button7.Text = "button7";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // sqlDataSource1
            // 
            this.sqlDataSource1.Name = "sqlDataSource1";
            // 
            // gridControl1
            // 
            this.gridControl1.DataSource = this.vBedBindingSource;
            this.gridControl1.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gridControl1.Location = new System.Drawing.Point(217, 380);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(1033, 488);
            this.gridControl1.TabIndex = 13;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // vBedBindingSource
            // 
            this.vBedBindingSource.DataMember = "vBed";
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colRoomID,
            this.colRoomName,
            this.colBedID,
            this.colBedName});
            this.gridView1.DetailHeight = 277;
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.GroupCount = 1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.AllowGroupExpandAnimation = DevExpress.Utils.DefaultBoolean.False;
            this.gridView1.OptionsBehavior.AutoExpandAllGroups = true;
            this.gridView1.OptionsDetail.AllowExpandEmptyDetails = true;
            this.gridView1.OptionsPrint.ExpandAllDetails = true;
            this.gridView1.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colRoomName, DevExpress.Data.ColumnSortOrder.Ascending)});
            // 
            // colRoomID
            // 
            this.colRoomID.FieldName = "RoomID";
            this.colRoomID.MinWidth = 22;
            this.colRoomID.Name = "colRoomID";
            this.colRoomID.Visible = true;
            this.colRoomID.VisibleIndex = 0;
            this.colRoomID.Width = 82;
            // 
            // colRoomName
            // 
            this.colRoomName.FieldName = "RoomName";
            this.colRoomName.MinWidth = 22;
            this.colRoomName.Name = "colRoomName";
            this.colRoomName.Visible = true;
            this.colRoomName.VisibleIndex = 1;
            this.colRoomName.Width = 82;
            // 
            // colBedID
            // 
            this.colBedID.FieldName = "BedID";
            this.colBedID.MinWidth = 22;
            this.colBedID.Name = "colBedID";
            this.colBedID.Visible = true;
            this.colBedID.VisibleIndex = 1;
            this.colBedID.Width = 82;
            // 
            // colBedName
            // 
            this.colBedName.FieldName = "BedName";
            this.colBedName.MinWidth = 22;
            this.colBedName.Name = "colBedName";
            this.colBedName.Visible = true;
            this.colBedName.VisibleIndex = 2;
            this.colBedName.Width = 82;
            // 
            // timeEdit1
            // 
            this.timeEdit1.EditValue = new System.DateTime(2021, 11, 24, 0, 0, 0, 0);
            this.timeEdit1.Location = new System.Drawing.Point(801, 292);
            this.timeEdit1.Name = "timeEdit1";
            this.timeEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.timeEdit1.Size = new System.Drawing.Size(100, 26);
            this.timeEdit1.TabIndex = 16;
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(491, 254);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(75, 23);
            this.button8.TabIndex = 17;
            this.button8.Text = "button8";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(34, 477);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(141, 23);
            this.simpleButton1.TabIndex = 439;
            this.simpleButton1.Text = "simpleButton1";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // FrmTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1205, 591);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.timeEdit1);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnSaveToFile);
            this.Controls.Add(this.btnShowReportFromDB);
            this.Controls.Add(this.txtReportPath);
            this.Controls.Add(this.btnSaveReport);
            this.Controls.Add(this.textEdit1);
            this.Controls.Add(this.button1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "FrmTest";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Text = "XtraForm1";
            this.Load += new System.EventHandler(this.XtraForm1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.richEditBarController1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtReportPath.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.toastNotificationsManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.vBedBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeEdit1.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Stimulsoft.Report.StiReport stiReport1;
        private System.Windows.Forms.Button button1;
        private DevExpress.XtraEditors.TextEdit textEdit1;
        private DevExpress.XtraEditors.TextEdit txtReportPath;
        private System.Windows.Forms.Button btnSaveReport;
        private System.Windows.Forms.Button btnShowReportFromDB;
        private System.Windows.Forms.Button btnSaveToFile;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private DevExpress.XtraRichEdit.UI.RichEditBarController richEditBarController1;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private DevExpress.XtraBars.ToastNotifications.ToastNotificationsManager toastNotificationsManager1;
        private System.Windows.Forms.Button button7;
        private DevExpress.DataAccess.Sql.SqlDataSource sqlDataSource1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private System.Windows.Forms.BindingSource vBedBindingSource;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn colRoomID;
        private DevExpress.XtraGrid.Columns.GridColumn colRoomName;
        private DevExpress.XtraGrid.Columns.GridColumn colBedID;
        private DevExpress.XtraGrid.Columns.GridColumn colBedName;
        private UC.UI.UcDateTime ucDateTime1;
        private DevExpress.XtraEditors.TimeEdit timeEdit1;
        private System.Windows.Forms.Button button8;
        private UC.UI.UcDate ucDate1;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
    }
}