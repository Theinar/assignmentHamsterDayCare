using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace HamsterDayCare.Domain
{
    public class Ticker
    {

        public event EventHandler<TickerArgs> tick;

        public void Start(TickerArgs _theArgs)
        {
            while (!_theArgs.PauseRequest
                && _theArgs.SimulationTime
                < _theArgs.FictionalEndDate)
            {
                tick?.Invoke(this, _theArgs);
                Thread.Sleep(_theArgs.TickInMilliseconds);
                if (_theArgs.SimulationTime.TimeOfDay != TimeSpan.Parse("17:00:00"))
                {
                    _theArgs.SimulationTime = _theArgs.SimulationTime.AddMinutes(6);
                }
                else
                {
                    _theArgs.SimulationTime = _theArgs.SimulationTime.AddHours(14);
                }

            }
        }

    }
}

