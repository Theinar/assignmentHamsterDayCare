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
        public static DateTime StartsFrom = DateTime.Now;
        static void Main(string[] args)
        {
            //CreateAndAddHamsterClientele();
            //CheckInHamsters();
            //AddDaycareLog();
            //AddCage(10);
            //CheckOutHamsters();
        }

        /// <summary>
        /// Creates the hamster from .csv file. And adds them to the database
        /// </summary>
        private static void CreateAndAddHamsterClientele()
        {
            List<Hamster> hamsterClientele = new List<Hamster>();   //List for hamsters
            StringBuilder sb = new StringBuilder();
            using (StreamReader sr = new StreamReader("Hamsterlista30.csv"))    //File containing hamsters
            {
                while (!sr.EndOfStream)         // Creates a string of the complete file
                {
                    sb.Append(sr.ReadLine());
                    sb.Append("¤");
                }
            }
            var attribs = sb.ToString();
            var hamsterAttribArr = attribs.Split("¤");              //Spliting the string frirst into lines and then down to parsible string objects

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
            hamsterClientele = hamsterClientele.OrderBy(h => h.Gender).ToList();    // Sorts the list by gender to simplefy cage assignment


            for (int i = 0; i < hamsterClientele.Count; i++)    // Adds list to Hamsters DB entity
            {
                hDCDbContext.Hamsters.Add(hamsterClientele[i]);
                hDCDbContext.SaveChanges();

            }

        }
        /// <summary>
        /// Initial seeding of cages. Takes an int wich represents the number of cages that will be added to database
        /// </summary>
        /// <param name="length"></param>
        private static void AddCage(int length)
        {
            for (int i = 0; i < length; i++)
            {
                hDCDbContext.Cages.Add(new Cage() { Capacity = 3 });
                hDCDbContext.SaveChanges(); 
            }
        }
        /// <summary>
        /// Methode that wich is callen att the start of each simulated day. A representation of the arrival of the hamsters. On methodecall the
        /// methode instansiates a new DayCareLog instance wich each DayCareStay is added to. A DayCAreStay is a object containing all 
        /// recorded activities of each animal for this spesific day.
        /// </summary>
        private static void CheckInHamsters()
        {
            bool loopBool = true;
            Cage defaultCage = null;
            hDCDbContext.DayCareLogs.Add(new DayCareLog());
            hDCDbContext.SaveChanges();
            var dayCareLog = hDCDbContext.DayCareLogs.OrderByDescending(d => d.Id).First();

            while (loopBool)
            {
                var hamster = hDCDbContext.Hamsters //Selects e hamster from hamsters in DB
                    .FirstOrDefault(h => h.CageId == null) ?? new Hamster();

                var cage = hDCDbContext.Cages   // Selects first avaleble cage
                        .AsEnumerable()
                        .FirstOrDefault(c => c.Capacity > c.NrOfHamsters
                        & ((c.Gender == hamster.Gender)
                        || (c.NrOfHamsters == 0))) ?? defaultCage;


                if (hamster != null && cage != null)
                {
                    hDCDbContext.DayCareStays.Add(new DayCareStay() //instansiates i new DayCareStays with above selected objects
                    {
                        DayCareLogId = dayCareLog.Id,
                        HamasterId = hamster.id,
                        CageId = cage.Id,
                        Arrival = StartsFrom,

                    });
                    hamster.CageId = cage.Id;   // Updates database fields for each entity 
                    cage.Hamsters.Add(hamster);
                    cage.NrOfHamsters++;

                    if (cage.Gender == Gender.NotChosen)
                    {
                        cage.Gender = hamster.Gender;
                    }


                    hDCDbContext.SaveChanges();     //Saves changes in database before loop starts over
                }
                else
                {
                    loopBool = false;
                }

            }
        }
        /// <summary>
        /// Metode that represents the outchecking of all animals in the daycare, CageId of each animal 
        /// is set to null, animals are removed from thair cages, when a cage is empty cage.Gender is set 
        /// to unchosen, checkout time is set
        /// </summary>
        private static void CheckOutHamsters()
        {
            bool loopBool = true;   // Defaule values wich is given to Linq operationes if thay end up without an object
            Cage defaultCage = null;

            while (loopBool) // while wich continues untill all animals hav left thair cages
            {
                var hamster = hDCDbContext.Hamsters                             // Selects the hamster wich is agout to check put
                    .FirstOrDefault(h=>h.CageId != null) ?? new Hamster();      // sets a dummy instans to prevens false value in next step

                var cage = hDCDbContext.Cages                                   // Finds wich cage that animal is in 
                    .FirstOrDefault(c=>c.Id == hamster.CageId) ?? defaultCage;

                var thisStay = hDCDbContext.DayCareStays.OrderByDescending(d => d.Id).FirstOrDefault(d => d.Id == hamster.id) ?? null;


                if (cage != null)        // see xml summary of methode for explanation
                {

                    hamster.CageId = null;      

                    cage.Hamsters.Remove(hamster);
                    cage.NrOfHamsters--;
                    if (cage.NrOfHamsters == 0)
                    {
                        cage.Gender = Gender.NotChosen;
                    }

                    thisStay.CheckOut = DateTime.Now;

                    hDCDbContext.SaveChanges();
                }
                else
                {
                    loopBool = false;
                }

            }
        }



    }
}
