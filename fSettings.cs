using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TeachersAssessment
{
    public partial class fSettings : Form
    {
        public fSettings()
        {
            InitializeComponent();
            this.Load += new EventHandler(fSettings_Load);
         
        }

        void fSettings_Load(object sender, EventArgs e)
        {
            TablerDBDataSet.GradesDataTable p_grades = new TablerDBDataSet.GradesDataTable();
            System.Data.MPrSQL.MPrSQLDataAdapter gradesTableAdapter = MyTableAdapters.GradesTableAdapter();
            gradesTableAdapter.Fill(p_grades);
            comboBox1.DataSource = p_grades;
            comboBox1.DisplayMember = p_grades.GradeNameColumn.ColumnName;
            comboBox1.ValueMember = p_grades.GradeIDColumn.ColumnName;
        }

        private void btnImportStudents_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedValue == null) return;
            DataSet1.StudentsDataTable students = new DataSet1.StudentsDataTable();
            System.Data.MPrSQL.MPrSQLDataAdapter studentsDA = MyTableAdapters.StudentsTableAdapter();
            studentsDA.Fill(students);
            students.SAPIDColumn.AutoIncrement = false;
            int gradeId = (int)comboBox1.SelectedValue;
            OpenFileDialog l_fdiag = new OpenFileDialog();
            l_fdiag.ShowDialog();
            Cursor.Current = Cursors.WaitCursor;
            int i = 0, j = 0;
            try
            {                
                string[] Lines = System.IO.File.ReadAllLines(l_fdiag.FileName);
                foreach (string line in Lines)
                {
                    string[] array = line.Split(',');
                    //if (array.Length < 5) continue;
                    ulong l_sapid = 0;
                    ulong.TryParse(array[1], out l_sapid);
                    DataSet1.StudentsRow l_strw = students.FindBySAPID(l_sapid);//array[1]);
                    if (l_strw == null)
                    {
                        l_strw = students.NewStudentsRow();
                        l_strw.SAPID = l_sapid;
                    }
                    l_strw.FullName = array[2].Trim();
                    string[] name = l_strw.FullName.Split(' ');
                    l_strw.FirstName = name[1];
                    l_strw.MiddleName = "";                    
                    if (name.Length == 2)
                    {
                        l_strw.LastName = name[0];
                        l_strw.MiddleName = "";
                    }
                    else if (name.Length == 3)
                    {
                        l_strw.MiddleName = name[2];
                        if (l_strw.MiddleName == null || l_strw.MiddleName == "")
                        {
                            l_strw.MiddleName = "";
                        }
                    }
                    l_strw.LastName = name[0];

                    
                    //l_strw.GradeName = array[4];//p_grades.FindByGradeID(gradeId).GradeName;// array[3];
                    l_strw.GradeID = (int)comboBox1.SelectedValue;
                    l_strw.GradeName = comboBox1.Text;

                    l_strw.Batch = array[3];
                    l_strw.Elective = "none";
                    if (array.Length == 5)
                    {
                        l_strw.Elective = array[4];
                    }
                    if (l_strw.RowState == DataRowState.Detached)
                    {
                        students.AddStudentsRow(l_strw);
                        i++;
                    }
                    else
                    {
                        j++;
                    }
                }
                int l_countUpdated = studentsDA.Update(students);
                MessageBox.Show("Number of Student records added: " + i.ToString() + " Modified: " + j.ToString() +
                                            "\r\n" + "Total Actual Updates = " + l_countUpdated.ToString());
            }
            catch (Exception l_e)
            {
                MessageBox.Show(l_e.Message);
            }
            Cursor.Current = Cursors.Default;

        }

        private void btnTeacherRoles_Click(object sender, EventArgs e)
        {
            FormADDCLASSTEACHERS l_form = new FormADDCLASSTEACHERS();
            l_form.Show();
        }
    }
}
