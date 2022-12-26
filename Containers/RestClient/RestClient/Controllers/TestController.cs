using Microsoft.AspNetCore.Mvc;
using RestClient.Services;

namespace RestWebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{
    private Client _apiClient;
    public TestController()
    {
        _apiClient = new Client("http://displayit-app-test.centralus.azurecontainer.io:7070/", new HttpClient());
    }

    [HttpGet]
    public async Task<ICollection<Database>> GetAllDatabases()
    {
        
        return await _apiClient.DatabaseAllAsync();
    }
}