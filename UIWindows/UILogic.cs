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
    class UILogic
    {
        HDCDbContext hDCDbContext;
        DateTime startsFrom;

        public UILogic(HDCDbContext _hDCDbContext, DateTime _startsFrom)
        {
            hDCDbContext = _hDCDbContext;
            startsFrom = _startsFrom;
        }
    

        public string WriteOut()
        {
            var hamsters = hDCDbContext.Hamsters.ToList();

            var nrOfCages = hDCDbContext.Cages.Count();

            var nrOfExersiceAreas = hDCDbContext.

            string aString = String.Format("");

            return aString;
        }

    }
}
