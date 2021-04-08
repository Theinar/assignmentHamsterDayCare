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

namespace UIWindows
{
    public class UILogic
    {
        HDCDbContext hDCDbContext;
        TickerArgs theArgs;

        public UILogic(HDCDbContext _hDCDbContext, TickerArgs _theArgs)
        {
            hDCDbContext = _hDCDbContext;
            theArgs = _theArgs;
        }
    

        public string WriteOut()
        {
            var hamsters = hDCDbContext.Hamsters.ToList();

            var nrOfCages = hDCDbContext.Cages.Count();

            //var nrOfExersiceAreas = hDCDbContext.

            string aString = String.Format("");

            return aString;
        }

    }
}
