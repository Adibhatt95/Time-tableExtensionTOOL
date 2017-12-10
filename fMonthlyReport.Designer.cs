namespace TeachersAssessment
{
    partial class fMonthlyReport
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblTeacher = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.lblError = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnExport = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.SrNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Subj = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Branch = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Sem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NoSche = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NumActualHeld = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NumCanc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NumAdj = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ResAbsTeach = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ResPoor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ResOth = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NoLess20Per = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.No20To50 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.No50to80 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NoAbove80 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LightCyan;
            this.panel1.Controls.Add(this.lblTeacher);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.lblError);
            this.panel1.Controls.Add(this.dateTimePicker1);
            this.panel1.Controls.Add(this.lblTitle);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1120, 46);
            this.panel1.TabIndex = 1;
            // 
            // lblTeacher
            // 
            this.lblTeacher.AutoSize = true;
            this.lblTeacher.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTeacher.Location = new System.Drawing.Point(3, 16);
            this.lblTeacher.Name = "lblTeacher";
            this.lblTeacher.Size = new System.Drawing.Size(70, 16);
            this.lblTeacher.TabIndex = 4;
            this.lblTeacher.Text = "Teacher:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(755, 13);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(43, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "Go";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lblError
            // 
            this.lblError.AutoSize = true;
            this.lblError.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblError.ForeColor = System.Drawing.Color.Red;
            this.lblError.Location = new System.Drawing.Point(863, 13);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(0, 16);
            this.lblError.TabIndex = 2;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new System.Drawing.Point(638, 13);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(111, 20);
            this.dateTimePicker1.TabIndex = 1;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(420, 12);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(212, 20);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Montly Report Starting from :";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.LightCyan;
            this.panel2.Controls.Add(this.btnExport);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 487);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1120, 48);
            this.panel2.TabIndex = 2;
            // 
            // btnExport
            // 
            this.btnExport.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExport.Location = new System.Drawing.Point(770, 6);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(126, 32);
            this.btnExport.TabIndex = 0;
            this.btnExport.Text = "Export to Excel";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridView1.ColumnHeadersHeight = 100;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SrNo,
            this.Subj,
            this.Branch,
            this.Sem,
            this.NoSche,
            this.NumActualHeld,
            this.NumCanc,
            this.NumAdj,
            this.ResAbsTeach,
            this.ResPoor,
            this.ResOth,
            this.NoLess20Per,
            this.No20To50,
            this.No50to80,
            this.NoAbove80});
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.Location = new System.Drawing.Point(0, 46);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(1120, 441);
            this.dataGridView1.TabIndex = 3;
            // 
            // SrNo
            // 
            this.SrNo.HeaderText = "Sr. No.";
            this.SrNo.Name = "SrNo";
            this.SrNo.ReadOnly = true;
            // 
            // Subj
            // 
            this.Subj.HeaderText = "Subject";
            this.Subj.Name = "Subj";
            this.Subj.ReadOnly = true;
            // 
            // Branch
            // 
            this.Branch.HeaderText = "Branch";
            this.Branch.Name = "Branch";
            this.Branch.ReadOnly = true;
            // 
            // Sem
            // 
            this.Sem.HeaderText = "Semester";
            this.Sem.Name = "Sem";
            this.Sem.ReadOnly = true;
            // 
            // NoSche
            // 
            this.NoSche.HeaderText = "No. of Lect/Pract Scheduled";
            this.NoSche.Name = "NoSche";
            this.NoSche.ReadOnly = true;
            // 
            // NumActualHeld
            // 
            this.NumActualHeld.HeaderText = "No. of Lect/Pract actually held";
            this.NumActualHeld.Name = "NumActualHeld";
            this.NumActualHeld.ReadOnly = true;
            // 
            // NumCanc
            // 
            this.NumCanc.HeaderText = "No. of Lect/Pract Cancelled";
            this.NumCanc.Name = "NumCanc";
            this.NumCanc.ReadOnly = true;
            // 
            // NumAdj
            // 
            this.NumAdj.HeaderText = "No. of Lect/Pract Adjusted";
            this.NumAdj.Name = "NumAdj";
            this.NumAdj.ReadOnly = true;
            // 
            // ResAbsTeach
            // 
            this.ResAbsTeach.HeaderText = "Reason: Absence of Teacher";
            this.ResAbsTeach.Name = "ResAbsTeach";
            this.ResAbsTeach.ReadOnly = true;
            // 
            // ResPoor
            // 
            this.ResPoor.HeaderText = "Reason: Poor Attendance";
            this.ResPoor.Name = "ResPoor";
            this.ResPoor.ReadOnly = true;
            // 
            // ResOth
            // 
            this.ResOth.HeaderText = "Reason: Rescheuled for other Reasons";
            this.ResOth.Name = "ResOth";
            this.ResOth.ReadOnly = true;
            // 
            // NoLess20Per
            // 
            this.NoLess20Per.HeaderText = "No. of Lect/Pract with less than 20% attendance";
            this.NoLess20Per.Name = "NoLess20Per";
            this.NoLess20Per.ReadOnly = true;
            // 
            // No20To50
            // 
            this.No20To50.HeaderText = "No. of Lect/Pract with 20% to 50% attendance";
            this.No20To50.Name = "No20To50";
            this.No20To50.ReadOnly = true;
            // 
            // No50to80
            // 
            this.No50to80.HeaderText = "No. of Lect/Pract with 50% to 80% attendance";
            this.No50to80.Name = "No50to80";
            this.No50to80.ReadOnly = true;
            // 
            // NoAbove80
            // 
            this.NoAbove80.HeaderText = "No. of Lect/Pract with more than 90% attendance";
            this.NoAbove80.Name = "NoAbove80";
            this.NoAbove80.ReadOnly = true;
            // 
            // fMonthlyReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1120, 535);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "fMonthlyReport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Monthly Report";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lblError;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn SrNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Subj;
        private System.Windows.Forms.DataGridViewTextBoxColumn Branch;
        private System.Windows.Forms.DataGridViewTextBoxColumn Sem;
        private System.Windows.Forms.DataGridViewTextBoxColumn NoSche;
        private System.Windows.Forms.DataGridViewTextBoxColumn NumActualHeld;
        private System.Windows.Forms.DataGridViewTextBoxColumn NumCanc;
        private System.Windows.Forms.DataGridViewTextBoxColumn NumAdj;
        private System.Windows.Forms.DataGridViewTextBoxColumn ResAbsTeach;
        private System.Windows.Forms.DataGridViewTextBoxColumn ResPoor;
        private System.Windows.Forms.DataGridViewTextBoxColumn ResOth;
        private System.Windows.Forms.DataGridViewTextBoxColumn NoLess20Per;
        private System.Windows.Forms.DataGridViewTextBoxColumn No20To50;
        private System.Windows.Forms.DataGridViewTextBoxColumn No50to80;
        private System.Windows.Forms.DataGridViewTextBoxColumn NoAbove80;
        private System.Windows.Forms.Label lblTeacher;
        private System.Windows.Forms.Button btnExport;
    }
}