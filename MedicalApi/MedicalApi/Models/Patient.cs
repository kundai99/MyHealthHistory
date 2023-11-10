using System;
using System.Collections.Generic;

namespace MedicalDatabase.Models;

public partial class Patient
{
    public long PId { get; set; }

    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public string Id { get; set; } = null!;

    public string Sex { get; set; } = null!;

    public string Race { get; set; } = null!;

    public long Age { get; set; }

    public long AccId { get; set; }
}
