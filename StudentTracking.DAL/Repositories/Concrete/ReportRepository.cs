using Dapper;
using StudentTracking.CORE.Entities;
using StudentTracking.DAL.Repositories.Abstract;
using StudentTracking.VM.Report;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace StudentTracking.DAL.Repositories.Concrete
{
    public class ReportRepository : BaseRepository, IReportRepository
    {
        //base repository yi IDBconnection, Itransaction değişkenlerini burada tekrar yazmamak için kullandik
        public ReportRepository(IDbConnection connection, IDbTransaction transaction) : base(connection, transaction)
        {
        }

        public bool Add(Report entity)
        {
            var sqlQuery = "INSERT INTO Report (StudentID, Description, Score, Date, WeekOfYear) VALUES (@StudentID, @Description, @Score, @Date, @WeekOfYear)";

            var parameters = new DynamicParameters();

            parameters.Add("@StudentID", entity.StudentID, DbType.Int32);
            parameters.Add("@Description", entity.Description, DbType.String);
            parameters.Add("@Score", entity.Score, DbType.Int32);
            parameters.Add("@Date", entity.Date, DbType.DateTime);
            parameters.Add("@WeekOfYear", entity.WeekOfYear, DbType.Int32);

            var rowsAffected = Connection.Execute(sqlQuery, parameters, Transaction);

            return rowsAffected > 0;
        }

        public IEnumerable<Report> GetAll()
        {
            var sqlQuery = "SELECT * FROM Report";

            var parameters = new DynamicParameters();

            return Connection.Query<Report>(sqlQuery, parameters, Transaction);
        }

        public Report GetByID(int ID)
        {
            var sqlQuery = "SELECT * FROM Report WHERE ID = @ID";

            var parameters = new DynamicParameters();

            parameters.Add("@ID", ID, DbType.Int32);

            return Connection.QuerySingleOrDefault<Report>(sqlQuery, parameters, Transaction);
        }

        public bool HardDelete(int ID)
        {
            var sqlQuery = "DELETE FROM Report WHERE ID = @ID";

            var parameters = new DynamicParameters();

            parameters.Add("@ID", ID, DbType.Int32);

            var rowsAffected = Connection.Execute(sqlQuery, parameters, Transaction);

            return rowsAffected > 0;
        }

        public bool SoftDelete(int ID)
        {
            var sqlQuery = "UPDATE Report SET isActive = 0 WHERE ID = @ID";

            var parameters = new DynamicParameters();

            parameters.Add("@ID", ID, DbType.Int32);

            var rowsAffected = Connection.Execute(sqlQuery, parameters, Transaction);

            return rowsAffected > 0;
        }

        public bool Update(Report VM)
        {
            var sqlQuery = "UPDATE Report SET StudentID=@StudentID, Description=@Description, Score=@Score, Date=@Date, WeekOfYear=@WeekOfYear WHERE ID = @ID";

            var parameters = new DynamicParameters();

            parameters.Add("@ID", VM.ID, DbType.Int32);
            parameters.Add("@StudentID", VM.StudentID, DbType.Int32);
            parameters.Add("@Description", VM.Description, DbType.String);
            parameters.Add("@Score", VM.Score, DbType.Int32);
            parameters.Add("@Date", VM.Date, DbType.DateTime);
            parameters.Add("@WeekOfYear", VM.WeekOfYear, DbType.Int32);

            var rowsAffected = Connection.Execute(sqlQuery, parameters, Transaction);

            return rowsAffected > 0;
        }


        public IEnumerable<Report> GetAllWithStudentID()
        {
            var sqlQuery = @"
                    SELECT r.ID as Id,r.Description,r.Score,r.Date,r.WeekOfYear,r.isActive,
                           s.ID as Id,s.FirstName,s.LastName
                    FROM Report r 
                    JOIN Student s ON r.StudentID = s.ID";

            var parameters = new DynamicParameters();

            var result = Connection.Query<Report, Student, Report>(
                sqlQuery,
                (report, student) =>
                {
                    report.Student = student;

                    return report;
                },
                parameters,
                //splitOn: "Id", default value zaten
                transaction: Transaction
            );

            return result;
        }

        public (DateTime, DateTime) GetWeekStartEndDate(DateTime date)
        {
            var dayOfWeek = (int)date.DayOfWeek;
            var startDate = date.Date.AddDays(-dayOfWeek + 1); // Pazartesi
            var endDate = startDate.AddDays(5); // Cuma

            return (startDate, endDate);
        }

        public IEnumerable<Student> GetReportWithDetails(int classID)
        {
            var sqlQuery = @"
                    SELECT r.ID as Id, r.StudentID, r.Description, r.Score, r.Date, r.WeekOfYear, r.isActive,
                           s.ID as Id, s.FirstName, s.LastName, s.Email, s.StatusID, s.ClassID, s.isActive,
                           q.ID AS Id, q.QuestionName, q.Description, q.Date, q.isActive,
                           se.ExamID as ExamId, se.Score, se.Description, se.isActive,
                           e.ID AS ExamIdS, e.Name, e.Body, e.Date, e.isActive, e.StatusID
                    FROM Report r
                     LEFT JOIN Student s ON s.ID = r.StudentID
                     LEFT JOIN Question q ON s.ID = q.StudentID
                     LEFT JOIN StudentExam se ON s.ID = se.StudentID
                     LEFT JOIN Exam e ON e.ID = se.ExamID
                    WHERE s.ClassID = @ClassID";

            var Date = GetWeekStartEndDate(DateTime.Now);
            var parameters = new DynamicParameters();
            parameters.Add("@ClassID", classID, DbType.Int32);

            var students = new Dictionary<int, Student>();

            var result = Connection.Query<Student, Question, StudentExam, Exam, Student>(
                sqlQuery,
                (student, question, studentExam, exam) =>
                {
                    if (!students.TryGetValue(student.ID, out Student studentEntry))
                    {
                        studentEntry = student;
                        studentEntry.Question = new List<Question>();
                        studentEntry.StudentExam = new List<StudentExam>();
                        students.Add(studentEntry.ID, studentEntry);
                    }

                    if (question != null && question.Date >= Date.Item1 && question.Date <= Date.Item2
                          && !studentEntry.Question.Any(q => q.ID == question.ID))
                    {
                        studentEntry.Question.Add(question);
                    }

                    if (studentExam != null && !studentEntry.StudentExam.Any(se => se.ExamID == studentExam.ExamID))
                    {
                        studentExam.Exam = exam;
                        studentEntry.StudentExam.Add(studentExam);
                    }

                    return studentEntry;
                },
                parameters,
                  splitOn: "Id,ExamId,ExamIdS"


            );

            return students.Values.ToList();
        }
    }
}