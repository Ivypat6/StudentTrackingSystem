using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentTrackingSystem.Service.Services
{
    public interface IStudentService
    {
        Task<IEnumerable<StudentDto>> GetAllStudentsAsync();
        Task<StudentDto> GetStudentByIdAsync(int id);
        Task AddStudentAsync(StudentDto studentDto);
        Task UpdateStudentAsync(StudentDto studentDto);
        Task DeleteStudentAsync(int id);

        // Additional methods for grades, attendance, etc. 
        Task<IEnumerable<GradeDto>> GetStudentGradesAsync(int studentId);
        Task AddGradeAsync(GradeDto gradeDto);

        Task<IEnumerable<AttendanceDto>> GetStudentAttendanceAsync(int
    studentId);
        Task AddAttendanceAsync(AttendanceDto attendanceDto);

        Task<IEnumerable<BehaviorRecordDto>>
    GetStudentBehaviorRecordsAsync(int studentId);
        Task AddBehaviorRecordAsync(BehaviorRecordDto behaviorRecordDto);

        // Analytics methods 
        Task<IEnumerable<PredictionResultDto>>
    GetPerformancePredictionsAsync();
        Task<DashboardStatsDto> GetDashboardStatsAsync();
    }
}
