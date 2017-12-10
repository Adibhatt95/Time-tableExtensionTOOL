using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TeachersAssessment
{
    public partial class fMonthlyReport : Form
    {
        enum COLS { SUBJ = 1, BRCH = 2, SEM = 3, SCHD = 4, HELD = 5, CANC = 6, ADJ = 7, ABS = 8, POOR =9, OTH = 10, TW20 = 11, FI50 = 12, EI80 = 13, HU100 = 14, DONE = 15};
        int p_teacherID = -1;
        struct DETAILS
        {
            public string[] SEMOTHERS;
            public int[] DATA;
        };

        const int STARTOFINTS = 4;
        System.Collections.Hashtable p_subjs = new System.Collections.Hashtable();

        public fMonthlyReport(int l_teacherID, string teachername)
        {
            InitializeComponent();
            lblTeacher.Text += " " + teachername;
            p_teacherID = l_teacherID;

            MessageBox.Show("Please note that the report will be accurate only if All the Daily Report data \r\n" +
                                    " and Attendance data is entered completely ");

            dateTimePicker1.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);

            foreach (DataGridViewColumn l_dgrvCol in dataGridView1.Columns)
            {                
                l_dgrvCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
                if (l_dgrvCol.Index == 0)
                {
                    l_dgrvCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }
                l_dgrvCol.MinimumWidth = 66;
            }
            this.Load += new EventHandler(fMonthlyReport_Load);

            lblError.Text = "* - Not implemented yet";
        }

        void fMonthlyReport_Load(object sender, EventArgs e)
        {
            this.BeginInvoke(new EventHandler(fillDataGrid));
        }

        void fillDataGrid(object sender, EventArgs e)
        {
            gatherData();

            if (p_subjs.Count == 0)
            {
                dataGridView1.Hide();
                lblError.Text = "No Records Found";
                return;
            }
            foreach (string l_subj in p_subjs.Keys)
            {
                int rowi = dataGridView1.Rows.Add();
                DETAILS l_details = (DETAILS) p_subjs[l_subj];
                dataGridView1[0, rowi].Value = (rowi+1).ToString();
                dataGridView1[(int)COLS.SUBJ, rowi].Value = l_subj;
                dataGridView1[(int)COLS.BRCH, rowi].Value = tGlobal.DEPARTMENT;
                dataGridView1[(int)COLS.SEM, rowi].Value = l_details.SEMOTHERS[0];
                for (int c = 4; c < (int)COLS.DONE; c++)
                {
                    dataGridView1[c, rowi].Value = l_details.DATA[c-STARTOFINTS];
                    if (c >= (int)COLS.TW20)
                    {
                        dataGridView1[c, rowi].Value = "*";
                    }
                }
            }
        }

        string getSemester(int month, string gradename)
        {
            string l_sem = "N.A.";
            char l_first = gradename.Substring(0,1)[0];
             
            if (month >= 7 && month <= 12)
            {
                switch (l_first)
                {
                    case 'F':
                        l_sem = "I";
                        break;
                    case 'S':
                        l_sem = "III";
                        break;
                    case 'T':
                        l_sem = "V";
                        break;
                    case 'B':
                        l_sem = "VII";
                        break;
                }
            }
            else
            {
                switch (l_first)
                {
                    case 'F':
                        l_sem = "II";
                        break;
                    case 'S':
                        l_sem = "IV";
                        break;
                    case 'T':
                        l_sem = "VI";
                        break;
                    case 'B':
                        l_sem = "VIII";
                        break;
                }

            }
            return l_sem;
        }

        void gatherData()
        {
            DateTime l_dtTo = dateTimePicker1.Value.AddMonths(1);
            DataSet1.AssessmentDataTable AssessmentTable = new DataSet1.AssessmentDataTable();
            System.Data.MPrSQL.MPrSQLDataAdapter AssessDA = MyTableAdapters.AssessTableAdapter();

            TablerDBDataSet.GradesDataTable Grades = new TablerDBDataSet.GradesDataTable();
            System.Data.MPrSQL.MPrSQLDataAdapter gradesDA = MyTableAdapters.GradesTableAdapter();

            AssessDA.Fill(AssessmentTable);
            int l_month = dateTimePicker1.Value.Month;

            gradesDA.Fill(Grades);

            foreach (DataSet1.AssessmentRow l_arw in AssessmentTable.Rows)
            {
                if (l_arw.IsSubjectNull() || l_arw.TeacherID != p_teacherID || l_arw.GradeID < 0) continue;
                if (!l_arw.IsConducted == false && l_arw.IsReasonNull() == false && l_arw.Reason.StartsWith("Official")) continue;

                if (l_arw.Date < dateTimePicker1.Value || l_arw.Date >= l_dtTo) continue;

                DETAILS l_details;

                string l_subject = l_arw.Subject;
                if (l_arw.IsPractical)
                {
                    l_subject += " PRACT ";
                    if (l_arw.IsBatchETCNull() == false)
                    {
                        l_subject += " " + l_arw.BatchETC;
                    }                        
                }

                if (p_subjs.ContainsKey(l_subject) == false)
                {
                    l_details = new DETAILS();
                    l_details.DATA = new int[(int)COLS.DONE];
                    l_details.SEMOTHERS = new string[2];
                    p_subjs.Add(l_subject, l_details);
                }
                else
                {
                    l_details = (DETAILS)p_subjs[l_subject];
                }
                TablerDBDataSet.GradesRow l_grdrw = Grades.FindByGradeID(l_arw.GradeID);
                string l_gradename = "NA";
                if (l_grdrw != null)
                {
                    l_gradename = l_grdrw.GradeName;
                }
                if (l_details.SEMOTHERS[0] == null)
                {
                    l_details.SEMOTHERS[0] = getSemester(l_month, l_gradename);
                }

                if (l_arw.IsTeacherIDSwappedNull() == false && l_arw.TeacherIDSwapped == p_teacherID)
                {
                    l_details.DATA[(int)COLS.HELD - STARTOFINTS]++;
                    //not added to scheduled so continue
                    continue;
                }

                l_details.DATA[(int)COLS.SCHD-STARTOFINTS]++;

                if (l_arw.IsConducted)
                {
                    l_details.DATA[(int)COLS.HELD - STARTOFINTS]++;
                }
                else
                {

                    if (l_arw.IsAdjustedNotconductedNull())
                    {
                        l_details.DATA[(int)COLS.ADJ - STARTOFINTS]++;
                    }
                    else if (l_arw.IsReasonNull() == false)
                    {
                        if (l_arw.Reason.StartsWith("SWA"))
                        {
                            l_details.DATA[(int)COLS.ADJ - STARTOFINTS]++;
                        }
                        else
                        {
                            if (l_arw.Reason.StartsWith("ABS"))
                            {
                                l_details.DATA[(int)COLS.ABS - STARTOFINTS]++;
                            }
                            else if (l_arw.Reason.StartsWith("MASS"))
                            {
                                l_details.DATA[(int)COLS.POOR - STARTOFINTS]++;
                            }
                            else
                            {
                                l_details.DATA[(int)COLS.OTH - STARTOFINTS]++;
                            }
                            l_details.DATA[(int)COLS.CANC - STARTOFINTS]++;                    
                        }
                    }

                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            
            p_subjs.Clear();
              
            this.BeginInvoke(new EventHandler(fillDataGrid));
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Not implmented yet");
        }
    }
}
