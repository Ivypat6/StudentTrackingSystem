using AutoMapper;
using StudentTrackingSystem.Data.Reositories.Interface;
using StudentTrackingSystem.Shared.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentTrackingSystem.Service.Services
{
    public class StudentService : IStudentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IPredictionService _predictionService;

        public StudentService(IUnitOfWork unitOfWork, IMapper mapper, IPredictionService predictionService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _predictionService = predictionService;
        }

        public async Task<IEnumerable<StudentDto>> GetAllStudentsAsync()
        {
            var students = await _unitOfWork.Students.GetAllAsync();
            return _mapper.Map<IEnumerable<StudentDto>>(students);
        }

        public async Task<StudentDto> GetStudentByIdAsync(int id)
        {
            var student = await _unitOfWork.Students.GetByIdAsync(id);
            return _mapper.Map<StudentDto>(student);
        }

        public async Task AddStudentAsync(StudentDto studentDto)
        {
            var student = _mapper.Map<Student>(studentDto);
            await _unitOfWork.Students.AddAsync(student);
            await _unitOfWork.CommitAsync();
        }

        // Implement other methods similarly... 

        public async Task<IEnumerable<PredictionResultDto>>
    GetPerformancePredictionsAsync()
        {
            // Get all students with their grades, attendance, and behavior records
            var students = await _unitOfWork.Students.GetAllAsync();
            var predictionResults = new List<PredictionResultDto>();

            foreach (var student in students)
            {
                var grades = await _unitOfWork.Grades.GetAsync(g => g.StudentId == student.Id);
                var attendances = await _unitOfWork.Attendances.GetAsync(a => a.StudentId == student.Id);
                var behaviorRecords = await _unitOfWork.BehaviorRecords.GetAsync(b => b.StudentId == student.Id);

                var prediction = await _predictionService.PredictStudentPerformanceAsync(student, grades, attendances, behaviorRecords);
                predictionResults.Add(prediction);
            }

            return predictionResults;
        }

        public Task UpdateStudentAsync(StudentDto studentDto)
        {
            throw new NotImplementedException();
        }

        public Task DeleteStudentAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<GradeDto>> GetStudentGradesAsync(int studentId)
        {
            throw new NotImplementedException();
        }

        public Task AddGradeAsync(GradeDto gradeDto)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AttendanceDto>> GetStudentAttendanceAsync(int studentId)
        {
            throw new NotImplementedException();
        }

        public Task AddAttendanceAsync(AttendanceDto attendanceDto)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<BehaviorRecordDto>> GetStudentBehaviorRecordsAsync(int studentId)
        {
            throw new NotImplementedException();
        }

        public Task AddBehaviorRecordAsync(BehaviorRecordDto behaviorRecordDto)
        {
            throw new NotImplementedException();
        }

        public Task<DashboardStatsDto> GetDashboardStatsAsync()
        {
            throw new NotImplementedException();
        }
    }
}
