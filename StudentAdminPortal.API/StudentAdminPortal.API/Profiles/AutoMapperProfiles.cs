using AutoMapper;
using StudentAdminPortal.API.DomainModels;
using StudentAdminPortal.API.Profiles.AfterMaps;
using Address = StudentAdminPortal.API.DataModels.Address;
using Gender = StudentAdminPortal.API.DataModels.Gender;
using Student = StudentAdminPortal.API.DataModels.Student;

namespace StudentAdminPortal.API.Profiles;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<Student, DomainModels.Student>().ReverseMap();
        
        CreateMap<Gender, DomainModels.Gender>().ReverseMap();
        
        CreateMap<Address,DomainModels.Address>().ReverseMap();

        CreateMap<UpdateStudentRequest, Student>().AfterMap<UpdateStudentRequestAfterMap>();

    }
}