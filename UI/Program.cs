using System;
using HamsterDayCare.Domain;
using HamsterDayCare.Data;
using System.Linq;
using System.IO;
using System.Threading;
using RandomNameGeneratorLibrary;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace UI
{

    class Program
    {

        public static HDCDbContext hDCDbContext = new HDCDbContext();
        public static DateTime StartsFrom;
        public static DateTime SimulatedTime;


        static void Main(string[] args)
        {
            //CreateAndAddHamsterClientele();
            //CheckInHamsters();
            //AddDaycareLog();
            //AddCage(10);
            //CheckOutHamsters();
            // AddExersiceArea(1);
            Console.WriteLine("pick a date (YYYY.MM.dd):");
            string date = Console.ReadLine();

            DateTime StartsFrom = SetTimes(date);

            Console.WriteLine(StartsFrom);


        }



        /*
         *  Alla datetime är satta till Now mins checkout och MoveHamsterToExersiceArea
         *  Lägga till meddelanden ifall metoden körs utan utall
         *  Lägg till fler activities
         *  läs tenta dokumentet en gång extra
         * 
            */







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
        private static void AddExersiceArea(int length)
        {
            for (int i = 0; i < length; i++)
            {
                hDCDbContext.ExerciseAreas.Add(new ExerciseArea() { Capacity = 6 });
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
                        Arrival = SimulatedTime,

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
            // Defaule values wich is given while lopp to Linq operationes if thay end up without an object
            bool loopBool = true;   
            Cage defaultCage = null;

            // while wich continues untill all animals hav left thair cages
            while (loopBool) 
            {
                // Selects the hamster wich is about to check put
                // sets a dummy instans to prevens false value in next step
                var hamster = hDCDbContext.Hamsters                             
                    .FirstOrDefault(h => h.CageId != null) ?? new Hamster();

                // Finds wich cage that animal is in 
                var cage = hDCDbContext.Cages                                   
                    .FirstOrDefault(c => c.Id == hamster.CageId) ?? defaultCage;

                // Selects ongoing DaycareStay
                var thisStay = hDCDbContext.DayCareStays.OrderByDescending(d => d.Id).FirstOrDefault(d => d.Id == hamster.id) ?? null;

                // Executes if cage is chosen
                if (cage != null)
                {
                    // resets hamster cageid to default
                    hamster.CageId = null;

                    //removes specific hamster form specified cage
                    cage.Hamsters.Remove(hamster);  
                    cage.NrOfHamsters--;
                    // if cage is empty cage.Gender is set to NotChosen
                    if (cage.NrOfHamsters == 0)
                    {
                        cage.Gender = Gender.NotChosen;    
                    }

                    // Sets checkout time on DayCareStay
                    thisStay.CheckOut = SimulatedTime;

                    //saves to db each loop itteration
                    hDCDbContext.SaveChanges();
                }
                else
                {
                    // if cage is null loopBoll is set to false
                    loopBool = false; 
                }
            }
        }
        /// <summary>
        /// Moves animals from cage to exersiceArea, sets gender on ExersiceArea and cage if needed. Updates DaycareStay.Activitys aswell
        /// </summary>
        /// <param name="nrOfHamsters"></param>
        private static void MoveHamsterToExersiceArea(int nrOfHamsters)
        {
            //Loop itterates and add as manny animals as the nrOfHamsters variable indicates
            for (int i = 0; i < nrOfHamsters; i++)
            {
                // Defaule values wich is given Linq operationes if thay end up without an object
                Cage defaultCage = null;

                // Selects the hamster wich is about to exersice
                // sets a dummy instans to prevens false value in next step
                var hamster = hDCDbContext.Hamsters             
                    .OrderBy(h => h.LastExercise).FirstOrDefault(c => c.CageId != null) ?? new Hamster();

                // Finds wich cage that animal is in 
                var cage = hDCDbContext.Cages                    
                    .FirstOrDefault(c => c.Id == hamster.CageId) ?? defaultCage;

                //Finds ongoing DayCareStay instatnce
                var thisStay = hDCDbContext.DayCareStays       
                    .OrderByDescending(d => d.Id)
                    .FirstOrDefault(d => d.Id == hamster.id) ?? null;

                // Selects an Exersicerea sets to null if none is chosen
                var ExersiceArea = hDCDbContext.ExerciseAreas.FirstOrDefault(e => e.NrOfHamsters < e.Capacity) ?? null;
                
                // Log that executes if ExerciseArea is chosen
                if (ExersiceArea != null)
                {
                    // ExersiceArea gender is set if needed
                    if (ExersiceArea.Gender == Gender.NotChosen)
                    {
                        ExersiceArea.Gender = hamster.Gender;
                    }

                    // Adding Exersice to DayCareStay.Activity and setting dage and hamster values
                    thisStay.Activities.Add( new Activity    
                    {
                        AccuredAt = SimulatedTime,
                        HamsterId = hamster.id,
                        TypeOfActivity = TypeOfActivity.Exercises
                    });

                    // Cage set to gender.NotChosen if empty
                    cage.NrOfHamsters--;            
                    if (cage.NrOfHamsters == 0)
                    {
                        cage.Gender = Gender.NotChosen;
                    }

                    // Updates hamster values
                    hamster.CageId = null;
                    hamster.ExerciseAreaId = ExersiceArea.Id;

                    // updates the numbre of animals in cage
                    ExersiceArea.NrOfHamsters++;

                    //saves to db each loop itteration  
                    hDCDbContext.SaveChanges(); 
                }
            }

        }
        /// <summary>
        /// Moves animals exersiceArea to cage, sets gender on ExersiceArea and cage if needed. Updates DaycareStay.Activitys aswell
        /// </summary>
        /// <param name="nrOfHamsters"></param>
        private static void MoveHamsterFromExersiceArea(int nrOfHamsters)
        {
            for (int i = 0; i < nrOfHamsters; i++)
            {
                // Defaule values wich is given Linq operationes if thay end up without an object
                Cage defaultCage = null;

                // Selects the hamster wich is about to exersice
                // if there is no animals left a dummy instans is selected as default to prevens false value in next step
                var hamster = hDCDbContext.Hamsters                    
                    .OrderBy(h => h.LastExercise)
                    .FirstOrDefault(c => c.ExerciseAreaId != null) ?? new Hamster();

                // Finds wich cage that animal is in 
                var cage = hDCDbContext.Cages                    
                    .FirstOrDefault(c => c.NrOfHamsters < c.Capacity 
                    && c.Gender == hamster.Gender 
                    || c.Gender == Gender.NotChosen) ?? defaultCage;

                //Finds ongoing DayCareStay instatnce
                var thisStay = hDCDbContext.DayCareStays        
                    .OrderByDescending(d => d.Id)
                    .FirstOrDefault(d => d.Id == hamster.id) ?? null;

                //selects a exersiceArea, null is default if none gets chosen
                var ExersiceArea = hDCDbContext.ExerciseAreas.FirstOrDefault(e => e.NrOfHamsters > 0) ?? null;

                //Logic is invoked only if ExersiceArea gets selected
                if (ExersiceArea != null)
                {
                    //updates number of hmasters and Gender of ExersiceArea
                    ExersiceArea.NrOfHamsters--;
                    if (ExersiceArea.NrOfHamsters == 0)
                    {
                        ExersiceArea.Gender = Gender.NotChosen;
                    }

                    // Adding Exersice to DayCareStay.Activity and setting dage and hamster values
                    var activity = new Activity    
                    {
                        AccuredAt = SimulatedTime,
                        HamsterId = hamster.id,
                        TypeOfActivity = TypeOfActivity.Exercises
                    };

                    // Sets cage.gender
                    if (cage.Gender == Gender.NotChosen)        
                    {
                        cage.Gender = hamster.Gender;
                    }
                    //Updates number of hamsters in cage
                    cage.NrOfHamsters++;

                    //updates info in hamster
                    hamster.CageId = cage.Id;              
                    hamster.ExerciseAreaId = null;
                    hamster.LastExercise = SimulatedTime;

                    //saves to db each loop itteration  
                    hDCDbContext.SaveChanges();                 
                }
            }

        }
        private static void SetTimes(string date)
        {
            string sevenOclock = " 07:00:00:0000";
            string startsFromString = date + sevenOclock;
            string format = "yyyy.MM.dd HH:mm:ss:ffff";
            StartsFrom = DateTime.ParseExact(startsFromString, format,
                                             CultureInfo.InvariantCulture);
            SimulatedTime = StartsFrom;
           
        }
    }
}
