using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;


namespace TeachersAssessment
{
    public partial class Login : Form
    {
        DataSet1.PasswordsDataTable passwords = new DataSet1.PasswordsDataTable();
        System.Data.MPrSQL.MPrSQLDataAdapter passwordsDA = MyTableAdapters.PasswordsAdapter();
        TablerDBDataSet.TeachersDataTable p_Teachers = new TablerDBDataSet.TeachersDataTable();
        System.Data.MPrSQL.MPrSQLDataAdapter l_TeachersDA = MyTableAdapters.TeachersTableAdapter();

        int tID;
        public Login(int teacherID)
        {
            InitializeComponent();
            tID = teacherID;
            passwordsDA.Fill(passwords);
            l_TeachersDA.Fill(p_Teachers);
            this.GotFocus += new EventHandler(Login_GotFocus);
            label1.Visible = false;
            label2.Text = p_Teachers.FindByTeachersID(tID).TeachersName + ", your password:";
        }

        void Login_GotFocus(object sender, EventArgs e)
        {
            passwordsDA.Fill(passwords);
            l_TeachersDA.Fill(p_Teachers);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Register d_entryR = new Register(tID);
            d_entryR.ShowDialog();
            passwords.Clear();
            passwordsDA.Fill(passwords);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataSet1.PasswordsRow[] passwordsRows = (DataSet1.PasswordsRow[])passwords.Select(passwords.TeacherIDColumn.ColumnName + " = " + tID);
            if (passwordsRows.Length == 1)
            {
                if (passwordsRows[0].Password == EncryptPassword(textBox1.Text).Replace(" ", ""))
                {
                    Form1 d_entry = new Form1(tID, null);
                    d_entry.Show();
                    this.Close();
                }
                else
                {
                    label1.Visible = true;
                }
            }
            else if(passwordsRows.Length == 0)
            {
                if (textBox1.Text.Equals("123"))
                {
                    Form1 d_entry = new Form1(tID);
                    d_entry.Show();
                    this.Close();
                }
                else
                {
                    label1.Visible = true;
                }
            }
        
           
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            label1.Visible = false;
        }
    }
}
