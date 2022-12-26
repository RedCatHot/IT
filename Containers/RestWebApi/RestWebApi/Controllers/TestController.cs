using Mapster;
using Microsoft.AspNetCore.Mvc;
using RestWebApi.BLL.Abstractions;
using RestWebApi.DAL;
using RestWebApi.DAL.Entities.Columns.Abstractions;

namespace RestWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public async Task<ICollection<Database>> GetAllDatabases()
        {
            var api = new RestWebApi.Services.Client("http://displayit-app-test.centralus.azurecontainer.io:7070/", new HttpClient());
            
            return (await api.DatabaseAllAsync()).Adapt<ICollection<Database>>();
        }
    }
}