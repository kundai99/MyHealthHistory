using System;
using System.Collections.Generic;

namespace MedicalDatabase.Models;

public partial class Patientrecord
{
    public long Prid { get; set; }

    public string? Name { get; set; }

    public string? Surname { get; set; }

    public long? Age { get; set; }

    public string? Idnumber { get; set; }
}
