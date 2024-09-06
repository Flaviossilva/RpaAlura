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
                    var command = new SqlCommand("INSERT INTO Courses (Titulo, Professor, Carga_Horaria, Descricao) VALUES (@Titulo, @Professor, @Carga_Horaria, @Descricao)", connection);
                    command.Parameters.AddWithValue("@Titulo", course.Title);
                    command.Parameters.AddWithValue("@Professor", course.Professor);
                    command.Parameters.AddWithValue("@Carga_Horaria", course.Duration);
                    command.Parameters.AddWithValue("@Descricao", course.Description);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
