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

        static BackendLogic dayCareBackEndAccess;
        static HDCDbContext hDCDbContext;

        public delegate void SimulationFinished(object sender, EventArgs e);
        public event SimulationFinished SimulationFinishedEvent;

        TickerArgs theArgs;
        string mainReportTypes;

        Ticker theTicker;
        bool showTickReport;

        delegate void SetTextCallbackMain(string text);
        delegate void SetTextCallbackSecondTypes(string text);
        delegate void SetTextCallbackSecondValues(string text);



        public static bool reportRelease = false;
        public static bool reportAwaiter_whilebool = true;

        public Form_Main(HDCDbContext _hDCDbContext
            , Ticker _theTicker
            , BackendLogic _dayCareBackEndAccess)
        {
            InitializeComponent();
            theArgs =  new TickerArgs();
            hDCDbContext = _hDCDbContext;
            theTicker = _theTicker;
            dayCareBackEndAccess = _dayCareBackEndAccess;
            this.label1.Text = "";
            this.label2.Text = "";
            this.SimulationFinishedEvent += button_Show_EndReport_VisibleChanged;

            this.mainReportTypes =               $"INFO TYPE\n\n" +
                                                 $"Simulation ID:\n\n" +
                                                 $"Simulation started at:\n" +
                                                 $"Simulation time now:\n" +
                                                 $"Tick number now:\n\n" +
                                                 $"Number of Hamdters in Cleintele this simulation:\n" +
                                                 $"Capacity of daycare:\n" +
                                                 $"The Hamster gender distribution is:\n\n" +
                                                 $"AVG Wait for first Exersice:\n" +
                                                 $"Number of Cages:\n" +
                                                 $"Capacity of each Cage:\n\n" +
                                                 $"Number of Exersice Areas:\n" +
                                                 $"Capacity of each Exersice Area:\n";

            this.MainReport_Test_Types.Text = "Main Report Window";

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

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            Program.SimulationAwaiter_whilebool = false;
            theArgs.CanselationRequest = true;
            Thread.Sleep(theArgs.TickInMilliseconds);
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
                Program.OnSimulationStart();
                this.MainReport_Test_Types.Text = mainReportTypes;
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
            await dayCareBackEndAccess.SimulationProgress(e);
        }

        private void button_tick_Click(object sender, EventArgs e)
        {
            if (!TickReport.IsShowing)
            {                
                TickReport form = new TickReport();
                form.Show();

            }
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
                Form_EndReport form = new Form_EndReport();
                form.Show();
            }
        }

        private void button_Show_EndReport_VisibleChanged(object sender, EventArgs e)
        {
            if (button_Show_EndReport.InvokeRequired)
            {
                button_Show_EndReport.Invoke(new MethodInvoker(delegate
                {
                    button_Show_EndReport.Visible = true;
                }));
            }
            else
            {
                button_Show_EndReport.Visible = true;
            }

        }

        private void button_Owner_Click_1(object sender, EventArgs e)
        {
            List<string> labelString = new List<string>();
            labelString.Add("Choose Cage");
            labelString.Add("Enter the ID of the Cage you wich to see");
            if (!Form_Choose.IsShowing)
            {
                Form_Choose form = new Form_Choose(labelString);
                form.Show();
            }
        }
        private async void UIInfo()
        {
            while (!theArgs.CanselationRequest)
            {
                Thread.Sleep(theArgs.TickInMilliseconds);
                SetMainReportTextValues(ReportArgs.MainReportValues);
                if (ReportArgs.IsTrackingHamster != null)
                {
                    SetSecondReportTextTypes(ReportArgs.SecondReportTypes);
                    SetSecondReportTextValues(ReportArgs.SecondReportValues);
                }

            }
            if (theArgs.Finished)
            {
                await dayCareBackEndAccess.GenerateEndReport(theArgs);
                Thread.Sleep(100);
                SimulationFinishedEvent?.Invoke(this, EventArgs.Empty);
            }


        }
        private void SetMainReportTextTypes(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.MainReport_Test_Types.InvokeRequired)
            {
                SetTextCallbackMain d = new SetTextCallbackMain(SetMainReportTextTypes);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.MainReport_Test_Types.Text = text;
            }
        }

        private void SetMainReportTextValues(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.MainReport_Test_Values.InvokeRequired)
            {
                SetTextCallbackMain d = new SetTextCallbackMain(SetMainReportTextValues);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.MainReport_Test_Values.Text = text;
            }
        }
        private void SetSecondReportTextValues(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.label2.InvokeRequired)
            {
                SetTextCallbackSecondValues d = new SetTextCallbackSecondValues(SetSecondReportTextValues);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.label2.Text = text;
            }
        }
        private void SetSecondReportTextTypes(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.label1.InvokeRequired)
            {
                SetTextCallbackSecondTypes d = new SetTextCallbackSecondTypes(SetSecondReportTextTypes);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.label1.Text = text;
            }
        }

    }
}
