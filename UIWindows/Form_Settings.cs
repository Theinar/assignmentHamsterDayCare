using System;
using UI;
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

        private void Button_Submit_Click(object sender, EventArgs e)
        {

            //Variables to be sent to build new args with
            int? _MaxnrOfHamInEachCage = null;
            int? _MaxnrOfHamInExArea = null;
            int? _NumberOfcages = null;
            int? _NumberOfExAreas = null;
            string _FilePath = null;
            DateTime? _FictionalDate = DateTime.Now;
            int? _EndTick = null;
            int? _TickInMilliseconds = null;

            // bool that stays true if all variables gets parsed in the right format
            bool everythingParseble = true;

            // Taking string from textbox to parse into datetime
            string dateString = this.textBox_set_Fictional_Date.Text;


            string sevenOclock = " 07:00:00:0000";
           string startsFromString = dateString + sevenOclock;
            string format = "yyyyMMdd HH:mm:ss:ffff";


            try
            {
                _FictionalDate = DateTime.ParseExact(startsFromString, format,
                                         CultureInfo.InvariantCulture);
                _EndTick = int.Parse(this.textBox_set_nr_of_days.Text);
                _TickInMilliseconds = int.Parse(this.textBox_Set_ticks_Per_Second.Text) / 1000;
            }
            catch (Exception ex)
            {
                everythingParseble = false;
            }

            if (!Paul_Standard_Checked)
            {
                _MaxnrOfHamInEachCage = 3;
                _MaxnrOfHamInExArea = 6;
                _NumberOfcages = 10;
                _NumberOfExAreas = 1;
                _FilePath = "Hamsterlista30.csv";
            }
            else
            {

                try
                {
                    if (checkBox_Import_csv.Checked)
                    {
                        _MaxnrOfHamInEachCage = int.Parse(this.checkBox_Import_csv.Text);
                    }
                    if (checkBox_Change_ExArea_cap.Checked)
                    {
                        _MaxnrOfHamInExArea = int.Parse(this.checkBox_Change_ExArea_cap.Text);
                    }
                    if (checkBox_Change_nr_of_cages.Checked)
                    {
                        _NumberOfcages = int.Parse(this.textBox_Change_number_of_cages.Text);
                    }
                    if (checkBox_Change_number_Of_ExAreas.Checked)
                    {
                        _NumberOfExAreas = int.Parse(this.textBox_Change_Cage_Cap.Text);
                    }
                    if (checkBox_Import_csv.Checked)
                    {
                        _FilePath = "HamsterlistaCustom.csv";
                    }
                }
                catch (Exception)
                {
                    everythingParseble = false;
                }

            }

            if (everythingParseble == true)
            {

                Program.ChangeTheArgs(_MaxnrOfHamInEachCage,
                _MaxnrOfHamInExArea,
                _NumberOfcages,
                 _NumberOfExAreas,
                _FilePath,
                 _FictionalDate,
                 _EndTick,
                _TickInMilliseconds);
                MessageBox.Show("Your settings has now been applied");
            }
            else
            {
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
