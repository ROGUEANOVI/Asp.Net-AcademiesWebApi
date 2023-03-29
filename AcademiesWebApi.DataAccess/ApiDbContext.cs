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

        public virtual DbSet<School> Schools { get; set; } = null!;
        public virtual DbSet<Teacher> Teachers { get; set; } = null!;
        public virtual DbSet<Student> Students { get; set; } = null!;
        public virtual DbSet<StudentCourse> StudentCourses { get; set; } = null!;
        public virtual DbSet<Course> Courses { get; set; } = null!;
        public virtual DbSet<Grade> Grades { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region SCHOOL
            modelBuilder.Entity<School>( entity =>
                {
                    entity.ToTable("School");

                    entity.HasKey(p => p.SchoolID);
                    entity.Property(e => e.SchoolID)
                            .HasColumnName("SchoolID");

                    entity.Property(e => e.Name)
                            .HasColumnName("Name")
                            .IsRequired()
                            .HasMaxLength(100)
                            .IsUnicode(false);

                    entity.Property(e => e.Web)
                            .HasColumnName("Web")
                            .IsRequired()
                            .HasMaxLength(254)
                            .IsUnicode(false);

                    entity.Property(e => e.Email)
                            .HasColumnName("Email")
                            .IsRequired()
                            .HasMaxLength(254)
                            .IsUnicode(false);

                    entity.Property(e => e.Phone)
                            .HasColumnName("Phone")
                            .IsRequired()
                            .HasMaxLength(45)
                            .IsUnicode(false);

                    entity.HasData(
                         new School()
                         {
                             SchoolID = 1,
                             Name = "NSMI",
                             Web = "www.nsmi.com",
                             Email = "nsmi@manaure.edu.co",
                             Phone = "3459697979"
                         },
                         new School()
                         {
                             SchoolID = 2,
                             Name = "SENA CBC",
                             Web = "www.nsmi-cbc.com",
                             Email = "misena@sena.edu.co",
                             Phone = "6704334062"
                         }
                    );
                }
            );

            
            #endregion

            #region STUDENT
            modelBuilder.Entity<Student>(entity =>
            {
                entity.ToTable("Student");

                entity.HasKey(e => e.StudentID);
                entity.Property(e => e.StudentID)
                        .HasColumnName("StudentID");

                entity.Property(e => e.FirstName)
                        .HasColumnName("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false);

                entity.Property(e => e.LastName)
                        .HasColumnName("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false);

                entity.Property(e => e.IdentificationType)
                        .HasColumnName("IdType")
                        .IsRequired()
                        .IsUnicode(false);

                entity.Property(e => e.IdentificationNumber)
                        .HasColumnName("IdNumber")
                        .IsRequired()
                        .HasMaxLength(10)
                        .IsUnicode(false);

                entity.Property(e => e.Email)
                        .HasColumnName("Email")
                        .IsRequired()
                        .HasMaxLength(254)
                        .IsUnicode(false);

                entity.Property(e => e.Phone)
                        .HasColumnName("Phone")
                        .IsRequired()
                        .HasMaxLength(45)
                        .IsUnicode(false);

                entity.Property(e => e.SchoolID)
                        .HasColumnName("SchoolID");

                entity.HasOne(e => e.School)
                        .WithMany(e => e.Students)
                          .HasForeignKey(e => e.SchoolID)
                            .HasConstraintName("FK_students_schools");
                
            });
            #endregion

            #region TEACHER
            modelBuilder.Entity<Teacher>(entity =>
            {
                entity.ToTable("Teacher");

                entity.HasKey(e => e.TeacherID);
                entity.Property(e => e.TeacherID)
                        .HasColumnName("TeacherID");

                entity.Property(e => e.FirstName)
                        .HasColumnName("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false);

                entity.Property(e => e.LastName)
                        .HasColumnName("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false);

                entity.Property(e => e.IdentificationNumber)
                        .HasColumnName("IdNumber")
                        .IsRequired()
                        .HasMaxLength(10)
                        .IsUnicode(false);

                entity.Property(e => e.Email)
                        .HasColumnName("Email")
                        .IsRequired()
                        .HasMaxLength(254)
                        .IsUnicode(false);

                entity.Property(e => e.Phone)
                        .HasColumnName("Phone")
                        .IsRequired()
                        .HasMaxLength(45)
                        .IsUnicode(false);

                entity.Property(e => e.SchoolID)
                        .HasColumnName("schoolID");

                entity.HasOne(e => e.School)
                        .WithMany(e => e.Teachers)
                          .HasForeignKey(e => e.SchoolID)
                            .HasConstraintName("FK_teachers_schools");
            });
            #endregion

            #region COURSE
            modelBuilder.Entity<Course>(entity =>
            {
                entity.ToTable("Course");

                entity.HasKey(e => e.CourseID);
                entity.Property(e => e.CourseID)
                        .HasColumnName("CourseID");

                entity.Property(e => e.Name)
                        .HasColumnName("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false);

                entity.Property(e => e.Description)
                        .HasColumnName("Description")
                        .HasColumnType("text")
                        .IsRequired()
                        .IsUnicode(false);

                entity.Property(e => e.TeacherID)
                        .HasColumnName("TeacherID");

                entity.HasOne(e => e.Teacher)
                        .WithMany(e => e.Courses)
                          .HasForeignKey(e => e.TeacherID)
                            .HasConstraintName("FK_courses_teachers");
            });
            #endregion

            #region STUDENT_COURSE
            modelBuilder.Entity<StudentCourse>(entity =>
            {
                entity.ToTable("StudentCourse");

                entity.HasKey(e => e.StudentCourseID);
                entity.Property(e => e.StudentCourseID)
                        .HasColumnName("StudentCourseID");

                entity.Property(e => e.CourseID)
                        .HasColumnName("CourseID");

                entity.Property(e => e.StudentID)
                        .HasColumnName("StudentID");

                entity.HasOne(e => e.Course)
                        .WithMany(e => e.StudentsCourses)
                          .HasForeignKey(e => e.CourseID)
                            .OnDelete(DeleteBehavior.ClientSetNull)
                              .HasConstraintName("FK_students_courses_course");

                entity.HasOne(e => e.Student)
                        .WithMany(e => e.StudentsCourses)
                          .HasForeignKey(e => e.StudentID)
                            .OnDelete(DeleteBehavior.ClientSetNull)
                              .HasConstraintName("FK_students_courses_student");
            });
            #endregion

            #region GRADE
            modelBuilder.Entity<Grade>(entity =>
            {
                entity.ToTable("Grade");

                entity.HasKey(e => e.GradeID);
                entity.Property(e => e.GradeID)
                        .HasColumnName("GradeID");

                entity.Property(e => e.Qualification)
                        .HasColumnName("Qualification");

                entity.Property(e => e.StudentID)
                        .HasColumnName("StudentID");
                
                entity.Property(e => e.CourseID)
                        .HasColumnName("CourseID");

                entity.HasOne(d => d.Course)
                        .WithMany(p => p.Grades)
                          .HasForeignKey(d => d.CourseID)
                            .OnDelete(DeleteBehavior.ClientSetNull)
                              .HasConstraintName("FK_grades_courses");

                entity.HasOne(d => d.Student)
                        .WithMany(p => p.Grades)
                          .HasForeignKey(d => d.StudentID)
                            .OnDelete(DeleteBehavior.ClientSetNull)
                              .HasConstraintName("FK_grades_students");
            });
            #endregion


            //modelBuilder.Ignore<Entity<>>();

            OnModelCreatingPartial(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }


        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
    
}
