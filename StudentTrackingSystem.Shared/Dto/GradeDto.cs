using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentTrackingSystem.Shared.Dto
{
    public class GradeDto
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public string AssessmentType { get; set; }
        public float Score { get; set; }
        public float MaxScore { get; set; } = 100;
        public DateTime AssessmentDate { get; set; }
        public string Comments { get; set; }
        public string StudentName { get; set; }
        public string CourseName { get; set; }
    }
}
