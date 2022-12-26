using EssentialDatabase.Core.Entities.Columns.Abstractions;
using EssentialDatabase.Core.Entities.Columns.Enums;

namespace EssentialDatabase.Core.Columns;
public class StringColumn : Column
{
    public new ColumnType Type { get; } = ColumnType.String;
    public StringColumn(string name) : base(name) { }

    public bool Validate(string value) => true;
}