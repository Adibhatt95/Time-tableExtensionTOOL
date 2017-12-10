using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;

namespace TeachersAssessment
{

  /*  public struct NAME_INT_PAIR
    {
        public string ANYNAME;
        public int ANYID;
    };*/
    
    public partial class fEnterDailyReportData : Form
    {
        public TablerDBDataSet.TeachersDataTable Teachers = new TablerDBDataSet.TeachersDataTable();
        public TablerDBDataSet.AllocationTableDataTable AllocationTable = new TablerDBDataSet.AllocationTableDataTable();
        public TablerDBDataSet.SubjectsDataTable SubjectTable = new TablerDBDataSet.SubjectsDataTable();
        public TablerDBDataSet.TSFInalDataTable TsFinalTable = new TablerDBDataSet.TSFInalDataTable();
        string[] p_reasons = { "Absence of Teacher", "Swapped", "No Students", "Swapped and No Students", "Miscellaneous", "Official Calendar Holiday" };
        DataSet1.AssessmentDataTable AssessmentTable = new DataSet1.AssessmentDataTable();
        
        System.Data.MPrSQL.MPrSQLDataAdapter AssessDA = MyTableAdapters.AssessTableAdapter();
        System.Data.MPrSQL.MPrSQLDataAdapter SubjectDA = MyTableAdapters.SubjectsTableAdapter();
        System.Data.MPrSQL.MPrSQLDataAdapter TsfinalDA = MyTableAdapters.TSFInalTableAdapter();
        int[] p_teachers;
        int slotnum;
        DateTime l_d;
        int p_gradeid1;
        TablerDBDataSet.AllocationTableRow[] l_allocrow;
        DataSet1.AssessmentRow[] AssessRws;
        bool p_isPractical = false;
        tGlobal p_global;

        public fEnterDailyReportData(int slot, DateTime l_dt, string Grade, string time, int gradeid, TablerDBDataSet.AllocationTableDataTable l_alloc,TablerDBDataSet.TeachersDataTable l_teachers)
        {
            InitializeComponent();
            p_global = tGlobal.GetInstance(-1);
            p_global.Set_TimeTableglobal();
            AssessDA.Fill(AssessmentTable);
            TsfinalDA.Fill(TsFinalTable);
            cbReasons.DataSource = p_reasons;
            lblDate.Text = l_dt.Date.ToString().Substring(0,11);
            lblGradeName.Text = Grade;
            SubjectDA.Fill(SubjectTable);
            p_gradeid1 = gradeid;
            l_d = l_dt;
            AllocationTable = l_alloc;
            Teachers=l_teachers;
            checkBox1.Visible = false;
           
            if (gradeid > 0)
            {
                cbTeacherSwapped.DataSource = Teachers;
                cbTeacherSwapped.DisplayMember = Teachers.TeachersNameColumn.ColumnName;
                cbTeacherSwapped.ValueMember = Teachers.TeachersIDColumn.ColumnName;
                cbTeacherSwapped.AutoCompleteMode = AutoCompleteMode.Suggest;
                cbTeacherSwapped.AutoCompleteSource = AutoCompleteSource.ListItems;

                cbBoxSubject.DataSource = SubjectTable;
                cbBoxSubject.DisplayMember = SubjectTable.SubjectNameColumn.ColumnName;
                cbBoxSubject.ValueMember = SubjectTable.SubjectIDColumn.ColumnName;
                cbBoxSubject.AutoCompleteMode = AutoCompleteMode.Suggest;
                cbBoxSubject.AutoCompleteSource = AutoCompleteSource.ListItems;
            }
            else
            {
                cbTeachers.Text = "All Teachers";
                cbBoxSubject.Enabled = false;
                cbBoxSubject.SelectedIndex = -1;
            }            
            

            lblswap.Visible = false;
            cbTeacherSwapped.Visible = false;
            l_allocrow = (TablerDBDataSet.AllocationTableRow[])AllocationTable.Select(AllocationTable.GRDIDColumn.ColumnName + " = " + gradeid + " AND "
                + AllocationTable.SlotAllotedColumn.ColumnName + " = " + slot);
            

            if (l_allocrow.Length > 0)
            {
                if (l_allocrow[0].Duration > p_global.lect_min)
                {
                    string time1 = null; int l_index = -1;

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

                    if (l_allocrow[0].Duration == 120)
                    {
                        p_isPractical = true;
                    }
                    int l_incr = (l_allocrow[0].Duration / p_global.lect_min) - 1;
                    TablerDBDataSet.AllocationTableRow[] l_allocrowOther = (TablerDBDataSet.AllocationTableRow[])AllocationTable.Select(AllocationTable.GRDIDColumn.ColumnName + " = " + gradeid + " AND "
                                                + AllocationTable.SlotAllotedColumn.ColumnName + " = " + (slot+l_incr).ToString());
                    if (l_allocrowOther.Length > 0 && l_allocrowOther[0].TSFinalID == l_allocrow[0].TSFinalID)
                    {
                        time1 = l_ttv_table[rowindex+l_incr].LectureHr;
                        l_index = time1.IndexOf("to");
                        time1 = time1.Substring(l_index + 2, time1.Length - l_index - 2);

                        l_index = time.IndexOf("to");
                        time = time.Substring(0, l_index);
                        time = time + " to " + time1;
                    }
                    else
                    {
                        l_allocrowOther = (TablerDBDataSet.AllocationTableRow[])AllocationTable.Select(AllocationTable.GRDIDColumn.ColumnName + " = " + gradeid + " AND "
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
                    }
                    
                }

                if (p_isPractical)
                {
                    if (l_allocrow[0].IsBatchesNull() == false && l_allocrow[0].Batches.Trim().Length > 0)
                    {
                        if (l_allocrow[0].Batches.Contains(":L"))
                        {
                            p_isPractical = false;
                        }
                    }
                }

                if (l_allocrow[0].IsTeachersStrNull() || l_allocrow[0].TeachersStr.Trim().Length == 0)
                {
                    TablerDBDataSet.SubjectsRow l_subjrw = SubjectTable.FindBySubjectID(l_allocrow[0].SUBJID);
                    if (l_subjrw != null)
                    {
                        cbBoxSubject.SelectedValue = l_subjrw.SubjectID;
                        if (l_subjrw.SubjectName.ToLower().Contains("math") && l_subjrw.SubjectName.ToLower().Contains("tut"))
                        {
                            p_isPractical = true;
                        }
                    }
                }
                else
                {
                    cbBoxSubject.SelectedIndex = -1;
                }


                slotnum = slot; //for practicals this is the first slot number always in datgabase first slot num is used
                
                DataSet1.AssessmentRow l_AssessRw = null;
                if (l_allocrow[0].IsTeachersStrNull() || l_allocrow[0].TeachersStr.Length == 0)
                {
                    p_teachers = new int[1];
                    p_teachers[0] = l_allocrow[0].TeacherID;
                    cbTeachers.Items.Add(Teachers.FindByTeachersID(l_allocrow[0].TeacherID).TeachersName);
                    cbTeachers.SelectedIndex = 0;
                    //comboBox3.Enabled = false;
                    l_AssessRw = GetAssessmentRow(l_d, slot, l_allocrow[0].TeacherID);

                    if (l_AssessRw != null)
                    {
                        if (! l_AssessRw.IsReasonNull()) cbReasons.SelectedItem = l_AssessRw.Reason;
                        if (! l_AssessRw.IsRemarkNull()) tbRemarks.Text = l_AssessRw.Remark;
                        if (!l_AssessRw.IsSubjectNull()) cbBoxSubject.Text = l_AssessRw.Subject;
                        if (l_AssessRw.IsTeacherIDSwappedNull() == false)
                        {
                            cbTeacherSwapped.SelectedValue = l_AssessRw.TeacherIDSwapped;
                            cbTeacherSwapped.Visible = true;
                            checkBox1.Visible = true;
                            lblswap.Visible = true;
                        }
                        else
                        {
                            cbTeacherSwapped.Visible = false;
                            lblswap.Visible = false;

                        }
                        if (! l_AssessRw.IsAdjustedNotconductedNull()) checkBox1.Checked = l_AssessRw.AdjustedNotconducted;
                    }
                }
                else
                {
                    string[] l_tchrsStrs = l_allocrow[0].TeachersStr.Split(',');
                    //fill up array of structure NAME_INT_PAIR here
                    p_teachers = new int[l_tchrsStrs.Length+1];
                    int l_i = 0;
                    cbTeachers.Items.Add("All Teachers");
                    p_teachers[l_i++] = -1000; 
                    foreach(string trchrid in l_tchrsStrs)
                    {
                        int l_tid = 0;
                        int.TryParse(trchrid, out l_tid);
                        if (l_tid == 0) continue;
                       // NAME_INT_PAIR l_nid = new NAME_INT_PAIR();
                       cbTeachers.Items.Add(Teachers.FindByTeachersID(l_tid).TeachersName);
                      
                            p_teachers[l_i++] = l_tid;
                    }

                     DataSet1.AssessmentRow[] l_assrws = GetAssessmentRows(l_d, slot,p_gradeid1);
                     if (l_assrws.Length == l_tchrsStrs.Length - 1)
                     {
                         l_AssessRw = l_assrws[0];
                         if (!l_AssessRw.IsReasonNull()) cbReasons.SelectedItem = l_AssessRw.Reason;
                         if (!l_AssessRw.IsReasonNull()) tbRemarks.Text = l_AssessRw.Remark;
                         if (!l_AssessRw.IsSubjectNull()) cbBoxSubject.Text = l_AssessRw.Subject;
                         cbTeacherSwapped.Visible = false;
                         lblswap.Visible = false;
                         cbTeachers.SelectedIndex = 0;
                     }
                     else
                     {
                         cbTeachers.SelectedIndex = -1;
                     }

                    /*l_AssessRw = GetAssessmentRow(l_d, slot, -1000);

                    if (l_AssessRw != null)
                    {
                        if (l_AssessRw.TeacherID == -1000)
                        {
                            cbTeachers.SelectedIndex = 0;
                        }
                        if (! l_AssessRw.IsReasonNull()) cbReasons.SelectedItem = l_AssessRw.Reason;
                        if (! l_AssessRw.IsReasonNull()) tbRemarks.Text = l_AssessRw.Remark;
                        cbTeacherSwapped.Visible = false;
                        lblswap.Visible = false;
                    }*/

                }
                if (l_AssessRw == null)
                {
                    cbReasons.SelectedIndex = -1;
                }
            }
            
            else
            {
                if (p_gradeid1 > 0)
                {
                    cbTeachers.Text = "No Lecture Found in time table for the grade";
                }
            }

            lblTime.Text = time;

            this.Load += new EventHandler(Form2_Load); 

        }

        void Form2_Load(object sender, EventArgs e)
        {
            cbReasons.SelectionChangeCommitted += new EventHandler(cbReasons_SelectionChangeCommitted);
            cbTeachers.SelectionChangeCommitted += new EventHandler(cbTeachers_SelectionChangeCommitted);
        }

        string p_batch = "(BATCH:NA)";

        int getSubjectID(TablerDBDataSet.AllocationTableRow l_allowRow)
        {
            int subjid = -1;
            TablerDBDataSet.TSFInalRow l_tsfinalrw = TsFinalTable.FindByID(l_allocrow[0].TSFinalID);
            if (l_tsfinalrw != null && l_tsfinalrw.IsSubjRoomStrNull() == false)
            {
                string[] l_subjRooms = l_tsfinalrw.SubjRoomStr.Split(',');
                if (cbTeachers.SelectedIndex > 0 && cbTeachers.SelectedIndex < l_subjRooms.Length)
                {
                    string[] l_sr = l_subjRooms[cbTeachers.SelectedIndex - 1].Split(':');
                    int.TryParse(l_sr[0], out subjid);
                    cbBoxSubject.SelectedValue = subjid;
                    if (l_allocrow[0].IsBatchesNull() == false)
                    {
                        string[] l_batches = l_allocrow[0].Batches.Split(',');

                        p_batch = l_batches[cbTeachers.SelectedIndex - 1];
                    }
                }
                
            }
            else
            {
                subjid = l_allocrow[0].SUBJID;
            }
            return subjid;
        }

        void cbTeachers_SelectionChangeCommitted(object sender, EventArgs e)
        {
            TablerDBDataSet.AllocationTableRow[] l_allocrows = (TablerDBDataSet.AllocationTableRow[])AllocationTable.Select(AllocationTable.GRDIDColumn.ColumnName + " = " + p_gradeid1 + " AND "
                        + AllocationTable.SlotAllotedColumn.ColumnName + " = " + slotnum);
            if (cbTeachers.SelectedIndex >= 0)
            {
                DataSet1.AssessmentRow l_AssessRw = GetAssessmentRow(l_d, slotnum, p_teachers[cbTeachers.SelectedIndex]);
                
                if (l_AssessRw != null)
                {
                    if (! l_AssessRw.IsReasonNull()) cbReasons.SelectedItem = l_AssessRw.Reason;
                    if (! l_AssessRw.IsRemarkNull()) tbRemarks.Text = l_AssessRw.Remark;
                    if (l_allocrows.Length > 0)
                        cbBoxSubject.SelectedValue = getSubjectID(l_allocrow[0]);
                    if (l_AssessRw.IsTeacherIDSwappedNull() == false)
                    {
                        cbTeacherSwapped.SelectedValue = l_AssessRw.TeacherIDSwapped;
                        cbTeacherSwapped.Visible = true;
                        lblswap.Visible = true;
                    }
                    else
                    {
                        cbTeacherSwapped.Hide();
                        lblswap.Hide();
                    }
                }
                else
                {
                    cbReasons.SelectedIndex = -1;
                    tbRemarks.Text = "";
                    int teacherNo = cbTeachers.SelectedIndex;                    
                    if (l_allocrows.Length > 0)
                    {
                        if (p_isPractical)
                        {
                            cbBoxSubject.SelectedValue = getSubjectID(l_allocrows[0]);           
                        }
                        else
                        {
                            cbBoxSubject.SelectedValue = l_allocrows[0].SUBJID;
                        }
                    }
                }
           }
            else 
            {
                DataSet1.AssessmentRow[] l_AssessRws = GetAssessmentRows(l_d, slotnum,p_gradeid1);

                if (l_AssessRws != null)
                {
                    if (l_AssessRws.Length == p_teachers.Length - 1)
                    {
                        if (!l_AssessRws[0].IsReasonNull()) cbReasons.SelectedItem = l_AssessRws[0].Reason;
                        if (!l_AssessRws[0].IsRemarkNull()) tbRemarks.Text = l_AssessRws[0].Remark;
                        if (l_AssessRws[0].IsTeacherIDSwappedNull() == false)
                        {
                            cbTeacherSwapped.SelectedValue = l_AssessRws[0].TeacherIDSwapped;
                            cbTeacherSwapped.Visible = true;
                            lblswap.Visible = true;
                        }
                        else
                        {
                            cbTeacherSwapped.Hide();
                            lblswap.Hide();
                        }
                    }
                }
                else
                {
                    cbReasons.SelectedIndex = -1;
                    tbRemarks.Text = "";
                    if (l_allocrows.Length > 0)
                    {
                        cbBoxSubject.SelectedValue = l_allocrows[0].SUBJID;
                        //cbBoxSubject.Text = SubjectTable.FindBySubjectID(l_allocrows[0].SUBJID).SubjectName;
                    }
                }
            }

        }

        void cbReasons_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cbReasons.SelectedIndex == 1 || cbReasons.SelectedIndex == 3)
            {
                if (p_teachers == null || (cbTeachers.SelectedIndex >= 0 && p_teachers[cbTeachers.SelectedIndex] != -1000))
                {
                    cbTeacherSwapped.Show();
                    lblswap.Show();
                    checkBox1.Visible = true;
                }
                else
                {
                    cbReasons.SelectedIndex = -1;
                }
            }
            else
            {
                cbTeacherSwapped.Hide();
                lblswap.Hide();
            }
        }

        DataSet1.AssessmentRow[] GetAssessmentRows(DateTime l_dt, int slotNum, int gradeid)
        {
            DataSet1.AssessmentRow[]  AssessRws1 = (DataSet1.AssessmentRow[])AssessmentTable.Select(AssessmentTable.SlotNumColumn.ColumnName + " = " + slotnum + " AND " +
               AssessmentTable.DateColumn.ColumnName + " = " + "'" + l_dt.Date.ToString() +"'" + " AND " + AssessmentTable.IsConductedColumn.ColumnName + " = " + false
                + " AND " + AssessmentTable.GradeIDColumn.ColumnName + " = " + gradeid);
         
            return AssessRws1;
        }

        DataSet1.AssessmentRow GetAssessmentRow(DateTime l_dt, int slotNum, int l_tid)
        {
             AssessRws = (DataSet1.AssessmentRow[])AssessmentTable.Select(AssessmentTable.SlotNumColumn.ColumnName + " = " + slotnum + " AND " +
                AssessmentTable.DateColumn.ColumnName + " = " + "'" + l_dt.Date.ToString() + "'" + " AND " + AssessmentTable.TeacherIDColumn.ColumnName + " = " +l_tid);
             if (AssessRws.Length > 0)
             {
                 return AssessRws[0];
             }
             return null;
        }

        bool p_donotOverwrite = false;
        public void SaveClassConducted(bool l_doNotOverwrite)
        {
            p_donotOverwrite = l_doNotOverwrite;
            btnSave_Click(null, null);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int l_teacher = -1;
            if (p_gradeid1 > 0 && (cbTeachers.SelectedIndex < 0 || (p_teachers[0] == -1000 && cbTeachers.SelectedIndex == 0)) && p_teachers != null)
            {
                for (int i = 1; i < p_teachers.Length; i++)
                {
                    l_teacher = p_teachers[i];
                    if (l_teacher == 0) continue;
                    DataSet1.AssessmentRow l_AssessRw4 = GetAssessmentRow(l_d, slotnum, l_teacher);
                    if (l_AssessRw4 == null)
                    {
                        l_AssessRw4 = AssessmentTable.NewAssessmentRow();
                        l_AssessRw4.Date = l_d.Date;
                        l_AssessRw4.SlotNum = slotnum;
                        l_AssessRw4.GradeID = p_gradeid1;
                        if (cbBoxSubject.Text.Trim().Length > 0)
                        {
                            l_AssessRw4.Subject = cbBoxSubject.Text;
                        }
                        else
                        {
                            l_AssessRw4.SetSubjectNull();
                        }
                        if (p_teachers != null)
                        {
                            l_AssessRw4.TeacherID = p_teachers[i];
                        }
                        else
                        {
                            l_AssessRw4.TeacherID = -1;
                        }
                        if (sender == null)
                        {
                            l_AssessRw4.IsConducted = true;
                        }
                        else
                        {
                            l_AssessRw4.IsConducted = false;
                            l_AssessRw4.Reason = cbReasons.SelectedValue.ToString();
                            l_AssessRw4.Remark = tbRemarks.Text;
                            if (cbTeacherSwapped.Visible && cbTeacherSwapped.SelectedValue != null && (cbReasons.SelectedIndex == 1 || cbReasons.SelectedIndex == 3))
                            {
                                l_AssessRw4.TeacherIDSwapped = Convert.ToInt32(cbTeacherSwapped.SelectedValue);
                                l_AssessRw4.AdjustedNotconducted = checkBox1.Checked;
                            }
                        }
                        l_AssessRw4.BatchETC = p_batch;
                        AssessmentTable.AddAssessmentRow(l_AssessRw4);
                        //AssessmentTable.AddAssessmentRow(l_d.Date,slotnum,gradeid1,p_teachers[comboBox3.SelectedIndex],Convert.ToInt32(comboBox2.SelectedValue), comboBox1.SelectedValue.ToString(), tbRemarks.Text);
                    }
                    else if (p_donotOverwrite == false)
                    {
                        if (sender == null)
                        {
                            l_AssessRw4.IsConducted = true;
                            l_AssessRw4.Remark = "";
                            l_AssessRw4.Reason = "";
                            l_AssessRw4.SetSubjectNull();
                            l_AssessRw4.SetTeacherIDSwappedNull();
                        }
                        else
                        {
                            l_AssessRw4.IsConducted = false;
                            l_AssessRw4.Remark = tbRemarks.Text;
                            l_AssessRw4.Reason = cbReasons.Text;
                            if (cbBoxSubject.Text.Trim().Length > 0)
                            {
                                l_AssessRw4.Subject = cbBoxSubject.Text;
                            }
                            else
                            {
                                l_AssessRw4.SetSubjectNull();
                            }
                            if (cbTeacherSwapped.Visible && cbTeacherSwapped.SelectedValue != null && (cbReasons.SelectedIndex == 1 || cbReasons.SelectedIndex == 3))
                            {
                                l_AssessRw4.TeacherIDSwapped = (int)cbTeacherSwapped.SelectedValue;
                                l_AssessRw4.AdjustedNotconducted = checkBox1.Checked;
                            }
                            else
                            {
                                l_AssessRw4.SetTeacherIDSwappedNull();
                            }
                        }
                    }
                    l_AssessRw4.Time = lblTime.Text; 
                    l_AssessRw4.IsPractical = p_isPractical;
                }
                AssessDA.Update(AssessmentTable);
                this.Close();
            }
            else
            {
                if (p_teachers != null && cbTeachers.SelectedIndex >= 0)
                {
                    l_teacher = p_teachers[cbTeachers.SelectedIndex];
                }
                DataSet1.AssessmentRow l_AssessRw = GetAssessmentRow(l_d, slotnum, l_teacher);
                if (l_AssessRw == null)
                {
                    l_AssessRw = AssessmentTable.NewAssessmentRow();
                    l_AssessRw.Date = l_d.Date;
                    l_AssessRw.SlotNum = slotnum;
                    l_AssessRw.GradeID = p_gradeid1;

                    l_AssessRw.TeacherID = l_teacher;

                    if (sender == null)
                    {
                        l_AssessRw.IsConducted = true;
                    }
                    else
                    {
                        l_AssessRw.IsConducted = false;
                        
                        l_AssessRw.Reason = cbReasons.SelectedValue.ToString();
                        l_AssessRw.Remark = tbRemarks.Text;
                        if (cbTeacherSwapped.Visible && cbTeacherSwapped.SelectedValue != null && cbReasons.SelectedIndex == 1)
                        {
                            l_AssessRw.TeacherIDSwapped = Convert.ToInt32(cbTeacherSwapped.SelectedValue);
                            l_AssessRw.AdjustedNotconducted = checkBox1.Checked; 

                        }
                        if (cbBoxSubject.Text.Trim().Length > 0)
                        {
                            l_AssessRw.Subject = cbBoxSubject.Text;
                        }
                        else
                        {
                            l_AssessRw.SetSubjectNull();
                        }
                    }
                    AssessmentTable.AddAssessmentRow(l_AssessRw);
                    //AssessmentTable.AddAssessmentRow(l_d.Date,slotnum,gradeid1,p_teachers[comboBox3.SelectedIndex],Convert.ToInt32(comboBox2.SelectedValue), comboBox1.SelectedValue.ToString(), tbRemarks.Text);
                }
                else if (p_donotOverwrite == false)                    
                {
                    if (sender == null)
                    {
                        l_AssessRw.IsConducted = true;
                        l_AssessRw.Remark = "";
                        l_AssessRw.Reason = "";
                        l_AssessRw.TeacherID = l_teacher;
                        l_AssessRw.SetSubjectNull();
                        l_AssessRw.SetTeacherIDSwappedNull();
                    }
                    else
                    {
                        l_AssessRw.IsConducted = false;
                        l_AssessRw.GradeID = p_gradeid1;
                        l_AssessRw.Remark = tbRemarks.Text;
                        l_AssessRw.Reason = cbReasons.Text;
                        if (cbBoxSubject.Text.Trim().Length > 0)
                        {
                            l_AssessRw.Subject = cbBoxSubject.Text;
                        }
                        else
                        {
                            l_AssessRw.SetSubjectNull();
                        }
                        if (cbTeacherSwapped.Visible && cbTeacherSwapped.SelectedValue != null && (cbReasons.SelectedIndex == 1 || cbReasons.SelectedIndex == 3) )
                        {
                            l_AssessRw.TeacherIDSwapped = (int)cbTeacherSwapped.SelectedValue;
                            l_AssessRw.AdjustedNotconducted = checkBox1.Checked;
                        }
                        else
                        {
                            l_AssessRw.SetTeacherIDSwappedNull();
                        }
                    }
                }

                
                /*if (l_teacher != 1000 && sender != null)
                {
                    DataSet1.AssessmentRow l_AssessRwToBeDeleted = GetAssessmentRow(l_d, slotnum, -1000);                     
                    if (l_AssessRwToBeDeleted != null)
                    {
                        l_AssessRwToBeDeleted.Delete();
                    }
                }*/
                l_AssessRw.IsPractical = p_isPractical;
                l_AssessRw.Time = lblTime.Text;
                int l_count  = AssessDA.Update(AssessmentTable);
                //MessageBox.Show("Updated " + l_count.ToString() + " Records");
                this.Close();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int l_teacherid = -1;
            if (p_teachers != null && cbTeachers.SelectedIndex >= 0 && cbTeachers.SelectedIndex < p_teachers.Length)
            {
                l_teacherid = p_teachers[cbTeachers.SelectedIndex];
            }
            if (l_teacherid != -1000)
            {
                DataSet1.AssessmentRow l_AssessRw = GetAssessmentRow(l_d, slotnum, l_teacherid);
                if (l_AssessRw != null)
                {
                    //l_AssessRw.Delete();
                    l_AssessRw.IsConducted = true;
                    l_AssessRw.Remark = "";
                    l_AssessRw.Reason = "";
                    l_AssessRw.SetTeacherIDSwappedNull();
                    AssessDA.Update(AssessmentTable);
                }
                else
                {
                    MessageBox.Show("Not found");
                }
            }
            else
            {
                foreach (int l_teacherID in p_teachers)
                {
                    if (l_teacherID != -1000 && l_teacherID > 0)
                    {
                        DataSet1.AssessmentRow l_AssessRw = GetAssessmentRow(l_d, slotnum, l_teacherID);
                        if (l_AssessRw != null)
                        {
                            //l_AssessRw.Delete();
                            l_AssessRw.IsConducted = true;
                            l_AssessRw.Remark = "";
                            l_AssessRw.Reason = "";
                            l_AssessRw.SetTeacherIDSwappedNull();

                            AssessDA.Update(AssessmentTable);
                        }
                        else
                        {
                            MessageBox.Show("One Teacher Not found out of " + (p_teachers.Length-1).ToString() );
                        }
                    }
                }
            }
            this.Close();

        }
       

      
    }
}
