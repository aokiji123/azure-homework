using System.ComponentModel.DataAnnotations;

namespace _01_homework.Models
{
    public enum Grade
    {
        A, B, C, D, F
    }

    public class Enrollment
    {
        public int EnrollmentId { get; set; }
        
        public int StudentId { get; set; }
        
        public int CourseId { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime EnrollmentDate { get; set; }
        
        public Grade? Grade { get; set; }
        
        // Navigation properties
        public virtual Student Student { get; set; } = null!;
        public virtual Course Course { get; set; } = null!;
    }
} 