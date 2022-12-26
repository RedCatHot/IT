using RestWebApi.DAL.Entities.Columns.Abstractions;
using RestWebApi.DAL.Entities.Columns.Enums;

namespace RestWebApi.DAL.Columns;

public class ColorColumn : Column
{
    public new ColumnType Type { get; } = ColumnType.Color;
    public ColorColumn(string name) : base(name) { }

    //123;523;325
    public bool Validate(string value)
    {
        var rgbValues = value.Split(';');
        
        return rgbValues.Length == 3 && rgbValues.All(x => int.TryParse(x, out _));
    }
}