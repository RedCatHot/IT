using EssentialDatabase.Core.Entities.Columns.Abstractions;
using EssentialDatabase.Core.Entities.Columns.Enums;

namespace EssentialDatabase.Core.Columns;

public class CharColumn : Column
{
    public new ColumnType Type { get; } = ColumnType.Char;
    public CharColumn(string name) : base(name) { }

    public bool Validate(string value) => char.TryParse(value, out _);
}