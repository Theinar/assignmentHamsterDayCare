using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamsterDayCare.Domain
{
    public class Cage
    {
        public int Id { get; set; }
        public Gender Gender { get; set; }
        public ICollection<Hamster> Hamsters { get; set; }
        public int Capacity { get; set; }
        public int NrOfHamsters { get; set; }
        public Cage()
        {
            Gender = Gender.NotChosen;
        }
    }
}
