using Azure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestWebApi.BLL.Abstractions;
using RestWebApi.BLL.Managers;
using RestWebApi.DAL;
using RestWebApi.DAL.Entities.Columns.Abstractions;

namespace RestWebApi.BLL
{
    public static class ProgramExtensions
    {
        public static void AddBusinessLogicLayerDependensies(this IServiceCollection services)
        {
            services.AddManagers();
        }

        private static void AddManagers(this IServiceCollection services)
        {
            services.AddScoped<IEntityManager<Database>, EntityManager<Database>>();
            services.AddScoped<IEntityManager<Table>, EntityManager<Table>>();
            services.AddScoped<IEntityManager<Column>, EntityManager<Column>>();
            services.AddScoped<IEntityManager<Row>, EntityManager<Row>>();
        }
    }
}