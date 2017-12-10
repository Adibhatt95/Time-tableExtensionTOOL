using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Data.MPrSQL;

namespace TeachersAssessment
{
    public class tGlobal
    {
        public int NumOfSlotsInWeek = 56;
        public int NumOFActualLectInWeek = 44;
        public int NumOfSlotsSat = 6;
        public int NumOfSlotsInADay = 22;
        public int[] RecessSlots = new int[] { 3, 13, 23, 33, 43, 53, 7, 17, 27, 37, 47 };
        public int RecessLect1 = 3, RecessLect2 = 7;
        string start_time;
        public int lect_min = 45, break1_dur = 15, break2_dur = 45;
        TablerDBDataSet.SettingsDataTable p_setdb = new TablerDBDataSet.SettingsDataTable();
        System.Data.MPrSQL.MPrSQLDataAdapter p_setda = MyTableAdapters.SettingsTableAdapter();
        int p_schoolid = -1;
       // public bool allTeachersExcelExported = false;
        //public static string NLDATAFILE = "NLightsTimeTableData.db";
        //public static string TADATAFILE = "TATable.db";
        //public static string DATAPATH = null;
        public int TeacherIDSelected = 0;

        public enum GRADE_CODES { ALLBE=-1, ALLTE=-2, ALLSE=-3,  ALL=-4 };
        public static string[] GRADE_NAME = { "NONE", "All BE Classes", "All TE Classes", "All SE Classes", "ALL Classes" };
        public static string DEPARTMENT;

        private tGlobal(int l_schoolID)
        {
            p_schoolid = l_schoolID;
        }

        static System.Collections.Hashtable p_instances = new System.Collections.Hashtable();


        public static tGlobal GetInstance(int schoolID)
        {
            tGlobal l_instance = null;
            if (p_instances.ContainsKey(schoolID))
            {
                l_instance = (tGlobal)p_instances[schoolID];
            }
            else
            {
                l_instance = new tGlobal(schoolID);
                p_instances.Add(schoolID, l_instance);
            }
            return l_instance;
        }

        public static void DROPAUTO()
        {
            System.Data.MPrSQL.MPrSQLConnection l_conn = new System.Data.MPrSQL.MPrSQLConnection(TeachersAssessment.Properties.Settings.Default.DataSet1Conn); 
  
            try
            {
                l_conn.Open();
               string s0;
                System.Data.MPrSQL.MPrSQLCommand cmd;
                s0 = "ALTER TABLE Students CHANGE SAPID SAPID BIGINT(20) UNSIGNED NOT NULL;";
                cmd = new System.Data.MPrSQL.MPrSQLCommand(s0, l_conn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception l_e)
            {
                throw new Exception(l_e.Message);
                //return;
            }
            finally
            {
                l_conn.Close();
            }
        }


        public static void CreateAllTables()
        {
            //System.Data.MPrSQL.tGlobal.CreateDatabase("localhost", "3306", "root", "3210", "NLightsTimeTableData");
            //System.Data.MPrSQL.tGlobal.CreateDatabase("localhost", "3306", "root", "3210", "AssessmentTables");
            
            try
            {
                System.Data.MPrSQL.MPrSQLConnection l_conn = new System.Data.MPrSQL.MPrSQLConnection(TeachersAssessment.Properties.Settings.Default.DataSet1Conn);
            }
            catch (Exception l_e)
            {
                //MessageBox.Show("Error in accessing db, " + l_e.Message);
                throw new Exception("Can not open connection, " + l_e.Message);
                //return;
            }

            string l_str = TeachersAssessment.Properties.Settings.Default.DataSet1Conn;
            int l_index = l_str.IndexOf("DataBase", StringComparison.CurrentCultureIgnoreCase);
            string l_connstr = l_str.Remove(l_index);
            l_str = l_str.Remove(0, l_index);
            System.Data.MPrSQL.MPrSQLConnection conn = new System.Data.MPrSQL.MPrSQLConnection(l_connstr);
            l_index = l_str.IndexOf("=");
            l_str = l_str.Remove(0, l_index + 1);
            System.Data.MPrSQL.MPrSQLCommand cmd;
            string s0;

            try
            {
                conn.Open();
                s0 = "CREATE DATABASE IF NOT EXISTS   `" + l_str + "`;";
                cmd = new System.Data.MPrSQL.MPrSQLCommand(s0, conn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception l_e)
            {
                throw new Exception(l_e.Message);
                //return;
            }
            finally
            {               
                conn.Close();
            }

            int l_succ = 0;
            try
            {
                System.Data.MPrSQL.tGlobal.CreateTable(new DataSet1.AssessmentDataTable(), null, out l_succ);
            }
            catch (Exception l_e)
            {
                goto INSCOL;
                //MessageBox.Show("Error  in accessing db, " + l_e.Message);
                //return;
            }
            try
            {
                System.Data.MPrSQL.tGlobal.CreateTable(new DataSet1.StudentsDataTable(), null, out l_succ);
            }
            catch { }

            try
            {
                System.Data.MPrSQL.tGlobal.CreateTable(new DataSet1.AttendanceDataTable(), null, out l_succ);
            }
            catch (Exception e)
            {
                //MessageBox.Show(e.Message);
            }
            try
            {
                System.Data.MPrSQL.tGlobal.CreateTable(new DataSet1.ClassTeachersDataTable(), null, out l_succ);
            }
            catch(Exception e) {
                //MessageBox.Show(e.Message);
            }
            try
            {
                DataSet1.AssessmentDataTable l_atbl = new DataSet1.AssessmentDataTable();
                System.Data.MPrSQL.tGlobal.InsertColumn(l_atbl.TableName, l_atbl.SubjectColumn.ColumnName, "varchar(200)");
            }
            catch
            {
            }
            try
            {
                DataSet1.AssessmentDataTable l_atbl = new DataSet1.AssessmentDataTable();
                System.Data.MPrSQL.tGlobal.InsertColumn(l_atbl.TableName, l_atbl.AdjustedNotconductedColumn.ColumnName, "tinyint(1)");
            }
            catch
            {
            }
            try
            {
               System.Data.MPrSQL.tGlobal.CreateTable(new DataSet1.PasswordsDataTable(), null, out l_succ);
            }
            
            catch
            {
            }


        INSCOL:

            try
            {
                DataSet1.AssessmentDataTable l_atbl = new DataSet1.AssessmentDataTable();
                System.Data.MPrSQL.tGlobal.InsertColumn(l_atbl.TableName, l_atbl.BatchETCColumn.ColumnName, "varchar(200)");
            }
            catch
            {
            }
            return;
        }



        public void Set_TimeTableglobal()
        {
            p_setda.Fill(p_setdb);
            TablerDBDataSet.SettingsRow l_SetRow = p_setdb.FindBySettingIDSchoolID((int)UCSchools.SETTYPE.numoflect, -1);
            if (l_SetRow != null && l_SetRow.IsValueNull() == false)
            {
                int.TryParse(l_SetRow.Value, out NumOfSlotsInADay); // = Convert.ToInt32(l_SetRow.Value);
            }
            l_SetRow = p_setdb.FindBySettingIDSchoolID((int)UCSchools.SETTYPE.numoflectsat, -1);
            if (l_SetRow != null && l_SetRow.IsValueNull() == false)
            {
                int.TryParse(l_SetRow.Value, out NumOfSlotsSat);//NumOfSlotsSat = Convert.ToInt32(l_SetRow.Value);
                NumOfSlotsInWeek = NumOfSlotsInADay * 5 + NumOfSlotsSat;
            }
            l_SetRow = p_setdb.FindBySettingIDSchoolID((int)UCSchools.SETTYPE.break1, -1);
            if (l_SetRow != null && l_SetRow.IsValueNull() == false)
            {
                int.TryParse(l_SetRow.Value, out RecessLect1);
                //RecessLect1 = Convert.ToInt32(l_SetRow.Value);
            }
            l_SetRow = p_setdb.FindBySettingIDSchoolID((int)UCSchools.SETTYPE.break2, -1);

            if (l_SetRow != null && l_SetRow.IsValueNull() == false)
            {
                int.TryParse(l_SetRow.Value, out RecessLect2);
                //RecessLect2 = Convert.ToInt32(l_SetRow.Value);
            }

            l_SetRow = p_setdb.FindBySettingIDSchoolID((int)UCSchools.SETTYPE.break1Dur, -1);
            if (l_SetRow != null && l_SetRow.IsValueNull() == false)
            {
                int.TryParse(l_SetRow.Value, out break1_dur);
                //break1_dur = Convert.ToInt32(l_SetRow.Value);
            }
            l_SetRow = p_setdb.FindBySettingIDSchoolID((int)UCSchools.SETTYPE.break2Dur, -1);
            if (l_SetRow != null && l_SetRow.IsValueNull() == false)
            {
                int.TryParse(l_SetRow.Value, out break2_dur);
                //break2_dur = Convert.ToInt32(l_SetRow.Value);
            }

            List<int> l_list = new List<int>();
            int break1 = RecessLect1;
            int break2 = RecessLect2;
            if (NumOfSlotsInWeek > NumOfSlotsInADay)
            {
                while (break2 <= NumOfSlotsInWeek)
                {
                    l_list.Add(break1);
                    l_list.Add(break2);
                    break1 = break1 + NumOfSlotsInADay;
                    break2 = break2 + NumOfSlotsInADay;
                    if (break2 > NumOfSlotsInWeek && break1 <= NumOfSlotsInWeek)
                    {
                        l_list.Add(break1);
                    }
                }
            }
            RecessSlots = l_list.ToArray();
            l_SetRow = p_setdb.FindBySettingIDSchoolID((int)UCSchools.SETTYPE.start_time, -1);
            if (l_SetRow != null && l_SetRow.IsValueNull() == false)
            {
                start_time = (l_SetRow.Value);
            }
            l_SetRow = p_setdb.FindBySettingIDSchoolID((int)UCSchools.SETTYPE.lect_min, -1);
            if (l_SetRow != null && l_SetRow.IsValueNull() == false)
            {
                int.TryParse(l_SetRow.Value, out lect_min);
                //lect_min = Convert.ToInt32(l_SetRow.Value);
            }
            NumOFActualLectInWeek = NumOfSlotsInWeek - RecessSlots.Length;
        }

        public vIEWDataSet1.TimeTableViewDataTable GetViewTable()
        {
            if (start_time == null)
            {
                Set_TimeTableglobal();
            }
            vIEWDataSet1.TimeTableViewDataTable l_ttv_table = new vIEWDataSet1.TimeTableViewDataTable();

            string[] a = new string[2];
            a = start_time.Split(':');
            int x = Convert.ToInt32(a[0]);
            int y = Convert.ToInt32(a[1]);
            int b, c;
            string l_ampm = " am";
            for (int i = 0; i < NumOfSlotsInADay; i++)
            {
                vIEWDataSet1.TimeTableViewRow l_trow = l_ttv_table.NewTimeTableViewRow();
                if ((i + 1) % NumOfSlotsInADay == RecessLect1)
                {
                    //break1
                    c = y + break1_dur;
                }
                else if ((i + 1) % NumOfSlotsInADay == RecessLect2)
                {
                    //break1
                    c = y + break2_dur;
                }
                else
                {
                    c = y + lect_min;
                }

                b = x;
                if (c >= 60)
                {
                    c = c - 60;
                    b++;
                } 
                if (b == 12 || b == 13)
                {
                    if (b == 13)
                    {
                        b = 1;
                    }
                    l_ampm = " pm";
                }
                l_trow.LectureHr = x.ToString() + ":" + y.ToString("00") + " to " + b.ToString() + ":" + c.ToString("00") + l_ampm;
                l_trow.Monday = (i + 1).ToString();
                l_trow.Tuesday = (i + 1 + NumOfSlotsInADay).ToString();
                l_trow.Wednesday = (i + 1 + NumOfSlotsInADay * 2).ToString();
                l_trow.Thursday = (i + 1 + NumOfSlotsInADay * 3).ToString();
                l_trow.Friday = (i + 1 + NumOfSlotsInADay * 4).ToString();
                l_trow.Saturday = (i + 1 + NumOfSlotsInADay * 5).ToString();
                l_ttv_table.AddTimeTableViewRow(l_trow);
                x = b;
                y = c;
            }
            return l_ttv_table;
        }

        public static string OpenFilePathFromUser()
        {
            OpenFileDialog l_fdiag = new OpenFileDialog();
            l_fdiag.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            l_fdiag.ShowDialog();

            return l_fdiag.FileName;
        }

        public static string SaveFilePathFromUser()
        {
            SaveFileDialog l_fdiag = new SaveFileDialog();
            l_fdiag.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            
            l_fdiag.ShowDialog();

            return l_fdiag.FileName;
        }

        public static string TeacherNameForMulti(TablerDBDataSet.TSFInalRow l_tsFinalRw, int l_tsFinalID, TablerDBDataSet.TSFInalDataTable l_tsfinalTbl,
                                                                                                       TablerDBDataSet.TeachersDataTable l_ttable,
                                                                                                       TablerDBDataSet.SubjectsDataTable l_stable,
                                                                                                       TablerDBDataSet.RoomsDataTable l_rtbl)
        {
            string l_tname = "";
            if (l_tsFinalRw == null)
            {
                l_tsFinalRw = l_tsfinalTbl.FindByID(l_tsFinalID);
            }

            if (l_tsFinalRw == null) return "NA";

            if (l_tsFinalRw.IsTeachersStrNull() || l_tsFinalRw.IsSubjRoomStrNull())
            {
                TablerDBDataSet.SubjectsRow l_srw1 = l_stable.FindBySubjectID(l_tsFinalRw.SubjectsID);
                TablerDBDataSet.TeachersRow l_trw1 = l_ttable.FindByTeachersID (l_tsFinalRw.TeachersID);
                TablerDBDataSet.RoomsRow l_rrw1 =  l_rtbl.FindByRoomsID(l_tsFinalRw.RoomID);

                if (l_trw1 != null && l_trw1.IsTeacherAbbrNull() == false)
                {
                    l_tname += l_trw1.TeacherAbbr;
                }
                else
                {
                    l_tname += "NA";
                }
                if (l_srw1 != null)
                {
                    l_tname += "(" + l_srw1.SubjectName + ")";
                }
                if (l_rrw1 != null)
                {
                    l_tname += "{" + l_rrw1.RoomsName + "}";
                }
                goto RET;
            }
            string[] l_tids = l_tsFinalRw.TeachersStr.Split(',');
            string[] l_subroom = l_tsFinalRw.SubjRoomStr.Split(',');
            int i = 0;
            foreach (string l_tid in l_tids)
            {
                int id = -1;
                int.TryParse(l_tid, out id);

                TablerDBDataSet.TeachersRow l_trw = l_ttable.FindByTeachersID(id);
                if (l_trw == null) goto RET;

                string[] l_sr = l_subroom[i].Split(':');

                int.TryParse(l_sr[0], out id);

                TablerDBDataSet.SubjectsRow l_srw = l_stable.FindBySubjectID(id);
                if (l_srw == null) goto RET;

                int.TryParse(l_sr[1], out id);

                TablerDBDataSet.RoomsRow l_rrw = l_rtbl.FindByRoomsID(id);
                if (l_rrw == null)
                {
                    l_tname += l_trw.TeacherAbbr + "(" + l_srw.SubjectName + ")" + "{" + "NA" + "}, ";
                }
                else
                {
                    l_tname += l_trw.TeacherAbbr + "(" + l_srw.SubjectName + ")" + "{" + l_rrw.RoomsName + "}, ";
                }
                i++;
            }

        RET:
            return l_tname;
        }

    }
}