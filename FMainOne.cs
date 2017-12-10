using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace TeachersAssessment
{
    public partial class FMainOne : Form
    {
        string[] p_depts;
        string[] p_passwords;
        string[] p_connStrings;
        string[] p_dbsTT;
        string[] p_dbsAT;
        ToolTip p_ttip;

        public FMainOne()
        {
            InitializeComponent();
            p_ttip = new ToolTip();

            this.Load += new EventHandler(FMainOne_Load);
            btnTeacherAssessment.Click +=new EventHandler(btnTeacherAssessment_Click);
            btnTimeTable.Click +=new EventHandler(btnTimeTable_Click);
            linkLabel1.Click += new EventHandler(linkLabel1_Click);
            btnViewMultiDeptRoomAvail.Click += btnViewMultiDeptRoomAvail_Click;
            pbrefreshTeacherList.MouseHover += new EventHandler(pbrefreshTeacherList_MouseHover);
            btnDlyReport.Click +=new EventHandler(btnDlyReport_Click);
            btnMonthlyReport.Click += new EventHandler(btnMonthlyReport_Click);
            btnMonthReportHOD.Click += new EventHandler(btnMonthReportHOD_Click);
            btnAttenWeeklyReport.Click +=new EventHandler(btnAttenWeeklyReport_Click);

            this.FormClosed += new FormClosedEventHandler(FMainOne_FormClosed);
        }

        void btnMonthReportHOD_Click(object sender, EventArgs e)
        {
            fSelectDepartDateTeacher l_fselect = new fSelectDepartDateTeacher(p_connStrings, p_dbsTT, p_dbsAT, p_depts, "MR");
            l_fselect.ShowDialog();
        }

        void btnMonthlyReport_Click(object sender, EventArgs e)
        {
            fMonthlyReport l_fmreport = new fMonthlyReport(Convert.ToInt32(cbTeachers.SelectedValue), cbTeachers.Text);
            l_fmreport.ShowDialog();
        }

        void btnViewMultiDeptRoomAvail_Click(object sender, EventArgs e)
        {
            fRoomAvailabilityView l_roomView  = null;
            if (cbLoginType.Text.Equals("admin"))
            {
                l_roomView = new fRoomAvailabilityView(p_connStrings, p_dbsTT, p_dbsAT, p_depts);
            }
            else
            {
                string[] l_connstrings = new string[1];
                string[] l_dbsTT = new string[1];
                string[] l_dbsAT = new string[1];
                string[] l_depts = new string[1];

                l_connstrings[0] = p_connStrings[cbLoginType.SelectedIndex];
                l_dbsAT[0] = p_dbsAT[cbLoginType.SelectedIndex];
                l_dbsTT[0] = p_dbsTT[cbLoginType.SelectedIndex];
                l_depts[0] = p_depts[cbLoginType.SelectedIndex];

                l_roomView = new fRoomAvailabilityView(l_connstrings, l_dbsTT, l_dbsAT, l_depts);

            }
            l_roomView.WindowState = FormWindowState.Maximized;
            l_roomView.ShowDialog();
        }

        void FMainOne_FormClosed(object sender, FormClosedEventArgs e)
        {
            NorthernLights.MyTableAdapters.CloseConnections();
            TeachersAssessment.MyTableAdapters.CloseConnections();    
        }

        void pbrefreshTeacherList_MouseHover(object sender, EventArgs e)
        {
            p_ttip.Show("Refresh Teacher List", pbrefreshTeacherList, 1000);
        }

        bool loadInitFile()
        {
            cbLoginType.SelectedValueChanged -= new EventHandler(cbLoginType_LostFocus);
            string[] l_strs = null;
            string[] l_info = null;
            if (System.IO.File.Exists("ta.ini") == false && System.IO.File.Exists(".\\NorthernLights.ini"))
            {
                /*string[] a = { System.Data.MPrSQL.tGlobal.GetConnectionString("localhost", "3306", "DJ1", "12345678", "AssessmentTables"), //"Data Source = D:\\Documents\\NLReleaseDates\\JJGMKJ\\JGMKJBackUP\\TATable_MYSQL.db";
                               System.Data.MPrSQL.tGlobal.GetConnectionString("localhost", "3306", "DJ1", "12345678", "NLightsTimeTableData")
                             };
                System.IO.File.WriteAllLines(".\\NorthernLights.ini", a);*/
                
                l_strs = System.IO.File.ReadAllLines(".\\NorthernLights.ini");
                textBox1.Select();
            }
            else if (System.IO.File.Exists("ta.enc"))
            {
                fConfigureDepts.DecryptFile(".\\ta.enc", ".\\ta.ini");
                l_strs = System.IO.File.ReadAllLines(".\\ta.ini");
                
            }
            else if (System.IO.File.Exists("ta.ini"))
            {
                l_strs = System.IO.File.ReadAllLines(".\\ta.ini");

            }
            else
            {
                MessageBox.Show("Initialization files do not exist");
                this.Close();
            }
            p_depts = new string[l_strs.Length];
            p_connStrings = new string[l_strs.Length];
            p_passwords = new string[l_strs.Length];
            p_dbsAT = new string[l_strs.Length];
            p_dbsTT = new string[l_strs.Length];
            int i = 0;
            foreach (string l_firstLine in l_strs)
            {
                if (l_firstLine.Trim().Length == 0)
                {
                    MessageBox.Show("Please remove last line in .ini file");
                    this.Close();
                    return false;
                }
                l_info = l_firstLine.Split(',');
                if (l_info.Length < 5)
                {
                    MessageBox.Show("Badly Formed init string");
                    this.Close();
                    return false;
                }
                p_depts[i] = l_info[1];
                p_connStrings[i] = l_info[3];
                p_passwords[i] = l_info[2];
                if (l_info.Length > 5)
                {
                    p_dbsTT[i] = l_info[4];
                    p_dbsAT[i] = l_info[5];
                }
                i++;
            }
            cbLoginType.DataSource = p_depts;
            cbLoginType.SelectedValueChanged += new EventHandler(cbLoginType_LostFocus);
            return true;
        }
        void FMainOne_Load(object sender, EventArgs e)
        {
            if (loadInitFile())
            {
                this.BeginInvoke(new EventHandler(selectDepartment));
            }
            //cbTeachers.SelectedValueChanged += new EventHandler(cbTeachers_SelectedValueChanged);
            
        }

        void selectDepartment(object sender, EventArgs e)
        {
            if (p_depts.Length == 2)
            {
                cbLoginType.SelectedIndex = 1;
            }

        }

        void cbTeachers_SelectedValueChanged(object sender, EventArgs e)
        {
            flowLayoutTeacher.Hide();
            flowLayoutAdmin.Hide();
            btnLogin.Text = "Login";
        }

        TablerDBDataSet.TeachersDataTable p_tttble = new TablerDBDataSet.TeachersDataTable();
        DataSet1.PasswordsDataTable p_passwordtbl = new DataSet1.PasswordsDataTable();
        DataSet1.ClassTeachersDataTable p_classteachers = new DataSet1.ClassTeachersDataTable();
        System.Data.MPrSQL.MPrSQLDataAdapter p_passwordda = null;
        System.Data.MPrSQL.MPrSQLDataAdapter classteachersDA = null;

        void loadTeacherTT()
        {
            label4.Show();
            Cursor.Current = Cursors.WaitCursor;
            NorthernLights.MyTableAdapters.CloseConnections();
            TeachersAssessment.MyTableAdapters.CloseConnections();
    
            TeachersAssessment.Properties.Settings.Default.TablerDBConnectionString = p_connStrings[cbLoginType.SelectedIndex] + "Database=" + p_dbsTT[cbLoginType.SelectedIndex];
            TeachersAssessment.Properties.Settings.Default.DataSet1Conn = p_connStrings[cbLoginType.SelectedIndex] + "Database=" + p_dbsAT[cbLoginType.SelectedIndex];
            NorthernLights.Properties.Settings.Default.TablerDBConnectionString = p_connStrings[cbLoginType.SelectedIndex] + "Database=" + p_dbsTT[cbLoginType.SelectedIndex];
            try
            {
                System.Data.MPrSQL.tGlobal.p_connectionString = NorthernLights.Properties.Settings.Default.TablerDBConnectionString;
                NorthernLights.tGlobal.CreateAllTables();

                System.Data.MPrSQL.tGlobal.p_connectionString = TeachersAssessment.Properties.Settings.Default.DataSet1Conn;
                TeachersAssessment.tGlobal.CreateAllTables();

                TeachersAssessment.tGlobal.DROPAUTO();

                NorthernLights.MyTableAdapters.OpenConnections();
                TeachersAssessment.MyTableAdapters.OpenConnections();

                p_tttble.Clear();
                System.Data.MPrSQL.MPrSQLDataAdapter l_da = NorthernLights.MyTableAdapters.TeachersTableAdapter();

                l_da.Fill(p_tttble);

                p_tttble.PrimaryKey = null;

                TablerDBDataSet.TeachersRow l_trw = p_tttble.NewTeachersRow();
                l_trw.TeachersName = "HOD";
                l_trw.TeacherAbbr = "HOD";
                l_trw.TeachersID = -1000;
                p_tttble.AddTeachersRow(l_trw);

                l_trw = p_tttble.NewTeachersRow();
                l_trw.TeachersName = "DEPARTMENT ADMIN";
                l_trw.TeacherAbbr = "DEPADMIN";
                l_trw.TeachersID = -2000;
                p_tttble.AddTeachersRow(l_trw);

                p_passwordtbl.Clear();
                if (p_passwordda == null)
                {
                    p_passwordda = TeachersAssessment.MyTableAdapters.PasswordsAdapter();
                }
                p_passwordda.Fill(p_passwordtbl);

                if (classteachersDA == null)
                {
                    classteachersDA = TeachersAssessment.MyTableAdapters.ClassTeachersAdapter();
                }
                classteachersDA.Fill(p_classteachers);

                DataView l_dv = new DataView(p_tttble);
                l_dv.Sort = p_tttble.TeachersNameColumn.ColumnName;
                cbTeachers.DataSource = l_dv;
                cbTeachers.DisplayMember = p_tttble.TeachersNameColumn.ColumnName;
                cbTeachers.ValueMember = p_tttble.TeachersIDColumn.ColumnName;

                cbTeachers.AutoCompleteMode = AutoCompleteMode.Suggest;
                cbTeachers.AutoCompleteSource = AutoCompleteSource.ListItems;

                if (p_tttble.Rows.Count == 0)
                {
                    flowLayoutAdmin.Hide();
                    flowLayoutTeacher.Show();
                }

                cbTeachers.Select();
            }
            catch (Exception l_e)
            {
                MessageBox.Show("Error in accessing database, " + l_e.Message);
            }
            Cursor.Current = Cursors.Default;
            label4.Hide();
        }

        void cbLoginType_LostFocus(object sender, EventArgs e)
        {
            textBox1.Text = "";
            p_tttble.Clear();
            cbTeachers.Text = "";
            flowLayoutAdmin.Hide();
            flowLayoutTeacher.Hide();

            if (cbLoginType.SelectedIndex >= 0 && p_dbsTT[cbLoginType.SelectedIndex] != null &&
                        p_dbsTT[cbLoginType.SelectedIndex].Trim().Length > 0 && cbLoginType.Text.Equals("admin") == false)
            {
                loadTeacherTT();
                cbTeachers.Enabled = true;
                pbrefreshTeacherList.Enabled = true;
                label2.Enabled = true;
            }
            else
            {
                pbrefreshTeacherList.Enabled = false;
                cbTeachers.Enabled = false;
                label2.Enabled = false;
            }
        }

        private void btnTimeTable_Click(object sender, EventArgs e)
        {
            NorthernLights.Fmain l_fmaintt = new NorthernLights.Fmain();
            l_fmaintt.ShowDialog();            
        }

        private void btnTeacherAssessment_Click(object sender, EventArgs e)
        {
            TeachersAssessment.tGlobal l_tGlobal = TeachersAssessment.tGlobal.GetInstance(-1);
            l_tGlobal.TeacherIDSelected = Convert.ToInt32(cbTeachers.SelectedValue.ToString());
            ViewDailReport d_entry = new ViewDailReport(l_tGlobal.TeacherIDSelected, cbTeachers.Text, Convert.ToInt32(cbTeachers.SelectedValue.ToString()));
            d_entry.ShowDialog();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {            
            if (btnLogin.Text.Equals("Log&out"))
            {                
                flowLayoutAdmin.Hide();
                flowLayoutTeacher.Hide();
                btnLogin.Text = "Login";
                textBox1.Text = "";
                if (cbLoginType.Text.Equals("admin") == false || p_tttble.Rows.Count > 0)
                {
                    cbTeachers.Enabled = true;
                }
                cbLoginType.Enabled = true;
                textBox1.Enabled = true;
                pbrefreshTeacherList.Show();
                return;
            }
            if (cbLoginType.SelectedValue == null)
            {
                MessageBox.Show("Please select Department/LoginType", "Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
                flowLayoutTeacher.Hide();
                flowLayoutAdmin.Hide();
                return;
            }
            if (cbLoginType.Text.Length < 4)
            {
                TeachersAssessment.tGlobal.DEPARTMENT = cbLoginType.Text;
            }
            else
            {
                TeachersAssessment.tGlobal.DEPARTMENT = cbLoginType.Text.Substring(0, 4).ToUpper();
            }
            pbrefreshTeacherList.Hide();
            int l_i = cbLoginType.SelectedIndex;

            if (l_i < 0) return;
            if (cbTeachers.Enabled == false  && cbLoginType.Text.Equals("admin"))
            {
                if (textBox1.Text.Equals(p_passwords[l_i]))
                {
                    flowLayoutAdmin.Show();
                    
                    cbLoginType.Enabled = false;
                    textBox1.Enabled = false;
                    cbTeachers.Enabled = false;

                    btnLogin.Text = "Log&out";
                    this.AcceptButton = null;
                }
                else
                {
                    flowLayoutTeacher.Hide();
                    flowLayoutAdmin.Hide();
                    MessageBox.Show("Sorry! Incorrect password. Please retry.", "Password", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (cbTeachers.Enabled && cbTeachers.SelectedValue != null) //teacher login
            {
                int l_tid = (int)cbTeachers.SelectedValue;
                DataSet1.PasswordsRow[] l_prws = (DataSet1.PasswordsRow[])p_passwordtbl.Select(p_passwordtbl.TeacherIDColumn.ColumnName + " = " + l_tid);
                if ( ((l_prws.Length == 0 || l_prws[0].IsPasswordNull()) && textBox1.Text.Trim().Length == 0)
                       || (l_prws.Length > 0 && l_prws[0].Password == EncryptPassword(textBox1.Text).Replace(" ", "")))
                {
                    if (l_tid == -2000)
                    {
                        flowLayoutTeacher.Show();
                        btnDlyReport.Hide();
                        btnTeacherAssessment.Hide();
                        linkLabel1.Hide();
                        btnTimeTable.Show();
                        btnSettings.Show();
                    }
                    else if (l_tid == -1000)
                    {
                        flowLayoutAdmin.Show();
                        btnSetupDepts.Hide();
                    }
                    else
                    {
                        //l_tGlobal.TeacherIDSelected = l_tid;
                        flowLayoutTeacher.Show();
                        btnDlyReport.Show();
                        btnTeacherAssessment.Show();
                        btnTimeTable.Hide();
                        btnSettings.Hide();
                    }
                    linkLabel1.Show();
                    btnLogin.Text = "Log&out";
                    this.AcceptButton = null;
                    cbTeachers.Enabled = false;
                    cbLoginType.Enabled = false;
                    textBox1.Enabled = false;
                }
                else
                {
                    MessageBox.Show("Sorry! Incorrect password. Please retry.", "Password", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    flowLayoutTeacher.Hide();
                    flowLayoutAdmin.Hide();
                }
            }
        }

        private void btnSetupDepts_Click(object sender, EventArgs e)
        {
            fConfigureDepts l_fconfdept = new fConfigureDepts(p_depts, p_passwords, p_connStrings, p_dbsTT, p_dbsAT);
            l_fconfdept.ShowDialog();
            loadInitFile();
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            fSettings l_fs = new fSettings();
            l_fs.ShowDialog();
            p_classteachers.Clear();
            p_classteachers.AcceptChanges();
            classteachersDA.Fill(p_classteachers);
        }

        public string EncryptPassword(string stringToEncrypt)
        {
            MD5 md5 = new MD5CryptoServiceProvider();

            //compute hash from the bytes of text

            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(stringToEncrypt));

            //get hash result after compute it

            byte[] result = md5.Hash;

            StringBuilder strBuilder = new StringBuilder();

            for (int i = 0; i < result.Length; i++)
            {

                //change it into 2 hexadecimal digits

                //for each byte

                strBuilder.Append(result[i].ToString("x2"));

            }

            return strBuilder.ToString();

        }

        void linkLabel1_Click(object sender, EventArgs e)
        {
            if (cbTeachers.SelectedIndex >= 0)
            {
                int t = Convert.ToInt32(cbTeachers.SelectedValue);
                Register d_entryR = new Register(t);
                d_entryR.ShowDialog();
                p_passwordtbl.Clear();
                flowLayoutAdmin.Hide();
                flowLayoutTeacher.Hide();
                btnLogin.Text = "Login";
                textBox1.Text = "";

                cbLoginType.Enabled = cbTeachers.Enabled = textBox1.Enabled = true;
                cbTeachers.Select();

                p_passwordda.Fill(p_passwordtbl);
            }
            else
            {
                label3.Show();
            }
        }

        private void btnDlyReport_Click(object sender, EventArgs e)
        {
            if (p_classteachers.Rows.Count == 0)
            {
                classteachersDA.Dispose();
                classteachersDA = MyTableAdapters.ClassTeachersAdapter();
                classteachersDA.Fill(p_classteachers);
            }
            DataSet1.ClassTeachersRow[] l_clrws = (DataSet1.ClassTeachersRow[])
                                                                p_classteachers.Select(p_classteachers.TeacherIDColumn.ColumnName + " = "  
                                                                + cbTeachers.SelectedValue.ToString());
            if (l_clrws.Length > 0)
            {
                TeachersAssessment.tGlobal l_tGlobal = TeachersAssessment.tGlobal.GetInstance(-1);
                l_tGlobal.TeacherIDSelected = 0;
                ViewDailReport d_entry = new ViewDailReport(l_clrws[0].GradeID, cbTeachers.Text, Convert.ToInt32(cbTeachers.SelectedValue));
                d_entry.ShowDialog();               
            }
            else
            {
                MessageBox.Show("You need to be a class teacher to access this area.");
            }
        }

        private void refreshTeacherList_Click(object sender, EventArgs e)
        {
            loadTeacherTT();           
        }

        private void btnMDailyReport_Click(object sender, EventArgs e)
        {
            fSelectDepartDateTeacher l_fselect = new fSelectDepartDateTeacher(p_connStrings, p_dbsTT, p_dbsAT, p_depts, "DR");
            l_fselect.ShowDialog();
        }

        void btnAttenWeeklyReport_Click(object sender, System.EventArgs e)
        {
          fSelectDepartDateTeacher l_fselect = new fSelectDepartDateTeacher(p_connStrings, p_dbsTT, p_dbsAT, p_depts, "WR");
            l_fselect.ShowDialog();  
        }


    }
}
