using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Collections;
using System.IO;

namespace TeachersAssessment
{
    public partial class fConfigureDepts : Form
    {
        string[] p_depts;
        string[] p_passwords;
        string[] p_connStrings;
        string[] p_dbsTT;
        string[] p_dbsAT;

        public fConfigureDepts(string[] l_depts,
                                string[] l_passwords,
                                string[] l_connStrings,
                                string[] l_dbsTT,
                                string[] l_dbsAT)
        {
            InitializeComponent();
            p_depts = l_depts;
            p_passwords = l_passwords;
            p_connStrings = l_connStrings;
            p_dbsAT = l_dbsAT;
            p_dbsTT = l_dbsTT;

 
            this.Load += new EventHandler(fConfigureDepts_Load);
        }

        void fConfigureDepts_Load(object sender, EventArgs e)
        {
            dataGridView1.CellValidated += new DataGridViewCellEventHandler(dataGridView1_CellValidated);
            for (int l_i = 0; l_i < p_depts.Length; l_i++)
            {
                dataGridView1.Rows.Add();
                dataGridView1[0, l_i].Value = p_depts[l_i];
                dataGridView1[1, l_i].Value = p_passwords[l_i];
                dataGridView1[2, l_i].Value = p_connStrings[l_i];
                dataGridView1[3, l_i].Value = p_dbsTT[l_i];
                dataGridView1[4, l_i].Value = p_dbsAT[l_i];
            }
        }

        void dataGridView1_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 && dataGridView1[e.ColumnIndex, e.RowIndex].Value is string
                            && ((string)dataGridView1[e.ColumnIndex, e.RowIndex].Value).Trim().Length > 0
                            && e.RowIndex > 0)
            {
                if (dataGridView1[2, e.RowIndex].Value == null)
                {
                    dataGridView1[2, e.RowIndex].Value = dataGridView1[2, e.RowIndex - 1].Value;
                    dataGridView1[3, e.RowIndex].Value = dataGridView1[e.ColumnIndex, e.RowIndex].Value.ToString() + "TT";
                    dataGridView1[4, e.RowIndex].Value = dataGridView1[e.ColumnIndex, e.RowIndex].Value.ToString() + "AT";
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int i = 0;
            List<string> l_strs = new List<string>();
            foreach (DataGridViewRow l_rw in dataGridView1.Rows)
            {
                if (l_rw.IsNewRow) continue;

                string str = i.ToString() + ",";
                foreach (DataGridViewCell l_cell in l_rw.Cells)
                {
                    if (l_cell.Value is string && ((string)l_cell.Value).Trim().Length > 0)
                    {
                        str += l_cell.Value.ToString() + ",";
                    }
                    else
                    {
                        if (i > 0 || l_cell.ColumnIndex <= 1)
                        {
                            MessageBox.Show("Row:" + i.ToString() + " skipped as value not filled");
                            str = null;
                            break;
                        }
                    }
                }
                if (str != null)
                {
                    l_strs.Add(str);
                }
                i++;
            }
            if (l_strs.Count > 0)
            {
                try
                {
                    System.IO.File.Delete(".\\ta.ini");
                    System.IO.File.WriteAllLines(".\\ta.ini", l_strs.ToArray());
                    EncryptFile(".\\ta.ini", ".\\ta.enc");

                    MessageBox.Show("Saved");
                    this.Close();
                }
                catch (Exception l_e){
                    MessageBox.Show(l_e.Message);
                }
            }
        }

        // Steve Lydford - 12/05/2008.
        ///
        /// Encrypts a file using Rijndael algorithm.
        ///</summary>
        ///<param name="inputFile"></param>
        ///<param name="outputFile"></param>
        public static void EncryptFile(string inputFile, string outputFile)
        {

            try
            {
                string password = @"myKey123"; // Your Key Here
                UnicodeEncoding UE = new UnicodeEncoding();
                byte[] key = UE.GetBytes(password);

                string cryptFile = outputFile;
                FileStream fsCrypt = new FileStream(cryptFile, FileMode.Create);

                RijndaelManaged RMCrypto = new RijndaelManaged();

                CryptoStream cs = new CryptoStream(fsCrypt,
                    RMCrypto.CreateEncryptor(key, key),
                    CryptoStreamMode.Write);

                FileStream fsIn = new FileStream(inputFile, FileMode.Open);

                int data;
                while ((data = fsIn.ReadByte()) != -1)
                    cs.WriteByte((byte)data);


                fsIn.Close();
                cs.Close();
                fsCrypt.Close();
            }
            catch
            {
                MessageBox.Show("Encryption failed!", "Error");
            }
        }
        ///<summary>
        /// Steve Lydford - 12/05/2008.
        ///
        /// Decrypts a file using Rijndael algorithm.
        ///</summary>
        ///<param name="inputFile"></param>
        ///<param name="outputFile"></param>
        public static void DecryptFile(string inputFile, string outputFile)
        {
            {
                string password = @"myKey123"; // Your Key Here

                UnicodeEncoding UE = new UnicodeEncoding();
                byte[] key = UE.GetBytes(password);

                FileStream fsCrypt = new FileStream(inputFile, FileMode.Open);

                RijndaelManaged RMCrypto = new RijndaelManaged();

                CryptoStream cs = new CryptoStream(fsCrypt,
                    RMCrypto.CreateDecryptor(key, key),
                    CryptoStreamMode.Read);

                FileStream fsOut = new FileStream(outputFile, FileMode.Create);

                int data;
                while ((data = cs.ReadByte()) != -1)
                    fsOut.WriteByte((byte)data);

                fsOut.Close();
                cs.Close();
                fsCrypt.Close();

            }
        }
     
    }
}
