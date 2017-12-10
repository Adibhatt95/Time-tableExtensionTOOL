using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TeachersAssessment
{
    public partial class fSelectDepartDateTeacher : Form
    {
        string[] p_connStrings, p_dbTTS, p_dbATs;

        string p_type = "";
        public fSelectDepartDateTeacher(string[] l_connStrings, string[] l_dbTTS, string[] l_dbATs, string[] l_depts, string l_type)
        {
            InitializeComponent();
            p_type = l_type;

            string[] l_Depts = new string[l_depts.Length - 1];
            for (int i = 1; i < l_depts.Length; i++ )
            {
                l_Depts[i - 1] = l_depts[i];
            }
            comboBox1.DataSource = l_Depts;

            if (l_Depts.Length == 1)
            {
                comboBox1.SelectedIndex = 0;
                comboBox1.Visible = false;
                label1.Visible = false;
            }
            p_connStrings = l_connStrings;
            p_dbATs = l_dbATs;
            p_dbTTS = l_dbTTS;


            this.Load += fSelectDepartDateTeacher_Load;
        }

        void fSelectDepartDateTeacher_Load(object sender, EventArgs e)
        {
            if (p_type.Equals("WR") || p_type.Equals("MR"))
            {
                label2.Visible = true;
                comboBox2.Visible = true;
            }

          
            button1.Click += button1_Click;

            if (comboBox2.Visible)
            {
                comboBox1.SelectedValueChanged += comboBox1_SelectedValueChanged;
            }
            
        }

        void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (comboBox2.Visible && comboBox2.Items.Count == 0)
            {
                if (initDatabase() == false)
                    return;
                TablerDBDataSet.TeachersDataTable l_ttbl = new TablerDBDataSet.TeachersDataTable();
                System.Data.MPrSQL.MPrSQLDataAdapter l_da = NorthernLights.MyTableAdapters.TeachersTableAdapter();

                l_da.Fill(l_ttbl);

                DataView l_dv = new DataView(l_ttbl);
                l_dv.Sort = l_ttbl.TeachersNameColumn.ColumnName;

                comboBox2.DataSource = l_dv;
                comboBox2.DisplayMember = l_ttbl.TeachersNameColumn.ColumnName;
                comboBox2.ValueMember = l_ttbl.TeachersIDColumn.ColumnName;

                comboBox2.AutoCompleteMode = AutoCompleteMode.Suggest;
                comboBox2.AutoCompleteSource = AutoCompleteSource.ListItems;
            }
        }

        bool p_initDone = false;

        bool initDatabase()
        {            
            Cursor.Current = Cursors.WaitCursor;
            NorthernLights.MyTableAdapters.CloseConnections();
            TeachersAssessment.MyTableAdapters.CloseConnections();

            TeachersAssessment.Properties.Settings.Default.TablerDBConnectionString = p_connStrings[comboBox1.SelectedIndex + 1] + "Database=" + p_dbTTS[comboBox1.SelectedIndex + 1];
            TeachersAssessment.Properties.Settings.Default.DataSet1Conn = p_connStrings[comboBox1.SelectedIndex + 1] + "Database=" + p_dbATs[comboBox1.SelectedIndex + 1];
            NorthernLights.Properties.Settings.Default.TablerDBConnectionString = p_connStrings[comboBox1.SelectedIndex + 1] + "Database=" + p_dbTTS[comboBox1.SelectedIndex + 1];
            try
            {
                System.Data.MPrSQL.tGlobal.p_connectionString = NorthernLights.Properties.Settings.Default.TablerDBConnectionString;
                NorthernLights.tGlobal.CreateAllTables();

                System.Data.MPrSQL.tGlobal.p_connectionString = TeachersAssessment.Properties.Settings.Default.DataSet1Conn;
                TeachersAssessment.tGlobal.CreateAllTables();

                TeachersAssessment.tGlobal.DROPAUTO();

                NorthernLights.MyTableAdapters.OpenConnections();
                TeachersAssessment.MyTableAdapters.OpenConnections();

                TeachersAssessment.tGlobal l_tGlobal = TeachersAssessment.tGlobal.GetInstance(-1);
                l_tGlobal.TeacherIDSelected = -1;
            }
            catch
            {
                int i = NorthernLights.Properties.Settings.Default.TablerDBConnectionString.IndexOf("password");
                string l_dbstr = NorthernLights.Properties.Settings.Default.TablerDBConnectionString.Substring(0, i);
                MessageBox.Show("Can not access the databases, Department: " + comboBox1.Text + " - " + l_dbstr);
                return false;
            }
            p_initDone = true;
            return true;

        }

        void button1_Click(object sender, EventArgs e)
        {
            if (p_initDone == false)
            {
                if (!initDatabase())
                {
                    return;
                }
            }
            if (comboBox1.Text.Length < 4)
            {
                TeachersAssessment.tGlobal.DEPARTMENT = comboBox1.Text;
            }
            else
            {
                TeachersAssessment.tGlobal.DEPARTMENT = comboBox1.Text.Substring(0, 4).ToUpper();
            }

            if (comboBox2.Visible == false)
            {
                ViewDailReport d_entry = new ViewDailReport(-1, null, -1);
                d_entry.HideDailyReportEditButtons();
                d_entry.ShowDialog();
            }
            else if (p_type.StartsWith("MR"))
            {
                fMonthlyReport l_freport = new fMonthlyReport((Convert.ToInt32(comboBox2.SelectedValue)), comboBox2.Text);
                l_freport.ShowDialog();
            }
            else
            {
                ViewAttendanceForms d_entry = new ViewAttendanceForms(comboBox2.Text);
                d_entry.ShowDialog();
            }
        }
    }
}
