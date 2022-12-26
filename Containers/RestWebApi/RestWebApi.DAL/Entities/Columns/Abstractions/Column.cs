using RestWebApi.DAL.Entities.Columns.Enums;

namespace RestWebApi.DAL.Entities.Columns.Abstractions;

public class Column : BaseEntity
{
    public Guid NextColumnId { get; set; }
    public string Name { get; set; }
    public ColumnType Type { get; }

    public Guid TableId { get; set; }
    public Table Table { get; set; }

    public Column(string name)
    {
        Name = name;
    }
    public Column() { }

    public bool Validate(string value) => string.IsNullOrWhiteSpace(Name);
}