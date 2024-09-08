using Microsoft.Extensions.DependencyInjection;
using RpaAlura.Dominio.Servicos;
using RpaAlura.Infraestrutura.InjecaoDependencia;

class Program
{
    static async Task Main(string[] args)
    {
        // Configuração de Injeção de Dependência
        var serviceProvider = ServiceConfig.Configure();
        var formAutomationService = serviceProvider.GetRequiredService<IFormAutomationService>();

        // Inicia a automação
        Console.WriteLine("Digite o termo de buscas:");
        string? searchTerm = Console.ReadLine();

        await formAutomationService.ExecuteFormFillingAsync(searchTerm);
    }
}
