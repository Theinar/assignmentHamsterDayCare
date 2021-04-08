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

namespace UI
{

    public class BackendLogic
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
        internal async Task SeedDB(TickerArgs e)
        {
            // Read self explanatory titles
            var addCage = AddCage(e.NumberOfcages, e.MaxnrOfHamInEachCage);

            var addExersiceAreas = AddExersiceAreas(e.NumberOfExAreas, e.MaxnrOfHamInExArea);

            // awaits completion of task methodes
            await Task.WhenAll(addCage, addExersiceAreas);

        }
        /// <summary>
        /// Methode is a type of Reset, resets all values to NULL exept for the logs
        /// Varning this renders parts of earlier logs useles sins re entered enteties vill get new IDs
        /// and there by get uncompairable.
        /// </summary>
        /// <param name="e"></param>
        internal void UnSeedDBAndStartFresh(TickerArgs _theArgs)
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
        internal void UnSeedDBAndStartWithPaulStandard(TickerArgs _theArgs)
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
            var hamsters = hDCDbContext.Hamsters;
            
            var cages = hDCDbContext.Cages;

            var exAreas = hDCDbContext.ExerciseAreas;

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

            // Saves changes
            hDCDbContext.SaveChanges();

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
        /// <summary>
        /// Ensures that there are no old entitys from earlier canseld simulations
        /// </summary>
        /// <returns></returns>
        internal void EnsureDaysReadyToStart()
        {
            //Selects all hamsters
            var hamsters = hDCDbContext.Hamsters.Where(h => h.id > 0).ToList();

            // itterates throu animals setting thair whereabouts to null
            for (int i = 0; i < hamsters.Count; i++)
            {
                hamsters[i].CageId = null;
                hamsters[i].ExerciseAreaId = null;
            }

            // selects cages and ensures that thay are empty
            var cages = hDCDbContext.Cages.Where(c => c.Id > 0).ToList();

            for (int i = 0; i < cages.Count; i++)
            {
                cages[i].NrOfHamsters = 0;
            }
            // selects ExerciseAreas and ensures that thay are empty
            var exArea =  hDCDbContext.ExerciseAreas.Where(ex => ex.Id > 0).ToList();

            for (int i = 0; i < exArea.Count; i++)
            {
                exArea[i].NrOfHamsters = 0;
            }
            hDCDbContext.SaveChanges();
        }

        #endregion

        #region Animal activity methodes

        /// <summary>
        /// The metode that gets called every tick, uses theArgs to determen course of action in the spesific tick
        /// </summary>
        /// <param name="_theArgs"></param>
        /// <returns></returns>
        public async Task SimulationProgress(TickerArgs _theArgs)
        {

            if (_theArgs.TickCounter % 100 == 0)               
            {
               await CheckInHamsters(_theArgs);
            }

            if ( _theArgs.TickCounter % 10 == 0)
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
            if ((_theArgs.TickCounter + 1) % 100 == 0)
            {
                var exArea = hDCDbContext.ExerciseAreas.First();
                if (exArea.NrOfHamsters != 0)
                {
                    await MoveHamsterFromExersiceArea(_theArgs); /////////////// kanske sker samtidigt som den nedan
                }
                CheckOutHamsters(_theArgs);
            }

            await Task.WhenAll();
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
                    .FirstOrDefault(h => h.CageId == null) ?? new Hamster() { id = 0 };

                
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
                        HamasterId = hamster.id,
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
                    .OrderBy(h => h.LastActivity).FirstOrDefault(c => c.CageId != null) ?? new Hamster() { id = 0 };

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
                        AccuredAt = _theArgs.SimulationTime,
                        HamsterId = hamster.id,
                        TypeOfActivity = TypeOfActivity.MoveToExeExerciseArea
                    });
                    thisStay.Activities.Add(new Activity
                    {
                        AccuredAt = _theArgs.SimulationTime,
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
                        HamsterId = hamster.id,
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

        #region Other

        /// <summary>
        /// Sets the Datetime StartsFrom and simulated time to 7:00:00 on the date thet the user enter
        /// </summary>
        /// <param name="date"></param>


        #endregion

        #endregion
    }
}