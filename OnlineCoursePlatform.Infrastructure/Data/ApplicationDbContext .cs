using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineCoursePlatform.Domain.Entities;
using System.Reflection.Emit;

namespace OnlineCoursePlatform.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // User-Course (Instructor) Relationship
            modelBuilder.Entity<Course>()
                .HasOne(c => c.Instructor)
                .WithMany(u => u.Courses)
                .HasForeignKey(c => c.InstructorId)
                .OnDelete(DeleteBehavior.Restrict);
            // Course-Module Relationship
            modelBuilder.Entity<Module>()
                .HasOne(m => m.Course)
                .WithMany(c => c.Modules)
                .HasForeignKey(m => m.CourseId)
                .OnDelete(DeleteBehavior.Cascade);
            // Module-Lesson Relationship
            modelBuilder.Entity<Lesson>()
                .HasOne(l => l.Module)
                .WithMany(m => m.Lessons)
                .HasForeignKey(l => l.ModuleId)
                .OnDelete(DeleteBehavior.Cascade);
            // Course-Enrollment Relationship
            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Course)
                .WithMany()
                .HasForeignKey(e => e.CourseId)
                .OnDelete(DeleteBehavior.Cascade);
            // Student-Enrollment Relationship
            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Student)
                .WithMany(u => u.Enrollments)
                .HasForeignKey(e => e.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            addUser(modelBuilder);
            seedRoles(modelBuilder);
            assignRole(modelBuilder);
        }
        private static void seedRoles(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole() { Id = "a330b209-871f-45fc-9a8d-f357f9bff3b1", Name = "Admin", ConcurrencyStamp = "1", NormalizedName = "Admin".ToUpper() },
                new IdentityRole() { Id = "b331b209-871f-45fc-9a8d-f357f9bff3b1", Name = "Instructor", ConcurrencyStamp = "2", NormalizedName = "Seller".ToUpper() },
                new IdentityRole() { Id = "c332b209-871f-45fc-9a8d-f357f9bff3b1", Name = "Student", ConcurrencyStamp = "3", NormalizedName = "Customer".ToUpper() }
                );
        }
        private static void addUser(ModelBuilder modelBuilder)
        {
            var adminUser = new User()
            {
                Id = "7e53a491-a9de-4c75-af44-ff3271a5176c",
                FirstName = "Super",
                LastName = "Admin",
                UserName = "super_admin",
                Email = "super@admin.com",
                EmailConfirmed = true,
                NormalizedUserName = "super_admin".ToUpper(),
                NormalizedEmail = "super@admin.com".ToUpper(),
                DateOfBirth = new DateTime(2000, 1, 1)

            };
            var passwordHasher = new PasswordHasher<User>();
            adminUser.PasswordHash = passwordHasher.HashPassword(adminUser, "P@ssw0rd");
            modelBuilder.Entity<User>().HasData(adminUser);
        }

        private void assignRole(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    UserId = "7e53a491-a9de-4c75-af44-ff3271a5176c", // Admin user ID
                    RoleId = "a330b209-871f-45fc-9a8d-f357f9bff3b1"  // Admin role ID
                }
            );
        }
    }
}
