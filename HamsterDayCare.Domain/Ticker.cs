using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace HamsterDayCare.Domain
{
    public class Ticker
    {

        public event EventHandler<TickerArgs> tick;
        public static TickerArgs theArgs;
        public bool startRequest = false;
        public bool reStartRequest = false;
        public bool canselationRequest = false;
        public bool pauseRequest = false;

        public void Start(TickerArgs _theArgs)
        {
            theArgs = _theArgs;

            theArgs.CanselationRequest = false;

            if (reStartRequest)
            {
                theArgs.NumberOfTicks = 0;
                theArgs.SimulationTime = _theArgs.FictionalStartDate;
                startRequest = true;
                canselationRequest = false;
                pauseRequest = false;
                reStartRequest = false;
                theArgs.CanselationRequest = false;
            }

            while (!canselationRequest)
            {
                tick?.Invoke(this, theArgs);
                Thread.Sleep(theArgs.TickInMilliseconds);

                if (!pauseRequest)
                {
                    if (theArgs.SimulationTime.TimeOfDay != TimeSpan.Parse("17:00:00"))
                    {
                        theArgs.SimulationTime = theArgs.SimulationTime.AddMinutes(6);
                    }
                    else
                    {
                        theArgs.SimulationTime = theArgs.SimulationTime.AddHours(14);
                    }

                    theArgs.NumberOfTicks++;

                    if (theArgs.NumberOfTicks > theArgs.EndTick)
                    {
                        startRequest = false;
                        canselationRequest = true;
                        theArgs.Finished = true;
                        theArgs.CanselationRequest = true;
                    } 
                }
            }
            theArgs.CanselationRequest = true;
            reStartRequest = true;
        }

    }
}

