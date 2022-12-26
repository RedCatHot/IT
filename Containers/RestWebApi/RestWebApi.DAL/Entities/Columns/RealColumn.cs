using RestWebApi.DAL.Entities.Columns.Abstractions;
using RestWebApi.DAL.Entities.Columns.Enums;

namespace RestWebApi.DAL.Columns;

public class RealColumn : Column
{
    public new ColumnType Type { get; } = ColumnType.Real;
    public RealColumn(string name) : base(name) { }

    public bool Validate(string value) => double.TryParse(value, out _);
}