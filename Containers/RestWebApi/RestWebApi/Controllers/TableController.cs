using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestWebApi.BLL.Abstractions;
using RestWebApi.DAL;
using RestWebApi.DAL.LinkGeneratorEntities;

namespace RestWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TableController : ControllerBase
    {
        private readonly IEntityManager<Table> _entityManager;

        public TableController(IEntityManager<Table> entityManager)
        {
            _entityManager = entityManager;
        }

        [HttpGet]
        public LinkCollectionWrapper<Table> GetAll() => _entityManager.GetAll(HttpContext);

        [HttpGet("{id}")]
        public async Task<Table?> GetById(Guid id) => await _entityManager.GetByIdAsync(HttpContext, id);

        [HttpPost]
        public async Task<Table> Create(Table model) => await _entityManager.CreateAsync(model);

        [HttpPut]
        public async Task<Table> Update(Table model) => await _entityManager.UpdateAsync(model);

        [HttpDelete]
        public async Task<Table> Delete(Table model) => await _entityManager.DeleteAsync(model);
    }
}