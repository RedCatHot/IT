using EssentialDatabase.Core.Entities.Columns.Abstractions;
using EssentialDatabase.Core.Entities.Columns.Enums;

namespace EssentialDatabase.Core.Columns;

public class IntColumn : Column
{
    public new ColumnType Type { get; } = ColumnType.Int;
    public IntColumn(string name) : base(name) { }

    public bool Validate(string value) => int.TryParse(value, out _);
}