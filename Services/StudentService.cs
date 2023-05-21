using System.Collections.Generic;
using System.Threading.Tasks;
using HogwartsPotions.Models;
using HogwartsPotions.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace HogwartsPotions.Services;

public class StudentService : IStudentService
{
    private readonly HogwartsContext _context;


    public StudentService(HogwartsContext context)
    {
        _context = context;
    }

    public async Task<Student> AddStudentToList(Student student)
    {
        _context.Students.Add(student);
        await _context.SaveChangesAsync();
        return student;
    }


    public async Task<Student> GetStudentByName(string name)
    {
        var student = await _context.Students.FindAsync(name);
        return student;
    }

    public async Task<List<Student>> GetStudents()
    {
        return await _context.Students.ToListAsync();
    }
}