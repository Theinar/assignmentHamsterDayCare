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
    public partial class TickReport : Form
    {
        static bool isShowing;
        public static bool IsShowing { get => isShowing; }
        public TickReport()
        {
            InitializeComponent();
            this.label_tickreport_textTypes.Text = $"NUMBER OF\n" +
                                                   $"Check Ins accured:\n\n" +
                                                   $"Check Outs accured: \n\n" +
                                                   $"Moves to Exersice Area: \n\n" +
                                                   $"Moves from Exersice Area: \n\n" +
                                                   $"Hamsters in Cages:\n\n" +
                                                   $"Hamsters in Exersice Areas: ";

            this.label_tickreport_Head.Text = ReportArgs.TickNowReportHead;
            this.label_Tick_Values.Text = ReportArgs.TickNowReportValues;
            isShowing = true;
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            if (e.CloseReason == CloseReason.WindowsShutDown) return;
            isShowing = false;
            e.Cancel = true;
            this.Dispose();

        }
    }
}
