using Microsoft.EntityFrameworkCore;

namespace _01_homework.Models
{
    public class SchoolContext : DbContext
    {
        public SchoolContext(DbContextOptions<SchoolContext> options) : base(options)
        {
        }

        public DbSet<Student> Students { get; set; } = null!;
        public DbSet<Course> Courses { get; set; } = null!;
        public DbSet<Enrollment> Enrollments { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure Student entity
            modelBuilder.Entity<Student>()
                .HasIndex(s => s.Email)
                .IsUnique();

            // Configure Course entity
            modelBuilder.Entity<Course>()
                .HasIndex(c => c.CourseCode)
                .IsUnique();

            // Configure Enrollment entity (Many-to-Many relationship)
            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Student)
                .WithMany(s => s.Enrollments)
                .HasForeignKey(e => e.StudentId);

            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Course)
                .WithMany(c => c.Enrollments)
                .HasForeignKey(e => e.CourseId);

            // Seed data
            modelBuilder.Entity<Student>().HasData(
                new Student { StudentId = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com", EnrollmentDate = new DateTime(2023, 1, 15) },
                new Student { StudentId = 2, FirstName = "Jane", LastName = "Smith", Email = "jane.smith@example.com", EnrollmentDate = new DateTime(2024, 1, 15) }
            );

            modelBuilder.Entity<Course>().HasData(
                new Course { CourseId = 1, CourseCode = "CS101", Title = "Introduction to Computer Science", Credits = 3, Department = "Computer Science", Description = "Basic computer science concepts" },
                new Course { CourseId = 2, CourseCode = "MATH101", Title = "Calculus I", Credits = 4, Department = "Mathematics", Description = "Introduction to differential calculus" }
            );

            modelBuilder.Entity<Enrollment>().HasData(
                new Enrollment { EnrollmentId = 1, StudentId = 1, CourseId = 1, EnrollmentDate = new DateTime(2024, 8, 1), Grade = Grade.A },
                new Enrollment { EnrollmentId = 2, StudentId = 1, CourseId = 2, EnrollmentDate = new DateTime(2024, 8, 1), Grade = Grade.B },
                new Enrollment { EnrollmentId = 3, StudentId = 2, CourseId = 1, EnrollmentDate = new DateTime(2024, 11, 1), Grade = Grade.A }
            );
        }
    }
} 