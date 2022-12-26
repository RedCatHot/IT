using RestWebApi.DAL.Entities.Columns.Abstractions;
using RestWebApi.DAL.Entities.Columns.Enums;

namespace RestWebApi.DAL.Columns;

public class IntColumn : Column
{
    public new ColumnType Type { get; } = ColumnType.Int;
    public IntColumn(string name) : base(name) { }

    public bool Validate(string value) => int.TryParse(value, out _);
}