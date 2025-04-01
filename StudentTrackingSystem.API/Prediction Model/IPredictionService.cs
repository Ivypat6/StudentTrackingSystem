using StudentTrackingSystem.Data.Models;
using StudentTrackingSystem.Shared.Dto;

namespace StudentTrackingSystem.API.Prediction_Model
{
    public interface IPredictionService
    {
        Task<PredictionResultDto> PredictStudentPerformanceAsync(Student
    student, IEnumerable<Grade> grades, IEnumerable<Attendance>
    attendances, IEnumerable<BehaviorRecord> behaviorRecords);
        Task TrainModelAsync();
    }
}
