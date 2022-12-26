namespace EssentialDatabase.PersistenceService.Abstractions;

public interface IPersistenceService
{

    Task SaveDatabasesAsync(ICollection<Db> databases);

    Task<ICollection<Db>?> RetrieveDatabasesAsync(string filePath);
}
