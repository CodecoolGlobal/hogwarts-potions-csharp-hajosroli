using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HogwartsPotions.Models.Entities;
using HogwartsPotions.Models.Enums;
using HogwartsPotions.Services;
using Microsoft.AspNetCore.Mvc;

namespace HogwartsPotions.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StudentController : ControllerBase
{
    private readonly IStudentService _studentService;

    public StudentController(IStudentService studentService)
    {
        _studentService = studentService;
    }


    // GET: api/Student/getAllStudents
    [HttpGet("getAllStudents")]
    public async Task<ActionResult<List<Student>>> GetAllStudents()
    {
       var result = await _studentService.GetStudents();
       if (result is null) return NotFound("No Students");
       return Ok(result);
    }

    // GET: api/Student/5
    [HttpGet("{id}", Name = "Get")]
    public string Get(int id)
    {
        return "value";
    }
    
    // GET: api/Student/houseTypes
    [HttpGet("houseTypes")]
    public async Task<ActionResult<List<string>>> GetHouseTypes()
    {
        var types = Enum.GetNames(typeof(HouseType)).ToList();
        return types;
    }

    // GET: api/Student/houseTypes
    [HttpGet("petTypes")]
    public async Task<ActionResult<List<string>>> GetPetTypes()
    {
        var types = Enum.GetNames(typeof(PetType)).ToList();
        return types;
    }
    // POST: api/addStudent
    [HttpPost("addStudent")]
    public async Task<ActionResult<Student>> AddStudent([FromBody] Student student)
    {
        var result = await _studentService.AddStudentToList(student);
        if (result is null) return Conflict("Student with the same is already exists");
        return Ok(student);
    }

    // PUT: api/Student/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE: api/Student/5
    [HttpDelete("deleteStudent/{id}")]
    public async Task<bool> Delete(int id)
    {
        return await _studentService.DeleteStudent(id);
    }
}