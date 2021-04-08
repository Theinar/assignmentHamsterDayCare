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

namespace UIWindows
{
    internal partial class Form_Main : Form
    {
        BackendLogic dayCareBackEnd;

        internal BackendLogic DayCareBackEnd { get => dayCareBackEnd; set => dayCareBackEnd = value; }

        public Form_Main(BackendLogic _dayCareBackEnd, string _aString)
        {
            DayCareBackEnd = _dayCareBackEnd;
            InitializeComponent();
            this.textBox_Main_TextBox.Text = _aString;
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
    }
}
