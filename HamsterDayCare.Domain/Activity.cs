using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamsterDayCare.Domain
{
    public enum TypeOfActivity
    {
        Exercises
    }
    public class Activity
    {
        public int Id { get; set; }
        public int HamsterId { get; set; }
        public TypeOfActivity TypeOfActivity { get; set; }
        public DateTime AccuredAt { get; set; }
    }
}
