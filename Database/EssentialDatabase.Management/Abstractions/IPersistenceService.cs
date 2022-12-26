using EssentialDatabase.Core.Entities;

namespace EssentialDatabase.Management.Abstractions;

public interface IPersistenceService
{
    Task SaveDatabasesAsync(ICollection<Database> databases);

    Task<ICollection<Database>?> RestoreDatabasesAsync();
}
