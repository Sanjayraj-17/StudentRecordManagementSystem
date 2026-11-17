
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
	}
}
