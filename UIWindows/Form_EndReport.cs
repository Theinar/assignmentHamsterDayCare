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


    public partial class Form_EndReport : Form
    {
        static bool isShowing;
        public static bool IsShowing { get => isShowing; }
        public Form_EndReport()
        {
            InitializeComponent();
            this.label_EndReportTypes.Text = ReportArgs.EndReportTypes;
            this.label_Endreport_vauess.Text = ReportArgs.EndReportValues;
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
