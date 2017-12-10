using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;

namespace TeachersAssessment
{

    public partial class ViewDailReport : Form
    {
        //TablerDBDataSet.GradesDataTable GRADES;// = new TablerDBDataSet.GradesDataTable();
        TablerDBDataSet.RoomsDataTable ROOMS = new TablerDBDataSet.RoomsDataTable();
        TablerDBDataSet.TSFInalDataTable TSFinalTbl = new TablerDBDataSet.TSFInalDataTable();
        TablerDBDataSet.TeachersDataTable Teachers = new TablerDBDataSet.TeachersDataTable();
        TablerDBDataSet.SubjectsDataTable Subjects = new TablerDBDataSet.SubjectsDataTable();
        DataSet1.AttendanceDataTable Attend = new DataSet1.AttendanceDataTable();
        TablerDBDataSet.PreAssignDataTable PreAssign;
        tGlobal p_global = tGlobal.GetInstance(-1);
        TablerDBDataSet.GradesPreAssignDataTable GradePreAssign;
        TablerDBDataSet.AllocationTableDataTable AllocationTable = new TablerDBDataSet.AllocationTableDataTable();
        //System.Data.MPrSQL.MPrSQLDataAdapter p_allocda;
        DataSet1.ClassTeachersDataTable classteachers = new DataSet1.ClassTeachersDataTable();
        System.Data.MPrSQL.MPrSQLDataAdapter classteachersDA = MyTableAdapters.ClassTeachersAdapter();
        DataSet1.AssessmentDataTable assessTable = new DataSet1.AssessmentDataTable();
        System.Collections.Hashtable p_teachersFortheSubPreAssign = new System.Collections.Hashtable();
        TablerDBDataSet.GradesDataTable p_grades = new TablerDBDataSet.GradesDataTable();
        System.Data.MPrSQL.MPrSQLDataAdapter AttendDA = MyTableAdapters.AttendanceTableAdapter();
        System.Data.MPrSQL.MPrSQLDataAdapter AssessDA = MyTableAdapters.AssessTableAdapter();
        string p_teacherName = null;

        public ViewDailReport(int l_gradeID, string l_teacherName,int teacherID)
        {
            InitializeComponent();           
            
            System.Data.MPrSQL.MPrSQLDataAdapter SubDA = MyTableAdapters.SubjectsTableAdapter();
            System.Data.MPrSQL.MPrSQLDataAdapter TeacherDA = MyTableAdapters.TeachersTableAdapter();
            System.Data.MPrSQL.MPrSQLDataAdapter RoomDA = MyTableAdapters.RoomsTableAdapter();
            System.Data.MPrSQL.MPrSQLDataAdapter gradesTableAdapter = MyTableAdapters.GradesTableAdapter();
            System.Data.MPrSQL.MPrSQLDataAdapter allocationDA = MyTableAdapters.AllocationTableTableAdapter();
            System.Data.MPrSQL.MPrSQLDataAdapter TsFinal = MyTableAdapters.TSFInalTableAdapter();
          
            label7.Visible = false;
            label8.Visible = false;
            label9.Visible = false;
            label10.Visible = false;
            SubDA.Fill(Subjects);
            classteachersDA.Fill(classteachers);
            TeacherDA.Fill(Teachers);
            RoomDA.Fill(ROOMS);
            //AssessDA.Fill(assessTable);
            allocationDA.Fill(AllocationTable);
            gradesTableAdapter.Fill(p_grades);
          //  DataSet1.AssessmentRow[] assessrws = (DataSet1.AssessmentRow[])assessTable.Select(assessTable.Columns + " = *");
            if (p_global.TeacherIDSelected <= 0)
            {
                comboBox1.DataSource = p_grades;
                comboBox1.DisplayMember = p_grades.GradeNameColumn.ColumnName;
                comboBox1.ValueMember = p_grades.GradeIDColumn.ColumnName;
                chkBoxSelGrade.Visible = true;
                chkShowConducted.Visible = true;
                
                chkBoxSelGrade.CheckedChanged += new EventHandler(chkBoxSelectedGrade_CheckedChanged);
                btnTeacherAssess.Visible = false;
                dataGridView1.ColumnCount = 5;
                dataGridView1.Columns[0].Name = "Date";
                dataGridView1.Columns[0].ValueType = typeof(System.DateTime);
                dataGridView1.Columns[1].Name = "Grade";
                dataGridView1.Columns[2].Name = "Time";
                dataGridView1.Columns[3].Name = "Teacher";
                dataGridView1.Columns[4].Name = "Reason";
                dataGridView1.AutoSize = true;
                label6.Visible = true;
                label6.Text = "Welcome, " + l_teacherName;
                loadDataGrid();

                DataSet1.ClassTeachersRow[] classTRows = (DataSet1.ClassTeachersRow[])classteachers.Select(classteachers.TeacherIDColumn.ColumnName
                     + " = " + teacherID);

                if (classTRows.Length > 0)
                {
                    comboBox1.SelectedValue = classTRows[0].GradeID;
                    if (classTRows[0].GradeID == (int)tGlobal.GRADE_CODES.ALL)
                    {
                        comboBox1.Enabled = true;
                    }
                    else
                    {
                        comboBox1.Enabled = false;
                    }
                }
                else
                {
                    comboBox1.Enabled = true;
                }
               // dataGridView1.Rows.Add(assessTable.DateColumn.ColumnName);
            }
            else
            {
                comboBox1.DataSource = p_grades;
                comboBox1.DisplayMember = p_grades.GradeNameColumn.ColumnName;
                comboBox1.ValueMember = p_grades.GradeIDColumn.ColumnName;
                comboBox1.Visible = false;
                label2.Visible = false;
                button2.Visible = true;   
                btnTeacherAssess.Visible = true;
                btnEditGrade.Visible = false;
                btnAllBE.Visible = false;
                btnAllClasses.Visible = false;
                btnAllSE.Visible = false;
                btnAllTE.Visible = false;
                btnExport.Visible = false;
                label5.Visible = false;
                label6.Visible = true;
                p_teacherName = Teachers.FindByTeachersID(p_global.TeacherIDSelected).TeachersName.ToString();
                label6.Text = "Welcome, " + p_teacherName;
                label7.Visible = true; 
                label8.Visible = true;
                label9.Visible = true;
                label10.Visible = true;
                
                dataGridView1.ColumnCount = 4;
                dataGridView1.Columns[0].Name = "Date";
                dataGridView1.Columns[0].ValueType = typeof(System.DateTime);
                dataGridView1.Columns[1].Name = "Grade";
                dataGridView1.Columns[2].Name = "Time";
                dataGridView1.Columns[3].Name = "Attendance";
                dataGridView1.AutoSize = true;
                loadDataGridAttend();
                label11.Text = "Lectures/Practicals for which attendance is given already:";
            }
           
            TsFinal.Fill(TSFinalTbl);
            

            btnAllBE.Click +=new EventHandler(btnAllBE_Click);
            btnAllClasses.Click +=new EventHandler(btnAllClasses_Click);
            btnAllSE.Click +=new EventHandler(btnAllSE_Click);
            btnAllTE.Click +=new EventHandler(btnAllTE_Click);
            btnEditGrade.Click += new EventHandler(btnEditSelectedGrade_Click);
           // btnTeacherAssess.Click +=new EventHandler(btnTeacherAssess_Click_1);
            //btnExport.Click +=new EventHandler(btnExport_Click_1);

            dataGridView1.CellFormatting += new DataGridViewCellFormattingEventHandler(dataGridView1_CellFormatting);

            //this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);

        }

        public void HideDailyReportEditButtons()
        {
            btnExport.Visible = true;
            btnTeacherAssess.Visible = false;
            btnEditGrade.Visible = false;
            btnAllBE.Visible = false;
            btnAllClasses.Visible = false;
            btnAllSE.Visible = false;
            btnAllTE.Visible = false;
            
            label5.Visible = false;
            label6.Visible = false;
        }
        void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            /*DialogResult l_re = MessageBox.Show("   Do you want to logout and exit ?", "Logout", MessageBoxButtons.YesNoCancel);
            if (l_re != System.Windows.Forms.DialogResult.Yes)
            {
                e.Cancel = true;
            }*/

        }

        void loadDataGridAttend()
        {
            System.Data.MPrSQL.MPrSQLDataAdapter l_sda = MyTableAdapters.StudentsTableAdapter();
            DataSet1.StudentsDataTable l_stbl = new DataSet1.StudentsDataTable();
            l_sda.Fill(l_stbl);
            Attend.Clear();
            dataGridView1.Rows.Clear();
            AttendDA.Fill(Attend);
            string select;
            DataSet1.AttendanceRow[] AttendRows = (DataSet1.AttendanceRow[])Attend.Select(Attend.TeacherIDColumn.ColumnName + " = " + p_global.TeacherIDSelected, Attend.SlotIDColumn.ColumnName+" , " 
                                                                                                        + Attend.DateColumn.ColumnName);
            int slottemp=0;
            DateTime l_dtemp = DateTime.MinValue;
            foreach (DataSet1.AttendanceRow AttendRow in AttendRows)
            {
                if (slottemp != AttendRow.SlotID || l_dtemp != AttendRow.Date)
                {
                    object l_students_count = 0;

                    if (AttendRow.IsPracticalorNOTNull() == false && AttendRow.PracticalorNOT && AttendRow.GradeNAME.Length > 3)
                    {
                        int i = AttendRow.GradeNAME.IndexOf("(");
                        int j = AttendRow.GradeNAME.IndexOf(")");
                        string l_batch = "";
                        if (i > 0 && j > 0)
                        {
                            l_batch = AttendRow.GradeNAME.Substring(i+1, j-i-1);
                        }
                        l_students_count = l_stbl.Compute("count(" + l_stbl.SAPIDColumn.ColumnName + ")", l_stbl.GradeIDColumn.ColumnName + " = " + AttendRow.GradeID.ToString() + " AND " +
                                                                                                        l_stbl.BatchColumn.ColumnName + " = " + "'" + l_batch +"'");
                    }
                    else
                    {
                        l_students_count = l_stbl.Compute("count(" + l_stbl.SAPIDColumn.ColumnName + ")", l_stbl.GradeIDColumn.ColumnName + " = " + AttendRow.GradeID.ToString());
                    }

                    slottemp = AttendRow.SlotID;
                    l_dtemp = AttendRow.Date;
                    select = Attend.TeacherIDColumn.ColumnName + " = " + p_global.TeacherIDSelected + " AND " + 
                        Attend.DateColumn.ColumnName + " = '" + AttendRow.Date + "' AND " + Attend.SlotIDColumn.ColumnName + " = " + slottemp + " AND " 
                        + Attend.AbsORPresColumn.ColumnName + " = " + true;
                    DataSet1.AttendanceRow[] Attendthatdayandslot = (DataSet1.AttendanceRow[])Attend.Select(select);
                    dataGridView1.Rows.Add(AttendRow.Date, AttendRow.GradeNAME, AttendRow.Time, Attendthatdayandslot.Length.ToString() + "/" + l_students_count.ToString());
                }

            }
            dataGridView1.Sort(dataGridView1.Columns[0], ListSortDirection.Descending);
        }

        void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (p_global.TeacherIDSelected > 0)
                return;
            if (e.RowIndex >= 0 && dataGridView1.Rows[e.RowIndex].IsNewRow == false)
            {
                if (dataGridView1[4, e.RowIndex].Value is string &&
                        ((string)dataGridView1[4, e.RowIndex].Value).Equals("CONDUCTED"))
                {
                    dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = System.Drawing.Color.LightGreen;
                }
                else
                {
                    dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = System.Drawing.Color.IndianRed;
                }
            }
        }

        void chkBoxSelectedGrade_CheckedChanged(object sender, EventArgs e)
        {
            loadDataGrid();
        }

        void loadDataGrid()
        {
            if (assessTable != null)
            {
                assessTable.Clear();
            }
            dataGridView1.Hide(); 
            Cursor.Current = Cursors.WaitCursor;

            dataGridView1.Rows.Clear();

            Cursor.Current = Cursors.Default;

            AssessDA.Fill(assessTable);
            
            foreach (DataSet1.AssessmentRow Assrw in assessTable)
            {
                if (chkShowConducted.Checked == false && Assrw.IsIsConductedNull() == false && Assrw.IsConducted) continue;

                if (chkBoxSelGrade.Checked && comboBox1.SelectedValue != null)
                {
                    if (Assrw.Date != dateTimePicker1.Value.Date 
                        || (Assrw.GradeID > 0 && Assrw.GradeID != (int)comboBox1.SelectedValue))
                    {
                        continue;
                    }
                }
                string l_time = "N.A";
                
                /*vIEWDataSet1.TimeTableViewDataTable l_ttv_table = p_global.GetViewTable();////
                int l_row = (Assrw.SlotNum % 12);
                if (l_row == 0)
                {
                    l_row = 11;
                }
                else
                {
                    l_row = l_row - 1;
                }
                l_time = l_ttv_table[l_row].LectureHr;*/
                l_time = Assrw.Time;
                TablerDBDataSet.TeachersRow l_trw = Teachers.FindByTeachersID(Assrw.TeacherID);
                string l_tchrName = "N.A";
                
                if (l_trw != null)
                {
                    l_tchrName = l_trw.TeachersName;
                }
                else if (Assrw.TeacherID == -1000)
                {
                    l_tchrName = "All Teachers (practicals)";
                }
                

                string l_grdName = "N.A";
                if (Assrw.GradeID > 0)
                {
                    TablerDBDataSet.GradesRow l_grw = p_grades.FindByGradeID(Assrw.GradeID);
                    if (l_grw != null)
                    {
                        l_grdName = l_grw.GradeName;
                    }
                }
                else if (Math.Abs(Assrw.GradeID) < tGlobal.GRADE_NAME.Length)
                {
                    l_grdName = tGlobal.GRADE_NAME[Math.Abs(Assrw.GradeID)];
                }

                string l_reason = "";
                if (!Assrw.IsReasonNull())
                {
                    l_reason = Assrw.Reason;
                }
                if (Assrw.IsConducted)
                {
                    l_reason = "CONDUCTED";
                }
                if (l_reason.Equals("Swapped"))
                {
                    string l_tchrNameSwapped = "N.A";
                    l_trw = Teachers.FindByTeachersID(Assrw.TeacherIDSwapped);
                    if (l_trw != null)
                    {
                        l_tchrNameSwapped = l_trw.TeachersName;
                    }
                    //string[] row = new string[] { Assrw.Date/*.ToString().Substring(0, 10)*/, l_time, l_tchrName, Assrw.Reason + " by " + l_tchrNameSwapped };
                    dataGridView1.Rows.Add(Assrw.Date, l_grdName, l_time, l_tchrName, l_reason + " by " + l_tchrNameSwapped);
                    continue;
                }
                {
                    //string[] row = new string[] { Assrw.Date.ToString().Substring(0, 10), l_time, l_tchrName, Assrw.Reason };
                    dataGridView1.Rows.Add(Assrw.Date, l_grdName, l_time, l_tchrName, l_reason);
                }
            }
            
            dataGridView1.Sort(dataGridView1.Columns[0], ListSortDirection.Descending);
            dataGridView1.ClearSelection();
            dataGridView1.Show();
        }

        void exitRestart(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(500);
            System.Diagnostics.Process.Start(Application.ExecutablePath);
            Application.Exit();
        }

        private void btnEditSelectedGrade_Click(object sender, EventArgs e)
        {            
         
            if (dateTimePicker1.Value.DayOfWeek.ToString() == "Sunday")
            {
                MessageBox.Show("Sunday Selected");
                return;
            }

            if (comboBox1.SelectedValue == null)
            {
                MessageBox.Show("Please select appropriate grade");
                return;
            }

            fViewTimeTable l_fviewTT = new fViewTimeTable(p_grades, ROOMS, TSFinalTbl, Teachers, Subjects, PreAssign, AllocationTable, GradePreAssign, p_teachersFortheSubPreAssign);
            try
            {
                l_fviewTT.FillTimeTableTblandBindWithGridView(-1, (int)(comboBox1.SelectedValue), comboBox1.Text, dateTimePicker1.Value);

                l_fviewTT.ShowDialog();
                loadDataGrid();
            }
            catch (Exception l_e)
            {
                MessageBox.Show("Error showing grade time table, " + l_e.Message);
            }
          
        }

        private void btnExport_Click_1(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            string l_path = null;
            bool forgot = checkIfForgotten();
            if (forgot == true)
            {
                
            }
            else
            {
                ExportToExcel l_exportToExcel = new ExportToExcel();

                l_exportToExcel.Export(dateTimePicker1.Value, l_path, AllocationTable, Subjects, Teachers, p_grades, TSFinalTbl, comboBox1.Text);

                Cursor.Current = Cursors.Default;
            }
        }
       bool checkIfForgotten()
        {
            p_global.Set_TimeTableglobal();
            int a = Convert.ToInt32( dateTimePicker1.Value.DayOfWeek);
                 
           int col = p_global.NumOfSlotsInADay *(a-1)+1;
           string classteacherswhoForgot = "Class Teacher(s) of ";
           string classWhichHasProjectDay = "";
           int counttotal;
           int j=0;
           foreach (TablerDBDataSet.GradesRow l_grdrw in p_grades) // int gradeid = 0; gradeid <= l_grades.Rows.Count; gradeid++)
           {
               int i = l_grdrw.GradeID;

               TablerDBDataSet.AllocationTableRow[] l_allocRowsAllforonegradeLECT = (TablerDBDataSet.AllocationTableRow[])
                               AllocationTable.Select(AllocationTable.SlotAllotedColumn.ColumnName + " >= " + col + " AND " + AllocationTable.SlotAllotedColumn.ColumnName + " <= " + (col + p_global.NumOfSlotsInADay-1) + " AND " +
                               AllocationTable.GRDIDColumn.ColumnName + " = " + i + " AND " + AllocationTable.DurationColumn.ColumnName
                               + " = " + 60);
               TablerDBDataSet.AllocationTableRow[] l_allocRowsAllforonegradePRACT = (TablerDBDataSet.AllocationTableRow[])
                               AllocationTable.Select(AllocationTable.SlotAllotedColumn.ColumnName + " >= " + col + " AND " + AllocationTable.SlotAllotedColumn.ColumnName + " <= " + (col + p_global.NumOfSlotsInADay-1) + " AND " +
                               AllocationTable.GRDIDColumn.ColumnName + " = " + i + " AND " + AllocationTable.DurationColumn.ColumnName
                               + " = " + 120);
               counttotal = l_allocRowsAllforonegradeLECT.Length / 2 + l_allocRowsAllforonegradePRACT.Length / 4;
               DataView l_dv = new DataView(assessTable);
               l_dv.RowFilter = assessTable.DateColumn.ColumnName + " = '" + dateTimePicker1.Value.Date
                                                + "' AND " + assessTable.GradeIDColumn.ColumnName + " = " + i;
               //DataSet1.AssessmentRow[] l_assessRws = (DataSet1.AssessmentRow[])assessTable.Select(assessTable.DateColumn.ColumnName + " = '" + dateTimePicker1.Value.Date
               //    + "' AND " + assessTable.GradeIDColumn.ColumnName + " = " + i);
               DataTable l_tbl = l_dv.ToTable(true, assessTable.SlotNumColumn.ColumnName);
               //if (l_assessRws.Length != counttotal)
               if (l_tbl.Rows.Count < counttotal)
               {
                   TablerDBDataSet.GradesRow l_grw = p_grades.FindByGradeID(i);
                   if (l_grw != null)
                   {
                       classteacherswhoForgot += (l_grw.GradeName + " ");
                   }
               }
               if (counttotal == 0)
               {
                   j = i;
               }
           }
           
           if (classteacherswhoForgot.Equals("Class Teacher(s) of "))
           {
               return false;
           }
           else
           {
               if (j != 0)
               {
                   TablerDBDataSet.GradesRow l_grw = p_grades.FindByGradeID(j);
                   if (l_grw != null)
                   {
                       classWhichHasProjectDay = " and " + l_grw.GradeName + " has project day";
                   }
               }
               MessageBox.Show(classteacherswhoForgot + "have not completed data entry of daily report status" + classWhichHasProjectDay);
               return true;
           }

           // return true;
        }

        private void btnTeacherAssess_Click_1(object sender, EventArgs e)
        {
            if (dateTimePicker1.Value.DayOfWeek == DayOfWeek.Sunday)
            {
                MessageBox.Show("Please select the date other than Sunday!");
                return;
            }
            fViewTimeTable l_fviewTT = new fViewTimeTable(p_grades, ROOMS, TSFinalTbl, Teachers, Subjects, PreAssign, AllocationTable, GradePreAssign, p_teachersFortheSubPreAssign);
            try
            {
                int l_gradeid = -1;
                if (comboBox1.Visible)
                {
                    l_gradeid = (int)comboBox1.SelectedValue;
                }
                l_fviewTT.FillTimeTableTblandBindWithGridViewForTeac(-1, p_global.TeacherIDSelected, Teachers.FindByTeachersID(p_global.TeacherIDSelected).TeachersName.ToString(),dateTimePicker1.Value.Date,l_gradeid);

                l_fviewTT.ShowDialog();

                dataGridView1.Rows.Clear();
                loadDataGridAttend();
            }
            catch (Exception l_e)
            {
                MessageBox.Show("Error showing time table, " + l_e.Message);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedValueChanged += new EventHandler(dateTimePicker1_ValueChanged);
            dateTimePicker1.ValueChanged += new EventHandler(dateTimePicker1_ValueChanged);

            dataGridView1.ClearSelection();
        }

        void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if (chkBoxSelGrade.Checked)
            {
                loadDataGrid();
            }
        }


        private void btnAllClasses_Click(object sender, EventArgs e)
        {
            forMultipleGrades((int)tGlobal.GRADE_CODES.ALL);
             
        }

        private void btnAllSE_Click(object sender, EventArgs e)
        {
            forMultipleGrades((int)tGlobal.GRADE_CODES.ALLSE);
        }

        private void btnAllTE_Click(object sender, EventArgs e)
        {
            forMultipleGrades((int)tGlobal.GRADE_CODES.ALLTE);
        }

        private void btnAllBE_Click(object sender, EventArgs e)
        {
            forMultipleGrades((int)tGlobal.GRADE_CODES.ALLBE);
        }

        void forMultipleGrades(int l_gradeid)
        {
            if (dateTimePicker1.Value.DayOfWeek.ToString() == "Sunday")
            {
                MessageBox.Show("Sunday Selected");
                return;
            }

            fViewTimeTable l_fviewTT = new fViewTimeTable(p_grades, ROOMS, TSFinalTbl, Teachers, Subjects, PreAssign, AllocationTable, GradePreAssign, p_teachersFortheSubPreAssign);
            try
            {
                l_fviewTT.SetGrade(l_gradeid, tGlobal.GRADE_NAME[Math.Abs(l_gradeid)], dateTimePicker1.Value);

                l_fviewTT.ShowDialog();
                loadDataGrid();
            }
            catch (Exception l_e)
            {
                MessageBox.Show("Error showing time table, " + l_e.Message);
            }
        }

        private void chkShowConducted_CheckedChanged(object sender, EventArgs e)
        {
            loadDataGrid();
        }

      
        private void button2_Click(object sender, EventArgs e)
        {
            ViewAttendanceForms d_entry = new ViewAttendanceForms(p_teacherName);
            d_entry.ShowDialog();
        }

         
      
    }
}
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        