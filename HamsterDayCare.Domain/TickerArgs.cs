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
        DateTime fictionalEndDate;
        DateTime simulationTime;
        int tickInMilliseconds;
        bool pauseRequest;
        int maxnrOfHamInEachCage;
        int maxnrOfHamInExArea;
        private int numberOfcages;
        private int numberOfExAreas;


        public int NumberOfTicks { get => numberOfTicks; set => numberOfTicks = value; }
        public DateTime ActualTimeStart { get => actualTimeStart; private set => actualTimeStart = value; }
        public DateTime FictionalDate { get => fictionalStartDate; private set => fictionalStartDate = value; }
        public DateTime SimulationTime { get => simulationTime; set => simulationTime = value; }
        public bool PauseRequest { get => pauseRequest; private set => pauseRequest = value; }
        public DateTime FictionalEndDate { get => fictionalEndDate; private set => fictionalEndDate = value; }
        public int TickInMilliseconds { get => tickInMilliseconds; private set => tickInMilliseconds = value; }
        public int MaxnrOfHamInEachCage { get => maxnrOfHamInEachCage; private set => maxnrOfHamInEachCage = value; }
        public int MaxnrOfHamInExArea { get => maxnrOfHamInExArea; private set => maxnrOfHamInExArea = value; }
        public int NumberOfcages { get => numberOfcages; set => numberOfcages = value; }
        public int NumberOfExAreas { get => numberOfExAreas; set => numberOfExAreas = value; }

        public TickerArgs(DateTime _fictionalDate
                        , int _nrOfDaysInSimulation
                        , int _tickInMilliseconds)
        {
            ActualTimeStart = DateTime.Now;
            FictionalDate = _fictionalDate;
            SimulationTime = _fictionalDate;
            FictionalEndDate = _fictionalDate.AddDays(_nrOfDaysInSimulation);
            NumberOfTicks = 0;
            TickInMilliseconds = _tickInMilliseconds;
            PauseRequest = false;
            MaxnrOfHamInEachCage = 3;
            MaxnrOfHamInExArea = 6;
            NumberOfcages = 10;
            NumberOfExAreas = 1;
        }
    }
}
