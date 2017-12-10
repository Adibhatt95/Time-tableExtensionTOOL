using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace TeachersAssessment
{
    public partial class UCSchools : UserControl
    {
        public enum SETTYPE{none=0, min_mins=1, lect_min=2, start_time=3, numoflect=4, numoflectsat=5, break1=6, break2=7, break1Dur=8, break2Dur=9};
        System.Data.MPrSQL.MPrSQLDataAdapter settingTableADapter = MyTableAdapters.SettingsTableAdapter();

        public UCSchools()
        {
            InitializeComponent();
            schoolsTableAdapter = MyTableAdapters.SchoolsTableAdapter();

            schoolsTableAdapter.Fill(tablerDBDataSet.Schools);
           
            settingTableADapter.Fill(tablerDBDataSet.Settings);
            
            //start time
            TablerDBDataSet.SettingsRow l_settings = tablerDBDataSet.Settings.FindBySettingIDSchoolID((int)SETTYPE.start_time, -1);
            if (l_settings != null && l_settings.IsValueNull() == false) tbTimeFrom.Text = l_settings.Value;
            

            //num of lects
            l_settings = tablerDBDataSet.Settings.FindBySettingIDSchoolID((int)SETTYPE.numoflect, -1);

            if (l_settings != null && l_settings.IsValueNull() == false) tbNumPeriod.Text = l_settings.Value;


            //lect_min
            l_settings = tablerDBDataSet.Settings.FindBySettingIDSchoolID((int)SETTYPE.lect_min, -1);

            if (l_settings != null && l_settings.IsValueNull() == false) tbDurPeriod.Text = l_settings.Value;
            
            //dur break1
            l_settings = tablerDBDataSet.Settings.FindBySettingIDSchoolID((int)SETTYPE.break1, -1);

            if (l_settings != null && l_settings.IsValueNull() == false) tbFirstBreakDur.Text = l_settings.Value;


            //dur break2
            l_settings = tablerDBDataSet.Settings.FindBySettingIDSchoolID((int)SETTYPE.break2, -1);

            if (l_settings != null && l_settings.IsValueNull() == false) tbSecondBrkDur.Text = l_settings.Value;

            //min minutes of lect
            l_settings = tablerDBDataSet.Settings.FindBySettingIDSchoolID((int)SETTYPE.min_mins, -1);

            if (l_settings != null && l_settings.IsValueNull() == false) tbMinDur.Text = l_settings.Value;
            
            //num of lects on saturday
            l_settings = tablerDBDataSet.Settings.FindBySettingIDSchoolID((int)SETTYPE.numoflectsat, -1);

            if (l_settings != null && l_settings.IsValueNull() == false) tbNumPeriodSat.Text=l_settings.Value;

            l_settings = tablerDBDataSet.Settings.FindBySettingIDSchoolID((int)SETTYPE.break1Dur, -1);

            if (l_settings != null && l_settings.IsValueNull() == false) tbBreak1Dur.Text = l_settings.Value;

            l_settings = tablerDBDataSet.Settings.FindBySettingIDSchoolID((int)SETTYPE.break2Dur, -1);

            if (l_settings != null && l_settings.IsValueNull() == false) tbBreak2Dur.Text = l_settings.Value;

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            schoolsTableAdapter.Update(tablerDBDataSet.Schools);
            MessageBox.Show("Saved");
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            schoolsTableAdapter.Fill(tablerDBDataSet.Schools);
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            //////////////// START TIME
            TablerDBDataSet.SettingsRow l_settings = tablerDBDataSet.Settings.FindBySettingIDSchoolID((int)SETTYPE.start_time,-1);
             if(l_settings==null)
            {
                l_settings = tablerDBDataSet.Settings.NewSettingsRow();
                l_settings.SchoolID = -1;
                l_settings.SettingID = (int)SETTYPE.start_time;
            }  
            l_settings.Value = tbTimeFrom.Text;
            if (l_settings.RowState == DataRowState.Detached)
            {
                tablerDBDataSet.Settings.AddSettingsRow(l_settings);
            }

            ///////////NUM OF LECTS
            l_settings = tablerDBDataSet.Settings.FindBySettingIDSchoolID((int)SETTYPE.numoflect, -1);

            if (l_settings == null)
            {
                l_settings = tablerDBDataSet.Settings.NewSettingsRow();
                l_settings.SchoolID = -1;
                l_settings.SettingID = (int)SETTYPE.numoflect;
            }
            l_settings.Value = tbNumPeriod.Text;
            if (l_settings.RowState == DataRowState.Detached)
            {
                tablerDBDataSet.Settings.AddSettingsRow(l_settings);
            }

            ///////////////////LECT DURATION
            l_settings = tablerDBDataSet.Settings.FindBySettingIDSchoolID((int)SETTYPE.lect_min, -1);

            if (l_settings == null)
            {
                l_settings = tablerDBDataSet.Settings.NewSettingsRow();
                l_settings.SchoolID = -1;
                l_settings.SettingID = (int)SETTYPE.lect_min;
            }
            l_settings.Value = tbDurPeriod.Text;
            if (l_settings.RowState == DataRowState.Detached)
            {
                tablerDBDataSet.Settings.AddSettingsRow(l_settings);
            }

            ///////////////////////DUR OF 1ST BREAK
            l_settings = tablerDBDataSet.Settings.FindBySettingIDSchoolID((int)SETTYPE.break1, -1);

            if (l_settings == null)
            {
                l_settings = tablerDBDataSet.Settings.NewSettingsRow();
                l_settings.SchoolID = -1;
                l_settings.SettingID = (int)SETTYPE.break1;
            }
            l_settings.Value = tbFirstBreakDur.Text;
            if (l_settings.RowState == DataRowState.Detached)
            {
                tablerDBDataSet.Settings.AddSettingsRow(l_settings);
            }

            //////////////////DUR OF SECOND BREAK
            l_settings = tablerDBDataSet.Settings.FindBySettingIDSchoolID((int)SETTYPE.break2, -1);

            if (l_settings == null)
            {
                l_settings = tablerDBDataSet.Settings.NewSettingsRow();
                l_settings.SchoolID = -1;
                l_settings.SettingID = (int)SETTYPE.break2;
            }
            l_settings.Value = tbSecondBrkDur.Text;
            if (l_settings.RowState == DataRowState.Detached)
            {
                tablerDBDataSet.Settings.AddSettingsRow(l_settings);
            }

            //////////////MINIMUM DURATION
            l_settings = tablerDBDataSet.Settings.FindBySettingIDSchoolID((int)SETTYPE.min_mins, -1);

            if (l_settings == null)
            {
                l_settings = tablerDBDataSet.Settings.NewSettingsRow();
                l_settings.SchoolID = -1;
                l_settings.SettingID = (int)SETTYPE.min_mins;
            }
            l_settings.Value = tbMinDur.Text;
            if (l_settings.RowState == DataRowState.Detached)
            {
                tablerDBDataSet.Settings.AddSettingsRow(l_settings);
            }

            ////////////////NUM LECT ON SATURDAY
            l_settings = tablerDBDataSet.Settings.FindBySettingIDSchoolID((int)SETTYPE.numoflectsat, -1);

            if (l_settings == null)
            {
                l_settings = tablerDBDataSet.Settings.NewSettingsRow();
                l_settings.SchoolID = -1;
                l_settings.SettingID = (int)SETTYPE.numoflectsat;
            }
            l_settings.Value = tbNumPeriodSat.Text;
            if (l_settings.RowState == DataRowState.Detached)
            {
                tablerDBDataSet.Settings.AddSettingsRow(l_settings);
            }

            ////////////////break1Dur
            l_settings = tablerDBDataSet.Settings.FindBySettingIDSchoolID((int)SETTYPE.break1Dur, -1);

            if (l_settings == null)
            {
                l_settings = tablerDBDataSet.Settings.NewSettingsRow();
                l_settings.SchoolID = -1;
                l_settings.SettingID = (int)SETTYPE.break1Dur;
            }
            l_settings.Value = tbBreak1Dur.Text;
            if (l_settings.RowState == DataRowState.Detached)
            {
                tablerDBDataSet.Settings.AddSettingsRow(l_settings);
            }

            ////////////////break2Dur
            l_settings = tablerDBDataSet.Settings.FindBySettingIDSchoolID((int)SETTYPE.break2Dur, -1);

            if (l_settings == null)
            {
                l_settings = tablerDBDataSet.Settings.NewSettingsRow();
                l_settings.SchoolID = -1;
                l_settings.SettingID = (int)SETTYPE.break2Dur;
            }
            l_settings.Value = tbBreak2Dur.Text;
            if (l_settings.RowState == DataRowState.Detached)
            {
                tablerDBDataSet.Settings.AddSettingsRow(l_settings);
            }


            settingTableADapter.Update(tablerDBDataSet.Settings);

            tGlobal l_global = tGlobal.GetInstance(-1);
            l_global.Set_TimeTableglobal();

            MessageBox.Show("Saved for school with ID: " + comboBox1.SelectedValue);

        }       

    }
}
