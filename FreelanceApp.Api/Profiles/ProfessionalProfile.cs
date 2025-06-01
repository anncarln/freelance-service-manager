using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FreelanceApp.Api.Dtos;
using FreelanceApp.Api.Models;

namespace FreelanceApp.Api.Profiles
{
    public class ProfessionalProfile : Profile
    {
        public ProfessionalProfile()
        {
            CreateMap<Professional, ReadProfessionalDto>()
                .ForMember(dest => dest.Specialty, opt => opt.MapFrom(scr => scr.Specialty));

            CreateMap<CreateProfessionalDto, Professional>();

            CreateMap<Specialty, ReadSpecialtyDto>();
        }
    }
}