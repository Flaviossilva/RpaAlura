using System.Data.SqlClient;
using RpaAlura.Dominio.Entidades;
using RpaAlura.Dominio.Repositorios;

namespace RpaAlura.Infraestrutura.Persistencia
{
    public class SqlCourseRepository : ICourseRepository
    {
        private readonly string _connectionString;

        public SqlCourseRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task SaveCoursesAsync(List<Course> courses)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                foreach (var course in courses)
                {
                    var command = new SqlCommand("INSERT INTO Courses (Title, Professor, Duration, Description) VALUES (@Title, @Professor, @Duration, @Description)", connection);
                    command.Parameters.AddWithValue("@Title", course.Title);
                    command.Parameters.AddWithValue("@Professor", course.Professor);
                    command.Parameters.AddWithValue("@Duration", course.Duration);
                    command.Parameters.AddWithValue("@Description", course.Description);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
