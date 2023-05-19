using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HogwartsPotions.Models.Entities;
using HogwartsPotions.Services;

namespace HogwartsPotions.Controllers
{
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
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}