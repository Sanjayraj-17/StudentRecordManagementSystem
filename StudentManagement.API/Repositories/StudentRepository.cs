
using StudentManagement.API.Data;
using StudentManagement.API.Models;

namespace StudentManagement.API.Repositories
{
    public class StudentRepository
    {
        private readonly ApplicationDbContext _context;

        public StudentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddStudentAsync(Student student)
        {
            await _context.Students.AddAsync(student);

            await _context.SaveChangesAsync();
        }
    }
}
