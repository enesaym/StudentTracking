using StudentTracking.CORE.Entities;
using StudentTracking.VM.Exam;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentTracking.DAL.Repositories.Abstract
{
    public interface IExamRepository
    {
        bool Add(Exam VM);
        bool Update(Exam VM);
        bool HardDelete(int ID);
        bool SoftDelete(int ID);
        bool AddExamGrades(StudentExam entity);
        Exam GetByID(int ID);
        ICollection<Exam> GetAllWithStatus();
        IEnumerable<Exam> GetAll();
        IEnumerable<Exam> GetExamsByClassId(int classId);
        bool UpdateExamGrades(StudentExam entity);
    }
}
