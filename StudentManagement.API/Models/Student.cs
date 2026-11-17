
using System.ComponentModel.DataAnnotations;

namespace StudentManagement.API.Models
{
    public class Student
    {
        public int StudentId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [StringLength(150)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Department { get; set; } = string.Empty;

        [Range(1, 100)]
        public int Age { get; set; }
    }
}
