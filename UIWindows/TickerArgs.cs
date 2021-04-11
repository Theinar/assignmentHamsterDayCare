using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamsterDayCare.Domain
{
    public class TickerArgs : EventArgs
    {
        static int numberOfTicks = 0;
        static DateTime fictionalStartDate = DateTime.Now;
        static DateTime simulationTime = DateTime.Now;
        static string filePath = "Hamsterlista30.csv";
        static int endTick = 100;
        static int tickInMilliseconds = 500;
        static bool canselationRequest = false;
        static int maxnrOfHamInEachCage = 3;
        static int maxnrOfHamInExArea = 6;
        static int numberOfcages = 10;
        static int numberOfExAreas = 1;
        static bool finished = false;
        public static int settingsID;


        public int NumberOfTicks { get => numberOfTicks;  set => numberOfTicks = value; }
        public DateTime FictionalStartDate { get => fictionalStartDate; set => fictionalStartDate = value; }
        public DateTime SimulationTime { get => simulationTime; set => simulationTime = value; }
        public string FilePath { get => filePath; set => filePath = value; }
        public int EndTick { get => endTick; set => endTick = value; }
        public int TickInMilliseconds { get => tickInMilliseconds; set => tickInMilliseconds = value; }
        public bool CanselationRequest { get => canselationRequest; set => canselationRequest = value; }
        public int MaxnrOfHamInEachCage { get => maxnrOfHamInEachCage; set => maxnrOfHamInEachCage = value; }
        public int MaxnrOfHamInExArea { get => maxnrOfHamInExArea; set => maxnrOfHamInExArea = value; }
        public int NumberOfcages { get => numberOfcages; set => numberOfcages = value; }
        public int NumberOfExAreas { get => numberOfExAreas; set => numberOfExAreas = value; }
        public bool Finished { get => finished; set => finished = value; }
        public int SettingsID { get => settingsID; set => settingsID = value; }

        public TickerArgs(DateTime _fictionalDate
                        , int _nrOfDaysInSimulation
                        , int _tickInMilliseconds)
        {
            FictionalStartDate = _fictionalDate;
            SimulationTime = _fictionalDate;
            EndTick = (_nrOfDaysInSimulation * 100);
            NumberOfTicks = 0;
            TickInMilliseconds = _tickInMilliseconds;
            CanselationRequest = false;
            MaxnrOfHamInEachCage = 3;
            MaxnrOfHamInExArea = 6;
            NumberOfcages = 10;
            NumberOfExAreas = 1;
            FilePath = "Hamsterlista30.csv";
        }
        public TickerArgs()
        {

        }

    }
}
