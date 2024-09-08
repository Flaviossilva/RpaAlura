using RpaAlura.Dominio.Repositorios;
using RpaAlura.Dominio.Servicos;
using RpaAlura.Infraestrutura.Automacao;

namespace RpaAlura.Rpa
{
    public class FormAutomationService : IFormAutomationService
    {
        private readonly IBrowserAutomation _browserAutomation;
        private readonly ICourseRepository _courseRepository;

        public FormAutomationService(IBrowserAutomation browserAutomation, ICourseRepository courseRepository)
        {
            _browserAutomation = browserAutomation;
            _courseRepository = courseRepository;
        }

        public async Task ExecuteFormFillingAsync(string searchTerm)
        {
            try
            {
              
                var courses = _browserAutomation.SearchAsync(searchTerm);
                await _courseRepository.SaveCoursesAsync(courses);
                Console.WriteLine("Automação concluída com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
            }
        }
    }
}
