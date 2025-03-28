using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentTrackingSystem.Data.Models
{
    public class Grade : BaseEntity
    {
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public string AssessmentType { get; set; } // Exam, Quiz, 

        public float Score { get; set; }
        public float MaxScore { get; set; } = 100; // Default max score 
        public DateTime AssessmentDate { get; set; }
        public string Comments { get; set; }

        // Navigation properties 
        public Student Student { get; set; }
        public Course Course { get; set; }
    }
}
