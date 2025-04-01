using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentTrackingSystem.Service.Services;
using StudentTrackingSystem.Shared.Dto;

namespace StudentTrackingSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentDto>>>
    GetStudents()
        {
            var students = await _studentService.GetAllStudentsAsync();
            return Ok(students);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StudentDto>> GetStudent(int id)
        {
            var student = await _studentService.GetStudentByIdAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return Ok(student);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<ActionResult<StudentDto>> PostStudent(StudentDto studentDto)
        {
            await _studentService.AddStudentAsync(studentDto);
            return CreatedAtAction(nameof(GetStudent), new
            {
                id =
    studentDto.Id
            }, studentDto);
        }

        // Add other endpoints for grades, attendance, etc. 
    }
}
