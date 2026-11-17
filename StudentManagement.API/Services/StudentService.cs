
using StudentManagement.API.Models;
using StudentManagement.API.Repositories;

namespace StudentManagement.API.Services
{
    public class StudentService
    {
        private readonly StudentRepository _repository;

        public StudentService(StudentRepository repository)
        {
            _repository = repository;
        }

        public async Task AddStudentAsync(Student student)
        {
            await _repository.AddStudentAsync(student);
        }
    }
}
