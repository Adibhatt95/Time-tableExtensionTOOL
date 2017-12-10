namespace TeachersAssessment
{
    partial class FMainOne
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FMainOne));
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.cbTeachers = new System.Windows.Forms.ComboBox();
            this.flowLayoutTeacher = new System.Windows.Forms.FlowLayoutPanel();
            this.btnTimeTable = new System.Windows.Forms.Button();
            this.btnTeacherAssessment = new System.Windows.Forms.Button();
            this.btnSettings = new System.Windows.Forms.Button();
            this.btnDlyReport = new System.Windows.Forms.Button();
            this.flowLayoutAdmin = new System.Windows.Forms.FlowLayoutPanel();
            this.btnViewMultiDeptRoomAvail = new System.Windows.Forms.Button();
            this.btnMDailyReport = new System.Windows.Forms.Button();
            this.btnAttenWeeklyReport = new System.Windows.Forms.Button();
            this.btnSetupDepts = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.cbLoginType = new System.Windows.Forms.ComboBox();
            this.btnLogin = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.pbrefreshTeacherList = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lbltitle = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnMonthlyReport = new System.Windows.Forms.Button();
            this.btnMonthReportHOD = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.flowLayoutTeacher.SuspendLayout();
            this.flowLayoutAdmin.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbrefreshTeacherList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.tableLayoutPanel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(5, 55);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(625, 519);
            this.panel1.TabIndex = 2;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.AutoSize = true;
            this.tableLayoutPanel2.BackColor = System.Drawing.Color.AliceBlue;
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 259F));
            this.tableLayoutPanel2.Controls.Add(this.cbTeachers, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.flowLayoutTeacher, 1, 8);
            this.tableLayoutPanel2.Controls.Add(this.flowLayoutAdmin, 1, 7);
            this.tableLayoutPanel2.Controls.Add(this.label2, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.linkLabel1, 1, 9);
            this.tableLayoutPanel2.Controls.Add(this.label1, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.cbLoginType, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.btnLogin, 1, 5);
            this.tableLayoutPanel2.Controls.Add(this.label3, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.textBox1, 1, 3);
            this.tableLayoutPanel2.Controls.Add(this.pbrefreshTeacherList, 2, 2);
            this.tableLayoutPanel2.Controls.Add(this.label4, 2, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 10;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 17F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 13F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 13F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 13F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(623, 517);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // cbTeachers
            // 
            this.cbTeachers.BackColor = System.Drawing.Color.LightCyan;
            this.cbTeachers.Enabled = false;
            this.cbTeachers.FormattingEnabled = true;
            this.cbTeachers.Location = new System.Drawing.Point(147, 49);
            this.cbTeachers.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cbTeachers.Name = "cbTeachers";
            this.cbTeachers.Size = new System.Drawing.Size(235, 23);
            this.cbTeachers.TabIndex = 3;
            // 
            // flowLayoutTeacher
            // 
            this.flowLayoutTeacher.AutoSize = true;
            this.flowLayoutTeacher.Controls.Add(this.btnTimeTable);
            this.flowLayoutTeacher.Controls.Add(this.btnTeacherAssessment);
            this.flowLayoutTeacher.Controls.Add(this.btnSettings);
            this.flowLayoutTeacher.Controls.Add(this.btnDlyReport);
            this.flowLayoutTeacher.Controls.Add(this.btnMonthlyReport);
            this.flowLayoutTeacher.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutTeacher.Location = new System.Drawing.Point(147, 355);
            this.flowLayoutTeacher.Margin = new System.Windows.Forms.Padding(2);
            this.flowLayoutTeacher.Name = "flowLayoutTeacher";
            this.flowLayoutTeacher.Size = new System.Drawing.Size(237, 185);
            this.flowLayoutTeacher.TabIndex = 2;
            this.flowLayoutTeacher.Visible = false;
            // 
            // btnTimeTable
            // 
            this.btnTimeTable.BackColor = System.Drawing.Color.SeaGreen;
            this.btnTimeTable.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.btnTimeTable.Location = new System.Drawing.Point(2, 2);
            this.btnTimeTable.Margin = new System.Windows.Forms.Padding(2);
            this.btnTimeTable.Name = "btnTimeTable";
            this.btnTimeTable.Size = new System.Drawing.Size(233, 33);
            this.btnTimeTable.TabIndex = 0;
            this.btnTimeTable.Text = "Time Tabler";
            this.btnTimeTable.UseVisualStyleBackColor = false;
            // 
            // btnTeacherAssessment
            // 
            this.btnTeacherAssessment.BackColor = System.Drawing.Color.SeaGreen;
            this.btnTeacherAssessment.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.btnTeacherAssessment.Location = new System.Drawing.Point(2, 39);
            this.btnTeacherAssessment.Margin = new System.Windows.Forms.Padding(2);
            this.btnTeacherAssessment.Name = "btnTeacherAssessment";
            this.btnTeacherAssessment.Size = new System.Drawing.Size(233, 33);
            this.btnTeacherAssessment.TabIndex = 1;
            this.btnTeacherAssessment.Text = "Attendance System";
            this.btnTeacherAssessment.UseVisualStyleBackColor = false;
            // 
            // btnSettings
            // 
            this.btnSettings.BackColor = System.Drawing.Color.SeaGreen;
            this.btnSettings.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.btnSettings.Location = new System.Drawing.Point(2, 76);
            this.btnSettings.Margin = new System.Windows.Forms.Padding(2);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(233, 33);
            this.btnSettings.TabIndex = 7;
            this.btnSettings.Text = "Settings";
            this.btnSettings.UseVisualStyleBackColor = false;
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // btnDlyReport
            // 
            this.btnDlyReport.BackColor = System.Drawing.Color.SeaGreen;
            this.btnDlyReport.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.btnDlyReport.Location = new System.Drawing.Point(2, 113);
            this.btnDlyReport.Margin = new System.Windows.Forms.Padding(2);
            this.btnDlyReport.Name = "btnDlyReport";
            this.btnDlyReport.Size = new System.Drawing.Size(233, 33);
            this.btnDlyReport.TabIndex = 2;
            this.btnDlyReport.Text = "Daily Report";
            this.btnDlyReport.UseVisualStyleBackColor = false;
            // 
            // flowLayoutAdmin
            // 
            this.flowLayoutAdmin.AutoSize = true;
            this.flowLayoutAdmin.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutAdmin.Controls.Add(this.btnViewMultiDeptRoomAvail);
            this.flowLayoutAdmin.Controls.Add(this.btnMDailyReport);
            this.flowLayoutAdmin.Controls.Add(this.btnAttenWeeklyReport);
            this.flowLayoutAdmin.Controls.Add(this.btnMonthReportHOD);
            this.flowLayoutAdmin.Controls.Add(this.btnSetupDepts);
            this.flowLayoutAdmin.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutAdmin.Location = new System.Drawing.Point(147, 162);
            this.flowLayoutAdmin.Margin = new System.Windows.Forms.Padding(2);
            this.flowLayoutAdmin.Name = "flowLayoutAdmin";
            this.flowLayoutAdmin.Padding = new System.Windows.Forms.Padding(2);
            this.flowLayoutAdmin.Size = new System.Drawing.Size(241, 189);
            this.flowLayoutAdmin.TabIndex = 1;
            this.flowLayoutAdmin.Visible = false;
            // 
            // btnViewMultiDeptRoomAvail
            // 
            this.btnViewMultiDeptRoomAvail.BackColor = System.Drawing.Color.LightCyan;
            this.btnViewMultiDeptRoomAvail.Location = new System.Drawing.Point(4, 4);
            this.btnViewMultiDeptRoomAvail.Margin = new System.Windows.Forms.Padding(2);
            this.btnViewMultiDeptRoomAvail.Name = "btnViewMultiDeptRoomAvail";
            this.btnViewMultiDeptRoomAvail.Size = new System.Drawing.Size(233, 33);
            this.btnViewMultiDeptRoomAvail.TabIndex = 0;
            this.btnViewMultiDeptRoomAvail.Text = "View Multiple Rooms Availability";
            this.btnViewMultiDeptRoomAvail.UseVisualStyleBackColor = false;
            // 
            // btnMDailyReport
            // 
            this.btnMDailyReport.BackColor = System.Drawing.Color.LightCyan;
            this.btnMDailyReport.Location = new System.Drawing.Point(4, 41);
            this.btnMDailyReport.Margin = new System.Windows.Forms.Padding(2);
            this.btnMDailyReport.Name = "btnMDailyReport";
            this.btnMDailyReport.Size = new System.Drawing.Size(233, 33);
            this.btnMDailyReport.TabIndex = 3;
            this.btnMDailyReport.Text = "View Daily Reports of Department";
            this.btnMDailyReport.UseVisualStyleBackColor = false;
            this.btnMDailyReport.Click += new System.EventHandler(this.btnMDailyReport_Click);
            // 
            // btnAttenWeeklyReport
            // 
            this.btnAttenWeeklyReport.BackColor = System.Drawing.Color.LightCyan;
            this.btnAttenWeeklyReport.Location = new System.Drawing.Point(4, 78);
            this.btnAttenWeeklyReport.Margin = new System.Windows.Forms.Padding(2);
            this.btnAttenWeeklyReport.Name = "btnAttenWeeklyReport";
            this.btnAttenWeeklyReport.Size = new System.Drawing.Size(233, 33);
            this.btnAttenWeeklyReport.TabIndex = 1;
            this.btnAttenWeeklyReport.Text = "View Attendance Weekly Reports";
            this.btnAttenWeeklyReport.UseVisualStyleBackColor = false;
            // 
            // btnSetupDepts
            // 
            this.btnSetupDepts.BackColor = System.Drawing.Color.LightCyan;
            this.btnSetupDepts.Location = new System.Drawing.Point(4, 152);
            this.btnSetupDepts.Margin = new System.Windows.Forms.Padding(2);
            this.btnSetupDepts.Name = "btnSetupDepts";
            this.btnSetupDepts.Size = new System.Drawing.Size(233, 33);
            this.btnSetupDepts.TabIndex = 2;
            this.btnSetupDepts.Text = "Configure Departments";
            this.btnSetupDepts.UseVisualStyleBackColor = false;
            this.btnSetupDepts.Click += new System.EventHandler(this.btnSetupDepts_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Enabled = false;
            this.label2.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(2, 46);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(141, 29);
            this.label2.TabIndex = 2;
            this.label2.Text = "Teacher:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.linkLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel1.Location = new System.Drawing.Point(148, 542);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(239, 13);
            this.linkLabel1.TabIndex = 51;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Change Password";
            this.linkLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.linkLabel1.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(67, 17);
            this.label1.Margin = new System.Windows.Forms.Padding(67, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 29);
            this.label1.TabIndex = 0;
            this.label1.Text = "Login Type:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbLoginType
            // 
            this.cbLoginType.BackColor = System.Drawing.Color.LightCyan;
            this.cbLoginType.FormattingEnabled = true;
            this.cbLoginType.Location = new System.Drawing.Point(147, 20);
            this.cbLoginType.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cbLoginType.Name = "cbLoginType";
            this.cbLoginType.Size = new System.Drawing.Size(235, 23);
            this.cbLoginType.TabIndex = 1;
            // 
            // btnLogin
            // 
            this.btnLogin.BackColor = System.Drawing.Color.Khaki;
            this.btnLogin.Location = new System.Drawing.Point(147, 119);
            this.btnLogin.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(233, 25);
            this.btnLogin.TabIndex = 6;
            this.btnLogin.Text = "Login";
            this.btnLogin.UseVisualStyleBackColor = false;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(2, 75);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(141, 28);
            this.label3.TabIndex = 5;
            this.label3.Text = "Password:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.LightYellow;
            this.textBox1.Location = new System.Drawing.Point(147, 77);
            this.textBox1.Margin = new System.Windows.Forms.Padding(2);
            this.textBox1.Name = "textBox1";
            this.textBox1.PasswordChar = '*';
            this.textBox1.Size = new System.Drawing.Size(235, 24);
            this.textBox1.TabIndex = 4;
            // 
            // pbrefreshTeacherList
            // 
            this.pbrefreshTeacherList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbrefreshTeacherList.Enabled = false;
            this.pbrefreshTeacherList.Image = ((System.Drawing.Image)(resources.GetObject("pbrefreshTeacherList.Image")));
            this.pbrefreshTeacherList.Location = new System.Drawing.Point(392, 48);
            this.pbrefreshTeacherList.Margin = new System.Windows.Forms.Padding(2);
            this.pbrefreshTeacherList.Name = "pbrefreshTeacherList";
            this.pbrefreshTeacherList.Padding = new System.Windows.Forms.Padding(3);
            this.pbrefreshTeacherList.Size = new System.Drawing.Size(23, 23);
            this.pbrefreshTeacherList.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbrefreshTeacherList.TabIndex = 52;
            this.pbrefreshTeacherList.TabStop = false;
            this.pbrefreshTeacherList.Click += new System.EventHandler(this.refreshTeacherList_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.SaddleBrown;
            this.label4.Location = new System.Drawing.Point(392, 17);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(255, 29);
            this.label4.TabIndex = 53;
            this.label4.Text = "Connecting, Please wait ...";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label4.Visible = false;
            // 
            // lbltitle
            // 
            this.lbltitle.AutoSize = true;
            this.lbltitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbltitle.Font = new System.Drawing.Font("Palatino Linotype", 14F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbltitle.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbltitle.Location = new System.Drawing.Point(118, 0);
            this.lbltitle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbltitle.Name = "lbltitle";
            this.lbltitle.Size = new System.Drawing.Size(433, 48);
            this.lbltitle.TabIndex = 1;
            this.lbltitle.Text = "D.J.Sanghvi - TimeTabler and Teacher Assessment";
            this.lbltitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(73, 3);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(73, 3, 2, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(41, 42);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.LightCyan;
            this.flowLayoutPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowLayoutPanel1.Controls.Add(this.pictureBox1);
            this.flowLayoutPanel1.Controls.Add(this.lbltitle);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(5, 5);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(625, 50);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // btnMonthlyReport
            // 
            this.btnMonthlyReport.BackColor = System.Drawing.Color.SeaGreen;
            this.btnMonthlyReport.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.btnMonthlyReport.Location = new System.Drawing.Point(2, 150);
            this.btnMonthlyReport.Margin = new System.Windows.Forms.Padding(2);
            this.btnMonthlyReport.Name = "btnMonthlyReport";
            this.btnMonthlyReport.Size = new System.Drawing.Size(233, 33);
            this.btnMonthlyReport.TabIndex = 8;
            this.btnMonthlyReport.Text = "Monthly Report";
            this.btnMonthlyReport.UseVisualStyleBackColor = false;
            // 
            // btnMonthReportHOD
            // 
            this.btnMonthReportHOD.BackColor = System.Drawing.Color.LightCyan;
            this.btnMonthReportHOD.Location = new System.Drawing.Point(4, 115);
            this.btnMonthReportHOD.Margin = new System.Windows.Forms.Padding(2);
            this.btnMonthReportHOD.Name = "btnMonthReportHOD";
            this.btnMonthReportHOD.Size = new System.Drawing.Size(233, 33);
            this.btnMonthReportHOD.TabIndex = 4;
            this.btnMonthReportHOD.Text = "View Monthly Reports of Teacher";
            this.btnMonthReportHOD.UseVisualStyleBackColor = false;
            // 
            // FMainOne
            // 
            this.AcceptButton = this.btnLogin;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.MediumAquamarine;
            this.ClientSize = new System.Drawing.Size(635, 579);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Name = "FMainOne";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "D.J.Sanghvi - Time Tabler & Teacher Assessment:";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.flowLayoutTeacher.ResumeLayout(false);
            this.flowLayoutAdmin.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbrefreshTeacherList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.ComboBox cbTeachers;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutTeacher;
        private System.Windows.Forms.Button btnTimeTable;
        private System.Windows.Forms.Button btnTeacherAssessment;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutAdmin;
        private System.Windows.Forms.Button btnAttenWeeklyReport;
        private System.Windows.Forms.Button btnSetupDepts;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbLoginType;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label lbltitle;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btnDlyReport;
        private System.Windows.Forms.Button btnSettings;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.PictureBox pbrefreshTeacherList;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnViewMultiDeptRoomAvail;
        private System.Windows.Forms.Button btnMDailyReport;
        private System.Windows.Forms.Button btnMonthlyReport;
        private System.Windows.Forms.Button btnMonthReportHOD;
    }
}