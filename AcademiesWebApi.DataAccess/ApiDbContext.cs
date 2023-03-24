using AcademiesWebApi.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademiesWebApi.DataAccess
{
    public partial class ApiDbContext : IdentityDbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {

        }

        public virtual DbSet<FootballTeam> FootballTeam  { get; set; } = null!;

        public virtual DbSet<School> School { get; set; } = null!;
        public virtual DbSet<Teacher> Teacher { get; set; } = null!;
        public virtual DbSet<Student> Student { get; set; } = null!;
        public virtual DbSet<StudentCourse> StudentCourse { get; set; } = null!;
        public virtual DbSet<Course> Course { get; set; } = null!;
        public virtual DbSet<Grade> Grade { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region SCHOOL
            modelBuilder.Entity<School>( entity =>
                {
                    entity.ToTable("School");

                    entity.HasKey(p => p.Id);
                    entity.Property(e => e.Id)
                            .HasColumnName("id");

                    entity.Property(e => e.Name)
                            .HasColumnName("name")
                            .IsRequired()
                            .HasMaxLength(100)
                            .IsUnicode(false);

                    entity.Property(e => e.Web)
                            .HasColumnName("web")
                            .IsRequired()
                            .HasMaxLength(254)
                            .IsUnicode(false);

                    entity.Property(e => e.Email)
                            .HasColumnName("email")
                            .IsRequired()
                            .HasMaxLength(254)
                            .IsUnicode(false);

                    entity.Property(e => e.Phone)
                            .HasColumnName("phone")
                            .IsRequired()
                            .HasMaxLength(45)
                            .IsUnicode(false);
                }
            );
            #endregion

            #region STUDENT
            modelBuilder.Entity<Student>(entity =>
            {
                entity.ToTable("Student");

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                        .HasColumnName("id");

                entity.Property(e => e.FirstName)
                        .HasColumnName("firstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false);

                entity.Property(e => e.LastName)
                        .HasColumnName("lastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false);

                entity.Property(e => e.Email)
                        .HasColumnName("email")
                        .IsRequired()
                        .HasMaxLength(254)
                        .IsUnicode(false);

                entity.Property(e => e.Phone)
                        .HasColumnName("phone")
                        .IsRequired()
                        .HasMaxLength(45)
                        .IsUnicode(false);

                entity.Property(e => e.SchoolId)
                        .HasColumnName("schoolId");

                entity.HasOne(e => e.School)
                        .WithMany(e => e.Students)
                          .HasForeignKey(e => e.SchoolId)
                            .HasConstraintName("FK_students_schools");
                
            });
            #endregion

            #region TEACHER
            modelBuilder.Entity<Teacher>(entity =>
            {
                entity.ToTable("Teacher");

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                        .HasColumnName("id");

                entity.Property(e => e.FirstName)
                        .HasColumnName("firstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false);

                entity.Property(e => e.LastName)
                        .HasColumnName("lastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false);

                entity.Property(e => e.Email)
                        .HasColumnName("email")
                        .IsRequired()
                        .HasMaxLength(254)
                        .IsUnicode(false);

                entity.Property(e => e.Phone)
                        .HasColumnName("phone")
                        .IsRequired()
                        .HasMaxLength(45)
                        .IsUnicode(false);

                entity.Property(e => e.SchoolId).HasColumnName("schoolId");

                entity.HasOne(e => e.School)
                        .WithMany(e => e.Teachers)
                          .HasForeignKey(e => e.SchoolId)
                            .HasConstraintName("FK_teachers_schools");
            });
            #endregion

            #region COURSE
            modelBuilder.Entity<Course>(entity =>
            {
                entity.ToTable("Course");

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                        .HasColumnName("id");

                entity.Property(e => e.Name)
                        .HasColumnName("name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false);

                entity.Property(e => e.Description)
                        .HasColumnName("description")
                        .HasColumnType("text")
                        .IsRequired()
                        .IsUnicode(false);

                entity.Property(e => e.TeacherId)
                        .HasColumnName("teacherId");

                entity.HasOne(e => e.Teacher)
                        .WithMany(e => e.Courses)
                          .HasForeignKey(e => e.TeacherId)
                            .HasConstraintName("FK_courses_teachers");
            });
            #endregion

            #region STUDENT_COURSE
            modelBuilder.Entity<StudentCourse>(entity =>
            {
                entity.ToTable("StudentCourse");

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                        .HasColumnName("id");

                entity.Property(e => e.CourseId)
                        .HasColumnName("courseId");

                entity.Property(e => e.StudentId)
                        .HasColumnName("studentId");

                entity.HasOne(e => e.Course)
                        .WithMany(e => e.StudentsCourses)
                          .HasForeignKey(e => e.CourseId)
                            .OnDelete(DeleteBehavior.ClientSetNull)
                              .HasConstraintName("FK_students_courses_course");

                entity.HasOne(e => e.Student)
                        .WithMany(e => e.StudentsCourses)
                          .HasForeignKey(e => e.StudentId)
                            .OnDelete(DeleteBehavior.ClientSetNull)
                              .HasConstraintName("FK_students_courses_student");
            });
            #endregion

            #region GRADE
            modelBuilder.Entity<Grade>(entity =>
            {
                entity.ToTable("Grade");

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                        .HasColumnName("id");

                entity.Property(e => e.Qualification)
                        .HasColumnName("qualification");

                entity.Property(e => e.StudentId)
                        .HasColumnName("studentId");
                
                entity.Property(e => e.CourseId)
                        .HasColumnName("courseId");

                entity.HasOne(d => d.Course)
                        .WithMany(p => p.Grades)
                          .HasForeignKey(d => d.CourseId)
                            .OnDelete(DeleteBehavior.ClientSetNull)
                              .HasConstraintName("FK_grades_courses");

                entity.HasOne(d => d.Student)
                        .WithMany(p => p.Grades)
                          .HasForeignKey(d => d.StudentId)
                            .OnDelete(DeleteBehavior.ClientSetNull)
                              .HasConstraintName("FK_grades_students");
            });
            #endregion


            modelBuilder.Ignore<Entity>();

            OnModelCreatingPartial(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }


        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
    
}
