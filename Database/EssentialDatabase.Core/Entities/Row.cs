namespace EssentialDatabase.Core.Entities;

public class Row
{
    public Dictionary<string, string?> Values { get; set; } = new();

    public string? this[string columnName]
    {
        get => Values[columnName];
        set => Values[columnName] = value;
    }
}
