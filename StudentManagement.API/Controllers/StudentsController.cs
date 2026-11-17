
using Microsoft.AspNetCore.Mvc;
using StudentManagement.API.Models;
using StudentManagement.API.Services;

namespace StudentManagement.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class StudentsController : ControllerBase
	{
		private readonly StudentService _studentService;

		public StudentsController(StudentService studentService)
		{
			_studentService = studentService;
		}

		// CREATE - POST: api/Students
		[HttpPost]
		public async Task<IActionResult> AddStudent(Student student)
		{
			await _studentService.AddStudentAsync(student);

			return Ok(new
			{
				message = "Student added successfully",
				student
			});
		}

		// READ - GET: api/Students
		[HttpGet]
		public async Task<IActionResult> GetAllStudents()
		{
			var students = await _studentService.GetAllStudentsAsync();

			return Ok(students);
		}

		// READ - GET: api/Students/1
		[HttpGet("{id}")]
		public async Task<IActionResult> GetStudentById(int id)
		{
			var student = await _studentService.GetStudentByIdAsync(id);

			if (student == null)
			{
				return NotFound(new
				{
					message = "Student not found"
				});
			}

			return Ok(student);
		}

		// UPDATE - PUT: api/Students/1
		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateStudent(
			int id, Student student)
		{
			var result = await _studentService.UpdateStudentAsync(
				id, student);

			if (!result)
			{
				return NotFound(new
				{
					message = "Student not found"
				});
			}

			return Ok(new
			{
				message = "Student updated successfully"
			});
		}

		// DELETE - DELETE: api/Students/1
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteStudent(int id)
		{
			var result = await _studentService.DeleteStudentAsync(id);

			if (!result)
			{
				return NotFound(new
				{
					message = "Student not found"
				});
			}

			return Ok(new
			{
				message = "Student deleted successfully"
			});
		}
	}
}
