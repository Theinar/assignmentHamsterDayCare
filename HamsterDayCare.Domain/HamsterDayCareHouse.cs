using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamsterDayCare.Domain
{
    class HamsterDayCareHouse
    {
        public event EventHandler<DayCareInfo> DayCareInfo;
        public ICollection<Cage> Cages { get; set; } = new List<Cage>();
        public ExerciseArea ExerciseArea { get; set; }
        public DayCareLog DayCareLog { get; set; }
    }
}
