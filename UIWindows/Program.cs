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
        public static bool simulationRelease = false;
        private static Ticker theTicker;
        private static HDCDbContext hDCDbContext = new HDCDbContext();

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

            theArgs = new TickerArgs();
            theTicker = new Ticker();
            theTicker.tick += StartSimulation;
            dayCareBackEnd = new BackendLogic(hDCDbContext, theArgs);

            Thread t2 = new Thread(StartSimulation);
            t2.Start();

            SimulationAwaiter(theTicker);

        }
        private static void StartSimulation()
        {
            Application.Run(new Form_Main());
        }
        private static async void StartSimulation(object sender, TickerArgs _theArgs)
        {
            dayCareBackEnd.SimulationProgress(_theArgs);
        }
        private static void SimulationAwaiter(Ticker _theTicker)
        {
            bool whilebool = true;

            while (whilebool)
            {
                if (simulationRelease)
                {
                    _theTicker.Start(theArgs);
                }
            }
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
