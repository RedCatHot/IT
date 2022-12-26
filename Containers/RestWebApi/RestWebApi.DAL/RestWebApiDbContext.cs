using Microsoft.EntityFrameworkCore;
using RestWebApi.DAL.Entities.Columns.Abstractions;

namespace RestWebApi.DAL;

public class RestWebApiDbContext : DbContext
{
    public RestWebApiDbContext(DbContextOptions options) : base(options) {}

    #region Entities

    public DbSet<Database> Databases { get; set; }
    public DbSet<Table> Tables { get; set; }
    public DbSet<Column> Columns { get; set; }
    public DbSet<Row> Row { get; set; }

    #endregion Entities

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}