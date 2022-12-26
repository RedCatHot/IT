using System.Xml.Linq;

namespace EssentialDatabase.Core.Entities;

public class Database
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public ICollection<Table> Tables { get; set; } = new List<Table>();

    public Database(string name) => Name = name;
    public Database() { }
}
