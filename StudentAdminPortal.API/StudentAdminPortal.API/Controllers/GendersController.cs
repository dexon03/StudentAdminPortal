using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StudentAdminPortal.API.DomainModels;
using StudentAdminPortal.API.Repositories;

namespace StudentAdminPortal.API.Controllers;

[ApiController]
public class GendersController : Controller
{
    private IStudentRepository _genderRepository;
    private readonly IMapper _mapper;

    public GendersController(IStudentRepository genderRepository, IMapper mapper)
    {
        _genderRepository = genderRepository;
        _mapper = mapper;
    }
    
    [HttpGet]
    [Route("[controller]")]
    public async Task<IActionResult> GetAllGenders()
    {
        var genders = await _genderRepository.GetGendersAsync();

        if (genders is null || !genders.Any())
        {
            return NotFound();
        }
        
        return Ok(_mapper.Map<List<Gender>>(genders));
    }   
    
}