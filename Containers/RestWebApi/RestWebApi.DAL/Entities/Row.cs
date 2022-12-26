namespace RestWebApi.DAL;

public class Row : BaseEntity
{
    public Guid TableId { get; set; }
    public Table Table { get; set; }

}
