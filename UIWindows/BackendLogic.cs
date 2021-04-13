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

        #region Fields

        private HDCDbContext hDCDbContext;
        //private TickerArgs theArgs;
        private ReportArgs ReportArgs; 
        #endregion

        #region Constructor
        public BackendLogic(HDCDbContext _hDCDbContext, TickerArgs _theArgs, ReportArgs _ReportArgs)
        {
            hDCDbContext = _hDCDbContext;
            //theArgs = _theArgs;
            ReportArgs = _ReportArgs;

        } 
        #endregion

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

            hDCDbContext.SaveChanges();

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
                var exArea = hDCDbContext.ExerciseAreas.FirstOrDefault(ex => ex.NrOfHamsters > 0) ?? null;

                // if exArea.NumberOfHmasters != 0 exArea must be unpopulated first
                if (exArea != null)
                {
                    for (int i = 0; i < _theArgs.NumberOfExAreas; i++)
                    {
                        await MoveHamsterFromExersiceArea(_theArgs);
                    }
                    for (int i = 0; i < _theArgs.NumberOfExAreas; i++)
                    {
                        MoveHamsterToExersiceArea(_theArgs);
                    }
                }
                // if exArea.NumberOfHmasters == 0 exArea can be populates first
                else
                {
                    for (int i = 0; i < _theArgs.NumberOfExAreas; i++)
                    {
                        MoveHamsterToExersiceArea(_theArgs);
                    }
                }


            }
            // else occures when daycare closes. a last move is made befour checkout
            if ((_theArgs.NumberOfTicks + 1) % 100 == 0)
            {

                for (int i = 0; i < _theArgs.NumberOfExAreas; i++)
                {
                    await MoveHamsterFromExersiceArea(_theArgs); /////////////// kanske sker samtidigt som den nedan
                }

                CheckOutHamsters(_theArgs);

            }
            await Task.WhenAll();

            UpdateReportArgs(_theArgs);
            if (ReportArgs.IsTrackingHamster == false)
            {
                CageReport();
            }
            if (ReportArgs.IsTrackingHamster == true)
            {
               HamsterReport(_theArgs);
               
            }

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
        /// <summary>
        /// Moves animals exersiceArea to cage, sets gender on ExersiceArea and cage if needed. Updates DaycareStay.Activitys aswell
        /// </summary>
        /// <param name="nrOfHamsters"></param>
        public async Task MoveHamsterFromExersiceArea(TickerArgs _theArgs)
        {
            // Finds wich exersiceArea wich contains animals
            var exersiceArea = hDCDbContext.ExerciseAreas
                .FirstOrDefault(ex => ex.NrOfHamsters > 0) ?? null;

            if (exersiceArea != null)
            {
                for (int i = 0; i < exersiceArea.Capacity; i++)
                {
                    // Defaule values wich is given Linq operationes if thay end up without an object
                    Cage defaultCage = null;

                    // Selects the hamster wich is about to exersice
                    // if there is no animals left a dummy instans is selected as default to prevens false value in next step
                    var hamster = hDCDbContext.Hamsters
                        .First(h => h.ExerciseAreaId == exersiceArea.Id);

                    //selects a cage, null is default if none gets chosen
                    var cage = this.hDCDbContext.Cages
                        .First(c => c.NrOfHamsters < c.Capacity
                        && (c.Gender == Gender.NotChosen
                        || c.Gender == hamster.Gender));


                    //Finds ongoing DayCareStay instatnce
                    var thisStay = this.hDCDbContext.DayCareStays
                        .OrderByDescending(d => d.Id)
                        .First(d => d.Id == hamster.Id);

                    //updates number of hmasters and Gender of ExersiceArea
                    exersiceArea.NrOfHamsters--;
                    if (exersiceArea.NrOfHamsters == 0)
                    {
                        exersiceArea.Gender = Gender.NotChosen;
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

        private async Task UpdateReportArgs(TickerArgs _theArgs)
        {
            var mainreportTask = UpdateMainReport(_theArgs);
            var generateTickReport = GenerateTickReport(_theArgs);
            
            await Task.WhenAll(mainreportTask, generateTickReport);
        }
        /// <summary>
        /// methode updates ReportArgs wich is use in UI windows of information to user
        /// </summary>
        /// <param name="_theArgs"></param>
        /// <returns></returns>
        private Task UpdateMainReport(TickerArgs _theArgs)
        {
            // finds this simulation id
            var simulationID = hDCDbContext.DayCareLogs.OrderByDescending(dcl => dcl.Id).First();
            // gets number of hamsters
            var numberOfhamsters = hDCDbContext.Hamsters.Where(h => h.CageId != null || h.ExerciseAreaId != null).Count();
            // gets GenderProcent from other method
            var GenderProcent = GenderProcentQoute();
            // gets the agrage time each animal had to wait between arrival and first exersice
            var avgWaitForActivity = AvrageExersiceWait(_theArgs);
            // gets the activities that accured this tick

            string mainReportValue;

            // seting values for mainReport
            mainReportValue =
             $"VALUE\n\n" +
             $"{simulationID.Id}\n\n" +
             $"{ _theArgs.FictionalStartDate}\n" +
             $"{_theArgs.SimulationTime}\n" +
             $"{_theArgs.NumberOfTicks}\n\n" +
             $"{numberOfhamsters}\n" +
             $"{_theArgs.MaxnrOfHamInEachCage *_theArgs.NumberOfcages}\n" +
             $"{GenderProcent} (M/F)\n\n" +
             $"{avgWaitForActivity}\n" +
             $"{_theArgs.NumberOfcages}\n" +
             $"{_theArgs.MaxnrOfHamInEachCage}\n\n" +
             $"{_theArgs.NumberOfExAreas}\n" +
             $"{_theArgs.MaxnrOfHamInExArea}\n";

            // assigning them to args
            ReportArgs.MainReportValues = mainReportValue;

            return Task.CompletedTask;
        }
        /// <summary>
        /// methode updates ReportArgs wich is use in Tickreport
        /// </summary>
        /// <param name="_theArgs"></param>
        /// <returns></returns>
        internal Task GenerateTickReport(TickerArgs _theArgs)
        {
            // generates statistics about that has happend during last tick
            var activitiesThistick =
                hDCDbContext.Activities
                .Where(a => a.AccuredAt == _theArgs.SimulationTime).ToList();
                 var acts = SortActs(activitiesThistick);

            // gets number of hamsters wich is in thair bages at this moment
            var hamstersInCages = hDCDbContext.Hamsters.Where(h => h.CageId != null).Count();

            // gets how manny hamsters are in different ExAreas at the moment
            var hamstersInExAreas = hDCDbContext.Hamsters.Where(h => h.ExerciseAreaId != null).Count();
                 
            // // values are updated in report args
            var tickReportBodyValues = $"\n{acts[0]}\n\n" +
                                       $"{acts[1]}\n\n" +
                                       $"{acts[2]}\n\n" +
                                       $"{acts[3]}\n\n" +
                                       $"{hamstersInCages}\n\n" +
                                       $"{hamstersInExAreas}\n\n";

            ReportArgs.TickNowReportHead = $"TICK NR {_theArgs.NumberOfTicks}";
            ReportArgs.TickNowReportValues = tickReportBodyValues;

            return Task.CompletedTask;
        }
        /// <summary>
        /// sorts chosen activities 
        /// </summary>
        /// <param name="activitiesThistick"></param>
        /// <returns></returns>
        private static List<int> SortActs(List<Activity> activitiesThistick)
        {
            int checkIN = 0;
            int checkOut = 0;
            int moveToExersice = 0;
            int moveFromEx = 0;
            List<int> acts = new List<int>();
            for (int i = 0; i < activitiesThistick.Count; i++)
            {
                if (activitiesThistick[i].TypeOfActivity == TypeOfActivity.CheckIn)
                {
                    checkIN++;
                }
                else if (activitiesThistick[i].TypeOfActivity == TypeOfActivity.CheckIn)
                {
                    checkOut++;
                }
                else if (activitiesThistick[i].TypeOfActivity == TypeOfActivity.CheckIn)
                {
                    moveToExersice++;
                }
                else if (activitiesThistick[i].TypeOfActivity == TypeOfActivity.CheckIn)
                {
                    moveFromEx++;
                }
            }
            acts.Add(checkIN);
            acts.Add(checkOut);
            acts.Add(moveToExersice);
            acts.Add(moveFromEx);
            return acts;
        }

        private Task HamsterReport(TickerArgs _theArgs)
        {
            //Selects wich hamster to track 
            var hamsterToTrack = hDCDbContext.Hamsters
                                       .First(h => h.Id == ReportArgs.TrackingID);

            var standardAct = new Activity { AccuredAt =  _theArgs.FictionalStartDate, TypeOfActivity = TypeOfActivity.CheckIn};

            var lastActivity = hDCDbContext.Activities
                                  .Where(a => a.HamsterId == hamsterToTrack.Id)
                                  .OrderByDescending(a => a.AccuredAt).FirstOrDefault() ?? standardAct;


 
            // selects the whereaboutes of specified hamster
            string whereAbouts = string.Empty;
            int? inCage = hamsterToTrack.CageId;
            int? InExArea = hamsterToTrack.ExerciseAreaId;

            if (inCage != null)
            {
                whereAbouts = $"Cage {inCage}";
            }
            else if (InExArea != null)
            {
                whereAbouts = $"Exercise Area  {InExArea}";
            }
            else
            {
                whereAbouts = $"Not in Daycare";
            }


            ReportArgs.SecondReportTypes = $"Last Activity Accured At:\n" +
                                            $"Activity:\n" +
                                            $"Age:\n" +
                                            $"Name:\n" +
                                            $"Hamster ID:\n " +
                                            $"Owner:\n" +
                                            $"Whereabouts:";


            ReportArgs.SecondReportValues = $"{hamsterToTrack.LastActivity.TimeOfDay.ToString()}\n" + //Last Activity Accured A
                                            $"{lastActivity.TypeOfActivity}\n" +                      //"Activity:
                                            $"{hamsterToTrack.Age}\n" +                               //"Age:
                                            $"{hamsterToTrack.Name}\n" +                              // "Name:
                                            $"{hamsterToTrack.Id}\n" +                                // hamster ID
                                            $"{hamsterToTrack.Owner}\n" +                             // owner               
                                            $"{whereAbouts}\n";                                       // whereabouts                      

            return Task.CompletedTask;

        }
        private Task CageReport()
        {
            // selects the specifyed cage
            var cage = hDCDbContext.Cages.First(c => c.Id == ReportArgs.TrackingID);

            // selects the specifyed cages population
            var hamsters = hDCDbContext.Hamsters.Where(h => h.CageId == cage.Id).ToList();

            // creates dummy to put in list if not all places is filled
            var dummy = new Hamster() { Name = "Slot not filled", Owner = "" , Id = 0};

            for (int i = hamsters.Count; i < 6; i++)
            {

                hamsters.Add(dummy);

            }

            // updates ReportArgs with new values
            ReportArgs.SecondReportTypes = $"Number Of hamsters now: {cage.NrOfHamsters}";

            ReportArgs.SecondReportValues = $"Name, Hamster ID, Owner\n\n" +
                                            $"{hamsters[0].Name}, {hamsters[0].Id}, {hamsters[0].Owner}\n\n" +
                                            $"{hamsters[1].Name}, {hamsters[0].Id}, {hamsters[1].Owner}\n\n" +
                                            $"{hamsters[2].Name}, {hamsters[0].Id}, {hamsters[2].Owner}\n\n" +
                                            $"{hamsters[3].Name}, {hamsters[0].Id}, {hamsters[3].Owner}\n\n" +
                                            $"{hamsters[4].Name}, {hamsters[0].Id}, {hamsters[4].Owner}\n\n" +
                                            $"{hamsters[5].Name}, {hamsters[0].Id}, {hamsters[5].Owner}\n\n";

            return Task.CompletedTask;

        }
        internal Task GenerateEndReport(TickerArgs _theArgs)
        {
            // finds this simulation id
            var simulationID = hDCDbContext.DayCareLogs.OrderByDescending(dcl => dcl.Id).First();
            // gets number of hamsters
            var numberOfhamsters = hDCDbContext.Hamsters.Count();
            // gets GenderProcent from other method
            var GenderProcent = GenderProcentQoute();
            // Sets the time to closing time the dau before
            _theArgs.SimulationTime = _theArgs.SimulationTime.AddHours(-14.0);
            // gets the agrage time each animal had to wait between arrival and first exersice
            var avgWaitForActivity = AvrageExersiceWait(_theArgs);
            // get this daycareLog
            var dcl = hDCDbContext.DayCareLogs.OrderByDescending(d => d.Id).First();
            // counts number of activities
            var totNumberOfActiviteies = hDCDbContext.DayCareStays
                .Where(d => d.DayCareLogId == dcl.Id)
                .Select(a => a.Activities).Count();

            string EndReportTypes = $"INFO TYPE\n\n" +
                                     $"Simulation ID:\n\n" +
                                     $"Simulation started at:\n" +
                                     $"Simulation ended at:\n" +
                                     $"Nubmer of ticks total \n\n" +
                                     $"Number of Hamdters in Cleintele this simulation:\n" +
                                     $"Capacity of daycare in simulation\n" +
                                     $"The Hamster gender distribution is:\n\n" +
                                     $"AVG Wait for first Exersice:\n" +
                                     $"Number of Cages:\n" +
                                     $"Capacity of each Cage:\n\n" +
                                     $"Number of Exersice Areas:\n" +
                                     $"Capacity of each Exersice Area:\n" +
                                     $"Number of activities made";

            string endReportValue = $"VALUE\n\n" +
                                   $"{simulationID.Id}\n\n" +
                                   $"{ _theArgs.FictionalStartDate}\n" +
                                   $"{_theArgs.SimulationTime}\n" +
                                   $"{_theArgs.EndTick}\n\n" +
                                   $"{numberOfhamsters}\n" +
                                   $"{_theArgs.NumberOfcages * _theArgs.MaxnrOfHamInEachCage}\n" +
                                   $"{GenderProcent} (M/F)\n\n" +
                                   $"{avgWaitForActivity}\n" +
                                   $"{_theArgs.NumberOfcages}\n" +
                                   $"{_theArgs.MaxnrOfHamInEachCage}\n\n" +
                                   $"{_theArgs.NumberOfExAreas}\n" +
                                   $"{_theArgs.MaxnrOfHamInExArea}\n" +
                                   $"{totNumberOfActiviteies}";

            ReportArgs.EndReportTypes = EndReportTypes;
            ReportArgs.EndReportValues = endReportValue;

            return Task.CompletedTask;
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
        /// <summary>
        /// generates Avrage wait from animal arrival to first exersice, based on each day
        /// </summary>
        /// <param name="_theArgs"></param>
        /// <returns></returns>
        private string AvrageExersiceWait(TickerArgs _theArgs)
        {
            // list of double for all the timeperiodes in minutes
            var timeSpanInMinutesList = new List<double>();

            // selects Iqueriables wich is used to det Datetimes wich avrage si based on
            var dayCareLog = hDCDbContext.DayCareLogs.OrderByDescending(dcl => dcl.Id).First();
            var dayCareStays = hDCDbContext.DayCareStays.Where(cds => cds.DayCareLogId == dayCareLog.Id);

            // foreach wich equation to get wait timespan
            foreach (var dcs in dayCareStays)
            {
                var firstActivity = dcs.Activities.OrderBy(a => a.AccuredAt).Select(ac => ac.AccuredAt).First();
                var firstActivityOfNow = dcs.Activities.OrderBy(a => a.AccuredAt).Select(ac => ac.AccuredAt).Skip(1).DefaultIfEmpty(_theArgs.SimulationTime).FirstOrDefault();

                var timeSpanInMinutes = (firstActivityOfNow - firstActivity).TotalMinutes;

                timeSpanInMinutesList.Add(timeSpanInMinutes);

            }
            // calculates avrage in double
            var avgWaitTime = timeSpanInMinutesList.Average();

            // result to use
            string resault = $"{avgWaitTime}";

            return resault;
        }
        #endregion

        #endregion
    }
}