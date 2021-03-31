using HamsterDayCare.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.Extensions.Logging;
using System.Threading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace HamsterDayCare.Data
{
    public class HDCDbContext : DbContext
    {
        public DbSet<Hamster> Hamsters { get; set; }
        public DbSet<Cage> Cages { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<ExerciseArea> ExerciseArea { get; set; }
        public DbSet<DayCareStay> DayCareStays { get; set; }
        public DbSet<DayCareLog> DayCareLog { get; set; }

        
        private  StreamWriter logAll = new StreamWriter("HamsterProdictionLogAll.txt", append: true);
        private StreamWriter logLast = new StreamWriter("HamsterProdictionLogLast.txt");
       // private StreamReader sr = new StreamReader("HamsterProdictionLogLast.txt");



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           
            optionsBuilder.UseSqlServer(
                " Server = BARRI\\SQLEXPRESS; Database = TestHamster; Trusted_Connection = True;")
                .LogTo(Console.WriteLine, LogLevel.Information).EnableSensitiveDataLogging();
           
    //        optionsBuilder.UseSqlServer(
    //" Server = BARRI\\SQLEXPRESS; Database = TestHamster; Trusted_Connection = True;")
    //.LogTo(logAll.WriteLine, LogLevel.Information).EnableSensitiveDataLogging();

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Hamster>()
            //    .HasMany(h => h.Battles)
            //    .WithMany(b => b.Samurais)
            //    .UsingEntity<BattleSamurai>(bs => bs.HasOne<Battle>().WithMany(), bs => bs.HasOne<Samurai>().WithMany())
            //    .Property(bs => bs.JoinedBattle)
            //    .HasDefaultValueSql("getdate()");

            //modelBuilder.Entity<Horse>().ToTable("Hourse");
            //modelBuilder.Entity<DayCareLog>().HasNoKey();

        }
        //private void ReadAndWriter()
        //{
        //    using (this.sr)
        //    {
        //        while (!this.sr.EndOfStream)
        //        {
        //            string line = sr.ReadLine();
        //            logAll.WriteLine(line);
        //        }
        //    }
        //}
    }
}
