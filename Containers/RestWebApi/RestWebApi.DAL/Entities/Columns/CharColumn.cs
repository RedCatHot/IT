using RestWebApi.DAL.Entities.Columns.Abstractions;
using RestWebApi.DAL.Entities.Columns.Enums;

namespace RestWebApi.DAL.Columns;

public class CharColumn : Column
{
    public new ColumnType Type { get; } = ColumnType.Char;
    public CharColumn(string name) : base(name) { }

    public bool Validate(string value) => char.TryParse(value, out _);
}