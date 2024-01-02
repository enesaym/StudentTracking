using AutoMapper;
using StudentTracking.CORE.Entities;
using StudentTracking.VM.Class;
using StudentTracking.VM.Exam;
using StudentTracking.VM.Project;
using StudentTracking.VM.Question;
using StudentTracking.VM.Report;
using StudentTracking.VM.Status;
using StudentTracking.VM.Student;
using StudentTracking.VM.StudentExam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StudentTracking.BLL.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Student, StudentSelectVM>().ReverseMap();
            CreateMap<Question, QuestionSelectVM>().ReverseMap();
            CreateMap<StudentExam, StudentExamSelectVM>().ReverseMap();
            CreateMap<Exam, ExamSelectVM>().ReverseMap();
            CreateMap<Status, StatusSelectVM>().ReverseMap();
            CreateMap<Report, ReportSelectVM>().ReverseMap();
            CreateMap<Project, ProjectSelectVM>().ReverseMap();
            CreateMap<Class, ClassSelectVM>().ReverseMap();

            CreateMap<Student, StudentUpdateVM>().ReverseMap();
            CreateMap<Question, QuestionUpdateVM>().ReverseMap();
            //CreateMap<StudentExam, StudentExamUpdateVM>().ReverseMap();
            CreateMap<Exam, ExamUpdateVM>().ReverseMap();
            CreateMap<Status, StatusUpdateVM>().ReverseMap();
            CreateMap<Report, ReportUpdateVM>().ReverseMap();
            CreateMap<Project, ProjectUpdateVM>().ReverseMap();
            CreateMap<Class, ClassUpdateVM>().ReverseMap();

            CreateMap<Student, StudentInsertVM>().ReverseMap();
            CreateMap<Question, QuestionInsertVM>().ReverseMap();
            //CreateMap<StudentExam, StudentExamInsertVM>().ReverseMap();
            CreateMap<Exam, ExamInsertVM>().ReverseMap();
            CreateMap<Status, StatusInsertVM>().ReverseMap();
            CreateMap<Report, ReportInsertVM>().ReverseMap();
            CreateMap<Project, ProjectInsertVM>().ReverseMap();
            CreateMap<Class, ClassInsertVM>().ReverseMap();


        }
    }
}
