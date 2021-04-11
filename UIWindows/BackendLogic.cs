using System;
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

namespace UIWindows
{

    public class BackendLogic
    {

        private HDCDbContext hDCDbContext;
        //private TickerArgs theArgs;
        private ReportArgs reportArgs;

        public BackendLogic(HDCDbContext _hDCDbContext, TickerArgs _theArgs, ReportArgs _reportArgs)
        {
            hDCDbContext = _hDCDbContext;
            //theArgs = _theArgs;
            reportArgs = _reportArgs;

        }

        #region Methodes

        #region Initial seeding

        /// <summary>
        /// Initial seeding of DB, effects cages and ExAreas
        /// </summary>
        internal async Task SeedDB(TickerArgs _theArgs)
        {
            // Read self explanatory titles
            var addCage = AddCage(_theArgs.NumberOfcages, _theArgs.MaxnrOfHamInEachCage);

            var addExersiceAreas = AddExersiceAreas(_theArgs.NumberOfExAreas, _theArgs.MaxnrOfHamInExArea);

            // awaits completion of task methodes
            await Task.WhenAll(addCage, addExersiceAreas);

        }
        /// <summary>
        /// Methode is a type of Reset, resets all values to NULL exept for the logs
        /// Varning this renders parts of earlier logs useles sins re entered enteties vill get new IDs
        /// and there by get uncompairable.
        /// </summary>
        /// <param name="e"></param>
        internal void UnSeedDBAndSeedFromNewArgs(TickerArgs _theArgs)
        {
            var resetDb = ResetDb();

            var seedDb = SeedDB(_theArgs);

            var createAndAddHamsterClientele = CreateAndAddHamsterClientele(_theArgs);

            Task.WhenAll(resetDb, seedDb, createAndAddHamsterClientele);

        }
        /// <summary>
        /// Methode is a type of Reset, resets all values to NULL exept for the logs
        /// Varning this renders parts of earlier logs useles sins re entered enteties vill get new IDs
        /// and there by get uncompairable. Paul standard is as the directives given by teacher in assignment explaination
        /// </summary>
        /// <param name="e"></param>

        /// <summary>
        /// Methode used to resed db in order to reseed it with different DB model
        /// </summary>
        /// <returns></returns>
        private Task ResetDb()
        {
            // Selecting all entities that needs to be removed in order to reed seed db (not logs)
            //var hamsters = hDCDbContext.Hamsters;

            //var cages = hDCDbContext.Cages;

            //var exAreas = hDCDbContext.ExerciseAreas;

            ////Removes selected entities
            //if (hamsters != null)
            //{
            //    hDCDbContext.Hamsters.RemoveRange(hamsters);
            //}
            //if (cages != null)
            //{
            //    hDCDbContext.Cages.RemoveRange(cages);
            //}
            //if (exAreas != null)
            //{
            //    hDCDbContext.ExerciseAreas.RemoveRange(exAreas);
            //}
            hDCDbContext.Database.EnsureDeleted();

            // Saves changes
            hDCDbContext.SaveChanges();

            hDCDbContext = new HDCDbContext();

            hDCDbContext.Database.EnsureCreated();

            return Task.CompletedTask;
        }
        /// <summary>
        /// Methode used to resed db in order to reseed it with different DB model
        /// </summary>
        /// <returns>A db with totaly blank datasets</returns>       
        private Task ResetDbCompleetly()
        {
            // Selecting all entities that needs to be removed in order to reed seed db (INKL LOGS!!!)
            var hamsters = hDCDbContext.Hamsters;

            var cages = hDCDbContext.Cages;

            var exAreas = hDCDbContext.ExerciseAreas;

            var logs = hDCDbContext.DayCareLogs;

            var stays = hDCDbContext.DayCareStays;

            //Removes selected entities
            if (hamsters != null)
            {
                hDCDbContext.Hamsters.RemoveRange(hamsters);
            }
            if (cages != null)
            {
                hDCDbContext.Cages.RemoveRange(cages);
            }
            if (exAreas != null)
            {
                hDCDbContext.ExerciseAreas.RemoveRange(exAreas);
            }
            if (logs != null)
            {
                hDCDbContext.DayCareLogs.RemoveRange(logs);
            }
            if (stays != null)
            {
                hDCDbContext.DayCareStays.RemoveRange(stays);
            }

            // Saves changes
            hDCDbContext.SaveChanges();

            return Task.CompletedTask;
        }

        /// <summary>
        /// methode thet is part off initial seeding, creates the hamster from .csv file. And adds them to the database
        /// </summary>
        internal Task CreateAndAddHamsterClientele(TickerArgs _theArgs)
        {
            List<Hamster> hamsterClientele = new List<Hamster>();   //List for hamsters
            StringBuilder sb = new StringBuilder();
            using (StreamReader sr = new StreamReader(_theArgs.FilePath))    //File containing hamsters
            {
                while (!sr.EndOfStream)         // Creates a string of the complete file
                {
                    sb.Append(sr.ReadLine());
                    sb.Append("¤");
                }
            }
            var attribs = sb.ToString();
            var hamsterAttribArr = attribs.Split("¤");              //Spliting the string first into lines and then down to parsible string objects

            int loopStart = 0;

            if (_theArgs.FilePath == "HamsterlistaCustom.csv")
            {
                loopStart = 1;
            }

            for (int i = loopStart; i < hamsterAttribArr.Length - 1; i++)
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
        private async Task AddExersiceAreas(int _numberOfExAreas, int _maxnrOfHamInEachCage)
        {

            // instansiates ÉxerciseArea and sets Gendet to .NotChosen
            for (int i = 0; i < _numberOfExAreas; i++)
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
        /// <summary>
        /// Ensures that there are no old entitys from earlier canseld simulations
        /// sets all alues to initial values
        /// </summary>
        /// <returns></returns>
        public async Task StartOfTheDayRoutine(TickerArgs _theArgs)
        {
            _theArgs.NumberOfTicks = 0;
            _theArgs.SimulationTime = _theArgs.FictionalStartDate;

            var cages = hDCDbContext.Cages.Where(c => c.Id > 0).ToList();

            var exAreas = hDCDbContext.ExerciseAreas.Where(ex => ex.Id > 0).ToList();

            var hamsters = hDCDbContext.Hamsters.Where(h => h.Id > 0).ToList();

            for (int i = 0; i < cages.Count; i++)
            {
                cages[i].NrOfHamsters = 0;
                cages[i].Gender = Gender.NotChosen;
            }

            for (int i = 0; i < exAreas.Count; i++)
            {
                exAreas[i].NrOfHamsters = 0;
                exAreas[i].Gender = Gender.NotChosen;
            }

            for (int i = 0; i < hamsters.Count; i++)
            {
                hamsters[i].CageId = null;
                hamsters[i].ExerciseAreaId = null;
            }

            await Task.CompletedTask;

        }

        /// <summary>
            /// The metode that gets called every tick, uses theArgs to determen course of action in the spesific tick
            /// </summary>
            /// <param name="_theArgs"></param>
            /// <returns></returns>
        public async Task SimulationProgress(TickerArgs _theArgs)
        {
            // Number of ticks determins action 
            if (_theArgs.NumberOfTicks % 100 == 0)
            {
                await CheckInHamsters(_theArgs);
            }

            if (_theArgs.NumberOfTicks % 10 == 0)
            {
                //selects the exersicearea wich decides course of action
                var exArea = hDCDbContext.ExerciseAreas.FirstOrDefault(e => e.NrOfHamsters != 0) ?? null;

                // if exArea.NumberOfHmasters != 0 exArea must be unpopulated first
                if (exArea != null)
                {
                    MoveHamsterFromExersiceArea(_theArgs);
                    MoveHamsterToExersiceArea(_theArgs);
                }
                // if exArea.NumberOfHmasters == 0 exArea can be populates first
                else
                {
                    MoveHamsterToExersiceArea(_theArgs);
                }
            }
            // else occures when daycare closes. a last move is made befour checkout
            if ((_theArgs.NumberOfTicks + 1) % 100 == 0)
            {
                var exArea = hDCDbContext.ExerciseAreas.First();
                if (exArea.NrOfHamsters != 0)
                {
                    await MoveHamsterFromExersiceArea(_theArgs); /////////////// kanske sker samtidigt som den nedan
                }
                CheckOutHamsters(_theArgs);
            }

            await Task.WhenAll();

            UpdateMainReport(_theArgs);

        }

        /// <summary>
        /// Methode that wich is callen att the start of each simulated day. A representation of the arrival of the hamsters. On methodecall the
        /// methode instansiates a new DayCareLog instance wich each DayCareStay is added to. A DayCAreStay is a object containing all 
        /// recorded activities of each animal for this spesific day.
        /// </summary>
        public async Task CheckInHamsters(TickerArgs _theArgs)
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
                    .FirstOrDefault(h => h.CageId == null) ?? new Hamster() { Id = 0 };


                // Selects first avaleble cage
                var cage = this.hDCDbContext.Cages
                        .AsEnumerable()
                        .FirstOrDefault(c => c.Capacity > c.NrOfHamsters
                        & ((c.Gender == hamster.Gender)
                        || (c.NrOfHamsters == 0))) ?? defaultCage;

                // Executes logic if valid values are chosen in cone above
                if (hamster != null && cage != null)
                {

                    //instansiates i new DayCareStays with above selected objects
                    this.hDCDbContext.DayCareStays.Add(new DayCareStay()
                    {
                        DayCareLogId = dayCareLog.Id,
                        HamasterId = hamster.Id,
                        CageId = cage.Id,
                        Arrival = _theArgs.SimulationTime,

                    });

                    // save so that the object above can be referenced
                    this.hDCDbContext.SaveChanges();

                    // Selects current DaycareStay and adds new activity
                    var thisStay = this.hDCDbContext.DayCareStays.OrderByDescending(d => d.Id).First();
                    thisStay.Activities.Add(new Activity()
                    {
                        AccuredAt = _theArgs.SimulationTime,
                        TypeOfActivity = TypeOfActivity.CheckIn
                    });

                    // Updates database fields for each entity 
                    hamster.CageId = cage.Id;
                    hamster.LastActivity = _theArgs.SimulationTime;

                    // Updates the hamsters in cage
                    try
                    {
                        cage.Hamsters.Add(hamster);
                    }
                    catch (Exception ex)
                    {

                        throw;
                    }
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
            await Task.CompletedTask;
        }
        /// <summary>
        /// Metode that represents the outchecking of all animals in the daycare, CageId of each animal 
        /// is set to null, animals are removed from thair cages, when a cage is empty cage.Gender is set 
        /// to unchosen, checkout time is set
        /// </summary>
        public void CheckOutHamsters(TickerArgs _theArgs)
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
                    .FirstOrDefault(h => h.CageId != null) ?? new Hamster() { Id = 0 };

                // Finds wich cage that animal is in 
                var cage = this.hDCDbContext.Cages
                    .FirstOrDefault(c => c.Id == hamster.CageId) ?? defaultCage;

                // Selects ongoing DaycareStay
                var thisStay = this.hDCDbContext.DayCareStays.OrderByDescending(d => d.Id).FirstOrDefault(d => d.HamasterId == hamster.Id) ?? null;

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
                    thisStay.CheckOut = _theArgs.SimulationTime;

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
        public void MoveHamsterToExersiceArea(TickerArgs _theArgs)
        {

            for (int j = 0; j < _theArgs.NumberOfExAreas; j++)
            {
                //Loop itterates and add as manny animals as the nrOfHamsters variable indicates
                for (int i = 0; i < _theArgs.MaxnrOfHamInExArea; i++)
                {
                    // Defaule values wich is given in Linq operations if thay end up without an object
                    Cage defaultCage = null;

                    // Selects the hamster wich is about to exersice
                    // sets a dummy instans to prevens false value in next step
                    var hamster = this.hDCDbContext.Hamsters
                        .OrderBy(h => h.LastActivity).FirstOrDefault(c => c.CageId != null) ?? new Hamster() { Id = 0 };

                    // Finds wich cage that animal is in 
                    var cage = this.hDCDbContext.Cages
                        .FirstOrDefault(c => c.Id == hamster.CageId) ?? defaultCage;

                    //Finds ongoing DayCareStay instatnce for above selected hamster
                    var thisStay = this.hDCDbContext.DayCareStays
                        .OrderByDescending(d => d.Id)
                        .FirstOrDefault(d => d.HamasterId == hamster.Id) ?? null;

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
                            AccuredAt = _theArgs.SimulationTime,
                            HamsterId = hamster.Id,
                            TypeOfActivity = TypeOfActivity.MoveToExeExerciseArea
                        });
                        thisStay.Activities.Add(new Activity
                        {
                            AccuredAt = _theArgs.SimulationTime,
                            HamsterId = hamster.Id,
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

        }
        /// <summary>
        /// Moves animals exersiceArea to cage, sets gender on ExersiceArea and cage if needed. Updates DaycareStay.Activitys aswell
        /// </summary>
        /// <param name="nrOfHamsters"></param>
        public async Task MoveHamsterFromExersiceArea(TickerArgs _theArgs)
        {
            for (int i = 0; i < _theArgs.MaxnrOfHamInExArea; i++)
            {
                // Defaule values wich is given Linq operationes if thay end up without an object
                Cage defaultCage = null;

                // Selects the hamster wich is about to exersice
                // if there is no animals left a dummy instans is selected as default to prevens false value in next step
                var hamster = this.hDCDbContext.Hamsters
                    .OrderBy(h => h.LastActivity)
                    .FirstOrDefault(c => c.ExerciseAreaId != null) ?? new Hamster() { Id = 0 };

                // Finds wich cage that animal is in 
                var cage = this.hDCDbContext.Cages
                    .FirstOrDefault(c => c.NrOfHamsters < c.Capacity
                    && c.Gender == hamster.Gender
                    || c.Gender == Gender.NotChosen) ?? defaultCage;

                //Finds ongoing DayCareStay instatnce
                var thisStay = this.hDCDbContext.DayCareStays
                    .OrderByDescending(d => d.Id)
                    .FirstOrDefault(d => d.Id == hamster.Id) ?? null;

                //selects a exersiceArea, null is default if none gets chosen
                var ExersiceArea = this.hDCDbContext.ExerciseAreas.FirstOrDefault(ex => ex.NrOfHamsters > 0) ?? null;

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
                        AccuredAt = _theArgs.SimulationTime,
                        HamsterId = hamster.Id,
                        TypeOfActivity = TypeOfActivity.MoveFromExerciseArea
                    };

                    // Sets cage.gender
                    if (cage != null)
                    {
                        if (cage.Gender == Gender.NotChosen)
                        {
                            cage.Gender = hamster.Gender;
                        }

                        //Updates number of hamsters in cage
                        cage.NrOfHamsters++;
                    }
                    //updates info in hamster
                    hamster.CageId = cage.Id;
                    hamster.ExerciseAreaId = null;
                    hamster.LastActivity = _theArgs.SimulationTime;

                    //saves to db each loop itteration  
                    this.hDCDbContext.SaveChanges();
                }
            }
            await Task.CompletedTask;
        }

        #endregion

        #region UI Stuff

        internal void UpdateReportArgs(TickerArgs _theArgs)
        {
            UpdateMainReport(_theArgs);
        }

        private void UpdateMainReport(TickerArgs _theArgs)
        {

            var simulationID = hDCDbContext.DayCareLogs.OrderByDescending(dcl => dcl.Id).First();
            var numberOfhamsters = hDCDbContext.Hamsters.Count();
            var GenderProcent = GenderProcentQoute();
            var avgWaitForActivity = AvrageExersiceWait(_theArgs);
            string mainReport = "";

            //mainReport = String.Format("{0, -120}{1}\n\n{2, -120}{3}\n{4,-110}{5}\n{6}{7, 120}\n{8}{9, 120}\n{10}{11, 120}{12}\n" +
            //                           "{13, -120}{14}\n{15, -120}{16}\n\n{17, -120}{18}\n{19, -120}{20}\n\n{21, -120}{22}\n{23, -120}{24}"
            //                          , "INFO TYPE" //index 0
            //                          , "VALUE"     //index 1
            //                          , "Simulation ID:"    //index 2
            //                          , simulationID.Id     //index 3
            //                          , "Simulation started at:"        //index 4
            //                          , _theArgs.FictionalStartDate     //index 5
            //                          , "Simulation time now:"      //index 6
            //                          , _theArgs.SimulationTime     //index 7
            //                          , "Number of Hamdters in Cleintele this simulation:"  //index 8
            //                          , numberOfhamsters                                    //index 9
            //                          , "The Hamster gender distribution is"    //index 10
            //                          , GenderProcent                           //index 11
            //                          , "(M/F)"                                  //index 12
            //                          , "Tick number:"              //index 13
            //                          , _theArgs.NumberOfTicks    //index 14
            //                          , "AVG Wait for first Exersice: " //index 15
            //                          , avgWaitForActivity           //index 16
            //                          , "Number of Cages:"                           //index 17
            //                          , _theArgs.NumberOfcages                        //index 18
            //                          , "Capacity of each Cage:"                //indes 19
            //                          , _theArgs.MaxnrOfHamInEachCage            //index 20
            //                          , "Number of Exersice Areas:"              //index 21
            //                          , _theArgs.NumberOfExAreas             //index 22
            //                          , "Capacity of each Exersice Area:"    //index 23
            //                          , _theArgs.MaxnrOfHamInExArea);        //index 24


            mainReport = $"INFO TYPE                                                        VALUE\n\n" +
                         $"Simulation ID:                                                   {simulationID.Id}\n\n" +
                         $"Simulation started at:                                           { _theArgs.FictionalStartDate}\n" +
                         $"Simulation time now:                                             {_theArgs.SimulationTime}\n" +
                         $"Tick number now:                                                 {_theArgs.NumberOfTicks}\n\n" +
                         $"Number of Hamdters in Cleintele this simulation:                 {numberOfhamsters}\n" +
                         $"The Hamster gender distribution is:                              {GenderProcent} (M/F)\n\n" +
                         $"AVG Wait for first Exersice:                                     {avgWaitForActivity}\n" +
                         $"Number of Cages:                                                 {_theArgs.NumberOfcages}\n" +
                         $"Capacity of each Cage:                                           {_theArgs.MaxnrOfHamInEachCage}\n\n" +
                         $"Number of Exersice Areas:                                        {_theArgs.NumberOfExAreas}\n" +
                         $"Capacity of each Exersice Area:                                  {_theArgs.MaxnrOfHamInExArea}\n"; 

            reportArgs.MainReport = mainReport;
        }
        /// <summary>
        /// Calculates Animal Gender % with dbQueries
        /// </summary>
        /// <returns></returns>
        private string GenderProcentQoute()
        {
            // Queries for total number of hamsters and male hamster
            double numberOfhamsters = hDCDbContext.Hamsters.Count();
            double numberOfMale = hDCDbContext.Hamsters.Where(h => h.Gender == Gender.Male).Count();

            // calculates the % male and female
            double hamstersMaleProc = Math.Round(((numberOfMale / numberOfhamsters) * 100), 2);
            double hamstersFrmaleProc = 100 - hamstersMaleProc;

            // returns the calculated result as string
            string result = $"{hamstersMaleProc} / {hamstersFrmaleProc} %";
            return result;
        }
        private string AvrageExersiceWait(TickerArgs _theArgs)
        {
            DateTime defaultNowTime = _theArgs.SimulationTime;
            var timeSpanInMinutesList = new List<double>();

            var dayCareLog = hDCDbContext.DayCareLogs.OrderByDescending(dcl => dcl.Id).First();
            var dayCareStays = hDCDbContext.DayCareStays.Where(cds => cds.DayCareLogId == dayCareLog.Id);

            foreach (var dcs in dayCareStays)
            {
                var firstActivity = dcs.Activities.OrderBy(a => a.AccuredAt).Select(ac => ac.AccuredAt).First();
                var firstActivityOfNow = dcs.Activities.OrderBy(a => a.AccuredAt).Select(ac => ac.AccuredAt).Skip(1).DefaultIfEmpty(defaultNowTime).FirstOrDefault();

                var timeSpanInMinutes = (firstActivityOfNow - firstActivity).TotalMinutes;

                timeSpanInMinutesList.Add(timeSpanInMinutes);

            }

            var avgWaitTime = timeSpanInMinutesList.Average();

            string resault = $"{avgWaitTime}";

            return resault;
        }
        #endregion

        #endregion
    }
}