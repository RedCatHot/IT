using RestWebApi.DAL.Entities.Columns.Abstractions;
using RestWebApi.DAL.Entities.Columns.Enums;

namespace RestWebApi.DAL.Columns;


public class ColorIntervalColumn : Column
{
    public new ColumnType Type { get; } = ColumnType.ColorInterval;
    public ColorIntervalColumn(string name) : base(name) { }

    //11..123;22..523;11..325
    public bool Validate(string value)
    {
        var rgbIntervalValues = value.Split(';');

        foreach (var rgbIntervalValue in rgbIntervalValues)
        {
            if (rgbIntervalValue.Contains(".."))
            {
                var rgbValues = rgbIntervalValue.Split("..");

                if (rgbValues.Length == 2 && rgbIntervalValues.All(x => int.TryParse(x, out _)))
                    continue;
            }
            return false;
        }

        return true;
    }
}