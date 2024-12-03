using FML.Core.DomainObjects;
using System.ComponentModel.DataAnnotations.Schema;

public class House : Entity, IAggregateRoot
{
    public string Name { get; set; }
    public bool IsActive { get; set; }
    public Guid FamilyId { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? ZipCode { get; set; }

    [NotMapped]
    public Family Family { get; set; }
    [NotMapped]
    public List<Relative> Residents { get; set; }
}
