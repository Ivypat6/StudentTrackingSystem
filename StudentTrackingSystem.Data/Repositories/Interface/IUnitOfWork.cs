using StudentTrackingSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentTrackingSystem.Data.Reositories.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Student> Students { get; }
        IRepository<Course> Courses { get; }
        IRepository<Enrollment> Enrollments { get; }
        IRepository<Attendance> Attendances { get; }
        IRepository<Grade> Grades { get; }
        IRepository<BehaviorRecord> BehaviorRecords { get; }

        Task<int> CommitAsync();
    }
}
