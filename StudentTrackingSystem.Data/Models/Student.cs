using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentTrackingSystem.Data.Models
{
    public class Student : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }
        // Navigation properties 
        public ICollection<Enrollment> Enrollments { get; set; }
        public ICollection<Attendance> Attendances { get; set; }
        public ICollection<Grade> Grades { get; set; }
        public ICollection<BehaviorRecord> BehaviorRecords { get; set; }
    }
}
