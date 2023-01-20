using Microsoft.EntityFrameworkCore;
using StudentAdminPortal.API.DataModels;
using Address = StudentAdminPortal.API.DomainModels.Address;

namespace StudentAdminPortal.API.Repositories;

public class SqlStudentRepository : IStudentRepository
{
    readonly StudentAdminContext _context;

    public SqlStudentRepository(StudentAdminContext context)
    {
        _context = context;
    }
    public async Task<List<Student?>> GetStudentsAsync()
    {
       return await _context.Students.Include(nameof(Gender)).Include(nameof(Address)).ToListAsync();
    }

    public async Task<Student> GetStudentAsync(Guid studentId)
    {
        return await _context.Students
            .Include(nameof(Gender))
            .Include(nameof(Address))
            .FirstOrDefaultAsync(x => x.Id == studentId);
    }
}