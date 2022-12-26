using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestWebApi.BLL.Abstractions;
using RestWebApi.DAL;
using RestWebApi.DAL.Entities.Columns.Abstractions;
using RestWebApi.DAL.LinkGeneratorEntities;

namespace RestWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ColumnController : ControllerBase
    {
        private readonly IEntityManager<Column> _entityManager;

        public ColumnController(IEntityManager<Column> entityManager)
        {
            _entityManager = entityManager;
        }

        [HttpGet]
        public LinkCollectionWrapper<Column> GetAll() => _entityManager.GetAll(HttpContext);

        [HttpGet("{id}")]
        public async Task<Column?> GetById(Guid id) => await _entityManager.GetByIdAsync(HttpContext, id);

        [HttpPost]
        public async Task<Column> Create(Column model) => await _entityManager.CreateAsync(model);

        [HttpPut]
        public async Task<Column> Update(Column model) => await _entityManager.UpdateAsync(model);

        [HttpDelete]
        public async Task<Column> Delete(Column model) => await _entityManager.DeleteAsync(model);
    }
}