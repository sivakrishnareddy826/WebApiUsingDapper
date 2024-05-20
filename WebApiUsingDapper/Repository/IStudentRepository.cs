using WebApiUsingDapper.Models;

namespace WebApiUsingDapper.Repository
{
    public interface IStudentRepository
    {
        public Task<IEnumerable<Student>> GetStudents();
        public Task<Student> GetStudent(int id);
        public Task CreateStudent(Student student);
        public Task UpdateStudent(int id,Student student);
        public Task DeleteStudent(int id);
    }
}
