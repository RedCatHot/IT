using EssentialDatabase.Core.Entities.Columns.Abstractions;
using EssentialDatabase.Core.Entities.Columns.Enums;
using System.Drawing;

namespace EssentialDatabase.Core.Columns;

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