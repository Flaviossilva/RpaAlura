﻿using Microsoft.Extensions.DependencyInjection;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using RpaAlura.Dominio.Repositorios;
using RpaAlura.Dominio.Servicos;
using RpaAlura.Infraestrutura.Automacao;
using RpaAlura.Infraestrutura.Persistencia;
using RpaAlura.Rpa;
using System;
using System.Data.SqlClient;

namespace RpaAlura.Infraestrutura.InjecaoDependencia
{
    public static class ServiceConfig
    {
        public static ServiceProvider Configure()
        {
            var services = new ServiceCollection();

            // Configurar injeção de dependências
            services.AddScoped<IWebDriver, ChromeDriver>();
            services.AddScoped<IBrowserAutomation, SeleniumBrowserAutomation>();
            services.AddScoped<ICourseRepository>(provider =>
            {
                var builder = new SqlConnectionStringBuilder
                {
                    DataSource = "VC0003\\SQLEXPRESS01",
                    InitialCatalog = "RpaAlura",
                    UserID = "Desenvolvedor",
                    TrustServerCertificate = true,
                    Password = "rpaalura"
                };
                return new SqlCourseRepository(builder.ConnectionString);
            }); services.AddScoped<IFormAutomationService, FormAutomationService>();

            return services.BuildServiceProvider();
        }
    }
}
