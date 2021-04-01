using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamsterDayCare.Domain
{
    public class Hamster
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string Owner { get; set; }
        public int Age { get; set; }
        public Gender Gender { get; set; }
        public int? CageId { get; set; }
        public DateTime LastExercise { get; set; }
        public virtual Cage Cage { get; set; }
        public int? ExerciseAreaId { get; set; }
        public virtual ExerciseArea ExerciseArea { get; set; }


    }
}
