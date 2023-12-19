using System;
using System.Collections.Generic;

namespace Labb3school.Models;

public partial class Profession
{
    public int ProfessionId { get; set; }

    public string? ProfessionName { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
