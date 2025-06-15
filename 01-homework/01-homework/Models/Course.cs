using System.ComponentModel.DataAnnotations;

namespace _01_homework.Models
{
    public class Course
    {
        public int CourseId { get; set; }
        
        [Required]
        [StringLength(10)]
        public string CourseCode { get; set; } = string.Empty;
        
        [Required]
        [StringLength(100)]
        public string Title { get; set; } = string.Empty;
        
        [Range(1, 10)]
        public int Credits { get; set; }
        
        [StringLength(500)]
        public string? Description { get; set; }
        
        [Required]
        [StringLength(50)]
        public string Department { get; set; } = string.Empty;
        
        // Navigation property
        public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    }
} 