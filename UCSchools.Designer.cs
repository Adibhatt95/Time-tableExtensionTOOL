namespace TeachersAssessment
{
    partial class UCSchools
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
            this.components = new System.ComponentModel.Container();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.schoolIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.schoolNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.schoolsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tablerDBDataSet = new TeachersAssessment.TablerDBDataSet();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.schoolsBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.tbTimeFrom = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.tbFirstBreakDur = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tbSecondBrkDur = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tbMinDur = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.tbBreak2Dur = new System.Windows.Forms.TextBox();
            this.tbNumPeriod = new System.Windows.Forms.TextBox();
            this.tbDurPeriod = new System.Windows.Forms.TextBox();
            this.tbBreak1Dur = new System.Windows.Forms.TextBox();
            this.tbNumPeriodSat = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.schoolsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tablerDBDataSet)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.schoolsBindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.Controls.Add(this.btnSave);
            this.flowLayoutPanel1.Controls.Add(this.btnCancel);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 321);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(749, 29);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(3, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(84, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.schoolIDDataGridViewTextBoxColumn,
            this.schoolNameDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.schoolsBindingSource;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Left;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(311, 321);
            this.dataGridView1.TabIndex = 1;
            // 
            // schoolIDDataGridViewTextBoxColumn
            // 
            this.schoolIDDataGridViewTextBoxColumn.DataPropertyName = "SchoolID";
            this.schoolIDDataGridViewTextBoxColumn.HeaderText = "SchoolID";
            this.schoolIDDataGridViewTextBoxColumn.Name = "schoolIDDataGridViewTextBoxColumn";
            this.schoolIDDataGridViewTextBoxColumn.ReadOnly = true;
            this.schoolIDDataGridViewTextBoxColumn.Visible = false;
            // 
            // schoolNameDataGridViewTextBoxColumn
            // 
            this.schoolNameDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.schoolNameDataGridViewTextBoxColumn.DataPropertyName = "SchoolName";
            this.schoolNameDataGridViewTextBoxColumn.HeaderText = "School Name";
            this.schoolNameDataGridViewTextBoxColumn.Name = "schoolNameDataGridViewTextBoxColumn";
            // 
            // schoolsBindingSource
            // 
            this.schoolsBindingSource.DataMember = "Schools";
            this.schoolsBindingSource.DataSource = this.tablerDBDataSet;
            // 
            // tablerDBDataSet
            // 
            this.tablerDBDataSet.DataSetName = "TablerDBDataSet";
            this.tablerDBDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.comboBox1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tbTimeFrom, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.button1, 1, 10);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.tbFirstBreakDur, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.label6, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.tbSecondBrkDur, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.label7, 0, 8);
            this.tableLayoutPanel1.Controls.Add(this.tbMinDur, 1, 8);
            this.tableLayoutPanel1.Controls.Add(this.label8, 0, 9);
            this.tableLayoutPanel1.Controls.Add(this.label9, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.label10, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.tbBreak2Dur, 1, 7);
            this.tableLayoutPanel1.Controls.Add(this.tbNumPeriod, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.tbDurPeriod, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.tbBreak1Dur, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.tbNumPeriodSat, 1, 9);
            this.tableLayoutPanel1.Controls.Add(this.label11, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.label12, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.label13, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.label14, 2, 4);
            this.tableLayoutPanel1.Controls.Add(this.label15, 2, 5);
            this.tableLayoutPanel1.Controls.Add(this.label16, 2, 6);
            this.tableLayoutPanel1.Controls.Add(this.label17, 2, 7);
            this.tableLayoutPanel1.Controls.Add(this.label18, 2, 8);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(311, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 11;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(438, 321);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(156, 27);
            this.label1.TabIndex = 0;
            this.label1.Text = "School:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBox1
            // 
            this.comboBox1.DataSource = this.schoolsBindingSource1;
            this.comboBox1.DisplayMember = "SchoolName";
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(165, 3);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(122, 21);
            this.comboBox1.TabIndex = 1;
            this.comboBox1.ValueMember = "SchoolID";
            // 
            // schoolsBindingSource1
            // 
            this.schoolsBindingSource1.DataMember = "Schools";
            this.schoolsBindingSource1.DataSource = this.tablerDBDataSet;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(3, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(156, 26);
            this.label2.TabIndex = 2;
            this.label2.Text = "Time From:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbTimeFrom
            // 
            this.tbTimeFrom.Location = new System.Drawing.Point(165, 30);
            this.tbTimeFrom.Name = "tbTimeFrom";
            this.tbTimeFrom.Size = new System.Drawing.Size(122, 20);
            this.tbTimeFrom.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(3, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(156, 26);
            this.label3.TabIndex = 4;
            this.label3.Text = "Number of Periods in a day:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Location = new System.Drawing.Point(3, 79);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(156, 26);
            this.label4.TabIndex = 6;
            this.label4.Text = "Duration of Period:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(165, 264);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(122, 51);
            this.button1.TabIndex = 100;
            this.button1.Text = "Save these settings for the above school";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Location = new System.Drawing.Point(3, 105);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(156, 26);
            this.label5.TabIndex = 9;
            this.label5.Text = "First Break Slot:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbFirstBreakDur
            // 
            this.tbFirstBreakDur.Location = new System.Drawing.Point(165, 108);
            this.tbFirstBreakDur.Name = "tbFirstBreakDur";
            this.tbFirstBreakDur.Size = new System.Drawing.Size(122, 20);
            this.tbFirstBreakDur.TabIndex = 10;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Location = new System.Drawing.Point(3, 157);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(156, 26);
            this.label6.TabIndex = 11;
            this.label6.Text = "Second Break Slot:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbSecondBrkDur
            // 
            this.tbSecondBrkDur.Location = new System.Drawing.Point(165, 160);
            this.tbSecondBrkDur.Name = "tbSecondBrkDur";
            this.tbSecondBrkDur.Size = new System.Drawing.Size(122, 20);
            this.tbSecondBrkDur.TabIndex = 21;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Location = new System.Drawing.Point(3, 209);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(156, 26);
            this.label7.TabIndex = 13;
            this.label7.Text = "Minimum Duration:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbMinDur
            // 
            this.tbMinDur.Location = new System.Drawing.Point(165, 212);
            this.tbMinDur.Name = "tbMinDur";
            this.tbMinDur.Size = new System.Drawing.Size(122, 20);
            this.tbMinDur.TabIndex = 25;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label8.Location = new System.Drawing.Point(3, 235);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(156, 26);
            this.label8.TabIndex = 15;
            this.label8.Text = "Number of periods on Saturday:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label9.Location = new System.Drawing.Point(3, 131);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(156, 26);
            this.label9.TabIndex = 17;
            this.label9.Text = "First Break Duration:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label10.Location = new System.Drawing.Point(3, 183);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(156, 26);
            this.label10.TabIndex = 18;
            this.label10.Text = "Second Break Duration:";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbBreak2Dur
            // 
            this.tbBreak2Dur.Location = new System.Drawing.Point(165, 186);
            this.tbBreak2Dur.Name = "tbBreak2Dur";
            this.tbBreak2Dur.Size = new System.Drawing.Size(122, 20);
            this.tbBreak2Dur.TabIndex = 23;
            // 
            // tbNumPeriod
            // 
            this.tbNumPeriod.Location = new System.Drawing.Point(165, 56);
            this.tbNumPeriod.Name = "tbNumPeriod";
            this.tbNumPeriod.Size = new System.Drawing.Size(122, 20);
            this.tbNumPeriod.TabIndex = 5;
            // 
            // tbDurPeriod
            // 
            this.tbDurPeriod.Location = new System.Drawing.Point(165, 82);
            this.tbDurPeriod.Name = "tbDurPeriod";
            this.tbDurPeriod.Size = new System.Drawing.Size(122, 20);
            this.tbDurPeriod.TabIndex = 7;
            // 
            // tbBreak1Dur
            // 
            this.tbBreak1Dur.Location = new System.Drawing.Point(165, 134);
            this.tbBreak1Dur.Name = "tbBreak1Dur";
            this.tbBreak1Dur.Size = new System.Drawing.Size(122, 20);
            this.tbBreak1Dur.TabIndex = 19;
            // 
            // tbNumPeriodSat
            // 
            this.tbNumPeriodSat.Location = new System.Drawing.Point(165, 238);
            this.tbNumPeriodSat.Name = "tbNumPeriodSat";
            this.tbNumPeriodSat.Size = new System.Drawing.Size(122, 20);
            this.tbNumPeriodSat.TabIndex = 29;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label11.Location = new System.Drawing.Point(293, 27);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(142, 26);
            this.label11.TabIndex = 101;
            this.label11.Text = "Format HH:MM";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label12.Location = new System.Drawing.Point(293, 53);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(142, 26);
            this.label12.TabIndex = 102;
            this.label12.Text = "Example 10, includes breaks";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label13.Location = new System.Drawing.Point(293, 79);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(142, 26);
            this.label13.TabIndex = 103;
            this.label13.Text = "in mins e.g. 45 mins";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label14.Location = new System.Drawing.Point(293, 105);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(142, 26);
            this.label14.TabIndex = 104;
            this.label14.Text = "3rd or 4th slot in a day";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label15.Location = new System.Drawing.Point(293, 131);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(142, 26);
            this.label15.TabIndex = 105;
            this.label15.Text = "in mins e.g. 15 mins";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label16.Location = new System.Drawing.Point(293, 157);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(142, 26);
            this.label16.TabIndex = 106;
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label17.Location = new System.Drawing.Point(293, 183);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(142, 26);
            this.label17.TabIndex = 107;
            this.label17.Text = "in mins e.g. 45 mins";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label18.Location = new System.Drawing.Point(293, 209);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(142, 26);
            this.label18.TabIndex = 108;
            this.label18.Text = "in mins e.g. 15 mins";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // UCSchools
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "UCSchools";
            this.Size = new System.Drawing.Size(749, 350);
            this.flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.schoolsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tablerDBDataSet)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.schoolsBindingSource1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.BindingSource schoolsBindingSource;
        private TablerDBDataSet tablerDBDataSet;
        private System.Data.MPrSQL.MPrSQLDataAdapter schoolsTableAdapter;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.BindingSource schoolsBindingSource1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbTimeFrom;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbNumPeriod;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbDurPeriod;
        private System.Windows.Forms.DataGridViewTextBoxColumn schoolIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn schoolNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbFirstBreakDur;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbSecondBrkDur;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbMinDur;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tbNumPeriodSat;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox tbBreak1Dur;
        private System.Windows.Forms.TextBox tbBreak2Dur;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;

    }
}
