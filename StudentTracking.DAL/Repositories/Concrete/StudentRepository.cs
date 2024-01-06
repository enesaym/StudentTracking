using Dapper;
using StudentTracking.CORE.Entities;
using StudentTracking.DAL.Repositories.Abstract;
using StudentTracking.VM.Student;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace StudentTracking.DAL.Repositories.Concrete
{
    public class StudentRepository : BaseRepository, IStudentRepository
    {
        public StudentRepository(IDbConnection connection, IDbTransaction transaction) : base(connection, transaction)
        {

        }

        public bool Add(Student VM)
        {
            var sqlQuery = "INSERT INTO Student (FirstName, LastName, Email, StatusID, ClassID) VALUES (@FirstName, @LastName, @Email, @StatusID, @ClassID)";

            var parameters = new DynamicParameters();

            parameters.Add("@FirstName", VM.FirstName, DbType.String);
            parameters.Add("@LastName", VM.LastName, DbType.String);
            parameters.Add("@Email", VM.Email, DbType.String);
            parameters.Add("@StatusID", VM.StatusID, DbType.Int32);
            parameters.Add("@ClassID", VM.ClassID, DbType.Int32);

            var rowsAffected = Connection.Execute(sqlQuery, parameters, Transaction);

            return rowsAffected > 0;
        }

        public bool Update(Student VM)
        {
            var sqlQuery = "UPDATE Student SET FirstName=@FirstName, LastName=@LastName, Email = @Email, StatusID = @StatusID, ClassID = @ClassID WHERE ID = @ID";

            var parameters = new DynamicParameters();

            parameters.Add("@ID", VM.ID, DbType.String);
            parameters.Add("@FirstName", VM.FirstName, DbType.String);
            parameters.Add("@LastName", VM.LastName, DbType.String);
            parameters.Add("@Email", VM.Email, DbType.String);
            parameters.Add("@StatusID", VM.StatusID, DbType.Int32);
            parameters.Add("@ClassID", VM.ClassID, DbType.Int32);

            var rowsAffected = Connection.Execute(sqlQuery, parameters, Transaction);

            return rowsAffected > 0;
        }

        public bool HardDelete(int ID)
        {
            var sqlQuery = "DELETE FROM Student WHERE ID = @ID";

            var parameters = new DynamicParameters();

            parameters.Add("@ID", ID, DbType.Int32);

            var rowsAffected = Connection.Execute(sqlQuery, parameters, Transaction);

            return rowsAffected > 0;
        }

        public bool SoftDelete(int ID)
        {
            var sqlQuery = "UPDATE Student SET isActive=0 WHERE ID = @ID";

            var parameters = new DynamicParameters();

            parameters.Add("@ID", ID, DbType.Int32);

            var rowsAffected = Connection.Execute(sqlQuery, parameters, Transaction);

            return rowsAffected > 0;
        }

        public Student GetByID(int ID)
        {
            var sqlQuery = "SELECT * FROM Student WHERE ID = @ID";

            var parameters = new DynamicParameters();

            parameters.Add("@ID", ID, DbType.Int32);

            return Connection.QuerySingleOrDefault<Student>(sqlQuery, parameters, Transaction);
        }

        public IEnumerable<Student> GetAll()
        {
            var sqlQuery = "SELECT * FROM Student";

            var parameters = new DynamicParameters();

            return Connection.Query<Student>(sqlQuery, parameters, Transaction);
        }


        /// <summary>
        /// Sınıf ID sine göre eğitime devam eden öğrencileri getirir. (Status 101: Eğitime devam ediyor)
        /// </summary>
        /// <param name="classID">Sınıf ID</param>
        /// <returns>Öğrenci Listesi</returns>
        public IEnumerable<StudentSelectVM> GetStudentsByClass(int classID)
        {
            var sqlQuery = "SELECT * FROM Student WHERE ClassID = @ClassID and StatusID = 101";

            var parameters = new DynamicParameters();

            parameters.Add("@ClassID", classID, DbType.Int32);

            return Connection.Query<StudentSelectVM>(sqlQuery, parameters, Transaction);
        }

        public IEnumerable<Student> GetStudentsWithQuestionsAndExamsByClass(int classID)
        {
            var sqlQuery = @"
                    SELECT s.ID as Id, s.FirstName, s.LastName, s.Email, s.StatusID, s.ClassID, s.isActive,
                           q.ID AS Id, q.QuestionName, q.Description, q.Date, q.isActive,
                           e.ExamID as ExamId, e.Score, e.Description, e.isActive
                    FROM Student s
                     JOIN Question q ON s.ID = q.StudentID
                     JOIN StudentExam e ON s.ID = e.StudentID
                    WHERE s.ClassID = @ClassID";

            var parameters = new DynamicParameters();
            parameters.Add("@ClassID", classID, DbType.Int32);

            var result = Connection.Query<Student, Question, StudentExam, Student>(
                sqlQuery,
                (student, question, exam) =>
                {
                    student.Question = student.Question ?? new List<Question>();
                    if (question != null)
                        student.Question.Add(question);

                    student.StudentExam = student.StudentExam ?? new List<StudentExam>();
                    if (exam != null)
                        student.StudentExam.Add(exam);

                    return student;
                },
                parameters,
                splitOn: "Id, ExamId",
                transaction: Transaction
            );

            // VERİLER TEKRAR EDİYOR BURAYI ÇÖZ

            var groupedResults = result.GroupBy(s => s.ID).Select(group =>
            {
                var groupedStudent = group.First(); 
                groupedStudent.Question = group.SelectMany(s => s.Question).Distinct().ToList();
                groupedStudent.StudentExam = group.SelectMany(s => s.StudentExam).Distinct().ToList();
                return groupedStudent;
            });


            return groupedResults;
        }

        public (DateTime, DateTime) GetWeekStartEndDate(DateTime date)
        {
            var dayOfWeek = (int)date.DayOfWeek;
            var startDate = date.Date.AddDays(-dayOfWeek + 1); // Pazartesi
            var endDate = startDate.AddDays(6); // Cuma

            return (startDate, endDate);
        }

        public (DateTime, DateTime) GetWeekStartEndDateByClass(DateTime classStartDate, int week)
        {
            //sınıfın baslangic tarihinden itibaren verilen haftanın pazartesi cumasını verir
            var date = GetWeekStartEndDate(classStartDate.AddDays(7 * (week - 1)));
            return (date.Item1, date.Item2);
        }

        public IEnumerable<Student> GetStudentWithDetails(int classID)
        {
            var sqlQuery = @"
                    SELECT s.ID as Id, s.FirstName, s.LastName, s.Email, s.StatusID, s.ClassID, s.isActive,
                           q.ID AS Id, q.QuestionName, q.Description, q.Date, q.isActive,
                           se.ExamID as ExamId, se.Score, se.Description, se.isActive,
                           e.ID AS ExamIdS, e.Name, e.Body, e.Date, e.isActive, e.StatusID
                    FROM Student s
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
						if (studentExam.Exam.Date >= Date.Item1 && studentExam.Exam.Date <= Date.Item2)
						{
                            studentEntry.StudentExam.Add(studentExam);
                        }
                        
                    }

                    return studentEntry;
                },
                parameters,
                  splitOn: "Id,ExamId,ExamIdS"


            );

            return students.Values.ToList();
        }

        public IEnumerable<Student> GetStudentWithDetailsReport(int classID, int week)
        {
            var sqlQuery = @"
                    SELECT s.ID as Id, s.FirstName, s.LastName, s.Email, s.StatusID, s.ClassID, s.isActive,
                           q.ID AS Id, q.QuestionName, q.Description, q.Date, q.isActive,
                           se.ExamID as ExamId, se.Score, se.Description, se.isActive,
                           e.ID AS ExamIdS, e.Name, e.Body, e.Date, e.isActive, e.StatusID,
                           r.ID AS ReportId, r.StudentID, r.Description, r.Score, r.Date, r.WeekOfYear, r.isActive,
                           c.ID AS ClassIdd, c.StartedDate
                    FROM Student s
                     LEFT JOIN Question q ON s.ID = q.StudentID
                     LEFT JOIN StudentExam se ON s.ID = se.StudentID
                     LEFT JOIN Exam e ON e.ID = se.ExamID
                     LEFT JOIN Report r ON r.StudentID = s.ID
                     LEFT JOIN Class c ON s.ClassID = c.ID
                    WHERE s.ClassID = @ClassID";

            var parameters = new DynamicParameters();
            parameters.Add("@ClassID", classID, DbType.Int32);

            var students = new Dictionary<int, Student>();

            var result = Connection.Query<Student, Question, StudentExam, Exam, Report,Class, Student>(
                sqlQuery,
                (student, question, studentExam, exam, report,Class) =>
                {
                    var Date = GetWeekStartEndDateByClass(Class.StartedDate.Value, week);
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

                    if (studentExam != null && report.Date >= Date.Item1 && report.Date <= Date.Item2 && !studentEntry.StudentExam.Any(se => se.ExamID == studentExam.ExamID))
                    {
                        studentExam.Exam = exam;
                        studentEntry.StudentExam.Add(studentExam);
                    }

                    if(report !=null && report.Date >= Date.Item1 && report.Date <= Date.Item2 && !studentEntry.Report.Any(r=> r.ID == report.ID))
                    {
                        studentEntry.Report.Add(report);
                    }

                    return studentEntry;
                },
                parameters,
                  splitOn: "Id,ExamId,ExamIdS,ReportId,ClassIdd"
            );

            return students.Values.ToList();
        }

        public Student GetStudentWithDetailsByID(int studentID)
        {
            var sqlQuery = @"
                    SELECT s.ID as Id, s.FirstName, s.LastName, s.Email, s.StatusID, s.ClassID, s.isActive,
                           q.ID AS Id, q.QuestionName, q.Description, q.Date, q.isActive,
                           se.ExamID as SExamId, se.Score, se.Description, se.isActive,
                           e.ID AS ExamId, e.Name, e.Body, e.Date, e.isActive, e.StatusID
                    FROM Student s
                     LEFT JOIN Question q ON s.ID = q.StudentID
                     LEFT JOIN StudentExam se ON s.ID = se.StudentID
                     LEFT JOIN Exam e ON e.ID = se.ExamID
                    WHERE s.ID = @StudentID";

            var Date = GetWeekStartEndDate(DateTime.Now);
            var parameters = new DynamicParameters();
            parameters.Add("@StudentID", studentID, DbType.Int32);

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
                        if (studentExam.Exam.Date >= Date.Item1 && studentExam.Exam.Date <= Date.Item2)
                        {
                            studentEntry.StudentExam.Add(studentExam);
                        }
                       
                   
                    }

                    return studentEntry;
                },
                parameters,
                  splitOn: "Id,SExamId,ExamId"


            ).FirstOrDefault();

            return students.Values.ToList().FirstOrDefault();
        }

        /// <summary>
        /// Project ID sine göre öğrencileri getirir.
        /// </summary>
        /// <param name="projectID">Proje ID</param>
        /// <returns>Öğrenci Listesi</returns>
        public IEnumerable<Student> GetStudentsByProject(int projectID)
        {
            var sqlQuery = "select s.ID,s.FirstName, s.LastName from Student s join StudentProject sp on sp.StudentID=s.ID where sp.ProjectID=@ProjectID";

            var parameters = new DynamicParameters();
            parameters.Add("@ProjectID", projectID, DbType.Int32);

            return Connection.Query<Student>(sqlQuery, parameters, transaction: Transaction);
        }

        public IEnumerable<Student> GetStudentsByProjectWithDetails(int projectID)
        {
            var sqlQuery = @"
        SELECT s.ID, s.FirstName, s.LastName, sp.ProjectID,sp.Score, sp.Description
        FROM Student s 
        JOIN StudentProject sp ON sp.StudentID = s.ID 
        WHERE sp.ProjectID = @ProjectID;
    ";

            var parameters = new DynamicParameters();
            parameters.Add("@ProjectID", projectID, DbType.Int32);

            var studentDictionary = new Dictionary<int, Student>();

            Connection.Query<Student, StudentProject, Student>(
                sqlQuery,
                (student, studentProject) =>
                {
                    if (!studentDictionary.TryGetValue(student.ID, out var studentEntry))
                    {
                        studentEntry = student;
                        studentEntry.StudentProject = new List<StudentProject>();
                        studentDictionary.Add(studentEntry.ID, studentEntry);
                    }
                    studentEntry.StudentProject.Add(studentProject);
                    return studentEntry;
                },
                parameters,
                splitOn: "ProjectID",
                transaction: Transaction
            );

            return studentDictionary.Values;
        }


        public IEnumerable<Student> GetAllJustFullName()
        {
            var sqlQuery = "select s.ID,FirstName,LastName from Student s";

            return Connection.Query<Student>(sqlQuery, transaction:
                Transaction);
        }


    }
}
