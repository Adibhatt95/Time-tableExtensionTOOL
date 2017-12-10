using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;

namespace TeachersAssessment
{
    public partial class FormADDCLASSTEACHERS : Form
    {
        TablerDBDataSet.TeachersDataTable Teachers = new TablerDBDataSet.TeachersDataTable();
        TablerDBDataSet.GradesDataTable GRADES = new TablerDBDataSet.GradesDataTable();
        DataSet1.ClassTeachersDataTable classteachers = new DataSet1.ClassTeachersDataTable();
        System.Data.MPrSQL.MPrSQLDataAdapter classteachersDA = MyTableAdapters.ClassTeachersAdapter();

        public FormADDCLASSTEACHERS()
        {
            InitializeComponent();
            System.Data.MPrSQL.MPrSQLDataAdapter TeacherDA = MyTableAdapters.TeachersTableAdapter();
            System.Data.MPrSQL.MPrSQLDataAdapter gradesTableAdapter = MyTableAdapters.GradesTableAdapter();
            
            TeacherDA.Fill(Teachers);
            gradesTableAdapter.Fill(GRADES);
            classteachersDA.Fill(classteachers);
            
            TablerDBDataSet.GradesRow l_grwnew = GRADES.NewGradesRow();
            l_grwnew.GradeID = (int) tGlobal.GRADE_CODES.ALL;
            l_grwnew.GradeName = "All";
            GRADES.AddGradesRow(l_grwnew);
            gradesBindingSource.DataSource = GRADES;

            DataView l_dv = new DataView(Teachers);

            

            l_dv.Sort = Teachers.TeachersNameColumn.ColumnName;

            teachersBindingSource.DataSource = l_dv;

            this.Load += new EventHandler(FormADDCLASSTEACHERS_Load);

            dataGridView1.UserDeletingRow += new DataGridViewRowCancelEventHandler(dataGridView1_UserDeletingRow);
        }
        
        List<int> ids_deleted = new List<int>();

        void dataGridView1_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            DataGridViewRow l_rw = e.Row;
            if (l_rw.Cells[0].Value != null)
            {
                int id = -1;
                if (int.TryParse(l_rw.Cells[0].Value.ToString(), out id))
                {
                    ids_deleted.Add(id);
                }
            }
        }

        
        void FormADDCLASSTEACHERS_Load(object sender, EventArgs e)
        {
            foreach (DataSet1.ClassTeachersRow l_clrw in classteachers)
            {
                int i = dataGridView1.Rows.Add();

                dataGridView1[0, i].Value = l_clrw.ID;
                if (GRADES.FindByGradeID(l_clrw.GradeID) != null && Teachers.FindByTeachersID(l_clrw.TeacherID) != null)
                {
                    ((DataGridViewComboBoxCell)dataGridView1[1, i]).Value = l_clrw.GradeID;
                    ((DataGridViewComboBoxCell)dataGridView1[2, i]).Value = l_clrw.TeacherID;
                }


            }
        }

        
        private void button1_Click_1(object sender, EventArgs e)
        {
            foreach (DataGridViewRow l_rw in dataGridView1.Rows)
            {
                if ((l_rw.Cells[0].Value == null || l_rw.Cells[0].Value.ToString().Trim().Length == 0) 
                        && l_rw.IsNewRow == false)
                {
                    DataSet1.ClassTeachersRow classteachersRow = classteachers.NewClassTeachersRow();

                    try
                    {
                        classteachersRow.GradeID = (int)((DataGridViewComboBoxCell)l_rw.Cells[1]).Value;
                        TablerDBDataSet.GradesRow l_grw = GRADES.FindByGradeID(classteachersRow.GradeID);
                        classteachersRow.GradeName = l_grw.GradeName;
                        classteachersRow.TeacherID = (int)((DataGridViewComboBoxCell)l_rw.Cells[2]).Value;
                        classteachersRow.TeacherName = ((DataGridViewComboBoxCell)l_rw.Cells[2]).FormattedValue.ToString();
                        classteachers.AddClassTeachersRow(classteachersRow);                       

                    }
                    catch (Exception ex)
                    {
                        //MessageBox.Show("FAILED" + ex.Message);
                    }
                }
                else if (l_rw.Cells[0].Value != null)
                {
                    int id = -1;
                    if (int.TryParse(l_rw.Cells[0].Value.ToString(), out id))
                    {
                        DataSet1.ClassTeachersRow classteachersRow = classteachers.FindByID(id);
                        if (classteachersRow != null)
                        {
                            classteachersRow.GradeID = (int)((DataGridViewComboBoxCell)l_rw.Cells[1]).Value;
                            TablerDBDataSet.GradesRow l_grw = GRADES.FindByGradeID(classteachersRow.GradeID);
                            classteachersRow.GradeName = l_grw.GradeName;
                            classteachersRow.TeacherID = (int)((DataGridViewComboBoxCell)l_rw.Cells[2]).Value;
                            classteachersRow.TeacherName = ((DataGridViewComboBoxCell)l_rw.Cells[2]).FormattedValue.ToString();
                        }
                    }                
                }
            }
            foreach (int id in ids_deleted)
            {
                DataSet1.ClassTeachersRow classteachersRow = classteachers.FindByID(id);
                if (classteachersRow != null)
                {
                    classteachersRow.Delete();
                }
            }
            try
            {
                classteachersDA.Update(classteachers);
                MessageBox.Show("Saved");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("FAILED" + ex.Message);
            }
        }
    }
}
