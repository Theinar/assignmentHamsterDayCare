using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using HamsterDayCare.Domain;
using HamsterDayCare.Data;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using RandomNameGeneratorLibrary;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace UIWindows
{
    static class Program
    {



        private static HDCDbContext hDCDbContext = new HDCDbContext();
        private static TickerArgs theArgs;
        private static BackendLogic dayCareBackEnd;
       // private static UILogic dayCareUI;
        private static int nrOfDaysInSimulation;
        private static int tickInMilliSec;
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //SetTimes();
            theArgs = new TickerArgs();
            Ticker theTicker = new Ticker();

            theTicker.tick += StartSimulation;

            dayCareBackEnd = new BackendLogic(hDCDbContext, theArgs);

           // dayCareUI = new UILogic(hDCDbContext, theArgs);

            Application.Run(new Form_Main(dayCareBackEnd));

         }


        //private static void SetTimes()
        //{
        //    //Console.WriteLine("pick a date (YYYY.MM.dd):           -- Change så klart");
        //    string date = "1997.08.29";  // Console.ReadLine();

        //    string sevenOclock = " 07:00:00:0000";
        //    string startsFromString = date + sevenOclock;
        //    string format = "yyyy.MM.dd HH:mm:ss:ffff";
        //    fictionalDate = DateTime.ParseExact(startsFromString, format,
        //                                     CultureInfo.InvariantCulture);
        //    nrOfDaysInSimulation = 1;
        //    //Console.WriteLine("Please chose a tickrate (ms)");
        //    //tickInMilliSec = int.Parse(Console.ReadLine());
        //    tickInMilliSec = 200;
        //}
        private static async void StartSimulation(object sender, TickerArgs _theArgs)
        {
            dayCareBackEnd.SimulationProgress(_theArgs);
        }
        internal static void ChangeTheArgs(TickerArgs _newArgs)
        {
            // If user acceses settings and makes changes theas are here enforcaed 
            theArgs = _newArgs;
        }
    }
}
