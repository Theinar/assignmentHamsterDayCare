using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIWindows
{
    public class ReportArgs : EventArgs
    {
        static string mainReportTypes;
        static string mainReportValues = "";
        static string tickNowReportTypes = "";
        static string tickNowReportValues = "";
        static string tickNowReportHead = "";
        static string endReportTypes = ""; 
        static string endReportValues = "";
        static string secondReportTypes = "";
        static string secondReportValues = "";
        static bool? isTrackingHamster = null;
        static int trackingID = 0;




        public static string TickNowReportTypes { get => tickNowReportTypes; set => tickNowReportTypes = value; }
        public static string EndReportValues { get => endReportValues; set => endReportValues = value; }
        public static string TickNowReportHead { get => tickNowReportHead; set => tickNowReportHead = value; }
        public static string TickNowReportValues { get => tickNowReportValues; set => tickNowReportValues = value; }
        public static string MainReportValues { get => mainReportValues; set => mainReportValues = value; }
        public static string EndReportTypes { get => endReportTypes; set => endReportTypes = value; }
        public static string SecondReportTypes { get => secondReportTypes; set => secondReportTypes = value; }
        public static string SecondReportValues { get => secondReportValues; set => secondReportValues = value; }
        public static bool? IsTrackingHamster { get => isTrackingHamster; set => isTrackingHamster = value; }
        public static int TrackingID { get => trackingID; set => trackingID = value; }

        public ReportArgs()
        {
        }
    }
}
