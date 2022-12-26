using EssentialDatabase.Core.Entities;
using EssentialDatabase.Management.Abstractions;
using System.Text.Json;

namespace EssentialDatabase.Management;

public class PersistenceService : IPersistenceService
{
    public string FilePath { get; protected set; }

    public PersistenceService(string filePath) => FilePath = filePath;

    public async Task SaveDatabasesAsync(ICollection<Database> databases) => await File.WriteAllTextAsync(FilePath, JsonSerializer.Serialize(databases));

    public async Task<ICollection<Database>?> RestoreDatabasesAsync() => JsonSerializer.Deserialize<List<Database>>(await File.ReadAllTextAsync(FilePath));
}
