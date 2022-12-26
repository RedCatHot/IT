namespace EssentialDatabase.CLI.Models;

public record Command(string Value)
{
    public ICollection<CommandKey> Keys = new List<CommandKey>();
}