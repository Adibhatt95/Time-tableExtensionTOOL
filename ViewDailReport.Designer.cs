namespace TeachersAssessment
{
    partial class ViewDailReport
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ViewDailReport));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.btnTeacherAssess = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnAllTE = new System.Windows.Forms.Button();
            this.btnAllSE = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.btnEditGrade = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.btnAllBE = new System.Windows.Forms.Button();
            this.btnAllClasses = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label7 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.chkBoxSelGrade = new System.Windows.Forms.CheckBox();
            this.chkShowConducted = new System.Windows.Forms.CheckBox();
            this.button2 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.BackColor = System.Drawing.Color.Aquamarine;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.lblTitle);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(8);
            this.panel1.Size = new System.Drawing.Size(985, 77);
            this.panel1.TabIndex = 7;
            // 
            // btnClose
            // 
            this.btnClose.AutoSize = true;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.ForeColor = System.Drawing.Color.Red;
            this.btnClose.Location = new System.Drawing.Point(885, 31);
            this.btnClose.Margin = new System.Windows.Forms.Padding(2);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(76, 34);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(39, 36);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(268, 23);
            this.label3.TabIndex = 2;
            this.label3.Text = "Daily Report/Attendance System";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.DimGray;
            this.lblTitle.Location = new System.Drawing.Point(8, 8);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(12, 0, 3, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(390, 25);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "D.J.Sanghvi College Of Engineering";
            // 
            // label11
            // 
            this.label11.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(449, 281);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(512, 19);
            this.label11.TabIndex = 20;
            this.label11.Text = "Lectures not conducted/conducted so far(Click on column name to sort):-";
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(428, 70);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(26, 18);
            this.label5.TabIndex = 6;
            this.label5.Text = "OR";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label5.Visible = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(94, 204);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.MaximumSize = new System.Drawing.Size(333, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(263, 17);
            this.label10.TabIndex = 18;
            this.label10.Text = "4. Proceed to select time of lecture/practical.";
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(94, 167);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.MaximumSize = new System.Drawing.Size(333, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(329, 17);
            this.label9.TabIndex = 17;
            this.label9.Text = "3. To begin, click on the blue button on the right.";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(94, 130);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.MaximumSize = new System.Drawing.Size(167, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(143, 34);
            this.label8.TabIndex = 16;
            this.label8.Text = "2. Please select Date as well.";
            // 
            // btnTeacherAssess
            // 
            this.btnTeacherAssess.BackColor = System.Drawing.Color.Cyan;
            this.btnTeacherAssess.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnTeacherAssess.FlatAppearance.BorderSize = 8;
            this.btnTeacherAssess.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime;
            this.btnTeacherAssess.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Lime;
            this.btnTeacherAssess.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnTeacherAssess.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTeacherAssess.Location = new System.Drawing.Point(429, 244);
            this.btnTeacherAssess.Name = "btnTeacherAssess";
            this.btnTeacherAssess.Size = new System.Drawing.Size(262, 33);
            this.btnTeacherAssess.TabIndex = 14;
            this.btnTeacherAssess.Text = "Enter Attendance Data";
            this.btnTeacherAssess.UseVisualStyleBackColor = false;
            this.btnTeacherAssess.Click += new System.EventHandler(this.btnTeacherAssess_Click_1);
            // 
            // btnExport
            // 
            this.btnExport.BackColor = System.Drawing.Color.Aquamarine;
            this.btnExport.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnExport.Location = new System.Drawing.Point(95, 244);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(195, 33);
            this.btnExport.TabIndex = 12;
            this.btnExport.Text = "Export Daily Report";
            this.btnExport.UseVisualStyleBackColor = false;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click_1);
            // 
            // btnAllTE
            // 
            this.btnAllTE.BackColor = System.Drawing.Color.PaleGreen;
            this.btnAllTE.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAllTE.Location = new System.Drawing.Point(429, 170);
            this.btnAllTE.Name = "btnAllTE";
            this.btnAllTE.Size = new System.Drawing.Size(262, 30);
            this.btnAllTE.TabIndex = 8;
            this.btnAllTE.Text = "All TE Classes";
            this.btnAllTE.UseVisualStyleBackColor = false;
            // 
            // btnAllSE
            // 
            this.btnAllSE.BackColor = System.Drawing.Color.PaleGreen;
            this.btnAllSE.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAllSE.Location = new System.Drawing.Point(429, 133);
            this.btnAllSE.Name = "btnAllSE";
            this.btnAllSE.Size = new System.Drawing.Size(262, 30);
            this.btnAllSE.TabIndex = 9;
            this.btnAllSE.Text = "All SE Classes";
            this.btnAllSE.UseVisualStyleBackColor = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(94, 70);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(190, 19);
            this.label6.TabIndex = 7;
            this.label6.Text = "Select From the Following:";
            this.label6.Visible = false;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(95, 4);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(256, 24);
            this.dateTimePicker1.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(4, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 37);
            this.label2.TabIndex = 2;
            this.label2.Text = "Select Grade:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(95, 35);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(256, 23);
            this.comboBox1.TabIndex = 3;
            // 
            // btnEditGrade
            // 
            this.btnEditGrade.BackColor = System.Drawing.Color.Aquamarine;
            this.btnEditGrade.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnEditGrade.Location = new System.Drawing.Point(429, 35);
            this.btnEditGrade.Name = "btnEditGrade";
            this.btnEditGrade.Size = new System.Drawing.Size(262, 31);
            this.btnEditGrade.TabIndex = 5;
            this.btnEditGrade.Text = "Edit For Selected Grade";
            this.btnEditGrade.UseVisualStyleBackColor = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Location = new System.Drawing.Point(4, 1);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 30);
            this.label4.TabIndex = 0;
            this.label4.Text = "Select Date:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnAllBE
            // 
            this.btnAllBE.BackColor = System.Drawing.Color.PaleGreen;
            this.btnAllBE.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAllBE.Location = new System.Drawing.Point(429, 207);
            this.btnAllBE.Name = "btnAllBE";
            this.btnAllBE.Size = new System.Drawing.Size(262, 30);
            this.btnAllBE.TabIndex = 10;
            this.btnAllBE.Text = "All BE Classes";
            this.btnAllBE.UseVisualStyleBackColor = false;
            // 
            // btnAllClasses
            // 
            this.btnAllClasses.BackColor = System.Drawing.Color.PaleGreen;
            this.btnAllClasses.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAllClasses.Location = new System.Drawing.Point(429, 93);
            this.btnAllClasses.Name = "btnAllClasses";
            this.btnAllClasses.Size = new System.Drawing.Size(262, 33);
            this.btnAllClasses.TabIndex = 11;
            this.btnAllClasses.Text = "All Classes";
            this.btnAllClasses.UseVisualStyleBackColor = false;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.btnAllClasses, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.btnAllBE, 2, 6);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.comboBox1, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label6, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.btnAllSE, 2, 4);
            this.tableLayoutPanel1.Controls.Add(this.btnAllTE, 2, 5);
            this.tableLayoutPanel1.Controls.Add(this.btnExport, 1, 7);
            this.tableLayoutPanel1.Controls.Add(this.btnTeacherAssess, 2, 7);
            this.tableLayoutPanel1.Controls.Add(this.label7, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.label8, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.label9, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.label10, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.label5, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.label11, 2, 8);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 2, 9);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 1, 9);
            this.tableLayoutPanel1.Controls.Add(this.dateTimePicker1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnEditGrade, 2, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 77);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(33, 36, 3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 10;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(985, 588);
            this.tableLayoutPanel1.TabIndex = 9;
            // 
            // label7
            // 
            this.label7.AutoEllipsis = true;
            this.label7.AutoSize = true;
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Location = new System.Drawing.Point(94, 90);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.MaximumSize = new System.Drawing.Size(333, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(329, 39);
            this.label7.TabIndex = 15;
            this.label7.Text = "1. Please select Grade you wish to enter attendance for.";
            // 
            // panel2
            // 
            this.panel2.AutoSize = true;
            this.panel2.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panel2.Controls.Add(this.dataGridView1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(428, 303);
            this.panel2.Margin = new System.Windows.Forms.Padding(2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(554, 282);
            this.panel2.TabIndex = 23;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Transparent;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridView1.Size = new System.Drawing.Size(554, 282);
            this.dataGridView1.TabIndex = 21;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.Controls.Add(this.chkBoxSelGrade);
            this.flowLayoutPanel1.Controls.Add(this.chkShowConducted);
            this.flowLayoutPanel1.Controls.Add(this.button2);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(94, 303);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(2);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(329, 282);
            this.flowLayoutPanel1.TabIndex = 24;
            // 
            // chkBoxSelGrade
            // 
            this.chkBoxSelGrade.AutoSize = true;
            this.chkBoxSelGrade.Location = new System.Drawing.Point(2, 2);
            this.chkBoxSelGrade.Margin = new System.Windows.Forms.Padding(2);
            this.chkBoxSelGrade.Name = "chkBoxSelGrade";
            this.chkBoxSelGrade.Size = new System.Drawing.Size(238, 21);
            this.chkBoxSelGrade.TabIndex = 22;
            this.chkBoxSelGrade.Text = "Show only Selected Grade and Date->";
            this.chkBoxSelGrade.UseVisualStyleBackColor = true;
            this.chkBoxSelGrade.Visible = false;
            // 
            // chkShowConducted
            // 
            this.chkShowConducted.AutoSize = true;
            this.chkShowConducted.Location = new System.Drawing.Point(2, 27);
            this.chkShowConducted.Margin = new System.Windows.Forms.Padding(2);
            this.chkShowConducted.Name = "chkShowConducted";
            this.chkShowConducted.Size = new System.Drawing.Size(246, 21);
            this.chkShowConducted.TabIndex = 23;
            this.chkShowConducted.Text = "Include classes conducted in the view->";
            this.chkShowConducted.UseVisualStyleBackColor = true;
            this.chkShowConducted.Visible = false;
            this.chkShowConducted.CheckedChanged += new System.EventHandler(this.chkShowConducted_CheckedChanged);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Aquamarine;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button2.Location = new System.Drawing.Point(3, 53);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(195, 33);
            this.button2.TabIndex = 26;
            this.button2.Text = "View Attendance Stats";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Visible = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // ViewDailReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(985, 665);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(670, 56);
            this.Name = "ViewDailReport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "D.J.Sanghvi Reporting (ver 4.5)";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnTeacherAssess;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnAllTE;
        private System.Windows.Forms.Button btnAllSE;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button btnEditGrade;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnAllBE;
        private System.Windows.Forms.Button btnAllClasses;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.CheckBox chkBoxSelGrade;
        private System.Windows.Forms.CheckBox chkShowConducted;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnClose;

    }
}

