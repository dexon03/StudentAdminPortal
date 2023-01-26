using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StudentAdminPortal.API.DomainModels;
using StudentAdminPortal.API.Repositories;
using DataModels = StudentAdminPortal.API.DataModels;

namespace StudentAdminPortal.API.Controllers;
[ApiController]
public class StudentsController : Controller
{
    private IStudentRepository _studentRepository;
    private readonly IMapper _mapper;
    private readonly IImageRepository _imageRepository;

    public StudentsController(IStudentRepository studentRepository,IMapper mapper, IImageRepository imageRepository)
    {
        _studentRepository = studentRepository;
        _mapper = mapper;
        _imageRepository = imageRepository;
    }
    
    [HttpGet]
    [Route("[controller]")]
    public async Task<IActionResult> GetAllStudentsAsync()
    {
        var students = await  _studentRepository.GetStudentsAsync();
        
        return Ok(_mapper.Map<List<Student>>(students));
    }


    [HttpGet]
    [Route("[controller]/{studentId:guid}"), ActionName("GetStudentAsync")]
    public async Task<IActionResult> GetStudentAsync([FromRoute] Guid studentId)
    {
        // Fetch student detail
        var student = await  _studentRepository.GetStudentAsync(studentId);

        // Return student
        return student is null ? NotFound() : Ok(_mapper.Map<Student>(student));
    }

    [HttpPut]
    [Route("[controller]/{studentId:guid}")]
    public async Task<IActionResult> UpdateStudentAsync([FromRoute] Guid studentId, [FromBody] UpdateStudentRequest request)
    {
        if (await _studentRepository.Exists(studentId))
        {
            // Update student
            var updatedStudent = await _studentRepository.UpdateStudent(studentId, _mapper.Map<DataModels.Student>(request));
            if (updatedStudent != null)
            {
                return Ok(_mapper.Map<Student>(updatedStudent));
            }
        }
        return NotFound();
    }

    [HttpDelete]
    [Route("[controller]/{studentId:guid}")]
    public async Task<IActionResult> DeleteStudentAsync([FromRoute] Guid studentId)
    {
        if (await _studentRepository.Exists(studentId))
        {
            var deletedStudent = await _studentRepository.DeleteStudent(studentId);
            return Ok(_mapper.Map<Student>(deletedStudent));
        }
        return NotFound();
    }

    [HttpPost]
    [Route("[controller]/Add")]
    public async Task<IActionResult> AddStudentAsync([FromBody] CreateStudentRequest request)
    {
        var student = await _studentRepository.CreateStudent(_mapper.Map<DataModels.Student>(request));
        return CreatedAtAction(nameof(GetStudentAsync), new { studentId = student.Id }, _mapper.Map<Student>(student));
    }

    [HttpPost]
    [Route("[controller]/{studentId:guid}/upload-image")]
    public async Task<IActionResult> UploadImage([FromRoute] Guid studentId, IFormFile profileImage)
    {
        var validExtensions = new List<string> { ".png", ".jpeg", ".jpg", ".gif", };
        if (profileImage != null && profileImage.Length > 0)
        {
            var extension = Path.GetExtension(profileImage.FileName);
            if (validExtensions.Contains(extension))
            {
                if (await _studentRepository.Exists(studentId))
                {
                    var fileName = Guid.NewGuid() + Path.GetExtension(profileImage.FileName);
                    var fileImagePath = await _imageRepository.Upload(profileImage, fileName);
                    if (await _studentRepository.UpdateProfileImage(studentId, fileImagePath))
                    {
                        return Ok(fileImagePath);
                    }

                    return StatusCode(StatusCodes.Status500InternalServerError, "Error while uploading profile image");
                }
            }
            return BadRequest("This is not a valid Image format");
        }

        return NotFound();

    }
}