using FML.Core.DomainObjects;

public class Family : Entity, IAggregateRoot
{
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public bool IsActive { get; set; }
    public string History { get; set; }

    public List<Familiar> Relatives { get; set; }
    public List<House> Houses { get; set; }
}
