using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiUsingDapper.Models;
using WebApiUsingDapper.Repository;

namespace WebApiUsingDapper.Controllers
{
    [Route("api/students")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentRepository _studentRepository;

        public StudentsController(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetStudents()
        {
            var students = await _studentRepository.GetStudents();
            
            return Ok(students);
        }
        [HttpGet("{id}",Name ="StudentById")]
        public async Task<IActionResult> GetStudent(int id)
        {
            var student = await _studentRepository.GetStudent(id);
            if(student is null)
            {
                return NotFound();
            }
            return Ok(student);
        }
        [HttpPost]
        public async Task<IActionResult> CreateStudent([FromBody] Student student)
        {
            await _studentRepository.CreateStudent(student);
            return Ok("Student Inserted");
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(int id,[FromBody]Student student)
        {
            var dbStudent = await _studentRepository.GetStudent(id);
            if(dbStudent is null)
            {
                return NotFound();
            }
            await _studentRepository.UpdateStudent(id,student);
            return Ok("Student is Updated"); 
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student = _studentRepository.GetStudent(id);
            if(student is null)
            {
                return NotFound();
            }
            await _studentRepository.DeleteStudent(id);
            return Ok("Student Deleted");
            
        }
    }
}
