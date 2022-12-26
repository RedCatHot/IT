using EssentialDatabase.Core.Entities.Columns.Enums;
using System.Xml.Linq;

namespace EssentialDatabase.Core.Entities.Columns.Abstractions;

public class Column
{
    public Column NextColumn { get; set; }
    public string Name { get; set; }
    public ColumnType Type { get; }

    public Column(string name)
    {
        Name = name;
    }
    public Column() { }

    public bool Validate(string value) => string.IsNullOrWhiteSpace(Name);
}