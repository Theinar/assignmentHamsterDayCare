using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamsterDayCare.Domain
{
    public class DayCareStay
    {
        public int Id { get; set; }
        public int HamasterId { get; set; }
        public int CageId { get; set; }
        public int DayCareLogId { get; set; }
        public DateTime Arrival { get; set; }
        public DateTime CheckOut { get; set; }
        public virtual ICollection<Activity> Activities { get; set; } = new List<Activity>();
        public DayCareStay()
        {

        }

    }
}
 