using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentTrackingSystem.Data.Models
{
    public class Attendance : BaseEntity
    {
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public DateTime Date { get; set; }
        public bool IsPresent { get; set; }
        public string Remarks { get; set; }

        // Navigation properties 
        public Student Student { get; set; }
        public Course Course { get; set; }
    }
}
