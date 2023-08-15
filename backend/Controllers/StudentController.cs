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
    public async Task<List<Student>> GetAllStudents()
    {
        return await _studentService.GetStudents();
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
    public async Task<Student> AddStudent([FromBody] Student student)
    {
        await _studentService.AddStudentToList(student);
        return student;
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