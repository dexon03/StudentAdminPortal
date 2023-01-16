using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StudentAdminPortal.API.Repositories;
using Student = StudentAdminPortal.API.DomainModels.Student;

namespace StudentAdminPortal.API.Controllers;
[ApiController]
public class StudentsController : Controller
{
    private IStudentRepository _repository;
    private readonly IMapper _mapper;

    public StudentsController(IStudentRepository repository,IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    [HttpGet]
    [Route("[controller]")]
    public async Task<IActionResult> GetAllStudentsAsync()
    {
        var students = await  _repository.GetStudentsAsync();
        
        return Ok(_mapper.Map<List<Student>>(students));
    }
    
    
}