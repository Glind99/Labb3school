using System;
using System.Collections.Generic;

namespace Labb3school.Models;

public partial class Student
{
    public int StudentId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public int? Phone { get; set; }

    public int? FkClassId { get; set; }

    public DateOnly? Birthdate { get; set; }

    public virtual Class? FkClass { get; set; }

    public virtual ICollection<Grade> Grades { get; set; } = new List<Grade>();
}
