using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestWebApi.BLL.Abstractions;
using RestWebApi.DAL;
using RestWebApi.DAL.LinkGeneratorEntities;

namespace RestWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RowController : ControllerBase
    {
        private readonly IEntityManager<Row> _entityManager;

        public RowController(IEntityManager<Row> entityManager)
        {
            _entityManager = entityManager;
        }

        [HttpGet]
        public LinkCollectionWrapper<Row> GetAll() => _entityManager.GetAll(HttpContext);

        [HttpGet("{id}")]
        public async Task<Row?> GetById(Guid id) => await _entityManager.GetByIdAsync(HttpContext, id);

        [HttpPost]
        public async Task<Row> Create(Row model) => await _entityManager.CreateAsync(model);

        [HttpPut]
        public async Task<Row> Update(Row model) => await _entityManager.UpdateAsync(model);

        [HttpDelete]
        public async Task<Row> Delete(Row model) => await _entityManager.DeleteAsync(model);
    }
}