using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TeachersAssessment
{
    public partial class fRoomAvailabilityView : Form
    {
        NorthernLights.tGlobal p_global = null;

        System.Collections.Hashtable p_RoomshshTbl = new System.Collections.Hashtable(),
            p_colorHshTbl = new System.Collections.Hashtable();

        TablerDBDataSet.RoomsDataTable p_Rooms = new TablerDBDataSet.RoomsDataTable();

        string[] p_connStrings, p_dbTTS, p_dbATs, p_depts;

        int p_dayOfWeek;
        //Color[] p_colors;

        public fRoomAvailabilityView(string[] l_connStrings, string[] l_dbTTS, string[] l_dbATs, string[] l_depts)
        {
            InitializeComponent();

            //p_colors = new Color[] { Color.Beige, Color.Brown, Color.DarkGreen, Color.DarkBlue, Color.Crimson, Color.Orchid };
            //int j = 0;
            /*foreach (string l_dept in l_depts)
            {
                p_colorHshTbl.Add(l_dept, p_colors[j]);
                j++;
            }*/
            dateTimePicker1.Value = DateTime.Today;
            p_dayOfWeek = (int)dateTimePicker1.Value.DayOfWeek;
            lblError.Text = dateTimePicker1.Value.DayOfWeek.ToString();
            p_connStrings = l_connStrings;
            p_dbATs = l_dbATs;
            p_dbTTS = l_dbTTS;
            p_depts = l_depts;
            int l_start = 1;
            if (p_connStrings.Length == 1)
            {
                l_start = 0;
            }
            for (int i = l_start; i < l_connStrings.Length; i++)
            {
                collectRoomsDataForADept(l_connStrings[i], l_dbTTS[i], l_dbATs[i], l_depts[i]);
            }
            
            this.Load += fRoomAvailabilityView_Load;
        }

        void collectRoomsDataForADept(string l_connString, string l_dbTT, string l_dbAT, string l_dept)
        {
            NorthernLights.Properties.Settings.Default.TablerDBConnectionString = l_connString + "Database=" + l_dbTT;
            System.Data.MPrSQL.tGlobal.p_connectionString = NorthernLights.Properties.Settings.Default.TablerDBConnectionString;
            TeachersAssessment.Properties.Settings.Default.DataSet1Conn = l_connString + "Database=" + l_dbAT;

            try
            {
                Cursor.Current = Cursors.WaitCursor;
                NorthernLights.MyTableAdapters.CloseConnections();
                TeachersAssessment.MyTableAdapters.CloseConnections();
                NorthernLights.MyTableAdapters.OpenConnections();
                TeachersAssessment.MyTableAdapters.OpenConnections();
            }
            catch
            {
                int i = NorthernLights.Properties.Settings.Default.TablerDBConnectionString.IndexOf("password");
                string l_dbstr = NorthernLights.Properties.Settings.Default.TablerDBConnectionString.Substring(0, i);

                MessageBox.Show("Can not access the databases, Dept: " + l_dept + " - "  + l_dbstr + " availability data will be incomplete");
                return;
            }
                    

            p_global = NorthernLights.tGlobal.GetInstance(-1);
            p_global.Set_TimeTableglobal();

            p_Rooms.Clear();
            System.Data.MPrSQL.MPrSQLDataAdapter l_da = NorthernLights.MyTableAdapters.RoomsTableAdapter();
            l_da.Fill(p_Rooms);
            TablerDBDataSet.RoomsRow[] l_roomrws = (TablerDBDataSet.RoomsRow[])p_Rooms.Select(null, p_Rooms.RoomsNameColumn.ColumnName);
            foreach (TablerDBDataSet.RoomsRow l_roomrw in l_roomrws)
            {
                 string l_key = l_roomrw.RoomsName.Trim();
                string l_deptRoomKey = l_dept + " - " + l_key;
               
                if (p_RoomshshTbl.ContainsKey(l_key) == false)
                {
                    System.Collections.Hashtable l_hstbl = new System.Collections.Hashtable();

                    p_RoomshshTbl.Add(l_key, l_hstbl);

                    p_deptRoomKeys.Add(l_deptRoomKey, l_key);
               
                }
            }
            l_da = NorthernLights.MyTableAdapters.AllocationTableTableAdapter();
            TablerDBDataSet.AllocationTableDataTable AllocationTable = new TablerDBDataSet.AllocationTableDataTable();
            l_da.Fill(AllocationTable);            

            l_da = NorthernLights.MyTableAdapters.GradesTableAdapter();
            TablerDBDataSet.GradesDataTable l_grades = new TablerDBDataSet.GradesDataTable();
            l_da.Fill(l_grades);

            l_da = NorthernLights.MyTableAdapters.SubjectsTableAdapter();
            TablerDBDataSet.SubjectsDataTable l_subjects = new TablerDBDataSet.SubjectsDataTable();
            l_da.Fill(l_subjects);

            l_da = NorthernLights.MyTableAdapters.TeachersTableAdapter();
            TablerDBDataSet.TeachersDataTable l_teachers = new TablerDBDataSet.TeachersDataTable();
            l_da.Fill(l_teachers);

            l_da = NorthernLights.MyTableAdapters.TSFInalTableAdapter();
            TablerDBDataSet.TSFInalDataTable TSFinalTbl = new TablerDBDataSet.TSFInalDataTable();
            l_da.Fill(TSFinalTbl);

            TablerDBDataSet.AllocationTableRow[] l_allocrows = (TablerDBDataSet.AllocationTableRow[])AllocationTable.Select();//AllocationTable.RoomIDColumn.ColumnName + " = " + roomid);
          
            foreach (TablerDBDataSet.AllocationTableRow l_alrw in l_allocrows)
            {
                if (l_alrw.SlotAlloted < 0) continue;
                int lowerLimit = (p_dayOfWeek-1)*p_global.NumOfSlotsInADay + 1;
                int upperLimit = lowerLimit + p_global.NumOfSlotsInADay;
                if (l_alrw.SlotAlloted < lowerLimit || l_alrw.SlotAlloted >= upperLimit)
                {
                    continue;
                }
                TablerDBDataSet.TSFInalRow l_tsfrw = TSFinalTbl.FindByID(l_alrw.TSFinalID);
                int l_roomID = l_alrw.RoomID;

                int l_rowno = l_alrw.SlotAlloted % p_global.NumOfSlotsInADay;
                string l_gradename = "unknown";
                TablerDBDataSet.GradesRow l_grw = null;
                if (l_tsfrw == null)
                {
                    continue;
                }
                l_grw = l_grades.FindByGradeID(l_tsfrw.GradesID);
                if (l_grw != null)
                {
                    l_gradename = l_grw.GradeName;
                }
                string l_details = l_dept +" : "+ l_gradename + " : -" + tGlobal.TeacherNameForMulti(l_tsfrw, l_tsfrw.ID, TSFinalTbl, l_teachers, l_subjects, p_Rooms);
                if (l_roomID == 0)
                {
                    if (l_tsfrw == null || l_tsfrw.IsSubjRoomStrNull())
                    {
                        continue;
                    }
                    else
                    {
                        string[] l_subjRooms = l_tsfrw.SubjRoomStr.Split(',');
                        foreach (string l_str in l_subjRooms)
                        {
                            string[] l_subjRoom = l_str.Split(':');
                            if (l_subjRoom.Length >= 2)
                            {
                                int.TryParse(l_subjRoom[1], out l_roomID);
                                addToViewTables(l_dept, l_roomID, l_rowno, l_details);
                            }
                        }
                    }
                }
                else
                {
                    addToViewTables(l_dept, l_roomID, l_rowno, l_details);
                }
            }
            NorthernLights.MyTableAdapters.CloseConnections();
            Cursor.Current = Cursors.Default;
        }

        System.Collections.Hashtable p_deptRoomKeys = new System.Collections.Hashtable();

        void addToViewTables(string l_dept, int roomid, int l_rowno, string l_details)
        {
            TablerDBDataSet.RoomsRow l_rrw = p_Rooms.FindByRoomsID(roomid);
            if (l_dept.Length > 4)
            {
                l_dept = l_dept.Substring(0, 4);
            }
            if (l_rrw != null)
            {
                string l_key = l_rrw.RoomsName.Trim();
                string l_deptRoomKey = l_dept + " - " + l_key;
                System.Collections.Hashtable l_hstbl = null;
                if (p_RoomshshTbl.ContainsKey(l_key) == false)
                {
                    l_hstbl = new System.Collections.Hashtable();                    
                    p_RoomshshTbl.Add(l_key, l_hstbl);
                    p_deptRoomKeys.Add(l_deptRoomKey, l_key);
                }
                else
                {
                    l_hstbl = (System.Collections.Hashtable) p_RoomshshTbl[l_key];
                }
                if (l_hstbl.ContainsKey(l_rowno))
                {
                    MessageBox.Show("Room Conflict: " + l_details + " row " + l_rowno.ToString());
                }
                else
                {
                    l_hstbl.Add(l_rowno, l_details);
                }
            }
        }

        void fRoomAvailabilityView_Load(object sender, EventArgs e)
        {
            FillTimeTableTblandBindWithGridViewForRoom();

            dataGridView1.CellFormatting += dataGridView1_CellFormatting;
        }

        void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridView1[e.ColumnIndex, e.RowIndex].Tag is int)
            {
                if ((int)(dataGridView1[e.ColumnIndex, e.RowIndex].Tag) == 1 && dataGridView1.Columns[e.ColumnIndex].HeaderText.Contains("*") == false)
                {
                    dataGridView1.Columns[e.ColumnIndex].HeaderText += "*";
                }
                dataGridView1[e.ColumnIndex, e.RowIndex].Style.BackColor = Color.Red;//(Color)dataGridView1[e.ColumnIndex, e.RowIndex].Tag;
            }
            else
            {
                dataGridView1[e.ColumnIndex, e.RowIndex].Style.BackColor = dataGridView1.RowTemplate.DefaultCellStyle.BackColor;
            }
                
        }


        void initializeDataGrid()
        {
            dataGridView1.RowsDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            //dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.AllowDrop = true;

            dataGridView1.ShowCellToolTips = true;
            dataGridView1.CellToolTipTextNeeded += dataGridView1_CellToolTipTextNeeded;

                      
        }

        
        void dataGridView1_CellToolTipTextNeeded(object sender, DataGridViewCellToolTipTextNeededEventArgs e)
        {
            e.ToolTipText = dataGridView1[e.ColumnIndex, e.RowIndex].Tag.ToString();
        }

        public void FillTimeTableTblandBindWithGridViewForRoom()
        {
            lblTitle.Text = "Room Availibility";
            if (p_RoomshshTbl.Count == 0 || p_deptRoomKeys.Count == 0)
            {
                lblError.Text = "No data found for any department";
                return;
            }
            NorthernLights.vIEWDataSet1.TimeTableViewDataTable l_ttv_table = p_global.GetViewTable();////

            DataGridViewTextBoxColumn l_dgrvc = new DataGridViewTextBoxColumn();
            l_dgrvc.HeaderText = "Time";
            dataGridView1.Columns.Add(l_dgrvc);
            foreach  (NorthernLights.vIEWDataSet1.TimeTableViewRow l_ttvrw in l_ttv_table )
            {
                int i = dataGridView1.Rows.Add();
                dataGridView1.Rows[i].Cells[0].Value = l_ttvrw.LectureHr;
            }

            string[] l_roomnames = new string[p_RoomshshTbl.Count];
            
            p_deptRoomKeys.Keys.CopyTo(l_roomnames, 0);
            Array.Sort(l_roomnames);          
            foreach(string l_fullRoomName in l_roomnames)
            {
                string l_roomname = (string) p_deptRoomKeys[l_fullRoomName];
                int i = l_fullRoomName.IndexOf("-");
                string l_Dept = "NA";
                if (i > 0 && i < l_fullRoomName.Length)
                {
                    l_Dept = l_fullRoomName.Substring(0, i - 1);
                }
                
                l_dgrvc = new DataGridViewTextBoxColumn();
                l_dgrvc.Width = 48;
                l_dgrvc.HeaderText = l_roomname;
                int c =dataGridView1.Columns.Add(l_dgrvc);
                System.Collections.Hashtable l_htbl = (System.Collections.Hashtable) p_RoomshshTbl[l_roomname];
                foreach (int l_rownoRaw in l_htbl.Keys)
                {
                    int l_rowno = l_rownoRaw - 1;
                    string l_ttptext = l_htbl[l_rownoRaw].ToString();
                    int j = l_ttptext.IndexOf(':');
                    j = j > l_Dept.Length ? l_Dept.Length : j;
                    string l_dept = l_ttptext.Substring(0, j);
                    if (l_dept.Equals(l_Dept) == false)
                    {
                        dataGridView1[c, l_rowno].Tag = 1;
                    }
                    else
                    {
                        dataGridView1[c, l_rowno].Tag = 0;
                    }
                    /*
                    if (p_colorHshTbl.ContainsKey(l_dept))
                    {
                        dataGridView1[c, l_rowno].Tag = p_colorHshTbl[l_dept];
                    }
                    else
                    {
                        dataGridView1[c, l_rowno].Tag = Color.Red;
                    }*/
                    dataGridView1[c, l_rowno].ToolTipText = l_ttptext;
                }
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            p_dayOfWeek = (int)dateTimePicker1.Value.DayOfWeek;
            lblError.Text = dateTimePicker1.Value.DayOfWeek.ToString();
            foreach (System.Collections.Hashtable l_hashTble in p_RoomshshTbl.Values)
            {
                l_hashTble.Clear();                
            }
            p_RoomshshTbl.Clear();
            p_deptRoomKeys.Clear();
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            int l_start = 1;
            if (p_connStrings.Length == 1)
            {
                l_start = 0;
            }
            for (int i = l_start; i < p_connStrings.Length; i++)
            {
                collectRoomsDataForADept(p_connStrings[i], p_dbTTS[i], p_dbATs[i], p_depts[i]);
            }
            FillTimeTableTblandBindWithGridViewForRoom();
        }
            

        
    }
}