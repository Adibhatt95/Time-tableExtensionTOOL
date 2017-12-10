using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;
using Word = Microsoft.Office.Interop.Word;
using Excel = Microsoft.Office.Interop.Excel;

namespace TeachersAssessment
{
    public partial class ViewAttendanceForms : Form
    {
        TablerDBDataSet.GradesDataTable p_grades = new TablerDBDataSet.GradesDataTable();
        System.Data.MPrSQL.MPrSQLDataAdapter AttendDA = MyTableAdapters.AttendanceTableAdapter();
        System.Data.MPrSQL.MPrSQLDataAdapter AssessDA = MyTableAdapters.AssessTableAdapter();
        TablerDBDataSet.AllocationTableDataTable AllocationTable = new TablerDBDataSet.AllocationTableDataTable();
        System.Data.MPrSQL.MPrSQLDataAdapter allocationDA = MyTableAdapters.AllocationTableTableAdapter();
        DataSet1.AttendanceDataTable Attend = new DataSet1.AttendanceDataTable();
        DataSet1.AssessmentDataTable assessTable = new DataSet1.AssessmentDataTable();
        System.Data.MPrSQL.MPrSQLDataAdapter GradesDA = MyTableAdapters.GradesTableAdapter();
        
        System.Data.MPrSQL.MPrSQLDataAdapter subjDA = MyTableAdapters.SubjectsTableAdapter();
        TablerDBDataSet.SubjectsDataTable p_subjsTbl = new TablerDBDataSet.SubjectsDataTable();

        System.Data.MPrSQL.MPrSQLDataAdapter tsfinalDA = MyTableAdapters.TSFInalTableAdapter();
        TablerDBDataSet.TSFInalDataTable p_tsFinalTbl = new TablerDBDataSet.TSFInalDataTable();
    
        System.Data.MPrSQL.MPrSQLDataAdapter teachDA = MyTableAdapters.TeachersTableAdapter();
        TablerDBDataSet.TeachersDataTable p_tTbl = new TablerDBDataSet.TeachersDataTable();

        tGlobal p_global = tGlobal.GetInstance(-1);
        DataSet1.StudentsDataTable Students = new DataSet1.StudentsDataTable();
        System.Data.MPrSQL.MPrSQLDataAdapter StudentDA = MyTableAdapters.StudentsTableAdapter();
        String subject;
        String p_tName;
        int countScheduled=0;
        System.Collections.Hashtable p_teachersList = new System.Collections.Hashtable();

        public ViewAttendanceForms(string l_teacherName)
        {
            InitializeComponent();
            this.Text = "View Attendance Stat for : " + l_teacherName;
            string[] type={"Type","Lecture","Practical","Elective Lecture"};
            comboBoxType.DataSource = type;
            comboBoxType.SelectedIndex = 0;
            AttendDA.Fill(Attend);
            AssessDA.Fill(assessTable);
            GradesDA.Fill(p_grades);
            comboBoxGrade.DataSource = p_grades;
            comboBoxGrade.ValueMember = p_grades.GradeIDColumn.ColumnName;
            comboBoxGrade.DisplayMember = p_grades.GradeNameColumn.ColumnName;
            StudentDA.Fill(Students);
            allocationDA.Fill(AllocationTable);
            button1.Click +=new EventHandler(button1_Click_1);
            comboBoxBatch.Visible = false;
            label11.Visible = false;

            subjDA.Fill(p_subjsTbl);
            DataView l_dvsubj = new DataView(p_subjsTbl);

            l_dvsubj.Sort = p_subjsTbl.SubjectNameColumn.ColumnName;
            comboBoxSubjects.DataSource = l_dvsubj;
            comboBoxSubjects.DisplayMember = p_subjsTbl.SubjectNameColumn.ColumnName;
            comboBoxSubjects.ValueMember = p_subjsTbl.SubjectIDColumn.ColumnName;
            comboBoxSubjects.AutoCompleteMode = AutoCompleteMode.Suggest;
            comboBoxSubjects.AutoCompleteSource = AutoCompleteSource.ListItems;

            p_tName = l_teacherName;

            tsfinalDA.Fill(p_tsFinalTbl);
            teachDA.Fill(p_tTbl);

            dateTimePicker1.ValueChanged += new EventHandler(dateTimePicker1_ValueChanged);
        }

        void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker2.Value = dateTimePicker1.Value.AddDays(7);
        }

        bool practorNot = false;

        private void button2_Click(object sender, EventArgs e)
        {
            if ((int)(dateTimePicker1.Value.DayOfWeek) != 1) 
            {
                MessageBox.Show("Warning: Monday is not selected as start date!");
            }
            if (comboBoxSubjects.SelectedIndex < 0)
            {
                MessageBox.Show("Please select subject");
                return;
            }
            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();
            dataGridView2.Columns.Clear();
            dataGridView2.Rows.Clear();
            dataGridView3.Columns.Clear();
            dataGridView3.Rows.Clear();
            dataGridView4.Columns.Clear();
            dataGridView4.Rows.Clear();

            if (comboBoxType.SelectedText == "Type")
            {
                MessageBox.Show("Please select Type");
            }

            if (comboBoxType.SelectedValue == "Lecture")                
            {
                tableLayoutPanel2.ColumnStyles[1].SizeType = SizeType.AutoSize;
                tableLayoutPanel2.ColumnStyles[2].SizeType = SizeType.AutoSize;
                tableLayoutPanel2.ColumnStyles[3].SizeType = SizeType.AutoSize;
                practorNot = false;

                DataSet1.AttendanceRow[] lAttendRows = (DataSet1.AttendanceRow[])Attend.Select(Attend.GradeIDColumn.ColumnName + " = " + comboBoxGrade.SelectedValue
                    + " AND " + Attend.DateColumn.ColumnName + " >= '" + dateTimePicker1.Value.Date + "' AND " + Attend.DateColumn.ColumnName + "<='" + dateTimePicker2.Value.Date + "' AND " +
                    Attend.PracticalorNOTColumn.ColumnName + " = " + practorNot /*+ " AND " + Attend.TeacherIDColumn.ColumnName + " = " + p_global.TeacherIDSelected*/
                    + " AND " + Attend.SubjectNameColumn.ColumnName + " = '" + comboBoxSubjects.Text + "'",
                                Attend.SlotIDColumn.ColumnName + "," + Attend.DateColumn.ColumnName + "," + Attend.SAPIDColumn.ColumnName
                                );
                //dataGridView1.ColumnCount = (int)Attend.Compute("count (distinct " + Attend.DateColumn.ColumnName + ")",  Attend.DateColumn.ColumnName+" <= '" 
                //    + dateTimePicker2.Value + "' AND " + Attend.DateColumn.ColumnName + " >= '" + dateTimePicker1.Value + "'")+2;

                dataGridView1.Columns.Add("SAP", "SAP");
                dataGridView1.Columns.Add("Name", "Name");
                dataGridView1.Columns[0].ValueType = typeof(long);
                this.dataGridView1.DefaultCellStyle.Font = new Font("Times New Roman", 9);
                if (lAttendRows.Length <= 0)
                {
                    return;
                }    

                subject = lAttendRows[0].SubjectName;
                //dataGridView1.Columns[0].ValueType = typeof(System.DateTime);

                dataGridView1.AutoSize = true;
                //int a = Convert.ToInt32(dateTimePicker1.Value.DayOfWeek);
                String absentorpres = "A";
                //int col = 12 * (a - 1) + 1;
                int Column = 1;
                DateTime date = DateTime.MinValue;

                int slot = -1;
                DataSet1.StudentsRow[] StuRows = (DataSet1.StudentsRow[])Students.Select(Students.GradeIDColumn.ColumnName + " = " + comboBoxGrade.SelectedValue);
                DateTime l_d = dateTimePicker1.Value;
                countScheduled = 0;

                while (l_d.Date <= dateTimePicker2.Value.Date)
                {
                    int a = Convert.ToInt32(l_d.DayOfWeek);
                    int col = 22 * (a - 1) + 1;
                    TablerDBDataSet.AllocationTableRow[] AllocRows = (TablerDBDataSet.AllocationTableRow[])AllocationTable.Select(AllocationTable.SUBJIDColumn.ColumnName
                        + " = " + (int) comboBoxSubjects.SelectedValue + " AND " + AllocationTable.SlotAllotedColumn.ColumnName + " >= " + col + " AND " + AllocationTable.SlotAllotedColumn.ColumnName
                        + " <= " + (col + 21) + " AND " + AllocationTable.GRDIDColumn.ColumnName + " = " + comboBoxGrade.SelectedValue);
                    countScheduled = countScheduled + AllocRows.Length;
                    
                    l_d = l_d.AddDays(1);
                }
                countScheduled = countScheduled / 2;
               // MessageBox.Show("Scheduled lectures:" + countScheduled);
                int row = 1;
                foreach (DataSet1.StudentsRow StuRow in StuRows)
                {
                    dataGridView1.Rows.Add(StuRow.SAPID, Students.FindBySAPID(StuRow.SAPID).FullName);
                }
                foreach (DataSet1.AttendanceRow AttendRow in lAttendRows)
                {
                    if (AttendRow.SlotID != slot || AttendRow.Date.Date != date)
                    {
                        row = 0;
                        date = AttendRow.Date.Date;
                        slot = AttendRow.SlotID;
                        Column = dataGridView1.Columns.Add(AttendRow.Date.Date.ToShortDateString().Substring(0,5) + " " + AttendRow.Time, AttendRow.Date.Date.ToShortDateString().Substring(0,5) + " at\r\n" + AttendRow.Time);
                    }
                    if (row < dataGridView1.Rows.Count && dataGridView1.Rows[row].Cells["SAP"].Value.Equals(AttendRow.SAPID))
                    {
                        if (AttendRow.AbsORPres == true)
                        {
                            absentorpres = "P";

                        }
                        else
                        {
                            absentorpres = "A";
                            dataGridView1.Rows[row].Cells[Column].Style.BackColor = System.Drawing.Color.Red;
                            dataGridView1.Rows[row].Cells[Column].Style.Font = new Font("Impact",9);
                        }
                        dataGridView1.Rows[row].Cells[Column].Value = absentorpres;

                        row++;
                    }
                }
            }
            else if (comboBoxType.SelectedValue == "Practical")
            {
                dataGridView2.Visible = dataGridView3.Visible = dataGridView4.Visible = true;
                tableLayoutPanel2.ColumnStyles[1].SizeType = SizeType.Percent;
                tableLayoutPanel2.ColumnStyles[2].SizeType = SizeType.Percent;
                tableLayoutPanel2.ColumnStyles[3].SizeType = SizeType.Percent;

                practorNot = true;
                DataSet1.AttendanceRow[] lAttendRows = (DataSet1.AttendanceRow[])Attend.Select(Attend.GradeIDColumn.ColumnName+" = "+comboBoxGrade.SelectedValue
                  + " AND " + Attend.DateColumn.ColumnName + " >= '" + dateTimePicker1.Value.Date + "' AND " + Attend.DateColumn.ColumnName + "<='" + dateTimePicker2.Value.Date + "' AND " +
                  Attend.PracticalorNOTColumn.ColumnName + " = " + practorNot
                  + " AND " + Attend.SubjectNameColumn.ColumnName + " = '" + comboBoxSubjects.Text + "'"
                  /*+ " AND " + Attend.TeacherIDColumn.ColumnName + " = " + p_global.TeacherIDSelected*/,
                              Attend.SlotIDColumn.ColumnName + "," + Attend.DateColumn.ColumnName + "," + Attend.SAPIDColumn.ColumnName);
                //dataGridView1.ColumnCount = (int)Attend.Compute("count (distinct " + Attend.DateColumn.ColumnName + ")",  Attend.DateColumn.ColumnName+" <= '" 
                //    + dateTimePicker2.Value + "' AND " + Attend.DateColumn.ColumnName + " >= '" + dateTimePicker1.Value + "'")+2;
                
                if (lAttendRows.Length <= 0)
                {
                    return;
                }    
                subject = lAttendRows[0].SubjectName;

                countScheduled = 0;
                DateTime l_d = dateTimePicker1.Value;
                //while (l_d.Date <= dateTimePicker2.Value.Date)
                {
                    //int a = Convert.ToInt32(l_d.DayOfWeek);
                    //int col = 22 * (a - 1) + 1;
                    /*TablerDBDataSet.AllocationTableRow[] AllocRows = (TablerDBDataSet.AllocationTableRow[])AllocationTable.Select(AllocationTable.SlotAllotedColumn.ColumnName + " >= " + col + " AND " + AllocationTable.SlotAllotedColumn.ColumnName
                                    + " <= " + (col + 21) + " AND " + AllocationTable.GRDIDColumn.ColumnName + " = " + comboBoxGrade.SelectedValue
                                    + " AND " + AllocationTable.DurationColumn.ColumnName + " = " + 120                                  
                                  );*/
                    TablerDBDataSet.AllocationTableRow[] AllocRows = (TablerDBDataSet.AllocationTableRow[])AllocationTable.Select(AllocationTable.GRDIDColumn.ColumnName + " = " + comboBoxGrade.SelectedValue
                                    + " AND " + AllocationTable.DurationColumn.ColumnName + " = " + 120
                                  );
                    foreach (TablerDBDataSet.AllocationTableRow l_alocrw in AllocRows)
                    {                        
                        TablerDBDataSet.TSFInalRow l_tsfinalrw = p_tsFinalTbl.FindByID(l_alocrw.TSFinalID);
                        if (l_tsfinalrw != null && 
                                    (l_tsfinalrw.SubjRoomStr.StartsWith(comboBoxSubjects.SelectedValue.ToString() + ":") 
                                        || l_tsfinalrw.SubjRoomStr.Contains("," + comboBoxSubjects.SelectedValue.ToString() + ":"))) 
                        {
                            countScheduled++;
                        }
                    }

                    //l_d = l_d.AddDays(1);
                }
                countScheduled = countScheduled / 4;

                int slot = -1;
                int l_gridviewno = 1;
                p_teachersList.Clear();
                foreach (object o in comboBoxBatch.Items)
                {
                    DataGridView dgv = dataGridView1;
                    switch (l_gridviewno)
                    {
                        case 2:
                            dgv = dataGridView2;
                            break;
                        case 3:
                            dgv = dataGridView3;
                            break;
                        case 4:
                            dgv = dataGridView4;
                            break;
                    }
                    l_gridviewno++;

                    dgv.Columns.Add("SAP", "SAP (Batch:" + o.ToString() +")");
                    //dgv.Columns.Add("Name", "Name");
                    //dgv.Columns.Add("Batch", "Batch");

                    dgv.Columns[0].ValueType = typeof(long);
                    dgv.Columns[0].DefaultCellStyle.Font =
                    dgv.DefaultCellStyle.Font = new Font("Times New Roman", 9);
                    //int a = Convert.ToInt32(dateTimePicker1.Value.DayOfWeek);
                    String absentorpres = "A";
                    //int col = 12 * (a - 1) + 1;
                    int Column = 1;
                    DateTime date = DateTime.MinValue;

                        
                    DataSet1.StudentsRow[] StuRows = (DataSet1.StudentsRow[])Students.Select(Students.GradeIDColumn.ColumnName + " = " + comboBoxGrade.SelectedValue 
                                                        + " AND " + 
                                                            Students.BatchColumn.ColumnName + " = '" + o.ToString() +"'");

                    //int row = 1;
                    System.Collections.Hashtable l_SAPROnos = new System.Collections.Hashtable();
                  
                    foreach (DataSet1.StudentsRow StuRow in StuRows)
                    {
                        int l_rowno = dgv.Rows.Add(StuRow.SAPID);// Students.FindBySAPID(StuRow.SAPID).FullName, StuRow.Batch);
                        if (l_SAPROnos.ContainsKey(StuRow.SAPID) == false)
                        {
                            l_SAPROnos.Add(StuRow.SAPID, l_rowno);
                        }
                    }
                    //row = 0;
                    foreach (DataSet1.AttendanceRow AttendRow in lAttendRows)
                    {
                        if (l_SAPROnos.ContainsKey(AttendRow.SAPID) == false) continue;
                        int row = (int)l_SAPROnos[AttendRow.SAPID];

                        if (AttendRow.SlotID != slot || AttendRow.Date.Date != date)
                        {
                            date = AttendRow.Date.Date;
                            slot = AttendRow.SlotID;
                            {
                                string l_colname = AttendRow.Date.Date.ToShortDateString().Substring(0, 5) + " at\r\n" + AttendRow.Time;
                                if (dgv.Columns.Contains(l_colname) == false)
                                {
                                    Column = dgv.Columns.Add(l_colname,
                                                                    l_colname);
                                }
                                else
                                {
                                    Column = dgv.Columns[l_colname].Index;
                                }
                            }
                        }
                        if (AttendRow.AbsORPres == true)
                        {
                            absentorpres = "P";
                        }
                        else
                        {
                            absentorpres = "A";
                            dgv.Rows[row].Cells[Column].Style.BackColor = System.Drawing.Color.Red;
                        }

                        dgv.Rows[row].Cells[Column].Value = absentorpres;

                        if (p_teachersList.ContainsKey(AttendRow.TeacherID) == false)
                        {
                            p_teachersList.Add(AttendRow.TeacherID, null);
                        }
                    }
                }
            }
            
        }

       

        Word._Application p_objApp = null;
        Word._Document p_objDoc = null;

        public void RunWordAppAndOpenDoc(string l_path)
        {
            p_objApp = new Word.Application();
            //p_objApp.Visible = true;
            string l_FilePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (l_path != null && System.IO.File.Exists(l_path))
            {
                p_objDoc = p_objApp.Documents.Open(l_path);
            }
            else
            {
                object objMiss = System.Reflection.Missing.Value;
                p_objDoc = p_objApp.Documents.Add(ref objMiss, ref objMiss,
                    ref objMiss, ref objMiss);
            }
            p_objDoc.PageSetup.LeftMargin = p_objDoc.PageSetup.RightMargin = 10.0F;
            p_objDoc.PageSetup.TopMargin = 10.0F;

            string l_name = "DJWeekly_" + dateTimePicker1.Value.Date.ToShortDateString().Substring(0, 5) + "_" + p_tName + "_" + comboBoxGrade.Text;
            if (comboBoxBatch.Visible)
            {
                l_name += comboBoxBatch.Text;
            }
            l_name = l_name.Replace('.', '_');
            l_name = l_name.Replace(' ', '_');
            l_name += ".docx";
            
            /*DialogResult l_res = System.Windows.Forms.DialogResult.Yes;
            if (System.IO.File.Exists(l_FilePath + "\\" + l_name))
            {
                l_res = System.Windows.Forms.DialogResult.No;
                l_res = MessageBox.Show("Do you want to overwrite the existing file \r\n" + l_name, "Query", MessageBoxButtons.YesNoCancel);
            }
            if (l_res == System.Windows.Forms.DialogResult.No)
            {
                p_objDoc.SaveAs();
            }
            else
            {
                p_objDoc.SaveAs(l_name);
            }*/

        }

        public string CloseSaveWordDoc()
        {
            string l_path = null;
            try
            {
                string l_FilePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                string l_name = "DJWeekly_" + dateTimePicker1.Value.Date.ToShortDateString().Substring(0, 5) + "_" + p_tName + "_" + comboBoxGrade.Text;
                if (comboBoxBatch.Visible)
                {
                    l_name += comboBoxBatch.Text;
                }
                l_name = l_name.Replace('.', '_');
                l_name = l_name.Replace(' ', '_');
                l_name += ".docx";
                DialogResult l_res = System.Windows.Forms.DialogResult.Yes;
                if (System.IO.File.Exists(l_FilePath + "\\" + l_name))
                {
                    l_res = System.Windows.Forms.DialogResult.No;
                    l_res = MessageBox.Show("Do you want to overwrite the existing file \r\n" + l_name, "Query", MessageBoxButtons.YesNoCancel);
                }
                if (l_res == System.Windows.Forms.DialogResult.No)
                {
                    p_objDoc.Save();
                }
                else
                {
                    p_objDoc.SaveAs(l_name);
                }

                //p_objDoc.Words.Last.InsertBreak(Word.WdBreakType.wdPageBreak);

                MessageBox.Show("Saved " + p_objDoc.FullName);
            }
            catch (Exception e)
            {
                MessageBox.Show("Saving Exception occured:" + e.Message);
            }
            finally
            {
                object doNotSaveChanges = Word.WdSaveOptions.wdDoNotSaveChanges; 
                p_objDoc.Close(ref doNotSaveChanges);
                releaseObject(p_objDoc);
            }
            return l_path;
        }

        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Exception Occured while releasing object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }

        public void ExportToWord()//int l_schoolID, int l_gradeID, TablerDBDataSet.AllocationTableDataTable l_allocation)
        {
            object objEndOfDocFlag = "\\endofdoc"; /* \endofdoc is a predefined bookmark */
            p_objDoc.PageSetup.BottomMargin = (float)5;
            p_objDoc.PageSetup.TopMargin = (float)5;

            Word.Section section = p_objDoc.Sections.First;
            Word.Range headerRange = section.Headers[Word.WdHeaderFooterIndex.wdHeaderFooterPrimary].Range;
            headerRange.Fields.Add(headerRange,Type.Missing);// Word.WdFieldType.wdFieldPage);
            headerRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;

            Word.Paragraph objPara1; //define paragraph object
            //object oRng = headerRange.Bookmarks.get_Item(ref objEndOfDocFlag).Range; //go to end of the page
            objPara1 = headerRange.Paragraphs.Add(); //add paragraph at end of document
            objPara1.Format.SpaceAfter = 0; //defind some style

            string l_file = Application.StartupPath + "\\DJSanghviTitle.png";
            if (System.IO.File.Exists(l_file))
            {
                objPara1.Range.InlineShapes.AddPicture(l_file, Type.Missing, true, Type.Missing);
            }

            Word.Paragraph objPara2; //define paragraph object
            //object oRng = headerRange.Bookmarks.get_Item(ref objEndOfDocFlag).Range; //go to end of the page
            objPara2 = headerRange.Paragraphs.Add(); //add paragraph at end of document
            objPara2.LeftIndent = 0.2F;

            //   objPara2.Range.Text = lblTitle.Text; //add some text in paragraph
            objPara2.Range.Bold = 1;
            objPara2.Format.SpaceAfter = 0; //defind some style
            objPara2.Format.SpaceBefore = 0; //defind some style
            //objPara2.Range.InsertParagraphAfter(); //insert paragraph
            objPara2.Range.Bold = 0;
            objPara2.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
            string l_yearstr = "2015 - 2016";
            if (dateTimePicker1.Value.Date.Month <= 6)
            {
                l_yearstr = (dateTimePicker1.Value.Date.Year - 1).ToString() + " - " + dateTimePicker1.Value.Date.Year.ToString();
            }
            else
            {
                l_yearstr = (dateTimePicker1.Value.Date.Year).ToString() + " - " + (dateTimePicker1.Value.Date.Year + 1).ToString();
            }
            objPara2.Range.Text = "Academic Year : " + l_yearstr + " \r\nWeekly Report of Attendance Record of Lecture";
            p_objDoc.GridSpaceBetweenHorizontalLines = 0;
            //Insert a 2 x 2 table, (table with 2 row and 2 column)


            string strText;
            object objMiss = System.Reflection.Missing.Value;

            Word.Paragraph objPara21; //define paragraph object
            //object oRng = headerRange.Bookmarks.get_Item(ref objEndOfDocFlag).Range; //go to end of the page
            objPara21 = headerRange.Paragraphs.Add(); //add paragraph at end of document
            objPara21.LeftIndent = 0.2F;

            //   objPara2.Range.Text = lblTitle.Text; //add some text in paragraph
            objPara21.Range.Bold = 1;
            objPara21.Format.SpaceAfter = 0; //defind some style
            objPara21.Format.SpaceBefore = 0; //defind some style
            //objPara21.Range.InsertParagraphAfter(); //insert paragraph
            objPara21.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;

            
            Word.Range objWordRng = objPara21.Range;

            Word.Table objTab2;
            objTab2 = headerRange.Tables.Add(objWordRng, 5, 2, ref objMiss, ref objMiss);
            
            objTab2.Borders.InsideLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;
            objTab2.Borders.OutsideLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;
            objTab2.Borders.InsideLineWidth = Word.WdLineWidth.wdLineWidth025pt;
            objTab2.Borders.OutsideLineWidth = Word.WdLineWidth.wdLineWidth050pt;
            objTab2.Range.Bold = 1;
            objTab2.Cell(1, 2).Range.Text = "Date From: " + dateTimePicker1.Value.Date.ToShortDateString() + " To: " + dateTimePicker2.Value.ToShortDateString();
            objTab2.Cell(2, 1).Range.Text = "Faculty name:   " + p_tName;
            objTab2.Cell(2, 2).Range.Text = "Subject Name:  "+subject;
            objTab2.Cell(3, 1).Range.Text = "Class:  " + p_grades.FindByGradeID((int)comboBoxGrade.SelectedValue).GradeName;
            objTab2.Cell(4, 1).Range.Text = "Scheduled Lectures:  " + countScheduled;
            objTab2.Cell(4, 2).Range.Text = "Conducted Lectures:  " + (dataGridView1.Columns.Count - 2).ToString();

            objTab2.Range.Paragraphs.SpaceAfter = 0;
           
            Word.Paragraph objPara3; //define paragraph object
            object oRng3 = p_objDoc.Bookmarks.get_Item(ref objEndOfDocFlag).Range; //go to end of the page
            objPara3 = p_objDoc.Content.Paragraphs.Add(ref oRng3); //add paragraph at end of document
            objPara3.LeftIndent = 0.2F;
            //   objPara2.Range.Text = lblTitle.Text; //add some text in paragraph
            objPara3.Range.Bold = 1;
            objPara3.Format.SpaceAfter = 0; //defind some style
            objPara3.Format.SpaceBefore = 0; //defind some style
            //objPara3.Range.InsertParagraphAfter(); //insert paragraph
            objPara3.Range.Bold = 0;
           // objPara3.Range.Text = "ADDED";
            
            Word.Range objWordRng2 = p_objDoc.Bookmarks.get_Item(ref objEndOfDocFlag).Range;

            Word.Table objTab1; //create table object
            objTab1 = p_objDoc.Tables.Add(objWordRng2, dataGridView1.Rows.Count + 1, dataGridView1.Columns.Count, ref objMiss, ref objMiss); //add table object in word document
            
            objTab1.Borders.InsideLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;
            objTab1.Borders.OutsideLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;
            objTab1.Borders.InsideLineWidth = Word.WdLineWidth.wdLineWidth025pt;
            objTab1.Borders.OutsideLineWidth = Word.WdLineWidth.wdLineWidth050pt;
            objTab1.Range.Paragraphs.SpaceAfter = 0;
            objTab1.Range.Paragraphs.SpaceBefore = 0;
            objTab1.Range.Font.Size = 8;
            foreach (DataGridViewColumn l_col in dataGridView1.Columns)
            {
                objTab1.Columns[l_col.Index + 1].Width = p_objApp.InchesToPoints(1.0F);
                if (l_col.Index == 0) continue;

                if (l_col.Index == 2)
                {
                    objTab1.Columns[l_col.Index].Width *= 2; 
                }

                objTab1.Cell(1, l_col.Index + 1).Range.Text = l_col.HeaderText; //add some text to cell
                objTab1.Cell(1, l_col.Index + 1).LeftPadding = 0;
                objTab1.Cell(1, l_col.Index + 1).RightPadding = 0;

            }
            objTab1.Range.Font.Size = 8;
           
            foreach (DataGridViewRow iRow in dataGridView1.Rows)
            {
                objTab1.Rows[iRow.Index + 2].Height = p_objApp.InchesToPoints(0.5F);
                foreach (DataGridViewCell iCols in iRow.Cells)
                {
                    //if (iCols.Value is string)
                    {
                        strText = (string)iCols.Value.ToString();//"row:" + iRow + "col:" + iCols;
                        //int l_slotno = 0;
                        Word.Cell l_cell = objTab1.Cell(iRow.Index + 2, iCols.ColumnIndex + 1);
                        /*if (strText.Length < 3)
                        {
                            if (int.TryParse(strText, out l_slotno))
                            {
                                if ((iRow.Index == p_global.RecessLect1 - 1 || iRow.Index == p_global.RecessLect2 - 1))
                                {
                                    Color c = Color.Gray;
                                    l_cell.Row.SetHeight(20.0F, Word.WdRowHeightRule.wdRowHeightExactly);
                                    var wdc = (Microsoft.Office.Interop.Word.WdColor)(c.R + 0x100 * c.G + 0x10000 * c.B);
                                    l_cell.Range.Shading.BackgroundPatternColor = wdc;
                                }
                                continue;
                            }
                        }*/

                        //if (strText.Length > 2)
                        {
                            l_cell.Range.Text = strText; //add some text to cell
                            if (strText.Equals("A"))
                            {
                                l_cell.Range.Font.Bold = 1;
                                l_cell.Range.Underline = Word.WdUnderline.wdUnderlineThick;
                            }
                            l_cell.VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                            l_cell.LeftPadding = 3;
                            l_cell.TopPadding = 3;
                            l_cell.Row.SetHeight(36.0F, Word.WdRowHeightRule.wdRowHeightExactly);
                        }
                        //Color c = iCols.Style.BackColor;
                        //var wdc = (Microsoft.Office.Interop.Word.WdColor)(c.R + 0x100 * c.G + 0x10000 * c.B);

                        //l_cell.Range.Shading.BackgroundPatternColor = wdc;

                    }
                }
            }

            objTab1.Rows[1].Range.Font.Bold = 1; //make first row of table BOLD
            objTab1.Rows[1].Range.Font.Size = 6; //make first row of table BOLD
           
            objTab1.Columns[1].Width = p_objApp.InchesToPoints(1.2F); //increase first column width

            objTab1.Columns.AutoFit();
            objTab1.Rows.HeightRule = Word.WdRowHeightRule.wdRowHeightAuto;

            
        }

        void ExportToExcelPract()
        {
            object objEndOfDocFlag = "\\endofdoc"; /* \endofdoc is a predefined bookmark */
            p_objDoc.PageSetup.BottomMargin = (float)5;
            p_objDoc.PageSetup.TopMargin = (float)5;

            Word.Section section = p_objDoc.Sections.First;
            Word.Range headerRange = section.Headers[Word.WdHeaderFooterIndex.wdHeaderFooterPrimary].Range;
            headerRange.Fields.Add(headerRange,Type.Missing);// Word.WdFieldType.wdFieldPage);
            headerRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;

            Word.Paragraph objPara1; //define paragraph object
            //object oRng = headerRange.Bookmarks.get_Item(ref objEndOfDocFlag).Range; //go to end of the page
            objPara1 = headerRange.Paragraphs.Add(); //add paragraph at end of document
            objPara1.Format.SpaceAfter = 0; //defind some style

            string l_file = Application.StartupPath + "\\DJSanghviTitle.png";
            if (System.IO.File.Exists(l_file))
            {
                objPara1.Range.InlineShapes.AddPicture(l_file, Type.Missing, true, Type.Missing);
            }

            Word.Paragraph objPara2; //define paragraph object
            //object oRng = headerRange.Bookmarks.get_Item(ref objEndOfDocFlag).Range; //go to end of the page
            objPara2 = headerRange.Paragraphs.Add(); //add paragraph at end of document
            objPara2.LeftIndent = 0.2F;

            //   objPara2.Range.Text = lblTitle.Text; //add some text in paragraph
            objPara2.Range.Bold = 1;
            objPara2.Format.SpaceAfter = 0; //defind some style
            objPara2.Format.SpaceBefore = 0; //defind some style
            //objPara2.Range.InsertParagraphAfter(); //insert paragraph
            objPara2.Range.Bold = 0;
            objPara2.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
            string l_yearstr = "2015 - 2016";
            if (dateTimePicker1.Value.Date.Month <= 6)
            {
                l_yearstr = (dateTimePicker1.Value.Date.Year - 1).ToString() + " - " + dateTimePicker1.Value.Date.Year.ToString();
            }
            else
            {
                l_yearstr = (dateTimePicker1.Value.Date.Year).ToString() + " - " + (dateTimePicker1.Value.Date.Year + 1).ToString();
            }
            objPara2.Range.Text = "Academic Year : " + l_yearstr + " \r\nWeekly Report of Attendance Record of Practicals";
            p_objDoc.GridSpaceBetweenHorizontalLines = 0;
            //Insert a 2 x 2 table, (table with 2 row and 2 column)
            if (p_teachersList.Count > 1)
            {
                p_tName = "";
                foreach (int l_tid in p_teachersList.Keys)
                {
                    TablerDBDataSet.TeachersRow l_trow = p_tTbl.FindByTeachersID(l_tid);
                    if (l_trow != null)
                    {
                        p_tName += l_trow.TeachersName + ", ";
                    }
                }
            }


            string strText;
            object objMiss = System.Reflection.Missing.Value;

            Word.Paragraph objPara21; //define paragraph object
            //object oRng = headerRange.Bookmarks.get_Item(ref objEndOfDocFlag).Range; //go to end of the page
            objPara21 = headerRange.Paragraphs.Add(); //add paragraph at end of document
            objPara21.LeftIndent = 0.2F;

            //   objPara2.Range.Text = lblTitle.Text; //add some text in paragraph
            objPara21.Range.Bold = 1;
            objPara21.Format.SpaceAfter = 0; //defind some style
            objPara21.Format.SpaceBefore = 0; //defind some style
            //objPara21.Range.InsertParagraphAfter(); //insert paragraph
            objPara21.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;

            
            Word.Range objWordRng = objPara21.Range;

            Word.Table objTab2;
            objTab2 = headerRange.Tables.Add(objWordRng, 5, 2, ref objMiss, ref objMiss);
            
            objTab2.Borders.InsideLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;
            objTab2.Borders.OutsideLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;
            objTab2.Borders.InsideLineWidth = Word.WdLineWidth.wdLineWidth025pt;
            objTab2.Borders.OutsideLineWidth = Word.WdLineWidth.wdLineWidth050pt;
            objTab2.Range.Bold = 1;
            objTab2.Cell(1, 2).Range.Text = "Date From: " + dateTimePicker1.Value.Date.ToShortDateString() + " To: " + dateTimePicker2.Value.ToShortDateString();
            objTab2.Cell(2, 1).Range.Text = "Faculty name:   " + p_tName;
            objTab2.Cell(2, 2).Range.Text = "Subject Name:  "+subject;
            objTab2.Cell(3, 1).Range.Text = "Class:  " + p_grades.FindByGradeID((int)comboBoxGrade.SelectedValue).GradeName;
            objTab2.Cell(4, 1).Range.Text = "Scheduled Practicals:  " + countScheduled;
            objTab2.Cell(4, 2).Range.Text = "Conducted Practicals:  " + (dataGridView1.Columns.Count + dataGridView2.ColumnCount + dataGridView3.ColumnCount + dataGridView4.ColumnCount - 4).ToString();
            objTab2.Range.Paragraphs.SpaceAfter = 0;
           
            Word.Paragraph objPara3; //define paragraph object
            object oRng3 = p_objDoc.Bookmarks.get_Item(ref objEndOfDocFlag).Range; //go to end of the page
            objPara3 = p_objDoc.Content.Paragraphs.Add(ref oRng3); //add paragraph at end of document
            objPara3.LeftIndent = 0.2F;
            //   objPara2.Range.Text = lblTitle.Text; //add some text in paragraph
            objPara3.Range.Bold = 1;
            objPara3.Format.SpaceAfter = 0; //defind some style
            objPara3.Format.SpaceBefore = 0; //defind some style
            //objPara3.Range.InsertParagraphAfter(); //insert paragraph
            objPara3.Range.Bold = 0;
           // objPara3.Range.Text = "ADDED";
            
            Word.Range objWordRng2 = p_objDoc.Bookmarks.get_Item(ref objEndOfDocFlag).Range;
            Word.Table objTabMain;
            objTabMain = p_objDoc.Tables.Add(objWordRng2, 1, 4, ref objMiss, ref objMiss); //add table object in word document  
            //objTabMain.Borders.InsideLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleDot;
            //objTabMain.Borders.OutsideLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleDot;
            
            int l_gridviewno = 1;
            foreach (object o in comboBoxBatch.Items)
            {
                DataGridView dgv = dataGridView1;
                switch (l_gridviewno)
                {
                    case 2:
                        dgv = dataGridView2;
                        break;
                    case 3:
                        dgv = dataGridView3;
                        break;
                    case 4:
                        dgv = dataGridView4;
                        break;
                }
               
                objWordRng2.SetRange(objTabMain.Cell(1, l_gridviewno).Range.Start, objTabMain.Cell(1, l_gridviewno).Range.Start);
                Word.Table objTab1;
                objTab1 = objTabMain.Cell(1, l_gridviewno).Tables.Add(objWordRng2, dgv.Rows.Count + 1, dgv.Columns.Count, ref objMiss, ref objMiss); //add table object in word document
                l_gridviewno++;
                try
                {
                    objTab1.Borders.InsideLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;
                    objTab1.Borders.OutsideLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;
                    objTab1.Borders.InsideLineWidth = Word.WdLineWidth.wdLineWidth025pt;
                    objTab1.Borders.OutsideLineWidth = Word.WdLineWidth.wdLineWidth050pt;
                    objTab1.Range.Paragraphs.SpaceAfter = 0;
                    objTab1.Range.Paragraphs.SpaceBefore = 0;
                    objTab1.Range.Font.Size = 8;
                }
                catch { }
                //objTab1.Cell(1, 0).Range.Text = dgv.Columns[0].HeaderText; //add some text to cell
                
                foreach (DataGridViewColumn l_col in dgv.Columns)
                {
                    objTab1.Cell(1, l_col.Index+1).Range.Text = l_col.HeaderText; //add some text to cell
                    objTab1.Cell(1, l_col.Index+1).LeftPadding = 0;
                    objTab1.Cell(1, l_col.Index+1).RightPadding = 0;

                }
                
                objTab1.Range.Font.Size = 8;

                foreach (DataGridViewRow iRow in dgv.Rows)
                {
                    objTab1.Rows[iRow.Index + 2].Height = p_objApp.InchesToPoints(0.5F);
                    foreach (DataGridViewCell iCols in iRow.Cells)
                    {
                        int l_index = iCols.ColumnIndex + 1;
                        if (iCols.Value != null)
                        {
                            strText = (string)iCols.Value.ToString();//"row:" + iRow + "col:" + iCols;
                            //int l_slotno = 0;
                            Word.Cell l_cell = objTab1.Cell(iRow.Index + 2, l_index);
                            

                            //if (strText.Length > 2)
                            {
                                l_cell.Range.Text = strText; //add some text to cell
                                if (strText.Equals("A"))
                                {
                                    l_cell.Range.Font.Bold = 1;
                                    l_cell.Range.Underline = Word.WdUnderline.wdUnderlineThick;
                                }
                                l_cell.VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                                l_cell.LeftPadding = 3;
                                l_cell.TopPadding = 3;
                                l_cell.Row.SetHeight(36.0F, Word.WdRowHeightRule.wdRowHeightExactly);
                            }
                            //Color c = iCols.Style.BackColor;
                            //var wdc = (Microsoft.Office.Interop.Word.WdColor)(c.R + 0x100 * c.G + 0x10000 * c.B);

                            //l_cell.Range.Shading.BackgroundPatternColor = wdc;

                        }
                    }
                }

                objTab1.Rows[1].Range.Font.Bold = 1; //make first row of table BOLD
                objTab1.Rows[1].Range.Font.Size = 6; //make first row of table BOLD

                objTab1.Columns[1].Width = p_objApp.InchesToPoints(1.2F); //increase first column width

                objTab1.Columns.AutoFit();
                objTab1.Rows.HeightRule = Word.WdRowHeightRule.wdRowHeightAuto;
            }

            
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
        //    tbPath.Text = tGlobal.OpenFilePathFromUser();

            Cursor.Current = Cursors.WaitCursor;
            string l_path = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            
            if (dataGridView1.Rows.Count <= 1 || dataGridView1.Columns.Count == 0)
            {
                MessageBox.Show("Data not found, please select appropriate values and click View Attendance");
                return;
            }
            try
            {
                if (comboBoxType.SelectedValue == "Practical")
                {
                    RunWordAppAndOpenDoc(l_path);
                    ExportToExcelPract();
                    CloseSaveWordDoc();
                }
                else
                {
                    RunWordAppAndOpenDoc(l_path);
                    ExportToWord();
                    CloseSaveWordDoc();

                }
            }
            catch (Exception l_e)
            {
                object doNotSaveChanges = Word.WdSaveOptions.wdDoNotSaveChanges; 
                p_objDoc.Close(ref doNotSaveChanges);
                try
                {
                    releaseObject(p_objDoc);
                }
                catch { }
                MessageBox.Show(l_e.Message);
            }
            Cursor.Current = Cursors.Default;

            
        }

        /*private void button3_Click(object sender, EventArgs e)
        {
          //  Cursor.Current = Cursors.WaitCursor;
            //string l_path = null;
        //    ExportToExcel l_exportToExcel = new ExportToExcel();

          //  l_exportToExcel.exportMonthly(dateTimePicker1.Value, l_path, AllocationTable, Subjects, Teachers, p_grades);

            //Cursor.Current = Cursors.Default;
           
        
            MessageBox.Show("Scheduled lectures:"+countScheduled);
            DataSet1.AttendanceRow[] lAttendRows = (DataSet1.AttendanceRow[])Attend.Select(Attend.GradeIDColumn.ColumnName + " = " + comboBoxGrade.SelectedValue
               + " AND " + Attend.DateColumn.ColumnName + " >= '" + dateTimePicker1.Value.Date + "' AND " + Attend.DateColumn.ColumnName + "<='" + dateTimePicker2.Value.Date + "' AND " +
               Attend.PracticalorNOTColumn.ColumnName + " = " + practorNot + " AND " + Attend.TeacherIDColumn.ColumnName + " = " + p_global.TeacherIDSelected,
                           Attend.SlotIDColumn.ColumnName + "," + Attend.DateColumn.ColumnName + "," + Attend.SAPIDColumn.ColumnName);
            
        }*/

        private void comboBoxType_SelectedValueChanged(object sender, EventArgs e)
        {
            if (comboBoxType.SelectedValue == "Practical")
            {
                string l_batch = "";
                if (comboBoxGrade.Text.EndsWith("-A"))
                {
                    l_batch = "A";
                }
                else if (comboBoxGrade.Text.EndsWith("-B"))
                {
                    l_batch = "B";
                }
                string[] batch = { l_batch + "1", l_batch + "2", l_batch + "3", l_batch + "4" };
                comboBoxBatch.DataSource = batch;
                /*
                comboBoxBatch.Visible = true;
                if (((int)comboBoxGrade.SelectedValue % 2) == 0)
                {
                    String[] ifB = { "B1", "B2", "B3", "B4" };
                    comboBoxBatch.DataSource = ifB;
                }
                else
                {
                    String[] ifA = { "A1", "A2", "A3", "A4" };
                    comboBoxBatch.DataSource = ifA;
                }*/
                //label11.Visible = true;
            }
            else
            {
                comboBoxBatch.Visible = false;
                label11.Visible = false;
            }
        }

        private void comboBoxGrade_SelectedValueChanged(object sender, EventArgs e)
        {
            if (comboBoxType.SelectedValue == "Practical")
            {
                //comboBoxBatch.Visible = true;
                NorthernLights.tGlobal l_glbl = NorthernLights.tGlobal.GetInstance(-1);
                comboBoxBatch.DataSource = l_glbl.BatchesList(comboBoxGrade.Text);
                /*if (((int)comboBoxGrade.SelectedValue % 2) == 0)
                {
                    String[] ifB = { "B1", "B2", "B3", "B4" };
                    comboBoxBatch.DataSource = ifB;
                }
                else
                {
                    String[] ifA = { "A1", "A2", "A3", "A4" };
                    comboBoxBatch.DataSource = ifA;
                }*/
                //label11.Visible = true;
            }
        }

     
                    

      /*  private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (tbPath.Text.Trim().Length > 0)
                {
                    RunWordAppAndOpenDoc(tbPath.Text);
                    ExportToWord();
                    CloseSaveWordDoc();
                    MessageBox.Show("This Time Table has been added to the existing selected word document");
                }
                else
                {
                    MessageBox.Show("Please select file to export");
                }
            }
            catch
            {
                MessageBox.Show("Please close the word document you want to add to before proceeding");
            }
        }*/

    }
}
