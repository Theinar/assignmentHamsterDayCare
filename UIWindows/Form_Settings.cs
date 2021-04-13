using System;
using HamsterDayCare.Domain;
using System.IO;
using HamsterDayCare.Data;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;

namespace UIWindows
{
    public partial class Form_Settings : Form
    {
        static bool isShowing;
        public bool Paul_Standard_Checked { get; set; }
        public static bool IsShowing { get => isShowing; private set => isShowing = value; }

        public Form_Settings()
        {
            IsShowing = true;
            InitializeComponent();
            Paul_Standard_Checked = true;
            this.label1.Text = "VARNING! If you get greedy\n" +
                "with the settings the application \n" +
                "will crash! Be responsible!";
        }
        private void radioButton_Paulstandard_Click_Changed(object sender, EventArgs e)
        {
            Paul_Standard_Checked = true;
            // If Palstandard is chosen the rest of the settings labels gets hidden
            this.label_Change_Cage_Cap.Visible = true;
            this.label_Change_ExArea_Cap.Visible = true;
            this.label_Change_Nr_ExAreas.Visible = true;
            this.label_Change_NR_Of_Cages.Visible = true;
            this.label_Import_Clientele.Visible = true;
            // same with thair checkboxes
            this.checkBox_Cange_Cage_cap.Visible = true;
            this.checkBox_Change_ExArea_cap.Visible = true;
            this.checkBox_Change_nr_of_cages.Visible = true;
            this.checkBox_Change_number_Of_ExAreas.Visible = true;
            this.checkBox_Import_csv.Visible = true;
        }
        private void radioButton_Cunstom_Click_Changed(object sender, EventArgs e)
        {
            Paul_Standard_Checked = false;

            // If Palstandard is chosen the rest of the settings labels gets hidden
            this.label_Change_Cage_Cap.Visible = false;
            this.label_Change_ExArea_Cap.Visible = false;
            this.label_Change_Nr_ExAreas.Visible = false;
            this.label_Change_NR_Of_Cages.Visible = false;
            this.label_Import_Clientele.Visible = false;
            // same with thair checkboxes
            this.checkBox_Cange_Cage_cap.Visible = false;
            this.checkBox_Change_ExArea_cap.Visible = false;
            this.checkBox_Change_nr_of_cages.Visible = false;
            this.checkBox_Change_number_Of_ExAreas.Visible = false;
            this.checkBox_Import_csv.Visible = false;
            // and sets thair properties to unchecked
            this.checkBox_Cange_Cage_cap.Checked = false;
            this.checkBox_Change_ExArea_cap.Checked = false;
            this.checkBox_Change_nr_of_cages.Checked = false;
            this.checkBox_Change_number_Of_ExAreas.Checked = false;
            this.checkBox_Import_csv.Checked = false;

        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            if (e.CloseReason == CloseReason.WindowsShutDown) return;
            IsShowing = false;
            e.Cancel = true;
            this.Dispose();

        }
        /// <summary>
        /// This button click sets new settings for simulation. This is done by constructing a new tickerargs and sending it to 
        /// program where it replaces the old one
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Submit_Click(object sender, EventArgs e)
        {
            MakeNewArgsAndDBFromSettings();
        }

        private void MakeNewArgsAndDBFromSettings()
        {
            TickerArgs newArgs = new TickerArgs();

            // bool that stays true if all variables gets parsed in the right format
            bool everythingParseble = true;

            // Taking string from textbox to parse into datetime
            string dateString = this.textBox_set_Fictional_Date.Text;

            // strings that is used to parse Datetime in right format
            string sevenOclock = " 07:00:00:0000";
            string startsFromString = dateString + sevenOclock;
            string format = "yyyyMMdd HH:mm:ss:ffff";

            // Try tests if users format valid
            try
            {
                newArgs.FictionalStartDate = DateTime.ParseExact(startsFromString, format,
                                         CultureInfo.InvariantCulture);
                newArgs.EndTick = int.Parse(this.textBox_set_nr_of_days.Text) * 100;
                newArgs.TickInMilliseconds = 1000 / (int.Parse(this.textBox_Set_ticks_Per_Second.Text));
                newArgs.SettingsID = TickerArgs.settingsID + 1;
            }
            catch (Exception ex)
            {
                // sets to fals if exeprion is cought
                everythingParseble = false;
            }

            // Sets the values that are effected by paulstandard 
            if (!Paul_Standard_Checked)
            {
                newArgs.MaxnrOfHamInEachCage = 3;
                newArgs.MaxnrOfHamInExArea = 6;
                newArgs.NumberOfcages = 10;
                newArgs.NumberOfExAreas = 1;
                newArgs.FilePath = "Hamsterlista30.csv";
            }
            // else if Custom is selected in form
            else
            {
                // trys to catch format incorrection
                try
                {
                    // IFs to check if user has checked boxex in Settings form
                    if (checkBox_Cange_Cage_cap.Checked)
                    {
                        newArgs.MaxnrOfHamInEachCage = int.Parse(this.textBox_Change_Cage_Cap.Text);
                    }
                    if (checkBox_Change_ExArea_cap.Checked)
                    {
                        newArgs.MaxnrOfHamInExArea = int.Parse(this.textBox_Change_EXArea_cap.Text);
                    }
                    if (checkBox_Change_nr_of_cages.Checked)
                    {
                        newArgs.NumberOfcages = int.Parse(this.textBox_Change_number_of_cages.Text);
                    }
                    if (checkBox_Change_number_Of_ExAreas.Checked)
                    {
                        newArgs.NumberOfExAreas = int.Parse(this.textBox_Change_Cage_Cap.Text);
                    }
                    if (checkBox_Import_csv.Checked)
                    {
                        newArgs.FilePath = "HamsterlistaCustom.csv";
                    }
                }
                catch (Exception)
                {
                    // sets to false if exeprion is cought
                    everythingParseble = false;
                }

            }

            // If no format exeptions where cought everythingParseble is true and newArgs is sent to Program Class
            if (everythingParseble == true)
            {
                Program.ChangeTheArgAndRebuildFromSettings(newArgs);

                using (StreamWriter saveSettings = new StreamWriter("Settings.csv"))
                {
                    saveSettings.Write($"{newArgs.EndTick}," +
                                        $"{newArgs.FictionalStartDate}," +
                                        $"{newArgs.FilePath}," +
                                        $"{newArgs.MaxnrOfHamInEachCage}," +
                                        $"{newArgs.MaxnrOfHamInExArea}," +
                                        $"{newArgs.NumberOfcages}," +
                                        $"{newArgs.NumberOfExAreas}," +
                                        $"{newArgs.NumberOfTicks}," +
                                        $"{newArgs.CanselationRequest}," +
                                        $"{newArgs.SimulationTime}," +
                                        $"{newArgs.TickInMilliseconds}," +
                                        $"{newArgs.SettingsID}");
                }
                // Notefies user that new settings are in place
                MessageBox.Show("Your settings has now been applied\nPlease restart application fore new settings work propperly");
            }
            else
            {
                // Notefies use that something is wrong with format
                MessageBox.Show("It seems there is something wrong with your formating\n" +
                                "Please check if all checked feilds are in correct format");
            }
        }



        #region Logics of Checkboxes

        private void checkBox_Cange_Cage_cap_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBox_Cange_Cage_cap.Checked)
            {
                this.textBox_Change_Cage_Cap.Visible = true;
            }
            if (!this.checkBox_Cange_Cage_cap.Checked)
            {
                this.textBox_Change_Cage_Cap.Visible = false;
            }
        }

        private void checkBox_Change_nr_of_cages_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBox_Change_nr_of_cages.Checked)
            {
                this.textBox_Change_number_of_cages.Visible = true;
            }
            if (!this.checkBox_Change_nr_of_cages.Checked)
            {
                this.textBox_Change_number_of_cages.Visible = false;
            }
        }

        private void checkBox_Change_number_Of_ExAreas_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBox_Change_number_Of_ExAreas.Checked)
            {
                this.textBox_change_nr_of_EXAREA.Visible = true;
            }
            if (!this.checkBox_Change_number_Of_ExAreas.Checked)
            {
                this.textBox_change_nr_of_EXAREA.Visible = false;
            }
        }

        private void checkBox_Change_ExArea_cap_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBox_Change_ExArea_cap.Checked)
            {
                this.textBox_Change_EXArea_cap.Visible = true;
            }
            if (!this.checkBox_Change_ExArea_cap.Checked)
            {
                this.textBox_Change_EXArea_cap.Visible = false;
            }
        } 
        #endregion


    }
}
