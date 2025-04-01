using Microsoft.ML.Data;

namespace StudentTrackingSystem.API.Prediction_Model
{
    public class StudentPerformanceData
    {
        [LoadColumn(0)] public float AverageGrade { get; set; }
        [LoadColumn(1)] public float AttendanceRate { get; set; }
        [LoadColumn(2)] public int BehaviorIncidents { get; set; }
        [LoadColumn(3)] public string PerformanceCategory { get; set; } // "At Risk", "On Track", "Excelling" 
}
}
