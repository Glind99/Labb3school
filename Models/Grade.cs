using System;
using System.Collections.Generic;

namespace Labb3school.Models;

public partial class Grade
{
    public int GradeId { get; set; }

    public int? Grade1 { get; set; }

    public string? Subject { get; set; }

    public int? FkEmployeeId { get; set; }

    public int? FkStudentId { get; set; }

    public DateOnly? Date { get; set; }

    public virtual Employee? FkEmployee { get; set; }

    public virtual Student? FkStudent { get; set; }
}
