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
    class UILogic
    {
        HDCDbContext hDCDbContext;
        DateTime startsFrom;

        public UILogic(HDCDbContext _hDCDbContext, DateTime _startsFrom)
        {
            hDCDbContext = _hDCDbContext;
            startsFrom = _startsFrom;
        }
        internal async Task UIReport(TickerArgs e)
        {

            var nrHamInCages = hDCDbContext.Hamsters.Where(h => h.CageId != null).Count();
            var nrHamInExArea = hDCDbContext.Hamsters.Where(h => h.ExerciseAreaId != null).Count();
            var exArea = hDCDbContext.ExerciseAreas.First();

            Console.SetCursorPosition(0, 0);
            //Console.WriteLine($"NUmber of hamsters in their cages: {nrHamInCages}");
            //Console.WriteLine($"Number of hamsters in ExerciseArea: {nrHamInExArea}");
            //Console.WriteLine($"The gendet of the hamsters in exersicearea is at the moment:{exArea.Gender}");
            await Task.CompletedTask;
        }
    }
}
