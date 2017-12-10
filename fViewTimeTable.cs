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
    public partial class fViewTimeTable : Form
    {
        TablerDBDataSet.GradesDataTable GRADES;// = new TablerDBDataSet.GradesDataTable();
        TablerDBDataSet.RoomsDataTable ROOMS;
        TablerDBDataSet.TSFInalDataTable TSFinalTbl;// = new TablerDBDataSet.TSFInalDataTable();
        TablerDBDataSet.TeachersDataTable Teachers;
        TablerDBDataSet.SubjectsDataTable Subjects;
        TablerDBDataSet.PreAssignDataTable PreAssign;
        TablerDBDataSet.GradesPreAssignDataTable GradesPreAssign;
        public TablerDBDataSet.AllocationTableDataTable AllocationTable;//= new TablerDBDataSet.AllocationTableDataTable();
        DataSet1.AssessmentDataTable AssessTb = new DataSet1.AssessmentDataTable();
        DataSet1.AttendanceDataTable AttendDT = new DataSet1.AttendanceDataTable();
        tGlobal p_global = tGlobal.GetInstance(-1);

        int TEacherIDOfTimeTableShown = -1;
        string TeacherNameSelected = null;
        bool p_teacherTTSelected = false;
        public bool ISMoved = false;
        System.Collections.Hashtable p_preTSlotsCombos = null;

        public fViewTimeTable(TablerDBDataSet.GradesDataTable l_GRADES, 
                                TablerDBDataSet.RoomsDataTable l_ROOMS,
                                TablerDBDataSet.TSFInalDataTable l_TSFinalTbl,// = new TablerDBDataSet.TSFInalDataTable();
                                TablerDBDataSet.TeachersDataTable l_Teachers,
                                TablerDBDataSet.SubjectsDataTable l_Subjects,
                                TablerDBDataSet.PreAssignDataTable l_PreAssign,
                                TablerDBDataSet.AllocationTableDataTable l_AllocationTable,
                                TablerDBDataSet.GradesPreAssignDataTable l_gradesPreAsssign,
                                System.Collections.Hashtable l_preTSlotsCombos                              
            )
        {
            InitializeComponent();

            GRADES = l_GRADES;
            ROOMS = l_ROOMS;
            TSFinalTbl = l_TSFinalTbl;
            Teachers = l_Teachers;
            Subjects = l_Subjects;
            PreAssign = l_PreAssign;
            
            LoadAssessTable();
            AllocationTable = l_AllocationTable;
            p_preTSlotsCombos = l_preTSlotsCombos;
            GradesPreAssign = l_gradesPreAsssign;
            initializeDataGrid();



            this.Load += new EventHandler(fViewTimeTable_Load);

        }
        DataSet1.AssessmentRow[] AssessRows;
        DataSet1.AttendanceRow[] AttendRows;

        void fViewTimeTable_Load(object sender, EventArgs e)
        {
            fillCellsForPreassignments(p_defaultTeacherID);
            dataGridView1.CellDoubleClick +=new DataGridViewCellEventHandler(dataGridView1_CellDoubleClick);
            dataGridView1.MouseClick +=new MouseEventHandler(dataGridView1_MouseClick);
            this.FormClosing += new FormClosingEventHandler(fViewTimeTable_FormClosing);
        }

        void fViewTimeTable_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (p_global.TeacherIDSelected > 0)
                return;
            foreach (DataGridViewColumn l_dgrcol in dataGridView1.Columns)
            {
                if (l_dgrcol.Index == 0 || l_dgrcol.Visible == false) continue;

                foreach (DataGridViewRow l_dgvrw in dataGridView1.Rows)
                {
                    if ((dataGridView1[l_dgrcol.Index, l_dgvrw.Index].Value != null
                        && ((string)dataGridView1[l_dgrcol.Index, l_dgvrw.Index].Value).Trim().Length > 3)
                        &&
                        (dataGridView1[l_dgrcol.Index, l_dgvrw.Index].Style.BackColor != System.Drawing.Color.LightGreen
                                && dataGridView1[l_dgrcol.Index, l_dgvrw.Index].Style.BackColor != System.Drawing.Color.IndianRed
                                && dataGridView1[l_dgrcol.Index, l_dgvrw.Index].Style.BackColor != System.Drawing.Color.Orange))
                    {
                        DialogResult l_rslt = MessageBox.Show("Please either mark All classes as conducted or edit teacher assessment records"
                                            + ", some classes may not be marked. \r\n\r\nClick yes to continue marking and entering data.", "Query", MessageBoxButtons.YesNoCancel );

                        if (l_rslt != System.Windows.Forms.DialogResult.No)
                        {
                            e.Cancel = true;
                            return;
                        }
                        l_rslt = System.Windows.Forms.DialogResult.No;
                        l_rslt = MessageBox.Show("     Do you want to quit ?"
                                            , "Query", MessageBoxButtons.YesNoCancel);

                        if (l_rslt != System.Windows.Forms.DialogResult.Yes)
                        {
                            e.Cancel = true;
                            return;
                        }
                        else
                        {
                            return;
                        }
                    }
                }
            }
        }

        void LoadAssessTable()
        {
            if (p_global.TeacherIDSelected <= 0)
            {
                AssessTb.Clear();
                System.Data.MPrSQL.MPrSQLDataAdapter AssessDA = MyTableAdapters.AssessTableAdapter();
                AssessDA.Fill(AssessTb);
            }
            else
            {
                AttendDT.Clear();
                System.Data.MPrSQL.MPrSQLDataAdapter AttendDA = MyTableAdapters.AttendanceTableAdapter();
                AttendDA.Fill(AttendDT);
            }
        }

        void initializeDataGrid()
        {
            dataGridView1.RowsDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            //dataGridView1.RowTemplate.MinimumHeight = 48;
            dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            //dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.AllowDrop = true;
            //dataGridView1.MouseDown += new MouseEventHandler(dataGridView1_MouseDown);
            //this.dataGridView1.DragDrop += new System.Windows.Forms.DragEventHandler(this.dataGridView1_DragDrop);
            //this.dataGridView1.DragEnter += new System.Windows.Forms.DragEventHandler(this.dataGridView1_DragEnter);
            this.dataGridView1.CellFormatting += new DataGridViewCellFormattingEventHandler(dataGridView1_CellFormatting);
            
            dataGridView1.ColumnAdded += new DataGridViewColumnEventHandler(dataGridView1_ColumnAdded);
            
            dataGridView1.ShowCellToolTips = true;
            dataGridView1.CellToolTipTextNeeded += new DataGridViewCellToolTipTextNeededEventHandler(dataGridView1_CellToolTipTextNeeded);
            //dataGridView1.MouseClick += new MouseEventHandler(dataGridView1_MouseClick);
            //dataGridView1.MouseDoubleClick +=new MouseEventHandler(dataGridView1_MouseDoubleClick);
            if (p_global.TeacherIDSelected <= 0)
            {
                label2.Text = "1. Please double click on a slot to indicate that lecture has been conducted. Box should turn green.";
                label4.Text = "2. If it is a practical, double clicking would mean that all 4 batches' practical were conducted.";
                label3.Text = "3. Please right click and select 'Edit slot for selected teacher' to give reason for lecture not being conducted.";
                label5.Text = "4. If all 4 batches of practicals are not conducted the slot will turn red, or else it will be orange(1-3 batches conducted) or green(all conducted).";
                label6.Text = "5. To see which teachers are scheduled to conduct a practical or elective subject lecture, please hover over the practical slot and you will see the teachers listed in the form of a tool tip.";
                label7.Text = "6. If there is any problem with class time table, please inform Khushali Ma'am or Dharmesh Sir.";
            }
            else
            {
                label2.Text = "1. Please double click on a slot to give attendance for that time and grade.";
                label4.Text = "2. You can click on any slot, even those in which you are not assigned to teach in, this is if you have swapped lecture with someone else.";
                label3.Text = "3. You can change grade later if need be.";
                label5.Text = "4. Slots for which attendance is given will turn green.";
                label6.Text = "5. Re-clicking a slot will show you the attendance as per what you have given for that slot and that date.";
                label7.Text = "6. If there is any problem with your time table, please inform Khushali Ma'am or Lakshmi Ma'am";
            }
                                  
        }

        
        void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                try
                {
                    DataGridView.HitTestInfo info = dataGridView1.HitTest(e.X, e.Y);
                    dataGridView1.CurrentCell = dataGridView1[info.ColumnIndex, info.RowIndex];
                    if (dataGridView1[info.ColumnIndex, info.RowIndex].Value.ToString().StartsWith("~"))
                    {
                        MessageBox.Show("Please right click on header/title of the lecture or pratical slot");
                        return;
                    }
                }
                catch
                {
                    dataGridView1.CurrentCell = null;
                }
                
                ContextMenu lm = new ContextMenu();
                if (p_global.TeacherIDSelected <= 0)
                {
                    lm.MenuItems.Add("Add/Edit Teacher Attendance Record", addTeacherAbsencesRec);
                    lm.MenuItems.Add("Mark Class as Conducted", addClassConductedRecord);
                    lm.Show(dataGridView1, new Point(e.X, e.Y));
                }               

            }


        }

       
        public List<int> UAllocateThis = new List<int>();
        bool p_remassoc_msg = true;
        void unalloc(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentCell == null) return;
            int l_slot = ((dataGridView1.CurrentCell.ColumnIndex - 1) * p_global.NumOfSlotsInADay + dataGridView1.CurrentCell.RowIndex) + 1;
            if (TEacherIDOfTimeTableShown > 0) //that mean teacher time table is shown
            {
           
                //get the row to find which grade it is moving from//it has to move to same grade
                TablerDBDataSet.AllocationTableRow[] l_alrwsForThisTeacherANDthisSlotFrom = (TablerDBDataSet.AllocationTableRow[])
                                                AllocationTable.Select("(" + AllocationTable.TeacherIDColumn.ColumnName + " = " + TEacherIDOfTimeTableShown + " OR " +
                                                        AllocationTable.TeachersStrColumn.ColumnName + " like '%" + TEacherIDOfTimeTableShown.ToString() + ",%')"
                                                                    + " AND " + AllocationTable.SlotAllotedColumn.ColumnName + " = " + l_slot);
                foreach (TablerDBDataSet.AllocationTableRow l_alrwT in l_alrwsForThisTeacherANDthisSlotFrom)
                {
                    if (l_alrwT.TeacherID == TEacherIDOfTimeTableShown ||
                            (l_alrwT.IsTeachersStrNull() == false &&
                            (l_alrwT.TeachersStr.StartsWith(TEacherIDOfTimeTableShown.ToString() + ",") || l_alrwT.TeachersStr.Contains("," + TEacherIDOfTimeTableShown.ToString() + ",")))
                        )
                    {
                        UAllocateThis.Add(l_alrwT.ID);
                        dataGridView1.CurrentCell.Value = "";
                        if (l_alrwT.Duration > p_global.lect_min && p_remassoc_msg)
                        {
                            p_remassoc_msg = false;
                           MessageBox.Show("When you unallocate practical slot, please unallocate all the associated slots as well");
                        }
                        break;
                    }
                }
            }
            else if (p_gradeid > 0)
            {
                TablerDBDataSet.AllocationTableRow[] l_alrws = (TablerDBDataSet.AllocationTableRow[])
                                              AllocationTable.Select( AllocationTable.GRDIDColumn.ColumnName + " = " + p_gradeid + " AND " + AllocationTable.SlotAllotedColumn.ColumnName + " = " + l_slot);
                if (l_alrws.Length > 0)
                {
                    UAllocateThis.Add(l_alrws[0].ID);
                    dataGridView1.CurrentCell.Value = "";
                    if (l_alrws[0].Duration > p_global.lect_min && p_remassoc_msg)
                    {
                        p_remassoc_msg = false;
                        MessageBox.Show("When you unallocate practical slot, please unallocate all the associated slots as well");
                    }
                }
            }
            else if (p_roomID > 0)
            {
                TablerDBDataSet.AllocationTableRow[] l_alrws = (TablerDBDataSet.AllocationTableRow[])
                                              AllocationTable.Select( AllocationTable.RoomIDColumn.ColumnName + " = " + p_roomID + " AND " + AllocationTable.SlotAllotedColumn.ColumnName + " = " + l_slot);
                if (l_alrws.Length > 0)
                {
                    UAllocateThis.Add(l_alrws[0].ID);
                    dataGridView1.CurrentCell.Value = "";
                    if (l_alrws[0].Duration > p_global.lect_min && p_remassoc_msg)
                    {
                        p_remassoc_msg = false;
                        MessageBox.Show("When you unallocate practical slot, please unallocate all the associated slots as well");
                    }
                }
            }
            
        }

        void dataGridView1_CellToolTipTextNeeded(object sender, DataGridViewCellToolTipTextNeededEventArgs e)
        {
            string l_key = e.RowIndex.ToString() + ":" + e.ColumnIndex.ToString();
            if (p_htblTsfinalISInView.ContainsKey(l_key) == false) return;
            int l_tsfinalID = (int)p_htblTsfinalISInView[l_key];
            e.ToolTipText = getMultiTeacherNames(l_tsfinalID);
        }

        string getMultiTeacherNames(int l_tsfinalID)
        {
            string l_names = "";
            TablerDBDataSet.TSFInalRow l_tsfinalrow = TSFinalTbl.FindByID(l_tsfinalID);
            if (l_tsfinalrow != null && l_tsfinalrow.IsTeachersStrNull() == false)
            {
                string[] l_strs = l_tsfinalrow.TeachersStr.Split(',');
                foreach (string l_str in l_strs)
                {
                    int l_id = 0;
                    int.TryParse(l_str, out l_id);
                    if (l_id != 0)
                    {
                        TablerDBDataSet.TeachersRow l_trw = Teachers.FindByTeachersID(l_id);
                        l_names += l_trw.TeachersName + ", ";
                    }
                }             
            }
            return l_names;
        }


        void dataGridView1_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            e.Column.SortMode = DataGridViewColumnSortMode.NotSortable;
            e.Column.MinimumWidth = 120;
        }

        void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            dataGridView1.Columns[0].DefaultCellStyle.BackColor = System.Drawing.SystemColors.Control;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font(this.Font.FontFamily, 14.0F);
            //if (e.RowIndex == tGlobal.RecessLect1-1 || e.RowIndex == tGlobal.RecessLect2-1)
            //{
            //    dataGridView1[e.ColumnIndex, e.RowIndex].Style.BackColor = System.Drawing.Color.DarkGray;
            //    return;
            //}
            if (dataGridView1[e.ColumnIndex, e.RowIndex].Value.ToString().StartsWith("{"))
            {
                return;
            }
            if (e.RowIndex > 0 && dataGridView1[e.ColumnIndex, e.RowIndex].Value.ToString().StartsWith("~"))
            {
                dataGridView1[e.ColumnIndex, e.RowIndex].Style.BackColor = dataGridView1[e.ColumnIndex, e.RowIndex - 1].Style.BackColor;
                return;
            }
            string l_key = e.RowIndex.ToString() + ":" + e.ColumnIndex.ToString();
            
            TablerDBDataSet.TSFInalRow l_tsfinalrow = null;
            if (p_htblTsfinalISInView.ContainsKey(l_key))
            {
                int l_tsfinalID = (int)p_htblTsfinalISInView[l_key];
                l_tsfinalrow = TSFinalTbl.FindByID(l_tsfinalID);
            }

             int slotnumber = (e.ColumnIndex - 1) * p_global.NumOfSlotsInADay + e.RowIndex + 1;
            
             /*TablerDBDataSet.AllocationTableRow[] allocrows = (TablerDBDataSet.AllocationTableRow[])AllocationTable.Select(AllocationTable.SlotAllotedColumn.ColumnName
                 + " = " + slotnumber + " AND " + AllocationTable.GRDIDColumn.ColumnName + " = " + p_gradeid);
             TablerDBDataSet.AllocationTableRow[] allocrows1 = (TablerDBDataSet.AllocationTableRow[])AllocationTable.Select(AllocationTable.SlotAllotedColumn.ColumnName
                 + " = " + (slotnumber-1) + " AND " + AllocationTable.GRDIDColumn.ColumnName + " = " + p_gradeid);*/
          
            if (p_global.TeacherIDSelected <= 0)
            {
                if (AssessRows.Length > 0 && e.ColumnIndex > 0 && dataGridView1.Columns[e.ColumnIndex].Visible)
                {
                    int l_slotCount = 0, l_slotCountNotConducted = 0;
                    foreach (DataSet1.AssessmentRow Assrw in AssessRows)
                    {
                        if ((Assrw.SlotNum == slotnumber)) // || (Assrw.IsPractical && Assrw.SlotNum == (slotnumber - 1) && allocrows.Length > 0 && allocrows1[0].Du)))
                        {
                            if (Assrw.IsIsConductedNull() == false && Assrw.IsConducted)
                            {
                                l_slotCount++;
                                //dataGridView1[e.ColumnIndex, e.RowIndex].Style.SelectionBackColor = System.Drawing.Color.LightGreen;
                            }
                            else
                            {
                                l_slotCountNotConducted++;
                                //dataGridView1[e.ColumnIndex, e.RowIndex].Style.SelectionBackColor = System.Drawing.Color.IndianRed;
                            }
                        }
                    }
                    if ((l_slotCount + l_slotCountNotConducted) > 0)
                    {
                        if (l_slotCountNotConducted > 0)
                        {
                            if (l_tsfinalrow != null && l_tsfinalrow.IsTeachersStrNull() == false)
                            {
                                string[] l_tchrs = l_tsfinalrow.TeachersStr.Split(',');
                                if (l_tchrs.Length - 1 > l_slotCountNotConducted)
                                {
                                    dataGridView1[e.ColumnIndex, e.RowIndex].Style.BackColor = System.Drawing.Color.Orange;
                                }
                                else
                                {
                                    dataGridView1[e.ColumnIndex, e.RowIndex].Style.BackColor = System.Drawing.Color.IndianRed;
                                }
                            }
                            else
                            {
                                dataGridView1[e.ColumnIndex, e.RowIndex].Style.BackColor = System.Drawing.Color.IndianRed;
                            }
                        }
                        else
                        {
                            dataGridView1[e.ColumnIndex, e.RowIndex].Style.BackColor = System.Drawing.Color.LightGreen;
                        }

                        return;
                    }

                }
                else
                {
                    if (dataGridView1[e.ColumnIndex, e.RowIndex].Value is string && dataGridView1[e.ColumnIndex, e.RowIndex].Value.ToString().StartsWith("~"))
                    {
                        dataGridView1[e.ColumnIndex, e.RowIndex].Style.BackColor = dataGridView1[e.ColumnIndex, e.RowIndex - 1].Style.BackColor;
                    }
                }
            }
            else
            {
                if (AttendRows.Length > 0)
                {
                    foreach (DataSet1.AttendanceRow Attrw in AttendRows)
                    {
                        if (((Attrw.IsPracticalorNOTNull() || Attrw.PracticalorNOT == false) && 
                                    (Attrw.SlotID == slotnumber || Attrw.SlotID+1 == slotnumber))  
                          || (Attrw.PracticalorNOT && (Attrw.SlotID <= slotnumber && Attrw.SlotID+3 >= slotnumber)))
                        {
                            dataGridView1[e.ColumnIndex, e.RowIndex].Style.BackColor = System.Drawing.Color.LightGreen;
                            if (dataGridView1[e.ColumnIndex, e.RowIndex].Value == null ||
                                    dataGridView1[e.ColumnIndex, e.RowIndex].Value.ToString().Trim().Length <= 3)
                                dataGridView1[e.ColumnIndex, e.RowIndex].Value = Attrw.GradeNAME + "{" + Attrw.SubjectName + "}";
                            return; 
                        }
                    }
                }
                
            }

            if (l_tsfinalrow != null)
            {
                /*TablerDBDataSet.AllocationTableRow[] l_alcrws = (TablerDBDataSet.AllocationTableRow[])
                                                            AllocationTable.Select(AllocationTable.TSFinalIDColumn.ColumnName + " = " + l_tsfinalID);
                if (l_alcrws.Length > 0)
                {
                    TablerDBDataSet.PreAssignRow[] l_prrws = (TablerDBDataSet.PreAssignRow[]) PreAssign.Select(PreAssign.SlotNoColumn.ColumnName + " = " + l_alcrws[0].SlotAlloted);
                    if (l_prrws.Length > 0)
                    {
                        dataGridView1[e.ColumnIndex, e.RowIndex].Style.BackColor = System.Drawing.Color.FromArgb(l_prrws[0].ColortInt);
                        dataGridView1[e.ColumnIndex, e.RowIndex].Value = l_prrws[0].SlotDescr;
                    }
                }*/
                TablerDBDataSet.SubjectsRow l_subrw = Subjects.FindBySubjectID(l_tsfinalrow.SubjectsID);

                if (l_subrw != null)
                {
                    // if(l_subrw.IsColorIntNull() == false && 
                       

                    if (l_subrw.IsColorIntNull() == false)
                    {
                        dataGridView1[e.ColumnIndex, e.RowIndex].Style.BackColor = System.Drawing.Color.FromArgb(l_subrw.ColorInt);
                            
                    }                      
                    else
                    {
                        dataGridView1[e.ColumnIndex, e.RowIndex].Style.BackColor = System.Drawing.Color.White;                       
                    }

                    return;
                }                
            }
            else
            {
                if (e.RowIndex == p_global.RecessLect1-1 || e.RowIndex == p_global.RecessLect2-1)
                {
                    dataGridView1[e.ColumnIndex, e.RowIndex].Style.BackColor = System.Drawing.Color.DarkGray;
                    dataGridView1.Rows[e.RowIndex].MinimumHeight = dataGridView1.Rows[e.RowIndex].Height = 10;
                }
                else
                {
                    dataGridView1[e.ColumnIndex, e.RowIndex].Style.BackColor = System.Drawing.Color.Gainsboro;
                }
            }
        }

        int p_defaultTeacherID = -1;
        

        void fillCellsForPreassignments(int l_teacherID)
        {
            if (PreAssign == null) return;

            p_defaultTeacherID = l_teacherID;
            
            foreach (TablerDBDataSet.PreAssignRow l_prvrw in PreAssign.Rows)
            {
                if (l_prvrw.IsSlotNumsNull()) continue;
                string[] l_slotNums = l_prvrw.SlotNums.Split(',');
                foreach (string l_Str in l_slotNums)
                {
                    int l_SlotNo = -1;
                    int.TryParse(l_Str, out l_SlotNo);
                    if (l_SlotNo < 0 || l_SlotNo > p_global.NumOfSlotsInWeek) continue;

                    int l_rowno = l_SlotNo % p_global.NumOfSlotsInADay;

                    int l_colno = l_SlotNo / p_global.NumOfSlotsInADay + 1;
                    if (l_SlotNo >= p_global.NumOfSlotsInADay && l_rowno == 0)
                    {
                        l_rowno = p_global.NumOfSlotsInADay - 1;
                        l_colno = l_colno - 1;
                    }
                    else
                    {
                        l_rowno--;
                    }
                    if (l_teacherID == -99999 ||
                        (p_preTSlotsCombos.ContainsKey(l_teacherID.ToString() + ":" + l_SlotNo.ToString())
                            || p_preTSlotsCombos.ContainsKey("-1:" + l_SlotNo.ToString())))
                    {
                        if (l_colno > 0 && l_rowno >= 0 && l_colno < dataGridView1.Columns.Count && l_rowno < dataGridView1.Rows.Count)
                        {
                            if (l_prvrw.IsSlotDescrNull()) l_prvrw.SlotDescr = "Pre Assigned";
                            if ( dataGridView1[l_colno, l_rowno].Value is string && 
                                   dataGridView1[l_colno, l_rowno].Value.ToString().StartsWith("{"))
                            {
                                dataGridView1[l_colno, l_rowno].Value += "; {" + l_prvrw.SlotDescr + "}";
                            }
                            else
                            {
                                dataGridView1[l_colno, l_rowno].Value = "{" + l_prvrw.SlotDescr + "}";
                            }
                            if (l_prvrw.IsColortIntNull() == false)
                            {
                                dataGridView1[l_colno, l_rowno].Style.BackColor = System.Drawing.Color.FromArgb(l_prvrw.ColortInt);                                
                            }
                            dataGridView1.InvalidateCell(l_colno, l_rowno);
                        }
                    }
                }

            }
        }
        
        void fillCellsForGradePreassignments(int l_GradeID)
        {
            if (GradesPreAssign == null) return;


            foreach (TablerDBDataSet.GradesPreAssignRow l_gprvrw in GradesPreAssign.Rows)
            {
                if (l_gprvrw.IsSlotNumsNull()) continue;
                if (l_gprvrw.GradeID != l_GradeID) continue;
                string[] l_slotNums = l_gprvrw.SlotNums.Split(',');
                foreach (string l_Str in l_slotNums)
                {
                    int l_SlotNo = -1;
                    int.TryParse(l_Str, out l_SlotNo);
                    if (l_SlotNo < 0 || l_SlotNo > p_global.NumOfSlotsInWeek) continue;

                    int l_rowno = l_SlotNo % p_global.NumOfSlotsInADay;

                    int l_colno = l_SlotNo / p_global.NumOfSlotsInADay + 1;
                    if (l_SlotNo >= p_global.NumOfSlotsInADay && l_rowno == 0)
                    {
                        l_rowno = p_global.NumOfSlotsInADay - 1;
                        l_colno = l_colno - 1;
                    }
                    else
                    {
                        l_rowno--;
                    }
                    if (l_colno > 0 && l_rowno >= 0 && l_colno < dataGridView1.Columns.Count && l_rowno < dataGridView1.Rows.Count)
                    {
                        if (l_gprvrw.IsSlotDescrNull()) l_gprvrw.SlotDescr = "Blocked";
                        if (dataGridView1[l_colno, l_rowno].Value is string &&
                                dataGridView1[l_colno, l_rowno].Value.ToString().StartsWith("{"))
                        {
                            dataGridView1[l_colno, l_rowno].Value += "; {" + l_gprvrw.SlotDescr + "}";
                        }
                        else
                        {
                            dataGridView1[l_colno, l_rowno].Value = "{" + l_gprvrw.SlotDescr + "}";
                        }
                        dataGridView1.InvalidateCell(l_colno, l_rowno);
                    }
                }
            }
        }

        int p_gradeid = -1, p_schoolid =-1, p_teacherID =-1, p_roomID= -1;
        DateTime l_d;

        public void SetGrade(int l_gradeid, string gradename, DateTime l_dt)
        {
            l_d = l_dt;
            p_gradeid = l_gradeid;
            lblTitle.Text = "Time Table: " + gradename + "    " + l_d.DayOfWeek.ToString() + " " + l_d.ToShortDateString();

            vIEWDataSet1.TimeTableViewDataTable l_ttv_table = p_global.GetViewTable();////

            dataGridView1.DataSource = l_ttv_table;
            //System.DateTime m = DateTime.Today;


            DayOfWeek l_dow = l_dt.DayOfWeek;
            int l_colWidth = 0;

            foreach (DataGridViewColumn l_col in dataGridView1.Columns)
            {
                if (l_col.HeaderText == l_dow.ToString() || l_col.Index == 0)
                    continue;
                else
                    l_col.Visible = false;

                l_colWidth += l_col.Width;
            }
            this.Width = l_colWidth + 300;
            fillCellsForPreassignments(-1);

            AssessRows = (DataSet1.AssessmentRow[])AssessTb.Select(AssessTb.GradeIDColumn.ColumnName + " = " + p_gradeid + " AND " + AssessTb.DateColumn.ColumnName
                      + " = '" + l_d.Date.ToString() + "'");
        }

        public void FillTimeTableTblandBindWithGridView(int l_schoolid, int gradeid, string gradename, DateTime l_dt)
        {
            l_d = l_dt;
            p_gradeid = gradeid;
            p_schoolid = l_schoolid;

            lblTitle.Text = "Time Table: " + gradename + "    " + l_d.DayOfWeek.ToString() +  " " + l_d.ToShortDateString();

            p_htblTsfinalISInView.Clear();
            TEacherIDOfTimeTableShown = -1;
            p_teacherTTSelected = false;

            vIEWDataSet1.TimeTableViewDataTable l_ttv_table = null;
            try
            {
                l_ttv_table = p_global.GetViewTable();////
            }
            catch
            {
            }
            if (l_ttv_table == null)
            {
                MessageBox.Show("Incomplete time table data, please enter appropriate data");
                return;
            }
            


            TablerDBDataSet.AllocationTableRow[] l_allocrows = (TablerDBDataSet.AllocationTableRow[])AllocationTable.Select(AllocationTable.GRDIDColumn.ColumnName + " = " + gradeid);
            int l_prevTsFinalID = -1, l_prevSlot = -1;

            foreach (TablerDBDataSet.AllocationTableRow l_alrw in l_allocrows)
            {
                if (l_alrw.SlotAlloted < 0) continue;

                int l_rowno = l_alrw.SlotAlloted % p_global.NumOfSlotsInADay;

                int l_colno = l_alrw.SlotAlloted / p_global.NumOfSlotsInADay + 1;
                if (l_alrw.SlotAlloted >= p_global.NumOfSlotsInADay && l_rowno == 0)
                {
                    l_rowno = p_global.NumOfSlotsInADay - 1;
                    l_colno = l_colno - 1;
                }
                else
                {
                    l_rowno--;
                }
                TablerDBDataSet.SubjectsRow l_subrw = Subjects.FindBySubjectID(l_alrw.SUBJID);
                TablerDBDataSet.TeachersRow l_trw = Teachers.FindByTeachersID(l_alrw.TeacherID);
                if (l_prevTsFinalID != l_alrw.TSFinalID || l_prevSlot + 1 != l_alrw.SlotAlloted)
                {
                    TablerDBDataSet.RoomsRow l_roomRow = null;
                    string l_displStr = tGlobal.TeacherNameForMulti(null, l_alrw.TSFinalID, TSFinalTbl, Teachers, Subjects, ROOMS);
                    if (l_displStr == null || l_displStr.Length == 0)
                    {
                        l_displStr = l_subrw.SubjectName + "/ " + l_trw.TeachersName;
                        if (l_alrw.IsRoomIDNull() == false)
                        {
                            l_roomRow = ROOMS.FindByRoomsID(l_alrw.RoomID);
                            if (l_roomRow != null)
                            {
                                l_displStr += "{" + l_roomRow.RoomsName + "}";
                            }
                        }
                    }
                    l_ttv_table[l_rowno][l_colno] = l_displStr;
                }
                else
                {
                    l_ttv_table[l_rowno][l_colno] = "~";
                }


                l_prevTsFinalID = l_alrw.TSFinalID;
                l_prevSlot = l_alrw.SlotAlloted;


                if (p_htblTsfinalISInView.ContainsKey(l_rowno.ToString() + ":" + l_colno.ToString()) == false)
                {
                    p_htblTsfinalISInView.Add(l_rowno.ToString() + ":" + l_colno.ToString(), l_alrw.TSFinalID);
                }
                else
                {
                    MessageBox.Show("Repeated, " + l_trw.TeachersName + " for " + l_subrw.SubjectName);
                }
            }

            //TablerDBDataSet.GradesRow l_grw = GRADES.FindByGradeID(gradeid);
            //lblTitle.Text = "Time Table for Grade:   " + l_grw.GradeName;


            dataGridView1.DataSource = l_ttv_table;
            //System.DateTime m = DateTime.Today;
            
            
           DayOfWeek l_dow = l_dt.DayOfWeek;
           int l_colWidth = 0;
           
            foreach (DataGridViewColumn l_col in dataGridView1.Columns)
            {
                if (l_col.HeaderText == l_dow.ToString()|| l_col.Index==0)
                    continue;
                else
                    l_col.Visible = false;
                
                l_colWidth += l_col.Width;
            }
            this.Width = l_colWidth + 300;
            fillCellsForPreassignments(-1);

            AssessRows = (DataSet1.AssessmentRow[])AssessTb.Select(AssessTb.GradeIDColumn.ColumnName + " = " + p_gradeid + " AND " + AssessTb.DateColumn.ColumnName
                      + " = '" + l_d.Date.ToString() + "'");
        }

        System.Collections.Hashtable p_htblTsfinalISInView = new System.Collections.Hashtable();

        public void FillTimeTableTblandBindWithGridViewForTeac(int schoolid, int teachid, string teachername, DateTime l_dt,int gradeid)
        {
            p_gradeid = gradeid;
            p_teacherID = teachid;
            p_schoolid = schoolid;
            l_d = l_dt;
            TEacherIDOfTimeTableShown = teachid;
            TeacherNameSelected = teachername;
              vIEWDataSet1.TimeTableViewDataTable l_ttv_table = p_global.GetViewTable();////
            
            lblTitle.Text = "Time Table: " + teachername + ", Date: " + l_dt.ToShortDateString();
            p_htblTsfinalISInView.Clear();
            p_teacherTTSelected = true;
            TablerDBDataSet.AllocationTableRow[] l_allocrows = (TablerDBDataSet.AllocationTableRow[])AllocationTable.Select(AllocationTable.TeacherIDColumn.ColumnName + " = " + teachid
                                                                                                + " OR " + AllocationTable.TeachersStrColumn.ColumnName + " LIKE '%" + teachid.ToString() + ",%'");

            int l_prevTsFinalID = -1, l_prevSlot = -1;

            foreach (TablerDBDataSet.AllocationTableRow l_alrw in l_allocrows)
            {
                if (l_alrw.SlotAlloted < 0) continue;
                if (l_alrw.IsTeachersStrNull() == false && l_alrw.TeachersStr.Length > 1)
                {
                    string l_selTeachers = l_alrw.TeachersStr;
                    bool l_isItContained = l_selTeachers.Contains("," + teachid + ",") ||
                                                  l_selTeachers.StartsWith(teachid + ",") ||
                                                  l_selTeachers.EndsWith("," + teachid);

                    if (l_isItContained == false) continue;
                }

                int l_rowno = l_alrw.SlotAlloted % p_global.NumOfSlotsInADay;

                int l_colno = l_alrw.SlotAlloted / p_global.NumOfSlotsInADay + 1;
                if (l_alrw.SlotAlloted >= p_global.NumOfSlotsInADay && l_rowno == 0)
                {
                    l_rowno = p_global.NumOfSlotsInADay - 1;
                    l_colno = l_colno - 1;
                }
                else
                {
                    l_rowno--;
                }
                TablerDBDataSet.SubjectsRow l_subrw = Subjects.FindBySubjectID(l_alrw.SUBJID);
                TablerDBDataSet.GradesRow l_grw = GRADES.FindByGradeID(l_alrw.GRDID);
                if (l_prevTsFinalID != l_alrw.TSFinalID || l_prevSlot + 1 != l_alrw.SlotAlloted)
                {
                    if (l_alrw.IsTeachersStrNull() || l_alrw.TeachersStr.Trim().Length <= 1)
                    {
                        l_ttv_table[l_rowno][l_colno] = l_grw.GradeName + "/ " + l_subrw.SubjectName;
                        if (l_alrw.IsRoomIDNull() == false)
                        {
                            TablerDBDataSet.RoomsRow l_roomRow = ROOMS.FindByRoomsID(l_alrw.RoomID);
                            if (l_roomRow != null)
                            {
                                l_ttv_table[l_rowno][l_colno] += "{" + l_roomRow.RoomsName + "}";
                            }
                        }
                    }
                    else
                    {
                        TablerDBDataSet.TSFInalRow l_tftw = TSFinalTbl.FindByID(l_alrw.TSFinalID);
                        l_ttv_table[l_rowno][l_colno] = l_grw.GradeName + "/ " + tGlobal.TeacherNameForMulti(l_tftw, l_tftw.ID, TSFinalTbl, Teachers, Subjects, ROOMS);
                    }
                }
                else
                {
                    l_ttv_table[l_rowno][l_colno] = "~";
                }

                l_prevTsFinalID = l_alrw.TSFinalID;
                l_prevSlot = l_alrw.SlotAlloted;


                if (p_htblTsfinalISInView.ContainsKey(l_rowno.ToString() + ":" + l_colno.ToString()) == false)
                {
                    p_htblTsfinalISInView.Add(l_rowno.ToString() + ":" + l_colno.ToString(), l_alrw.TSFinalID);
                }
                else
                {
                    MessageBox.Show("Repeated, " +  l_subrw.SubjectName + " in " + l_grw.GradeName + " Slot No:" + l_alrw.SlotAlloted.ToString());
                }
            }

            TablerDBDataSet.TeachersRow l_trw = Teachers.FindByTeachersID(teachid);
            //lblTitle.Text = "Time Table for Teacher:   " + l_trw.TeachersName;

        
            dataGridView1.DataSource = l_ttv_table;
            DayOfWeek l_dow = l_dt.DayOfWeek;
            int l_colWidth = 0;

            foreach (DataGridViewColumn l_col in dataGridView1.Columns)
            {
                if (l_col.HeaderText == l_dow.ToString() || l_col.Index == 0)
                    continue;
                else
                    l_col.Visible = false;

                l_colWidth += l_col.Width;
            }
            this.Width = l_colWidth + 300;
            fillCellsForPreassignments(teachid);
            //bindManualMoveLists();
            AssessRows = (DataSet1.AssessmentRow[])AssessTb.Select(AssessTb.GradeIDColumn.ColumnName + " = " + p_gradeid + " AND " + AssessTb.DateColumn.ColumnName
                    + " = '" + l_d.Date.ToString() + "'");
            AttendRows = (DataSet1.AttendanceRow[])AttendDT.Select(AttendDT.TeacherIDColumn.ColumnName + " = " + p_teacherID
                                                            + " AND " + AttendDT.DateColumn.ColumnName + " = '" + l_d.Date.ToString() + "'"
                                                            + " AND " + AttendDT.AbsORPresColumn.ColumnName + " = " + true);

        }

        public void FillTimeTableTblandBindWithGridViewForRoom(int schoolid, int roomid, string roomname)
        {
            p_schoolid = schoolid;
            p_roomID = roomid;

            lblTitle.Text = "Time Table: " + roomname;
            p_htblTsfinalISInView.Clear();
            p_teacherTTSelected = false;

            vIEWDataSet1.TimeTableViewDataTable l_ttv_table = p_global.GetViewTable();////


            TablerDBDataSet.AllocationTableRow[] l_allocrows = (TablerDBDataSet.AllocationTableRow[])AllocationTable.Select();//AllocationTable.RoomIDColumn.ColumnName + " = " + roomid);
            int l_prevTsFinalID = -1, l_prevSlot = -1;
            foreach (TablerDBDataSet.AllocationTableRow l_alrw in l_allocrows)
            {
                if (l_alrw.SlotAlloted < 0) continue;

                TablerDBDataSet.TSFInalRow l_tsfrw = TSFinalTbl.FindByID(l_alrw.TSFinalID);

                if ((l_alrw.IsTeachersStrNull() || l_alrw.TeachersStr.Length < 2) && l_alrw.RoomID != roomid)
                {
                    continue;
                }
                else if (l_alrw.RoomID == 0)
                {
                    if (l_tsfrw == null || l_tsfrw.IsSubjRoomStrNull() || !l_tsfrw.SubjRoomStr.Contains(":" + roomid.ToString() + ","))
                    {
                        continue;
                    }
                }
                int l_rowno = l_alrw.SlotAlloted % p_global.NumOfSlotsInADay;

                int l_colno = l_alrw.SlotAlloted / p_global.NumOfSlotsInADay + 1;
                if (l_alrw.SlotAlloted >= p_global.NumOfSlotsInADay && l_rowno == 0)
                {
                    l_rowno = p_global.NumOfSlotsInADay - 1;
                    l_colno = l_colno - 1;
                }
                else
                {
                    l_rowno--;
                }
                //TablerDBDataSet.SubjectsRow l_subrw = Subjects.FindBySubjectID(l_alrw.SUBJID);
                TablerDBDataSet.TeachersRow l_trw = Teachers.FindByTeachersID(l_alrw.TeacherID);
                TablerDBDataSet.GradesRow l_grw = GRADES.FindByGradeID(l_alrw.GRDID);
                if (l_prevTsFinalID != l_alrw.TSFinalID || l_prevSlot + 1 != l_alrw.SlotAlloted)
                {
                    if (l_tsfrw.IsSubjRoomStrNull() || l_tsfrw.SubjRoomStr.Length < 2)
                    {
                        l_ttv_table[l_rowno][l_colno] = /*l_subrw.SubjectName + ",\r\nGrade: " +*/ l_grw.GradeName
                                                                                + "/ " + l_trw.TeachersName;
                    }
                    else
                    {
                        l_ttv_table[l_rowno][l_colno] = l_grw.GradeName + "/ " + tGlobal.TeacherNameForMulti(l_tsfrw, l_tsfrw.ID, TSFinalTbl, Teachers, Subjects, ROOMS);
                    }
                }
                else
                {
                    l_ttv_table[l_rowno][l_colno] = "~";
                }
                
                l_prevTsFinalID = l_alrw.TSFinalID;
                l_prevSlot = l_alrw.SlotAlloted;

                if (p_htblTsfinalISInView.ContainsKey(l_rowno.ToString() + ":" + l_colno.ToString()) == false)
                {
                    p_htblTsfinalISInView.Add(l_rowno.ToString() + ":" + l_colno.ToString(), l_alrw.TSFinalID);
                }
                else
                {
                    MessageBox.Show("Repeated, " + l_trw.TeachersName + " in " + l_grw.GradeName);
                }

            }

            
            dataGridView1.DataSource = l_ttv_table;
            fillCellsForPreassignments(-1);
            //bindManualMoveLists();
        }

        public string p_slotSelectedDescr = null;
        public string p_slotSelected = "";

        public void FillForManualSelecttion(int l_gradeID, string l_gradename)
        {
            btnSaveClose.Visible = true;
            btnSaveClose.Click += new EventHandler(btnSaveClose_Click);
            dataGridView1.CellDoubleClick += new DataGridViewCellEventHandler(dataGridView1_CellDoubleClick);
            vIEWDataSet1.TimeTableViewDataTable l_ttv_table = p_global.GetViewTable();////
            dataGridView1.DataSource = l_ttv_table;
            p_defaultTeacherID = -99999;
            p_gradeid = l_gradeID;
        //   FillTimeTableTblandBindWithGridView(-1, l_gradeID, l_gradename);
            button5.Visible = button2.Visible = tbPath.Visible = button1.Visible = false;

        }

        public void FillForSelection(int l_gradeID)
        {
            //button1.Visible = button2.Visible = button5.Visible = tbPath.Visible = false;
            btnSaveClose.Visible = true;
            btnSaveClose.Click += new EventHandler(btnSaveClose_Click);
            dataGridView1.CellDoubleClick += new DataGridViewCellEventHandler(dataGridView1_CellDoubleClick);
            vIEWDataSet1.TimeTableViewDataTable l_ttv_table = p_global.GetViewTable();////
            dataGridView1.DataSource = l_ttv_table;
            p_defaultTeacherID = -99999;
            fillCellsForGradePreassignments(l_gradeID);
            button5.Visible = button2.Visible = tbPath.Visible = button1.Visible = false;
        }
        string time;

        void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1[e.ColumnIndex, e.RowIndex].Value.ToString().StartsWith("~"))
            {
                MessageBox.Show("Please double click on header/title of the lecture or pratical slot");
                return;
            }
            if (p_global.TeacherIDSelected <= 0)
            {
                addClassConductedRecord(false, null);
            }
            else if (p_global.TeacherIDSelected > 0)
            {
                addAttendanceRecord();
            }
        }

        void addClassConductedRecord(object sender, EventArgs l_e)
        {
            DataGridViewCellEventArgs e = new DataGridViewCellEventArgs(dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentCell.RowIndex);
            int slotnumber = (e.ColumnIndex - 1) * p_global.NumOfSlotsInADay + e.RowIndex + 1;
            string time = dataGridView1[0, e.RowIndex].Value.ToString();

            if (p_gradeid > 0)
            {
                bool l_donotOverwrite = false;
                if (sender is bool)
                {
                    l_donotOverwrite = (bool)sender;
                }
                fEnterDailyReportData l_dentry = new fEnterDailyReportData(slotnumber, l_d, GRADES.FindByGradeID(p_gradeid).GradeName, time, p_gradeid, AllocationTable, Teachers);
                l_dentry.SaveClassConducted(l_donotOverwrite);
                LoadAssessTable();
                AssessRows = (DataSet1.AssessmentRow[])AssessTb.Select(AssessTb.GradeIDColumn.ColumnName + " = " + p_gradeid + " AND " + AssessTb.DateColumn.ColumnName
                         + " = '" + l_d.Date.ToString() + "'");
                dataGridView1.Refresh();
                dataGridView1.ClearSelection();
            }
        }

        void addTeacherAbsencesRec(object sender, EventArgs l_e)
        {            
            int slotnumber;
            try
            {                
                DataGridViewCellEventArgs e = new DataGridViewCellEventArgs(dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentCell.RowIndex);
                if (dataGridView1[e.ColumnIndex, e.RowIndex].Value.ToString().StartsWith("~"))
                {
                    MessageBox.Show("Please double click on header/title of the lecture or pratical slot");
                    return;
                }
                if (p_global.TeacherIDSelected > 0)
                {
                    dataGridView1.CurrentCell = dataGridView1[e.ColumnIndex, e.RowIndex];
                    slotnumber = (e.ColumnIndex - 1) * p_global.NumOfSlotsInADay + e.RowIndex + 1;
              
                        time = dataGridView1[0, e.RowIndex].Value.ToString();
                        addToTable2(slotnumber, time, l_d, GRADES.FindByGradeID(p_gradeid).GradeName, p_gradeid, p_global.TeacherIDSelected, Teachers.FindByTeachersID(p_global.TeacherIDSelected).TeachersName);
                    

                    dataGridView1[e.ColumnIndex, e.RowIndex].Selected = false;
                }
                else
                {
                    dataGridView1.CurrentCell = dataGridView1[e.ColumnIndex, e.RowIndex];
                    slotnumber = (e.ColumnIndex - 1) * p_global.NumOfSlotsInADay + e.RowIndex + 1;
                    {
                        addClassConductedRecord(true, null);
                        time = dataGridView1[0, e.RowIndex].Value.ToString();
                        if (p_gradeid > 0)
                        {
                            /*TablerDBDataSet.AllocationTableRow[] alloc = (TablerDBDataSet.AllocationTableRow[])AllocationTable.Select(
                      AllocationTable.GRDIDColumn.ColumnName + " = " + p_gradeid + " AND " + AllocationTable.SlotAllotedColumn.ColumnName
                      + " = " + slotnumber);
                            if (alloc[0].Duration == 120)
                            {
                                string[] time1, time2;
                                string timef;
                                TablerDBDataSet.AllocationTableRow[] alloc2 = (TablerDBDataSet.AllocationTableRow[])AllocationTable.Select(
                              AllocationTable.GRDIDColumn.ColumnName + " = " + p_gradeid + " AND " + AllocationTable.SlotAllotedColumn.ColumnName
                              + " = " + (slotnumber + 1));
                                if (alloc[0].TeachersStr.Equals(alloc2[0].TeachersStr))
                                {
                                    time1 = dataGridView1[0, e.RowIndex].Value.ToString().Split(' ');
                                    time2 = dataGridView1[0, e.RowIndex + 1].Value.ToString().Split(' ');
                                    timef = time1[0] + " to " + time2[2];
                                }
                            }

                            else
                            {*/
                            addToTable(slotnumber, l_d, GRADES.FindByGradeID(p_gradeid).GradeName, time, p_gradeid, AllocationTable, Teachers);
                            //}
                        }
                        else
                        {
                            addToTable(slotnumber, l_d, tGlobal.GRADE_NAME[Math.Abs(p_gradeid)], time, p_gradeid, AllocationTable, Teachers);
                        }
                        dataGridView1[e.ColumnIndex, e.RowIndex].Selected = false;
                    }
                   
                }
            }
            catch(Exception er)
            {
                dataGridView1.CurrentCell = null;
                MessageBox.Show(er.Message);
            }
            dataGridView1.ClearSelection();
        }

        void btnSaveClose_Click(object sender, EventArgs e)
        {
            p_slotSelected = "";
            p_slotSelectedDescr = "";
            foreach (DataGridViewCell l_cell in dataGridView1.SelectedCells)
            {
                if (l_cell.Value is string)
                {
                    int l_slot = -1;
                    if (int.TryParse(l_cell.Value.ToString(), out l_slot) == false)
                    {
                        l_slot = (l_cell.ColumnIndex - 1) * p_global.NumOfSlotsInADay + l_cell.RowIndex + 1;
                    }
                    if (l_slot > 0)
                    {
                        p_slotSelected += l_slot.ToString() + ",";
                    }
                    //p_slotSelectedDescr += dataGridView1.Columns[l_cell.ColumnIndex].HeaderText + ", " + dataGridView1[0, l_cell.RowIndex].Value + "; ";
               }
            }
            /*if (dataGridView1.SelectedCells.Count > 0)
            {
                p_slotSelectedDescr = "To be done";
            }*/
            this.Close();
        }
    
        void addToTable(int slot, DateTime l_dt, string Grade, string time,int gradeid,TablerDBDataSet.AllocationTableDataTable l_alloc,TablerDBDataSet.TeachersDataTable l_teachers)
        {
            fEnterDailyReportData l_dentry = new fEnterDailyReportData(slot,l_dt,Grade,time,p_gradeid,l_alloc,l_teachers);
            l_dentry.ShowDialog();
            LoadAssessTable();
            AssessRows = (DataSet1.AssessmentRow[])AssessTb.Select(AssessTb.GradeIDColumn.ColumnName + " = " + p_gradeid + " AND " + AssessTb.DateColumn.ColumnName
                     + " = '" + l_d.Date.ToString() + "'");
            dataGridView1.Refresh();
        }

       void addToTable2(int slot, string time, DateTime l_dt, string gradename, int gradeid, int teacherid, string teachername)
       {
           MessageBox.Show(slot + " " + time + l_dt.Date.ToString() + gradename);
           AttendanceFormcs AttendForm = new AttendanceFormcs(slot,l_dt,time,gradename,gradeid,teacherid,teachername);
           AttendForm.ShowDialog();
           LoadAssessTable();
           AttendRows = (DataSet1.AttendanceRow[])AttendDT.Select(AttendDT.TeacherIDColumn.ColumnName + " = " + p_teacherID
                                                           + " AND " + AttendDT.DateColumn.ColumnName + " = '" + l_d.Date.ToString() + "'"
                                                           + " AND " +  AttendDT.AbsORPresColumn.ColumnName + " = " + true);
           dataGridView1.ClearSelection();

       }
        private void btnSaveClose_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        void addAttendanceRecord()
        {
            int day = (int)l_d.DayOfWeek;
            DataGridViewCellEventArgs e = new DataGridViewCellEventArgs(dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentCell.RowIndex);
            int slotnumber = (day - 1) * p_global.NumOfSlotsInADay + e.RowIndex + 1;

            string time = dataGridView1[0, e.RowIndex].Value.ToString();
            string l_gradename = "NA";
            if (p_gradeid > 0)
            {
                l_gradename = GRADES.FindByGradeID(p_gradeid).GradeName;
            }
            AttendanceFormcs l_Atnery = new AttendanceFormcs(slotnumber,l_d,time,l_gradename,p_gradeid,p_global.TeacherIDSelected,Teachers.FindByTeachersID(p_global.TeacherIDSelected).TeachersName);
            l_Atnery.ShowDialog();
            LoadAssessTable();
            AttendRows = (DataSet1.AttendanceRow[])AttendDT.Select(AttendDT.TeacherIDColumn.ColumnName + " = " + p_teacherID
                                                           + " AND " + AttendDT.DateColumn.ColumnName + " = '" + l_d.Date.ToString() + "'"
                                                           + " AND " + AttendDT.AbsORPresColumn.ColumnName + " = " + true);
            dataGridView1.ClearSelection();
            dataGridView1.Refresh();
        }

 

      

       
      
#if false
        #region DRAGDROP
        int p_rowIndex = -1, p_colIndex = -1;
        private void dataGridView1_MouseDown(object sender, MouseEventArgs e)
        {
            if (p_teacherTTSelected == false)
            {
                return;
            }
            p_rowIndex = -1; p_colIndex = -1;
            if (e.Button == MouseButtons.Left)
            {
                DataGridView.HitTestInfo info = dataGridView1.HitTest(e.X, e.Y);


                if (info.RowIndex >= 0)
                {
                    if (info.RowIndex >= 0 && info.ColumnIndex >= 0)
                    {
                        string text = "";// (String)
                        //dataGridView1.Rows[info.RowIndex].Cells[info.ColumnIndex].Value;
                        p_rowIndex = info.RowIndex;
                        p_colIndex = info.ColumnIndex;
                        if (text != null)
                        {
                            //Cursor.Current = Cursors.Hand;
                            dataGridView1.DoDragDrop(text, DragDropEffects.Copy);
                        }
                    }
                }
            }
        }


        private void dataGridView1_DragDrop(object sender, DragEventArgs e)
        {
            //string cellvalue = e.Data.GetData(typeof(string)) as string;
            Point cursorLocation = dataGridView1.PointToClient(new Point(e.X, e.Y));

            System.Windows.Forms.DataGridView.HitTestInfo hittest = dataGridView1.HitTest(cursorLocation.X, cursorLocation.Y);
            /*if (hittest.ColumnIndex != -1 
                && hittest.RowIndex != -1)
                dataGridView1[hittest.ColumnIndex, hittest.RowIndex].Value = cellvalue;*/

            int l_slotFrom = ((p_colIndex - 1) * p_global.NumOfSlotsInADay + p_rowIndex) + 1;
            int l_slotTo = ((hittest.ColumnIndex - 1) * p_global.NumOfSlotsInADay + hittest.RowIndex) + 1;
            if (l_slotFrom > 0 && l_slotFrom <= p_global.NumOfSlotsInWeek && l_slotTo > 0 && l_slotTo <= p_global.NumOfSlotsInWeek)
            {
                MoveSlot(l_slotFrom, l_slotTo);
            }
        }

        private void dataGridView1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        

        protected void MoveSlot(int l_slotToMoveFrom, int l_slotToMoveTo)
        {
            string l_err = "";
            if (TEacherIDOfTimeTableShown > 0 && l_slotToMoveFrom != l_slotToMoveTo) //that mean teacher time table is shown
            {
                TablerDBDataSet.AllocationTableRow l_ALRWFROM = null, l_ALRWTO = null;
                //get the row to find which grade it is moving from//it has to move to same grade
                TablerDBDataSet.AllocationTableRow[] l_alrwsForThisTeacherANDthisSlotFrom = (TablerDBDataSet.AllocationTableRow[])
                                                AllocationTable.Select("("+AllocationTable.TeacherIDColumn.ColumnName + " = " + TEacherIDOfTimeTableShown+" OR "+
                                                        AllocationTable.TeachersStrColumn.ColumnName + " like '%" + TEacherIDOfTimeTableShown.ToString() +",%')"
                                                                    + " AND " + AllocationTable.SlotAllotedColumn.ColumnName + " = " + l_slotToMoveFrom);

                int l_grpidWhereMoving = -1; //grade where it should move
                if (l_alrwsForThisTeacherANDthisSlotFrom.Length == 0) //nothing to move
                {
                    MessageBox.Show("Nothing to move");
                    return;
                }
                else //atlease one allocation row is found
                {
                    foreach (TablerDBDataSet.AllocationTableRow l_alrwT in l_alrwsForThisTeacherANDthisSlotFrom)
                    {
                        if (l_alrwT.TeacherID == TEacherIDOfTimeTableShown ||
                                (l_alrwT.IsTeachersStrNull() == false &&
                                (l_alrwT.TeachersStr.StartsWith(TEacherIDOfTimeTableShown.ToString() + ",") || l_alrwT.TeachersStr.Contains("," + TEacherIDOfTimeTableShown.ToString() + ",")))
                            )
                        {
                            if (l_alrwT.Duration > p_global.lect_min)
                            {
                                MessageBox.Show("Moving from practical lecture can not be done, operation can not be completed, please unallocate and allocate again");
                                return;
                            }
                            else
                            if (l_alrwT.IsTeachersStrNull() == false && l_alrwT.TeachersStr.Length > 1 
                                    && isDroppedToPracticals(l_alrwT.TeachersStr, l_slotToMoveTo, out l_err) == false)
                            {
                                MessageBox.Show(l_err);
                                return;
                            }
                            l_ALRWFROM = l_alrwT;
                            l_grpidWhereMoving = l_alrwT.GRDID;
                            break;
                        }
                    }
                }

                if (isGradesBlocked(l_ALRWFROM.GRDID, l_grpidWhereMoving, l_slotToMoveFrom, l_slotToMoveTo))
                {
                    DialogResult lresult = MessageBox.Show("Grade is Pre allocated. Would you like to ignore and continue drag/drop?", "Query", MessageBoxButtons.YesNo);
                    if (lresult == System.Windows.Forms.DialogResult.No)
                    {
                        return;
                    }

                }

                //check if it is the same teacher in that slot
                TablerDBDataSet.AllocationTableRow[] l_alrwsForThisTeacherANDthisSlotTO = (TablerDBDataSet.AllocationTableRow[])
                                                AllocationTable.Select("("+AllocationTable.TeacherIDColumn.ColumnName + " = " + TEacherIDOfTimeTableShown+" OR "+
                                                                      AllocationTable.TeachersStrColumn.ColumnName + " like '%" + TEacherIDOfTimeTableShown.ToString() +",%')"
                                                                    + " AND " + AllocationTable.SlotAllotedColumn.ColumnName + " = " + l_slotToMoveTo); //see here is slot to move to
                int l_TeacherinMoveTOSlot = -1, l_roomToMoveTo = -1;
                if (l_alrwsForThisTeacherANDthisSlotTO.Length > 0) //nothing to move
                {
                    foreach (TablerDBDataSet.AllocationTableRow l_alrwT in l_alrwsForThisTeacherANDthisSlotTO)
                    {
                        if (l_alrwT.TeacherID == TEacherIDOfTimeTableShown || (l_alrwT.IsTeachersStrNull() == false 
                                                                                    && ( l_alrwT.TeachersStr.StartsWith(TEacherIDOfTimeTableShown.ToString() + ",")
                                                                                        || l_alrwT.TeachersStr.Contains("," + TEacherIDOfTimeTableShown.ToString() + ","))))
                                                                                                    
                        {                           

                            if (l_alrwT.IsTeachersStrNull() == false
                                   && l_alrwT.TeachersStr.Length > 1
                                   && isTeachersFree(TEacherIDOfTimeTableShown, l_alrwT.TeachersStr, l_slotToMoveFrom, out l_err) == false
                                   )
                            {

                                MessageBox.Show("Multple Teachers Not Free " + l_err + "\r\n from " + getMultiTeacherNames(l_alrwT.TSFinalID));
                                return;
                            }
                            else 
                            {
                                MessageBox.Show("For this type of swapping, please put the teacher in break slot temporarily and move other slot");
                                //same teacher then
                                /*l_ALRWFROM.SlotAlloted = l_slotToMoveTo;
                               
                                l_alrwT.SlotAlloted = l_slotToMoveFrom;
                                //just refill table and done
                                FillTimeTableTblandBindWithGridViewForTeac(p_schoolid, TEacherIDOfTimeTableShown, TeacherNameSelected);*/
                                
                                return; //job done
                            }
                        }
                    }               

                }

                //now find if in the slot it is moving the teacher there is free to come here
                //so find teacher id
                l_alrwsForThisTeacherANDthisSlotTO = (TablerDBDataSet.AllocationTableRow[])
                                                AllocationTable.Select(AllocationTable.GRDIDColumn.ColumnName + " = " + l_grpidWhereMoving
                                                                    + " AND " + AllocationTable.SlotAllotedColumn.ColumnName + " = " + l_slotToMoveTo); //see here is slot to move to

                l_TeacherinMoveTOSlot = -1;
                string teacherTOString = null;
                string l_teacherMoveToNames = null;
                if (l_alrwsForThisTeacherANDthisSlotTO.Length > 0) //nothing to move
                {
                    if (l_alrwsForThisTeacherANDthisSlotTO[0].IsTeachersStrNull() == false && l_alrwsForThisTeacherANDthisSlotTO[0].TeachersStr.Length > 1)
                    {
                        teacherTOString = l_alrwsForThisTeacherANDthisSlotTO[0].TeachersStr;
                        l_teacherMoveToNames = getMultiTeacherNames(l_alrwsForThisTeacherANDthisSlotTO[0].TSFinalID);
                    }
                    l_TeacherinMoveTOSlot = l_alrwsForThisTeacherANDthisSlotTO[0].TeacherID;
                    if (l_teacherMoveToNames == null)
                    {
                        TablerDBDataSet.TeachersRow l_trw = Teachers.FindByTeachersID(l_alrwsForThisTeacherANDthisSlotTO[0].TeacherID);
                        l_teacherMoveToNames = l_trw.TeachersName;
                    }
                    if (l_alrwsForThisTeacherANDthisSlotTO[0].IsRoomIDNull() == false)
                    {
                        l_roomToMoveTo = l_alrwsForThisTeacherANDthisSlotTO[0].RoomID;
                    }
                    if (l_alrwsForThisTeacherANDthisSlotTO[0].Duration > p_global.lect_min)
                    {
                       MessageBox.Show("You are moving to practical slot,\r\n therefore this operation cannot be completed,\r\nTeachers:" 
                                                            + getMultiTeacherNames(l_alrwsForThisTeacherANDthisSlotTO[0].TSFinalID));
                       return;
                    }
                }
        
                //check of teacher can move to the from slot, in order to exchange
                if (
                                (l_TeacherinMoveTOSlot < 0 /*means there is no teacher, so nothing to worry*/
                                || l_TeacherinMoveTOSlot == TEacherIDOfTimeTableShown //same teacher so just swap            
                                || ( (teacherTOString != null && isTeachersFree(TEacherIDOfTimeTableShown, teacherTOString, l_slotToMoveFrom, out l_err)) || 
                                        (teacherTOString == null && isTeacherFree(l_TeacherinMoveTOSlot, l_slotToMoveFrom, out l_err)) ))
                                && isSlotFree(l_slotToMoveTo)
                                && isRoomFree(l_roomToMoveTo, l_slotToMoveTo))
                {
                    //lblResultOFMove.Text = "Success";
                    //yes now we can swop
                    //interchange the slot and fill time table again
                    if (l_teacherMoveToNames != null)
                    {
                        MessageBox.Show("To complete the move, the following teacher(s) moved :\r\n " + l_teacherMoveToNames);
                    }
                    l_ALRWFROM.SlotAlloted = l_slotToMoveTo;
                    if (l_TeacherinMoveTOSlot > 0)
                    {
                        l_alrwsForThisTeacherANDthisSlotTO[0].SlotAlloted = l_slotToMoveFrom;
                    }
                    ISMoved = true;
                    FillTimeTableTblandBindWithGridViewForTeac(p_schoolid, TEacherIDOfTimeTableShown, TeacherNameSelected);
                }
                else
                {
                    string l_msg = null;
                    TablerDBDataSet.TeachersRow l_trw = Teachers.FindByTeachersID(l_TeacherinMoveTOSlot);

                    if (l_trw != null)
                    {
                        l_msg = "Can not move as Teacher: " + l_trw.TeachersName + "(" + l_err + ")" + " from other slot is not be free";
                        if (l_roomToMoveTo > 0)
                        {
                            TablerDBDataSet.RoomsRow l_rrw = ROOMS.FindByRoomsID(l_roomToMoveTo);
                            if (l_rrw != null)
                            {
                                l_msg += "\r\nor Room To move to is " + l_rrw.RoomsName + " is not free";
                            }
                        }
                        MessageBox.Show(l_msg + " , move other lecture and try again");
                    }
                    else
                    {
                        MessageBox.Show("Slot is not free");
                    }
                    //lblResultOFMove.Text = "Teacher: " + l_trw.TeacherName + " from other slot can not move here, try to move the teacher somewhere and try again";
                }

            }
        }

        bool isGradesBlocked(int l_gradeFrom, int l_gradeTo, int l_slotFrom, int l_slotTo)
        {
            foreach (TablerDBDataSet.GradesPreAssignRow l_grdprw in GradesPreAssign.Rows)
            {
                if ((l_grdprw.GradeID == l_gradeFrom && (l_grdprw.SlotNums.StartsWith(l_slotFrom.ToString() + ",") || l_grdprw.SlotNums.Contains("," + l_slotFrom.ToString() + ","))
                    || (l_grdprw.GradeID == l_gradeTo && (l_grdprw.SlotNums.StartsWith(l_slotTo.ToString() + ",") || l_grdprw.SlotNums.Contains("," + l_slotTo.ToString() + ",")))))
                {
                    return true;
                }
            }
            return false;
        }

        bool isSlotFree(int slot)
        {
            TablerDBDataSet.PreAssignRow[] l_prrws = (TablerDBDataSet.PreAssignRow[])PreAssign.Select(PreAssign.SlotNoColumn.ColumnName + " = " + slot);
            if (l_prrws.Length == 0)
            {
                return true;
            }
            return false;

        }
        
        bool isTeachersFree(int l_teacherTimeTableShown, string l_str, int l_slot, out string o_err)
        {
            o_err = "";
            string[] ids=l_str.Split(',');
            foreach (string id in ids)
            {
                int a = 0;
                int.TryParse(id, out a);

                if (l_teacherTimeTableShown != a && (a > 0 && isTeacherFree(a, l_slot, out o_err) == false))
                {
                    return false;
                }
 

            }
            return true;
        }

        
        bool isTeacherFree(int teacherid, int slot, out string o_err)
        {
            o_err = "";
            TablerDBDataSet.AllocationTableRow[] l_alrwsForTchr = (TablerDBDataSet.AllocationTableRow[])
                                                AllocationTable.Select("(" + AllocationTable.TeacherIDColumn.ColumnName + " = " + teacherid + " OR " +
                                                                      AllocationTable.TeachersStrColumn.ColumnName + " like '%" + teacherid.ToString() + ",%')"
                                                                        + " AND " + AllocationTable.SlotAllotedColumn.ColumnName + " = " + slot);
            if (l_alrwsForTchr.Length == 0)
            {
                return true;
            }
            else
            {
                foreach (TablerDBDataSet.AllocationTableRow l_alrw in l_alrwsForTchr)
                {
                    if (l_alrw.TeacherID == teacherid ||
                                (l_alrw.IsTeachersStrNull() == false && (l_alrw.TeachersStr.StartsWith(teacherid.ToString() + ",") || l_alrw.TeachersStr.Contains("," + teacherid.ToString() + ","))))
                    {
                        if (l_alrw.TeacherID != TEacherIDOfTimeTableShown)
                        {
                            //teacher found
                            TablerDBDataSet.TeachersRow l_trw = Teachers.FindByTeachersID(l_alrw.TeacherID);
                            o_err = l_trw.TeachersName;
                            return false;
                        }
                    }
                }
                return true;
            }
            return false;
        }

        bool isRoomFree(int roomid, int slot)
        {
            if (roomid == 0) return true;
            TablerDBDataSet.AllocationTableRow[] l_alrwsForRoom = (TablerDBDataSet.AllocationTableRow[])
                                                AllocationTable.Select(AllocationTable.RoomIDColumn.ColumnName + " = " + roomid
                                                                    + " AND " + AllocationTable.SlotAllotedColumn.ColumnName + " = " + slot);
            if (l_alrwsForRoom.Length == 0)
            {
                return true;
            }
            return false;
        }

        bool isDroppedToPracticals(string l_str, int l_slot, out string o_err)
        {
            o_err = "";
            string[] ids = l_str.Split(',');
            foreach (string id in ids)
            {
                int a = 0;
                int.TryParse(id, out a);

                TablerDBDataSet.AllocationTableRow[] l_alrws = (TablerDBDataSet.AllocationTableRow[])
                                                AllocationTable.Select("("+AllocationTable.TeacherIDColumn.ColumnName + " = " + a +" OR "+
                                                        AllocationTable.TeachersStrColumn.ColumnName + " like '%" + a.ToString() +",%')"
                                                                    + " AND " + AllocationTable.SlotAllotedColumn.ColumnName + " = " + l_slot);

                foreach (TablerDBDataSet.AllocationTableRow l_alrwT in l_alrws)
                {
                    if (l_alrwT.TeacherID == a || (l_alrwT.IsTeachersStrNull() == false
                                                                                    && (l_alrwT.TeachersStr.StartsWith(a.ToString() + ",")
                                                                                        || l_alrwT.TeachersStr.Contains("," + a.ToString() + ","))))
                    {
                        if (l_alrwT.Duration > p_global.lect_min)
                        {
                            o_err = "Moving to practical lecture, operation can not be completed, please unallocate and allocate again";
                            return false;
                        }
                    }
                                                         
                }

            }
            return true;
        }
        #endregion //dragdrop

        private void button5_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            RunWordAppAndOpenDoc(null);
            ExportToWord();
            CloseSaveWordDoc();
            Cursor.Current = Cursors.Default;
        }


        Word._Application p_objApp = null;
        Word._Document p_objDoc = null;

        public void RunWordAppAndOpenDoc(string l_path)            
        {
            p_objApp = new Word.Application();
            //p_objApp.Visible = true;
            //string l_FilePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            
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
            p_objDoc.PageSetup.LeftMargin = p_objDoc.PageSetup.RightMargin = 30.0F;
            p_objDoc.PageSetup.TopMargin = 30.0F;            
        }

        public string CloseSaveWordDoc() 
        {
            string l_path = null;
            try
            {
                p_objDoc.Words.Last.InsertBreak(Word.WdBreakType.wdPageBreak);
                p_objDoc.Save();
                l_path = p_objDoc.FullName;              
                p_objDoc.Close();
                releaseObject(p_objDoc);
            }
            catch (Exception e)
            {
                MessageBox.Show("Saving Exception occured");
            }
            return l_path;
        }

        public void ExportToWord()//int l_schoolID, int l_gradeID, TablerDBDataSet.AllocationTableDataTable l_allocation)
        {
            object objEndOfDocFlag = "\\endofdoc"; /* \endofdoc is a predefined bookmark */

            Word.Paragraph objPara2; //define paragraph object
            object oRng = p_objDoc.Bookmarks.get_Item(ref objEndOfDocFlag).Range; //go to end of the page
            objPara2 = p_objDoc.Content.Paragraphs.Add(ref oRng); //add paragraph at end of document
            objPara2.LeftIndent = 0.2F;
            objPara2.Range.Text = lblTitle.Text; //add some text in paragraph
            objPara2.Range.Bold = 1;
            objPara2.Format.SpaceAfter = 10; //defind some style
            objPara2.Range.InsertParagraphAfter(); //insert paragraph
            objPara2.Range.Bold = 0;
            
            //Insert a 2 x 2 table, (table with 2 row and 2 column)
            Word.Table objTab1; //create table object
            Word.Range objWordRng = p_objDoc.Bookmarks.get_Item(ref objEndOfDocFlag).Range; //go to end of document
            object objMiss = System.Reflection.Missing.Value;
            objTab1 = p_objDoc.Tables.Add(objWordRng, dataGridView1.Rows.Count+1, dataGridView1.Columns.Count, ref objMiss, ref objMiss); //add table object in word document
            objTab1.Borders.InsideLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;
            objTab1.Borders.OutsideLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;
            objTab1.Borders.InsideLineWidth = Word.WdLineWidth.wdLineWidth025pt;
            objTab1.Borders.OutsideLineWidth = Word.WdLineWidth.wdLineWidth050pt;

            string strText;
            foreach (DataGridViewColumn l_col in dataGridView1.Columns)
            {
                objTab1.Columns[l_col.Index+1].Width = p_objApp.InchesToPoints(1.0F);
                if (l_col.Index == 0) continue;
                objTab1.Cell(1, l_col.Index+1).Range.Text = l_col.HeaderText; //add some text to cell

            }
            foreach (DataGridViewRow iRow in dataGridView1.Rows)
            {
                objTab1.Rows[iRow.Index+2].Height = p_objApp.InchesToPoints(0.5F);
                foreach (DataGridViewCell iCols in iRow.Cells)
                {
                    if (iCols.Value is string)
                    {
                        strText = (string)iCols.Value;//"row:" + iRow + "col:" + iCols;
                        int l_slotno = 0;
                        Word.Cell l_cell = objTab1.Cell(iRow.Index + 2, iCols.ColumnIndex + 1);
                        if (strText.Length < 3)
                        {
                           if (int.TryParse(strText, out l_slotno))
                           {
                               if ((iRow.Index  == p_global.RecessLect1-1 || iRow.Index == p_global.RecessLect2-1))
                               {
                                   Color c = Color.Gray;
                                   l_cell.Row.SetHeight(20.0F, Word.WdRowHeightRule.wdRowHeightExactly);
                                   var wdc = (Microsoft.Office.Interop.Word.WdColor)(c.R + 0x100 * c.G + 0x10000 * c.B);
                                   l_cell.Range.Shading.BackgroundPatternColor = wdc;
                               }                              
                               continue;
                           }
                        }
                       
                        //if (strText.Length > 2)
                        {                           
                            l_cell.Range.Text = strText; //add some text to cell
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
            objTab1.Columns[1].Width = p_objApp.InchesToPoints(1.2F); //increase first column width




        }

        private void button1_Click(object sender, EventArgs e)
        {
            tbPath.Text = tGlobal.OpenFilePathFromUser();
        }

        private void button2_Click(object sender, EventArgs e)
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
            catch{
                MessageBox.Show("Please close the word document you want to add to before proceeding");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            string l_path = null;
            ExportToExcel(lblTitle.Text, ref l_path);
  
            Cursor.Current = Cursors.Default;
        }
        
        Excel.Application xlApp;
        Excel.Workbook xlWorkBook;
        Excel.Worksheet xlWorkSheet;
        bool isItGrade = false;
        System.Collections.Hashtable p_Abb = new System.Collections.Hashtable();
        public bool ExportToExcel(string l_name, ref string path)
        {
            if (l_name == null)
            {
                l_name = lblTitle.Text;
            }
            
            bool l_ret = false;
            try
            {
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

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
                //.Add(misValue); 
                xlWorkBook = xlApp.Workbooks[1];
                xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.Add();//.get_Item(1);
                
                string c = lblTitle.Text;
                string[] d = c.Split(' ');
                if (isItGrade == false && d[2].Equals("Prof."))
                {
                    c = d[3].Substring(0, 1) + d[4].Substring(0, 1);
                }

                else if (isItGrade == false && d[2].Equals("Faculty"))
                {
                    c = d[2] + d[3];
                }
                else
                {
                    c = d[2];
                }
                if (p_Abb.ContainsKey(c) == false)
                {
                    xlWorkSheet.Name = c;
                }
                else
                {
                    c = d[3].Substring(0, 2) + d[4].Substring(0, 1);
                    xlWorkSheet.Name = c;
                }

                p_Abb.Add(c, null);
                
                xlWorkSheet.Range["A7"].RowHeight = 20;
                xlWorkSheet.Range["A7", "G7"].Font.Bold = true;
                //xlWorkSheet = (Excel.Worksheet)xlWorkBook.ActiveSheet; //.Sheets[1];

                xlWorkSheet.Cells[6, 4] = lblTitle.Text;
                xlWorkSheet.Range["A6"].Font.Bold = true;

                int l_startRowNum = 7;
                foreach (DataGridViewColumn l_col in dataGridView1.Columns)
                {
                    if (l_col.Index == 0) continue;
                    xlWorkSheet.Cells[l_startRowNum, l_col.Index + 1] = l_col.HeaderText; //add some text to cell

                }
                l_startRowNum++;
                xlWorkSheet.Range["A7"].RowHeight = 20;
                xlWorkSheet.Range["A8", "G19"].Font.Size = 10.0F;
                xlWorkSheet.Range["A8", "G19"].RowHeight = 36;
                xlWorkSheet.Range["A8", "G19"].ColumnWidth = 20;
                xlWorkSheet.Range["A8", "G19"].WrapText = true;
                xlWorkSheet.Range["A7", "G19"].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                xlWorkSheet.Range["A7", "G19"].Borders.Color = System.Drawing.ColorTranslator.ToOle(Color.Black);

                xlWorkSheet.get_Range("A7", "G19").HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                xlWorkSheet.get_Range("A7", "G19").VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                foreach (DataGridViewRow iRow in dataGridView1.Rows)
                {
                    foreach (DataGridViewCell iCols in iRow.Cells)
                    {
                        string strText = dataGridView1[iCols.ColumnIndex, iRow.Index].Value.ToString();
                        int l_slotno = 0;
                        if (strText.Length < 3)
                        {
                            if (int.TryParse(strText, out l_slotno))
                            {
                                if ((iRow.Index == p_global.RecessLect1 - 1 || iRow.Index == p_global.RecessLect2 - 1))
                                {
                                    string l_rnum = (iRow.Index + l_startRowNum).ToString();
                                    xlWorkSheet.Range["A" + l_rnum, "G" + l_rnum].RowHeight = 20;
                                    xlWorkSheet.Range["A" + l_rnum, "G" + l_rnum].Interior.Color = System.Drawing.ColorTranslator.ToOle(Color.Gray);
                                    //xlWorkSheet.Rows[iRow+4]SetHeight(20.0F, Word.WdRowHeightRule.wdRowHeightExactly);
                                    //var wdc = (Microsoft.Office.Interop.Word.WdColor)(color.R + 0x100 * color.G + 0x10000 * color.B);
                                    //l_cell.Range.Shading.BackgroundPatternColor = wdc;
                                }
                                continue;
                            }
                        }

                        {
                            (xlWorkSheet.Cells[iRow.Index + l_startRowNum, iCols.ColumnIndex + 1]) = strText;
                        }

                    }
                }
           
                /*for (int i = 2; i <= 7; i++)
                {
                    for (int j = l_startRowNum + 1; j <= 10 + l_startRowNum; j++)
                    {
                        if (xlWorkSheet.Cells[j, i] == xlWorkSheet.Cells[j + 1, i])
                        {
                //            g = j + 1;
                            xlWorkSheet.Range[xlWorkSheet.Cells[j, i], xlWorkSheet.Cells[j+1, i]].Merge();
                        }
                    }
                }*/
                

                path = saveExcelAndCleanUp(ref path);
                l_ret = true;
            }
            catch (Exception l_e)
            {
                MessageBox.Show("You may be exporting same sheet again, " + l_e.Message);
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

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (tbPath.Text.Trim().Length > 0)
                {
                    string l_path = tbPath.Text;
                    if (ExportToExcel(lblTitle.Text.ToString(), ref l_path))
                    {
                        MessageBox.Show("This Time Table has been added to the existing selected word document");
                    }
                }
                else
                {
                    MessageBox.Show("Please select file to export");
                }
            }
            catch
            {
                MessageBox.Show("Please close the excel document you want to add to before proceeding");
            }
        }
#endif

    }
} 
