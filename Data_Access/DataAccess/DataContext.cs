using Data_Access.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data_Access.DataAccess
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<Address> Address { get; set; }
        public DbSet<Course> Course { get; set; }
        public DbSet<DoWork> DoWork { get; set; }
        public DbSet<Student> Student { get; set; }
        public DbSet<StudentCourse> StudentCourse { get; set; }
        public DbSet<Gender> Gender { get; set; }
        public DbSet<AddressType> AddressType { get; set; }
        public DbSet<AuthUser> AuthUser { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentCourse>()
                .HasKey(t => new { t.StudentId, t.CourseId });

            modelBuilder.Entity<StudentCourse>()
                .HasOne(pt => pt.Student)
                .WithMany(p => p.StudentCourses)
                .HasForeignKey(pt => pt.StudentId);

            modelBuilder.Entity<StudentCourse>()
                .HasOne(pt => pt.Course)
                .WithMany(t => t.StudentCourses)
                .HasForeignKey(pt => pt.CourseId);
        }
    }
}
