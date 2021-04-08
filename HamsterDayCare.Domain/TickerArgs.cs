using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamsterDayCare.Domain
{
    public class TickerArgs : EventArgs
    {
        int numberOfTicks;
        DateTime actualTimeStart;
        DateTime fictionalStartDate;
        DateTime simulationTime;
        string filePath;
        int endTick;
        int tickInMilliseconds;
        bool pauseRequest;
        int maxnrOfHamInEachCage;
        int maxnrOfHamInExArea;
        private int numberOfcages;
        private int numberOfExAreas;


        public int TickCounter { get => numberOfTicks; set => numberOfTicks = value; }
        public DateTime ActualTimeStart { get => actualTimeStart; private set => actualTimeStart = value; }
        public DateTime FictionalDate { get => fictionalStartDate; private set => fictionalStartDate = value; }
        public DateTime SimulationTime { get => simulationTime; set => simulationTime = value; }
        public bool PauseRequest { get => pauseRequest; private set => pauseRequest = value; }
        public int TickInMilliseconds { get => tickInMilliseconds; private set => tickInMilliseconds = value; }
        public int MaxnrOfHamInEachCage { get => maxnrOfHamInEachCage; private set => maxnrOfHamInEachCage = value; }
        public int MaxnrOfHamInExArea { get => maxnrOfHamInExArea; private set => maxnrOfHamInExArea = value; }
        public int NumberOfcages { get => numberOfcages; set => numberOfcages = value; }
        public int NumberOfExAreas { get => numberOfExAreas; set => numberOfExAreas = value; }
        public int EndTick { get => endTick; private set => endTick = value; }
        public string FilePath { get => filePath; set => filePath = value; }

        public TickerArgs(DateTime _fictionalDate
                        , int _nrOfDaysInSimulation
                        , int _tickInMilliseconds)
        {
            ActualTimeStart = DateTime.Now;
            FictionalDate = _fictionalDate;
            SimulationTime = _fictionalDate;
            EndTick = (_nrOfDaysInSimulation * 100);
            TickCounter = 0;
            TickInMilliseconds = _tickInMilliseconds;
            PauseRequest = false;
            MaxnrOfHamInEachCage = 3;
            MaxnrOfHamInExArea = 6;
            NumberOfcages = 10;
            NumberOfExAreas = 1;
            FilePath = "Hamsterlista30.csv";
        }
    }
}
