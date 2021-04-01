using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamsterDayCare.Domain
{
    public class ExerciseArea
    {
        int a;
        public int Id { get; set; }
        public virtual ICollection<Hamster> Hamsters { get; set; } = new List<Hamster>();
        public int Capacity { get; set; }
        public int NrOfHamsters { get => Hamsters.Count; }
    }
}
