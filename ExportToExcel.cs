using System;
using System.Collections.Generic;

using System.Text;
using Excel = Microsoft.Office.Interop.Excel;
using System.Windows.Forms;
namespace TeachersAssessment
{
    public class ExportToExcel
    {
        Excel.Application xlApp;
        Excel.Workbook xlWorkBook;
        Excel.Worksheet xlWorkSheet;
        bool isItGrade = false;
        System.Collections.Hashtable p_Abb = new System.Collections.Hashtable();
        DataSet1.AssessmentDataTable AssessmentTable = new DataSet1.AssessmentDataTable();
        System.Data.MPrSQL.MPrSQLDataAdapter AssessDA = MyTableAdapters.AssessTableAdapter();
        tGlobal p_global = tGlobal.GetInstance(-1);

        void cleanUpExcel()
        {
            xlWorkBook.Close(false, null, null);
            xlApp.Quit();
            //releaseObject(xlWorkSheet);
            //releaseObject(xlWorkBook.Sheets);
            releaseObject(xlWorkBook);
            releaseObject(xlApp);

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


        public bool Export(DateTime l_date, string path, TablerDBDataSet.AllocationTableDataTable l_AllocationTable, TablerDBDataSet.SubjectsDataTable l_Subjects,
                                                                                TablerDBDataSet.TeachersDataTable l_Teachers, TablerDBDataSet.GradesDataTable l_grades,
                                                                                TablerDBDataSet.TSFInalDataTable l_tsfinaltbl, string teacherExporting)
        {       
            AssessmentTable.Clear();
            AssessDA.Fill(AssessmentTable);
            bool l_ret = false;
            int line = 1;
            try
            {
               // System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

                Object misValue = System.Reflection.Missing.Value;

                xlApp = new Excel.Application();
                if (path == null)
                {
                    xlApp.Workbooks.Add(misValue);

                }
                else
                {
                    xlApp.Workbooks.Open(path, misValue, misValue, misValue, misValue, misValue, true, misValue, misValue, true, misValue, misValue, misValue, misValue, misValue);

                }
                xlApp.DisplayAlerts = false;
                //.Add(misValue); 
                xlWorkBook = xlApp.Workbooks[1];
                xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.Add();//.get_Item(1);
                xlWorkSheet.Range["A2"].Value = "SVKM'S DWARKADAS J. SANGHVI COLLEGE OF ENGINEERING, MUMBAI";
                xlWorkSheet.Range["A2"].Font.Size = 20;
                xlWorkSheet.Range["A2"].Font.Name = "Mongolian Baiti";
                xlWorkSheet.Range["A2"].Font.Bold = true;
                xlWorkSheet.Range["B3"].Value = "DAILY REPORT ABOUT LECTURES & PRACTICALS CANCELLED/RESCHEDULED";
                xlWorkSheet.Range["B3"].Font.Size = 16;
                xlWorkSheet.Range["B3"].Font.Name = "Mongolian Baiti";
                xlWorkSheet.Range["B3"].Font.Bold = true;
                xlWorkSheet.Range["B4"].Value = "      Department/Section: " + tGlobal.DEPARTMENT + "           Date: " +l_date.DayOfWeek + ", " + l_date.ToShortDateString();

                xlWorkSheet.Range["A7", "M90"].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                xlWorkSheet.Range["A7", "M90"].VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;


                xlWorkSheet.Range["C7"].Value="Scheduled";
                xlWorkSheet.Range["E7"].Value = "Actually Held";
                xlWorkSheet.Range["G7"].Value = "Cancelled";
                xlWorkSheet.Range["I7"].Value = "Rescheduled/Adjusted";

                xlWorkSheet.Range["C7", "D7"].MergeCells = true;
                xlWorkSheet.Range["E7", "F7"].MergeCells = true;
                xlWorkSheet.Range["G7", "H7"].MergeCells = true;
                xlWorkSheet.Range["I7", "J7"].MergeCells = true;

                xlWorkSheet.Range["C8", "D8"].MergeCells = true;
                xlWorkSheet.Range["E8", "F8"].MergeCells = true;
                xlWorkSheet.Range["G8", "H8"].MergeCells = true;
                xlWorkSheet.Range["I8", "J8"].MergeCells = true;

                xlWorkSheet.Range["C9", "D9"].MergeCells = true;
                xlWorkSheet.Range["E9", "F9"].MergeCells = true;
                xlWorkSheet.Range["G9", "H9"].MergeCells = true;
                xlWorkSheet.Range["I9", "J9"].MergeCells = true;

                xlWorkSheet.Range["C7", "J7"].Font.Bold = true;
                

                xlWorkSheet.Range["B8"].Value = "Number of Theory Lectures";
                xlWorkSheet.Range["B9"].Value = "Number of Practical Lectures";
                xlWorkSheet.Range["B8", "B9"].Font.Bold = true;
                xlWorkSheet.Range["B8", "B9"].ColumnWidth = 30;

                xlWorkSheet.Range["A7", "J9"].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                xlWorkSheet.Range["A7", "J9"].Borders.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);

                xlWorkSheet.Range["A12"].Value = "Sr. No.";
                xlWorkSheet.Range["B12"].Value = "Name Of the faculty";
                xlWorkSheet.Range["C12"].Value = "Subject";
                xlWorkSheet.Range["D13"].Value = "Th.";
                xlWorkSheet.Range["E13"].Value = "Pr.";
                xlWorkSheet.Range["F12"].Value = "Time";
                xlWorkSheet.Range["G12"].Value = "Branch";
                xlWorkSheet.Range["H12"].Value = "Div";
                xlWorkSheet.Range["I12"].Value = "Absence of Teacher";
                xlWorkSheet.Range["I12"].WrapText = true;
                xlWorkSheet.Range["J12"].Value = "Poor Attendance";
                xlWorkSheet.Range["J12"].WrapText = true;
                xlWorkSheet.Range["K12"].Value = "Reason";
                xlWorkSheet.Range["L12"].Value = "Remark";

                xlWorkSheet.Range["F12"].ColumnWidth = 16;
                xlWorkSheet.Range["I12"].ColumnWidth = 13;
                xlWorkSheet.Range["J12"].ColumnWidth = 13;
                xlWorkSheet.Range["K12"].ColumnWidth = 13;
                xlWorkSheet.Range["K12"].ColumnWidth = 20;
                xlWorkSheet.Range["L12"].ColumnWidth = 20;
                

                xlWorkSheet.Range["B12", "M13"].Font.Bold = true;

                xlWorkSheet.Range["C12", "E12"].MergeCells = true;
                xlWorkSheet.Range["A12", "A13"].MergeCells = true;
                xlWorkSheet.Range["B12", "B13"].MergeCells = true;
                xlWorkSheet.Range["F12", "F13"].MergeCells = true;
                xlWorkSheet.Range["G12", "G13"].MergeCells = true;
                xlWorkSheet.Range["H12", "H13"].MergeCells = true;
                xlWorkSheet.Range["I12", "I13"].MergeCells = true;
                xlWorkSheet.Range["J12", "J13"].MergeCells = true;
                xlWorkSheet.Range["K12", "K13"].MergeCells = true;
                xlWorkSheet.Range["L12", "L13"].MergeCells = true;

                xlWorkSheet.Range["A12", "L13"].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                xlWorkSheet.Range["A12", "L13"].Borders.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);

                xlWorkSheet.Range["A13"].RowHeight = 40;
                int l_serialNum = 1, l_totalLecCount = 0, l_swapCountLec = 0, l_swapCountPract = 0;
                int l_rowNum = 14;

                int a = Convert.ToInt32(l_date.DayOfWeek);
                 
                int col = p_global.NumOfSlotsInADay*(a-1)+1;

                int durationlect = 60;

                int durationpr = 120;
                line = 2;

                TablerDBDataSet.SubjectsRow[] l_subjrows = (TablerDBDataSet.SubjectsRow[])l_Subjects.Select(l_Subjects.SubjectNameColumn.ColumnName + " LIKE '% Tut%' ");

                vIEWDataSet1.TimeTableViewDataTable l_ttv_table = p_global.GetViewTable();

                TablerDBDataSet.AllocationTableRow[] l_allocRowslect = (TablerDBDataSet.AllocationTableRow[])
                                l_AllocationTable.Select(l_AllocationTable.SlotAllotedColumn.ColumnName + " >= " + col + " AND " +l_AllocationTable.SlotAllotedColumn.ColumnName+ " <= " + (col+p_global.NumOfSlotsInADay-1) + " AND " + 
                                l_AllocationTable.DurationColumn.ColumnName + " = " + durationlect.ToString() 
                                + " AND " + l_AllocationTable.GRDIDColumn.ColumnName + " < " + 8 );

                /*TablerDBDataSet.AllocationTableRow[] l_allocRowspract = (TablerDBDataSet.AllocationTableRow[])
                                l_AllocationTable.Select(l_AllocationTable.SlotAllotedColumn.ColumnName + " >= " + col + " AND " +l_AllocationTable.SlotAllotedColumn.ColumnName+ " <= " + (col + p_global.NumOfSlotsInADay-1) + " AND " 
                                + l_AllocationTable.DurationColumn.ColumnName + " = " + durationpr.ToString()
                                + " AND " + l_AllocationTable.GRDIDColumn.ColumnName + " < " + 8);*/

                l_totalLecCount = l_allocRowslect.Length / 2;
                int l_totalPractScehduled = 0, l_totalMathTutSche = 0;
                foreach (TablerDBDataSet.AllocationTableRow l_allocRw in l_AllocationTable.Rows)
                {
                    if (l_allocRw.SlotAlloted >= col && l_allocRw.SlotAlloted <= (col + p_global.NumOfSlotsInADay - 1) && l_allocRw.GRDID < 8)
                    {
                        if (l_allocRw.Duration == durationpr)
                        {
                            l_totalPractScehduled++;
                            continue;
                        }
                        if (l_subjrows.Length > 0 && l_allocRw.SUBJID == l_subjrows[0].SubjectID)
                        {
                            l_totalMathTutSche ++;
                            continue;
                        }
                        if (l_subjrows.Length > 0 && l_allocRw.IsTeachersStrNull() == false && l_allocRw.TeachersStr.Trim().Length > 2)
                        {
                            TablerDBDataSet.TSFInalRow l_tsfrw = l_tsfinaltbl.FindByID(l_allocRw.TSFinalID);
                            if (l_tsfrw != null && l_tsfrw.IsSubjRoomStrNull() == false && l_tsfrw.SubjRoomStr.Contains(l_subjrows[0].SubjectID.ToString() + ":"))
                            {
                                l_totalMathTutSche ++;
                                continue;
                            }
                        }
                    }
                }
                l_totalPractScehduled = l_totalPractScehduled / 4 + l_totalMathTutSche / 2;
                l_totalLecCount = l_totalLecCount - l_totalMathTutSche /2;

                int notconductedlect = 0;
                int notconductedpract = 0;
                //int l_totalPractScehduled = (l_allocRowspract.Length / 4) + l_allocRowsTuts.Length;
 
               
                 DataSet1.AssessmentRow[] l_assrows = (DataSet1.AssessmentRow[])AssessmentTable.Select
                                        (AssessmentTable.DateColumn.ColumnName + " = '"+ l_date.Date.ToString() + "'",
                                            AssessmentTable.GradeIDColumn.ColumnName + "," + AssessmentTable.SlotNumColumn.ColumnName + "," + AssessmentTable.ReasonColumn.ColumnName);

                double l_height = 1;
                    
                for (int i = 0; i < l_assrows.Length; i++)                
                //foreach (DataSet1.AssessmentRow l_assrow in l_assrows)
                {
                    DataSet1.AssessmentRow l_assrow = l_assrows[i];
                    string l_tchrName = "N.A";
                    if (l_assrow.GradeID < 0)
                    {
                        l_tchrName = "All";
                    }
                    string l_remark = "";
                    l_height = 1;
                    if (l_assrow.IsIsConductedNull() == false && l_assrow.IsConducted) continue;

                    TablerDBDataSet.AllocationTableRow[] l_allocRow = (TablerDBDataSet.AllocationTableRow[])
                                l_AllocationTable.Select(l_AllocationTable.GRDIDColumn.ColumnName + " = " + l_assrow.GradeID.ToString()
                           + " AND " + l_AllocationTable.SlotAllotedColumn.ColumnName + " = " + l_assrow.SlotNum.ToString());
                    if (l_assrows.Length <= 0) continue;
                    /*int rowindex = l_assrow.SlotNum % p_global.NumOfSlotsInADay;
                    if (rowindex == 0)
                    {
                        rowindex = 11;
                    }
                    else
                    {
                        rowindex = rowindex - 1;
                    }*/
                    
                    TablerDBDataSet.TeachersRow l_trw = l_Teachers.FindByTeachersID(l_assrow.TeacherID);
                    if (l_trw != null)
                    {
                        l_tchrName = l_trw.TeachersName;
                    }                    
                    l_remark = l_assrow.Remark;
                    if (l_assrow.IsIsPracticalNull() == false && l_assrow.IsPractical)
                    {
                        for (int j = i + 1; j < l_assrows.Length; j++)
                        {
                            if (l_assrows[j].GradeID == l_assrow.GradeID && l_assrows[j].SlotNum == l_assrow.SlotNum && l_assrows[j].Reason.Equals(l_assrow.Reason))
                            {
                                l_trw = l_Teachers.FindByTeachersID(l_assrows[j].TeacherID);
                                if (l_trw != null)
                                {
                                    l_tchrName = l_tchrName + ", " + l_trw.TeachersName;
                                    if (l_remark.Trim().Length > 0)
                                    {
                                        l_remark = l_remark + ", " + l_assrows[j].Remark;
                                    }
                                    else
                                    {
                                        l_remark = l_assrows[j].Remark;
                                    }
                                    l_height = l_height + 0.4;
                                }
                                i = j;
                            }
                            else
                            {
                              
                                break;
                            }
                        }
                    }
                    string time = l_assrow.Time; //l_ttv_table[rowindex].LectureHr.ToString();
                    string l_rowStr = l_rowNum.ToString();
                    xlWorkSheet.Range["F" + l_rowStr].Value = time;
                    xlWorkSheet.Range["G" + l_rowStr].Value = tGlobal.DEPARTMENT;//"COMP";
                    line = 3;
                    if (l_assrow.GradeID > 0)
                    {
                        TablerDBDataSet.GradesRow l_grw = l_grades.FindByGradeID(l_assrow.GradeID);
                        if (l_grw != null)
                        {
                            xlWorkSheet.Range["H" + l_rowStr].Value = l_grw.GradeName;
                        }
                        else
                        {
                            xlWorkSheet.Range["H" + l_rowStr].Value = "NA";
                        }
                    }                       
                    else if (Math.Abs(l_assrow.GradeID) < tGlobal.GRADE_NAME.Length)
                    {
                        xlWorkSheet.Range["H" + l_rowStr].Value = tGlobal.GRADE_NAME[Math.Abs(l_assrow.GradeID)];
                    }
                    xlWorkSheet.Range["A" + l_rowStr].Value = l_serialNum++;
                    line = 4;
                    if (l_allocRow.Length > 0)
                    {
                        if (l_allocRow[0].Duration == 60 && l_assrow.IsPractical==false)
                        {
                            notconductedlect++;
                            xlWorkSheet.Range["D" + l_rowStr].Value = 1;
                            xlWorkSheet.Range["E" + l_rowStr].Value = 0;
                        }
                        if (l_allocRow[0].Duration == 60 && l_assrow.IsPractical == true)
                        {                            
                            xlWorkSheet.Range["D" + l_rowStr].Value = 0;
                            xlWorkSheet.Range["E" + l_rowStr].Value = 1;
                        }
                        if (l_allocRow[0].Duration == 120 && l_assrow.IsPractical == true)
                        {
                            notconductedpract++;
                            xlWorkSheet.Range["E" + l_rowStr].Value = 1;
                            xlWorkSheet.Range["D" + l_rowStr].Value = 0;
                        }
                        if (l_allocRow[0].Duration == 120 && l_assrow.IsPractical == false)
                        {
                            l_totalLecCount++;
                            l_totalPractScehduled--;
                            notconductedlect++;
                            xlWorkSheet.Range["E" + l_rowStr].Value = 0;
                            xlWorkSheet.Range["D" + l_rowStr].Value = 1;
                        }
                    }
                    if (l_assrow.Reason.Contains("Swapped"))
                    {
                        if (l_allocRow.Length > 0)
                        {
                            if (l_allocRow[0].Duration == 60 || l_assrow.IsPractical == false)
                            {
                                l_swapCountLec++;
                                if (l_assrow.IsAdjustedNotconductedNull() == false && l_assrow.AdjustedNotconducted)
                                {
                                    notconductedlect++; 
                                }
                                
                            }
                            else if (l_allocRow[0].Duration == 120)
                            {
                                l_swapCountPract++;
                                if (l_assrow.IsAdjustedNotconductedNull() == false && l_assrow.AdjustedNotconducted)
                                {
                                    notconductedpract++;
                                }
                            }
                        }
                        l_trw = l_Teachers.FindByTeachersID(l_assrow.TeacherIDSwapped);
                        if (l_trw != null)
                        {
                            xlWorkSheet.Range["K" + l_rowStr].Value = "ADJUSTED BY " + l_trw.TeachersName;
                        }
                        else
                        {
                            xlWorkSheet.Range["K" + l_rowStr].Value = "ADJUSTED BY 'not found' ";
                        }
                    }                 
                    else
                    {
                        xlWorkSheet.Range["K" + l_rowStr].Value = l_assrow.Reason;
                    }
                    line = 5;
                    if (l_assrow.Reason.Equals("No Students"))
                    {
                        xlWorkSheet.Range["J" + l_rowStr].Value = "YES";
                        xlWorkSheet.Range["I" + l_rowStr].Value = "NA";
                    }
                    else
                    {
                        xlWorkSheet.Range["J" + l_rowStr].Value = "NA";
                        xlWorkSheet.Range["I" + l_rowStr].Value = "YES";
                    }
                    if (l_assrow.Reason.Contains("No Students"))
                    {
                        xlWorkSheet.Range["J" + l_rowStr].Value = "YES";
                    }
                    if (l_assrow.Reason.Equals("Miscellaneous"))
                    {
                        xlWorkSheet.Range["K" + l_rowStr].Value = l_assrow.Reason;
                        xlWorkSheet.Range["J" + l_rowStr].Value = "YES";
                        xlWorkSheet.Range["I" + l_rowStr].Value = "NA";
                    }
                    xlWorkSheet.Range["L" + l_rowStr].Value = l_remark;
                    xlWorkSheet.Range["B" + l_rowStr].Value = l_tchrName;

                    xlWorkSheet.Range["C" + l_rowStr].Value = "N.A";
                    if (l_assrow.IsSubjectNull() == false && l_assrow.Subject.Trim().Length > 0)
                    {
                        xlWorkSheet.Range["C" + l_rowStr].Value = l_assrow.Subject;
                    }
                    else if (l_allocRow.Length > 0)
                    {

                        TablerDBDataSet.SubjectsRow l_srw = l_Subjects.FindBySubjectID(l_allocRow[0].SUBJID);
                        if (l_srw != null)
                        {
                            xlWorkSheet.Range["C" + l_rowStr].Value = l_srw.SubjectName;
                        }                    
                    }
                    xlWorkSheet.Range["A" +l_rowStr].RowHeight = 30 * l_height;
                    xlWorkSheet.Range["A" + l_rowStr, "L"+l_rowStr].WrapText = true;
                    xlWorkSheet.Range["A" + l_rowStr, "L" + l_rowStr].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    l_rowNum++;
                }
                line = 6;
                notconductedpract = 0;
                string build = "";
                string buildSwapped = "";
                l_height = 1;
                DataSet1.AssessmentRow[] l_assrowsSwapped;
                foreach(TablerDBDataSet.GradesRow l_grdrw in l_grades) // int gradeid = 0; gradeid <= l_grades.Rows.Count; gradeid++)
                {
                    int gradeid = l_grdrw.GradeID;
                    for (int i = col; i <= col + p_global.NumOfSlotsInADay-1; i++)
                    {
                        l_assrows = (DataSet1.AssessmentRow[])AssessmentTable.Select(AssessmentTable.SlotNumColumn.ColumnName + " = " + i + " AND "
                        + AssessmentTable.DateColumn.ColumnName + " = '" + l_date.Date.ToString() + "' AND " + AssessmentTable.IsPracticalColumn.ColumnName +
                             " = " + true + " AND " + AssessmentTable.GradeIDColumn.ColumnName + " = " + gradeid + " AND " + AssessmentTable.IsConductedColumn.ColumnName
                             + " = " + false + " AND " + AssessmentTable.ReasonColumn.ColumnName + " NOT LIKE 'Swapped%'");
                        if (l_assrows.Length >= 4)
                        {
                            notconductedpract++;
                            build = build + " 1(ALL BATCHES) +";
                            l_height++;
                        }
                        if (l_assrows.Length < 4 && l_assrows.Length > 0)
                        {
                            build = build + " 1(" + l_assrows.Length + " BATCHES) +";
                            l_height++;
                        }
                   
                        l_assrowsSwapped = (DataSet1.AssessmentRow[])AssessmentTable.Select(AssessmentTable.SlotNumColumn.ColumnName + " = " + i + " AND "
                        + AssessmentTable.DateColumn.ColumnName + " = '" + l_date.Date.ToString() + "' AND " + AssessmentTable.IsPracticalColumn.ColumnName +
                             " = " + true + " AND " + AssessmentTable.GradeIDColumn.ColumnName + " = " + gradeid + " AND " + AssessmentTable.IsConductedColumn.ColumnName
                             + " = " + false + " AND " + AssessmentTable.ReasonColumn.ColumnName + " LIKE 'Swapped%'");
                        if (l_assrowsSwapped.Length > 4)
                        {
                            l_height++;
                            buildSwapped = buildSwapped + " 1(ALL BATCHES) +";
                        }
                        if (l_assrowsSwapped.Length < 4 && l_assrowsSwapped.Length > 0)
                        {
                            buildSwapped = buildSwapped + " 1(" + l_assrowsSwapped.Length + " BATCHES) +";
                            l_height++;
                        }
                   
                    }
                }
                if (build.Equals(""))
                {
                    build = "0 ";
                }
                if (buildSwapped.Equals(""))
                {
                    buildSwapped = "0 ";
                }
                line = 7;
                /*  int temp=-1;
                  notconductedpract = 0;
                  int temp2=0;
                  temp = l_assrows[0].SlotNum;
                  foreach (DataSet1.AssessmentRow l_assrow in l_assrows)
                  {
                      if (temp == l_assrow.SlotNum)
                      {
                          temp2++;
                      }
                      else
                      {
                          temp = l_assrow.SlotNum;
                      }
                      if(temp2==4)
                      {
                          notconductedpract++;
                          temp2 = 0;
                      }
                      temp = l_assrow.SlotNum;
                  }
                  */
                xlWorkSheet.Range["C9", "D9"].Value = l_totalPractScehduled;
                xlWorkSheet.Range["C8", "D8"].Value = l_totalLecCount;

                xlWorkSheet.Range["E8", "E8"].Value = l_totalLecCount - notconductedlect + l_swapCountLec;
                xlWorkSheet.Range["E9", "E9"].Value = l_totalPractScehduled - notconductedpract;
                xlWorkSheet.Range["G8", "G8"].Value = notconductedlect-l_swapCountLec;
                xlWorkSheet.Range["G9", "G9"].Value = build.Substring(0,build.Length-1);
                xlWorkSheet.Range["G9", "G9"].WrapText = true;
                xlWorkSheet.Range["A9"].RowHeight = Convert.ToDouble(xlWorkSheet.Range["A9", "G9"].RowHeight) * l_height;
                xlWorkSheet.Range["I8", "I8"].Value = l_swapCountLec;
                xlWorkSheet.Range["I9", "I9"].Value = buildSwapped.Substring(0,buildSwapped.Length-1);
                
                l_rowNum = l_rowNum + 2;
                xlWorkSheet.Range["B"+l_rowNum.ToString()].Value = "Signature: " + "______________________";
                xlWorkSheet.Range["I" + l_rowNum.ToString()].Value = "Name and Designation:";
                xlWorkSheet.Range["K" + l_rowNum.ToString()].Value = "";
                xlWorkSheet.Range["K" + l_rowNum.ToString()].Font.Underline = true;

                line = 8;

                path = saveExcelAndCleanUp(ref path);
                l_ret = true;
                
            }

            catch (Exception l_e)
            {
                MessageBox.Show("Error:" + line.ToString() + " ::  " + l_e.Message);
            }
            finally
            {
                //if (p_global.allTeachersExcelExported = true)
                {
                    cleanUpExcel();
                }
            }
            return l_ret;
        }

        public string saveExcelAndCleanUp(ref string path)
        {
            Object misValue = System.Reflection.Missing.Value;
            if (path == null)
            {
                path = tGlobal.SaveFilePathFromUser();
                xlWorkBook.SaveAs(path, Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlNoChange, misValue, misValue, misValue, misValue, misValue);
            }
            else
            {
                xlWorkBook.Save();
            }
            //xlWorkBook.Close(true, misValue, misValue);
            return path;
        }
    //     public exportMontly(

    }
}

