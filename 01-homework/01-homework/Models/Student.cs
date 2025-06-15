using System.ComponentModel.DataAnnotations;

namespace _01_homework.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; } = string.Empty;
        
        [Required]
        [StringLength(50)]
        public string LastName { get; set; } = string.Empty;
        
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        
        [DataType(DataType.Date)]
        public DateTime EnrollmentDate { get; set; }
        
        // Navigation property
        public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
        
        // Display name for the student
        public string FullName => $"{FirstName} {LastName}";
    }
} 