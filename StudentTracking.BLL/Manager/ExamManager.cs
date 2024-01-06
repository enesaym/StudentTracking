using StudentTracking.BLL.Common;
using StudentTracking.BLL.Mapper;
using StudentTracking.CORE.Entities;
using StudentTracking.DAL.UnitOfWork;
using StudentTracking.VM.Exam;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using StudentTracking.VM.StudentExam;
using static Dapper.SqlMapper;

namespace StudentTracking.BLL.Manager
{
    public class ExamManager
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly MyMapper _mapper;

        public ExamManager(IUnitOfWork unitOfWork, MyMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public Result<ExamInsertVM> Add(ExamInsertVM VM)
        {
            try
            {
                var entity = _mapper.Map<ExamInsertVM, Exam>(VM);
                bool state = _unitOfWork.ExamRepository.Add(entity);

                return new Result<ExamInsertVM> { Success = true, Data = VM };
            }
            catch (Exception ex)
            {
                // exception loglama
                return new Result<ExamInsertVM> { Success = false, Data = VM, Message = "Bir hata oluştu. Detaylar için logları kontrol edin." };
            }
        }

        public Result<ExamUpdateVM> Update(ExamUpdateVM VM)
        {
            try
            {
                var entity = _mapper.Map<ExamUpdateVM, Exam>(VM);
                bool state = _unitOfWork.ExamRepository.Update(entity);

                return new Result<ExamUpdateVM> { Success = true, Data = VM };
            }
            catch (Exception ex)
            {
                // exception loglama
                return new Result<ExamUpdateVM> { Success = false, Data = VM, Message = "Bir hata oluştu. Detaylar için logları kontrol edin." };
            }
        }

        public Result<int> HardDelete(int ID)
        {
            try
            {
                bool state = _unitOfWork.ExamRepository.HardDelete(ID);

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
                bool state = _unitOfWork.ExamRepository.SoftDelete(ID);

                return new Result<int> { Success = true, Data = ID };
            }
            catch (Exception ex)
            {
                // exception loglama
                return new Result<int> { Success = true, Data = ID, Message = "Bir hata oluştu. Detaylar için logları kontrol edin." };
            }
        }

        public Result<ExamSelectVM> GetByID(int ID)
        {
            ExamSelectVM VM = null;
            try
            {
                Exam entity = _unitOfWork.ExamRepository.GetByID(ID);
                VM = _mapper.Map<Exam, ExamSelectVM>(entity);

                return new Result<ExamSelectVM> { Success = true, Data = VM };
            }
            catch (Exception ex)
            {
                // exception loglama
                return new Result<ExamSelectVM> { Success = false, Data = VM, Message = "Bir hata oluştu. Detaylar için logları kontrol edin." };
            }
        }

        public Result<List<ExamSelectVM>> GetAll()
        {
            List<ExamSelectVM> VM = new List<ExamSelectVM>();
            try
            {
                List<Exam> entity = _unitOfWork.ExamRepository.GetAll().ToList();

                entity.ForEach(x =>
                {
                    VM.Add(_mapper.Map<Exam, ExamSelectVM>(x));
                });

                return new Result<List<ExamSelectVM>> { Success = true, Data = VM };
            }
            catch (Exception ex)
            {
                // exception loglama
                return new Result<List<ExamSelectVM>> { Success = false, Data = VM, Message = "Bir hata oluştu. Detaylar için logları kontrol edin." };
            }
        }

        public Result<List<ExamSelectVM>> GetAllWithStatus()
        {
            List<ExamSelectVM> VM = null;
            try
            {
                var entity = _unitOfWork.ExamRepository.GetAllWithStatus().ToList();


                //VM = entity.Select(x=> _mapper.Map<Exam, ExamSelectVM>(x)).ToList();

                VM = _mapper.Map<List<Exam>, List<ExamSelectVM>>(entity);

                return new Result<List<ExamSelectVM>> { Success = true, Data = VM };
            }
            catch (Exception ex)
            {
                // exception loglama
                return new Result<List<ExamSelectVM>> { Success = false, Data = VM, Message = "Bir hata oluştu. Detaylar için logları kontrol edin." };
            }
        }

        public Result<List<ExamSelectVM>> GetExamsByClassId(int classId)
        {
            List<ExamSelectVM> VM = null;
            try
            {
                var entity = _unitOfWork.ExamRepository.GetExamsByClassId(classId).ToList();

                VM = _mapper.Map<List<Exam>, List<ExamSelectVM>>(entity);

                return new Result<List<ExamSelectVM>> { Success = true, Data = VM };
            }
            catch (Exception ex)
            {
                // exception loglama
                return new Result<List<ExamSelectVM>> { Success = false, Data = VM, Message = "Bir hata oluştu. Detaylar için logları kontrol edin." };
            }
        }

        public Result<bool> AddExamGrades(StudentExamSelectVM vm)
        {
            try
            {
                var entity = _mapper.Map<StudentExamSelectVM, StudentExam>(vm);

                bool state = _unitOfWork.ExamRepository.AddExamGrades(entity);

                return new Result<bool> { Success = true, Data = state };
            }
            catch (Exception ex)
            {
                // exception loglama
                return new Result<bool> { Success = false, Data = false, Message = "Bir hata oluştu. Detaylar için logları kontrol edin." };
            }
        }

        public Result<bool> UpdateExamGrades(StudentExamSelectVM vm)
        {
            try
            {
                var entity = _mapper.Map<StudentExamSelectVM, StudentExam>(vm);

                bool state = _unitOfWork.ExamRepository.UpdateExamGrades(entity);

                return new Result<bool> { Success = true, Data = state };
            }
            catch (Exception ex)
            {
                // exception loglama
                return new Result<bool> { Success = false, Data = false, Message = "Bir hata oluştu. Detaylar için logları kontrol edin." };
            }
        }
    }
}
