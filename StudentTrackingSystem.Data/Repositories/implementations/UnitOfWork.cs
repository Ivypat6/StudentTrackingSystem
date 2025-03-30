using StudentTrackingSystem.Data.Models;
using StudentTrackingSystem.Data.Reositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentTrackingSystem.Data.Repositories.implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Students = new Repository<Student>(_context);
            Courses = new Repository<Course>(_context);
            Enrollments = new Repository<Enrollment>(_context);
            Attendances = new Repository<Attendance>(_context);
            Grades = new Repository<Grade>(_context);
            BehaviorRecords = new Repository<BehaviorRecord>(_context);
        }

        public IRepository<Student> Students { get; private set; }
        public IRepository<Course> Courses { get; private set; }
        public IRepository<Enrollment> Enrollments { get; private set; }
        public IRepository<Attendance> Attendances { get; private set; }
        public IRepository<Grade> Grades { get; private set; }
        public IRepository<BehaviorRecord> BehaviorRecords
        {
            get; private
    set;
        }

        public async Task<int> CommitAsync() => await
    _context.SaveChangesAsync();

        public void Dispose() => _context.Dispose();
    }
}
