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
        public static bool SimulationAwaiter_whilebool = true;
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
            SetArgsFromSettings();

            theTicker = new Ticker();
            theTicker.tick += StartSimulation;
            dayCareBackEnd = new BackendLogic(hDCDbContext, theArgs);

            Thread t2 = new Thread(StartForm);
            t2.Start();

            SimulationAwaiter(theTicker);

        }
        private static void StartForm()
        {
            Application.Run(new Form_Main(hDCDbContext));
        }
        private static async void StartSimulation(object sender, TickerArgs _theArgs)
        {
            var backendTask = dayCareBackEnd.SimulationProgress(_theArgs);

            Task.WhenAll(backendTask);

            Form_Main.reportRelease = true;


        }
        internal static void SimulationOnFirstClick()
        {
            dayCareBackEnd.StartOfTheDayRoutine(theArgs);

        }
        private static void SetArgsFromSettings()
        {
            string fileContent = "";
            using (StreamReader readFromSettingsFile = new StreamReader("Settings.csv"))
            {

                    fileContent = readFromSettingsFile.ReadLine();
            }
            string[] fileContentArr = fileContent.Split(',');

            theArgs.EndTick = int.Parse(fileContentArr[0]);
            theArgs.FictionalStartDate = DateTime.Parse(fileContentArr[1]);
            theArgs.FilePath = fileContentArr[2];
            theArgs.MaxnrOfHamInEachCage = int.Parse(fileContentArr[3]);
            theArgs.MaxnrOfHamInExArea = int.Parse(fileContentArr[4]);
            theArgs.NumberOfcages = int.Parse(fileContentArr[5]);
            theArgs.NumberOfExAreas = int.Parse(fileContentArr[6]);
            theArgs.NumberOfTicks = int.Parse(fileContentArr[7]);
            theArgs.PauseRequest = bool.Parse(fileContentArr[8]);
            theArgs.SimulationTime = DateTime.Parse(fileContentArr[9]);
            theArgs.TickInMilliseconds = int.Parse(fileContentArr[10]);


        }
        private async static void SimulationAwaiter(Ticker _theTicker)
        {
            while (SimulationAwaiter_whilebool)
            {
                if (simulationRelease)
                {
                    _theTicker.Start(theArgs);
                    simulationRelease = false;
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
