using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamsterDayCare.Domain
{
    public class ExerciseArea
    {
        public int Id { get; set; }
        public ICollection<Hamster> Hamsters { get; set; } = new List<Hamster>();
        public int Capacity { get; set; }
        public int NrOfHamsters { get; set; }
    }
}
