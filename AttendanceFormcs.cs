using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;

namespace TeachersAssessment
{
   

    public partial class AttendanceFormcs : Form
    {
        int teacheridmain;
         string[] type = {"Lecture","Practical", "Elective"};
        int slotnum;
        DateTime l_dt;
        string timemain;
        TablerDBDataSet.TeachersDataTable Teachers = new TablerDBDataSet.TeachersDataTable();
        TablerDBDataSet.SubjectsDataTable Subjects = new TablerDBDataSet.SubjectsDataTable();
        TablerDBDataSet.AllocationTableDataTable AllocationTable = new TablerDBDataSet.AllocationTableDataTable();
        TablerDBDataSet.TSFInalDataTable TSFinalTbl = new TablerDBDataSet.TSFInalDataTable();
        TablerDBDataSet.GradesDataTable grades = new TablerDBDataSet.GradesDataTable();
        DataSet1.AttendanceDataTable Attend = new DataSet1.AttendanceDataTable();
        DataSet1.StudentsDataTable Students = new DataSet1.StudentsDataTable();
        System.Data.MPrSQL.MPrSQLDataAdapter SubDA = MyTableAdapters.SubjectsTableAdapter();
        System.Data.MPrSQL.MPrSQLDataAdapter TeacherDA = MyTableAdapters.TeachersTableAdapter();
        System.Data.MPrSQL.MPrSQLDataAdapter allocationDA = MyTableAdapters.AllocationTableTableAdapter();
        System.Data.MPrSQL.MPrSQLDataAdapter TsFinal = MyTableAdapters.TSFInalTableAdapter();
        System.Data.MPrSQL.MPrSQLDataAdapter AttendDA = MyTableAdapters.AttendanceTableAdapter();
        System.Data.MPrSQL.MPrSQLDataAdapter StudentDA = MyTableAdapters.StudentsTableAdapter();
        System.Data.MPrSQL.MPrSQLDataAdapter GradesDA = MyTableAdapters.GradesTableAdapter();
        int gradeidmain;
        int l_subject = -1;
        tGlobal p_global = tGlobal.GetInstance(-1);
        string p_batch_in_allocation = null;
        bool p_batch_in_lecture = false;
        NorthernLights.tGlobal p_glbl;
        DataSet1.AttendanceRow[] AttendRows;

        public AttendanceFormcs(int slot, DateTime l_d, string time, string gradename, int gradeid, int teacherid, string teachername)
        {
            InitializeComponent();
            p_glbl  = NorthernLights.tGlobal.GetInstance(-1);
            StudentDA.Fill(Students);
            SubDA.Fill(Subjects);
            TeacherDA.Fill(Teachers);
            allocationDA.Fill(AllocationTable);
            TsFinal.Fill(TSFinalTbl);
            AttendDA.Fill(Attend);
            GradesDA.Fill(grades);
            gradeidmain = gradeid;
            teacheridmain = teacherid;
            label1.Enabled = false;
            comboBoxBatch.Enabled = false;
            labelTime.Text = time;
            labelDate.Text = l_d.ToShortDateString();
            slotnum=slot;
            l_dt = l_d;
            timemain = time;
            comBoxGrade.DataSource = grades;
            comBoxGrade.DisplayMember = grades.GradeNameColumn.ColumnName;
            comBoxGrade.ValueMember = grades.GradeIDColumn.ColumnName;
            TablerDBDataSet.AllocationTableRow[] l_allocrows = (TablerDBDataSet.AllocationTableRow[])AllocationTable.Select("(" + AllocationTable.TeacherIDColumn.ColumnName + " = " + p_global.TeacherIDSelected
                                                                                                             + " OR " + TSFinalTbl.TeachersStrColumn.ColumnName + " like '%" + teacherid.ToString() + ",%')" 
                                                                                                          + " AND " + AllocationTable.SlotAllotedColumn.ColumnName + " = " + slot);
            makeGrid();
            initiateLectureSubjectComboBox(teacherid, gradeid);
            string[] batches = null;
            if (l_allocrows.Length > 0)
            {
                foreach (TablerDBDataSet.AllocationTableRow l_alrw in l_allocrows)
                {
                    if (l_alrw.TeacherID == p_global.TeacherIDSelected || (l_alrw.IsTeachersStrNull() == false && (l_alrw.TeachersStr.StartsWith(p_global.TeacherIDSelected.ToString() + ",")
                                                                                        || l_alrw.TeachersStr.Contains("," + p_global.TeacherIDSelected.ToString() + ","))))
                    {
                        int l_subjID = l_alrw.SUBJID;
                        TablerDBDataSet.TSFInalRow l_tsfinalRw = TSFinalTbl.FindByID(l_alrw.TSFinalID);
                        string[] l_subjStr = null;
                        if (l_tsfinalRw != null && l_tsfinalRw.IsSubjRoomStrNull() == false)                        
                        {
                            l_subjStr = l_tsfinalRw.SubjRoomStr.Split(',');
                        }
                        string[] tchrs = l_alrw.TeachersStr.Split(',');
                        int positionOfTchr = 0;
                        foreach (string l_str in tchrs)
                        {
                            if (l_str.Equals(p_global.TeacherIDSelected.ToString()))
                            {
                                //found
                                break;
                            }
                            positionOfTchr++;
                        }
                        if (l_alrw.IsBatchesNull() == false)
                        {                            
                            batches = l_alrw.Batches.Split(',');

                            if (positionOfTchr < batches.Length)
                            {
                                int i = batches[positionOfTchr].IndexOf(":");
                                p_batch_in_allocation = batches[positionOfTchr].Substring(0, i);
                                
                                if (batches[positionOfTchr].EndsWith(":L"))
                                {
                                    p_batch_in_lecture = true;
                                }
                            }
                        }
                           
                        if (l_subjStr != null && positionOfTchr < l_subjStr.Length)
                        {
                            int i = l_subjStr[positionOfTchr].IndexOf(":");
                            string l_tmpstr = l_subjStr[positionOfTchr].Substring(0, i);
                            int.TryParse(l_tmpstr, out l_subjID);
                        }
                        
                        
                        gradeidmain = l_alrw.GRDID;
                        l_subject = l_subjID;
                        comboBoxSubj.SelectedValue = l_subjID;
                        gradeid = gradeidmain;
                        gradename = grades.FindByGradeID(gradeidmain).GradeName;
                        comBoxGrade.SelectedValue = gradeidmain;
                        comboBoxBatch.DataSource = p_glbl.BatchesList(gradename) ;
                        comboBoxSubj.SelectedValue = l_subjID;
                        break;
                    }
                }
                
            }
            else
            {
                comBoxGrade.SelectedIndex = -1;
            }

            /*if (batches == null)
            {
                /*string l_batch = "";
                if (gradename.EndsWith("-A"))
                {
                    l_batch = "A";
                } 
                else if (gradename.EndsWith("-B"))
                {
                    l_batch = "B";
                }
                string[] batch = { l_batch + "1", l_batch + "2", l_batch + "3", l_batch + "4" };
                comboBoxBatch.DataSource = batch;
               
                
            }*/
            if (gradename != null && gradename.Equals("NA"))
            {
                comboBoxBatch.DataSource = p_glbl.BatchesList(gradename);
            }
            comboBoxLectPract.DataSource = type;
            
            string select = null;
            
            select = Attend.SlotIDColumn.ColumnName + " = " + slot 
                + " AND " + Attend.TeacherIDColumn.ColumnName + " = " + teacherid;

            AttendRows = (DataSet1.AttendanceRow[])Attend.Select(select);

           
            if (AttendRows.Length > 0)
            {
                //comboBoxSubj.Enabled = comBoxGrade.Enabled = comboBoxLectPract.Enabled = comboBoxBatch.Enabled = false;
                dataGridView1.Rows.Clear();
                dataGridView1.Refresh();
                gradeidmain = AttendRows[0].GradeID;
               
                comboBoxSubj.Text = AttendRows[0].SubjectName;
                gradeid = gradeidmain;
                gradename = grades.FindByGradeID(gradeidmain).GradeName;
                
                comBoxGrade.SelectedValue = gradeidmain;
                comboBoxBatch.DataSource = p_glbl.BatchesList(gradename);
                if (AttendRows[0].PracticalorNOT)
                {
                    timemain = getTimeIfPRacticals(true, false);
                    comboBoxLectPract.SelectedIndex = 1;
                    
                    string batchhere = Students.FindBySAPID(AttendRows[0].SAPID).Batch;
                    string batchspecific = batchhere;
                    if (batchspecific.Length > 1)
                    {
                        batchspecific = batchhere.Substring(1, 1);
                    }
                    comboBoxBatch.SelectedIndex = Convert.ToInt32(batchspecific) - 1;

                    initiateGridPract();
                    // comboBox3.SelectedValue = attendRows[0].SubjectName;
                }
                else
                {
                    timemain = getTimeIfPRacticals(false, true);
                    comboBoxLectPract.SelectedIndex = 0;
                    label1.Enabled = false;
                    comboBoxBatch.Enabled = false;
                    initiateGridLecture();
                    
                }
                comboBoxBatch.Enabled = false;
                comboBoxLectPract.Enabled = false;
                //comboBoxSubj.Enabled = false;
            }
            else
            {
                timemain = getTimeIfPRacticals(false, false);
                if (comboBoxLectPract.SelectedValue.Equals("Lecture"))
                {
                    label1.Enabled = false;
                    comboBoxBatch.Enabled = false;
                    dataGridView1.Rows.Clear();
                    dataGridView1.Refresh();
                   
                   //initiateLectureSubjectComboBox(teacherid, gradeid);
                }
                else
                {
                    comboBoxBatch.Enabled = true;
                    
                    dataGridView1.Rows.Clear();
                    dataGridView1.Refresh();
                  
                    //initiateLectureSubjectComboBox(teacherid, gradeid);
                    //comboBoxSubj.SelectedIndex = 0;
                }
                if (p_batch_in_allocation != null)
                {
                    comboBoxBatch.SelectedIndex = -1;
                    int i = 0;
                    foreach (string l_str in comboBoxBatch.Items)
                    {
                        if (l_str.StartsWith(p_batch_in_allocation))
                        {
                            comboBoxBatch.SelectedIndex = i;
                            //comboBoxBatch.Enabled = false;
                            //initiateGridPract();
                            break;
                        }
                        i++;
                    }
                    if (comboBoxBatch.SelectedIndex == -1)
                    {
                        MessageBox.Show("Warning, Batch mismatch, please contact administrator");
                    }
                }
                if (comboBoxBatch.Enabled == false)
                {
                    initiateGridLecture();
                }
                else
                {
                    initiateGridPract();
                }

            }
            this.Load += new EventHandler(AttendanceFormcs_Load);
        }

        void AttendanceFormcs_Load(object sender, EventArgs e)
        {
            dataGridView1.ClearSelection();
            this.comboBoxLectPract.SelectedValueChanged += new System.EventHandler(this.comboBoxLectPract_SelectedValueChanged);
            this.comBoxGrade.SelectedValueChanged += new EventHandler(comboBoxGrad_SelectedValueChanged);
            this.comboBoxBatch.SelectedValueChanged +=new EventHandler(comboBoxBatch_SelectedValueChanged);
            this.comboBoxSubj.SelectedValueChanged += new EventHandler(comboBoxSubj_SelectedValueChanged);
        }

        void comboBoxSubj_SelectedValueChanged(object sender, EventArgs e)
        {
            if (comboBoxLectPract.Text.StartsWith("Ele"))
            {
                dataGridView1.Rows.Clear();
                dataGridView1.Refresh();
                initiateGridLecture();
            }
        }
        
         string getTimeIfPRacticals(bool l_isForcedPracticals, bool l_isForcedLecture)
         {
            int slot = slotnum;
            string time = timemain;

            vIEWDataSet1.TimeTableViewDataTable l_ttv_table = p_global.GetViewTable();
            int rowindex = slot % p_global.NumOfSlotsInADay;
            if (rowindex == 0)
            {
                rowindex = 11;
            }
            else
            {
                rowindex = rowindex - 1;
            }
            string time1 = null; int l_index = -1;
            int l_incrFortime = 1;
            if (l_isForcedPracticals)
            {
                l_incrFortime = 3;
            }
            if (l_isForcedPracticals || l_isForcedLecture)
            {
                time1 = l_ttv_table[rowindex + l_incrFortime].LectureHr;
                l_index = time1.IndexOf("to");
                time1 = time1.Substring(l_index + 2, time1.Length - l_index - 2);

                l_index = time.IndexOf("to");
                time = time.Substring(0, l_index);
                time = time.Trim() + " to " + time1.Trim();
                timemain = time;
                goto EXIT;
            }
            /*else if (l_isForcedLecture)
            {
                time = l_ttv_table[rowindex].LectureHr + l_ttv_table[rowindex+1].LectureHr;
                goto EXIT;
            }*/

            TablerDBDataSet.AllocationTableRow[] l_allocrow = (TablerDBDataSet.AllocationTableRow[])AllocationTable.Select(AllocationTable.GRDIDColumn.ColumnName + " = " + gradeidmain + " AND "
               + AllocationTable.SlotAllotedColumn.ColumnName + " = " + slot);
           
            if (l_allocrow.Length > 0)
            {
                int l_incr = (l_allocrow[0].Duration / p_global.lect_min) - 1;
                //if (l_allocrow[0].Duration == 120)
                {
                    if (l_allocrow[0].Duration == 120)
                    {
                        if (p_batch_in_lecture)
                        {
                            comboBoxLectPract.SelectedIndex = 0;
                            l_incr = 1; //one lecture only
                        }
                        else
                        {
                            comboBoxLectPract.SelectedIndex = 1;
                        }
                    }
                    else
                    {
                        comboBoxLectPract.SelectedIndex = 0;
                    }
                    TablerDBDataSet.AllocationTableRow[] l_allocrowOther = (TablerDBDataSet.AllocationTableRow[])AllocationTable.Select(AllocationTable.GRDIDColumn.ColumnName + " = " + gradeidmain + " AND "
                                                + AllocationTable.SlotAllotedColumn.ColumnName + " = " + (slot +  l_incr).ToString());
                    if (l_allocrowOther.Length > 0 && l_allocrowOther[0].TSFinalID == l_allocrow[0].TSFinalID)
                    {
                        time1 = l_ttv_table[rowindex + l_incr].LectureHr;
                        l_index = time1.IndexOf("to");
                        time1 = time1.Substring(l_index + 2, time1.Length - l_index - 2);

                        l_index = time.IndexOf("to");
                        time = time.Substring(0, l_index);
                        time = time + " to " + time1;
                    }
                    /*else
                    {
                        l_allocrowOther = (TablerDBDataSet.AllocationTableRow[])AllocationTable.Select(AllocationTable.GRDIDColumn.ColumnName + " = " + gradeidmain + " AND "
                                                + AllocationTable.SlotAllotedColumn.ColumnName + " = " + (slot - 1).ToString());
                        if (l_allocrowOther.Length > 0 && l_allocrowOther[0].TSFinalID == l_allocrow[0].TSFinalID)
                        {
                            time1 = l_ttv_table[rowindex - 1].LectureHr;
                            l_index = time1.IndexOf("to");
                            time1 = time1.Substring(0, l_index);

                            l_index = time.IndexOf("to");
                            time = time.Substring(l_index + 2, time.Length - l_index - 2);
                            time = time1 + " to " + time;

                            slot = slot - 1;
                        }
                    }*/
                }
            }
            EXIT:
            slotnum = slot;
            timemain = time;
            labelTime.Text = time;
            return time;                
                    
        }

         void makeGrid()
         {
             DataGridViewCheckBoxColumn dgvCB = new DataGridViewCheckBoxColumn();

             dataGridView1.Columns.Add("SAP", "SAPID");
             dataGridView1.Columns.Add("NAME", "FULL NAME");

             //dataGridView1.Columns.Add("Present", "PRESENT");
             dataGridView1.Columns.Add(dgvCB);
             foreach (DataGridViewColumn col in dataGridView1.Columns)
             {
                 col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                 col.HeaderCell.Style.Font = new Font("Bookman Old Style", 12F, FontStyle.Bold, GraphicsUnit.Pixel);
             }
             dataGridView1.AllowUserToDeleteRows = false;
             dataGridView1.AllowUserToAddRows = false;
             dataGridView1.AllowUserToOrderColumns = false;
             dataGridView1.AllowUserToResizeColumns = false;
             // dataGridView1.Columns[0].HeaderCell.Style.Font = ;
             dataGridView1.Columns[0].ReadOnly = true;
             dataGridView1.Columns[1].ReadOnly = true;
             dataGridView1.Columns[2].HeaderText = "PRESENT";
             dataGridView1.Columns[0].Width = 100;
             dataGridView1.Columns[2].Width = 75;
             dataGridView1.Columns[2].ReadOnly = false;

             dataGridView1.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.Transparent;
             dataGridView1.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.DarkSlateBlue;

             dataGridView1.CellContentClick += new DataGridViewCellEventHandler(dataGridView1_CellContentClick);
             dataGridView1.CellFormatting += new DataGridViewCellFormattingEventHandler(dataGridView1_CellFormatting);
             dataGridView1.CellEndEdit += new DataGridViewCellEventHandler(dataGridView1_CellEndEdit);

         }

         void reinitBatchComboBox()
         {
             //comboBoxBatch.SelectedValueChanged -= new EventHandler(comboBoxBatch_SelectedValueChanged);
             /*string gradename = grades.FindByGradeID(gradeidmain).GradeName;
             string[] batch = { gradename + "1", gradename + "2", gradename + "3", gradename + "4" };
             comboBoxBatch.DataSource = batch;
             
             if (comboBoxBatch.Items.Count == 0)
             {
                 string l_batch = "A";
                 if (gradename.EndsWith("-B"))
                 {
                     l_batch = "B";
                 }
                 string[] batch = { l_batch + "1", l_batch + "2", l_batch + "3", l_batch + "4" };
                 comboBoxBatch.DataSource = batch;
             }*/
             comboBoxBatch.DataSource = p_glbl.BatchesList(comBoxGrade.Text);
             if (comboBoxLectPract.SelectedValue.Equals("Lecture"))
             {
                 comboBoxBatch.Enabled = false;
             }
             else
             {
                 comboBoxBatch.Enabled = true;
             }
             //comboBoxBatch.SelectedValueChanged += new EventHandler(comboBoxBatch_SelectedValueChanged);

         }

        void initiateGridPract()
        {  
            label1.Enabled = true;
            string batchname = comboBoxBatch.SelectedValue.ToString();

            if (AttendRows.Length > 0)
            {
                comBoxGrade.Enabled = comboBoxLectPract.Enabled = comboBoxBatch.Enabled = false;
                foreach (DataSet1.AttendanceRow attendRow in AttendRows)
                {
                    dataGridView1.Rows.Add(attendRow.SAPID, Students.FindBySAPID(attendRow.SAPID).FullName, attendRow.AbsORPres);
                }
            }
            else
            {
                DataSet1.StudentsRow[] row_Students = (DataSet1.StudentsRow[])Students.Select(Students.GradeIDColumn.ColumnName + " = " + gradeidmain + " AND " +
                                         Students.BatchColumn.ColumnName + " = '" + batchname + "' ");
                foreach (DataSet1.StudentsRow row_Student in row_Students)
                {

                    string l_middleName = null;
                    if (row_Student.IsMiddleNameNull() == false)
                    {
                        l_middleName = row_Student.MiddleName + " ";
                    }

                    //dataGridView1.Rows.Add(row_Student.SAPID, row_Student.FirstName + " " + l_middleName + row_Student.LastName, true);
                    dataGridView1.Rows.Add(row_Student.SAPID, row_Student.FullName, true);
                }
            }

        }

        void initiateGridLecture()
        {
            if (gradeidmain < 0) return;
            string gradename = grades.FindByGradeID(gradeidmain).GradeName;
           
            if (AttendRows.Length > 0)
            {
                comBoxGrade.Enabled = comboBoxLectPract.Enabled = comboBoxBatch.Enabled = false;
                foreach (DataSet1.AttendanceRow attendRow in AttendRows)
                {
                    dataGridView1.Rows.Add(attendRow.SAPID, Students.FindBySAPID(attendRow.SAPID).FullName, attendRow.AbsORPres);
                }
            }
            else 
            {
                DataSet1.StudentsRow[] row_Students = (DataSet1.StudentsRow[])Students.Select(Students.GradeIDColumn.ColumnName + " = " + gradeidmain);
                foreach (DataSet1.StudentsRow row_Student in row_Students)
                {
                    if (comboBoxLectPract.Text.StartsWith("Ele") && row_Student.IsElectiveNull() == false)
                    {
                        if (row_Student.Elective.Equals(comboBoxSubj.Text) == false)
                        {
                            continue;
                        }
                    }
                        
                    string l_middleName = null;
                    if (row_Student.IsMiddleNameNull() == false)
                    {
                        l_middleName = row_Student.MiddleName + " ";
                    }

                    //dataGridView1.Rows.Add(row_Student.SAPID, row_Student.FirstName + " " + l_middleName + row_Student.LastName, true);
                    dataGridView1.Rows.Add(row_Student.SAPID, row_Student.FullName, true);
                }
            }

        }

        void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            this.BeginInvoke(new EventHandler(refrsh));
        }

        void initiateLectureSubjectComboBox(int teacherid, int gradeid)
        {
            if (comboBoxSubj.DataSource == null)
            {
                /*TablerDBDataSet.TSFInalRow[] l_tsfinal = (TablerDBDataSet.TSFInalRow[])TSFinalTbl.Select(TSFinalTbl.TeachersIDColumn.ColumnName + " = " + teacherid
                                                                                     + " OR " + TSFinalTbl.TeachersStrColumn.ColumnName + " like '%" + teacherid.ToString() + ",%'");
                    //+ " AND " + TSFinalTbl.GradesIDColumn.ColumnName + " = " + gradeid + " AND " + TSFinalTbl.DurationColumn.ColumnName + " = " + 60);
                foreach (TablerDBDataSet.TSFInalRow l_tsfinalrow in l_tsfinal)
                {
                    if (l_tsfinalrow.TeachersStr.StartsWith(teacherid.ToString() + ",") || l_tsfinalrow.TeachersStr.Contains("," + teacherid.ToString() + ","))
                    {
                        comboBox3.Items.Add(Subjects.FindBySubjectID(l_tsfinalrow.SubjectsID).SubjectName);
                    }
                }*/
                DataView l_dv = new DataView(Subjects);
                l_dv.RowFilter = Subjects.SubjectNameColumn.ColumnName + " NOT LIKE 'Practicals*'";
                comboBoxSubj.DataSource = l_dv;
                
                comboBoxSubj.DisplayMember = Subjects.SubjectNameColumn.ColumnName;
                comboBoxSubj.ValueMember = Subjects.SubjectIDColumn.ColumnName;

            }

            if (comboBoxSubj.Items.Count > 0)
            {
                comboBoxSubj.SelectedValue = l_subject;
            }
        }
        

        void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.EndEdit();
            //MessageBox.Show(e.ColumnIndex.ToString() + ":" + e.RowIndex.ToString() + " " + dataGridView1[e.ColumnIndex, e.RowIndex].Value.ToString());
            
        }

        void refrsh(object sender, EventArgs e)
        {
            DataGridViewCell l_cell = dataGridView1.CurrentCell;
            dataGridView1.ClearSelection();
            //l_cell.Selected = true;

        }

        void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridViewCell l_cell = dataGridView1[2, e.RowIndex];
            /*if (l_changed && e.RowIndex == dataGridView1.CurrentCell.RowIndex)
            {
                MessageBox.Show(e.RowIndex.ToString() + " " + l_cell.Value);
                l_changed = false;
            }*/
            if (l_cell.Value.ToString().Equals("False"))
            {
                dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = System.Drawing.Color.IndianRed;
                dataGridView1.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = System.Drawing.Color.IndianRed;
            }
            else
            {
                dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = System.Drawing.Color.White;
                dataGridView1.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = System.Drawing.Color.Transparent;
            }
         
        }


        private void btnMarkAll_Click(object sender, EventArgs e)
        {
            if (btnMarkAll.Text.Contains("Absent"))
            {
                btnMarkAll.Text = "Mark All Present";
                btnMarkAll.ForeColor = System.Drawing.Color.Green;
                markAll(false);
            }
            else
            {
                btnMarkAll.Text = "Mark All Absent";
                btnMarkAll.ForeColor = System.Drawing.Color.Red;
                markAll(true);
            }
        }

        void markAll(bool l_presentAbsent)
        {
            foreach (DataGridViewRow l_row in dataGridView1.Rows)
            {
                l_row.Cells[2].Value = l_presentAbsent;
            }
        }

        private void comboBoxLectPract_SelectedValueChanged(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
            if (comboBoxLectPract.SelectedValue.Equals("Lecture") || comboBoxLectPract.SelectedValue.ToString().StartsWith("Elec"))
            {
                comboBoxBatch.Enabled = false;
                initiateGridLecture();
                //initiateLectureSubjectComboBox(teacheridmain, gradeidmain);
                getTimeIfPRacticals(false, true);
            }
            else
            {
                comboBoxBatch.Enabled = true;
                initiateGridPract();
                getTimeIfPRacticals(true, false);
            }
            
        }

        void resetBatchTOp_batch(object sender, EventArgs e)
        {
            comboBoxBatch.Text = p_batch_in_allocation;
        }

        private void comboBoxBatch_SelectedValueChanged(object sender, EventArgs e)
        {
            if (comboBoxBatch.Enabled)
            {
                if (comboBoxBatch.Text != null && p_batch_in_allocation != null)
                {
                    if (comboBoxBatch.Text.EndsWith(p_batch_in_allocation) == false)
                    {
                        MessageBox.Show("Warning!, Batch selected is not your batch.");
                        this.BeginInvoke(new EventHandler(resetBatchTOp_batch));
                        return;
                    }
                }
                dataGridView1.Rows.Clear();
                dataGridView1.Refresh();
                initiateGridPract();
            }
        }

        private void SAVE_Click_1(object sender, EventArgs e)
        {
            if (comboBoxBatch.Enabled && comboBoxBatch.SelectedValue == null)
            {
                MessageBox.Show("Please select batch for practicals");
                return;
            }
            if (comboBoxLectPract.SelectedValue == null || comBoxGrade.SelectedValue == null
                        || comboBoxSubj.SelectedValue == null)
            {
                MessageBox.Show("Please select appropriate grade, subject and type");
                return;
            }
            if (comboBoxSubj.Text.StartsWith("Practi"))
            {
                MessageBox.Show("Please select subject of the practicals, this is required for weekly/monthly reports");
                return;
            }
            progressBar1.Step = 1;
            progressBar1.Minimum = 0;
            progressBar1.Maximum = dataGridView1.Rows.Count;
            Cursor.Current = Cursors.WaitCursor;
            foreach (DataGridViewRow dgvRow in dataGridView1.Rows)
            {
                DataSet1.AttendanceRow l_strw = Attend.FindByDateSAPIDGradeIDSlotID(l_dt, Convert.ToUInt64(dgvRow.Cells["SAP"].Value), gradeidmain, slotnum);//array[1]);
                if (l_strw == null)
                {
                    l_strw = Attend.NewAttendanceRow();
                    l_strw.SAPID = Convert.ToUInt64(dgvRow.Cells["SAP"].Value);
                    l_strw.Date = l_dt;
                    l_strw.GradeID = gradeidmain;
                    l_strw.SlotID = slotnum;
                    l_strw.TeacherID = teacheridmain;
                    if (comboBoxLectPract.SelectedValue.Equals("Practical"))
                    {
                        l_strw.PracticalorNOT = true;
                        l_strw.GradeNAME = grades.FindByGradeID(gradeidmain).GradeName + " ("+ comboBoxBatch.Text + ")";
                    }
                    else
                    {
                        l_strw.PracticalorNOT = false;
                        l_strw.GradeNAME = grades.FindByGradeID(gradeidmain).GradeName;
                    }
                    //l_strw.GradeNAME = grades.FindByGradeID(gradeidmain).GradeName;
                    l_strw.Time = timemain;

                    if (comboBoxSubj.Items.Count == 0)
                    {
                        l_strw.SubjectName = " ";
                    }
                    else
                    {
                        l_strw.SubjectName = comboBoxSubj.Text;
                    }
                }
                /*else
                {
                    MessageBox.Show(l_strw.SAPID.ToString());
                }*/
                if (comboBoxSubj.Items.Count == 0)
                {
                    l_strw.SubjectName = " ";
                }
                else
                {
                    l_strw.SubjectName = comboBoxSubj.Text;
                }

                bool l_absPre = Convert.ToBoolean(dgvRow.Cells[2].Value);
                if (l_strw.IsAbsORPresNull() || l_strw.AbsORPres != l_absPre)
                {
                    l_strw.AbsORPres = l_absPre;
                }
                if (l_strw.RowState == DataRowState.Detached)
                {
                    Attend.AddAttendanceRow(l_strw);
                }
                progressBar1.PerformStep();
            }
            MyTableAdapters.BeginTransDataSet();
            int l_uptCount = AttendDA.Update(Attend);
            MyTableAdapters.EndTransactionDataSet();
            Cursor.Current = Cursors.Default;
            MessageBox.Show("Number of records saved/altered : " + l_uptCount.ToString());
            this.Close();
            //StudentDA.Update(Students);
            }

        private void Delete_Click(object sender, EventArgs e)
        {
            
            Cursor.Current = Cursors.WaitCursor;
            if (AttendRows.Length == 0)
            {
                MessageBox.Show("Nothing to Delete, Record does not exist, you haven't saved it yet.");
            }
            else
            {
                progressBar1.Step = 1;
                progressBar1.Minimum = 0;
                progressBar1.Maximum = dataGridView1.Rows.Count;
               
                foreach (DataSet1.AttendanceRow l_strw in AttendRows)
                {
                  
                    l_strw.Delete();
                   
                    progressBar1.PerformStep();
                }
                MyTableAdapters.BeginTransDataSet();
                AttendDA.Update(Attend);
                MyTableAdapters.EndTransactionDataSet();
                Cursor.Current = Cursors.Default;
                this.Close();
            }
                    
                    
        }

        private void comboBoxGrad_SelectedValueChanged(object sender, EventArgs e)
        {
            if (comboBoxLectPract.SelectedValue != null && comBoxGrade.SelectedValue != null)
            {
                dataGridView1.Rows.Clear();
                dataGridView1.Refresh();
                gradeidmain = (int)comBoxGrade.SelectedValue;

                //after changing grade
                reinitBatchComboBox();
                
                if (comboBoxLectPract.SelectedValue.Equals("Lecture") || comboBoxLectPract.Text.StartsWith("Ele"))
                {
                    comboBoxBatch.Enabled = false;
                    initiateGridLecture();
                    //initiateLectureSubjectComboBox(teacheridmain, gradeidmain);
                    getTimeIfPRacticals(false, true);
                }
                else
                {
                    comboBoxBatch.Enabled = true;
                    initiateGridPract();
                    getTimeIfPRacticals(true, false);
                }
            }
        }

        
    
        

    }
}
