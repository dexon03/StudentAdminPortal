using AutoMapper;
using StudentAdminPortal.API.DataModels;

namespace StudentAdminPortal.API.Profiles;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<Student, DomainModels.Student>().ReverseMap();
        
        CreateMap<Gender, DomainModels.Gender>().ReverseMap();
        
        CreateMap<Address,DomainModels.Address>().ReverseMap();
    }
}