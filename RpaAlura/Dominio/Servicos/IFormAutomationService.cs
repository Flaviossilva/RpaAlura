using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpaAlura.Dominio.Servicos
{
    public interface IFormAutomationService
    {
        Task ExecuteFormFillingAsync(string searchTerm);
    }
}

