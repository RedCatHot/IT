using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using RestWebApi.DAL;
using RestWebApi.DAL.LinkGeneratorEntities;

namespace RestWebApi.BLL.Abstractions
{
    public interface IEntityManager<Entity> where Entity : BaseEntity, new()
    {
        LinkCollectionWrapper<Entity> GetAll(HttpContext httpContext);

        Task<Entity?> GetByIdAsync(HttpContext httpContext, Guid Id);

        Task<Entity> CreateAsync(Entity model);

        Task<Entity> UpdateAsync(Entity model);

        Task<Entity> DeleteAsync(Entity model);
    }
}
