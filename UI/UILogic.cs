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
        TickerArgs theArgs;

        public UILogic(HDCDbContext _hDCDbContext, TickerArgs _theArgs)
        {
            hDCDbContext = _hDCDbContext;
            theArgs = _theArgs;
        }
        //void UIReport()
        //{
        //    PURPLE();
        //    Console.Write("╔═════════════════════════════════════════════════════════════════════════════════════════════════════════╗\n");
        //    Console.Write("║                                               AFTERYELP                                                 ║\n");
        //    Console.Write("║╔═══════════════════════════════════════════════════════════════════════════════════════════════════════╗║\n");
        //    Console.Write("║║   ");
        //    WriteOut(hDCDbContext, 0);
        //    Console.Write("║║   \n");
        //    Console.Write("║║   ");
        //    WriteOut(hDCDbContext, 1);
        //    Console.Write("║║   \n");
        //    Console.Write("║║   ");
        //    WriteOut(hDCDbContext, 2);
        //    Console.Write("║║   \n");
        //    Console.Write("║║   ");
        //    WriteOut(hDCDbContext, 3);
        //    Console.Write("║║   \n");
        //    Console.Write("║║   ");
        //    WriteOut(hDCDbContext, 4);
        //    Console.Write("║║   \n");
        //    Console.Write("║║   ");
        //    WriteOut(hDCDbContext, 5);
        //    Console.Write("║║   \n");
        //    Console.Write("║║   ");
        //    WriteOut(hDCDbContext, 6);
        //    Console.Write("║║   \n");
        //    Console.Write("║║   ");
        //    WriteOut(hDCDbContext, 7);
        //    Console.Write("║║   \n");
        //    Console.Write("║║   ");
        //    WriteOut(hDCDbContext, 8);
        //    Console.Write("║║   \n");
        //    Console.Write("║║   ");
        //    WriteOut(hDCDbContext, 9);
        //    Console.Write("║║   \n");
        //    Console.Write("║╚═══════════════════════════════════════════════════════════════════════════════════════════════════════╝║\n");
        //    Console.Write("╚═════════════════════════════════════════════════════════════════════════════════════════════════════════╝\n");
        //    STANDARDCOLOR();


        //    //═══╝╚═ ╚═╝ ╗ ╔═█████╗
        //} // Själva afterYeldrutan

        public async Task WriteOut() // skriver ut den dödes klagomål
        {
            var cages = hDCDbContext.Cages.Where(c => c.Id > 0).ToList();
            Console.SetCursorPosition(0,0);
            Console.WriteLine(theArgs.TickCounter);
            for (int i = 0; i < cages.Count; i++)
            {
                Console.WriteLine($"Cage nr {i}: {cages[i].NrOfHamsters}");

            }

            var exArea = hDCDbContext.ExerciseAreas.Where(c => c.Id > 0).ToList();

            for (int i = 0; i < cages.Count; i++)
            {
                Console.WriteLine($"ExArea nr {i}: {exArea[i].NrOfHamsters}");

            }


            await Task.CompletedTask;
        }


        #region Mini Methods for ConsolFontColor
        public static void RED()
        {
            Console.ForegroundColor = ConsoleColor.Red;
        }
        public static void DARKRED()
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
        }
        public static void PURPLE()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
        }
        public static void DARKPURPLE()
        {
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
        }
        public static void BLUE()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
        }
        public static void DARKBLUE()
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
        }
        public static void GREEN()
        {
            Console.ForegroundColor = ConsoleColor.Green;
        }
        public static void DARKGREEN()
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
        }
        public static void YELLOW()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
        }
        public static void CYAN()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
        }
        public static void STANDARDCOLOR()
        {
            Console.ForegroundColor = ConsoleColor.White;
        }
        #endregion
    }
}
