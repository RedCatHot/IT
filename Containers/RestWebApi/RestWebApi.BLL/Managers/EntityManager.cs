using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using RestWebApi.BLL.Abstractions;
using RestWebApi.DAL;
using RestWebApi.DAL.LinkGeneratorEntities;

namespace RestWebApi.BLL.Managers
{
    public class EntityManager<Entity> : IEntityManager<Entity> where Entity : BaseEntity, new()
    {
        public RestWebApiDbContext _dbContext { get; set; }
        private LinkGenerator _linkGenerator;


        public EntityManager(RestWebApiDbContext dbContext, LinkGenerator linkGenerator)
        {
            _dbContext = dbContext;
            _linkGenerator = linkGenerator;
        }

        public LinkCollectionWrapper<Entity> GetAll(HttpContext httpContext)
        {
            var entities = _dbContext.Set<Entity>().AsNoTracking().ToList();

            for (var index = 0; index < entities.Count(); index++)
            {
                var entityLinks = CreateLinksForEntity(httpContext, entities[index].Id);
                entities[index].Links.AddRange(entityLinks);
            }
            var entitiesWrapper = new LinkCollectionWrapper<Entity>(entities);
            return CreateLinksForEntities(httpContext, entitiesWrapper);
        }

        public async Task<Entity?> GetByIdAsync(HttpContext httpContext, Guid Id)
        {
            return await _dbContext.Set<Entity>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<Entity> CreateAsync(Entity model)
        {
            _dbContext.Set<Entity>().Add(model);
            await _dbContext.SaveChangesAsync();
            return model;
        }

        public async Task<Entity> UpdateAsync(Entity model)
        {
            _dbContext.Set<Entity>().Update(model);
            await _dbContext.SaveChangesAsync();
            return model;
        }

        public async Task<Entity> DeleteAsync(Entity model)
        {
            _dbContext.Set<Entity>().Remove(model);
            await _dbContext.SaveChangesAsync();
            return model;
        }

        private IEnumerable<Link> CreateLinksForEntity(HttpContext httpContext, Guid id)
            => new List<Link>
            {
                new Link(_linkGenerator.GetUriByAction(httpContext, nameof(GetByIdAsync), values: new { id }), "self","GET"),
                new Link(_linkGenerator.GetUriByAction(httpContext, nameof(DeleteAsync)), "delete_entity", "DELETE"),
                new Link(_linkGenerator.GetUriByAction(httpContext, nameof(UpdateAsync)), "update_entity", "PUT"),
                new Link(_linkGenerator.GetUriByAction(httpContext, nameof(CreateAsync)), "create_entity", "POST")
            };

        private LinkCollectionWrapper<Entity> CreateLinksForEntities(HttpContext httpContext, LinkCollectionWrapper<Entity> ownersWrapper)
        {
            ownersWrapper.Links.Add(new Link(_linkGenerator.GetUriByAction(httpContext, nameof(GetAll), values: new { }), "self", "GET"));
            return ownersWrapper;
        }
    }
}

