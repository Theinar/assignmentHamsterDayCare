using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamsterDayCare.Domain
{
    public class DayCareInfo : EventArgs
    {
        public ICollection<Hamster> Hamsters { get; set; } = new List<Hamster>();
        public ICollection<Activity> Activities { get; set; } = new List<Activity>();
    }
}
