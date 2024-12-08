using FML.Core.DomainObjects;
using System;
using System.Collections.Generic;

public class House : Entity
{
    public string Name { get; set; }
    public bool IsActive { get; set; }
    public Guid FamilyId { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? ZipCode { get; set; }

    public Family Family { get; set; }
    public List<Relative> Residents { get; set; }
}
