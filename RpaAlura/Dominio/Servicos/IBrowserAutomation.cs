using RpaAlura.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpaAlura.Dominio.Servicos
{
    public interface IBrowserAutomation
    {
        List<Course> Search(string term);
    }
}

