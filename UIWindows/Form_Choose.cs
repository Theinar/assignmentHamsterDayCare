using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UIWindows
{
    public partial class Form_Choose : Form
    {
        List<String> labelStrings;
        static bool isShowing;


        public static bool IsShowing { get => isShowing;}
        public List<string> LabelStrings { get => labelStrings;}

        public Form_Choose(List<String> _labelString)
        {
            isShowing = true;
            labelStrings = _labelString;
            InitializeComponent();
            Label_Head_Choose.Text = labelStrings[1];
            
        }

        private void Form_Choose_Load(object sender, EventArgs e)
        {

        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);



            if (e.CloseReason == CloseReason.WindowsShutDown) return;
            isShowing = false;
            e.Cancel = true;
            this.Dispose();

        }

        private void Button_Submit_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBox_For_ID.Text, out int idWeareLookingFor))
            {
                if (this.labelStrings[0] == "Choose Hamster")
                {
                    ReportArgs.IsTrackingHamster = true;
                }
                else if (this.labelStrings[0] == "Choose Cage")
                {
                    ReportArgs.IsTrackingHamster = false;
                }
                ReportArgs.TrackingID = idWeareLookingFor;
            }
            else
            {
                MessageBox.Show("Invalid Entry\nNo ID was chosen");
            }
            this.Close();
            
        }

    }
}
//MessageBox.Show("YES");