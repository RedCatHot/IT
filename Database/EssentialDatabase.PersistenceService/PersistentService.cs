using Database.Core.Entities;
using EssentialDatabase.PersistenceService.Abstractions;
using System.Text.Json;

namespace EssentialDatabase.PersistenceService;

public class PersistenceService : IPersistenceService
{
    public string FilePath { get; protected set; }

    public PersistenceService(string filePath) => FilePath = filePath;

    public async Task SaveDatabasesAsync(ICollection<Db> databases) => await File.WriteAllTextAsync(FilePath, JsonSerializer.Serialize(databases));

    public async Task<ICollection<Db>?> RetrieveDatabasesAsync(string filePath) => JsonSerializer.Deserialize<List<Db>>(await File.ReadAllTextAsync(filePath));
}
