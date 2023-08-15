using System.Collections.Generic;
using System.Threading.Tasks;
using HogwartsPotions.Models.Entities;

namespace HogwartsPotions.Services;

public interface IStudentService

{
    Task<Student> AddStudentToList(Student student);
    Task<List<Student>> GetStudents();
    Task<Student> GetStudentByName(string name);

    Task<bool> DeleteStudent(long id);
}