using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIWindows
{
    public class ReportArgs : EventArgs
    {
        string mainReport = "";
        string hamsterReport = "";
        string ownerReport = "";
        string tickNowReport = "";

        public string MainReport { get => mainReport; set => mainReport = value; }
        public string HamsterReport { get => hamsterReport; set => hamsterReport = value; }
        public string OwnerReport { get => ownerReport; set => ownerReport = value; }
        public string TickNowReport { get => tickNowReport; set => tickNowReport = value; }

        public ReportArgs()
        {

        }
    }
}
