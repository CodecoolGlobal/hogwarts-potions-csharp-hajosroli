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
        if (await _context.Students.AnyAsync(s => s.Name == student.Name))
        {
            return null;
        }
        _context.Students.Add(student);
        await _context.SaveChangesAsync();
        return student;
    }


    public async Task<Student> GetStudentByName(string name)
    {
        var student = await _context.Students.FindAsync(name);
        return student;
    }

    public async Task<bool> DeleteStudent(long id)
    {
        var student = await _context.Students.FindAsync(id);
        if (student is null) return false;
        _context.Students.Remove(student);
        await _context.SaveChangesAsync();
        return true;

    }

    public async Task<List<Student>> GetStudents()
    {
        return await _context.Students.ToListAsync();
    }
}