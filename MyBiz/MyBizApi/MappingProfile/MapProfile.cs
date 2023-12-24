using AutoMapper;
using MyBizApi.DTO.EmployeeDtos;
using MyBizApi.DTO.ProfessionDtos;
using MyBizApi.Entities;

namespace MyBizApi.MappingProfile
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {

            CreateMap<EmployeeCreateDto, Employee>().ReverseMap();
            CreateMap<EmployeeGetDto, Employee>().ReverseMap();
            CreateMap<EmployeeUpdateDto, Employee>().ReverseMap();


            CreateMap<ProfessionCreateDto, Profession>().ReverseMap();
            CreateMap<ProfessionGetDto, Profession>().ReverseMap();
            CreateMap<ProfessionUpdateDto, Profession>().ReverseMap();
        }
    }
}
