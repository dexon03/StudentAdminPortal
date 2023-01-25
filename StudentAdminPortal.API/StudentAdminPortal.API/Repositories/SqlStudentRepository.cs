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
        return (await _context.Students
            .Include(nameof(Gender))
            .Include(nameof(Address))
            .FirstOrDefaultAsync(x => x.Id == studentId))!;
    }

    public async Task<List<Gender>> GetGendersAsync()
    {
        return await _context.Genders.ToListAsync();
    }

    public async Task<bool> Exists(Guid studentId)
    {
        return _context.Students.AnyAsync(x => x.Id == studentId).Result;
    }

    public async Task<Student> UpdateStudent(Guid studentId, Student request)
    {
        var existingStudent = await GetStudentAsync(studentId);
        if (existingStudent != null)
        {
            existingStudent.FirstName = request.FirstName;
            existingStudent.LastName = request.LastName;
            existingStudent.DateOfBirth = request.DateOfBirth;
            existingStudent.GenderId = request.GenderId;
            existingStudent.Email = request.Email;
            existingStudent.PhoneNumber = request.PhoneNumber;
            existingStudent.Address.PhysicalAddress = request.Address.PhysicalAddress;
            existingStudent.Address.PostalAddress = request.Address.PostalAddress;

            await _context.SaveChangesAsync();
            return existingStudent;
        }

        return null;
    }

    public async Task<Student> DeleteStudent(Guid studentId)
    {
        var student = await GetStudentAsync(studentId);
        if (student != null)
        {
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
        }
        
        return student;
    }

    public async Task<Student> CreateStudent(Student request)
    {
        var student = await _context.Students.AddAsync(request);
        await _context.SaveChangesAsync();
        return student.Entity;
    }

    public async Task<bool> UpdateProfileImage(Guid studentId, string profileImageUrl)
    {
        var student = await GetStudentAsync(studentId);

        if (student != null)
        {
            student.ProfileImageUrl = profileImageUrl;
            await _context.SaveChangesAsync();
            return true;
        }

        return false;
    }
}