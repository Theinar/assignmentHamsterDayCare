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
        TickerArgs theArgs;
        static BackendLogic dayCareBackEnd;

        public Form_Main()
        {
            InitializeComponent();
            theArgs =  new TickerArgs();

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

        private void button3_Click(object sender, EventArgs e)
        {

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

        private void button_Settings_Click(object sender, EventArgs e)
        {
            if (!Form_Settings.IsShowing)
            {
                Form_Settings form = new Form_Settings();
                form.Show();
            }
        }

        private void Form_Main_Activated(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button_Run_Simulation_Click(object sender, EventArgs e)
        {
            Program.simulationRelease = true;
        }
        private static async void StartSimulation(object sender, TickerArgs e)
        {
            await dayCareBackEnd.SimulationProgress(e);
            //dayCareUI.WriteOut();
        }
    }
}
