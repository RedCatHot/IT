using EssentialDatabase.Core.Entities.Columns.Abstractions;

namespace EssentialDatabase.Core.Entities;

public class Table
{
    public string Name { get; set; }
    public ICollection<Row> Rows { get; set; } = new List<Row>();
    public IList<Column> Columns { get; set; } = new List<Column>();

    public Table(string name) => Name = name;
    public Table() { }
}
