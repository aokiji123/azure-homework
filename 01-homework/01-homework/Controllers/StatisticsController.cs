using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using _01_homework.Models;

namespace _01_homework.Controllers
{
    public class StatisticsController : Controller
    {
        private readonly SchoolContext _context;

        public StatisticsController(SchoolContext context)
        {
            _context = context;
        }

        // GET: Statistics
        public async Task<IActionResult> Index()
        {
            var statistics = new StatisticsViewModel();

            // Get all students by courses
            statistics.StudentsByCourses = await _context.Courses
                .Include(c => c.Enrollments)
                .ThenInclude(e => e.Student)
                .Select(c => new CourseStatistic
                {
                    Course = c,
                    StudentCount = c.Enrollments.Count(),
                    Students = c.Enrollments.Select(e => e.Student).ToList()
                })
                .ToListAsync();

            // Get total amount of students
            statistics.TotalStudents = await _context.Students.CountAsync();

            // Get students with grades higher than 90
            statistics.HighPerformingStudents = await _context.Enrollments
                .Include(e => e.Student)
                .Include(e => e.Course)
                .Where(e => e.Grade.HasValue)
                .ToListAsync();

            // Filter high performing students (grades > 90)
            statistics.HighPerformingStudents = statistics.HighPerformingStudents
                .Where(e => ConvertGradeToInteger(e.Grade.Value) > 90)
                .ToList();

            // Get unique high performing students
            statistics.UniqueHighPerformingStudents = statistics.HighPerformingStudents
                .GroupBy(e => e.StudentId)
                .Select(g => new StudentPerformance
                {
                    Student = g.First().Student,
                    HighGradeEnrollments = g.ToList(),
                    AverageGrade = g.Average(e => ConvertGradeToInteger(e.Grade.Value))
                })
                .ToList();

            return View(statistics);
        }

        private int ConvertGradeToInteger(Grade grade)
        {
            return grade switch
            {
                Grade.A => 95,  // A = 90-100, using 95 as representative
                Grade.B => 85,  // B = 80-89, using 85 as representative
                Grade.C => 75,  // C = 70-79, using 75 as representative
                Grade.D => 65,  // D = 60-69, using 65 as representative
                Grade.F => 50,  // F = 0-59, using 50 as representative
                _ => 0
            };
        }
    }

    // ViewModels for Statistics
    public class StatisticsViewModel
    {
        public List<CourseStatistic> StudentsByCourses { get; set; } = new List<CourseStatistic>();
        public int TotalStudents { get; set; }
        public List<Enrollment> HighPerformingStudents { get; set; } = new List<Enrollment>();
        public List<StudentPerformance> UniqueHighPerformingStudents { get; set; } = new List<StudentPerformance>();
    }

    public class CourseStatistic
    {
        public Course Course { get; set; } = null!;
        public int StudentCount { get; set; }
        public List<Student> Students { get; set; } = new List<Student>();
    }

    public class StudentPerformance
    {
        public Student Student { get; set; } = null!;
        public List<Enrollment> HighGradeEnrollments { get; set; } = new List<Enrollment>();
        public double AverageGrade { get; set; }
    }
} 