using System;
using System.Collections.Generic;

namespace Labb3school.Models;

public partial class Employee
{
    public int EmployeeId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public int? FkProfessionId { get; set; }

    public virtual Profession? FkProfession { get; set; }

    public virtual ICollection<Grade> Grades { get; set; } = new List<Grade>();
}
