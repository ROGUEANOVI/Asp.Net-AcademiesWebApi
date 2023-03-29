using AcademiesWebApi.DTOs.CourseDTOs;
using AcademiesWebApi.DTOs.GradeDTOs;
using AcademiesWebApi.DTOs.SchoolDTOs;
using AcademiesWebApi.DTOs.StudentCourseDTOs;
using AcademiesWebApi.DTOs.StudentDTOs;
using AcademiesWebApi.DTOs.TeacherDTOs;
using AcademiesWebApi.Entities;
using AutoMapper;

namespace AcademiesWebApi.Configuration
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<School, AddSchoolRequestDTO>().ReverseMap();
            CreateMap<School, EditSchoolRequestDTO>().ReverseMap();

            CreateMap<Teacher, AddTeacherRequestDTO>().ReverseMap();
            CreateMap<Teacher, EditTeacherRequestDTO>().ReverseMap();

            CreateMap<Student, AddStudentRequestDTO>().ReverseMap();
            CreateMap<Student, EditStudentRequestDTO>().ReverseMap();

            CreateMap<Course, AddCourseRequestDTO>().ReverseMap();
            CreateMap<Course, EditCourseRequestDTO>().ReverseMap();

            CreateMap<StudentCourse, AddStudentCourseRequestDTO>().ReverseMap();
            CreateMap<StudentCourse, EditStudentCourseRequestDTO>().ReverseMap();

            CreateMap<Grade, AddGradeRequestDTO>().ReverseMap();
            CreateMap<Grade, EditGradeRequestDTO>().ReverseMap();
        }
    }
}
