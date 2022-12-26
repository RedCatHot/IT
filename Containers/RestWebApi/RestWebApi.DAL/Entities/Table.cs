using RestWebApi.DAL.Entities.Columns.Abstractions;

namespace RestWebApi.DAL;

public class Table : BaseEntity
{
    public string Name { get; set; }
    public ICollection<Row> Rows { get; set; } = new List<Row>();
    public IList<Column> Columns { get; set; } = new List<Column>();

    public Guid DatabaseId { get; set; }
    public Database Database { get; set; }

    public Table(string name) => Name = name;
    public Table() { }
}
