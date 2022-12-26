using EssentialDatabase.Core.Entities.Columns.Abstractions;
using EssentialDatabase.Core.Entities.Columns.Enums;

namespace EssentialDatabase.Core.Columns;

public class RealColumn : Column
{
    public new ColumnType Type { get; } = ColumnType.Real;
    public RealColumn(string name) : base(name) { }

    public bool Validate(string value) => double.TryParse(value, out _);
}