using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentTrackingSystem.Shared.Dto
{
    public class PredictionResultDto
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public string Prediction { get; set; } // "At Risk", "On Track", "Excelling" 
        public float Probability { get; set; }
        public string[] RecommendedActions { get; set; }
    }
}
