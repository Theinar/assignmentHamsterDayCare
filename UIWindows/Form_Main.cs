using System;
using System.Collections.Generic;
using HamsterDayCare.Domain;
using HamsterDayCare.Data;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace UIWindows
{
    internal partial class Form_Main : Form
    {

        static BackendLogic dayCareBackEnd;
        static HDCDbContext hDCDbContext;
        
        TickerArgs theArgs;
        ReportArgs reportArgs;

        Ticker theTicker;

        delegate void SetTextCallback(string text);

        public static bool reportRelease = false;
        public static bool reportAwaiter_whilebool = true;

        public Form_Main(HDCDbContext _hDCDbContext
            , ReportArgs _reportArgs
            , Ticker _theTicker)
        {
            InitializeComponent();
            theArgs =  new TickerArgs();
            hDCDbContext = _hDCDbContext;
            reportArgs = _reportArgs;
            theTicker = _theTicker;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button_Hamster_Click(object sender, EventArgs e)
        {
            List<string> labelString = new List<string>();
            labelString.Add("Choose Hamster");
            labelString.Add("Enter the ID of the hamster you wich to see");
            if (!Form_Choose.IsShowing)
            {
                Form_Choose form = new Form_Choose(labelString);
                form.Show();
            }
        }

        private void button_Owner_Click(object sender, EventArgs e)
        {
            List<string> labelString = new List<string>();
            labelString.Add("Choose Owner");
            labelString.Add("Enter the ID of the Owner you wich to see");
            if (!Form_Choose.IsShowing)
            {
                Form_Choose form = new Form_Choose(labelString);
                form.Show();
            }
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            Program.SimulationAwaiter_whilebool = false;
        }
        private void button_Settings_Click(object sender, EventArgs e)
        {
            if (!theTicker.startRequest)
            {
                if (!Form_Settings.IsShowing)
                {
                    Form_Settings form = new Form_Settings();
                    form.Show();
                } 
            }
            else
            {
                MessageBox.Show("It is not possible to change settings\n with on going simulation");
            }
        }

        private void Form_Main_Activated(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private async void button_Run_Simulation_Click(object sender, EventArgs e)
        {
            if (!theTicker.startRequest || theTicker.canselationRequest)
            {
                theTicker.startRequest = true;
                theTicker.canselationRequest = false;
                theArgs.CanselationRequest = false;
                theArgs.Finished = false;

                Thread infoThread = new Thread(UIInfo);
                
                infoThread.Start();
                Program.SimulationOnFirstClick();
                Program.simulationRelease = true;
                this.button_stopSimulation.Visible = true;

            }

            else if (theTicker.startRequest && theTicker.pauseRequest)
            {
                theTicker.pauseRequest = false;
            }
            else if (theTicker.startRequest)
            {
                theTicker.pauseRequest = true;
            }


        }
        private static async void StartSimulation(object sender, TickerArgs e)
        {
            await dayCareBackEnd.SimulationProgress(e);
            //dayCareUI.WriteOut();
        }
        private async void UIInfo()
        {

                while (!theArgs.CanselationRequest)
                {
                    Thread.Sleep(theArgs.TickInMilliseconds);
                    SetMainReportText(reportArgs.MainReport);

                }
            
        }
        private void SetMainReportText(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.MainReport_Test.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetMainReportText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.MainReport_Test.Text = text;
            }
        }


        private void button_tick_Click(object sender, EventArgs e)
        {

        }

        private void button_stopSimulation_Click(object sender, EventArgs e)
        {
            theTicker.canselationRequest = true;
            theTicker.startRequest = false;
            theTicker.reStartRequest = true;
            theArgs.CanselationRequest = true;
            Program.simulationRelease = false;
        }

        private void button_Show_EndReport_Click(object sender, EventArgs e)
        {
            if (!Form_EndReport.IsShowing)
            {
                Form_EndReport form = new Form_EndReport(reportArgs.EndReport);
                form.Show();
            }
        }
    }
}
