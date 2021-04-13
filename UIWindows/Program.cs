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
        #region Fields
        public static event EventHandler<ReportArgs> report;
        static ReportArgs ReportArgs;

        private static HDCDbContext hDCDbContext;
        private static BackendLogic dayCareBackEndAccessAccess;

        private static TickerArgs theArgs;
        private static Ticker theTicker;

        internal static Form_Main Main_Form;

        public static bool simulationRelease = false;
        public static bool SimulationAwaiter_whilebool = true;

        private static int nrOfDaysInSimulation;
        private static int tickInMilliSec; 
        #endregion

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // creates a new custom EF dbConteqt
            hDCDbContext = new HDCDbContext();
            // if there are no db in system one will be created in SQLEXPERSS
            hDCDbContext.Database.EnsureCreated();

            // theArgs are the info engine in the backend, it is an info carrier
            // supplies info to various parts ot thr program and is ofthen updated
            theArgs = new TickerArgs();

            // Is the same idea as theArgs but in the UI part of the program
            // carries info to different parts of UI, used to construct info Raports
            ReportArgs = new ReportArgs();

            // is the more literal engine, drives the program ittration and drives the simulation
            theTicker = new Ticker();
            // Ticker has tick event wich starts each iterration of the simulatin
            theTicker.tick += StartSimulation;

            // BackendLogic id the go between UI and backend, here are pretty much
            // all the methodes used to gather data from backend
            dayCareBackEndAccessAccess = new BackendLogic(hDCDbContext, theArgs, ReportArgs);

            // sets theArgs form last saves settings
            SetArgsFromSettingsFile();

            // if Hamsters are empty 
            if (hDCDbContext.Hamsters.Count() == 0)
            {
                dayCareBackEndAccessAccess.SeedDB(theArgs);
            }

            // creates Main window form
            Main_Form = new Form_Main(hDCDbContext, theTicker, dayCareBackEndAccessAccess);

            // Creates separate UIthred to handle all of the forms logic
            Thread UIThread = new Thread(StartForm);
            UIThread.Start();

            // creates a loop to keep alive main thread untill simulation strts
            SimulationAwaiter(theTicker);

        }

        #region Methodes
        private static void StartForm()
        {
            Application.Run(Main_Form);
        }
        /// <summary>
        /// Method wich is called on each simulation itteration start
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="_theArgs"></param>
        private static async void StartSimulation(object sender, TickerArgs _theArgs)
        {
            if (!_theArgs.CanselationRequest)
            {
                var backendTask = dayCareBackEndAccessAccess.SimulationProgress(_theArgs);

                await Task.WhenAll(backendTask);

            }
        }
        /// <summary>
        /// metode wich is called onley on first click of simulation start
        /// </summary>
        internal static void OnSimulationStart()
        {
            dayCareBackEndAccessAccess.StartOfTheDayRoutine(theArgs);

        }
        /// <summary>
        /// metode to build theArgs from Settings file
        /// </summary>
        private static void SetArgsFromSettingsFile()
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
            theArgs.CanselationRequest = bool.Parse(fileContentArr[8]);
            theArgs.SimulationTime = DateTime.Parse(fileContentArr[9]);
            theArgs.TickInMilliseconds = int.Parse(fileContentArr[10]);
            theArgs.SettingsID = int.Parse(fileContentArr[11]);


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
        /// Methode wich is called when new settings are invoked, rebuilds and reseds db to fitt new settings
        /// </summary>
        /// <param name="_newArgs"></param>
        internal static void ChangeTheArgAndRebuildFromSettings(TickerArgs _newArgs)
        {
            theArgs = _newArgs;

            dayCareBackEndAccessAccess.UnSeedDBAndSeedFromNewArgs(theArgs);
        } 
        #endregion
    }
}
