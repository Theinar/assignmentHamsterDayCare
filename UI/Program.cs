using System;
using HamsterDayCare.Domain;
using HamsterDayCare.Data;
using System.Linq;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using RandomNameGeneratorLibrary;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace UI
{

    class Program
    {


        private static HDCDbContext hDCDbContext = new HDCDbContext();
        private static DateTime fictionalDate;
        private static BackendLogic dayCareBackEnd;
        private static UILogic dayCareUI;
        private static int nrOfDaysInSimulation;
        private static int tickInMilliSec;

        static void Main(string[] args)
        {
            SetTimes();
            TickerArgs theArgs = new TickerArgs(fictionalDate, nrOfDaysInSimulation, tickInMilliSec);
            Ticker theTicker = new Ticker();

            theTicker.tick += StartSimulation;

            dayCareBackEnd = new BackendLogic(hDCDbContext, fictionalDate);

            dayCareUI = new UILogic(hDCDbContext, theArgs);

            dayCareBackEnd.EnsureDaysReadyToStart();

            theTicker.Start(theArgs);


        }

        public static void CallBeckendTicker()
        {

        }
        public static void CallUILogic()
        {
            for (int i = 0; i < 50; i++)
            {
                Console.WriteLine("t2");
                Thread.Sleep(75);
            }
        }
        private static void SetTimes()
        {
            //Console.WriteLine("pick a date (YYYY.MM.dd):           -- Change så klart");
            string date = "1997.08.29";  // Console.ReadLine();

            string sevenOclock = " 07:00:00:0000";
            string startsFromString = date + sevenOclock;
            string format = "yyyy.MM.dd HH:mm:ss:ffff";
            fictionalDate = DateTime.ParseExact(startsFromString, format,
                                             CultureInfo.InvariantCulture);
            nrOfDaysInSimulation = 2;
            //Console.WriteLine("Please chose a tickrate (ms)");
            //tickInMilliSec = int.Parse(Console.ReadLine());
            tickInMilliSec = 500;
        }
        private static async void StartSimulation(object sender, TickerArgs e)
        {

           await dayCareBackEnd.SimulationProgress(e);
           dayCareUI.WriteOut();
        }

    }
}
