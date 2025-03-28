using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentTrackingSystem.Data.Models
{
    public class BehaviorRecord : BaseEntity
    {
        public int StudentId { get; set; }
        public DateTime Date { get; set; }
        public string BehaviorType { get; set; } // Positive/Negative 
        public string Description { get; set; }
        public string ActionTaken { get; set; }

        // Navigation properties 
        public Student Student { get; set; }
    }

}
