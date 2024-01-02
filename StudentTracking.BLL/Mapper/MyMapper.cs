using AutoMapper;
using StudentTracking.CORE.Entities;
using StudentTracking.VM.Exam;
using StudentTracking.VM.Question;
using StudentTracking.VM.Student;
using System;
using System.Linq;

namespace StudentTracking.BLL.Mapper
{
    public class MyMapper 
    {
        public TDestination Map<TSource, TDestination>(TSource source)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MapperProfile>();        
            });

            var mapper = config.CreateMapper();

            return mapper.Map<TSource, TDestination>(source);
        }
    }
}
