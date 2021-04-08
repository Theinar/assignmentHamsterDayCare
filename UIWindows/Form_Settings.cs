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

        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void radioButton_Paulstandard_Click_Changed(object sender, EventArgs e)
        {
            Paul_Standard_Checked = true;
        }
        private void radioButton_Cunstom_Click_Changed(object sender, EventArgs e)
        {
            Paul_Standard_Checked = false;

        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            if (e.CloseReason == CloseReason.WindowsShutDown) return;
            IsShowing = false;
            e.Cancel = true;
            this.Dispose();

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void Button_Submit_Click(object sender, EventArgs e)
        {

            //Variables to be sent to build new args with
            int? _MaxnrOfHamInEachCage = null;
            int? _MaxnrOfHamInExArea = null;
            int? _NumberOfcages = null;
            int? _NumberOfExAreas = null;
            string _FilePath = null;
            DateTime? _FictionalDate = null;
            int? _EndTick = null;
            int? _TickInMilliseconds = null;

            // bool that stays true if all variables gets parsed in the right format
            bool everythingParseble = true;

            // Taking string from textbox to parse into datetime
            string dateString = this.textBox_Cage_cap.Text;


            string sevenOclock = " 07:00:00:0000";
            string startsFromString = dateString + sevenOclock;
            string format = "yyyy.MM.dd HH:mm:ss:ffff";


            try
            {
                _FictionalDate = DateTime.ParseExact(startsFromString, format,
                                         CultureInfo.InvariantCulture);
                _EndTick = int.Parse(this.textBox_set_nr_of_days.Text);
                _TickInMilliseconds = int.Parse(this.textBox_Set_ticks_Per_Second.Text) / 1000;
            }
            catch (Exception)
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
                        _NumberOfExAreas = int.Parse(this.textBox_Change_nr_of_exAreas.Text);
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
                var newArgs = new
                {
                   maxnrOfHamInEachCage = _MaxnrOfHamInEachCage,
                   maxnrOfHamInExArea = _MaxnrOfHamInExArea,                
                   numberOfcages = _NumberOfcages,
                   numberOfExAreas = _NumberOfExAreas,
                   filePath = _FilePath,
                   fictionalDate = _FictionalDate,
                   endTick = _EndTick,
                   tickInMilliseconds = _TickInMilliseconds
                };

                Program.ChangeTheArgs(newArgs);
                MessageBox.Show("Your settings has now been applied");
            }
            else
            {
                MessageBox.Show("It seems there is something wrong with your formating\n" +
                                "Please check if all checked feilds are in correct format");
            }
        
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox_Cage_cap_ReadOnlyChanged(object sender, EventArgs e)
        {

        }
    }
}
