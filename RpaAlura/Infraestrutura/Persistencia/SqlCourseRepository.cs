using System.Data.SqlClient;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
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
                    if (ExistRegister(course))
                        continue;
                    var command = new SqlCommand("INSERT INTO Courses (Titulo, Professor, Carga_Horaria, Descricao) VALUES (@Titulo, @Professor, @Carga_Horaria, @Descricao)", connection);
                    command.Parameters.AddWithValue("@Titulo", course.Title);
                    command.Parameters.AddWithValue("@Professor", course.Professor);
                    command.Parameters.AddWithValue("@Carga_Horaria", course.Duration);
                    command.Parameters.AddWithValue("@Descricao", (course.Description));
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public bool ExistRegister(Course course)
        {
            int ret=0;
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand($"Select top 1 Count(*) from Courses where titulo='{course.Title}'",connection);
                command.ExecuteNonQuery();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read()) 
                {
                    ret=(int)reader[0];
                }
                if (ret == 1)
                    return true;
                return false;

            }
        }

    }
}
