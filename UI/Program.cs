using System;
using HamsterDayCare.Domain;
using HamsterDayCare.Data;
using System.Linq;
using System.IO;
using RandomNameGeneratorLibrary;
using System.Collections.Generic;
using System.Text;

namespace UI
{
    class Program
    {
        public static HDCDbContext hDCDbContext = new HDCDbContext();
        static void Main(string[] args)
        {

        }


        /* Fiffig hjälp i Console PM  get-help Entityframework
         * _context.blabla.TaqWith() lägger till en kommentar i SQL
         
         
         
         */




        private static void Shuffle<T>(IList<T> list)
        {
            Random rng = new Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
        private static List<Hamster> CreateHamsterClientele()
        {
            List<Hamster> hamsterClientele = new List<Hamster>();
            StringBuilder sb = new StringBuilder();
            using (StreamReader sr = new StreamReader("Hamsterlista30.csv"))
            {
                while (!sr.EndOfStream)
                {
                    sb.Append(sr.ReadLine());
                    sb.Append("¤");
                }
            }
            var attribs = sb.ToString();
            var hamsterAttribArr = attribs.Split("¤");

            for (int i = 0; i < hamsterAttribArr.Length - 1; i++)
            {
                var thisHamster = hamsterAttribArr[i].Split(";");
                int isMale = 0;
                if (thisHamster[2] == "K")
                {
                    isMale = 1;
                }
                hamsterClientele.Add(new Hamster()
                {
                    Name = thisHamster[0],
                    Age = int.Parse(thisHamster[1]),
                    Gender = (Gender)isMale,
                    Owner = thisHamster[3]
                });
            }
           // Shuffle<Hamster>(hamsterClientele);
            return hamsterClientele;

        }
        private static void CreateAndAddHamsterClientele()
        {
            List<Hamster> hamsterClientele = new List<Hamster>();
            StringBuilder sb = new StringBuilder();
            using (StreamReader sr = new StreamReader("Hamsterlista30.csv"))
            {
                while (!sr.EndOfStream)
                {
                    sb.Append(sr.ReadLine());
                    sb.Append("¤");
                }
            }
            var attribs = sb.ToString();
            var hamsterAttribArr = attribs.Split("¤");

            for (int i = 0; i < hamsterAttribArr.Length - 1; i++)
            {
                var thisHamster = hamsterAttribArr[i].Split(";");
                int isMale = 0;
                if (thisHamster[2] == "K")
                {
                    isMale = 1;
                }
                hamsterClientele.Add(new Hamster()
                {
                    Name = thisHamster[0],
                    Age = int.Parse(thisHamster[1]),
                    Gender = (Gender)isMale,
                    Owner = thisHamster[3]
                });
            }
            hamsterClientele = hamsterClientele.OrderBy(h => h.Gender).ToList();


            for (int i = 0; i < hamsterClientele.Count; i++)
            {
                hDCDbContext.Hamsters.Add(hamsterClientele[i]);
                hDCDbContext.SaveChanges();

            }

        }
        private static void GetHamsters(string text)
        {
            var hamsters = hDCDbContext.Hamsters.ToList();
            Console.WriteLine($"{text}: Hamster count is {hamsters.Count}");
            foreach (var hamster in hamsters)
            {
                Console.Write(hamster.Name);
            }
        }
        private static void AddCage()
        {
            hDCDbContext.Cages.Add(new Cage() { Capacity = 6 });
            hDCDbContext.SaveChanges();
        }
        private static void CheckInHamster()
        {
            var hamster = hDCDbContext.Hamsters.First();
            var cage = hDCDbContext.Cages.First(c => c.Gender == hamster.Gender && c.Capacity > c.NrOfHamsters || c.NrOfHamsters == 0);
            hDCDbContext.DayCareStays.Add()

        }


    }
}
