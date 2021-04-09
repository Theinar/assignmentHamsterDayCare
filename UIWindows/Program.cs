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


            //Thread t2 = new Thread(runUILogic);
            //t2.Start();


            Application.Run(new Form_Main());

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






/// <summary>
/// ///////////////////////// Kolla så att det nedan funkar nu när mycket av program har flyttats
/// </summary>
/// <param name="_newArgs"></param>






        internal static void ChangeTheArgs(TickerArgs _newArgs)
        {
            theArgs = _newArgs;
        }
    }
}
