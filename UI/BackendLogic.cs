﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
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

    internal class BackendLogic
    {

        private HDCDbContext hDCDbContext;
        private DateTime simulatedTime;
        public DateTime SimulatedTime { get => simulatedTime;}

        public BackendLogic(HDCDbContext _hDCDbContext, DateTime _simulatedTime)
        {
            hDCDbContext = _hDCDbContext;
            simulatedTime = _simulatedTime;

        }




        #region Methodes

        #region Initial seeding
        /// <summary>
        /// Initial seeding of DB
        /// </summary>
        private async Task SeedDB(TickerArgs e)
        {
            // Read self explanatory titles
            var addCage = AddCage(e.NumberOfcages, e.MaxnrOfHamInEachCage);

            var addExersiceAreas = AddExersiceAreas(e.NumberOfExAreas, e.MaxnrOfHamInExArea);

            // awaits completion of task methodes
            await Task.WhenAll(addCage, addExersiceAreas);

        }




        internal void UnSeedDBAndStartFresh(TickerArgs e)
        {
            var resetDb = ResetDb();                /////////// not implemented
            Thread.Sleep(500);

            var seedDb = SeedDB(e);
            Thread.Sleep(500);

            var createAndAddHamsterClientele = CreateAndAddHamsterClientele();

            Task.WhenAll(resetDb, seedDb, createAndAddHamsterClientele);

        }
        internal void UnSeedDBAndStartWithOldClientel(TickerArgs e)
        {
            var resetDb = ResetDb();                /////////// not implemented
            Thread.Sleep(500);

            var seedDb = SeedDB(e);
            Thread.Sleep(500);

            var createAndAddHamsterClientele = CreateAndAddHamsterClientele();

            Task.WhenAll(resetDb, seedDb, createAndAddHamsterClientele);

        }
        private Task ResetDb()
        {


            return Task.CompletedTask;
        }       // Not yet implemented !!!!!!!!!!!

        /// <summary>
        /// methode thet is part off initial seeding, creates the hamster from .csv file. And adds them to the database
        /// </summary>
        internal Task CreateAndAddHamsterClientele()
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

            return Task.CompletedTask;

        }
        /// <summary>
        /// methode thet is part off initial seeding, creates the hamster(s) from .csv file with content provided by user. And adds them to the database
        /// </summary>
        internal Task AddCustomHamsterClientele()
        {
            List<Hamster> hamsterClientele = new List<Hamster>();   //List for hamsters
            StringBuilder sb = new StringBuilder();
            using (StreamReader sr = new StreamReader("HamsterlistaCustom.csv"))    //File containing hamsters
            {
                while (!sr.EndOfStream)         // Creates a string of the complete file
                {
                    sb.Append(sr.ReadLine());
                    sb.Append("¤");
                }
            }
            var attribs = sb.ToString();
            var hamsterAttribArr = attribs.Split("¤");              //Spliting the string frirst into lines and then down to parsible string objects


            for (int i = 1; i < hamsterAttribArr.Length - 1; i++)
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

            return Task.CompletedTask;

        }
        /// <summary>
        /// methode thet is part off initial seeding, creates initial number of cages
        /// </summary>
        /// <param name="length"></param>
        private async Task AddCage(int length)
        {
            for (int i = 0; i < length; i++)
            {
                this.hDCDbContext.Cages.Add(new Cage() { Capacity = 3 });
                this.hDCDbContext.SaveChanges();
            }

            await Task.CompletedTask;
        }
        private async Task AddCage(int _numberOfCages, int _cageCapacity)
        {
            for (int i = 0; i < _numberOfCages; i++)
            {
                this.hDCDbContext.Cages.Add(new Cage() { Capacity = _cageCapacity });
                this.hDCDbContext.SaveChanges();
            }

            await Task.CompletedTask;
        }
        /// <summary>
        /// methode thet is part off initial seeding, adds instances og ExersiceAreas to db
        /// </summary>
        /// <param name="_numberOfcages"></param>
        private async Task AddExersiceAreas(int _numberOfcages, int _maxnrOfHamInEachCage)
        {
            // instansiates ÉxerciseArea and sets Gendet to .NotChosen
            for (int i = 0; i < _numberOfcages; i++)
            {
                this.hDCDbContext.ExerciseAreas.Add(new ExerciseArea()
                {
                    Capacity = _maxnrOfHamInEachCage,
                    Gender = Gender.NotChosen
                });
                this.hDCDbContext.SaveChanges();
            }

            await Task.CompletedTask;
        }

        #endregion

        #region Animal activity methodes

        internal Task SimulationProgress(TickerArgs e)
        {

            if (e.SimulationTime.TimeOfDay == e.FictionalDate.TimeOfDay)               
            {
                CheckInHamsters(e);
            }
            else if ( e.SimulationTime.TimeOfDay > e.FictionalDate.TimeOfDay 
                && e.SimulationTime.TimeOfDay < (e.FictionalDate.TimeOfDay + TimeSpan.FromHours(10)))
            {
                //selects the exersicearea wich decides course of action
                var exArea = hDCDbContext.ExerciseAreas.FirstOrDefault(e => e.NrOfHamsters != 0) ?? null;

                // if exArea.NumberOfHmasters != 0 exArea must be unpopulated first
                if (exArea != null)
                {
                    MoveHamsterFromExersiceArea(e);
                    MoveHamsterToExersiceArea(e);
                }
                // if exArea.NumberOfHmasters == 0 exArea can be populates first
                else
                {
                    MoveHamsterToExersiceArea(e);
                }
            }
            // else occures when daycare closes. a last move is made befour checkout
            else
            {
                var exArea = hDCDbContext.ExerciseAreas.First();
                if (exArea.NrOfHamsters != 0)
                {
                    MoveHamsterFromExersiceArea(e); 
                }
                CheckOutHamsters(e);
            }

            return Task.CompletedTask;
        }

        /// <summary>
        /// Methode that wich is callen att the start of each simulated day. A representation of the arrival of the hamsters. On methodecall the
        /// methode instansiates a new DayCareLog instance wich each DayCareStay is added to. A DayCAreStay is a object containing all 
        /// recorded activities of each animal for this spesific day.
        /// </summary>
        public Task CheckInHamsters(TickerArgs e)
        {
            // Defaule values wich is given while lopp to Linq operationes if thay end up without an object
            bool loopBool = true;
            Cage defaultCage = null;

            // Adds new daycarelog
            this.hDCDbContext.DayCareLogs.Add(new DayCareLog());
            // is saved directly so that it can be raferenced by other entities
            this.hDCDbContext.SaveChanges();

            //selects the loginstance
            var dayCareLog = this.hDCDbContext.DayCareLogs.OrderByDescending(d => d.Id).First();

            //assurance that loop itterates
            bool loopAssurance = false;

            while (loopBool)
            {
                loopAssurance = true;

                //Selects e hamster from hamsters in DB
                var hamster = this.hDCDbContext.Hamsters
                    .FirstOrDefault(h => h.CageId == null) ?? new Hamster() { id = 0 };

                
                // Selects first avaleble cage
                var cage = this.hDCDbContext.Cages
                        .AsEnumerable()
                        .FirstOrDefault(c => c.Capacity > c.NrOfHamsters
                        & ((c.Gender == hamster.Gender)
                        || (c.NrOfHamsters == 0))) ?? defaultCage;

                // Executes logic if valid vaues are chosen in cone above
                if (hamster != null && cage != null)
                {
                    //instansiates i new DayCareStays with above selected objects
                    this.hDCDbContext.DayCareStays.Add(new DayCareStay()
                    {
                        DayCareLogId = dayCareLog.Id,
                        HamasterId = hamster.id,
                        CageId = cage.Id,
                        Arrival = e.SimulationTime,

                    });

                    // save so that the object above can be referenced
                    this.hDCDbContext.SaveChanges();

                    // Selects current DaycareStay and adds new activity
                    var thisStay = this.hDCDbContext.DayCareStays.OrderByDescending(d => d.Id).First();
                    thisStay.Activities.Add(new Activity()
                    {
                        AccuredAt = e.SimulationTime,
                        TypeOfActivity = TypeOfActivity.CheckIn
                    });

                    // Updates database fields for each entity 
                    hamster.CageId = cage.Id;

                    // Updates the hamsters in cage
                    cage.Hamsters.Add(hamster);
                    cage.NrOfHamsters++;

                    // sets gender of cage if needed
                    if (cage.Gender == Gender.NotChosen)
                    {
                        cage.Gender = hamster.Gender;
                    }

                    //Saves changes in database before loop starts over
                    this.hDCDbContext.SaveChanges();

                }
                else
                {
                    loopBool = false;

                    if (loopAssurance = false)
                    {
                        Console.WriteLine("CheckInHamsters- loop never itterated");
                    }
                }

            }
            return Task.CompletedTask;
        }
        /// <summary>
        /// Metode that represents the outchecking of all animals in the daycare, CageId of each animal 
        /// is set to null, animals are removed from thair cages, when a cage is empty cage.Gender is set 
        /// to unchosen, checkout time is set
        /// </summary>
        public void CheckOutHamsters(TickerArgs e)
        {
            // Defaule values wich is given while lopp to Linq operationes if thay end up without an object
            bool loopBool = true;
            Cage defaultCage = null;

            //assurance that loop itterates
            bool loopAssurance = false;

            // while wich continues untill all animals hav left thair cages
            while (loopBool)
            {
                loopAssurance = true;
                // Selects the hamster wich is about to check put
                // sets a dummy instans to prevens false value in next step
                var hamster = this.hDCDbContext.Hamsters
                    .FirstOrDefault(h => h.CageId != null) ?? new Hamster() { id = 0 };

                // Finds wich cage that animal is in 
                var cage = this.hDCDbContext.Cages
                    .FirstOrDefault(c => c.Id == hamster.CageId) ?? defaultCage;

                // Selects ongoing DaycareStay
                var thisStay = this.hDCDbContext.DayCareStays.OrderByDescending(d => d.Id).FirstOrDefault(d => d.HamasterId == hamster.id) ?? null;

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
                    thisStay.CheckOut = e.SimulationTime;

                    //saves to db each loop itteration
                    this.hDCDbContext.SaveChanges();
                }
                else
                {
                    // if cage is null loopBoll is set to false
                    loopBool = false;

                    if (loopAssurance = false)
                    {
                        Console.WriteLine("CheckOutHamsters- loop never itterated");
                    }
                }
            }
        }
        /// <summary>
        /// Moves animals from cage to exersiceArea, sets gender on ExersiceArea and cage if needed. Updates DaycareStay.Activitys aswell
        /// </summary>
        /// <param name="nrOfHamsters"></param>
        public void MoveHamsterToExersiceArea(TickerArgs e)
        {
          
            //Loop itterates and add as manny animals as the nrOfHamsters variable indicates
            for (int i = 0; i < e.MaxnrOfHamInExArea; i++)
            {
                // Defaule values wich is given in Linq operations if thay end up without an object
                Cage defaultCage = null;

                // Selects the hamster wich is about to exersice
                // sets a dummy instans to prevens false value in next step
                var hamster = this.hDCDbContext.Hamsters
                    .OrderBy(h => h.LastExercise).FirstOrDefault(c => c.CageId != null) ?? new Hamster() { id = 0 };

                // Finds wich cage that animal is in 
                var cage = this.hDCDbContext.Cages
                    .FirstOrDefault(c => c.Id == hamster.CageId) ?? defaultCage;

                //Finds ongoing DayCareStay instatnce for above selected hamster
                var thisStay = this.hDCDbContext.DayCareStays
                    .OrderByDescending(d => d.Id)
                    .FirstOrDefault(d => d.HamasterId == hamster.id) ?? null;

                // Selects an Exersicerea sets to null if none is chosen
                var ExersiceArea = this.hDCDbContext.ExerciseAreas.FirstOrDefault(e => e.NrOfHamsters < e.Capacity) ?? null;

                // Log that executes if ExerciseArea is chosen
                if (ExersiceArea != null && cage != null)
                {
                    // ExersiceArea gender is set if needed
                    if (ExersiceArea.Gender == Gender.NotChosen)
                    {
                        ExersiceArea.Gender = hamster.Gender;
                    }

                    // Adding Exersice to DayCareStay.Activity and setting dage and hamster values
                    thisStay.Activities.Add(new Activity
                    {
                        AccuredAt = e.SimulationTime,
                        HamsterId = hamster.id,
                        TypeOfActivity = TypeOfActivity.MoveToExeExerciseArea
                    });
                    thisStay.Activities.Add(new Activity
                    {
                        AccuredAt = e.SimulationTime,
                        HamsterId = hamster.id,
                        TypeOfActivity = TypeOfActivity.Exercise
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
                    this.hDCDbContext.SaveChanges();
                }
            }

        }
        /// <summary>
        /// Moves animals exersiceArea to cage, sets gender on ExersiceArea and cage if needed. Updates DaycareStay.Activitys aswell
        /// </summary>
        /// <param name="nrOfHamsters"></param>
        public void MoveHamsterFromExersiceArea(TickerArgs e)
        {
            for (int i = 0; i < e.MaxnrOfHamInExArea; i++)
            {
                // Defaule values wich is given Linq operationes if thay end up without an object
                Cage defaultCage = null;

                // Selects the hamster wich is about to exersice
                // if there is no animals left a dummy instans is selected as default to prevens false value in next step
                var hamster = this.hDCDbContext.Hamsters
                    .OrderBy(h => h.LastExercise)
                    .FirstOrDefault(c => c.ExerciseAreaId != null) ?? new Hamster() { id = 0 };

                // Finds wich cage that animal is in 
                var cage = this.hDCDbContext.Cages
                    .FirstOrDefault(c => c.NrOfHamsters < c.Capacity
                    && c.Gender == hamster.Gender
                    || c.Gender == Gender.NotChosen) ?? defaultCage;

                //Finds ongoing DayCareStay instatnce
                var thisStay = this.hDCDbContext.DayCareStays
                    .OrderByDescending(d => d.Id)
                    .FirstOrDefault(d => d.Id == hamster.id) ?? null;

                //selects a exersiceArea, null is default if none gets chosen
                var ExersiceArea = this.hDCDbContext.ExerciseAreas.FirstOrDefault(e => e.NrOfHamsters > 0) ?? null;

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
                        AccuredAt = e.SimulationTime,
                        HamsterId = hamster.id,
                        TypeOfActivity = TypeOfActivity.MoveFromExerciseArea
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
                    hamster.LastExercise = e.SimulationTime;

                    //saves to db each loop itteration  
                    this.hDCDbContext.SaveChanges();
                }
            }

        }

        #endregion

        #region Other

        /// <summary>
        /// Sets the Datetime StartsFrom and simulated time to 7:00:00 on the date thet the user enter
        /// </summary>
        /// <param name="date"></param>


        #endregion

        #endregion
    }
}