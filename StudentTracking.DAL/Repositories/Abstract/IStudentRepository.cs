﻿using StudentTracking.CORE.Entities;
using StudentTracking.VM.Student;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentTracking.DAL.Repositories.Abstract
{
    public interface IStudentRepository
    {
        bool Add(Student VM);
        bool Update(Student VM);
        bool HardDelete(int ID);
        bool SoftDelete(int ID);
        Student GetByID(int ID);
        IEnumerable<Student> GetAll();
        IEnumerable<StudentSelectVM> GetStudentsByClass(int ID);
        IEnumerable<Student> GetStudentsByProject(int ID);
        IEnumerable<Student> GetAllJustFullName();
        IEnumerable<Student> GetStudentWithDetails(int classID);
        IEnumerable<Student> GetStudentWithDetailsReport(int classID, int week);
        IEnumerable<Student> GetStudentsWithQuestionsAndExamsByClass(int classID);
        Student GetStudentWithDetailsByID(int studentID);
    }
}
