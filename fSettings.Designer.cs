namespace TeachersAssessment
{
    partial class fSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fSettings));
            this.flowLayoutTeacher = new System.Windows.Forms.FlowLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.btnImportStudents = new System.Windows.Forms.Button();
            this.btnTeacherRoles = new System.Windows.Forms.Button();
            this.flowLayoutTeacher.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutTeacher
            // 
            this.flowLayoutTeacher.AutoSize = true;
            this.flowLayoutTeacher.Controls.Add(this.label2);
            this.flowLayoutTeacher.Controls.Add(this.comboBox1);
            this.flowLayoutTeacher.Controls.Add(this.btnImportStudents);
            this.flowLayoutTeacher.Controls.Add(this.btnTeacherRoles);
            this.flowLayoutTeacher.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutTeacher.Location = new System.Drawing.Point(10, 10);
            this.flowLayoutTeacher.Name = "flowLayoutTeacher";
            this.flowLayoutTeacher.Size = new System.Drawing.Size(415, 167);
            this.flowLayoutTeacher.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(4, 0);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(120, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "Select Grade:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(4, 24);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(4);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(307, 28);
            this.comboBox1.TabIndex = 5;
            // 
            // btnImportStudents
            // 
            this.btnImportStudents.BackColor = System.Drawing.Color.SeaGreen;
            this.btnImportStudents.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.btnImportStudents.Location = new System.Drawing.Point(3, 59);
            this.btnImportStudents.Name = "btnImportStudents";
            this.btnImportStudents.Size = new System.Drawing.Size(409, 46);
            this.btnImportStudents.TabIndex = 0;
            this.btnImportStudents.Text = "Import Student Records";
            this.btnImportStudents.UseVisualStyleBackColor = false;
            this.btnImportStudents.Click += new System.EventHandler(this.btnImportStudents_Click);
            // 
            // btnTeacherRoles
            // 
            this.btnTeacherRoles.BackColor = System.Drawing.Color.SeaGreen;
            this.btnTeacherRoles.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.btnTeacherRoles.Location = new System.Drawing.Point(3, 111);
            this.btnTeacherRoles.Name = "btnTeacherRoles";
            this.btnTeacherRoles.Size = new System.Drawing.Size(409, 49);
            this.btnTeacherRoles.TabIndex = 1;
            this.btnTeacherRoles.Text = "Set Class Teachers/Roles";
            this.btnTeacherRoles.UseVisualStyleBackColor = false;
            this.btnTeacherRoles.Click += new System.EventHandler(this.btnTeacherRoles_Click);
            // 
            // fSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(443, 198);
            this.Controls.Add(this.flowLayoutTeacher);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "fSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.flowLayoutTeacher.ResumeLayout(false);
            this.flowLayoutTeacher.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutTeacher;
        private System.Windows.Forms.Button btnImportStudents;
        private System.Windows.Forms.Button btnTeacherRoles;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}