using AutoMapper;
using HR.Domain.Classes;
using HR.Domain.DTOs.Employee;
using HR.Domain.DTOs.PerformanceReview;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Services.Mapping.PerformanceReviews
{
    public class PerformanceReviewMapper : Profile
    {
        public PerformanceReviewMapper()
        {
            AddUserMapping();
        }
        public void AddUserMapping()
        {
            CreateMap<EditPerformanceReviewDTO, PerformanceReview>();
            CreateMap<AddPerformanceReviewDTO, PerformanceReview>();
        }
    }
}
