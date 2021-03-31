using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamsterDayCare.Domain
{
    public class DayCareLog
    {
        public int Id { get; set; }
        public ICollection<DayCareStay> DayCareStays { get; set; } = new List<DayCareStay>();

    }
}
