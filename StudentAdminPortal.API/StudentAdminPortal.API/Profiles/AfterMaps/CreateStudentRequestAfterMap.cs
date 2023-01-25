using AutoMapper;
using StudentAdminPortal.API.DomainModels;
using DataModels = StudentAdminPortal.API.DataModels.Student;

namespace StudentAdminPortal.API.Profiles.AfterMaps;

public class CreateStudentRequestAfterMap : IMappingAction<CreateStudentRequest, DataModels.Student>
{
    public void Process(CreateStudentRequest source, DataModels.Student destination, ResolutionContext context)
    {
        destination.Id = Guid.NewGuid();
        destination.Address = new DataModels.Address
        {
            Id = Guid.NewGuid(),
            PhysicalAddress = source.PhysicalAddress,
            PostalAddress = source.PostalAddress
        };
    }
}