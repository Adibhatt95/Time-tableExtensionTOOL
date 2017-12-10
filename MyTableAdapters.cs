using System;
using System.Collections.Generic;
using System.Text;
using System.Data.MPrSQL;

namespace TeachersAssessment
{
    public class MyTableAdapters
    {
        public static MPrSQLConnection p_conn = null;
        public static MPrSQLConnection p_connDataSet1 = null;
        public static TablerDBDataSet tablerDataSet = new TablerDBDataSet();
        public static DataSet1 DS1 = new DataSet1();


        public static void OpenConnections()
        {
            p_conn = new MPrSQLConnection(TeachersAssessment.Properties.Settings.Default.TablerDBConnectionString);
            p_connDataSet1 = new MPrSQLConnection(TeachersAssessment.Properties.Settings.Default.DataSet1Conn);
        }

        public static void CloseConnections()
        {
            if (p_conn != null && p_connDataSet1 != null)
            {
                p_conn.Close();
                p_conn.Dispose();

                p_connDataSet1.Close();
                p_connDataSet1.Dispose();
            }
        }

        public static MPrSQLDataAdapter SchoolsTableAdapter()
        {
            MPrSQLDataAdapter l_da = new MPrSQLDataAdapter();
            l_da.SelectCommand = new MPrSQLCommand("SELECT * FROM " +  tablerDataSet.Schools.TableName, p_conn);
            System.Data.MPrSQL.MPrSQLCommandBuilder builder = new MPrSQLCommandBuilder(l_da);

            return l_da;
        }

        public static MPrSQLDataAdapter SettingsTableAdapter()
        {
            MPrSQLDataAdapter l_da = new MPrSQLDataAdapter();
            l_da.SelectCommand = new MPrSQLCommand("SELECT * FROM " + tablerDataSet.Settings.TableName, p_conn);
            System.Data.MPrSQL.MPrSQLCommandBuilder builder = new MPrSQLCommandBuilder(l_da);

            return l_da;
        }

        public static MPrSQLDataAdapter GradesTableAdapter()
        {
            MPrSQLDataAdapter l_da = new MPrSQLDataAdapter();
            l_da.SelectCommand = new MPrSQLCommand("SELECT * FROM " + tablerDataSet.Grades.TableName, p_conn);
            System.Data.MPrSQL.MPrSQLCommandBuilder builder = new MPrSQLCommandBuilder(l_da);

            return l_da;
        }


        public static MPrSQLDataAdapter GradesPreAssignTableAdapter()
        {
            MPrSQLDataAdapter l_da = new MPrSQLDataAdapter();
            l_da.SelectCommand = new MPrSQLCommand("SELECT * FROM " + tablerDataSet.GradesPreAssign.TableName, p_conn);
            System.Data.MPrSQL.MPrSQLCommandBuilder builder = new MPrSQLCommandBuilder(l_da);

            return l_da;
        }

        public static MPrSQLDataAdapter TeachersTableAdapter()
        {
            MPrSQLDataAdapter l_da = new MPrSQLDataAdapter();
            l_da.SelectCommand = new MPrSQLCommand("SELECT * FROM " + tablerDataSet.Teachers.TableName, p_conn);
            System.Data.MPrSQL.MPrSQLCommandBuilder builder = new MPrSQLCommandBuilder(l_da);

            return l_da;
        }


        public static MPrSQLDataAdapter TSFInalTableAdapter()
        {
            MPrSQLDataAdapter l_da = new MPrSQLDataAdapter();
            l_da.SelectCommand = new MPrSQLCommand("SELECT * FROM " + tablerDataSet.TSFInal.TableName, p_conn);
            System.Data.MPrSQL.MPrSQLCommandBuilder builder = new MPrSQLCommandBuilder(l_da);


            return l_da;
        }
        
        
      
        public static MPrSQLDataAdapter PreAssignTableAdapter()
        {
            MPrSQLDataAdapter l_da = new MPrSQLDataAdapter();
            l_da.SelectCommand = new MPrSQLCommand("SELECT * FROM " + tablerDataSet.PreAssign.TableName, p_conn);
            System.Data.MPrSQL.MPrSQLCommandBuilder builder = new MPrSQLCommandBuilder(l_da);

            builder.QuotePrefix = "[";
            builder.QuoteSuffix = "]";

            return l_da;
        }

        public static MPrSQLDataAdapter DepartmentsTableAdapter()
        {
            MPrSQLDataAdapter l_da = new MPrSQLDataAdapter();
            l_da.SelectCommand = new MPrSQLCommand("SELECT * FROM " + tablerDataSet.Departments.TableName, p_conn);
            System.Data.MPrSQL.MPrSQLCommandBuilder builder = new MPrSQLCommandBuilder(l_da);

            return l_da;
        }

        public static MPrSQLDataAdapter RoomsTableAdapter()
        {
            MPrSQLDataAdapter l_da = new MPrSQLDataAdapter();
            l_da.SelectCommand = new MPrSQLCommand("SELECT * FROM " + tablerDataSet.Rooms.TableName, p_conn);
            System.Data.MPrSQL.MPrSQLCommandBuilder builder = new MPrSQLCommandBuilder(l_da);

            return l_da;
        }

        public static MPrSQLDataAdapter ClassTeachersAdapter()
        {
            MPrSQLDataAdapter l_da = new MPrSQLDataAdapter();
          
            if (p_connDataSet1.State == System.Data.ConnectionState.Closed)
            {
                p_connDataSet1.Open();
            }
            l_da.SelectCommand = new MPrSQLCommand("SELECT * FROM " + DS1.ClassTeachers.TableName, p_connDataSet1);// tablerDataSet.Rooms.TableName, p_conn);
            System.Data.MPrSQL.MPrSQLCommandBuilder builder = new MPrSQLCommandBuilder(l_da);

            return l_da;
        }
        public static MPrSQLDataAdapter PasswordsAdapter()
        {
            MPrSQLDataAdapter l_da = new MPrSQLDataAdapter();

            if (p_connDataSet1.State == System.Data.ConnectionState.Closed)
            {
                p_connDataSet1.Open();
            }
            l_da.SelectCommand = new MPrSQLCommand("SELECT * FROM " + DS1.Passwords.TableName, p_connDataSet1);// tablerDataSet.Rooms.TableName, p_conn);
            System.Data.MPrSQL.MPrSQLCommandBuilder builder = new MPrSQLCommandBuilder(l_da);

            return l_da;
        }
        public static MPrSQLDataAdapter SubjectsTableAdapter()
        {
            MPrSQLDataAdapter l_da = new MPrSQLDataAdapter();
            l_da.SelectCommand = new MPrSQLCommand("SELECT * FROM " + tablerDataSet.Subjects.TableName, p_conn);
            System.Data.MPrSQL.MPrSQLCommandBuilder builder = new MPrSQLCommandBuilder(l_da);

            return l_da;
        }

        public static MPrSQLDataAdapter AllocationTableTableAdapter()
        {
            MPrSQLDataAdapter l_da = new MPrSQLDataAdapter();
            l_da.SelectCommand = new MPrSQLCommand("SELECT * FROM " + tablerDataSet.AllocationTable.TableName, p_conn);
            System.Data.MPrSQL.MPrSQLCommandBuilder builder = new MPrSQLCommandBuilder(l_da);


            return l_da;
        }

        public static MPrSQLDataAdapter AssessTableAdapter()
        {
            MPrSQLDataAdapter l_da = new MPrSQLDataAdapter();
            if (p_connDataSet1.State == System.Data.ConnectionState.Closed)
            {
                p_connDataSet1.Open();
            }
            l_da.SelectCommand = new MPrSQLCommand("SELECT * FROM " + DS1.Assessment.TableName, p_connDataSet1);
            System.Data.MPrSQL.MPrSQLCommandBuilder builder = new MPrSQLCommandBuilder(l_da);

            return l_da;
        }


        public static MPrSQLDataAdapter StudentsTableAdapter()
        {
            MPrSQLDataAdapter l_da = new MPrSQLDataAdapter();
            if (p_connDataSet1.State == System.Data.ConnectionState.Closed)
            {
                p_connDataSet1.Open();
            } 
            l_da.SelectCommand = new MPrSQLCommand("SELECT * FROM " + DS1.Students.TableName, p_connDataSet1);
            System.Data.MPrSQL.MPrSQLCommandBuilder builder = new MPrSQLCommandBuilder(l_da);

            return l_da;
        }
        public static MPrSQLDataAdapter AttendanceTableAdapter()
        {
           
            MPrSQLDataAdapter l_da = new MPrSQLDataAdapter();
            if (p_connDataSet1.State == System.Data.ConnectionState.Closed)
            {
                p_connDataSet1.Open();
            }
            l_da.SelectCommand = new MPrSQLCommand("SELECT * FROM " + DS1.Attendance.TableName, p_connDataSet1);
            System.Data.MPrSQL.MPrSQLCommandBuilder builder = new MPrSQLCommandBuilder(l_da);

            return l_da;
        }

        static MPrSQLTransaction p_transDataSet1 = null;
        public static void BeginTransDataSet()
        {
            if (p_connDataSet1.State == System.Data.ConnectionState.Closed)
            {
                p_connDataSet1.Open();
            }
            p_transDataSet1 = p_connDataSet1.BeginTransaction();
        }

        public static void EndTransactionDataSet()
        {
            if (p_transDataSet1 != null)
            {
                p_transDataSet1.Commit();
                p_transDataSet1.Dispose();
            }
        }

        static MPrSQLTransaction p_trans = null;
        public static void BeginTransaction()
        {
            if (p_conn.State == System.Data.ConnectionState.Closed)
            {
                p_conn.Open();
            }
            p_trans = p_conn.BeginTransaction();
        }

        public static void EndTransaction()
        {
            p_trans.Commit();
        }



    }
}
