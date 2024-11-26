using FML.Core.DomainObjects;
using System.ComponentModel.DataAnnotations.Schema;

public abstract class Entity
{
    public Guid Id { get; set; }
}

public class Family : Entity, IAggregateRoot
{
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public bool IsActive { get; set; }
    public string History { get; set; }

    public List<Relative> Relatives { get; set; }
    public List<House> Houses { get; set; }
}

public class Relative : Entity, IAggregateRoot
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
    public DateTime? DeathDate { get; set; }
    public Gender Gender { get; set; }
    public Guid FamilyId { get; set; }
    public Guid HouseId { get; set; }
    public Guid? FatherId { get; set; }
    public Guid? MotherId { get; set; }
    public bool IsActive { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public bool Patriarch { get; set; } = false;
    public bool Matriarch { get; set; } = false;
    public bool IsAlive { get; set; } = true;

    public string? LinkName { get; set; }
    public bool SecretSanta { get; set; } = false;

    public Guid? Spouse { get; set; }
    public Relative? SpouseObj { get; set; }
    public List<Relative>? Children { get; set; }

    // Navigation properties
    public Family? Family { get; set; }
    public House? House { get; set; }

    // Novo método para definir o HouseId
    public void SetHouseId(House house)
    {
        if (house != null)
        {
            HouseId = house.Id;
        }
    }
}

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

public enum Gender
{
    Male,
    Female,
    Other
}
