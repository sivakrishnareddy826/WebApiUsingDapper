using Dapper;
using System.Data;
using WebApiUsingDapper.Data;
using WebApiUsingDapper.Models;

namespace WebApiUsingDapper.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly DapperContext _context;

        public StudentRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task CreateStudent(Student student)
        {
            var query = "INSERT INTO student (Name,Email) VALUES(@Name,@Email)";
            var parameters = new DynamicParameters();
            parameters.Add("Name", student.Name,DbType.String);
            parameters.Add("Email",student.Email,DbType.String);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }

        }

        public async Task DeleteStudent(int id)
        {
            var query = "DELETE FROM student where Id=@id";
            using(var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query,new { id });
            }
        }

        public async Task<Student> GetStudent(int id)
        {
            var query = "SELECT * FROM student WHERE Id = @Id";
            using(var connection = _context.CreateConnection())
            {
                var student = await connection.QueryFirstOrDefaultAsync<Student>(query,new {id});
                return student;
            }
        }

        public async Task<IEnumerable<Student>> GetStudents()
        {
            var query = "SELECT * FROM student";

            using(var connection = _context.CreateConnection())
            {
                var students = await connection.QueryAsync<Student>(query);
                return students.ToList();
            }
        }

        public async Task UpdateStudent(int id, Student student)
        {
            var query = "UPDATE student SET Name = @Name,Email = @Email where Id = @Id";
            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32);
            parameters.Add("Name",student.Name,DbType.String);
            parameters.Add("Email",student.Email, DbType.String);
            using(var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
    }
}
