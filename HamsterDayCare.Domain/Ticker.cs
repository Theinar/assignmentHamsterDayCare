using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace HamsterDayCare.Domain
{
    public class Ticker
    {

        public event EventHandler<TickerArgs> tick;
        public bool startRequest = false;
        public bool reStartRequest = false;
        public bool canselationRequest = false;
        public bool pauseRequest = false;

        public void Start(TickerArgs _theArgs)
        {
            _theArgs.CanselationRequest = false;

            if (reStartRequest)
            {
                _theArgs.NumberOfTicks = 0;
                _theArgs.SimulationTime = _theArgs.FictionalStartDate;
                startRequest = true;
                canselationRequest = false;
                pauseRequest = false;
                reStartRequest = false;
                _theArgs.CanselationRequest = false;
            }

            while (!canselationRequest)
            {
                tick?.Invoke(this, _theArgs);
                Thread.Sleep(_theArgs.TickInMilliseconds);

                if (!pauseRequest)
                {
                    if (_theArgs.SimulationTime.TimeOfDay != TimeSpan.Parse("17:00:00"))
                    {
                        _theArgs.SimulationTime = _theArgs.SimulationTime.AddMinutes(6);
                    }
                    else
                    {
                        _theArgs.SimulationTime = _theArgs.SimulationTime.AddHours(14);
                    }

                    _theArgs.NumberOfTicks++;

                    if (_theArgs.NumberOfTicks > _theArgs.EndTick)
                    {
                        startRequest = false;
                        canselationRequest = true;
                        _theArgs.Finished = true;
                    } 
                }
            }
            _theArgs.CanselationRequest = true;
            reStartRequest = true;
        }

    }
}

