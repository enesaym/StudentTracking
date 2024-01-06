using StudentTracking.BLL.Common;
using StudentTracking.BLL.Mapper;
using StudentTracking.CORE.Entities;
using StudentTracking.DAL.UnitOfWork;
using StudentTracking.VM.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper.QueryableExtensions;

namespace StudentTracking.BLL.Manager
{
    public class StudentManager
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly MyMapper _mapper;

        public StudentManager(IUnitOfWork unitOfWork, MyMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public Result<StudentInsertVM> Add(StudentInsertVM VM)
        {
            try
            {
                var entity = _mapper.Map<StudentInsertVM, Student>(VM);
                bool state = _unitOfWork.StudentRepository.Add(entity);

                return new Result<StudentInsertVM> { Success = true, Data = VM };
            }
            catch (Exception ex)
            {
                // exception loglama
                return new Result<StudentInsertVM> { Success = false, Data = VM, Message = "Bir hata oluştu. Detaylar için logları kontrol edin." };
            }
        }

        public Result<StudentUpdateVM> Update(StudentUpdateVM VM)
        {
            try
            {
                var entity = _mapper.Map<StudentUpdateVM, Student>(VM);
                bool state = _unitOfWork.StudentRepository.Update(entity);

                return new Result<StudentUpdateVM> { Success = true, Data = VM };
            }
            catch (Exception ex)
            {
                // exception loglama
                return new Result<StudentUpdateVM> { Success = false, Data = VM, Message = "Bir hata oluştu. Detaylar için logları kontrol edin." };
            }
        }

        public Result<int> HardDelete(int ID)
        {
            try
            {
                bool state = _unitOfWork.StudentRepository.HardDelete(ID);

                return new Result<int> { Success = true, Data = ID };
            }
            catch (Exception ex)
            {
                // exception loglama
                return new Result<int> { Success = false, Data = ID, Message = "Bir hata oluştu. Detaylar için logları kontrol edin." };
            }
        }

        public Result<int> SoftDelete(int ID)
        {
            try
            {
                bool state = _unitOfWork.StudentRepository.SoftDelete(ID);

                return new Result<int> { Success = true, Data = ID };
            }
            catch (Exception ex)
            {
                // exception loglama
                return new Result<int> { Success = true, Data = ID, Message = "Bir hata oluştu. Detaylar için logları kontrol edin." };
            }
        }

        public Result<StudentSelectVM> GetByID(int ID)
        {
            StudentSelectVM VM = null;
            try
            {
                Student entity = _unitOfWork.StudentRepository.GetByID(ID);
                VM = _mapper.Map<Student, StudentSelectVM>(entity);

                return new Result<StudentSelectVM> { Success = true, Data = VM };
            }
            catch (Exception ex)
            {
                // exception loglama
                return new Result<StudentSelectVM> { Success = false, Data = VM, Message = "Bir hata oluştu. Detaylar için logları kontrol edin." };
            }
        }

        public Result<List<StudentSelectVM>> GetAll()
        {
            List<StudentSelectVM> VM = new List<StudentSelectVM>();
            try
            {
                List<Student> entity = _unitOfWork.StudentRepository.GetAll().ToList();

                entity.ForEach(x =>
                {
                    VM.Add(_mapper.Map<Student, StudentSelectVM>(x));
                });

                return new Result<List<StudentSelectVM>> { Success = true, Data = VM };
            }
            catch (Exception ex)
            {
                // exception loglama
                return new Result<List<StudentSelectVM>> { Success = false, Data = VM, Message = "Bir hata oluştu. Detaylar için logları kontrol edin." };
            }
        }

        public Result<List<StudentFullNameVM>> GetAllStudentByProject(int projectId)
        {
            List<StudentFullNameVM> VM = new List<StudentFullNameVM>();
            try
            {
                List<Student> entity = _unitOfWork.StudentRepository.GetStudentsByProject(projectId).ToList();

                entity.ForEach(x =>
                {
                    VM.Add(_mapper.Map<Student, StudentFullNameVM>(x));
                });

                return new Result<List<StudentFullNameVM>> { Success = true, Data = VM };
            }
            catch (Exception ex)
            {
                // exception loglama
                return new Result<List<StudentFullNameVM>> { Success = false, Data = VM, Message = "Bir hata oluştu. Detaylar için logları kontrol edin." };
            }
        }
        public Result<List<StudentSelectVM>> GetAllStudentByProjectWithDetails(int projectId)
        {
            List<StudentSelectVM> VM = new List<StudentSelectVM>();
            try
            {
                List<Student> entity = _unitOfWork.StudentRepository.GetStudentsByProjectWithDetails(projectId).ToList();

                entity.ForEach(x =>
                {
                    VM.Add(_mapper.Map<Student, StudentSelectVM>(x));
                });

                return new Result<List<StudentSelectVM>> { Success = true, Data = VM };
            }
            catch (Exception ex)
            {
                // exception loglama
                return new Result<List<StudentSelectVM>> { Success = false, Data = VM, Message = "Bir hata oluştu. Detaylar için logları kontrol edin." };
            }
        }

        public IEnumerable<StudentSelectVM> GetStudentsByClass(int classId)
        {
            var vm = _unitOfWork.StudentRepository.GetStudentsByClass(classId);

            return vm;
        }

        public Result<List<StudentSelectVM>> GetStudentsWithQuestionsAndExamsByClass(int classID)
        {
            List<StudentSelectVM> VM = new List<StudentSelectVM>();
            try
            {
                List<Student> entity = _unitOfWork.StudentRepository.GetStudentsWithQuestionsAndExamsByClass(classID).ToList();

                VM = entity.Select(x => _mapper.Map<Student, StudentSelectVM>(x)).ToList();

                return new Result<List<StudentSelectVM>> { Success = true, Data = VM };
            }
            catch (Exception ex)
            {
                // exception loglama
                return new Result<List<StudentSelectVM>> { Success = false, Data = VM, Message = "Bir hata oluştu. Detaylar için logları kontrol edin." };
            }

        }

        public Result<List<StudentSelectVM>> GetStudentWithDetails(int classID)
        {
            List<StudentSelectVM> VM = new List<StudentSelectVM>();
            try
            {
                List<Student> entity = _unitOfWork.StudentRepository.GetStudentWithDetails(classID).ToList();

                VM = entity.Select(x => _mapper.Map<Student, StudentSelectVM>(x)).ToList();

                return new Result<List<StudentSelectVM>> { Success = true, Data = VM };
            }
            catch (Exception ex)
            {
                // exception loglama
                return new Result<List<StudentSelectVM>> { Success = false, Data = VM, Message = "Bir hata oluştu. Detaylar için logları kontrol edin." };
            }
        }

        public Result<List<StudentSelectVM>> GetStudentWithDetailsReport(int classID, int week)
        {
            List<StudentSelectVM> VM = new List<StudentSelectVM>();
            try
            {
                List<Student> entity = _unitOfWork.StudentRepository.GetStudentWithDetailsReport(classID, week).ToList();

                VM = entity.Select(x => _mapper.Map<Student, StudentSelectVM>(x)).ToList();

                return new Result<List<StudentSelectVM>> { Success = true, Data = VM };
            }
            catch (Exception ex)
            {
                // exception loglama
                return new Result<List<StudentSelectVM>> { Success = false, Data = VM, Message = "Bir hata oluştu. Detaylar için logları kontrol edin." };
            }
        }

        public Result<StudentSelectVM> GetStudentWithDetailsByID(int studentID)
        {
            StudentSelectVM VM = new StudentSelectVM();
            try
            {
                Student entity = _unitOfWork.StudentRepository.GetStudentWithDetailsByID(studentID);

                VM = _mapper.Map<Student, StudentSelectVM>(entity);

                return new Result<StudentSelectVM> { Success = true, Data = VM };
            }
            catch (Exception ex)
            {
                // exception loglama
                return new Result<StudentSelectVM> { Success = false, Data = VM, Message = "Bir hata oluştu. Detaylar için logları kontrol edin." };
            }
        }

        public Result<List<StudentFullNameVM>> GetAllJustFullName()
        {
            List<StudentFullNameVM> VM = new List<StudentFullNameVM>();
            try
            {
                List<Student> entity = _unitOfWork.StudentRepository.GetAllJustFullName().ToList();

                entity.ForEach(x =>
                {
                    VM.Add(new StudentFullNameVM { ID = x.ID, FullName = x.FirstName + " " + x.LastName });
                });

                return new Result<List<StudentFullNameVM>> { Success = true, Data = VM };
            }
            catch (Exception ex)
            {
                // exception loglama
                return new Result<List<StudentFullNameVM>> { Success = false, Data = VM, Message = "Bir hata oluştu. Detaylar için logları kontrol edin." };
            }
        }
        public Result<List<StudentSelectVM>> GetStudentExamsByClassId(int classId)
        {
            List<StudentSelectVM> VM = new List<StudentSelectVM>();
            try
            {
                List<Student> entity = _unitOfWork.StudentRepository.GetStudentExamsByClassId(classId).ToList();

                VM = _mapper.Map<List<Student>, List<StudentSelectVM>>(entity);

                return new Result<List<StudentSelectVM>> { Success = true, Data = VM };
            }
            catch (Exception ex)
            {
                // exception loglama
                return new Result<List<StudentSelectVM>> { Success = false, Data = VM, Message = "Bir hata oluştu. Detaylar için logları kontrol edin." };
            }
        }

        
    }
}
