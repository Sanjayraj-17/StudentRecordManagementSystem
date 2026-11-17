
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

        public async Task<List<Student>> GetAllStudentsAsync()
        {
            return await _repository.GetAllStudentsAsync();
        }

        public async Task<Student?> GetStudentByIdAsync(int id)
        {
            return await _repository.GetStudentByIdAsync(id);
        }

        public async Task<bool> UpdateStudentAsync(int id, Student student)
        {
            return await _repository.UpdateStudentAsync(id, student);
        }

        public async Task<bool> DeleteStudentAsync(int id)
        {
            return await _repository.DeleteStudentAsync(id);
        }
    }
}
