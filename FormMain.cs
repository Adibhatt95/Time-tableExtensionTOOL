using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace TeachersAssessment
{
    public partial class FormMain : Form
    {
        int t;
        tGlobal p_global = tGlobal.GetInstance(-1);
        TablerDBDataSet.TeachersDataTable p_Teachers = new TablerDBDataSet.TeachersDataTable();
        TablerDBDataSet.AllocationTableDataTable p_AllocationTable = new TablerDBDataSet.AllocationTableDataTable();
        DataSet1.PasswordsDataTable passwords = new DataSet1.PasswordsDataTable();
        System.Data.MPrSQL.MPrSQLDataAdapter passwordsDA = MyTableAdapters.PasswordsAdapter();
        DataSet1.ClassTeachersDataTable classTechrTable = new DataSet1.ClassTeachersDataTable();
        System.Data.MPrSQL.MPrSQLDataAdapter classTchrDA = MyTableAdapters.ClassTeachersAdapter();
        public FormMain()
        {
            InitializeComponent();
            System.Data.MPrSQL.MPrSQLDataAdapter l_TeachersDA = MyTableAdapters.TeachersTableAdapter();
            System.Data.MPrSQL.MPrSQLDataAdapter l_allocDA = MyTableAdapters.AllocationTableTableAdapter();
            l_TeachersDA.Fill(p_Teachers);
            l_allocDA.Fill(p_AllocationTable);
            TablerDBDataSet.TeachersDataTable l_Teachers = new TablerDBDataSet.TeachersDataTable();
            foreach (TablerDBDataSet.TeachersRow l_trw in p_Teachers)
            {
                TablerDBDataSet.AllocationTableRow[] l_allocrows = (TablerDBDataSet.AllocationTableRow[])p_AllocationTable.Select(
                                                                                                                                p_AllocationTable.TeacherIDColumn.ColumnName + " = " + l_trw.TeachersID
                                                                                                   + " OR " + p_AllocationTable.TeachersStrColumn.ColumnName + " LIKE '%" + l_trw.TeachersID.ToString() + ",%'"
                                                                                                   );
                foreach (TablerDBDataSet.AllocationTableRow l_alrw in l_allocrows)
                {
                   if (l_alrw.IsTeachersStrNull() || l_alrw.TeachersStr.Trim().Length == 0 || (l_alrw.TeachersStr.StartsWith(l_trw.TeachersID.ToString() + ",") ||
                                l_alrw.TeachersStr.Contains("," + l_trw.TeachersID.ToString() + ",")))
                    {
                        l_Teachers.ImportRow(l_trw);
                        break;
                    }
                }
            }
            TablerDBDataSet.TeachersRow l_trw1 = p_Teachers.FindByTeachersID(1);
            l_Teachers.ImportRow(l_trw1);
            DataView l_dv = new DataView(l_Teachers);
            l_dv.Sort = l_Teachers.TeachersNameColumn.ColumnName;
            TeacherComboBox.DataSource = l_dv;
            TeacherComboBox.DisplayMember = p_Teachers.TeachersNameColumn.ColumnName;
            TeacherComboBox.ValueMember = p_Teachers.TeachersIDColumn.ColumnName;
            TeacherComboBox.AutoCompleteMode = AutoCompleteMode.Suggest;
            TeacherComboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
        
            TeacherComboBox.SelectedIndex = -1;
            TeacherComboBox.Text = "Teacher Name";
            label3.Visible = false;
            p_global.TeacherIDSelected = 0;
            

            passwordsDA.Fill(passwords);
            classTchrDA.Fill(classTechrTable);
            textBox1.TextChanged +=new EventHandler(textBox1_TextChanged);

            TeacherComboBox.Select();

        }



        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnDailyReport_Click(object sender, EventArgs e)
        {
            if (TeacherComboBox.SelectedIndex == -1)
            {
                label5.Visible = true;
            }
            else
            {
                p_global.TeacherIDSelected = 0;
               bool access = Login(Convert.ToInt32(TeacherComboBox.SelectedValue));
               if (access)
               {
                   textBox1.Text = "";
                   DataSet1.ClassTeachersRow[] l_clrws = (DataSet1.ClassTeachersRow[]) 
                                                                classTechrTable.Select(classTechrTable.TeacherIDColumn.ColumnName + " = " + TeacherComboBox.SelectedValue.ToString());
                   if (l_clrws.Length > 0)
                   {
                       Form1 d_entry = new Form1(l_clrws[0].GradeID, TeacherComboBox.Text,Convert.ToInt32(TeacherComboBox.SelectedValue));
                       d_entry.ShowDialog();
                       this.Close();
                   }
                   else
                   {
                       MessageBox.Show("You need to be a class teacher to access this area.");
                   }
               }
            }
          
        }
        private bool Login(int tID)
        {
            t = tID;
            DataSet1.PasswordsRow[] passwordsRows = (DataSet1.PasswordsRow[])passwords.Select(passwords.TeacherIDColumn.ColumnName + " = " + tID);
            if (passwordsRows.Length == 1)
            {
                if (passwordsRows[0].Password == EncryptPassword(textBox1.Text).Replace(" ", ""))
                {
              

                    return true;

                }
                else
                {
                    label3.Visible = true;
                    return false;
                }
            }
            else if (passwordsRows.Length == 0)
            {
                if (textBox1.Text.Equals("123"))
                {
                
                    return true;
                }
                else
                {
                    label3.Visible = true;
                    return false;
                }
            }
            return false;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            label3.Hide();
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
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (TeacherComboBox.SelectedIndex >= 0)
            {
                t = Convert.ToInt32(TeacherComboBox.SelectedValue);
                Register d_entryR = new Register(t);
                d_entryR.ShowDialog();
                passwords.Clear();
                passwordsDA.Fill(passwords);
            }
            else
            {
                label3.Show();
            }
        }

        private void btnTeacherAssess_Click(object sender, EventArgs e)
        {
            if (TeacherComboBox.SelectedIndex == -1)
            {
                label5.Visible = true;
            }
            else
            {
                bool access = Login(Convert.ToInt32(TeacherComboBox.SelectedValue));
                if (access)
                {
                    textBox1.Text = "";
                    p_global.TeacherIDSelected = Convert.ToInt32(TeacherComboBox.SelectedValue.ToString());
                    Form1 d_entry = new Form1(p_global.TeacherIDSelected, TeacherComboBox.Text,Convert.ToInt32(TeacherComboBox.SelectedValue.ToString()));
                    d_entry.ShowDialog();
                    this.Close();
                }
            }
        }

        private void TeacherComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            label5.Visible = false;
        }

  

        
      
    }
}
