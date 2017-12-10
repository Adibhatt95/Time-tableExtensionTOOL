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
    public partial class Register : Form
    {
        DataSet1.PasswordsDataTable passwords = new DataSet1.PasswordsDataTable();
        System.Data.MPrSQL.MPrSQLDataAdapter passwordsDA = MyTableAdapters.PasswordsAdapter();
        TablerDBDataSet.TeachersDataTable p_Teachers = new TablerDBDataSet.TeachersDataTable();
        System.Data.MPrSQL.MPrSQLDataAdapter l_TeachersDA = MyTableAdapters.TeachersTableAdapter();
        int teachersID;

        public Register(int tID)
        {
            InitializeComponent();
            teachersID = tID;
            passwordsDA.Fill(passwords);

            l_TeachersDA.Fill(p_Teachers);

            TablerDBDataSet.TeachersRow l_trw = p_Teachers.FindByTeachersID(tID);

            if (l_trw != null)
            {
                label2.Text = l_trw.TeachersName + ", your old password:";
            }            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataSet1.PasswordsRow[] passwordsRows = (DataSet1.PasswordsRow[])passwords.Select(passwords.TeacherIDColumn.ColumnName + " = " + teachersID);
            if (textBox2.Text.Trim().Length > 2 && textBox2.Text.Equals(textBox3.Text))
            {
                if (passwordsRows.Length == 1)
                {
                    if (passwordsRows[0].Password == EncryptPassword(textBox1.Text).Replace(" ", ""))
                    {
                        passwordsRows[0].Password = EncryptPassword(textBox3.Text).Replace(" ", "");
                        passwordsDA.Update(passwords);
                        MessageBox.Show("Success");
                        this.Close();

                    }
                    else
                    {
                        MessageBox.Show("Old Password does not match your previous password");
                    }
                }
                else if (passwordsRows.Length == 0)
                {
                    if (textBox1.Text.Trim().Length == 0  || textBox1.Text.Equals("123"))
                    {
                        DataSet1.PasswordsRow newpasswordRow = passwords.NewPasswordsRow();
                        newpasswordRow.TeacherID = teachersID;
                        newpasswordRow.Password = EncryptPassword(textBox3.Text).Replace(" ", "");
                        passwords.AddPasswordsRow(newpasswordRow);
                        passwordsDA.Update(passwords);
                        MessageBox.Show("Success");
                        this.Close();

                    }
                    else
                    {
                        MessageBox.Show("Old Password does not match your previous password");
                    }
                }
            }
            else
            {
                MessageBox.Show("Password too short or New passwords do not match. Password not confirmed.");
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
    }
}
