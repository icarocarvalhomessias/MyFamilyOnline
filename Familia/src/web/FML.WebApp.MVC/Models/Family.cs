using FML.Core.DomainObjects;
using System;
using System.Collections.Generic;

public class Family : Entity
{
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public bool IsActive { get; set; }
    public string History { get; set; }

    public List<Relative> Relatives { get; set; }
    public List<House> Houses { get; set; }
}
