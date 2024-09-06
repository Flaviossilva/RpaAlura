using RpaAlura.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpaAlura.Dominio.Repositorios
{
    public interface ICourseRepository
    {
        Task SaveCoursesAsync(List<Course> courses);
    }
}

