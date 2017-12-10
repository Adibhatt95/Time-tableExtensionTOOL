namespace TeachersAssessment
{
    partial class FormADDCLASSTEACHERS
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormADDCLASSTEACHERS));
            this.tablerDBDataSet = new TeachersAssessment.TablerDBDataSet();
            this.tablerDBDataSetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gradesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.teachersBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Grade = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Teacher = new System.Windows.Forms.DataGridViewComboBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.tablerDBDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tablerDBDataSetBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gradesBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teachersBindingSource)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // tablerDBDataSet
            // 
            this.tablerDBDataSet.DataSetName = "TablerDBDataSet";
            this.tablerDBDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // tablerDBDataSetBindingSource
            // 
            this.tablerDBDataSetBindingSource.DataSource = this.tablerDBDataSet;
            this.tablerDBDataSetBindingSource.Position = 0;
            // 
            // gradesBindingSource
            // 
            this.gradesBindingSource.DataMember = "Grades";
            this.gradesBindingSource.DataSource = this.tablerDBDataSetBindingSource;
            // 
            // teachersBindingSource
            // 
            this.teachersBindingSource.DataMember = "Teachers";
            this.teachersBindingSource.DataSource = this.tablerDBDataSetBindingSource;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 182);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(888, 56);
            this.panel1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.AutoSize = true;
            this.button1.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(41, 10);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(161, 34);
            this.button1.TabIndex = 0;
            this.button1.Text = "&Save";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.Grade,
            this.Teacher});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 28;
            this.dataGridView1.Size = new System.Drawing.Size(888, 182);
            this.dataGridView1.TabIndex = 1;
            // 
            // ID
            // 
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.Visible = false;
            // 
            // Grade
            // 
            this.Grade.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Grade.DataSource = this.gradesBindingSource;
            this.Grade.DisplayMember = "GradeName";
            this.Grade.HeaderText = "Grade";
            this.Grade.Name = "Grade";
            this.Grade.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Grade.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Grade.ValueMember = "GradeID";
            this.Grade.Width = 200;
            // 
            // Teacher
            // 
            this.Teacher.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Teacher.DataSource = this.teachersBindingSource;
            this.Teacher.DisplayMember = "TeachersName";
            this.Teacher.HeaderText = "Teacher";
            this.Teacher.Name = "Teacher";
            this.Teacher.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Teacher.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Teacher.ValueMember = "TeachersID";
            // 
            // FormADDCLASSTEACHERS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(888, 238);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.Name = "FormADDCLASSTEACHERS";
            this.Text = "Manage Grade & Class Teachers";
            ((System.ComponentModel.ISupportInitialize)(this.tablerDBDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tablerDBDataSetBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gradesBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teachersBindingSource)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource gradesBindingSource;
        private System.Windows.Forms.BindingSource tablerDBDataSetBindingSource;
        private TablerDBDataSet tablerDBDataSet;
        private System.Windows.Forms.BindingSource teachersBindingSource;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewComboBoxColumn Grade;
        private System.Windows.Forms.DataGridViewComboBoxColumn Teacher;

    }
}