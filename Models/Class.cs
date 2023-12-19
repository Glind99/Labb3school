using System;
using System.Collections.Generic;

namespace Labb3school.Models;

public partial class Class
{
    public string? ClassName { get; set; }

    public int ClassId { get; set; }

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
