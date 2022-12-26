using RestWebApi.DAL.LinkGeneratorEntities;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestWebApi.DAL;

public abstract class BaseEntity
{
    public Guid Id { get; set; }

    [NotMapped]
    public List<Link> Links { get; set; } = new List<Link>();
}
