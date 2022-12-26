using RestWebApi.DAL.Entities.Columns.Abstractions;
using RestWebApi.DAL.Entities.Columns.Enums;

namespace RestWebApi.DAL;
public class StringColumn : Column
{
    public new ColumnType Type { get; } = ColumnType.String;
    public StringColumn(string name) : base(name) { }

    public bool Validate(string value) => true;
}